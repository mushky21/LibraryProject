using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLib
{
    public enum CodeOccupation
    {
        //User type hierarchy
        Customer=0,//have permisions for search a item and rent it, to watch his rented items and his messages box 
        Librarian=1,//have permissions of customer + add item, remove item and edit it.
        DiscounManager=2,//have permissions of librarian+ add discount and remove it
        LibraryManager=3//have permissions of discountManager + add user of librarian and discountManger (employees)
    }
}
