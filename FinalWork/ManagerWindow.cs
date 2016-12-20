using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using SnmpSharpNet;
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

namespace FinalWork
{
    class OIDValueType
    {
        string IP;
        List<string> _OIDs;
        List<string> _Values;
        List<string> _Types;
        public OIDValueType(string ip)
        {
            IP = ip;
            _OIDs = new List<string>();
            _Values = new List<string>();
            _Types = new List<string>();
        }
        public void AddOIDValueType(string OID, string Val, string Type)
        {
            _OIDs.Add(OID);
            _Values.Add(Val);
            _Types.Add(Type);
        }
        public ListEntry GetCustom(int index)
        {
            ListEntry row = new ListEntry();
            row.Elements.Add(new ListEntry.Custom() { LocalName = "oid", Value = _OIDs[index] });
            row.Elements.Add(new ListEntry.Custom() { LocalName = "value", Value = _Values[index] });
            row.Elements.Add(new ListEntry.Custom() { LocalName = "type", Value = _Types[index] });
            return row;
        }
        public int Count
        {
            get { return _OIDs.Count; }
        }
        public string[] GetOIDsAsArray
        {
            get { return _OIDs.ToArray(); }
        }
        public string[] GetValuesAsArray
        {
            get { return _Values.ToArray(); }
        }
        public string[] GetTypesAsArray
        {
            get { return _Types.ToArray(); }
        }
    }
    class ManagerWindow:TabPage
    {
        List<DllManager> Mydlls = new List<DllManager>();
        List<string> Vals = new List<string>();
        List<object> Params = new List<object>();
        List<OIDValueType> ListData = new List<OIDValueType>();
        //Google
        SpreadsheetsService service;
        SpreadsheetEntry TargetTable;



        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.TabControl ProgramTabs;
        private System.Windows.Forms.ComboBox FuncBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RichTextBox outData;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader Num;
        private System.Windows.Forms.ColumnHeader FuncName;
        private System.Windows.Forms.ColumnHeader DllName;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;

        private System.Windows.Forms.ProgressBar progressBar1;


        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Params.Clear();
            Vals.Clear();
            listView1.Items.Clear();
        }
        private void LoadDll()
        {
            string[] dlls = Directory.GetFiles(Directory.GetCurrentDirectory()+"\\dll", "*.dll");
            for (int i = 0; i < dlls.Length; i++)
            {
                dlls[i] = dlls[i].Remove(0, dlls[i].LastIndexOf('\\') + 1);
            }
            for (int i = 0; i < dlls.Length; i++)
            {
                Mydlls.Add(new DllManager("dll\\"+dlls[i], i));
            }
        }

        private void LoadCombobox()
        {
            FuncBox.DisplayMember = "Value";
            FuncBox.ValueMember = "Key";
            Dictionary<string, string> comboSource = new Dictionary<string, string>();
            for (int i = 0; i < Mydlls.Count; i++)
            {
                for (int j = 0; j < Mydlls[i].FunctionsCount; j++)
                    if(Mydlls[i].GetFunction(j).Name != "FormComponets")
                        comboSource.Add(Mydlls[i].Name+ ".dll" + j.ToString(), Mydlls[i].GetFunction(j).Name);
            }
            comboSource.Add("Google API ", "DataToGoogleTable");
            FuncBox.DataSource = new BindingSource(comboSource, null);
        }

        private void button3_Click(object sender, EventArgs e)
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
                        Mydlls.Add(new DllManager(openFileDialog1.FileName, Mydlls.Count));
                        LoadCombobox();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listView1.SelectedItems.Count != 1)
            {
                return;
            }
            string FunctionName = listView1.Items[listView1.SelectedItems[0].Index].SubItems[1].Text;
            Params[listView1.SelectedItems[0].Index] = GetParamsByFnctionName(FunctionName, listView1.SelectedItems[0].Index);
        }
        private string ToGoogleTable()
        {
            WorksheetFeed wsFeed = TargetTable.Worksheets;
            WorksheetEntry worksheet = (WorksheetEntry)wsFeed.Entries[0];
            // Define the URL to request the list feed of the worksheet.
            AtomLink listFeedLink = worksheet.Links.FindService(GDataSpreadsheetsNameTable.ListRel, null);

            // Fetch the list feed of the worksheet.
            ListQuery listQuery = new ListQuery(listFeedLink.HRef.ToString());
            ListFeed listFeed = service.Query(listQuery);

            CellQuery cellQuery = new CellQuery(worksheet.CellFeedLink);
            CellFeed cellFeed = service.Query(cellQuery);

            CellEntry cellEntry = new CellEntry(1, 1, "oid");
            cellFeed.Insert(cellEntry);
            cellEntry = new CellEntry(1, 2, "value");
            cellFeed.Insert(cellEntry);
            cellEntry = new CellEntry(1, 3, "type");
            cellFeed.Insert(cellEntry);
            progressBar1.Maximum = ListData[ListData.Count - 1].Count;
            progressBar1.Value = 0;
            progressBar1.Visible = true;
            label2.Visible = true;
            for (int i = 0; i < ListData[ListData.Count - 1].Count; i++)
            {
                progressBar1.Value++;
                label3.Text = string.Format("Выполнено {0}/{1}", progressBar1.Value, ListData[ListData.Count - 1].Count);
                service.Insert(listFeed, ListData[ListData.Count - 1].GetCustom(i));
            }
            progressBar1.Visible = false;
            label2.Visible = false;
            return "G Ok";
        }
        private long SNMPWALK(List<string> Vals)
        {
            try
            {
                string host = Vals[0];
                string community = Vals[1];
                SimpleSnmp snmp = new SimpleSnmp(host, community);
                if (!snmp.Valid)
                {
                    Console.WriteLine("SNMP agent host name/ip address is invalid.");
                    return 0;
                }
                Dictionary<Oid, AsnType> result = snmp.Walk(SnmpVersion.Ver1, Vals[2]);
                if (result == null)
                {
                    label1.Text = "No results received.";
                    return 0;
                }
                ListData.Add(new OIDValueType(Vals[0]));
                foreach (KeyValuePair<Oid, AsnType> kvp in result)
                {
                    ListData[ListData.Count - 1].AddOIDValueType(kvp.Key.ToString(), kvp.Value.ToString().Trim(), SnmpConstants.GetTypeName(kvp.Value.Type));
                }
                return (long)GetDllByName("SNMPData").CallFunctions("Initializate", new object[] { Vals[0], Vals[3], ListData[ListData.Count - 1].GetOIDsAsArray, ListData[ListData.Count - 1].GetValuesAsArray, ListData[ListData.Count - 1].GetTypesAsArray, ListData[ListData.Count - 1].Count });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return 0;
            }
        }

        private DllManager GetDllByName(string dllName)
        {
            if (dllName.IndexOf('.') != -1)
            {
                dllName = dllName.Remove(dllName.IndexOf('.'));
            }
                for (int i = 0; i < Mydlls.Count; i++)
                    if (Mydlls[i].Name == dllName)
                        return Mydlls[i];
            return null;
        }
        private void FuncBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string pluginName = FuncBox.SelectedValue.ToString().Remove(FuncBox.SelectedValue.ToString().Length - 1);
            DllManager currentDll = GetDllByName(pluginName);
            if (currentDll != null)
                label1.Text = currentDll.Info;
            else
            {
                label1.Text = FuncBox.SelectedValue.ToString();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (FuncBox.SelectedIndex != -1)
            {
                string[] row = { (listView1.Items.Count + 1).ToString(), FuncBox.Text, FuncBox.SelectedValue.ToString().Remove(FuncBox.SelectedValue.ToString().Length - 1) };
                var listViewItem = new ListViewItem(row);
                listView1.Items.Add(listViewItem);
                Params.Add(GetParamsByFnctionName(FuncBox.Text, listView1.Items.Count));
            }
            else return;

        }
        private object GetParamsByFnctionName(string FunctionName, int FunctionIndex)
        {
            try
            {
                switch (FunctionName)
                {
                    case "Initializate":
                        StringBuilder fields = new StringBuilder(2048);
                        List<string> Vals = new List<string>();
                        InputForm inf = new InputForm();
                        GetDllByName("SNMPData").CallFunctions("FormComponets", new object[] { fields, fields.Capacity });//InitializateParameters
                        if (Params.Count - 1 >= FunctionIndex && Vals.Count != 0)
                        {
                            Vals = (List<string>)Params[FunctionIndex];
                            inf.AddControls(fields.ToString().Split(new char[] { ';' }), Vals);
                        }
                        else
                            inf.AddControls(fields.ToString().Split(new char[] { ';' }));

                        inf.ShowDialog();
                        if (inf.DialogResult != DialogResult.OK)
                        {
                            //Vals = inf.FieldValues;
                            return Vals;
                        }
                        else
                            Vals = inf.FieldValues;
                        return Vals;
                    default: return null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                this.label1.Text = "";
                long MyData = 0;
                for (int i = 0; i < listView1.Items.Count; i++)
                {
                    string res = "";
                    switch (listView1.Items[i].SubItems[1].Text)
                    {
                        case "DataToGoogleTable": res = ToGoogleTable();break;
                        case "Initializate":
                            MyData = SNMPWALK((List<string>)Params[i]); break;
                        case "EncodeHexProperty": MyData = (long)GetDllByName(listView1.Items[i].SubItems[2].Text).CallFunctions("EncodeHexProperty", new object[] { MyData }); break;
                        case "SortDataByProperties": MyData = (long)GetDllByName(listView1.Items[i].SubItems[2].Text).CallFunctions("SortDataByProperties", new object[] { MyData }); break;
                        case "HtmlExport": res = GetDllByName(listView1.Items[i].SubItems[2].Text).CallFunctions("HtmlExport", new object[] { MyData }).ToString(); break;
                        case "CSVExport": res = GetDllByName(listView1.Items[i].SubItems[2].Text).CallFunctions("CSVExport", new object[] { MyData }).ToString(); break;
                        default: break;
                    }
                    if (res != "")
                        outData.AppendText(listView1.Items[i].SubItems[1].Text + " = " + res + "\n");
                    else
                        outData.AppendText(listView1.Items[i].SubItems[1].Text + "->OK" + "\n");
                    res = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void CustomLayout()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ProgramTabs = new System.Windows.Forms.TabControl();
            this.FuncBox = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.outData = new System.Windows.Forms.RichTextBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.Num = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FuncName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DllName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button3 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuStrip2.SuspendLayout();
            this.ProgramTabs.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(8, 268);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(535, 14);
            this.progressBar1.TabIndex = 7;
            this.progressBar1.Value = 0;
            this.progressBar1.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(252, 289);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "";
            this.label3.Visible = false;
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(102, 26);
           // this.contextMenuStrip2.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip2_Opening);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(101, 22);
            this.clearToolStripMenuItem.Text = "Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // ProgramTabs
            // 
            this.ProgramTabs.Location = new System.Drawing.Point(12, 25);
            this.ProgramTabs.Name = "ProgramTabs";
            this.ProgramTabs.SelectedIndex = 0;
            this.ProgramTabs.Size = new System.Drawing.Size(662, 229);
            this.ProgramTabs.TabIndex = 9;
            // 
            // tabPage1
            // 
            this.Controls.Add(this.FuncBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Location = new System.Drawing.Point(4, 22);
            this.Name = "tabPage1";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Size = new System.Drawing.Size(654, 203);
            this.TabIndex = 0;
            //this.Text = "SNMP Walker";
            this.UseVisualStyleBackColor = true;
            // 
            // FuncBox
            // 
            this.FuncBox.FormattingEnabled = true;
            this.FuncBox.Location = new System.Drawing.Point(14, 17);
            this.FuncBox.Name = "FuncBox";
            this.FuncBox.Size = new System.Drawing.Size(159, 21);
            this.FuncBox.TabIndex = 19;
            FuncBox.SelectedIndexChanged += new System.EventHandler(this.FuncBox_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(363, 17);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 17;
            this.button1.Text = "Запуск";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 24;
            this.label2.Text = "Очередь";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(179, 17);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(86, 23);
            this.button2.TabIndex = 18;
            this.button2.Text = "Добавить";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(302, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 24;
            this.label4.Text = "Результаты";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.outData);
            this.panel1.Location = new System.Drawing.Point(300, 57);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(251, 116);
            this.panel1.TabIndex = 23;
            // 
            // outData
            // 
            this.outData.Location = new System.Drawing.Point(2, 3);
            this.outData.Name = "outData";
            this.outData.Size = new System.Drawing.Size(246, 110);
            this.outData.TabIndex = 9;
            this.outData.Text = "";
            // 
            // listView1
            // 
            this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Num,
            this.FuncName,
            this.DllName});
            this.listView1.ContextMenuStrip = this.contextMenuStrip2;
            this.listView1.GridLines = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView1.Location = new System.Drawing.Point(14, 59);
            //this.listView1.Location = new System.Drawing.Point(271, 60);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.ShowItemToolTips = true;
            this.listView1.Size = new System.Drawing.Size(285, 110);
            this.listView1.TabIndex = 20;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.MouseDoubleClick += new MouseEventHandler(listView1_MouseDoubleClick);
            // 
            // Num
            // 
            this.Num.Text = "#";
            this.Num.Width = 30;
            // 
            // FuncName
            // 
            this.FuncName.Text = "Процесс";
            this.FuncName.Width = 130;
            // 
            // DllName
            // 
            this.DllName.Text = "Источник";
            this.DllName.Width = 120;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(271, 17);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(86, 23);
            this.button3.TabIndex = 22;
            this.button3.Text = "Add DLL";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(13, 176);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 20);
            this.label1.TabIndex = 21;
            this.label1.Text = "";
            //
            //
            //
            this.contextMenuStrip2.ResumeLayout(false);
        }

        public ManagerWindow(string PageName, SpreadsheetsService _service, SpreadsheetEntry _TargetTable, List<DllManager> SharedDll)
        {
            Mydlls = SharedDll;
            service = _service;
            TargetTable = _TargetTable;
            this.Text = PageName;
            CustomLayout();
           // LoadDll();
            LoadCombobox();
        }
    }
}
