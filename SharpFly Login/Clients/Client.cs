using System;
using System.Net.Sockets;
using SharpFly_Login.Packets;
using SharpFly_Login.Database;
using SharpFly_Login.Utility;
using SharpFly_Login.Server;
using System.Data;
using System.Collections.Generic;

namespace SharpFly_Login.Clients
{

    delegate void ProcessData(SocketAsyncEventArgs e);

    class Client : IDisposable
    {

        #region Packet headers
        public enum IncomingPacketHeader
        {
            PING = 0x14,
            RELOG_REQUEST = 0x16,
            LOGIN_REQUEST = 0xFC,
            SOCK_FIN = 0xFF
        }

        public enum OutgoingPacketHeader
        {
            SESSION_KEY = 0x00,
            PING = 0x0B,
            SERVER_LIST = 0xFD,
            LOGIN_MESSAGE = 0xFE
        }

        public enum LoginError
        {
            ERROR_SERVICE_DOWN = 0x6D,
            ERROR_ACCOUNT_CONNECTED = 0x67,
            ERROR_ACCOUNT_BANNED = 0x77,
            ERROR_INVALID_PASSWORD = 0x78,
            ERROR_INVALID_USERNAME = 0x79,
            ERROR_VERIFICATION_REQUIRED = 0x7A,
            ERROR_ACCOUNT_MAINTENANCE = 0x85,
            ERROR_SERVER_ERROR = 0x88,
            ERROR_RESOURCE_FALSIFIED = 0x8A
        }
        #endregion

        private int m_PasswordSize = 42;
        private byte[] m_RemainingBytes = null;

        public byte[] Buffer { get; set; }
        public const int BufferSize = 1500;
        public string Password { get; set; }
        public List<byte> ReceivedBytes { get; set; }
        public uint SessionKey { get; set; }
        public Socket Socket { get; set; }
        public string Username { get; set; }

        private Client() { }

        public Client(Socket socket)
        {
            this.Buffer = new byte[BufferSize];
            ReceivedBytes = new List<byte>();
            this.Socket = socket;
            this.SessionKey = (uint)new Random().Next(0, ushort.MaxValue * 5);
            Console.WriteLine("Client " + this.Socket.RemoteEndPoint + " connected!");
            SendSessionKey();
        }

        public void ProcessData()
        {
            try
            {

                // Receive, test
                {
                    if (this.Socket != null)
                    {
                        if (this.Socket.Available <= 0)
                            return;

                        int byteCount = this.Socket.Receive(this.Buffer, this.Buffer.Length, SocketFlags.None);
                        if (byteCount <= 0)
                            return;
                        ReceivedBytes.AddRange(this.Buffer);
                    }
                }

                byte[] data = ReceivedBytes.ToArray();
                IncomingPacket[] packets = IncomingPacket.SplitPackets(data);
                foreach (IncomingPacket packet in packets)
                {
                    if (packet == null)
                        return;

                    if (m_RemainingBytes != null)
                    {
                        byte[] buffer = new byte[m_RemainingBytes.Length + packet.Buffer.Length];
                        Array.Copy(m_RemainingBytes, 0, buffer, 0, m_RemainingBytes.Length);
                        Array.Copy(packet.Buffer, 0, buffer, 0, packet.Buffer.Length);
                        packet.Buffer = buffer;
                        m_RemainingBytes = null;
                    }

                    if (!packet.VerifyHeaders((int)SessionKey))
                        m_RemainingBytes = packet.Buffer;
                    else
                    {
                        packet.Position = 13; // Go to headers
                        uint header = (uint)packet.ReadInt();
                        switch (header)
                        {
                            case (uint)IncomingPacketHeader.LOGIN_REQUEST:
                                LoginRequest(packet);
                                break;
                            case (uint)IncomingPacketHeader.PING:
                                break;
                            case (uint)IncomingPacketHeader.SOCK_FIN:
                                break;
                            case (uint)IncomingPacketHeader.RELOG_REQUEST:
                                RelogRequest(packet);
                                break;
                            default:
                                Console.WriteLine(String.Format("Unknown packet header {0}", header));
                                break;

                        }
                    }
                }
                ReceivedBytes.Clear();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Dispose()
        {
            LoginServer.ClientManager.RemoveUser(this);
            this.Socket.Shutdown(SocketShutdown.Both);
            this.Socket.Close();
        }

        #region Incoming packets
        public void LoginRequest(IncomingPacket packet)
        {   // aka CDPCertified::SendCertify()
            try
            {
                string buildDate = packet.ReadString();

                if (buildDate != Config.ClientBuildDate)
                {
                    this.SendLoginFail(LoginError.ERROR_RESOURCE_FALSIFIED);
                    this.Socket.Disconnect(false);
                }

                this.Username = packet.ReadString();

                DataTable dt = PreparedStatements.GETACCOUNTINFORMATIONS.Process(this.Username);

                if (dt.Rows.Count == 0)
                {
                    this.SendLoginFail(LoginError.ERROR_INVALID_USERNAME);
                    this.Dispose();
                    return;
                }

                byte[] passwordBytes = packet.ReadBytes(16 * m_PasswordSize);
                string password = Security.Rijndael.decrypt(passwordBytes).TrimEnd('\0');

                if ((string)dt.Rows[0]["Password"] != password)
                {
                    this.SendLoginFail(LoginError.ERROR_INVALID_PASSWORD);
                    this.Dispose();
                    return;
                }

                if (Convert.ToBoolean(dt.Rows[0]["Banned"]))
                {
                    this.SendLoginFail(LoginError.ERROR_ACCOUNT_BANNED);
                    this.Dispose();
                    return;
                }

                if (!Convert.ToBoolean(dt.Rows[0]["Verified"]))
                {
                    this.SendLoginFail(LoginError.ERROR_VERIFICATION_REQUIRED);
                    this.Dispose();
                    return;
                }

                /*
                TODO:
                if world down send this.SendLoginFail(LoginError.SERVICE_DOWN);
                if account already logged in send this.SendLoginFail(LoginError.ACCOUNT_CONNECTED);
                */

                this.SendServerList();
            }
            catch(Exception ex)
            {
                this.SendLoginFail(LoginError.ERROR_SERVER_ERROR);
                this.Dispose();
                Console.WriteLine(ex.Message);
            }
        }

        public void RelogRequest(IncomingPacket packet)
        {
            string username = packet.ReadString();
            string password = packet.ReadString();
            password = Security.MD5.ComputeString(String.Format("{0}{1}", Config.Md5Salt, password));
            if (username == this.Username && password == this.Password)
            {
                //TODO: kick from world
            }
        }
        #endregion
        #region Outgoing packets
        public void SendLoginFail(LoginError errorCode)
        {
            OutgoingPacket packet = new OutgoingPacket(OutgoingPacketHeader.LOGIN_MESSAGE);
            packet.Write((int)errorCode);
            packet.Send(this);
        }

        public void SendServerList()
        {
            Console.Write("");
            OutgoingPacket packet = new OutgoingPacket(OutgoingPacketHeader.SERVER_LIST);

        }

        public void SendSessionKey()
        {
            OutgoingPacket packet = new OutgoingPacket(OutgoingPacketHeader.SESSION_KEY);
            packet.Write((int)SessionKey);
            packet.Send(this);
        }
        #endregion
    }
}