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

        public ChatMessagePacket(string message)
        {
            this.type = PacketType.CHATMESSAGE;
            this.chatMessage = message;
        }
    }
}
