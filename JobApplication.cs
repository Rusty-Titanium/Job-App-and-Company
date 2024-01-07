using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Job_App_and_Company
{
    public class JobApplication
    {
        private String title, description;
        private PayRange pay;
        private DateTime date;

        public JobApplication () { }


        public JobApplication(String title, String description, PayRange pay, DateTime date)
        {
            this.title = title;
            this.description = description;
            this.pay = pay;
            this.date = date;
        }

        public String Title
        {
            get { return title; }

            set { title = value; }
        }

        public String Description
        {
            get { return description; }

            set { description = value; }
        }

        public PayRange Pay
        {
            get { return pay; }

            set { pay = value; }
        }

        public DateTime Date
        {
            get { return date; }

            set { date = value; }
        }

    }




    public class PayRange
    {
        // In the event I notice that listings will not have a pay range, but instead have a locked in pay amount, I need to create one more variable thats
        //      bool and if its true, then only minPay will get used or something.

        private int minPay, maxPay;
        private PayType paymentType;

        public PayRange() { }


        public PayRange(int minPay, int maxPay, PayType paymentType)
        {
            this.minPay = minPay;
            this.maxPay = maxPay;
            this.paymentType = paymentType;
        }

        public int MinPay
        {
            get { return minPay; }

            set { minPay = value; }
        }

        public int MaxPay
        {
            get { return maxPay; }

            set { maxPay = value; }
        }

        public PayType PaymentType
        {
            get { return paymentType; }

            set { paymentType = value; }
        }

        public override String ToString()
        {
            return "Beans";
        }


    }

}
