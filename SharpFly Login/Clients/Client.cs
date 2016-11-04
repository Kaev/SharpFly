using System;
using System.Net.Sockets;
using System.Data;
using System.Collections.Generic;
using SharpFly_Login.Server;
using SharpFly_Packet_Library.Packets;
using SharpFly_Packet_Library.Packets.LoginServer.Incoming;
using SharpFly_Packet_Library.Packets.LoginServer.Outgoing;
using SharpFly_Packet_Library.Security;
using SharpFly_Utility_Library.Database.LoginDatabase.Tables;
using SharpFly_Utility_Library.Sockets;

namespace SharpFly_Login.Clients
{
    public class Client : IDisposable
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
            if (!SocketChecker.IsSocketConnected(this.Socket))
            {
                this.Dispose();
                return;
            }

            if (this.Socket != null)
            {
                if (!this.Socket.Connected)
                {
                    this.Dispose();
                    return;
                }
                    

                if (this.Socket.Available <= 0)
                    return;

                int byteCount = this.Socket.Receive(this.Buffer, this.Buffer.Length, SocketFlags.None);
                if (byteCount <= 0)
                    return;
                ReceivedBytes.AddRange(this.Buffer);
            }

            byte[] data = ReceivedBytes.ToArray();
                IncomingPacket[] packets = IncomingPacket.SplitLoginServerPackets(data);
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
                        packet.Position = PacketHeader.Size; // Ignore headers
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
                                Console.WriteLine(String.Format("Unknown packet header {0:X08}", header));
                                break;

                        }
                    }
                }
                ReceivedBytes.Clear();
        }

        public void Dispose()
        {
            LoginServer.ClientManager.RemoveUser(this);
            this.Socket.Dispose();
        }

        #region Incoming packets
        public void LoginRequest(IncomingPacket packet)
        {
            LoginRequest request = new LoginRequest(packet);
            if (request.BuildDate != (string)LoginServer.Config.GetSetting("ClientBuildDate"))
            {
                this.SendLoginFail(LoginError.ERROR_RESOURCE_FALSIFIED);
                this.Dispose();
                return;
            }

            this.Username = request.Username;

            Account account;
            if (LoginServer.LoginDatabase.Accounts.ContainsKey(this.Username))
                account = LoginServer.LoginDatabase.Accounts[this.Username];
            else
            {
                account = SharpFly_Utility_Library.Database.LoginDatabase.Queries.Account.Instance.GetAccount(this.Username);
                if (account == null)
                {
                    this.SendLoginFail(LoginError.ERROR_INVALID_USERNAME);
                    this.Dispose();
                    return;
                }
                else
                    LoginServer.LoginDatabase.Accounts.Add(account.Accountname, account);
            }

            string password = Rijndael.decrypt(request.Password).TrimEnd('\0');
            if (account.Password != password)
            {
                this.SendLoginFail(LoginError.ERROR_INVALID_PASSWORD);
                this.Dispose();
                return;
            }

            if (account.Banned)
            {
                this.SendLoginFail(LoginError.ERROR_ACCOUNT_BANNED);
                this.Dispose();
                return;
            }

            if (!account.Verified)
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

        public void RelogRequest(IncomingPacket packet)
        {
            RelogRequest request = new RelogRequest(packet);
            string password = MD5.ComputeString(String.Format("{0}{1}", LoginServer.Config.GetSetting("Md5Salt"), request.Password));
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
            new ServerList(LoginServer.ClusterManager.GetClusters(), this.Username, this.Socket);
        }

        public void SendSessionKey()
        {
            new SessionKey((int)this.SessionKey, this.Socket);
        }
        #endregion
    }
}