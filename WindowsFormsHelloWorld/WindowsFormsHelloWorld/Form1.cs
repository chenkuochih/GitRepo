using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsHelloWorld
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Chinese_button_Click(object sender, EventArgs e)
        {
            this.display.Text = HelloWorld2.Hello();
        }

        private void English_button_Click(object sender, EventArgs e)
        {
            this.display.Text = HelloWorld1.Hello();
        }
    }
    public class HelloWorld1
    {
        public static string Hello()
        {
            return "Hello World!";
        }
    }
    public class HelloWorld2
    {
        public static string Hello()
        {
            return "你好，世界！";
        }
    }
}
