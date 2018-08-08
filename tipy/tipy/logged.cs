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
    public partial class logged : Form
    {
        public logged()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
           // functions.logout();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            datachange d = new datachange();
            d.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            functions.logout();
            Form1 f = new Form1();
            f.Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            functions.blist();
            blacklist f = new blacklist();
            f.Show();
            this.Hide();
        }

        private void logged_Load(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            functions.friendson();
            functions.friendsoff();
            list f = new list();
            f.Show();
            this.Hide();
        }

        private void radioButton1_MouseClick(object sender, MouseEventArgs e)
        {
            functions.online();
        }

        private void radioButton2_MouseClick(object sender, MouseEventArgs e)
        {
            functions.logout();
        }
    }
}
