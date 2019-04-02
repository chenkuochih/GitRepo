using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace TipCalculatorUniversal
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Tip tip;
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
            tip = new Tip();
        }
        private void amountTextBox_LotFoucus(object sender, RoutedEventArgs e)
        {
            billAmountTextBox.Text = tip.BillAmount;
        }

        private void billAmountTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            performCaculation();
        }

        private void amountTextBox_GotFoucus(object sender, RoutedEventArgs e)
        {
            billAmountTextBox.Text = "";
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            performCaculation();
        }

        private void performCaculation()
        {
            var selectedRadio = myStackPanel.Children.OfType<RadioButton>().FirstOrDefault(r => r.IsChecked == true);
            if (selectedRadio != null)
            {
                tip.CalculateTip(billAmountTextBox.Text, double.Parse(selectedRadio.Tag.ToString()));
            }
            if(tip != null)
            {
                amountToTipTextBlock.Text = tip.TipAmount;
                totalTextBlock.Text = tip.TotalAmount;
            }  
        }
    }
}
