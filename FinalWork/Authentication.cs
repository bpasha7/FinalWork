using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalWork
{
    public partial class Authentication : Form
    {

        string _Key;
        string _URL;

        public string Key
        {
            get { return _Key; }
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
            _Key = KeyBox.Text;
            this.Close();
        }

        private void Authentication_Load(object sender, EventArgs e)
        {
            try
            {
                //webBrowser1.Navigate(new Uri(_URL));
            }
            catch (System.UriFormatException)
            {
                return;
            }
        }
    }
}
