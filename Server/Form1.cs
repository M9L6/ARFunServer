using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Server
{
    public partial class Form1 : Form
    {

        private bool hasLoaded = false;
        private updateText u;


        public Form1()
        {
            InitializeComponent();
            Log.form = this;
            textBox3.ReadOnly = true;
            textBox3.UseWaitCursor = false;

            this.u = new updateText(TextAdd);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "开启服务器")
            {
                ServerStart.Start(textBox1.Text, Convert.ToInt32(textBox2.Text));
                textBox1.ReadOnly = true;
                textBox2.ReadOnly = true;
                button1.Text = "关闭服务器";
            }
            else
            {
                ServerStart.Stop();
                textBox1.ReadOnly = false;
                textBox2.ReadOnly = false;
                button1.Text = "开启服务器";
            }
           
        }

        private void label3_Click(object sender, EventArgs e)
        {
            
        }

        private void TextAdd(string text)
        {
            textBox3.AppendText(text);
        }

        public void AddText(string text)
        {
           
            base.Invoke(u, new object[] { text });
        }

        public delegate void updateText(string text);

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataManager.GetInstance().CreateSavePath(textBox4.Text); 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataManager.path = textBox4.Text + "/";
            if (!hasLoaded)
            {
                DataManager.GetInstance().LoadNameData();
                hasLoaded = true;
            }
        }
    }
}
