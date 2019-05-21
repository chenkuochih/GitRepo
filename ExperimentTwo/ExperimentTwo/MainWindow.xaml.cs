using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using WpfSerialPort;
using ZedGraph;

namespace ExperimentTwo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        //portsArray数组获得所有串口列表  
        string[] portsArray = SerialPort.GetPortNames();
        SerialPort serialPort = new SerialPort();
        SerialPort spReceive = new SerialPort();  //spReceive接受数据

        public delegate void Displaydelegate(byte[] InputBuf);
        Byte[] OutputBuf = new Byte[128];
        public Displaydelegate disp_delegate;
        private ObservableDataSource<Point> temp_dataSource = new ObservableDataSource<Point>();
        private ObservableDataSource<Point> light_dataSource = new ObservableDataSource<Point>();
        private DispatcherTimer timer = new DispatcherTimer();
        private int i = 0;
        private List<string> receiveSource = new List<string>();
        private byte[] data1 = new byte[10];
        private int index = 0;
        private SerialPort port = new SerialPort("COM3", 9600);

        int tickStart = 0;
        int log1 = 0;//判断是否画图
        int log2 = 0;//判断是否开始写入文件
        PointPairList list1 = new PointPairList();
        Random ran = new Random();
        private System.Windows.Forms.Timer timer1=new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer timer2 = new System.Windows.Forms.Timer();

        public class Data
        {
            public string temp;
            public string light;
        }
        Data data = new Data();

        public MainWindow()
        {
            InitializeComponent();

            timer1.Tick += new System.EventHandler(AddDataPoint);
            timer1.Interval = 200;
            timer2.Tick += new System.EventHandler(AddDataPoint2);
            timer2.Interval = 200;

            SetGraph();
            SetGraph2();
            foreach (string port in portsArray)
            {
                ComboBox_Port.Items.Add(port);
            }
            ComboBox_Rate.Items.Add(9600);
            ComboBox_Rate.Items.Add(19200);
            ComboBox_Rate.Items.Add(38400);
            ComboBox_Rate.Items.Add(57600);
            ComboBox_Rate.Items.Add(115200);
            ComboBox_Rate.Items.Add(921600);
            blue_slider.Maximum = 255;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
        }
        String str = "";
        private void Getstr()
        {
            try
            {
                //定义一个字段，来保存串口传来的信息。                
                int len = serialPort.BytesToRead;
                Byte[] readBuffer = new Byte[len];
                serialPort.Read(readBuffer, 0, len);
                str = Encoding.Default.GetString(readBuffer);
                //如果需要和界面上的控件交互显示数据，使用下面的方法。其中ttt是控件的名称。                 
                //this.jieshou.Dispatcher.Invoke(new Action(() =>                
                //{                
                //    jieshou.Text = str ;                
                //}));                  
                //serialPort.DiscardInBuffer();  //清空接收缓冲区 
            }
            catch (Exception ex)
            {
                serialPort.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            

        }






        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                string port = this.ComboBox_Port.SelectedItem.ToString();
                string rate = this.ComboBox_Rate.SelectedItem.ToString();
                //serialPort.ReceivedBytesThreshold = 1;
                serialPort.Parity = Parity.None;
                serialPort.StopBits = StopBits.One;
                serialPort.DataBits = 8;
                serialPort.Handshake = Handshake.None;
                serialPort.RtsEnable = false;
                //serialPort.Encoding = Encoding.ASCII;
                serialPort.PortName = port;
                serialPort.BaudRate = int.Parse(rate);
                serialPort.ReceivedBytesThreshold = 1;
                serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
                if (!serialPort.IsOpen)
                {
                    serialPort.Open();
                    MessageBox.Show("连接成功");
                    timer1.Start();
                    timer2.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            portWrite("00");
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            portWrite("01");
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (serialPort.IsOpen)
            {
                try
                {
                    serialPort.Close();
                    serialPort.Dispose();
                    MessageBox.Show("断开成功");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("当前未连接到开发板");
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            //int val_red = (int)red_slider.Value;
            //int val_green = (int)green_silder.Value;
            //int val_blue = (int)blue_slider.Value;
            //int val_white = (int)white_slider.Value;
            //MessageBox.Show("红灯亮度： " + val_red.ToString() + "\n"
            //    + "绿灯亮度： " + val_green.ToString() + "\n"
            //    + "蓝灯亮度： " + val_blue.ToString() + "\n"
            //    + "白灯亮度： " + val_white.ToString() + "\n");
            //val_red += (9 << 8);
            //val_green += (10 << 8);
            //val_white += (6 << 8);
            //val_blue += (5 << 8);
            //string light_red = val_red.ToString();
            //string light_green = val_green.ToString();
            //string light_blue = val_blue.ToString();
            //string light_white = val_white.ToString();
            //portWrite(light_red);
            //Thread.Sleep(100);
            //portWrite(light_green);
            ////MessageBox.Show(light_green);
            //Thread.Sleep(100);
            //portWrite(light_blue);
            //Thread.Sleep(100);
            //portWrite(light_white);

            byte val_red = (byte)red_slider.Value;
            byte val_green = (byte)green_silder.Value;
            byte val_blue = (byte)blue_slider.Value;
            byte val_white = (byte)white_slider.Value;
            MessageBox.Show("红灯亮度： " + val_red.ToString() + "\n"
                + "绿灯亮度： " + val_green.ToString() + "\n"
                + "蓝灯亮度： " + val_blue.ToString() + "\n"
                + "白灯亮度： " + val_white.ToString() + "\n");
            byte[] byte1 = new byte[2];
            byte[] byte2 = new byte[2];
            byte[] byte3 = new byte[2];
            byte[] byte4 = new byte[2];
            int num1 =9;
            byte1[0] = (byte)num1;
            byte1[1] = val_red;

            int num2 = 10;
            byte2[0] = (byte)num2;
            byte2[1] = val_green;

            int num3 = 6;
            byte3[0] = (byte)num3;
            byte3[1] = val_white;

            int num4 = 5;
            byte4[0] = (byte)num4;
            byte4[1] = val_blue;

            serialPort.Write(byte1, 0, 2);
            Thread.Sleep(1000);
            serialPort.Write(byte2, 0, 2);
            Thread.Sleep(1000);
            serialPort.Write(byte3, 0, 2);
            Thread.Sleep(1000);
            serialPort.Write(byte4, 0, 2);
            Thread.Sleep(1000);

            int colors = ((int)red_slider.Value << 16) + ((int)green_silder.Value << 8) + (int)blue_slider.Value;

            Color cl = Color.FromArgb(255, val_red, val_green, val_blue);
            Brush brush = new SolidColorBrush(cl);
            color_change.Fill = brush;
        }
        private void portWrite(string message)
        {
            if (serialPort.IsOpen)
            {
                serialPort.Write(message);
            }
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void ListView_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ListView_SelectionChanged_2(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ListView_SelectionChanged_3(object sender, SelectionChangedEventArgs e)
        {

        }


        

        private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string value = this.portRead();
                if (value[0] < '8')
                {
                    temp_textBox.Dispatcher.Invoke(new Action(() =>
                    {
                        data.temp = value;
                        temp_textBox.AppendText(value);
                        temp_textBox.ScrollToEnd();
                    }));
                }
                else
                {
                    light_textBox.Dispatcher.Invoke(new Action(() =>
                    {
                        data.light = value;
                        light_textBox.AppendText(value);
                        light_textBox.ScrollToEnd();
                    }));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private string portRead()
        {
            string value = "";
            try
            {
                if (serialPort.IsOpen && serialPort.BytesToRead > 0)
                {
                    value = serialPort.ReadLine();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return value;
        }

        private void AnimatedPlot(object sender, EventArgs e)
        {
            double x = i;
            if (data.temp != null)
            {
                double y = double.Parse(data.temp);
                Point point = new Point(x, y);
                temp_dataSource.AppendAsync(base.Dispatcher, point);
            }
            if (data.light != null)
            {
                double y = double.Parse(data.light);
                Point point = new Point(x, y);
                light_dataSource.AppendAsync(base.Dispatcher, point);
            }
            i++;
        }

        private void TiJiao_Click(object sender, RoutedEventArgs e)
        {
            String str = sent.Text;
            String[] words = str.Split(' ');
            byte[] byteArray = new byte[words.Length];
            byte num = 0;
            for (int i = 0; i < words.Length; i++)
            {
                for (int j = 0; j < words[i].Length; j++)
                {
                    char ch = words[i][j];
                    num = (byte)(num * 16+ getValue(ch));
                }
                byteArray[i] = num;
                num = 0;
            }
            serialPort.Write(byteArray, 0, words.Length);     
        }

        private byte getValue(char ch)
        {
            if (ch >= '0' && ch <= '9')
            {
                return (byte)(ch - '0');
            }
            else if (ch >= 'a' && ch <= 'f')
            {
                return (byte)(ch - 'a' + 10);
            }
            else if (ch >= 'A' && ch <= 'F')
            {
                return (byte)(ch - 'A' + 10);
            }
            else
            {
                return 255;
            }
        }

        private void Jieshou_button_Click(object sender, RoutedEventArgs e)
        {
            
        }


        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            if (serialPort == null)
            {
                return;
            }
            int numOfByte = serialPort.BytesToRead;
            for (int i = 0; i < numOfByte; i++)
            {
                byte rxdata = (byte)serialPort.ReadByte();
                data1[index] = rxdata;
                if ((rxdata & 0x80) != 0)//收到命令
                {
                    if (index != 0)
                    {
                        index = 0;
                    }
                    data1[0] = rxdata;
                    index++;
                }
                else if (index != 0 && (data1[0] & 0x80) != 0)
                {
                    if (index < 3)
                    {
                        data1[index] = rxdata;
                        index++;
                    }
                    if (index == 3)
                    {
                        if (data1[0] == 0xF9)
                        {
                            string str1 = ((data1[1] << 7) + data1[2]).ToString();
                            Dispatcher.BeginInvoke(new Action(delegate
                            {
                                jieshou.Text = str1;
                            }));

                        }
                        index = 0;
                    }
                }
                else if (data1[index] == 0xa)
                {
                    Byte[] readBuffer = new Byte[numOfByte-1];
                    serialPort.Read(readBuffer, 0, numOfByte-1);
                    
                    str = Encoding.Default.GetString(readBuffer);
                    String[] words = str.Split('\r');
                    string str2 = words[0];
                    Dispatcher.BeginInvoke(new Action(delegate
                    {
                        temp_textBox.Text = str2;
                        jieshou.Text += System.DateTime.Now.ToString() + "     温度:" + str2 + "\r\n";
                        if (jieshou.Text.Length > 500)
                        {
                            jieshou.Text = "";
                        }
                        if(log2 == 1)
                        {
                            writeFile(texts);
                        }
                    }));
                    break;
                }
                else if (data1[index] == 0xb)
                {
                    Byte[] readBuffer = new Byte[numOfByte - 1];
                    serialPort.Read(readBuffer, 0, numOfByte - 1);
                    str = Encoding.Default.GetString(readBuffer);
                    String[] words = str.Split('\r');
                    string str2 = words[0];
                    Dispatcher.BeginInvoke(new Action(delegate
                    {
                        light_textBox.Text = str2;
                        jieshou.Text += System.DateTime.Now.ToString() + "     光强：" + str2 + "\r\n";
                        if (jieshou.Text.Length > 500)
                        {
                            jieshou.Text = "";
                        }
                    }));
                    break;
                }

            }
        }
        private void SetGraph()
        {
            GraphPane myPane = graph1.GraphPane;
            GraphPane myPane2 = graph1.GraphPane;

            /// 设置标题
            myPane.Title.Text= "Test of Dynamic Data Update with ZedGraph " + "(After  25 seconds the graph scrolls)";
            /// 设置X轴说明文字
            myPane.XAxis.Title.Text = "时间";
            /// 设置Y轴文字
            myPane.YAxis.Title.Text = "温度";


            /// 设置1200个点，假设每50毫秒更新一次，刚好检测1分钟
            /// 一旦构造后将不能更改这个值
            //RollingPointPairList 
            //IPointList 
            RollingPointPairList list1 = new RollingPointPairList(1200);
            RollingPointPairList list2 = new RollingPointPairList(1200);

            /// 开始，增加的线是没有数据点的(也就是list为空)   
            ///增加一条名称 :Voltage ，颜色 Color.Bule ，无符号，无数据的空线条

            LineItem curve1 = myPane.AddCurve("Voltage1", list1, System.Drawing.Color.Blue, SymbolType.None/*.Diamond*/ );
            LineItem curve2 = myPane.AddCurve("Voltage2", list2, System.Drawing.Color.Red, SymbolType.None);


            curve2.IsY2Axis = true;
            myPane.Y2Axis.IsVisible = true;
            myPane.Y2Axis.Scale.Min = -5.0;
            myPane.Y2Axis.Scale.Max = 5.0;
            // Align the Y2 axis labels so they are flush to the axis
            myPane.Y2Axis.Scale.Align = AlignP.Inside;
            //curve2.YAxisIndex = 1;

            myPane.Y2Axis.Scale.MaxAuto = false;
            myPane.Y2Axis.Scale.MinAuto = false;

            /// Just manually control the X axis range so it scrolls continuously 
            /// instead of discrete step-sized jumps 
            /// X 轴最小值 0  

            myPane.XAxis.Scale.Min = 0;
            myPane.XAxis.Scale.MaxGrace = 0.01;
            myPane.XAxis.Scale.MinGrace = 0.01;
            /// X轴最大30 

            myPane.XAxis.Scale.Max = 30;

            /// X轴小步长1,也就是小间隔


            myPane.XAxis.Scale.MinorStep = 1;

            /// X轴大步长为5，也就是显示文字的大间隔


            myPane.XAxis.Scale.MajorStep = 5;

            /// Save the beginning time for reference 
            ///保存开始时间
            tickStart = Environment.TickCount;
            /// Scale the axes 
            /// 改变轴的刻度
            graph1.AxisChange();

        }

        void AddDataPoint(object sender, EventArgs e)
        {
            if (log1 == 1)
            {

                //确保CurveList不为空
                if (graph1.GraphPane.CurveList.Count <= 0) return;

                // Get the  first CurveItem in the graph 

                //取Graph第一个曲线，也就是第一步:在 GraphPane.CurveList 集合中查找 CurveItem 
                for (int idxList = 0; idxList < graph1.GraphPane.CurveList.Count; idxList++)
                {
                    LineItem curve = graph1.GraphPane.CurveList[idxList] as LineItem;
                    if (curve == null) return;

                    // Get the PointPairList 
                    //第二步:在CurveItem中访问PointPairList(或者其它的IPointList)，根据自己的需要增加新数据或修改已存在的数据

                    IPointListEdit list = curve.Points as IPointListEdit;

                    // If this is null, it means the reference at curve.Points does not  
                    // support IPointListEdit, so we won't be able to modify it 

                    if (list == null) return;


                    // Time is measured in seconds 
                    double time = (Environment.TickCount - tickStart) / 1000.0;
                    // 3 seconds per cycle 

                    list.Add(time, System.Convert.ToDouble(temp_textBox.Text));

                    // Keep the X scale at a rolling 30 second interval, with one 

                    // major step between the max X value and the end of the axis 
                    Scale xScale = graph1.GraphPane.XAxis.Scale;
                    if (time > xScale.Max - xScale.MajorStep)
                    {
                        xScale.Max = time + xScale.MajorStep;
                        xScale.Min = xScale.Max - 30.0;
                    }

                }
                // Make sure the Y axis is rescaled to accommodate actual data 
                //第三步:调用ZedGraphControl.AxisChange()方法更新X和Y轴的范围


                graph1.AxisChange(); // Force a redraw  
                                     //第四步:调用Form.Invalidate()方法更新图表


                graph1.Invalidate();
            }
        }

        private void SetGraph2()
        {
            GraphPane myPane = graph2.GraphPane;

            /// 设置标题
            myPane.Title.Text = "Test of Dynamic Data Update with ZedGraph " + "(After  25 seconds the graph scrolls)";
            /// 设置X轴说明文字
            myPane.XAxis.Title.Text = "时间";
            /// 设置Y轴文字
            myPane.YAxis.Title.Text = "光强";


            /// 设置1200个点，假设每50毫秒更新一次，刚好检测1分钟
            /// 一旦构造后将不能更改这个值
            //RollingPointPairList 
            //IPointList 
            RollingPointPairList list1 = new RollingPointPairList(1200);
            RollingPointPairList list2 = new RollingPointPairList(1200);

            /// 开始，增加的线是没有数据点的(也就是list为空)   
            ///增加一条名称 :Voltage ，颜色 Color.Bule ，无符号，无数据的空线条

            LineItem curve1 = myPane.AddCurve("Voltage1", list1, System.Drawing.Color.Blue, SymbolType.None/*.Diamond*/ );
            LineItem curve2 = myPane.AddCurve("Voltage2", list2, System.Drawing.Color.Red, SymbolType.None);


            curve2.IsY2Axis = true;
            myPane.Y2Axis.IsVisible = true;
            myPane.Y2Axis.Scale.Min = -5.0;
            myPane.Y2Axis.Scale.Max = 5.0;
            // Align the Y2 axis labels so they are flush to the axis
            myPane.Y2Axis.Scale.Align = AlignP.Inside;
            //curve2.YAxisIndex = 1;

            myPane.Y2Axis.Scale.MaxAuto = false;
            myPane.Y2Axis.Scale.MinAuto = false;

            /// Just manually control the X axis range so it scrolls continuously 
            /// instead of discrete step-sized jumps 
            /// X 轴最小值 0  

            myPane.XAxis.Scale.Min = 0;
            myPane.XAxis.Scale.MaxGrace = 0.01;
            myPane.XAxis.Scale.MinGrace = 0.01;
            /// X轴最大30 

            myPane.XAxis.Scale.Max = 30;

            /// X轴小步长1,也就是小间隔


            myPane.XAxis.Scale.MinorStep = 1;

            /// X轴大步长为5，也就是显示文字的大间隔


            myPane.XAxis.Scale.MajorStep = 5;

            /// Save the beginning time for reference 
            ///保存开始时间
            tickStart = Environment.TickCount;
            /// Scale the axes 
            /// 改变轴的刻度
            graph2.AxisChange();
        }

        void AddDataPoint2(object sender, EventArgs e)
        {
            if (log1 == 1)
            {
                if (graph2.GraphPane.CurveList.Count <= 0) return;

                for (int idxList = 0; idxList < graph2.GraphPane.CurveList.Count; idxList++)
                {
                    LineItem curve = graph2.GraphPane.CurveList[idxList] as LineItem;
                    if (curve == null) return;
                    IPointListEdit list = curve.Points as IPointListEdit;

                    if (list == null) return;

                    double time = (Environment.TickCount - tickStart) / 1000.0;
                    list.Add(time, System.Convert.ToDouble(light_textBox.Text));

                    Scale xScale = graph2.GraphPane.XAxis.Scale;
                    if (time > xScale.Max - xScale.MajorStep)
                    {
                        xScale.Max = time + xScale.MajorStep;
                        xScale.Min = xScale.Max - 30.0;
                    }

                }
                graph2.AxisChange(); // Force a redraw  
                graph2.Invalidate();
            }
            
        }


        private void Jieshou_TextChanged(object sender, TextChangedEventArgs e)
        {
            jieshou.SelectionStart = jieshou.Text.Length;
            jieshou.ScrollToEnd();
        }

        private void Start_picture_Click(object sender, RoutedEventArgs e)
        {
            log1 = 1;
            timer1.Start();
            timer2.Start();
        }

        private void End_picture_Click(object sender, RoutedEventArgs e)
        {
            log1 = 0;
            timer1.Stop();
            timer2.Stop();
        }

        //获得文件路径	
        string localFilePath ;
        //获取文件路径，不带文件名	
        //FilePath = localFilePath.Substring(0, localFilePath.LastIndexOf("\\"));	
        //获取文件名，带后缀名，不带路径	
        string fileNameWithSuffix;
        //去除文件后缀名	
        string fileNameWithoutSuffix;
        //在文件名前加上时间	
        string fileNameWithTime;
        //在文件名里加字符	
        string newFileName;

        public void openFile()
        {
            var saveFileDialog = new Microsoft.Win32.SaveFileDialog();
            saveFileDialog.Filter = "TXT File(*.txt)|*.txt";
            var result = saveFileDialog.ShowDialog();
            if (result == true)
            {
                FileStream savefs = new FileStream(saveFileDialog.FileName, FileMode.Create);
                StreamWriter savesw = new StreamWriter(savefs);
                savesw.Flush();
                savesw.Close();
            }
            //创建一个打开文件式的对话框
            OpenFileDialog ofd = new OpenFileDialog();
            //设置这个对话框的起始打开路径
            ofd.InitialDirectory = @"E:\";
            //设置打开的文件的类型，注意过滤器的语法
            ofd.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";//设置默认文件类型显示顺序
            //调用ShowDialog()方法显示该对话框，该方法的返回值代表用户是否点击了确定按钮
            if (ofd.ShowDialog() == true)
            {
                //获得文件路径	
                localFilePath = ofd.FileName.ToString();
                //获取文件路径，不带文件名	
                //FilePath = localFilePath.Substring(0, localFilePath.LastIndexOf("\\"));

                //获取文件名，带后缀名，不带路径	
                fileNameWithSuffix = localFilePath.Substring(localFilePath.LastIndexOf("\\") + 1);

                //去除文件后缀名	
                fileNameWithoutSuffix = fileNameWithSuffix.Substring(0, fileNameWithSuffix.LastIndexOf("."));
                //在文件名前加上时间	
                fileNameWithTime = DateTime.Now.ToString("yyyy-MM-dd ") + ofd.FileName; 
                //在文件名里加字符	
                newFileName = localFilePath.Insert(1, "Tets");
            }
            else
            {
                MessageBox.Show("没有选择文件");
            }
        }


        public void writeFile(string strs)
        {
            System.IO.File.WriteAllText(localFilePath, string.Empty);
            if (log2 == 1){
                FileStream fs = new FileStream(localFilePath, FileMode.Append);
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(strs);
                sw.Flush();
                sw.Close();
                fs.Close(); 
            }
            
        }

        string texts = "";
        private void Log_start_Click(object sender, RoutedEventArgs e)
        {
            //texts = "\r\n"+ System.DateTime.Now.ToString() + "\r\n" + "temp:" + temp_textBox.Text + "\r\n";
            //texts += "The PWM of red = " + red_slider.Value.ToString() + "\r\n";
            //texts += "The PWM of green = " + green_silder.Value.ToString() + "\r\n";
            //texts += "The PWM of blue = " + blue_slider.Value.ToString() + "\r\n";
            //texts += "The PWM of yellow = " + white_slider.Value.ToString() + "\r\n";
            //texts += "Port' s value = "+ComboBox_Port.SelectedValue.ToString();
            texts = "{" + "\"Time\":\"" + System.DateTime.Now.ToString() +"\",";
            texts += "\"The_PWM_of_red\":\"" + red_slider.Value.ToString() + "\",";
            texts += "\"The_PWM_of_green\":\"" + green_silder.Value.ToString() + "\",";
            texts += "\"The_PWM_of_blue\":\"" + blue_slider.Value.ToString() + "\",";
            texts += "\"The_PWM_of_yellow\":\"" + white_slider.Value.ToString() + "\",";
            texts += "\"Port_value\":\"" + ComboBox_Port.SelectedValue.ToString()+ "\"}";
            openFile();
            writeFile(str);
            log2 = 1;
        }

        private void Log_end_Click(object sender, RoutedEventArgs e)
        {
            log2 = 0;
            string fileName_json = localFilePath.Substring(0,localFilePath.LastIndexOf(".") + 1) + "json";
            //MessageBox.Show(fileName_json);
            FileInfo fi = new FileInfo(localFilePath);
            fi.MoveTo(fileName_json);
        }
    }
    
}