using System;
using System.Data;
using System.Data.OleDb;
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
            string[] strReadLine = new string[1000];
            while (!srReadFile.EndOfStream)
            {
                strReadLine[line] = srReadFile.ReadLine(); //读取每行数据
                line++;
                if(line > 999)
                {
                    Console.WriteLine("最多输入1000行数据，1000行后的数据将不作处理");
                    srReadFile.Close();
                    return strReadLine;
                }
                //Console.WriteLine(strReadLine); //屏幕打印每行数据
            }
            // 关闭读取流文件
            srReadFile.Close();
            return strReadLine;
        }

        //在控制台打印文本中的二维码
        public static void printQrEncoder(string args)
        {
            string[] SampleText = new string[1000];
            SampleText = Read(args);
            for (int i = 0; SampleText[i] != null; i++)
            {
                if (SampleText[i].Length <= 30 && SampleText[i].Length > 0)
                {
                    //设置二维码的纠错级别
                    QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.M);
                    //根据文本生成二维码
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
                else
                {
                    Console.WriteLine("输入字符的长度不能大于30位！");
                }
            }
        }

        //直接生成以命令行参数为内容的二维码并打印出来
        public static void printQrEncoder2(string args)
        {
            string SampleText = args;
            if (SampleText.Length <= 30 && SampleText.Length > 0)
            {
                //设置二维码的纠错级别
                QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.M);
                //根据文本生成二维码
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

        /// <summary>
        /// 将文件名的编号转换为三位数
        /// </summary>
        /// <param name="i"></param>
        /// <returns>最终编号</returns>
        public static string ThreeDigits(int i)
        {
            if (i > 0 && i < 10)
            {
                return "00" + i;
            }
            else if (i < 100)
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
            while(strReadFilePath == "")
            {
                Console.WriteLine("文件路径不能为空！！");
                Console.WriteLine("请重新输入：");
                strReadFilePath = Console.ReadLine();
            }
            return strReadFilePath;
        }

        public static void printCode(string[] SampleText)
        {
            Console.WriteLine("请输入要保存的文件路径：");
            String fileName2 = CreateFile();
            ////每一次执行前都把文件夹清空，但是不安全！！
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
                for (int i = 0; SampleText[i] != null; i++)
                {
                    //Console.WriteLine(i);
                    string Name = SampleText[i];
                    string fileName;
                    if (Name.Length <= 30 && Name.Length > 0)//限制条件，输入的字符串长度要小于30
                    {
                        fileName = fileName2 + ThreeDigits(i + 1) + Name.Substring(0, 4) + ".bmp";
                        var qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
                        var qrCode = qrEncoder.Encode(SampleText[i]);//生成二维码
                        GraphicsRenderer gRender = new GraphicsRenderer(new FixedModuleSize(30, QuietZoneModules.Four));
                        using (FileStream stream = new FileStream(fileName, FileMode.Create))
                        {
                            gRender.WriteToStream(qrCode.Matrix, ImageFormat.Bmp, stream, new Point(600, 600));//生成图片
                        }
                    }
                    else
                    {
                        Console.WriteLine("提示：输入字符的长度不能大于30位！！");
                    }
                }
            }
        }

        /// <summary>
        /// 生成二维码，并保存图片到指定路径下
        /// </summary>
        /// <param name="fileName">图片保存路径全名（包括路径和文件名）</param>
        /// <param name="content">要生成二维码的内容</param>
        public static void GenQrCode(string args)
        {
            string[] SampleText = new string[1000];
            SampleText = Read(args);
            printCode(SampleText);
        }

        //把EXCEL文件当做一个数据源来进行数据的读取操作
        public static DataSet ExcelToDS(string Path)
        {
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + Path + ";" + "Extended Properties=Excel 8.0;";
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            string strExcel = "";
            OleDbDataAdapter myCommand = null;
            DataSet ds = null;
            DataTable schemaTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            //表格数据存储在sheet1中
            strExcel = "select * from [sheet1$]";
            myCommand = new OleDbDataAdapter(strExcel, strConn);
            ds = new DataSet();
            myCommand.Fill(ds, "table1");
            return ds;
        }

        /// <summary>
        /// 生成二维码，并保存图片到指定路径下
        /// </summary>
        public static void ExcelQrCode(string strExcelPath)
        {
            DataSet dataSet = ExcelToDS(strExcelPath);
            string[] SampleText = new string[1000];
            int count = 0;
            //遍历一个表多行多列
            foreach (DataRow mDr in dataSet.Tables[0].Rows)
            {
                foreach (DataColumn mDc in dataSet.Tables[0].Columns)
                {
                    if (mDr[mDc].ToString() != "")
                    {
                        SampleText[count] = mDr[mDc].ToString();
                        count++;
                        //Console.WriteLine(mDr[mDc].ToString());
                    }

                }
            }
            printCode(SampleText);
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
                //不存在的文件，应该提示帮助信息，并正常终止。不应该异常终止。
                if (!File.Exists(args[0].Substring(2)))
                {
                    Console.WriteLine("提示：该文件不存在，请重新输入正确的文件路径！");                  
                }
                else
                {
                    Console.WriteLine("请问您打开的是txt文件还是excel表格？");
                    Console.WriteLine("1：txt文件     2：excel表格");
                    string strReadFilePath = Console.ReadLine();
                    if (strReadFilePath == "1")
                    {
                        GenQrCode(args[0].Substring(2));
                        Console.WriteLine("生成完毕");
                    }
                    else if (strReadFilePath == "2")
                    {
                        ExcelQrCode(args[0].Substring(2));
                        Console.WriteLine("生成完毕");
                    }
                    else
                    {
                        Console.WriteLine("请输入1或者2");
                    }
                }
            }
            else
            {
                if(args[0].Substring(2).Length <= 30 && args[0].Substring(2).Length > 0)
                {
                    Console.WriteLine("请问您输入的是txt文件夹路径还是字符串？");
                    Console.WriteLine("1：txt文件夹路径     2：字符串");
                    string strReadFilePath;
                    // 读取文件的源路径及其读取流
                    strReadFilePath = Console.ReadLine();
                    string extension = Path.GetExtension(args[0].Substring(2));
                    switch (strReadFilePath)
                    {
                        case "1":
                            if (!File.Exists(args[0].Substring(2)))
                            {
                                Console.WriteLine("提示：该文件不存在，请重新输入正确的文件路径！");
                                break;
                            }
                            else if (extension != ".txt")
                            {
                                Console.WriteLine("只能读取txt类型的文本文件");
                                break;
                            }
                            printQrEncoder(args[0]);
                            Console.WriteLine("生成完毕");
                            break;

                        case "2":
                            printQrEncoder2(args[0]);
                            Console.WriteLine("生成完毕");
                            break;
                        default:
                            Console.WriteLine("请输入1或者2");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("提示：输入字符的长度不能大于30位！");
                }
            }
            Console.ReadKey();
        }
    }
}