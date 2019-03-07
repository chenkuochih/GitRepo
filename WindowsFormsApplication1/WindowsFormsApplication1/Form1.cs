using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ThoughtWorks.QRCode;
using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sampleText = textBox1.Text;
            if (sampleText.Length > 30)
            {
                message_lable.Text = "字符长度大于30，请重新输入！";//限制输入的字符个数
            }
            else if (sampleText.Length == 0)
            {
                message_lable.Text = "未输入任何字符，请重新输入！";
            }
            else
            {
                if (textBox1.ForeColor == Color.Black)
                {
                    message_lable.Text = "";
                    if (!string.IsNullOrEmpty(textBox1.Text.Trim()))
                    {
                        string enCodeString = textBox1.Text.Trim();
                        QRCodeEncoder codeEncoder = new QRCodeEncoder();
                        pictureBox1.Image = codeEncoder.Encode(enCodeString, Encoding.UTF8);
                    }                  
                }
                else
                {
                    message_lable.Text = "未输入任何字符，请重新输入！";
                    textBox1.Text = "";
                }
            }
        }

        private void ToolStripMenuItem_Click(object sender, EventArgs e)//二维码另存为（右键）
        {
            if (pictureBox1 .Image!=null)
            {
                SaveFileDialog s = new SaveFileDialog();
                s.Title = "保存二维码图片";
                s.Filter = "图片文件(*.jpg)|*.jpg";
                if (s.ShowDialog()==DialogResult.OK)
                {
                    pictureBox1.Image.Save(s.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                    MessageBox.Show("保存成功");
                }
            }
        }

        string filename = string.Empty;
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog p = new OpenFileDialog();
            p.Title = "请选择二维码图片";
            p.Filter = "图片格式(*.jpg)|*.jpg";
            p.Multiselect = false;
            if (p.ShowDialog()==DialogResult.OK)
            {
                filename = p.FileName;
                ss();
            }
        }

        private void ss()
        {
            pictureBox1.Image = new Bitmap(filename);
            QRCodeDecoder qrDecoder = new QRCodeDecoder();
            string msg= qrDecoder.decode(new QRCodeBitmapImage(new Bitmap(pictureBox1.Image)), Encoding.UTF8);
            textBox1.Text = msg;
            textBox1.ForeColor = Color.Blue;//解码后的文字颜色
            MessageBox.Show("解析完成");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textbox1_click(object sender, EventArgs e)
        {
            if(message_lable.Text.Length == 0)
            {
                textBox1.Text = "";
            }
            pictureBox1.Image = null;//单击文本框二维码消失
            textBox1.ForeColor = Color.Black;//为了让文本框的文本颜色与注释不相同
            message_lable.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                SaveFileDialog s = new SaveFileDialog();
                s.Title = "保存二维码图片";
                s.Filter = "图片文件(*.jpg)|*.jpg";
                if (s.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image.Save(s.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                    MessageBox.Show("保存成功");
                }
            }
        }
    }
}
