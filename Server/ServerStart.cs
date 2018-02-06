using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Server
{
    class ServerStart
    {

        public static Socket server;
    



        private static void AcceptCallback(IAsyncResult ar)
        {
            
            try
            {
                Socket serverSocket = (Socket)ar.AsyncState;
                Socket clientSocket = serverSocket.EndAccept(ar);
                Session session = new Session { socket = clientSocket };
                clientSocket.BeginReceive(session.Message, 0, session.Message.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), session);
                serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), serverSocket);
            }
            catch(Exception e)
            {

            }

        }

        private static void ReceiveCallback(IAsyncResult ar)
        {
            Session session = (Session)ar.AsyncState;
            int count = 0;
            try
            {
                count = session.socket.EndReceive(ar);
                if (count == 0)
                {
                    
                    AccountManager.Accounts.Remove(session);
                    session.socket.Shutdown(SocketShutdown.Both);
                    session.socket.Close();
                    return;
                }
                byte[] bs = new byte[count];
                Buffer.BlockCopy(session.Message, 0, bs, 0, count);
                ReadMessage(session, bs);
               
            }
            catch(SocketException)
            {
                AccountManager.Accounts.Remove(session);
                session.socket.Shutdown(SocketShutdown.Both);
                session.socket.Close();
                return;
            }
       
            session.socket.BeginReceive(session.Message, 0, session.Message.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), session);
           
          
        }



        public static void ReadMessage(Session session, byte[] bytes)
        {
            try
            {
               
                ByteArray array = new ByteArray(bytes);
                int type = array.ReadInt();
                int command = array.ReadInt();
                int namelength = array.ReadInt();
                string name = null;
                if (namelength > 0)
                {
                    name = array.ReadUTFBytes((uint)namelength);
                }
                int commentLength = array.ReadInt();

               
                string comment = null;
                if (commentLength > 0)
                {
                    comment = array.ReadUTFBytes((uint)commentLength);
                }
                SocketModel model = new SocketModel { type = type, command = command, name = name, message = comment };
                LogicHandler.getInstance().process(session, model);
              
            }
            catch
            {

            }
        }



        public static void Start(string ip, int port)
        {
            IPAddress ipAdress = IPAddress.Parse(ip);
            IPEndPoint ipe = new IPEndPoint(ipAdress, port);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Bind(ipe);
            server.Listen(0);
            server.BeginAccept(new AsyncCallback(AcceptCallback), server);
         
        }




        public static void Stop()
        {
            if (server != null)
            {
            
                
                server.Close();
            }
           
        } 
            




    }
}
