using SharpFly_Packet_Library.Packets;
using SharpFly_Packet_Library.Packets.LoginServer.Outgoing;
using SharpFly_Packet_Library.Packets.ClusterServer.Incoming;
using SharpFly_Packet_Library.Packets.ClusterServer.Outgoing;
using SharpFly_Utility_Library.Database.LoginDatabase.Tables;
using SharpFly_Cluster.Server;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using SharpFly_Utility_Library.Sockets;

namespace SharpFly_Cluster.Player
{
    public class Player : IDisposable
    {

        #region "Network related attributes"
        private byte[] m_RemainingBytes = null;
        public byte[] Buffer { get; set; }
        public const int BufferSize = 1500;
        public List<byte> ReceivedBytes { get; set; }
        public Socket Socket { get; set; }
        #endregion

        #region "Account attributes"
        public bool Authenticated { get; set; } = false;
        public string Username { get; set; }
        public uint SessionKey { get; set; }
        #endregion

        private Player() { }

        public Player(Socket socket)
        {
            this.Buffer = new byte[BufferSize];
            ReceivedBytes = new List<byte>();
            this.SessionKey = (uint)new Random().Next(0, ushort.MaxValue * 5);
            this.Socket = socket;
            Console.WriteLine("Player " + this.Socket.RemoteEndPoint + " connected!");
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
            IncomingPacket[] packets = IncomingPacket.SplitClusterServerPackets(data);
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
                    packet.Position = PacketHeader.DataStartPosition; // Ignore headers and -1 integer
                    uint header = packet.ReadUInt();
                    switch (header)
                    {
                        case SharpFly_Packet_Library.Packets.ClusterServer.Incoming.OpCodes.QUERY_TICK_COUNT:
                            OnQueryTickCount(packet);
                            break;
                        case SharpFly_Packet_Library.Packets.ClusterServer.Incoming.OpCodes.AUTH_QUERY:
                            OnAuthQuery(packet);
                            break;
                        case SharpFly_Packet_Library.Packets.ClusterServer.Incoming.OpCodes.PING:
                            OnPing(packet);
                            break;
                        case SharpFly_Packet_Library.Packets.ClusterServer.Incoming.OpCodes.CHARACTER_LIST:
                            OnCharacterListRequest(packet);
                            break;
                        case SharpFly_Packet_Library.Packets.ClusterServer.Incoming.OpCodes.DELETE_CHARACTER:
                            Console.WriteLine("Delete character packet");
                            break;
                        case SharpFly_Packet_Library.Packets.ClusterServer.Incoming.OpCodes.CREATE_CHARACTER:
                            OnCreateCharacter(packet);
                            break;
                        case SharpFly_Packet_Library.Packets.ClusterServer.Incoming.OpCodes.WORLD_TRANSFER:
                            Console.WriteLine("World transfer packet");
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
            ClusterServer.PlayerManager.RemovePlayer(this);
            this.Socket.Dispose();
        }

        #region Incoming packets
        public void OnQueryTickCount(IncomingPacket packet)
        {
            SharpFly_Packet_Library.Packets.ClusterServer.Incoming.QueryTickCount request = new SharpFly_Packet_Library.Packets.ClusterServer.Incoming.QueryTickCount(packet);
            SendAuthQuery(0, 0, 0, 0);
        }

        public void OnAuthQuery(IncomingPacket packet)
        {
            SharpFly_Packet_Library.Packets.ClusterServer.Incoming.AuthQuery request = new SharpFly_Packet_Library.Packets.ClusterServer.Incoming.AuthQuery(packet);
            if (!Authenticated)
                SendPong(1); // not sure, doesn't happened yet tho
            else
                SendAuthQuery(0, 0, 0, 0);
        }

        public void OnCharacterListRequest(IncomingPacket packet)
        {
            CharacterListRequest request = new CharacterListRequest(packet);

            // Don't go back to character list, when you're already in there
            if (Authenticated)
                return;

            if (request.BuildDate != (string)ClusterServer.Config.GetSetting("ClientBuildDate"))
            {
                this.Dispose();
                return;
            }

            this.Username = request.Username;

            Account account;
            if (ClusterServer.LoginDatabase.Accounts.ContainsKey(this.Username))
                account = ClusterServer.LoginDatabase.Accounts[this.Username];
            else
            {
                account = SharpFly_Utility_Library.Database.LoginDatabase.Queries.Account.Instance.GetAccount(this.Username);
                if (account == null)
                {
                    this.Dispose();
                    return;
                }
                else
                    ClusterServer.LoginDatabase.Accounts.Add(account.Accountname, account);
            }

            if (account.Password != request.Password)
            {
                this.Dispose();
                return;
            }

            if (account.Banned)
            {
                this.Dispose();
                return;
            }

            if (!account.Verified)
            {
                this.Dispose();
                return;
            }

            Authenticated = true;
            SendServerIp();
            SendCharacterList(request.TimeGetTime);
        }

        public void OnCreateCharacter(IncomingPacket packet)
        {
            CreateCharacter request = new CreateCharacter(packet);
            SendCharacterList((uint)DateTime.Now.Ticks);
        }

        public void OnPing(IncomingPacket packet)
        {
            Ping request = new Ping(packet);
            if (Authenticated)
                SendPong(request.Time);
            else
                SendQueryTickCount((uint)DateTime.Now.Ticks);
        }
        #endregion
        #region Outgoing packets
        public void SendSessionKey()
        {
            new SessionKey((int)this.SessionKey, this.Socket);
        }

        public void SendAuthQuery(int value1, int value2, int value3, int value4)
        {
            new SharpFly_Packet_Library.Packets.ClusterServer.Outgoing.AuthQuery(value1, value2, value3, value4, this.Socket);
        }

        public void SendServerIp()
        {
            new ServerIp((string)ClusterServer.Config.GetSetting("ClusterAddress"), this.Socket);
        }

        public void SendCharacterList(uint TimeGetTime)
        {
            new CharacterList(TimeGetTime, this.Socket);
        }

        public void SendQueryTickCount(uint time)
        {
            new SharpFly_Packet_Library.Packets.ClusterServer.Outgoing.QueryTickCount(time, this.Socket);
        }

        public void SendPong(uint time)
        {
            new Pong(time, this.Socket);
        }

        #endregion
    }
}