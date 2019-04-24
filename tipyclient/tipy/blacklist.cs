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
    public partial class blacklist : Form
    {
        public blacklist()
        {
            InitializeComponent();
            if (functions.bllist.Count() != 0)
                for (int i = 0; i < functions.bllist.Count(); i++)
                    if (functions.bllist[i] != " ")
                        listBox1.Items.Add(functions.bllist[i]);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
             
        }

        private void button1_Click(object sender, EventArgs e)
        {
            functions.addblist(textBox2.Text);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            functions.blistdelete(functions.target);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            functions.target = listBox1.SelectedItem.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Visible == true) Visible = false;
            else Visible = true;
        }
    }
}
