using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tipy
{
    public partial class datachange : Form
    {
        public datachange()
        {
            InitializeComponent();
        }

        private void datachange_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Visible == true) Visible = false;
            else Visible = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            functions.datachange(textBox2.Text,textBox3.Text,textBox4.Text,textBox5.Text);
            logged l = new logged();
            l.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
