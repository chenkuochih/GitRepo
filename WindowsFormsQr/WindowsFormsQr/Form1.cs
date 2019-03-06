using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;

namespace WindowsFormsQr
{
    public partial class CQY : Form
    {
        public CQY()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {      
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string sampleText = textBox1.Text;
            if (sampleText.Length > 30)
            {
                message_lable.Text = "您输入的数字符长度大于30，请重新输入！";
            }
            else if (sampleText.Length == 0)
            {
                message_lable.Text = "您未输入任何字符，请重新输入！";
            }
            else
            {
                message_lable.Text = "";
                //QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
                //QrCode qrCode = new QrCode();
                //qrEncoder.TryEncode(sampleText, out qrCode);
                //GraphicsRenderer gGender = new GraphicsRenderer(new FixedModuleSize(30, QuietZoneModules.Four));
                //MemoryStream ms = new MemoryStream();
                //gGender.WriteToStream(qrCode.Matrix, ImageFormat.Png, ms);
                qrCodeGraphicControl.Text = sampleText;
            }
        }
    }
}
