using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BookLib
{
    public abstract class AbstractItem//access can be internal?
    {

        //static fields
        protected static int _CountISBN = 1;

        //instance fields
        protected string _name;
        protected readonly int _ISBN;
        protected string _publisher;
        protected double _priceOfRent;
        protected double _discountPrecent;//the high discount
        protected Dictionary<int, bool> _copies;
        protected Dictionary<string, double> _discountsPerItem;//all discounts which match to item
        //User is not exposed to copy numbers. However, copy numbers are saved to know the original quantity of the copies and not to subtract a copy each time the rent is executed.

        //ctor
        public AbstractItem(string name, int copyNum, string publisher, double priceOfRent)
        {
            _name = name;
            _ISBN = _CountISBN;
            _CountISBN++;//increase countISBN for next item
            _publisher = publisher;
            _priceOfRent = priceOfRent;

            //intilaize the dictionary of copy according to num of copy (default status of copy is false, when user rent, it wiil get a value of true) 
            _copies = new Dictionary<int, bool>();//get the amount of copies
            for (int i = 1; i < copyNum + 1; i++)
            {
                _copies.Add(i, false);
            }
            _discountsPerItem = new Dictionary<string, double>();
        }

        //need a set? i think for mistakes of typing the item's details
        //properties
        public int ISBN { get { return _ISBN; } }
        public string Publisher
        {
            get { return _publisher; }
            set
            {
                _publisher = value;

            }
        }
        public String Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public Dictionary<int, bool> CopyNum
        {
            get { return _copies; }
        }
        public double PriceOfRent
        {
            get { return _priceOfRent; }
            set
            {
                if (_priceOfRent <= 0) throw new NegativeNumberIsNotAllowedException("The number must be negative and and greater than zero");
                _priceOfRent = value;
            }
        }
        public double DiscountPrecent
        {
            get { return _discountPrecent; }
        }

        //calculate the current price according to discountPrecent and origin price.
        public double PriceCurrent()
        {
            return (1- (_discountPrecent*0.01))* _priceOfRent;
        }
        //add name of discount  to dictionary and update the max discount to the field.
        public void AddDiscount(string nameOfDiscount, double precentDiscount)
        {
            _discountsPerItem.Add(nameOfDiscount, precentDiscount);
            _discountPrecent = MaxDiscount();

        }
        //remove discount by code and update the max discount
        public bool RemoveDiscount(string nameOfDiscount)
        {
            if (_discountsPerItem.ContainsKey(nameOfDiscount))
            {
                _discountsPerItem.Remove(nameOfDiscount);
                _discountPrecent = MaxDiscount();
                return true;
            }
            return false;
        }
        //check the max discount from the dictionary and return it
        //if there are not discounts, the method will not enter to foreach and return 0
        //private because it is intended for internal using of this class
        private double MaxDiscount()
        {
            double max = 0;
            foreach (KeyValuePair<string, double> discount in _discountsPerItem)
            {
                if (discount.Value > max)
                {
                    max = discount.Value;
                }
            }
            return max;
        }

        //set num of copies by using private methods
        //return true if setting succeeded, else return false
        //if new num of copy is high more than current, add the current amount - the Subtraction between the requested amount of copies and the current.
        //if new num of copy is equal to the cuurent amount - return true and not update
        //if new num of copy is less than amount of copies, check if the requested amount for removing is available (and not in rent)
        //return true if is available and remove the requested amount of copies, else return false.
        //must be positive!!--exception 
        public bool SetNumOfCopies(int numOfCopy)
        {
            if (numOfCopy <= 0)
            {
                throw new NegativeNumberIsNotAllowedException("The number must be negative and and greater than zero");
            }
            int copiesForChange = numOfCopy - (_copies.Count);
            if (numOfCopy > _copies.Count)
            {
                AddCopy(copiesForChange);
                return true;
            }
            else if (numOfCopy == _copies.Count)
            {
                return true;
            }
            else
            {
                if (NumOfAvailableCopies() >= copiesForChange)
                {
                    for (int i = 0; i < copiesForChange; i++)
                    {
                        this.RemoveCopy();
                    }
                    return true;
                }
                return false;// return false if is not possible to remove the num of copy, because those are in rent by user.
            }
        }
        //private methods for method of SetNumOfCopies
        private void AddCopy(int amountOfCopy)
        {
            for (int i = _copies.Count+1, j = 0; i < amountOfCopy; j++, i++)
            {
                _copies.Add(i, false);
            }
        }
        //check if there is copy which is not rented by any user
        //if there is any copy, remove the last copy and put his value in this copy, else return false.
        private void RemoveCopy()
        {
            if (!_copies[_copies.Count])
            {
                _copies.Remove(_copies.Count);
            }
            else
            {
                for (int i = 1; i < _copies.Count; i++)
                {
                    if (!_copies[i])
                    {
                        _copies[i] = _copies[_copies.Count];
                        _copies.Remove(_copies.Count);
                        break;
                    }
                }

            }
        }

        //check if there is at least one copy in status of rent. in order to avoid remove of item which is belong to one userץ
        //return true if all copies are not in rent, else false.
        public bool IsRemovable()
        {
            for (int i = 1; i <= _copies.Count; i++)
            {
                if (_copies[i])
                {
                    return false;
                }
            }
            return true;
        }
        public int NumOfCopies()
        {
            return _copies.Count;
        }
        public int NumOfAvailableCopies()
        {
            int count = 0;
            for (int i = 1; i <= _copies.Count; i++)
            {
                if (!_copies[i])
                {
                    count++;
                }
            }
            return count;
        }


        //check if there is copy which available for rent.
        //if available - return true and update the copy to true.else return false
        public bool IsAvailableForRent()
        {
            for (int i = 1; i <= _copies.Count; i++)
            {
                if (!_copies[i])
                {
                    _copies[i] = true;
                    return true;
                }
            }
            return false;
        }
        public void UpadteReturnCopy()
        {
            for (int i = 1; i <= _copies.Count; i++)
            {
                if (_copies[i])
                {
                    _copies[i] = false;
                }
            }
        }

        // using of stringbuilder is more efficient, espacially for time's performance 
        //moreover, this method is useful for libary, therfore it is important that it will be most efficient
        public override string ToString()
        {
            StringBuilder descrpition = new StringBuilder();
            descrpition.Append(this.GetType().Name);//name of type
            descrpition.Append(" ISBN:").Append(_ISBN);
            descrpition.Append(" Name:").Append(_name);
            descrpition.Append(" Number of copy:").Append(_copies.Count);//?
            descrpition.Append(" Available:").Append(this.NumOfAvailableCopies());
            descrpition.Append(" Price:").Append(PriceCurrent());

            return descrpition.ToString();
        }


    }
}
