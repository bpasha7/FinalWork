using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace FinalWork
{
    public partial class Authentication : Form
    {

        string _Key;
        string _URL;
        string _Info;
        string _Gmail;
        string _Gpass;
        Stopwatch stopWatch;

        public string Key
        {
            get { return _Key; }
        }
        public string Gmail
        {
            set { _Gmail = value; }
        }
        public string Gpass
        {
            set { _Gpass = value; }
        }
        public string Info
        {
            get { return _Info; }
        }
        public string URL
        {
            set { _URL = value; }
        }

        public Authentication()
        {
            InitializeComponent();
        }

        private void Accept_Click(object sender, EventArgs e)
        {
               // GoogleAuth();
        }
        private HtmlElement GetElementIfExist(string idElement)
        {
            if (webBrowser1.Document.GetElementById(idElement) == null)
                return null;
            else
                return webBrowser1.Document.GetElementById(idElement);
        }
        private void GoogleAuth()
        {
            try
            {
                if (GetElementIfExist("Email") != null)
                    GetElementIfExist("Email").InnerText = "bezpa20@gmail.com";
                else
                {
                    return;
                }
                progressBar1.Value = 25;
                if (GetElementIfExist("Passwd") != null)
                    GetElementIfExist("Passwd").InnerText = "fuckoff7311v";
                else
                {
                    return;
                }
                progressBar1.Value = 50;
                if (GetElementIfExist("signIn") != null)
                    GetElementIfExist("signIn").InvokeMember("click");
                else
                {
                    return;
                }
                label1.Text = "Вход выполнен";
                webBrowser1.Navigate(_URL);
                progressBar1.Value = 75;
            }
            catch(Exception ex)
            {
                if (GetElementIfExist("gb_71") != null)
                    GetElementIfExist("gb_71").InvokeMember("click");
                else
                {
                    return;
                }
            }
        }
        private void GetCode()
        {
            stopWatch = new Stopwatch();
            stopWatch.Start();          
            while (GetElementIfExist("submit_approve_access") == null)
            {
                Application.DoEvents();
                if (stopWatch.Elapsed.Seconds > 5)
                    throw new Exception("Превышен интервал ожидания!");
            }           
            webBrowser1.Document.GetElementById("submit_approve_access").InvokeMember("click");
            System.Threading.Thread.Sleep(500);
            stopWatch.Restart();
            while (GetElementIfExist("code") == null)
            {
                Application.DoEvents();
                if (stopWatch.Elapsed.Seconds > 5)
                    throw new Exception("Превышен интервал ожидания!");
            }
            stopWatch.Stop();
            _Key = webBrowser1.Document.GetElementById("code").GetAttribute("value");
            progressBar1.Value = 95;
        }
        private void Authentication_Load(object sender, EventArgs e)
        {
          
        }

        private void Authentication_Shown(object sender, EventArgs e)
        {
            try
            {
                webBrowser1.Navigate(new Uri("https://accounts.google.com/ServiceLogin#identifier"));
                while (webBrowser1.ReadyState != WebBrowserReadyState.Complete)
                {
                    Application.DoEvents();
                }
                if (GetElementIfExist("gb_71") != null)
                    GetElementIfExist("gb_71").InvokeMember("click");
                label1.Text = "Вход в учетную запись";
                GoogleAuth();                
                label1.Text = "Вход выполнен, получение доступа...";
                label1.Location = new Point((this.Size.Width - label1.Size.Width)/2, label1.Location.Y);
                System.Threading.Thread.Sleep(1000);
                if (MessageBox.Show("Разрешить доступ к Google Таблицам?", "Подтвердите", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    System.Threading.Thread.Sleep(1000);
                    GetCode();
                }
                else
                    throw new Exception("Доступ не разрешен пользователем!");
                progressBar1.Value = 100;
                label1.Text = "Доступ получен!";
                System.Threading.Thread.Sleep(1000);
                _Info = "Доступ получен к Google Disk получен!";
                this.Close();
            }
            catch (Exception ex)
            {
                _Info = ex.Message;
                this.Close();
                while (webBrowser1.ReadyState != WebBrowserReadyState.Complete)
                {
                    Application.DoEvents();
                }
                if (GetElementIfExist("gb_71") != null)
                    GetElementIfExist("gb_71").InvokeMember("click");
            }
        }
    }
}
