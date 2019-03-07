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
