using System;
using System.Collections.Generic;
using System.Text;


namespace Server
{
    class LogicHandler
    {
        private static LogicHandler Instance;
        public static LogicHandler getInstance()
        {
            if (Instance == null)
            {
                Instance = new LogicHandler();
            }
            return Instance;
        }

        public  void process(Session session, SocketModel model)
        {
            try
            {
                switch (model.type)
                {
                    case Type.Create:
                        CreateHandler.GetInstance().process(session,model);
                        break;

                    case Type.SendMessage:
                        SendMessageHandler.GetInstance().process(session, model);
                        break;

                    default:
          
                        break;
                }
            }
            catch (Exception e)
            {
                Log.form.AddText(e.ToString());
            }
        }

    }
}
