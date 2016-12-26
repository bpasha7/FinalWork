using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.GData.Spreadsheets;
using Google.GData;
using Google.GData.Client;
using Google.GData.Extensions;
using System.Net;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.IO;
using System.Threading;
using Google.Apis.Vision.v1;
using Google.Apis.Oauth2.v2;
using System.Data.OleDb;
using Microsoft.Win32;

namespace FinalWork
{
    public partial class Form1 : Form
    {
        Stack<int> numbers;
        List<DllManager> SharedDll;
        int count = 1;
        string DirectoryPath;
        //строка соединения
        string strAccessConn;// = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Logs.mdb";
        OleDbConnection myAccessConn;
        //Табы
        List<ManagerWindow> MW;
        int MaxManagerTabsCount = 0;
        bool canClose = true;

        // OAuth2Parameters holds all the parameters related to OAuth 2.0.
        OAuth2Parameters parameters = new OAuth2Parameters();

        string CLIENT_ID;// = "229901477056-8hcinql2rearjj5hn7d0hep8r2r7qc24.apps.googleusercontent.com";

        string CLIENT_SECRET;// = "LZaDb-cvJg2HnwmQjDHkFAMg";

        string SCOPE = "https://spreadsheets.google.com/feeds https://docs.google.com/feeds";

        string REDIRECT_URI = "urn:ietf:wg:oauth:2.0:oob";

        string _Gmail;
        string _Gpass;

        SpreadsheetsService service;

        string ProgramStart = DateTime.Now.ToString("MM'/'dd'/'yyyy HH:mm:ss");

        SpreadsheetEntry TargetTable;
        List<SpreadsheetEntry> MySpreadsheet;

        void SetPathFromRegystry()
        {
            const string userRoot = "HKEY_LOCAL_MACHINE";
            const string subkey = "SNMP_Worker";
            const string keyName = userRoot + "\\SOFTWARE\\" + subkey;
            DirectoryPath = (string)Registry.GetValue(keyName,
            "PrgPath",
            null);
            DirectoryPath = DirectoryPath.Remove(DirectoryPath.LastIndexOf("\\"));
            strAccessConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + DirectoryPath + "\\Logs.mdb";
        }

        /// <summary>
        /// Открытие программы под лицензией
        /// </summary>
        /// <param name="Params">Содержание ключа</param>
        public Form1(string[] Params)
        {
            SetPathFromRegystry();
            InitializeComponent();
            
            this.Text += string.Format("({0}. {1} Days left. User: {2}.)", Params[0], Params[2], Params[1]);
            SetProgramInterfaceByLicence(Params[0], Params);
        }

        void SetProgramInterfaceByLicence(string LicenceType, string[] Params)
        {
            switch (LicenceType)
            {
                case "Trial":
                    MaxManagerTabsCount = 1;
                    break;
                case "Community":
                    MaxManagerTabsCount = 3;
                    KeyPicture.Image = Properties.Resources.Key as Bitmap;
                    KeyToolTip.SetToolTip(this.KeyPicture, String.Format("Имя: {0}\nДействителен до: {1}\nВерсия: {2}\nGoogle: {3}", Params[1] , Params[7], Params[0], Params[3]));
                    CLIENT_ID = Params[5];
                    CLIENT_SECRET = Params[6];
                    _Gmail = Params[3];
                    _Gpass = Params[4];
                    break;
                case "Professional":
                    MaxManagerTabsCount = 7;
                    KeyPicture.Image = Properties.Resources.Key as Bitmap;
                    KeyToolTip.SetToolTip(this.KeyPicture, String.Format("Имя: {0}\nДействителен до: {1}\nВерсия: {2}\nGoogle: {3}\n", Params[1], Params[7], Params[0], Params[3]));
                    CLIENT_ID = Params[5];
                    CLIENT_SECRET = Params[6];
                    _Gmail = Params[3];
                    _Gpass = Params[4];
                    break;
                default:
                    break;
            }

        }
        /// <summary>
        /// Функция для ведения лога программы
        /// </summary>
        /// <param name="name">Событие</param>
        /// <param name="txt">Описание</param>
        void ToLog(string name, string txt)
        {
            try
            {
                string strAccessInsert = string.Format("INSERT INTO Events(Ename, Emsg, Edate) VALUES(\"{0}\",\"{1}\",\"{2}\")", name, txt, DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"));
                OleDbCommand cmd = new OleDbCommand(strAccessInsert, myAccessConn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Data base Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        /// <summary>
        /// Функция для ведения лога Ошибок
        /// </summary>
        /// <param name="name">Событие</param>
        /// <param name="fromElement">Элемент вызвавший ошибку</param>
        /// <param name="txt">Описание</param>
        void ToErrorLog(string name, string fromElement, string txt)
        {
            try
            {
                string strAccessInsert = string.Format("INSERT INTO Errors(Ename, Emsg, Efrom, Edate) VALUES(\"{0}\",\"{1}\",\"{2}\",\"{3}\")", name, txt, fromElement, DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"));
                OleDbCommand cmd = new OleDbCommand(strAccessInsert, myAccessConn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Data base Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        public Form1()
        {
            SetPathFromRegystry();
            InitializeComponent();
            
            SetProgramInterfaceByLicence("Trial", null);
        }
        /// <summary>
        /// Загрузка плагинов по умолчанию и директории dll, находящейся в папке с исполняемым файлом
        /// </summary>
        private void LoadDll()
        {
            SharedDll = new List<DllManager>();
            MW = new List<ManagerWindow>();
            string[] dlls = Directory.GetFiles(DirectoryPath + "\\dll", "*.dll");

            for (int i = 0; i < dlls.Length; i++)
            {
                dlls[i] = dlls[i].Remove(0, dlls[i].LastIndexOf('\\') + 1);
            }
            for (int i = 0; i < dlls.Length; i++)
            {
                SharedDll.Add(new DllManager(DirectoryPath + "\\dll\\" + dlls[i], i));
            }
        }
        /// <summary>
        /// События при загрузке формы
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            /*const string userRoot = "HKEY_LOCAL_MACHINE";
            const string subkey = "SNMP_Worker";
            const string keyName = userRoot + "\\SOFTWARE\\" + subkey;
            DirectoryPath = (string)Registry.GetValue(keyName,"PrgPath",null);
            DirectoryPath = DirectoryPath.Remove(DirectoryPath.LastIndexOf("\\"));*/
            
           // MessageBox.Show(DirectoryPath);
            //SetPathFromRegystry();
            myAccessConn = new OleDbConnection(strAccessConn);
            myAccessConn.Open();
            numbers = new Stack<int>();
            numbers.Push(1);
            LoadDll();
            GoogleToolTip.SetToolTip(this.GooglePicture, "Доступ к Google Disk\nне разрешен.");
            DBtoolTip.SetToolTip(this.DBPicture, "Ведется протоколирование.");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void закрытьВкладкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProgramTabControl.TabPages.Remove(ProgramTabControl.SelectedTab);
        }

        private void плагиныToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void googleDriveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(_Gmail == null || _Gpass == null)
            {
                ToErrorLog("Отсутствие данных", "Авторизация Google", "Ключ не содержал данных!");
                return;
            }
            parameters.ClientId = CLIENT_ID;

            parameters.ClientSecret = CLIENT_SECRET;

            parameters.RedirectUri = REDIRECT_URI;

            ////////////////////////////////////////////////////////////////////////////
            // STEP 3: Get the Authorization URL
            ////////////////////////////////////////////////////////////////////////////

            // Set the scope for this particular service.
            parameters.Scope = SCOPE;

            // Get the authorization url.  The user of your application must visit
            // this url in order to authorize with Google.  If you are building a
            // browser-based application, you can redirect the user to the authorization
            // url.
            string authorizationUrl = OAuthUtil.CreateOAuth2AuthorizationUrl(parameters);
            //System.Diagnostics.Process.Start(authorizationUrl);
            Authentication auth = new Authentication();
            auth.URL = authorizationUrl;
            auth.Gpass = _Gpass;
            auth.Gmail = _Gmail;
            auth.ShowDialog();
            if (auth.Key!=null)
            {
                parameters.AccessCode = auth.Key;
                OAuthUtil.GetAccessToken(parameters);
                string accessToken = parameters.AccessToken;
                GOAuth2RequestFactory requestFactory =
                    new GOAuth2RequestFactory(null, "MySpreadsheetIntegration-v1", parameters);
                service = new SpreadsheetsService("MySpreadsheetIntegration-v1");
                service.RequestFactory = requestFactory;
                GooglePicture.Image = Properties.Resources.googleDrive as Bitmap;
                GoogleToolTip.SetToolTip(this.GooglePicture, "Доступ к Google Disk\nразрешен.");
            }
            else
                MessageBox.Show("Вход не выполнен", "Google Service Access", MessageBoxButtons.OK, MessageBoxIcon.Error);
            ToLog("Авторизация Google", string.Format(auth.Info));
            auth.Close();
        }

        private void sNMPWalkerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int num = numbers.Peek();
            numbers.Pop();
            if (num >= count)
                numbers.Push(++count);
            MW.Add(new ManagerWindow(string.Format("Менеджер {0}", num), service, TargetTable, SharedDll));
            ProgramTabControl.Controls.Add(MW[MW.Count - 1]);
            ProgramTabControl.SelectTab(ProgramTabControl.TabCount - 1);
            ToLog("Открытие окна", string.Format("Окно [Менеджер {0}] открыто", num));
        }

        private void закрытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ProgramTabControl.SelectedTab.Text != "Обозреватель" && MessageBox.Show("Закрыть данное окно?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                numbers.Push(Convert.ToInt32(ProgramTabControl.SelectedTab.Text.Remove(0, ProgramTabControl.SelectedTab.Text.IndexOf(' '))));
                ProgramTabControl.TabPages.Remove(ProgramTabControl.SelectedTab);
                ToLog("Закрытие окна", string.Format("Окно [Менеджер {0}] закрыто", numbers.Peek()));
            }
        }

        private void KeyPicture_MouseEnter(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MySpreadsheet != null && GoogleTableListView.SelectedItems.Count == 1)
            {                              
                TableInfo.Text = string.Format("Название: \"{0}\", Автор: {1} ",MySpreadsheet[GoogleTableListView.SelectedItems[0].Index].Title.Text, MySpreadsheet[GoogleTableListView.SelectedItems[0].Index].Authors[0].Email);
                TableInfo.Visible = true;
            }
        }

        private void UpdateGoogleTables_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (service != null)
            {
                GoogleTableListView.Items.Clear();
                ImageList IL = new ImageList();
                IL.ImageSize = new Size(32, 32);
                IL.Images.Add(Properties.Resources.Gtable as Bitmap);
                GoogleTableListView.LargeImageList = IL;
                SpreadsheetQuery query2 = new SpreadsheetQuery();
                SpreadsheetFeed feed2 = service.Query(query2);
                MySpreadsheet = new List<SpreadsheetEntry>();
                foreach (SpreadsheetEntry entry in feed2.Entries)
                {
                    MySpreadsheet.Add(entry);
                    GoogleTableListView.Items.Add(entry.Title.Text, 0);
                }
                TakeTable.Enabled = true;
                ToLog("Обновление таблиц", string.Format("Список таблиц Google обновлен"));
            }
            else
                MessageBox.Show("Вы не подключились к Google Disk!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }

        private void TakeTable_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (MySpreadsheet != null && GoogleTableListView.SelectedItems.Count == 1)
            {
                TargetTable = MySpreadsheet[GoogleTableListView.SelectedItems[0].Index];
                TakeTable.Text = "Выбрана таблица: " + TargetTable.Title.Text;
                TakeTable.Enabled = false;
                ToLog("Таблица выбрана", string.Format("Таблица {0} выбрана из Google аккаунта.", TargetTable.Title.Text));
            }
            else
                MessageBox.Show("Выберети только одну таблицу!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }

        private void GoogleTableListView_Leave(object sender, EventArgs e)
        {
            TableInfo.Visible = false;
        }

        private void DBPicture_Click(object sender, EventArgs e)
        {

        }

        private void toolStripProgressBar1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (ProgramTabControl.TabCount <= MaxManagerTabsCount)
            {
                int num = numbers.Peek();
                numbers.Pop();
                if (num >= count)
                    numbers.Push(++count);
                MW.Add(new ManagerWindow(string.Format("Менеджер {0}", num), service, TargetTable, SharedDll));
                ProgramTabControl.Controls.Add(MW[MW.Count - 1]);
                ProgramTabControl.SelectTab(ProgramTabControl.TabCount - 1);
                ToLog("Открытие окна", string.Format("Окно [Менеджер {0}] открыто", num));
            }
            else
            {
                ToErrorLog("Лимит менеджеров", "Гравная форма", "Привышен лимит для данной лицензии");
                MessageBox.Show("Привышен лимит для данной лицензии!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }

        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (ProgramTabControl.SelectedTab.Text != "Обозреватель" && MessageBox.Show("Закрыть данное окно?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                numbers.Push(Convert.ToInt32(ProgramTabControl.SelectedTab.Text.Remove(0, ProgramTabControl.SelectedTab.Text.IndexOf(' '))));
                ProgramTabControl.TabPages.Remove(ProgramTabControl.SelectedTab);
                ToLog("Закрытие окна", string.Format("Окно [Менеджер {0}] закрыто", numbers.Peek()));
            }        
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();
            openFileDialog1.Filter = "dll files (*.dll)|*.dll|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (openFileDialog1.OpenFile() != null)
                    {
                        SharedDll.Add(new DllManager(openFileDialog1.FileName, SharedDll.Count));
                        //LoadCombobox();
                    }
                    ///Обновить длл всем вкладкам
                    foreach (ManagerWindow item in MW)
                    {
                        item.SetDll(SharedDll);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }
        /// <summary>
        /// сохранение в файл содержание функций текущей вкладки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (ProgramTabControl.SelectedTab.Text == "Обозреватель")
                return;
            var CurrentListToSaveFunctionQueue = MW.Where(MWindow => MWindow.Text == ProgramTabControl.SelectedTab.Text).Single().GetListOfFunctions();
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "INI Файл (*.ini)|";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    StreamWriter streamWriter = new StreamWriter(saveFileDialog1.FileName);
                    foreach (string row in CurrentListToSaveFunctionQueue)
                    {
                        streamWriter.WriteLine(row);
                    }
                    streamWriter.Close();
                }
        }

        private void TabLog_Enter(object sender, EventArgs e)
        {
            OleDbCommand cmd = new OleDbCommand("Select  Ename, Emsg, Format(Edate, \"hh:mm:ss\") from Events WHERE (((Edate)>=#" + ProgramStart + "#));", myAccessConn);
            OleDbDataAdapter myDataAdapter = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            myDataAdapter.Fill(ds);
            ds.Tables[0].Columns[0].ColumnName = "Соыбтие";
            ds.Tables[0].Columns[1].ColumnName = "Описание";
            ds.Tables[0].Columns[2].ColumnName = "Дата";
            EventsDataView.DataSource = ds.Tables[0];
            EventsDataView.Columns[0].Width = 135;
            EventsDataView.Columns[1].Width = 285;
            EventsDataView.Columns[2].Width = 60;
           
        }

        private void ErrorTab_Enter(object sender, EventArgs e)
        {
            OleDbCommand cmd = new OleDbCommand("Select  Ename, Emsg, Efrom, Format(Edate, \"hh:mm:ss\") from Errors WHERE (((Edate)>=#" + ProgramStart + "#));", myAccessConn);
            OleDbDataAdapter myDataAdapter = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            myDataAdapter.Fill(ds);
            ds.Tables[0].Columns[0].ColumnName = "Ошибка";
            ds.Tables[0].Columns[1].ColumnName = "Описание";
            ds.Tables[0].Columns[2].ColumnName = "Источник";
            ds.Tables[0].Columns[3].ColumnName = "Дата";
            ErrorView.DataSource = ds.Tables[0];
            ErrorView.Columns[0].Width = 97;
            ErrorView.Columns[1].Width = 224;
            ErrorView.Columns[2].Width = 94;
            ErrorView.Columns[3].Width = 67;
        }

        private void tabPage1_Enter(object sender, EventArgs e)
        {
            TabLog_Enter(sender, e);
        }

        private void подключитьсяToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void сыернутьToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void закрытьВсеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ManagerWindow item in MW)
            {
                ProgramTabControl.TabPages.Remove(item);
            }
        }

        private void IcoProgram_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
                this.WindowState = FormWindowState.Minimized;
            else
                this.WindowState = FormWindowState.Normal;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            e.Cancel = canClose;
            return;
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();
            openFileDialog1.Filter = "ini files (*.ini)|*.ini|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (openFileDialog1.OpenFile() != null)
                    {
                        string[] temp = File.ReadAllLines(openFileDialog1.FileName);
                        if (temp.Length != 0)
                        {
                            var CurrentListToSaveFunctionQueue = MW.Where(MWindow => MWindow.Text == ProgramTabControl.SelectedTab.Text).Single();
                            CurrentListToSaveFunctionQueue.SetFromFilePlan(temp);
                        }
                        else
                            ToErrorLog("Пустой файл", "Обрабока файла", "Некорректное содежание файла!");

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void ProgramTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ProgramTabControl.SelectedTab.Text == "Обозреватель")
            {
                toolStripButton3.Enabled = false;
                toolStripButton7.Enabled = false;
            }
            else
            {
                toolStripButton3.Enabled = true;
                toolStripButton7.Enabled = true;
            }
        }

        private void обозревательToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton2_Click(sender, e);
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            canClose = false;
            this.Close();
        }
    }
}
