using System;
using System.Net.Sockets;
using System.Data;
using System.Collections.Generic;
using SharpFly_Login.Database;
using SharpFly_Login.Server;
using SharpFly_Packet_Library.Packets;
using SharpFly_Packet_Library.Packets.LoginServer.Incoming;
using SharpFly_Packet_Library.Packets.LoginServer.Outgoing;

namespace SharpFly_Login.Clients
{
    class Client : IDisposable
    {

        #region "Network related attributes"
        private byte[] m_RemainingBytes = null;
        public byte[] Buffer { get; set; }
        public const int BufferSize = 1500;
        public List<byte> ReceivedBytes { get; set; }
        public Socket Socket { get; set; }
        #endregion

        #region "Account attributes"
        public string Username { get; set; }
        public string Password { get; set; }
        public uint SessionKey { get; set; }
        #endregion

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
                if (this.Socket != null)
                {
                    if (this.Socket.Available <= 0)
                        return;

                    int byteCount = this.Socket.Receive(this.Buffer, this.Buffer.Length, SocketFlags.None);
                    if (byteCount <= 0)
                        return;
                    ReceivedBytes.AddRange(this.Buffer);
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
                        packet.Position = IncomingPacket.HeaderSize; // Ignore headers
                        uint header = packet.ReadUInt();
                        switch (header)
                        {
                            case SharpFly_Packet_Library.Packets.LoginServer.Incoming.OpCodes.LOGIN_REQUEST:
                                LoginRequest(packet);
                                break;
                            case SharpFly_Packet_Library.Packets.LoginServer.Incoming.OpCodes.PING:
                                break;
                            case SharpFly_Packet_Library.Packets.LoginServer.Incoming.OpCodes.SOCK_FIN:
                                break;
                            case SharpFly_Packet_Library.Packets.LoginServer.Incoming.OpCodes.RELOG_REQUEST:
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
                LoginRequest request = new LoginRequest(packet);
                if (request.BuildDate != (string)LoginServer.Config.GetSetting("ClientBuildDate"))
                {
                    this.SendLoginFail(LoginError.ERROR_RESOURCE_FALSIFIED);
                    this.Dispose();
                    return;
                }

                this.Username = request.Username;
                DataTable dt = PreparedStatements.GET_ACCOUNT_INFORMATIONS.Process(this.Username);
                if (dt.Rows.Count == 0)
                {
                    this.SendLoginFail(LoginError.ERROR_INVALID_USERNAME);
                    this.Dispose();
                    return;
                }

                string password = Security.Rijndael.decrypt(request.Password).TrimEnd('\0');
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
            RelogRequest request = new RelogRequest(packet);
            string password = Security.MD5.ComputeString(String.Format("{0}{1}", LoginServer.Config.GetSetting("Md5Salt"), request.Password));
            // check and kick from world
        }
        #endregion
        #region Outgoing packets
        public void SendLoginFail(uint errorCode)
        {
            new LoginFail(errorCode, this.Socket);
        }

        public void SendServerList()
        {
            new ServerList(this.Socket);
        }

        public void SendSessionKey()
        {
            new SessionKey((int)this.SessionKey, this.Socket);
        }
        #endregion
    }
}