# 第一次实验
## 实验需求 <br />
     在命令行里传递一个文件名，该文件是文本文件，文件中每行有一条字符串信息，用于生成QrCode.并保存到.png或bmp文件中，保存的文件名以信息所在行号三位数+信息的前四个字符构成。命令行可以是myqrcode -fqrcode.txt。 -f表示qrcode信息在后面的qrcode.txt文件中。如果没有-f则以现在的方式在控制台输出qrcode.
## 完成度 <br />
    （1）根据-f的有无以及参数的有无，分别有不同的输出情况。无参数：输出用户输入提示。有-f：根据-f后的txt文件生成二维码图片，并根据输入的文件目录保存到本地。没有-f：在控制台打印二维码。
    （2）改进：没有-f时有两种情况。一：根据传入的文件路径读取传入的文件的内容，并将文件内容以二维码的方式输出。二：直接打印传入的命令行参数。具体使用哪种情况视用户的选择而定。
    （3）用户输入字符串长度大于30时会给予提示并安全退出。
    （4）用户输入错误路径时会给予提示并安全退出。
    
## 代码分析 <br />
Read(string args)的作用是从命令行读取参数，以便于后面判断是否有-f
-------------
     public static string[] Read(string args)
        {
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

printQrEncoder(string args)的作用是在控制台打印文本中的二维码
------------
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
                else
                {
                    Console.WriteLine("输入字符的长度不能大于30位！");
                }
            }
        }
printQrEncoder2(string args)的作用是直接生成以命令行参数为内容的二维码并打印出来
----------------
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
GenQrCode(string args)的作用是生成二维码，并保存图片到指定路径下
----------------
        <param name="fileName">图片保存路径全名（包括路径和文件名）</param>
        <param name="content">要生成二维码的内容</param>
        public static void GenQrCode(string args)
        {
            //Console.WriteLine(args);
            string[] SampleText = new string[1000];
            SampleText = Read(args);
            Console.WriteLine("请输入要保存的文件路径：");
            String fileName2 = CreateFile();
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
<br /> 

执行结果截图
------------
### 有-f的代码截图

![有-f的代码截图](https://github.com/chenkuochih/GitRepo/blob/master/%E8%BF%90%E8%A1%8C%E7%BB%93%E6%9E%9C%E6%88%AA%E5%9B%BE/%E3%80%90%E5%AE%9E%E9%AA%8C%E4%B8%80%E3%80%91%E6%9C%89-f%E7%9A%84%E4%BB%A3%E7%A0%81%E7%BB%93%E6%9E%9C%EF%BC%881%EF%BC%89.png)
![有-f的代码截图](https://github.com/chenkuochih/GitRepo/blob/master/%E8%BF%90%E8%A1%8C%E7%BB%93%E6%9E%9C%E6%88%AA%E5%9B%BE/%E3%80%90%E5%AE%9E%E9%AA%8C%E4%B8%80%E3%80%91%E6%9C%89-f%E7%9A%84%E4%BB%A3%E7%A0%81%E7%BB%93%E6%9E%9C%EF%BC%882%EF%BC%89.png)

### 无-f的代码截图
![无-f的代码截图](https://github.com/chenkuochih/GitRepo/blob/master/%E8%BF%90%E8%A1%8C%E7%BB%93%E6%9E%9C%E6%88%AA%E5%9B%BE/%E3%80%90%E5%AE%9E%E9%AA%8C%E4%B8%80%E3%80%91%E6%97%A0-f%E7%9A%84%E4%BB%A3%E7%A0%81%E7%BB%93%E6%9E%9C%EF%BC%881%EF%BC%89.png)
![无-f的代码截图](https://github.com/chenkuochih/GitRepo/blob/master/%E8%BF%90%E8%A1%8C%E7%BB%93%E6%9E%9C%E6%88%AA%E5%9B%BE/%E3%80%90%E5%AE%9E%E9%AA%8C%E4%B8%80%E3%80%91%E6%97%A0-f%E7%9A%84%E4%BB%A3%E7%A0%81%E7%BB%93%E6%9E%9C%EF%BC%882%EF%BC%89.png)

### 无参数的代码截图
![无参数的代码截图](https://github.com/chenkuochih/GitRepo/blob/master/%E8%BF%90%E8%A1%8C%E7%BB%93%E6%9E%9C%E6%88%AA%E5%9B%BE/%E3%80%90%E5%AE%9E%E9%AA%8C%E4%B8%80%E3%80%91%E6%97%A0%E5%8F%82%E6%95%B0%E7%9A%84%E4%BB%A3%E7%A0%81%E7%BB%93%E6%9E%9C.png)


### 输入错误路径后的结果截图
![输入错误路径后的结果截图](https://github.com/chenkuochih/GitRepo/blob/master/%E8%BF%90%E8%A1%8C%E7%BB%93%E6%9E%9C%E6%88%AA%E5%9B%BE/%E3%80%90%E5%AE%9E%E9%AA%8C%E4%B8%80%E3%80%91%E8%BE%93%E5%85%A5%E9%94%99%E8%AF%AF%E8%B7%AF%E5%BE%84%E5%90%8E%E7%9A%84%E7%BB%93%E6%9E%9C.png)

### 输入长度大于三十的字符串的结果截图
![输入长度大于三十的字符串的结果截图](https://github.com/chenkuochih/GitRepo/blob/master/%E8%BF%90%E8%A1%8C%E7%BB%93%E6%9E%9C%E6%88%AA%E5%9B%BE/%E3%80%90%E5%AE%9E%E9%AA%8C%E4%B8%80%E3%80%91%E8%BE%93%E5%85%A5%E9%95%BF%E5%BA%A6%E5%A4%A7%E4%BA%8E%E4%B8%89%E5%8D%81%E7%9A%84%E5%AD%97%E7%AC%A6%E4%B8%B2%E7%9A%84%E7%BB%93%E6%9E%9C.png)

### 输入长度等于三十的字符的结果截图
![输入长度等于三十的字符的结果截图](https://github.com/chenkuochih/GitRepo/blob/master/%E8%BF%90%E8%A1%8C%E7%BB%93%E6%9E%9C%E6%88%AA%E5%9B%BE/%E3%80%90%E5%AE%9E%E9%AA%8C%E4%B8%80%E3%80%91%E8%BE%93%E5%85%A5%E9%95%BF%E5%BA%A6%E7%AD%89%E4%BA%8E%E4%B8%89%E5%8D%81%E7%9A%84%E5%AD%97%E7%AC%A6%E7%9A%84%E7%BB%93%E6%9E%9C.png)


<br />

# 第一次作业
### 思路 <br />
     添加一行Console.ResetColor();将所有设置的样式清除
### 代码如下 <br />
     static void Main(string[] args)
        {
            System.Console.WriteLine("Hello World! {0} is {1}",args[0],args[1]);
            Console.BackgroundColor = ConsoleColor.Green;
            System.Console.WriteLine("Hello World!");
            System.Console.WriteLine("Hello World!");
            Console.ForegroundColor = ConsoleColor.Yellow;
            System.Console.WriteLine("Hello World!");
            Console.ResetColor();
            System.Console.WriteLine("Hello World!");
            Console.ReadKey();
        }

<br />

# 第二次作业
1.无实例化的Hello World！
-------
### 思路：
     用public修饰的static成员变量和成员方法本质是全局变量和全局方法。
### 代码如下 <br />
     class HelloWorld
     {
            public static void Hello()
           {
             Console.WriteLine("Hello World!");
           }
       }
      class Program
      {
          static void Main(string[] args)
          {
                 HelloWorld.Hello();
           }
    }
<br />

2.实例化的Hello World！
-------
### 思路：
     new一个对象，通过对象调用方法输出Hello World！
### 代码如下 <br />
     class HelloWorld
    {
        public void Hello()
        {
            Console.WriteLine("Hello World!");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            HelloWorld v = new HelloWorld();
            v.Hello();
        }
    }
<br />

3.WPF的Hello World！
-------
### WPF的xaml文件内容 <br />
     <Window x:Class="WpfApp1.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:WpfApp1"
        mc:Ignorable="d"
        Title="MainWindow" Height="450" Width="800">
    <Grid>
        <Button Content="Say Hello" HorizontalAlignment="Center" Margin="345,288,357,93" VerticalAlignment="Center" Width="90" Click="Button_Click" 
                HorizontalContentAlignment="Center" VerticalContentAlignment="Center" Height="38" FontSize="16" FontWeight="Bold" Background="#FFBF88F7"/>
        <TextBlock HorizontalAlignment="Center" VerticalAlignment="Center"
                   Margin="284,93,298.6,248" Height="79" Width="211" RenderTransformOrigin="0.5,0.5"
                   Text="Hello World!" TextWrapping="Wrap" TextAlignment="Center" FontSize="34"
                   Name="textBlock1" FontWeight="Bold" Foreground="BlueViolet" FontFamily="./#仿宋_GB2312" Visibility="Hidden"/>
        <TextBlock HorizontalAlignment="Center" VerticalAlignment="Center" Margin="0,0,0,0" 
                   TextWrapping="Wrap" Text="命令行参数为：" TextAlignment="Center" FontSize="22"
                   Height="30" Width="772" Name="textBlock2" Foreground="BlueViolet" FontWeight="Bold" FontFamily="./#仿宋_GB2312"/>
    </Grid>
    </Window>
### WPF的cs文件内容 <br/>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Text();
        }

        private void Text()
        {
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length == 1)
            {
                textBlock2.Visibility = Visibility.Hidden;
            }
            else
            {
                string s = textBlock2.Text;
                for (int i = 1; i < args.Length; i++)
                {
                    s += args[i] + " ";
                }
                textBlock2.Text = s;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            textBlock1.Visibility = Visibility.Visible;
        }
    }

### 代码执行结果 <br />
![命令行参数](https://github.com/chenkuochih/GitRepo/blob/master/WPF的命令行输入.png)
![HelloWorld!](https://github.com/chenkuochih/GitRepo/blob/master/WPF的HelloWorld%EF%BC%81运行结果%EF%BC%881%EF%BC%89.png)
![HelloWorld!](https://github.com/chenkuochih/GitRepo/blob/master/WPF的HelloWorld%EF%BC%81运行结果%EF%BC%882%EF%BC%89.png)
<br />

4.WindowsForm中创建新类输出Hello World！
------
### 思路：
     使用工具栏添加了一个textbox和两个button，并更改其属性。创造两个类，一个类输出中文：你好，世界。另一个输出英文Hello World!。再在两个button的代码域内调用。最后效果：按下Chinese输出中文：你好，世界。按下English输出Hello World!
### 代码如下 <br />
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
![代码结果](https://github.com/chenkuochih/GitRepo/blob/master/WindowsForm中创建新类输出Hello%20World%EF%BC%81(1).png)
![中文：你好，世界！](https://github.com/chenkuochih/GitRepo/blob/master/WindowsForm中创建新类输出Hello%20World%EF%BC%81(2).png)
![英文：Hello World！](https://github.com/chenkuochih/GitRepo/blob/master/WindowsForm中创建新类输出Hello%20World%EF%BC%81(3).png)
<br />


5.Xamarin.form输出Hello World！
------
![最后一个HelloWorld](https://github.com/chenkuochih/GitRepo/blob/master/运行结果截图/作业二的第五个要求代码截图.png)

     这个代码！！！因为那个安卓机一直出不来所以无法看到代码效果，拖了好几天问老师才解决。
     本来想加一个点击按钮出弹窗，但是由于找了很久的资料也没看明白，所以就变成一个调皮的弹窗了。
