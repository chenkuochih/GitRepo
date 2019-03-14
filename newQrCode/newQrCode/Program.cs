using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
//using ThoughtWorks.QRCode.Codec;

namespace newQrCode
{
    class Program
    {
        public static string[] Read(string args)
        {
            //string strReadFilePath;
            //// 读取文件的源路径及其读取流
            //strReadFilePath = Console.ReadLine();
            //StreamReader srReadFile = new StreamReader(strReadFilePath);
            StreamReader srReadFile = new StreamReader(args);
            // 读取流直至文件末尾结束
            int line = 0;
            string[] strReadLine = new string[100];
            while (!srReadFile.EndOfStream)
            {
                strReadLine[line] = srReadFile.ReadLine(); //读取每行数据
                line++;
                //Console.WriteLine(strReadLine); //屏幕打印每行数据
            }
            // 关闭读取流文件
            srReadFile.Close();
            return strReadLine;
        }


        public static void printQrEncoder(string args)
        {
            string[] SampleText = new string[100];
            SampleText = Read(args);
            for (int i = 0; SampleText[i] != null; i++)
            {
                if (SampleText[i].Length < 30 && SampleText[i].Length > 0)
                {
                    QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.M);
                    QrCode qrCode = qrEncoder.Encode(SampleText[i]);
                    for (int j = 0; j < qrCode.Matrix.Width; j++)
                    {
                        for (int k = 0; k < qrCode.Matrix.Width; k++)
                        {
                            char charToPrint = qrCode.Matrix[k, j] ? '□' : '■';
                            Console.Write(charToPrint);
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine();
                }
            }
        }

        public static void printQrEncoder2(string args)
        {
            string SampleText = args;
            if (SampleText.Length < 30 && SampleText.Length > 0)
            {
                QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.M);
                QrCode qrCode = qrEncoder.Encode(SampleText);
                for (int j = 0; j < qrCode.Matrix.Width; j++)
                {
                    for (int k = 0; k < qrCode.Matrix.Width; k++)
                    {
                        char charToPrint = qrCode.Matrix[k, j] ? '□' : '■';
                        Console.Write(charToPrint);
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }

        public static string ThreeDigits(int i)
        {
            if(i>0 && i < 10)
            {
                return "00" + i;
            }
            else if(i<100)
            {
                return "0" + i;
            }
            else
            {
                return i.ToString();
            }
        }

        public static string CreateFile()
        {
            string strReadFilePath;
            // 读取文件的源路径及其读取流
            strReadFilePath = Console.ReadLine();
            return strReadFilePath;
        }

        /// <summary>
        /// 生成二维码，并保存图片到指定路径下
        /// </summary>
        /// <param name="fileName">图片保存路径全名（包括路径和文件名）</param>
        /// <param name="content">要生成二维码的内容</param>
        public static void GenQrCode(string args)
        {
            //Console.WriteLine(args);
            string[] SampleText = new string[1000];
            SampleText = Read(args);
            Console.WriteLine("请输入要保存的文件路径：");
            String fileName2 = CreateFile();
            ////每一次执行前都把文件夹清空。
            //foreach (FileInfo file in (new DirectoryInfo(fileName2)).GetFiles())
            //{
            //    file.Attributes = FileAttributes.Normal;
            //    file.Delete();
            //}
            //判断是否存在这个文件夹，如果不存在，主动创一个
            if (!Directory.Exists(fileName2))
            {
                Directory.CreateDirectory(fileName2);
            }
            if (SampleText != null)
            {
                for (int i = 0; SampleText[i]!=null; i++)
                {
                    //Console.WriteLine(i);
                    string Name = SampleText[i];
                    string fileName;
                    if (Name.Length <= 30 && Name.Length > 0)//限制条件
                    {
                        fileName = fileName2 + ThreeDigits(i+1) + Name.Substring(0, 4) + ".bmp";
                        var qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
                        var qrCode = qrEncoder.Encode(SampleText[i]);//生成二维码
                        GraphicsRenderer gRender = new GraphicsRenderer(new FixedModuleSize(30, QuietZoneModules.Four));
                        using (FileStream stream = new FileStream(fileName, FileMode.Create))
                        {
                            gRender.WriteToStream(qrCode.Matrix, ImageFormat.Bmp, stream, new Point(600, 600));//生成图片
                        }
                    }
                }
            }
            
           
        }

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("请您根据以下提示选择输入参数：");
                Console.WriteLine("批量生成二维码图片并保存到文件夹：-f+生成二维码的字符串文件夹路径");
                Console.WriteLine("              例：-fE:/GitRepo/myqrcode.txt");
                Console.WriteLine();
                Console.WriteLine("直接在控制台输出二维码：生成二维码的字符串文件夹路径或直接输入想打印的二维码内容");
                Console.WriteLine("              例：E:/GitRepo/myqrcode.txt或 C#真奇妙！");
            }
            //Console.WriteLine(args[0].Substring(0, 2)=="-f");
            else if (args[0].Substring(0, 2) == "-f")
            {
                GenQrCode(args[0].Substring(2));
                Console.WriteLine("生成完毕");
            }
            else
            {
                Console.WriteLine("请问您输入的是文件夹路径还是字符串？");
                Console.WriteLine("1：文件夹路径     2：字符串");
                string strReadFilePath;
                // 读取文件的源路径及其读取流
                strReadFilePath = Console.ReadLine();
                switch (strReadFilePath)
                {
                    case "1":
                        printQrEncoder(args[0]);
                        break;
                    case "2":
                        printQrEncoder2(args[0]);
                        break;
                }
                Console.WriteLine("生成完毕");
            }
            Console.ReadKey();
        }
    }
}

