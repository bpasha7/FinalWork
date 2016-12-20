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
    public partial class InputForm : Form
    {
        private List<TextBox> TextBoxs;
        private List<Label> Labels;

        public List<string> FieldValues
        {
            get
            {
                List<string> Vals = new List<string>();
                for (int i = 0; i < TextBoxs.Count; i++)
                    Vals.Add(TextBoxs[i].Text);
                return Vals;
            }
        }
        public void AddControls(string[] ParamName)
        {
            TextBoxs = new List<TextBox>();
            Labels = new List<Label>();
            for (int i = 0; i < ParamName.Length; i++)
            {
                TextBoxs.Add(new TextBox() { Name = ParamName[i], Location = new Point(100, 10 + i * 25) });
                Labels.Add(new Label() { Name = ParamName[i] + "Label", Location = new Point(10, 10 + i * 25), Text = ParamName[i] });
            }
            button1.Location = new Point(10, ParamName.Length * 25 + 10);
            this.Height = ParamName.Length * 25 + 85;
        }
        public void AddControls(string[] ParamName, List<string> LastData)
        {
            TextBoxs = new List<TextBox>();
            Labels = new List<Label>();
            for (int i = 0; i < ParamName.Length; i++)
            {
                TextBoxs.Add(new TextBox() { Name = ParamName[i], Location = new Point(100, 10 + i * 25), Text = LastData[i] });
                Labels.Add(new Label() { Name = ParamName[i] + "Label", Location = new Point(10, 10 + i * 25), Text = ParamName[i] });
            }
            button1.Location = new Point(10, ParamName.Length * 25 + 10);
            this.Height = ParamName.Length * 25 + 85;
        }
        public InputForm()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void InputForm_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < TextBoxs.Count; i++)
            {
                this.Controls.Add(TextBoxs[i]);
                this.Controls.Add(Labels[i]);
            }
        }
    }
}
