# GitRepo
WPF的Hello World！
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
![HelloWorld!](https://github.com/chenkuochih/GitRepo/blob/master/WPF的HelloWorld%EF%BC%81运行结果%EF%BC%881%EF%BC%89.png)
![HelloWorld!](https://github.com/chenkuochih/GitRepo/blob/master/WPF的HelloWorld%EF%BC%81运行结果%EF%BC%882%EF%BC%89.png)
![命令行参数](https://github.com/chenkuochih/GitRepo/blob/master/WPF的命令行输入.png)
