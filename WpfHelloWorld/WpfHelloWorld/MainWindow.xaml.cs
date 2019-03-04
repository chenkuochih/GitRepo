using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
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
}
