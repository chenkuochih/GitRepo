# 第三次实验
## 功能概述 <br />
    1) 使用WPF Midi Band提供的源程序，在Visual Studio中建立相应的解决方案。
    2) 能够成功编译WPF Midi Band提供的演示程序。并能正常播放MIDI文件。
    3) 理解演示程序的内部工作机制: 参照WPF Midi Band文章内容，理解以Event/Delegate方式实现的模块间的耦合机制，各种类的继承关系等。
    4) 对GUI界面中的控件大小、位置进行完善，使之能够随APP界面大小自动调整其自身大小。需要使用相应的Event完成此项工作。
    5) 实现其他GUI界面的用户体验提升。
## 项目特色 <br />
    1) 对GUI界面控件大小位置进行了完善
    2) 调整控件的位置，使控件在随窗口大小改变时更加美观。
    3) 改变了鼠标移动到Button上的样式
    4) 改变了控件字体及窗口背景
    
## 代码总量：160行 <br />
## 工作时间：3天 <br />
## 知识点总结图 <br />
![知识点总结](https://github.com/chenkuochih/GitRepo/blob/master/%E8%BF%90%E8%A1%8C%E7%BB%93%E6%9E%9C%E6%88%AA%E5%9B%BE/%E3%80%90%E5%AE%9E%E9%AA%8C%E4%B8%89%E3%80%91%E7%9F%A5%E8%AF%86%E7%82%B9%E6%80%BB%E7%BB%93.png)
## 结论 <br />
实验过程：
-------------
对GUI界面中的控件大小、位置进行完善，使之能够随APP界面大小自动调整其自身大小。需要使用相应的Event完成此项工作。
-------------
    <Viewbox  Name="Viewbox1" Stretch="Fill" >
        <Canvas Height="800" Name="Canvas1" Width="700" Background="Transparent" >
        ......
        </Canvas>
    </Viewbox>
实现对GUI界面的用户体验提升。
-------------
    （1）调整控件的位置，使控件在随窗口大小改变时更加美观。
        <ctrl:PianoControlWPF x:Name="pianoControl1" Margin="0,10,-21.2,0" HorizontalAlignment="Left" Height="50" Width="518">
        <ctrl:PianoControlWPF.RenderTransform>
        <ScaleTransform ScaleX="1.18" ScaleY="1.18"/>
        </ctrl:PianoControlWPF.RenderTransform>
        </ctrl:PianoControlWPF>
        <ctrl:GuitarControl Height="104" x:Name="guitarControl1" Margin="59,0,-62.2,0"/>
        <ctrl:BassControl Height="105" x:Name="bassControl1" Margin="59,0,-62.2,0"/>
        <ctrl:DrumControl x:Name="drumControl1" HorizontalAlignment="Left" VerticalAlignment="Center">
         <ctrl:DrumControl.RenderTransform>
              <TransformGroup>
                   <ScaleTransform ScaleX="1.0" ScaleY="1.0"/>
                   <TranslateTransform X="-0" Y="-30"/>
              </TransformGroup>
          </ctrl:DrumControl.RenderTransform>
        </ctrl:DrumControl>
        
    （2）改变了控件字体及窗口背景
        <LinearGradientBrush>
            <GradientStop Offset="0.0" Color="Blue"/>
            <GradientStop Offset="1.0" Color="Pink"/>
        </LinearGradientBrush>
        
    （3）改变了鼠标移动到Button上的样式
        <Style x:Key="btn" TargetType="Button">
            <Setter Property="Margin" Value="5,5,5,5"></Setter>
            <Setter Property="Template">
                <Setter.Value>
                    <ControlTemplate TargetType="Button">
                        <Grid>
                            <Border BorderThickness="1" Margin="10 0 10 0" VerticalAlignment="Center" HorizontalAlignment="Center" Width="100" Height="30" CornerRadius="5,5,5,5" Background="#3E98D7">
                                <TextBlock Grid.Column="1" Text="{TemplateBinding Content}" VerticalAlignment="Center" HorizontalAlignment="Center"  Foreground="White"></TextBlock>
                            </Border>
                        </Grid>
                        <ControlTemplate.Triggers>
                            <Trigger Property="Button.IsMouseOver"  Value="True">
                                <Setter Property="Opacity" Value="1" />
                                <Setter Property="FontSize" Value="13"></Setter>
                                <Setter Property="FontWeight" Value="Bold"></Setter>
                            </Trigger>
                            <Trigger Property="Button.IsEnabled" Value="False">
                                <Setter Property="Opacity" Value="0.5" />
                            </Trigger>
                            <Trigger Property="Button.IsEnabled" Value="True">
                                <Setter Property="Opacity" Value="1" />
                            </Trigger>
                        </ControlTemplate.Triggers>
                    </ControlTemplate>
                </Setter.Value>
            </Setter>
        </Style>
          
实验结果：
------------
对GUI界面控件大小位置进行完善
-------------
![控件大小](https://github.com/chenkuochih/GitRepo/blob/master/%E8%BF%90%E8%A1%8C%E7%BB%93%E6%9E%9C%E6%88%AA%E5%9B%BE/%E3%80%90%E5%AE%9E%E9%AA%8C%E4%B8%89%E3%80%91%E6%8E%A7%E4%BB%B6%E4%BD%8D%E7%BD%AE.png)
改变了控件字体及窗口背景
-------------
![窗口背景色](https://github.com/chenkuochih/GitRepo/blob/master/%E8%BF%90%E8%A1%8C%E7%BB%93%E6%9E%9C%E6%88%AA%E5%9B%BE/%E3%80%90%E5%AE%9E%E9%AA%8C%E4%B8%89%E3%80%91%E8%83%8C%E6%99%AF%E9%A2%9C%E8%89%B2.png)
改变鼠标移动到Button上的样式
-------------
![改变样式](https://github.com/chenkuochih/GitRepo/blob/master/%E8%BF%90%E8%A1%8C%E7%BB%93%E6%9E%9C%E6%88%AA%E5%9B%BE/%E3%80%90%E5%AE%9E%E9%AA%8C%E4%B8%89%E3%80%91%E9%BC%A0%E6%A0%87%E7%A7%BB%E5%8A%A8%E5%89%8D.png)
![改变样式](https://github.com/chenkuochih/GitRepo/blob/master/%E8%BF%90%E8%A1%8C%E7%BB%93%E6%9E%9C%E6%88%AA%E5%9B%BE/%E3%80%90%E5%AE%9E%E9%AA%8C%E4%B8%89%E3%80%91%E9%BC%A0%E6%A0%87%E7%A7%BB%E5%8A%A8%E5%90%8E.png)



# 第二次实验
## 功能概述 <br />
    1) 使用C# MIDI Toolkit提供的源程序，在Visual Studio中建立相应的解决方案。
    2) 能够成功编译C# MIDI Toolkit提供的演示程序。并能正常播放MIDI文件。
    3) 理解演示程序的内部工作机制: 参照C# MIDI Toolkit文章内容，理解Event/Delegate方式实现的模块间的耦合机制，各种类的继承关系等。
    4) 对GUI界面中的控件大小、位置进行完善，使之能够随APP界面大小自动调整其自身大小。需要使用相应的Event完成此项工作。
    5) 实现对GUI界面的用户体验提升。

## 项目特色 <br />
    1) 对GUI界面控件大小位置进行了完善
    2) 添加退出按钮
    3) 添加循环播放功能
    4) 创建播放列表
    5) 改变了控件字体及窗口背景
    
## 代码总量：484行 <br />
## 工作时间：3天 <br />
## 知识点总结图 <br />
![知识点总结](https://github.com/chenkuochih/GitRepo/blob/master/%E8%BF%90%E8%A1%8C%E7%BB%93%E6%9E%9C%E6%88%AA%E5%9B%BE/%E3%80%90%E5%AE%9E%E9%AA%8C%E4%BA%8C%E3%80%91%E7%9F%A5%E8%AF%86%E7%82%B9%E6%80%BB%E7%BB%93.png)
## 结论 <br />
实验过程：
-------------
对GUI界面中的控件大小、位置进行完善，使之能够随APP界面大小自动调整其自身大小。需要使用相应的Event完成此项工作。
-------------
    （1）将控件的宽，高，左边距，顶边距和字体大小暂存到tag属性中
        private void setTag(Control cons)
        {
            foreach (Control con in cons.Controls)
            {
                con.Tag = con.Width + ";" + con.Height + ";" + con.Left + ";" + con.Top + ";" + con.Font.Size;
                if (con.Controls.Count > 0)
                {
                    setTag(con);
                }
            }
        }
    （2）遍历窗体中的控件，重新设置控件的值
        private void setTag(Control cons)
        {
            foreach (Control con in cons.Controls)
            {
                con.Tag = con.Width + ";" + con.Height + ";" + con.Left + ";" + con.Top + ";" + con.Font.Size;
                if (con.Controls.Count > 0)
                {
                    setTag(con);
                }
            }
        }
    （3）随窗体高度和宽度缩放比例确定控件大小
        private void setTag(Control cons)
        {
            foreach (Control con in cons.Controls)
            {
                con.Tag = con.Width + ";" + con.Height + ";" + con.Left + ";" + con.Top + ";" + con.Font.Size;
                if (con.Controls.Count > 0)
                {
                    setTag(con);
                }
            }
        }
实现对GUI界面的用户体验提升。
-------------
    （1）添加退出按钮
        private void quit_button_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(this, "Are you sure you want to quit the program? ", "Closing prompt ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                //this.Close();
                System.Environment.Exit(0);//这是最彻底的退出方式，不管什么线程都被强制退出，把程序结束的很干净。
            }
        }
    （2）添加循环播放功能
        private void loopPlay_Click(object sender, EventArgs e)
        {
            if(AutoLoop == true)
            {
                AutoLoop = false;
                loop_button.Text = "Listplay";
            }
            else
            {
                AutoLoop = true;
                loop_button.Text = "loopPlay";
            }
            
        }
        
        private void HandleLoadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (AutoLoop)
            {
                AutoStart = true;
            }

            if (AutoStart)
            {
                startButton_Click(sender, e);
            }
        }
        
        private void loopPlay_Click(object sender, EventArgs e)
        {
            if(AutoLoop == true)
            {
                AutoLoop = false;
                loop_button.Text = "Listplay";
            }
            else
            {
                AutoLoop = true;
                loop_button.Text = "loopPlay";
            }
        }
    （3）创建播放列表，同时判断列表里的歌曲是否有重复
        private List<musicDatabase> musicList = new List<musicDatabase>();
        
        public class musicDatabase
        {
            public string musicName;
            public string musicPath;
        }
        
        public void Open(string fileName)
        {
            lastFileName = fileName;
            try
            {
                sequencer1.Stop();
                playing = false;
                sequence1.LoadAsync(fileName);
                this.Cursor = Cursors.WaitCursor;
                startButton.Enabled = false;
                continueButton.Enabled = false;
                stopButton.Enabled = false;
                openToolStripMenuItem.Enabled = false;
                for (int i = 0; i < lstNativeList.Items.Count; i++)
                {
                    if (lstNativeList.Items[i].ToString().Contains(Path.GetFileNameWithoutExtension(fileName)))
                    {
                        isRepeated = true;
                        MessageBox.Show("添加失败，该歌曲已在列表中");
                    }
                }
                if (isRepeated == false)
                {
                    lstNativeList.Items.Add((lstNativeList.Items.Count + 1).ToString() + " " + Path.GetFileNameWithoutExtension(fileName));
                    // 把信息放进数据库
                    musicDatabase md = new musicDatabase();
                    md.musicName = Path.GetFileNameWithoutExtension(fileName);
                    md.musicPath = fileName;
                    musicList.Add(md);
                    MessageBox.Show("添加成功！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        
        private void lstNativeList_DoubleClick(object sender, EventArgs e)
        {
            string musicName = lstNativeList.Items[lstNativeList.SelectedIndex].ToString();
            musicName = musicName.Substring(musicName.IndexOf(" ") + 1);
            //MessageBox.Show(musicName);
            string musicPath = "";
            for(int i = 0; i < musicList.Count; i++)
            {
                if(musicList[i].musicName == musicName)
                {
                    musicPath = musicList[i].musicPath;
                }
            }
            try
            {
                sequencer1.Stop();
                playing = false;
                sequence1.LoadAsync(musicPath);
                this.Cursor = Cursors.WaitCursor;
                startButton.Enabled = false;
                continueButton.Enabled = false;
                stopButton.Enabled = false;
                openToolStripMenuItem.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            try
            {
                playing = true;
                sequencer1.Start();
                timer1.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
    }
          
实验结果：
------------
对GUI界面控件大小位置进行完善
-------------
![控件大小](https://github.com/chenkuochih/GitRepo/blob/master/%E8%BF%90%E8%A1%8C%E7%BB%93%E6%9E%9C%E6%88%AA%E5%9B%BE/%E3%80%90%E5%AE%9E%E9%AA%8C%E4%BA%8C%E3%80%91%E6%8E%A7%E4%BB%B6%E5%A4%A7%E5%B0%8F.png)
添加退出按钮
-------------
![退出按钮](https://github.com/chenkuochih/GitRepo/blob/master/%E8%BF%90%E8%A1%8C%E7%BB%93%E6%9E%9C%E6%88%AA%E5%9B%BE/%E3%80%90%E5%AE%9E%E9%AA%8C%E4%BA%8C%E3%80%91%E9%80%80%E5%87%BA.png)
添加循环播放功能
-------------
![循环播放](https://github.com/chenkuochih/GitRepo/blob/master/%E8%BF%90%E8%A1%8C%E7%BB%93%E6%9E%9C%E6%88%AA%E5%9B%BE/%E3%80%90%E5%AE%9E%E9%AA%8C%E4%BA%8C%E3%80%91%E5%8D%95%E6%9B%B2%E5%BE%AA%E7%8E%AF.png)
创建播放列表
-------------
![播放列表](https://github.com/chenkuochih/GitRepo/blob/master/%E8%BF%90%E8%A1%8C%E7%BB%93%E6%9E%9C%E6%88%AA%E5%9B%BE/%E3%80%90%E5%AE%9E%E9%AA%8C%E4%BA%8C%E3%80%91%E5%88%97%E8%A1%A8.png)
![添加失败](https://github.com/chenkuochih/GitRepo/blob/master/%E8%BF%90%E8%A1%8C%E7%BB%93%E6%9E%9C%E6%88%AA%E5%9B%BE/%E3%80%90%E5%AE%9E%E9%AA%8C%E4%BA%8C%E3%80%91%E6%B7%BB%E5%8A%A0%E6%AD%8C%E6%9B%B2%E5%A4%B1%E8%B4%A5.png)



# 第四次作业——编写一个简单的Web Api程序
## 功能概述 <br />
1——当HomeController的View（“”）里的内容为Products时默认跳转到Products页面，执行Products页面的操作
------------
    (1)添加数据
    添加一条记录的请求类型:POST  请求url:  /api/Products
    请求到ProductsController.cs中的 public HttpResponseMessage PostProduct(Product item) 方法。
    在框内输入Name，Category，Price，点击添加。会有弹窗提示添加成功或者失败。此外，每次添加成功的数据ID默认为上一个添加成功的数据ID+1
    (2)查询数据
    先根据Id查询记录的请求类型:GET  请求url:  /api/Products/Id
    请求到ProductsController.cs中的public Product GetProduct(int id) 方法
    查询失败时会有提示
    (3)修改数据
    修改该Id的记录的请求类型:PUT  请求url:  /api/Products/Id
    请求到ProductsController.cs中的public void PutProduct(int id, Product product) 方法
    (4)删除数据
    删除输入Id的记录的请求类型:DELETE  请求url:  /api/Products/Id
    请求到ProductsController.cs中的public void DeleteProduct(int id) 方法
    (5)清空数据
    将当前页面框中数据清除。
   
#### 添加数据
![添加成功](https://github.com/chenkuochih/GitRepo/blob/master/%E8%BF%90%E8%A1%8C%E7%BB%93%E6%9E%9C%E6%88%AA%E5%9B%BE/%E6%B7%BB%E5%8A%A0%E8%AE%B0%E5%BD%95.png)
#### 添加成功
![添加成功](https://github.com/chenkuochih/GitRepo/blob/master/%E8%BF%90%E8%A1%8C%E7%BB%93%E6%9E%9C%E6%88%AA%E5%9B%BE/%E6%B7%BB%E5%8A%A0%E6%88%90%E5%8A%9F.png)

#### 查询数据
![查询数据](https://github.com/chenkuochih/GitRepo/blob/master/%E8%BF%90%E8%A1%8C%E7%BB%93%E6%9E%9C%E6%88%AA%E5%9B%BE/%E6%9F%A5%E8%AF%A2%E5%A4%B1%E8%B4%A5.png)   
   
#### 修改数据
 ![修改数据](https://github.com/chenkuochih/GitRepo/blob/master/%E8%BF%90%E8%A1%8C%E7%BB%93%E6%9E%9C%E6%88%AA%E5%9B%BE/%E4%BF%AE%E6%94%B9%E6%88%90%E5%8A%9F.png)
 


2——当HomeController的View（“”）里的内容为Products时默认跳转到Products页面，执行Products页面的操作
------------
#### (1)查询全部信息
   ![查询全部信息](https://github.com/chenkuochih/GitRepo/blob/master/%E8%BF%90%E8%A1%8C%E7%BB%93%E6%9E%9C%E6%88%AA%E5%9B%BE/%E6%9F%A5%E8%AF%A2%E6%89%80%E6%9C%89.png)
   
#### (2)根据ID查询
  ![根据ID查询](https://github.com/chenkuochih/GitRepo/blob/master/%E8%BF%90%E8%A1%8C%E7%BB%93%E6%9E%9C%E6%88%AA%E5%9B%BE/%E6%A0%B9%E6%8D%AEID%E6%9F%A5%E8%AF%A2.png)
  
#### (3)根据性别查询
   ![根据性别查询](https://github.com/chenkuochih/GitRepo/blob/master/%E8%BF%90%E8%A1%8C%E7%BB%93%E6%9E%9C%E6%88%AA%E5%9B%BE/%E6%A0%B9%E6%8D%AE%E5%A7%93%E5%90%8D%E6%9F%A5%E8%AF%A2.png)


# 第一次实验
## 功能概述 <br />
     （1） 当用户在命令行输入参数时，判断参数是否是QrCode可以生成的信息，长度是否符合本程序的基本要求（自定义长度为30），如果符合要求就直接产生QrCode的结果，将QrCode的编码矩阵在控制台屏幕上输出对应黑、白字符方块组成的QrCode码。
     （2） 如果不符合要求，提示用户输入正确的命令行信息，运行结束。
     （3）将程序的代码加上了注释，并说明其功能。
     （4）用手机识别可以还原数据。
     （5）在命令行里传递一个文件名(包括文件所在目录，可以是可执行文件所在目录的相对目录例如data\qrcode.txt)，该文件是文本文件，文件中每行有一条字符串信息。假定myqrcode为本程序编译后的可执行文件名，-f表示QrCode信息放在-f后的data\qrcode.txt文件中。在控制台界面输入如下命令，可以将qrcode.txt中的每一行信息生成一个QrCode。
     （6）生成的QrCode保存到.png中，保存的文件名以信息所在行号三位数+信息的前四个字符构成。如果没有-f则以上述a的方式在控制台输出QrCode。
     （7）读取excel表格中的数据，并将其打印到指定文件夹
     
## 项目特色 <br />
    （1）根据-f的有无以及参数的有无，分别有不同的输出情况。无参数：输出用户输入提示。有-f：根据-f后的txt文件/excel文件生成二维码图片。没有-f：在控制台打印二维码。
    （2）用户可以根据输入的文件目录将二维码保存到本地。
    （3）没有-f时有两种情况。一：根据传入的文件路径读取传入的文件的内容，并将文件内容以二维码的方式输出。二：直接打印传入的命令行参数。具体使用哪种情况视用户的选择而定。
    （4）用户输入字符串长度大于30时会给予提示并安全退出。
    （5）用户输入错误路径时会给予提示并安全退出。

## 代码总量：300行左右 <br />
## 工作时间：4天 <br />
## 知识点总结图 <br />
![知识点总结](https://github.com/chenkuochih/GitRepo/blob/master/%E8%BF%90%E8%A1%8C%E7%BB%93%E6%9E%9C%E6%88%AA%E5%9B%BE/%E7%9F%A5%E8%AF%86%E7%82%B9%E6%80%BB%E7%BB%93.png)
## 结论 <br />
实验过程：
-------------
使用Read(string args)函数，作用是从命令行读取参数，以便于后面判断是否有-f
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
                if(line > 999)
                {
                    Console.WriteLine("最多输入1000行数据，1000行后的数据将不作处理");
                    srReadFile.Close();
                    return strReadLine;
                }
            }
            // 关闭读取流文件
            srReadFile.Close();
            return strReadLine;
        }

使用printQrEncoder(string args)函数，作用是在控制台打印文本中的二维码
------------
     public static void printQrEncoder(string args)
        {
            string[] SampleText = new string[100];
            SampleText = Read(args);
            for (int i = 0; SampleText[i] != null; i++)
            {
                if (SampleText[i].Length < 30 && SampleText[i].Length > 0)
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
使用printQrEncoder2(string args)函数，作用是直接生成以命令行参数为内容的二维码并打印出来
----------------
     public static void printQrEncoder2(string args)
        {
            string SampleText = args;
            if (SampleText.Length < 30 && SampleText.Length > 0)
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
使用GenQrCode(string args)函数，作用是生成二维码，并保存图片到指定路径下
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
                    if (Name.Length <= 30 && Name.Length > 0)//限制条件，输入的字符串长度要小于30
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
                    else
                    {
                        Console.WriteLine("提示：输入字符的长度不能大于30位！！");
                    }
                }
            }   
        }
<br /> 

使用ExcelToDS(string Path)函数，作用是把EXCEL文件当做一个数据源来进行数据的读取操作
----------------
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

实验结果：
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

### 输入空路径的结果
![输入空路径的结果](https://github.com/chenkuochih/GitRepo/blob/master/%E8%BF%90%E8%A1%8C%E7%BB%93%E6%9E%9C%E6%88%AA%E5%9B%BE/%E3%80%90%E5%AE%9E%E9%AA%8C%E4%B8%80%E3%80%91%E8%BE%93%E5%85%A5%E7%A9%BA%E8%B7%AF%E5%BE%84%E7%9A%84%E7%BB%93%E6%9E%9C.png)

### 在控制台打印参数输入非txt文件的结果
![输入空路径的结果](https://github.com/chenkuochih/GitRepo/blob/master/%E8%BF%90%E8%A1%8C%E7%BB%93%E6%9E%9C%E6%88%AA%E5%9B%BE/%E3%80%90%E5%AE%9E%E9%AA%8C%E4%B8%80%E3%80%91%E5%9C%A8%E6%8E%A7%E5%88%B6%E5%8F%B0%E6%89%93%E5%8D%B0%E5%8F%82%E6%95%B0%E5%8F%AA%E8%83%BD%E8%AF%BB%E5%8F%96txt%E6%96%87%E4%BB%B6.png)



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
