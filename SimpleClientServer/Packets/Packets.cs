using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packets
{

    public enum PacketType
    {
        EMPTY,
        NICKNAME,
        CHATMESSAGE,
        IDPACKET,
        CLIENTLIST,
        SYSTEM,
    }

    public enum SystemType
    {
        EMPTY,
        CONNECTED,
        DISCONNECTED,
    }

    [Serializable]
    public class Packet
    {
        public PacketType type = PacketType.EMPTY;
    }
    
    [Serializable]
    public class NicknamePacket : Packet
    {
        public string nickName = String.Empty;
        public NicknamePacket(string nickName)
        {
            this.type = PacketType.NICKNAME;
            this.nickName = nickName;
        }
    }

    [Serializable]
    public class ChatMessagePacket : Packet 
    {
        public string chatMessage = String.Empty;
        public string sender = String.Empty;
        public ChatMessagePacket(string message, string sender)
        {
            this.type = PacketType.CHATMESSAGE;
            this.chatMessage = message;
            this.sender = sender;
        }
    }

    [Serializable]
    public class IDPacket : Packet
    {
        public string ID = String.Empty;
        public IDPacket(string ID)
        {

            this.type = PacketType.IDPACKET;
            this.ID = ID;
        }
    }

    [Serializable]
    public class ClientList : Packet
    {
        public string[] clients = null;

        public ClientList(string[] clients)
        {
            this.type = PacketType.CLIENTLIST;
            this.clients = clients;
        }
    }

    [Serializable]
    public class SysPacket : Packet
    {
        public string sender = string.Empty;
        public string message = string.Empty;

        public SystemType sysType = SystemType.EMPTY;

        public SysPacket(string sender, string message, SystemType type)
        {
            this.type = PacketType.SYSTEM;
            this.sender = sender;
            this.message = message;
            this.sysType = type;
        }
    }
}
