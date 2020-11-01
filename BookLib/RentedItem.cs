using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLib
{
    //class for structure of renting a item
    public class RentItem
    {
        //instance fields
        private AbstractItem _rentedItem;
        private string _dateOfRent;

        //properties
        public AbstractItem RentedItem
        {
            get { return _rentedItem; }
        }
        public string DateOfRented
        {
            get { return _dateOfRent; }
        }

        //ctor
        public RentItem(AbstractItem item)
        {
            _rentedItem = item;
            _dateOfRent = DateTime.Now.ToString();
        }
        //method for get date for return the rented item
        public string DateForReturn()
        {
            return DateTime.Parse(_dateOfRent).AddDays(30).ToString();
        }
        public override string ToString()
        {
            StringBuilder details = new StringBuilder();
            details.Append("Date of return:").Append(this.DateForReturn());
            details.Append(" Date of rent:").Append(_dateOfRent);
            details.Append(" Item: ").Append(_rentedItem);
            return details.ToString();
        }
    }
}
