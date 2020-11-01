using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BookLib
{
    public class ItemCollection
    {
        //instance fields
        private List<AbstractItem> _items;// for looping is more efficient, performance is better than on dictionary
        private Dictionary<string, List<AbstractItem>> dicName;//to improve performance for indexer by name( o(1) instead of o(n))
        private Dictionary<int, AbstractItem> dicISBN;// to improve performance for indexer by ISBN
        private HashSet<string> _namesOfDiscounts;


        //ctor
        public ItemCollection()
        {
            _items = new List<AbstractItem>();
            dicName = new Dictionary<string, List<AbstractItem>>();
            dicISBN = new Dictionary<int, AbstractItem>();
            _namesOfDiscounts = new HashSet<string>();
        }

        //properties
        public HashSet<string> Discounts
        {
            get { return _namesOfDiscounts; }
        }
        public List<AbstractItem> Items
        {
            get { return _items; }
        }

        // method which add item to items and update the dictioneries
        public void AddItem(AbstractItem item)
        {
            _items.Add(item);
            dicISBN.Add(item.ISBN, item);
            UpdateDicName(item);
        }
        //update dictionary of name when item is added
        private void UpdateDicName(AbstractItem item)
        {
            if (dicName.ContainsKey(item.Name))//if the key with this name  is in dicname (dictionary)
            {
                dicName[item.Name].Add(item);//add to list of the appropriated member's dicName (with the requested name) the item
            }
            else//if the key with this name is not in dicname (dictionary)
            {
                List<AbstractItem> list = new List<AbstractItem>();
                list.Add(item);//add list the item
                dicName.Add(item.Name, list);//add dicNmae new member with the list and the new key (with the requested name)
            }
        }

        //method which remove an item 
        // return true if the item is renovable and reomove it and else return false
        public bool RemoveItem(AbstractItem item)
        {
            // not must check if it's exist in items, because, the system use this method, only
            //after user choose item which is belong to item collection.
            if (!item.IsRemovable() || !dicISBN.ContainsKey(item.ISBN))
            {
                return false;
            }
            _items.Remove(item);
            dicISBN.Remove(item.ISBN);
            RemoveFromDicName(item);
            return true;

        }
        //methods for remove item from dictionary of name, when item is removed
        private void RemoveFromDicName(AbstractItem item)
        {
            if (dicName[item.Name].Count == 1)
            {
                dicName.Remove(item.Name);
            }
            else
            {
                dicName[item.Name].Remove(item);
            }
        }


        //discount's methods
        //get list for updating a discount and the precent of discount
        //add the discount by name and update all items for discount (by using their method for adding a discount)
        public bool AddDiscount(double precent, string nameDiscount, List<AbstractItem> itemsForDiscount)
        {
            if (_namesOfDiscounts.Contains(nameDiscount))
            {
                return false;
            }
            foreach (var item in itemsForDiscount)
            {
                item.AddDiscount(nameDiscount, precent);
            }
            _namesOfDiscounts.Add(nameDiscount);
            return true;

        }
        //remove a discount by code from the HashSet and from the dictionary of each item which has this discount
        //by using method of RemoveDiscount(AbstractItem).
        // return true if code is exist ans else return false
        public bool RemoveDiscount(string nameForDiscount)
        {
            if (_namesOfDiscounts.Contains(nameForDiscount))
            {
                foreach (var item in _items)
                {
                    item.RemoveDiscount(nameForDiscount);
                }
                _namesOfDiscounts.Remove(nameForDiscount);
                return true;
            }
            return false;
        }

        //indexers
        //indexer by ISBN
        public AbstractItem this[int ISBN]
        {
            get
            {
                if (dicISBN.ContainsKey(ISBN))
                {
                    return dicISBN[ISBN];
                }
                return null;//exception maybe
            }
        }
        //indexer by Name of item
        public List<AbstractItem> this[string name]
        {
            get
            {
                if (dicName.ContainsKey(name))
                {
                    return dicName[name];
                }
                return null;//exception maybe
            }
        }

        // indexer by predicate, by add condition to predicate for search items that match to the search
        public List<AbstractItem> this[Predicate<AbstractItem> predicate]
        {
            get { return _items.FindAll(predicate); }
        }
        //COMMENT:the methods if return and rent get a item which is chosen by user, so it is belong to list of items
        //and don't need to check it.
        //method for rent of item, return true if the rent succeeded and else return false.
        //Uses the method of IsAvailableForRent (go to AbstractItem)
        public bool RentItem(AbstractItem item)
        {
            if (item.IsAvailableForRent()&&dicISBN.ContainsKey(item.ISBN))//not must check if exist, because user check to rent from list of existed items
            {
                return true;
            }
            return false;
        }
        //method for return an item, updating of numCopy of item
        public void ReturnItem(AbstractItem item)
        {
            if (dicISBN.ContainsKey(item.ISBN))//not must check if exist, because user check to rent from list of existed items
            {
                item.UpadteReturnCopy();
            }
        }

        // get a array of predicates With methods. Array can't be empty and must contain predicate with one method at least
        //execute each method of search (by predicate) on the result of LIST Which was obtained by executing the previous predicate.
        //return LIST of AbstractItem (return empty list if no results match search).
        public List<AbstractItem> IntersectDelegatesForSearch(Predicate<AbstractItem>[] predicate)
        {
            List<AbstractItem> finalDic = _items.FindAll(predicate[0]);
            for (int i = 0; i < predicate.Length && predicate[i] != null; i++)
            {
                if (finalDic.Count == 0)
                {
                    break;
                }
                finalDic = finalDic.FindAll(predicate[i]);

            }
            return finalDic;
            // need to check if is not null, because not always the array is full. Somtimes user search by all criteria and somtimes not.
        }

    }

}

