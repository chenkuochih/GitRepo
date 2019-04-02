using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TipCalculatorUniversal
{
    public class Tip
    {
        public string BillAmount { get; set; }
        public string TipAmount { get; set; }
        public string TotalAmount { get; set; }

        public Tip()
        {
            this.BillAmount = String.Empty;
            this.TipAmount = String.Empty;
            this.TotalAmount = String.Empty;
        }

        public void CalculateTip(string originalAmount, double tipPercentage)
        {
            double billAmount = 0.0;
            double tipAmount = 0.0;
            double totalAmount = 0.0;

            if (double.TryParse(originalAmount.Replace('$', ' '), out billAmount))
            {
                tipAmount = billAmount * tipPercentage;
                totalAmount = billAmount + tipAmount;
            }

            this.BillAmount = String.Format("{0:C}", billAmount);
            this.TipAmount = String.Format("{0:C}", tipAmount);
            this.TotalAmount = String.Format("{0:C}", totalAmount);
        }
    }

}
