using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Server
{
    class DataManager
    {


        public static string path = "C:/Users/Administrator/Desktop/Server/";

        private static DataManager Instance;



     


        public static DataManager GetInstance()
        {
            if (Instance == null)
            {
                Instance = new DataManager();
            }

            return Instance;
        }


        public void CreateSavePath(string folderPath)
        {
            if (!File.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
          
        }



        public void LoadNameData()
        {

            if (!File.Exists(path  + "name.txt"))
            {
                return;
            }

            StreamReader sr = File.OpenText(path + "name.txt");

            while (true)
            {

                string r = sr.ReadLine();
                if (string.IsNullOrEmpty(r))
                {
                    break;
                }
                List<string> comments = new List<string>();
                AccountManager.Comments.Add(r, comments);
                LoadData(r);
            }

            sr.Dispose();
            sr.Close();
        }

        public void SaveNameData(string name)
        {
            StreamWriter sw;
            if (!File.Exists(path + "name.txt"))
            {
                sw = File.CreateText(path + "name.txt");
            }
            else
            {
                sw = File.AppendText(path + "name.txt");
            }
            sw.WriteLine(name);
            sw.Dispose();
            sw.Close();
        }


        public void LoadData(string name)
        {
            if (!File.Exists(path + name + ".txt"))
            {
                return;
            }


            StreamReader sr = File.OpenText(path + name + ".txt");

            if (!AccountManager.Comments.ContainsKey(name))
            {
                List<string> comments = new List<string>();
                AccountManager.Comments.Add(name, comments);
            }

            while (true)
            {

                string r = sr.ReadLine();
                if (string.IsNullOrEmpty(r))
                {
                    break;
                }
                AccountManager.Comments[name].Add(r);
            }

            sr.Dispose();
            sr.Close();

        }

        public void SaveData(string name, string comment)
        {

            StreamWriter sw;
            if (!File.Exists(path + name + ".txt"))
            {
                sw = File.CreateText(path + name + ".txt");
            }
            else
            {
                sw = File.AppendText(path + name + ".txt");
            }
            sw.WriteLine(comment);
            sw.Dispose();
            sw.Close();

        }




    }

}
