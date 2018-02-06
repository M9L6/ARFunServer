using System;
using System.Collections.Generic;
using System.Text;


namespace Server
{
    class SendMessageHandler
    {

        private static SendMessageHandler Instance;

        public static SendMessageHandler GetInstance()
        {
            if (Instance == null)
            {
                Instance = new SendMessageHandler();
            }
            return Instance;
        }


        public void process(Session session, SocketModel model)
        {
            try
            {
                switch (model.command)
                {
                    case Command.GetComments:
                     
                        if ( !AccountManager.Comments.ContainsKey(model.name))
                        {
                            session.write(model.type, model.command, model.name, string.Empty);
                        }
                        else
                        {
                            for (int i = 0; i < AccountManager.Comments[model.name].Count; i++)
                            {
                               
                                session.write(model.type, model.command, model.name, AccountManager.Comments[model.name][i]);
                              
                            }
                        }
                    
                        break;

                    case Command.SendComment:


                        
                        DataManager.GetInstance().SaveData(model.name, model.message);

                        if (!AccountManager.Comments.ContainsKey(model.name))
                        {

                            DataManager.GetInstance().SaveNameData(model.name);


                            List<string> comments = new List<string>();
                            comments.Add(model.message);
                            AccountManager.Comments.Add(model.name, comments);
                        }
                        else
                        {
                            AccountManager.Comments[model.name].Add(model.message);
                        }


                      
                        foreach (Session s in AccountManager.Accounts)
                        {
                            
                            if (s != session)
                            {
                                s.write(model.type, model.command, model.name, model.message);
                               
                            }

                        }
                        
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
