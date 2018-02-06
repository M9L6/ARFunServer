                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
namespace  Server
{
    public class Session
    {

      
        private List<byte> messages = new List<byte>();

        public Session()
        {
            this.Message = new byte[4096];
        }


        public void write(int type, int command, string name, string message)
        {
            SocketModel model = new SocketModel(type, command, name, message);
            ByteArray byteArray = new ByteArray();
            byteArray.WriteInt(model.type);
            byteArray.WriteInt(model.command);
            if (!string.IsNullOrEmpty(model.name))
            {
                byte[] bs1 = Encoding.UTF8.GetBytes(model.name);
                byteArray.WriteInt(bs1.Length);
                byteArray.WriteBytes(bs1); 
            }
            else
            {
                byteArray.WriteInt(0);
            }
            if (!string.IsNullOrEmpty(model.message))
            {
                byte[] bs2 = Encoding.UTF8.GetBytes(model.message);
                byteArray.WriteInt(bs2.Length);
                byteArray.WriteBytes(bs2);
            }
            else
            {
                byteArray.WriteInt(0);
            }
           
           socket.Send(byteArray.Buffer);
          
        }





        public Socket socket { get; set; }
        public byte[] Message { get; set; }
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    