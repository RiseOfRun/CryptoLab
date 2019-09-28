using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string alpha = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            Dictionary<char, char> key = new Dictionary<char, char>();
            for (int i = 0; i < alpha.Length; ++i)
            {
                key[alpha[i]] = textBox3.Text[i];
            }
            textBox1.Text = "";
            for (int i = 0; i < textBox2.Text.Length; ++i)
            {
                if (key.ContainsKey(textBox2.Text[i]))
                    textBox1.Text += key[textBox2.Text[i]];
            }


        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }
    }
}


