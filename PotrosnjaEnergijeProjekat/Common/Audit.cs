using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public enum MessageType { Info, Warning, Error}
    public  class Audit
    {
        int id;
        DateTime timestamp;
        MessageType messageType;
        string message;

        public Audit()
        {
            id = 0;
            timestamp = DateTime.Now;
            messageType = MessageType.Error;
            message = string.Empty;
        }
        public Audit(int id, DateTime timestamp, MessageType messageType, string message)
        {
            this.Id = id;
            this.Timestamp = timestamp;
            this.MessageType = messageType;
            this.Message = message;
        }

        public int Id { get => id; set => id = value; }
        public DateTime Timestamp { get => timestamp; set => timestamp = value; }
        public MessageType MessageType { get => messageType; set => messageType = value; }
        public string Message { get => message; set => message = value; }

        public override string ToString()
        {
            return $"{id}, {timestamp}: {messageType}, {message}";
        }
    }
}
