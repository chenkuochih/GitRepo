# GitRepo
WPF的Hello World！
-------
### WPF的xaml文件内容 <br />
                <Window x:Class="WpfHelloWorld.MainWindow"
                        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                 xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
                        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
                 xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
                 xmlns:local="clr-namespace:WpfHelloWorld"
                   mc:Ignorable="d"
                 Title="MainWindow" Height="450" Width="800">
                <Grid>
                 <TextBlock Margin="103,64,107,0" Name="textBlock1" Text="Hello, World!!" FontSize="24" TextDecorations="None" VerticalAlignment="Top" Visibility="Hidden" TextAlignment="Center"/>
                 <Button  Height="37" Margin="103,0,118,47" Name="button1" VerticalAlignment="Bottom" Click="button1_Click">Say Hello</Button>
                 </Grid>
                </Window>
### WPF的cs文件内容 <br/>
                public partial class MainWindow : Window
                  {
                   public MainWindow()
                   {
                     InitializeComponent();
                    }
                   private void button1_Click(object sender, RoutedEventArgs e)
                  {
                           textBlock1.Visibility = Visibility.Visible;
                  }
                  }
                }
### 代码执行结果 <br />
![HelloWorld!](https://github.com/chenkuochih/GitRepo/blob/master/WPF的HelloWorld%EF%BC%81运行结果%EF%BC%881%EF%BC%89.png)
![HelloWorld!](https://github.com/chenkuochih/GitRepo/blob/master/WPF的HelloWorld%EF%BC%81运行结果%EF%BC%882%EF%BC%89.png)
![命令行参数](https://github.com/chenkuochih/GitRepo/blob/master/WPF的命令行输入.png)
