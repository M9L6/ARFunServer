using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace Server
{
    class SocketModel
    {

        public int type;
        public int command;
        public string name;
        public string message;

        public SocketModel()
        {

        }

        public SocketModel(int type, int command, string name,string message)
        {
            this.type = type;
            this.command = command;
            this.name = name;
            this.message = message;
        }
             
        
    }
}
