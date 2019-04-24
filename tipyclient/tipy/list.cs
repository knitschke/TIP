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
                    if(functions.on[i]!=" ")
                listBox1.Items.Add(functions.on[i]);
            if (functions.off.Count() != 0)
                for (int i = 0; i < functions.off.Count(); i++)
                listBox2.Items.Add(functions.off[i]);

        }

        public string GetIP()
        {
            return functions.call(functions.target);
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
            if (Visible == true) Visible = false;
            else Visible = true;
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
            functions.call(functions.target);

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

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //functions.port1 = Int32.Parse(textBox3.Text);
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            //functions.port2 = Int32.Parse(textBox4.Text);
        }
    }
}
