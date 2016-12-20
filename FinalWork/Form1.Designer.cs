namespace FinalWork
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.менюToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.плагиныToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.окнаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.открытьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sNMPWalkerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.закрытьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.подключитьсяToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.справкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ProgramTabControl = new System.Windows.Forms.TabControl();
            this.TabsMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.закрытьВкладкуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.TableInfo = new System.Windows.Forms.Label();
            this.TakeTable = new System.Windows.Forms.LinkLabel();
            this.UpdateGoogleTables = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.GoogleTableListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button2 = new System.Windows.Forms.Button();
            this.IcoProgram = new System.Windows.Forms.NotifyIcon(this.components);
            this.KeyToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.GoogleToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.DBtoolTip = new System.Windows.Forms.ToolTip(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.KeyPicture = new System.Windows.Forms.PictureBox();
            this.GooglePicture = new System.Windows.Forms.PictureBox();
            this.DBPicture = new System.Windows.Forms.PictureBox();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.googleDriveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.yandexDiskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.обозревательToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.TabLogs = new System.Windows.Forms.TabControl();
            this.TabLog = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.menuStrip1.SuspendLayout();
            this.ProgramTabControl.SuspendLayout();
            this.TabsMenuStrip.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.KeyPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GooglePicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DBPicture)).BeginInit();
            this.TabLogs.SuspendLayout();
            this.TabLog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.менюToolStripMenuItem,
            this.плагиныToolStripMenuItem,
            this.окнаToolStripMenuItem,
            this.подключитьсяToolStripMenuItem,
            this.справкаToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(561, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "ProgramMenuStrip";
            // 
            // менюToolStripMenuItem
            // 
            this.менюToolStripMenuItem.Name = "менюToolStripMenuItem";
            this.менюToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.менюToolStripMenuItem.Text = "Меню";
            // 
            // плагиныToolStripMenuItem
            // 
            this.плагиныToolStripMenuItem.Name = "плагиныToolStripMenuItem";
            this.плагиныToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.плагиныToolStripMenuItem.Text = "Плагины";
            this.плагиныToolStripMenuItem.Click += new System.EventHandler(this.плагиныToolStripMenuItem_Click);
            // 
            // окнаToolStripMenuItem
            // 
            this.окнаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.открытьToolStripMenuItem,
            this.закрытьToolStripMenuItem});
            this.окнаToolStripMenuItem.Name = "окнаToolStripMenuItem";
            this.окнаToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.окнаToolStripMenuItem.Text = "Окна";
            // 
            // открытьToolStripMenuItem
            // 
            this.открытьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sNMPWalkerToolStripMenuItem,
            this.обозревательToolStripMenuItem});
            this.открытьToolStripMenuItem.Name = "открытьToolStripMenuItem";
            this.открытьToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.открытьToolStripMenuItem.Text = "Открыть";
            // 
            // sNMPWalkerToolStripMenuItem
            // 
            this.sNMPWalkerToolStripMenuItem.Name = "sNMPWalkerToolStripMenuItem";
            this.sNMPWalkerToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.sNMPWalkerToolStripMenuItem.Text = "SNMP Walker";
            this.sNMPWalkerToolStripMenuItem.Click += new System.EventHandler(this.sNMPWalkerToolStripMenuItem_Click);
            // 
            // закрытьToolStripMenuItem
            // 
            this.закрытьToolStripMenuItem.Name = "закрытьToolStripMenuItem";
            this.закрытьToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.закрытьToolStripMenuItem.Text = "Закрыть";
            this.закрытьToolStripMenuItem.Click += new System.EventHandler(this.закрытьToolStripMenuItem_Click);
            // 
            // подключитьсяToolStripMenuItem
            // 
            this.подключитьсяToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.googleDriveToolStripMenuItem,
            this.yandexDiskToolStripMenuItem});
            this.подключитьсяToolStripMenuItem.Name = "подключитьсяToolStripMenuItem";
            this.подключитьсяToolStripMenuItem.Size = new System.Drawing.Size(101, 20);
            this.подключитьсяToolStripMenuItem.Text = "Подключиться";
            // 
            // справкаToolStripMenuItem
            // 
            this.справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            this.справкаToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.справкаToolStripMenuItem.Text = "Справка";
            // 
            // ProgramTabControl
            // 
            this.ProgramTabControl.ContextMenuStrip = this.TabsMenuStrip;
            this.ProgramTabControl.Controls.Add(this.tabPage1);
            this.ProgramTabControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ProgramTabControl.Location = new System.Drawing.Point(0, 50);
            this.ProgramTabControl.Name = "ProgramTabControl";
            this.ProgramTabControl.SelectedIndex = 0;
            this.ProgramTabControl.Size = new System.Drawing.Size(562, 342);
            this.ProgramTabControl.TabIndex = 1;
            // 
            // TabsMenuStrip
            // 
            this.TabsMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.закрытьВкладкуToolStripMenuItem});
            this.TabsMenuStrip.Name = "TabsMenuStrip";
            this.TabsMenuStrip.Size = new System.Drawing.Size(167, 26);
            // 
            // закрытьВкладкуToolStripMenuItem
            // 
            this.закрытьВкладкуToolStripMenuItem.Name = "закрытьВкладкуToolStripMenuItem";
            this.закрытьВкладкуToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.закрытьВкладкуToolStripMenuItem.Text = "Закрыть вкладку";
            this.закрытьВкладкуToolStripMenuItem.Click += new System.EventHandler(this.закрытьВкладкуToolStripMenuItem_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.tabPage1.Controls.Add(this.TabLogs);
            this.tabPage1.Controls.Add(this.TableInfo);
            this.tabPage1.Controls.Add(this.TakeTable);
            this.tabPage1.Controls.Add(this.UpdateGoogleTables);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.GoogleTableListView);
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(554, 313);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Обозреватель";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // TableInfo
            // 
            this.TableInfo.AutoSize = true;
            this.TableInfo.Location = new System.Drawing.Point(8, 120);
            this.TableInfo.Name = "TableInfo";
            this.TableInfo.Size = new System.Drawing.Size(59, 16);
            this.TableInfo.TabIndex = 6;
            this.TableInfo.Text = "tableInfo";
            this.TableInfo.Visible = false;
            // 
            // TakeTable
            // 
            this.TakeTable.ActiveLinkColor = System.Drawing.Color.Orange;
            this.TakeTable.AutoSize = true;
            this.TakeTable.DisabledLinkColor = System.Drawing.Color.Black;
            this.TakeTable.LinkColor = System.Drawing.Color.Red;
            this.TakeTable.Location = new System.Drawing.Point(8, 94);
            this.TakeTable.Name = "TakeTable";
            this.TakeTable.Size = new System.Drawing.Size(122, 16);
            this.TakeTable.TabIndex = 5;
            this.TakeTable.TabStop = true;
            this.TakeTable.Text = "Выбрать таблицу";
            this.TakeTable.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.TakeTable_LinkClicked);
            // 
            // UpdateGoogleTables
            // 
            this.UpdateGoogleTables.ActiveLinkColor = System.Drawing.Color.Red;
            this.UpdateGoogleTables.AutoSize = true;
            this.UpdateGoogleTables.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.UpdateGoogleTables.Location = new System.Drawing.Point(269, 13);
            this.UpdateGoogleTables.Name = "UpdateGoogleTables";
            this.UpdateGoogleTables.Size = new System.Drawing.Size(72, 16);
            this.UpdateGoogleTables.TabIndex = 4;
            this.UpdateGoogleTables.TabStop = true;
            this.UpdateGoogleTables.Text = "Обновить";
            this.UpdateGoogleTables.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.UpdateGoogleTables_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 13);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(189, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Доступные Google Таблицы";
            // 
            // GoogleTableListView
            // 
            this.GoogleTableListView.BackColor = System.Drawing.SystemColors.Info;
            this.GoogleTableListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.GoogleTableListView.Cursor = System.Windows.Forms.Cursors.Hand;
            this.GoogleTableListView.Location = new System.Drawing.Point(8, 29);
            this.GoogleTableListView.MultiSelect = false;
            this.GoogleTableListView.Name = "GoogleTableListView";
            this.GoogleTableListView.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.GoogleTableListView.Size = new System.Drawing.Size(333, 62);
            this.GoogleTableListView.TabIndex = 1;
            this.GoogleTableListView.UseCompatibleStateImageBehavior = false;
            this.GoogleTableListView.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.GoogleTableListView.Leave += new System.EventHandler(this.GoogleTableListView_Leave);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(473, 117);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 0;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // IcoProgram
            // 
            this.IcoProgram.Icon = ((System.Drawing.Icon)(resources.GetObject("IcoProgram.Icon")));
            this.IcoProgram.Text = "SNMP Worker";
            this.IcoProgram.Visible = true;
            // 
            // KeyToolTip
            // 
            this.KeyToolTip.IsBalloon = true;
            this.KeyToolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.KeyToolTip.ToolTipTitle = "Доступ получен";
            // 
            // GoogleToolTip
            // 
            this.GoogleToolTip.IsBalloon = true;
            this.GoogleToolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.GoogleToolTip.ToolTipTitle = "Токен получен";
            // 
            // DBtoolTip
            // 
            this.DBtoolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.DBtoolTip.ToolTipTitle = "DataBase";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.toolStripButton2,
            this.toolStripButton3,
            this.toolStripSeparator2,
            this.toolStripButton6,
            this.toolStripButton5,
            this.toolStripButton4,
            this.toolStripSeparator3,
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(561, 25);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // KeyPicture
            // 
            this.KeyPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.KeyPicture.Image = ((System.Drawing.Image)(resources.GetObject("KeyPicture.Image")));
            this.KeyPicture.InitialImage = null;
            this.KeyPicture.Location = new System.Drawing.Point(525, 392);
            this.KeyPicture.Name = "KeyPicture";
            this.KeyPicture.Size = new System.Drawing.Size(32, 32);
            this.KeyPicture.TabIndex = 0;
            this.KeyPicture.TabStop = false;
            this.KeyPicture.MouseEnter += new System.EventHandler(this.KeyPicture_MouseEnter);
            // 
            // GooglePicture
            // 
            this.GooglePicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.GooglePicture.Image = global::FinalWork.Properties.Resources.Google_Drive1;
            this.GooglePicture.InitialImage = null;
            this.GooglePicture.Location = new System.Drawing.Point(492, 392);
            this.GooglePicture.Name = "GooglePicture";
            this.GooglePicture.Size = new System.Drawing.Size(32, 32);
            this.GooglePicture.TabIndex = 1;
            this.GooglePicture.TabStop = false;
            // 
            // DBPicture
            // 
            this.DBPicture.BackgroundImage = global::FinalWork.Properties.Resources.Base;
            this.DBPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.DBPicture.InitialImage = null;
            this.DBPicture.Location = new System.Drawing.Point(458, 392);
            this.DBPicture.Name = "DBPicture";
            this.DBPicture.Size = new System.Drawing.Size(33, 32);
            this.DBPicture.TabIndex = 2;
            this.DBPicture.TabStop = false;
            this.DBPicture.Click += new System.EventHandler(this.DBPicture_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = global::FinalWork.Properties.Resources.open;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "toolStripButton2";
            this.toolStripButton2.ToolTipText = "Загрузить .ini файл";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = global::FinalWork.Properties.Resources.Save;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton3.Text = "toolStripButton3";
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton5.Image = global::FinalWork.Properties.Resources.Action_tab;
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton5.Text = "toolStripButton5";
            this.toolStripButton5.ToolTipText = "Добавить вкладку";
            this.toolStripButton5.Click += new System.EventHandler(this.toolStripButton5_Click);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = global::FinalWork.Properties.Resources.Action_tab_remove_icon;
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton4.Text = "toolStripButton4";
            this.toolStripButton4.ToolTipText = "Закрыть вкладку";
            this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::FinalWork.Properties.Resources.Yadisk;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton6.Image = global::FinalWork.Properties.Resources.tasks;
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton6.Text = "toolStripButton6";
            this.toolStripButton6.Click += new System.EventHandler(this.toolStripButton6_Click);
            // 
            // googleDriveToolStripMenuItem
            // 
            this.googleDriveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("googleDriveToolStripMenuItem.Image")));
            this.googleDriveToolStripMenuItem.Name = "googleDriveToolStripMenuItem";
            this.googleDriveToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.googleDriveToolStripMenuItem.Text = "Google Drive";
            this.googleDriveToolStripMenuItem.Click += new System.EventHandler(this.googleDriveToolStripMenuItem_Click);
            // 
            // yandexDiskToolStripMenuItem
            // 
            this.yandexDiskToolStripMenuItem.Image = global::FinalWork.Properties.Resources.Yadisk;
            this.yandexDiskToolStripMenuItem.Name = "yandexDiskToolStripMenuItem";
            this.yandexDiskToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.yandexDiskToolStripMenuItem.Text = "Yandex Disk";
            // 
            // обозревательToolStripMenuItem
            // 
            this.обозревательToolStripMenuItem.Name = "обозревательToolStripMenuItem";
            this.обозревательToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.обозревательToolStripMenuItem.Text = "Обозреватель";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // TabLogs
            // 
            this.TabLogs.Controls.Add(this.TabLog);
            this.TabLogs.Location = new System.Drawing.Point(11, 139);
            this.TabLogs.Multiline = true;
            this.TabLogs.Name = "TabLogs";
            this.TabLogs.SelectedIndex = 0;
            this.TabLogs.Size = new System.Drawing.Size(540, 171);
            this.TabLogs.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.TabLogs.TabIndex = 7;
            // 
            // TabLog
            // 
            this.TabLog.Controls.Add(this.dataGridView1);
            this.TabLog.Location = new System.Drawing.Point(4, 25);
            this.TabLog.Name = "TabLog";
            this.TabLog.Padding = new System.Windows.Forms.Padding(3);
            this.TabLog.Size = new System.Drawing.Size(532, 142);
            this.TabLog.TabIndex = 0;
            this.TabLog.Text = "События";
            this.TabLog.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(526, 136);
            this.dataGridView1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 428);
            this.Controls.Add(this.KeyPicture);
            this.Controls.Add(this.GooglePicture);
            this.Controls.Add(this.DBPicture);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.ProgramTabControl);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SNMP Worker";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ProgramTabControl.ResumeLayout(false);
            this.TabsMenuStrip.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.KeyPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GooglePicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DBPicture)).EndInit();
            this.TabLogs.ResumeLayout(false);
            this.TabLog.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem менюToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem плагиныToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem окнаToolStripMenuItem;
        private System.Windows.Forms.TabControl ProgramTabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ContextMenuStrip TabsMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem закрытьВкладкуToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem открытьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sNMPWalkerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem закрытьToolStripMenuItem;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ToolStripMenuItem подключитьсяToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem googleDriveToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon IcoProgram;
        private System.Windows.Forms.ToolStripMenuItem справкаToolStripMenuItem;
        private System.Windows.Forms.PictureBox KeyPicture;
        private System.Windows.Forms.PictureBox DBPicture;
        private System.Windows.Forms.PictureBox GooglePicture;
        private System.Windows.Forms.ToolTip KeyToolTip;
        private System.Windows.Forms.ToolTip GoogleToolTip;
        private System.Windows.Forms.ToolTip DBtoolTip;
        private System.Windows.Forms.ToolStripMenuItem yandexDiskToolStripMenuItem;
        private System.Windows.Forms.ListView GoogleTableListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.LinkLabel UpdateGoogleTables;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel TakeTable;
        private System.Windows.Forms.Label TableInfo;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
        private System.Windows.Forms.ToolStripMenuItem обозревательToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.TabControl TabLogs;
        private System.Windows.Forms.TabPage TabLog;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}

