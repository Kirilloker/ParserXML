using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace newFileFormat
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 16; i++)
            {

                Button btn = new Button();
                btn.Text = "Нажми меня" + i.ToString();
                btn.Location = new Point(10 , 10 + 30 * i);
                btn.Size = new Size(100, 30);
                this.Controls.Add(btn);
            }

            for (int i = 0; i < 16; i++)
            {

                Button btn = new Button();
                btn.Text = "Нажми меня" + i.ToString();
                btn.Location = new Point(10 + 110 * i, 10 + 30 * i);
                btn.Size = new Size(100, 30);
                this.Controls.Add(btn);
            }

        }
    }
}
