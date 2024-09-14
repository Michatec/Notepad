using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace New_Notepad
{
    public partial class Settings : Form
    {
        private int BS = 0;
        public Settings()
        {
            InitializeComponent();
            BS = Properties.Settings.Default.BS;
            button1.Text = BS == 1 ? "Aus" : "An";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "An")
            {
                button1.Text = "Aus";
                BS = 1;
            }else{
                button1.Text = "An";
                BS = 0;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.BS = BS;
            Properties.Settings.Default.Save();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
