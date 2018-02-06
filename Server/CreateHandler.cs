using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
namespace Server
{
    class CreateHandler
    {

        private static CreateHandler Instance;
        public static CreateHandler GetInstance()
        {
            if (Instance == null)
            {
                Instance = new CreateHandler();
            }
            return Instance;
        }

        public void process(Session session, SocketModel model)
        {

            try
            {
                if (!AccountManager.Accounts.Contains(session)) 
                {
                    AccountManager.Accounts.Add(session);
                } 
               
            }
            catch (Exception e)
            {
                Log.form.AddText(e.ToString());

            }

        }




    }
}
