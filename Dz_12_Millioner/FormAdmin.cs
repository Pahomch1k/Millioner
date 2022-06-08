using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Dz_12_Millioner
{
    public partial class FormAdmin : Form
    {
        public FormAdmin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "")
                MessageBox.Show("Заполните все поля"); 
            else
            { 
                StreamWriter swQ = new StreamWriter("questoin2.txt", true);
                StreamWriter swA = new StreamWriter("Answers2.txt", true);

                string line = textBox1.Text + "\n" + textBox2.Text + "\n" + textBox3.Text + "\n" + textBox4.Text + "\n" + textBox5.Text; 
                swQ.WriteLine(line);  
                swQ.Close();

                line = textBox6.Text;
                swA.WriteLine(line);
                swA.Close();
            }
        }
    }
}
