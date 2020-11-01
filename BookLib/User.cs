using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLib
{
    public class User
    {
        //instance fields
        private string _userName;
        private string _password;
        private CodeOccupation _codeOccupation;
        private string _firstName;
        private string _lastName;
        private string _phone;
        private string _city;
        private string _birthDate;
        private List<RentItem> _rentedItems;
        private int _NumOfRentedItems;
        //private int codeCredit;


        //option credit of History Actions+event of warning message?can be from type of guid

        //ctor
        public User(string userName, string password, string phone, string city, string birthDate, CodeOccupation codeOccupation = 0)//OPTIONAL FOR REGISTER OF USER!!
        {
            _userName = userName;
            _password = password;
            _codeOccupation = codeOccupation;
            _phone = phone;
            _city = city;
            _rentedItems = new List<RentItem>();
            _NumOfRentedItems = 0;
            _birthDate = birthDate;
        }

        //properties
        public int NumOfRentedItems
        {
            get { return _NumOfRentedItems; }
        }
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }
        public string Password
        {
            get { return _password; }
            //maybe set for change password
            set { _password = value; }
        }
        public CodeOccupation CodeOccupation
        {
            get { return _codeOccupation; }
            set { _codeOccupation = value; }
        }
        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }
        public string City
        {
            get { return _city; }
            set { _city = value; }
        }
        public string BirthDate
        {
            get { return _birthDate; }
        }
        public List<RentItem> RentedItems
        {
            get { return _rentedItems; }
        }

        public override string ToString()
        {
            StringBuilder details = new StringBuilder();//no return password becuse it is privileged
            details.Append("Username:").Append(_userName);
            details.Append(" CodeOcuupation:").Append(_codeOccupation);
            details.Append(" Phone:").Append(_phone);
            details.Append(" City:").Append(_city);
            return details.ToString();
        }

    
        //maybe add type if there are more class which inherited, only by condition that the type is of abstract item
        //update rent by user
        //return true if user can rent more items, else return false
        public void UpdateRent(RentItem item)
        {
           _rentedItems.Add(item);
            _NumOfRentedItems++;
        }
        //update return of item by user, remove the item from the aprropriate list
        public void UpdateReturn(RentItem item)//OPTION: TO CREATE DICTIONARY BY KEY OF ISBN AND REMOVE BY ISBN?
        {
            if(_rentedItems.Contains(item))
            {
                _rentedItems.Remove(item);
            }
        }


    }


}
