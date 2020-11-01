using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLib
{
    public class Journal:AbstractItem
    {
        protected string _datePrint;//only DD/MM/YYYY (without hours)
        protected string _topic;
        protected Category _category;

        public Journal(string name, int copyNum, string publisher, double priceOfRent,string datePrint,string topic,Category category) : base(name, copyNum, publisher, priceOfRent)
        {
            _datePrint = datePrint;
            _topic = topic;
            _category = category;
        }

        //properties
        public string DatePrint
        {
            get { return _datePrint; }
            set
            {
                _datePrint = value;
            }
        }
        public string Topic
        {
            get { return _topic; }
            set { _topic = value; }
        }
        public Category Category
        {
            get { return _category; }
            set { _category = value; }
        }

        public override string ToString()
        {
            StringBuilder descrpition = new StringBuilder();
            descrpition.Append(base.ToString());

            descrpition.Append(" Date of print:").Append(_datePrint);
            descrpition.Append(" Topic:").Append(_topic);
            return descrpition.ToString();
        }
    }
}
