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
    public partial class list : Form
    {
        public list()
        {
            
            InitializeComponent();
            if(functions.on.Count()!=0)
            for (int i = 0; i < functions.on.Count(); i++)
                listBox1.Items.Add(functions.on[i]);
            if (functions.off.Count() != 0)
                for (int i = 0; i < functions.off.Count(); i++)
                listBox2.Items.Add(functions.off[i]);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            functions.addcontact(textBox2.Text);
            textBox2.Text = "";
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            functions.target=listBox1.SelectedItem.ToString();
            Console.WriteLine(functions.target);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            logged f = new logged();
            f.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            functions.frienddelete(functions.target);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            functions.target = listBox2.SelectedItem.ToString();
            Console.WriteLine(functions.target);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
