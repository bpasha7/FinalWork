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

namespace FinalWork
{
    public partial class Form1 : Form
    {
        Stack<int> numbers;
        List<DllManager> SharedDll;
        int count = 1;
        // OAuth2Parameters holds all the parameters related to OAuth 2.0.
        OAuth2Parameters parameters = new OAuth2Parameters();

        string CLIENT_ID = "229901477056-8hcinql2rearjj5hn7d0hep8r2r7qc24.apps.googleusercontent.com";

        string CLIENT_SECRET = "LZaDb-cvJg2HnwmQjDHkFAMg";

        string SCOPE = "https://spreadsheets.google.com/feeds https://docs.google.com/feeds";

        string REDIRECT_URI = "urn:ietf:wg:oauth:2.0:oob";

        SpreadsheetsService service;

        SpreadsheetEntry TargetTable;
        List<SpreadsheetEntry> MySpreadsheet;

        public Form1()
        {
            InitializeComponent();
        }
        private void LoadDll()
        {
            SharedDll = new List<DllManager>();
            string[] dlls = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\dll", "*.dll");
            for (int i = 0; i < dlls.Length; i++)
            {
                dlls[i] = dlls[i].Remove(0, dlls[i].LastIndexOf('\\') + 1);
            }
            for (int i = 0; i < dlls.Length; i++)
            {
                SharedDll.Add(new DllManager("dll\\" + dlls[i], i));
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            numbers = new Stack<int>();
            numbers.Push(1);
            LoadDll();
            KeyToolTip.SetToolTip(this.KeyPicture, String.Format("Имя: {0}\nДействителен до: {1}\nВерсия{2}","Петя Иванов", "01.01.2017", "Provessional"));
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

        /*public Bitmap GrayScale(Bitmap Bmp)
        {
            int rgb;
            Color c;

            for (int y = 0; y < Bmp.Height; y++)
                for (int x = 0; x < Bmp.Width; x++)
                {
                    c = Bmp.GetPixel(x, y);
                    rgb = (int)((c.R + c.G + c.B) / 3);
                    Bmp.SetPixel(x, y, Color.FromArgb(rgb, rgb, rgb));
                }
            return Bmp;
        }*/

        private void button2_Click(object sender, EventArgs e)
        {

            /*GooglePicture.Image = Properties.Resources.Key as Bitmap;
            return;

           KeyPicture.Image = GrayScale(new Bitmap(KeyPicture.Image));
            GooglePicture.Image = GrayScale(new Bitmap(GooglePicture.Image));
            DBPicture.Image = GrayScale(new Bitmap(DBPicture.Image));
            return;*/



            /*String spreadsheetId = "1iYSEsRqKbqQbkZE7oL3B8oTmBWUKwyqcKbp6RoI6cls";
            SpreadsheetsResource.ValuesResource.GetRequest request =
                            service.Spreadsheets.Values.Get(spreadsheetId, range);*/

            

            /*SpreadsheetQuery query1 = new SpreadsheetQuery();

            // Make a request to the API and get all spreadsheets.
            SpreadsheetFeed feed1 = service.Query(query1);

            SpreadsheetEntry spreadsheet = (SpreadsheetEntry)feed1.Entries[0];
            Console.WriteLine(spreadsheet.Title.Text);

            // Create a local representation of the new worksheet.
            WorksheetEntry worksheet = new WorksheetEntry();
            worksheet.Title.Text = "New Worksheet";
            worksheet.Cols = 10;
            worksheet.Rows = 20;

            // Send the local representation of the worksheet to the API for
            // creation.  The URL to use here is the worksheet feed URL of our
            // spreadsheet.
            WorksheetFeed wsFeed = spreadsheet.Worksheets;
            service.Insert(wsFeed, worksheet);
            SpreadsheetQuery query = new SpreadsheetQuery();*/
              SpreadsheetQuery query = new SpreadsheetQuery();
              SpreadsheetFeed feed = service.Query(query);

              if (feed.Entries.Count == 0)
              {
                  // TODO: There were no spreadsheets, act accordingly.
              }
              SpreadsheetEntry spreadsheet = (SpreadsheetEntry)feed.Entries[0];
             // (spreadsheet.Title.Text);

              // Get the first worksheet of the first spreadsheet.
              // TODO: Choose a worksheet more intelligently based on your
              // app's needs.
              WorksheetFeed wsFeed = spreadsheet.Worksheets;
              WorksheetEntry worksheet = (WorksheetEntry)wsFeed.Entries[0];

              // Define the URL to request the list feed of the worksheet.
              AtomLink listFeedLink = worksheet.Links.FindService(GDataSpreadsheetsNameTable.ListRel, null);

              // Fetch the list feed of the worksheet.
              ListQuery listQuery = new ListQuery(listFeedLink.HRef.ToString());
              ListFeed listFeed = service.Query(listQuery);

              CellQuery cellQuery = new CellQuery(worksheet.CellFeedLink);
              CellFeed cellFeed = service.Query(cellQuery);

              CellEntry cellEntry = new CellEntry(1, 1, "firstname");
              cellFeed.Insert(cellEntry);
              cellEntry = new CellEntry(1, 2, "lastname");
              cellFeed.Insert(cellEntry);
              cellEntry = new CellEntry(1, 3, "age");

              cellFeed.Insert(cellEntry);
              cellEntry = new CellEntry(1, 4, "height");
              cellFeed.Insert(cellEntry);

              // Create a local representation of the new row.
              ListEntry row = new ListEntry();
              row.Elements.Add(new ListEntry.Custom() { LocalName = "firstname", Value = "Joe" });
              row.Elements.Add(new ListEntry.Custom() { LocalName = "lastname", Value = "Smith" });
              row.Elements.Add(new ListEntry.Custom() { LocalName = "age", Value = "26" });
              row.Elements.Add(new ListEntry.Custom() { LocalName = "height", Value = "176" });

              // Send the new row to the API for insertion.
              service.Insert(listFeed, row);
            row.Elements.Add(new ListEntry.Custom() { LocalName = "firstname", Value = "Joe" });
            row.Elements.Add(new ListEntry.Custom() { LocalName = "lastname", Value = "Smith" });
            row.Elements.Add(new ListEntry.Custom() { LocalName = "age", Value = "26" });
            row.Elements.Add(new ListEntry.Custom() { LocalName = "height", Value = "176" });

            // Send the new row to the API for insertion.
            service.Insert(listFeed, row);


        }

        private void плагиныToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void googleDriveToolStripMenuItem_Click(object sender, EventArgs e)
        {
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
            //auth.URL = authorizationUrl;
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
            auth.Close();
        }

        private void sNMPWalkerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProgramTabControl.Controls.Add(new ManagerWindow("Test", service, TargetTable, SharedDll));
            ProgramTabControl.SelectTab(ProgramTabControl.TabCount - 1);
        }

        private void закрытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Закрыть данное окно?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                ProgramTabControl.TabPages.Remove(ProgramTabControl.SelectedTab);
        }

        private void KeyPicture_MouseEnter(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            IcoProgram.Visible = false;
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
            int  num = numbers.Peek();
            numbers.Pop();
            if(num >= count)
                numbers.Push(++count);
            ProgramTabControl.Controls.Add(new ManagerWindow(string.Format("Менеджер {0}", num), service, TargetTable, SharedDll));

            ProgramTabControl.SelectTab(ProgramTabControl.TabCount - 1);
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (ProgramTabControl.SelectedTab.Text != "Обозреватель" && MessageBox.Show("Закрыть данное окно?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                numbers.Push(Convert.ToInt32(ProgramTabControl.SelectedTab.Text.Remove(0, ProgramTabControl.SelectedTab.Text.IndexOf(' '))));
                ProgramTabControl.TabPages.Remove(ProgramTabControl.SelectedTab);
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
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }
    }
}
