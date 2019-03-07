using System;
using Gma.QrCodeNet.Encoding;

namespace 实验一_控制台输出二维码
{
    class Program
    {
        static void Main(string[] args)
        {
                string SampleText;
                do
                {
                    //Console.WriteLine("请输入长度大于30的字符");//提示输入信息
                    SampleText = Console.ReadLine();
                } while (SampleText.Length > 30);

                QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.M);
                QrCode qrCode = qrEncoder.Encode(SampleText);
                for (int j = 0; j < qrCode.Matrix.Width; j++)
                {
                    for (int i = 0; i < qrCode.Matrix.Width; i++)
                    {
                        char charToPrint = qrCode.Matrix[i, j] ? '□' : '■';
                        Console.Write(charToPrint);
                    }
                    Console.WriteLine();
                }
                Console.ReadKey();
            }
    }
}
