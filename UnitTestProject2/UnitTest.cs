
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookLib;
using System.Collections.Generic;

namespace UnitTestProject2
{

    [TestClass]
    public class UnitTest1
    {
        //checking of methods of class AbstractItem      
        [TestMethod]
        public void PriceCurrent()
        {
            List<Genre> genres = new List<Genre>();
            genres.Add(0);
            Book book = new Book("marko", 3, "sfarim", 56, 2018, "ari", 1, genres);
            Assert.IsTrue(book.PriceCurrent() == 56);//There is no discount
        }
        [TestMethod]
        public void AddDiscount()
        {
            List<Genre> genres = new List<Genre>();
            genres.Add(0);
            Book book = new Book("marko", 3, "sfarim", 56, 2018, "ari", 1, genres);
            book.AddDiscount("BlackFriday", 70);
            book.AddDiscount("Shavoot", 20);
            Assert.IsTrue(book.DiscountPrecent == 70);//only the max discount
        }
        [TestMethod]
        public void RemoveDiscountWhichNotExist()
        {
            List<Genre> genres = new List<Genre>();
            genres.Add(0);
            Book book = new Book("marko", 3, "sfarim", 56, 2018, "ari", 1, genres);
            Assert.IsFalse(book.RemoveDiscount("Kal"));//only the max discount
        }
        [TestMethod]
        public void RemoveDiscountWhichExist()
        {
            List<Genre> genres = new List<Genre>();
            genres.Add(0);
            Book book = new Book("marko", 3, "sfarim", 56, 2018, "ari", 1, genres);
            book.AddDiscount("Shavoot", 20);
            Assert.IsTrue(book.RemoveDiscount("Shavoot") && book.DiscountPrecent == 0);
        }
        [TestMethod]
        public void SetNumOFCopies()
        {
            Journal journal = new Journal("marko", 3, "sfarim", 56, "04/07/2010", "lll", 0);
            Assert.IsTrue(journal.SetNumOfCopies(2));
        }
        [TestMethod]
        public void IsRemovable()
        {
            Journal journal = new Journal("marko", 1, "sfarim", 56, "04/07/2010", "lll", 0);
            Assert.IsTrue(journal.IsRemovable());
        }
        [TestMethod]
        public void NumOfCopies()
        {
            Journal journal = new Journal("marko", 1, "sfarim", 56, "04/07/2010", "lll", 0);
            Assert.IsTrue(journal.NumOfCopies() == 1);
        }
        [TestMethod]
        public void NumOfAvailableCopies1()
        {
            Journal journal = new Journal("marko", 1, "sfarim", 56, "04/07/2010", "lll", 0);
            Assert.IsTrue(journal.NumOfAvailableCopies() == 1);
        }
        [TestMethod]
        public void IsAvailableForRent()
        {
            Journal journal = new Journal("marko", 1, "sfarim", 56, "04/07/2010", "lll", 0);
            Assert.IsTrue(journal.IsAvailableForRent());
        }
        [TestMethod]
        public void UpadteReturnCopy()
        {
            Journal journal = new Journal("marko", 1, "sfarim", 56, "04/07/2010", "lll", 0);
            journal.IsAvailableForRent();//rent of one copy
            journal.UpadteReturnCopy();//return one copy
            Assert.IsTrue(journal.NumOfAvailableCopies() == 1);
        }

        //checking of methods of class ItemCollection
        [TestMethod]
        public void ItemCollection()//ctor
        {
            ItemCollection items = new ItemCollection();
            Assert.IsNotNull(items);
        }
        [TestMethod]
        public void AddItem()
        {
            ItemCollection items = new ItemCollection();
            items.AddItem(new Journal("marko", 1, "sfarim", 56, "04/07/2010", "lll", 0));
            Assert.IsTrue(items.Items.Count == 1);
        }
        [TestMethod]
        public void RemoveItem1()
        {
            ItemCollection items = new ItemCollection();
            Journal journal = new Journal("marko", 1, "sfarim", 56, "04/07/2010", "lll", 0);
            Assert.IsFalse(items.RemoveItem(journal));
        }
        [TestMethod]
        public void AddDiscount2()
        {
            ItemCollection items = new ItemCollection();
            Assert.IsTrue(items.AddDiscount(70, "kk", new List<AbstractItem>()));
        }
        [TestMethod]
        public void RemoveDiscount()
        {
            ItemCollection items = new ItemCollection();
            items.AddDiscount(70, "kk", new List<AbstractItem>());
            Assert.IsTrue(items.RemoveDiscount("kk"));
        }
        [TestMethod]
        public void IndexerByISBN()
        {
            ItemCollection items = new ItemCollection();
            Journal journal = new Journal("marko", 1, "sfarim", 56, "04/07/2010", "lll", 0);
            items.AddItem(journal);
            Assert.AreEqual(journal, items[13]);
        }
        [TestMethod]
        public void IndexerByName()
        {
            ItemCollection items = new ItemCollection();
            Journal journal = new Journal("mark", 1, "sfarim", 56, "04/07/2010", "lll", 0);
            items.AddItem(journal);
            Assert.IsInstanceOfType(items["mark"], typeof(List<AbstractItem>));
        }
        [TestMethod]
        public void IndexerByPredicate()
        {
            ItemCollection items = new ItemCollection();
            Journal journal = new Journal("mark", 1, "sfari", 56, "04/07/2010", "lll", 0);
            items.AddItem(journal);
            Assert.IsTrue(items[x => x.Publisher == "sfari"].Count == 1);
        }
        [TestMethod]
        public void RentItem()
        {
            ItemCollection items = new ItemCollection();
            Journal journal = new Journal("mark", 1, "sfari", 56, "04/07/2010", "lll", 0);
            items.AddItem(journal);
            Assert.IsTrue(items.RentItem(journal));
        }
        [TestMethod]
        public void ReturnItem()
        {
            ItemCollection items = new ItemCollection();
            Journal journal = new Journal("mark", 1, "sfari", 56, "04/07/2010", "lll", 0);
            items.AddItem(journal);
            items.ReturnItem(journal);
            items.ReturnItem(journal);
            Assert.IsTrue(journal.NumOfAvailableCopies() == 1);
        }
        [TestMethod]
        public void IntersectDelegatesForSearch()
        {
            ItemCollection items = new ItemCollection();
            Journal journal = new Journal("mark", 7, "sfari", 56, "04/07/2010", "lll", 0);
            items.AddItem(journal);
            Predicate<AbstractItem>[] predicates = new Predicate<AbstractItem>[2];
            predicates[0] = x => x.Publisher == "sfari";
            predicates[1] = x => x.NumOfAvailableCopies() == 7;
            Assert.IsTrue(items.IntersectDelegatesForSearch(predicates).Contains(journal));

        }
        //checking of methods of class User
        [TestMethod]
        public void UpdateRent()
        {
            Journal journal = new Journal("mark", 7, "sfari", 56, "04/07/2010", "lll", 0);
            User user = new User("mushky", "kkk", "0586191191", "lod", "02/02/1997", CodeOccupation.Customer);
            user.UpdateRent(new RentItem(journal));
            Assert.IsTrue(user.RentedItems.Count == 1);
        }
        [TestMethod]
        public void UpdateReturn()
        {
            Journal journal = new Journal("mark", 7, "sfari", 56, "04/07/2010", "lll", 0);
            User user = new User("mushky", "kkk", "0586191191", "lod", "02/02/1997", CodeOccupation.Customer);
            RentItem rent = new RentItem(journal);
            user.UpdateRent(rent);
            user.UpdateReturn(rent);
            Assert.IsTrue(user.RentedItems.Count == 0);
        }

        //checking of methods of class Manager users
        [TestMethod]
        public void ManagerUsers()//ctor
        {
            ManagerUsers manager = new ManagerUsers();
            Assert.IsNotNull(manager);
        }
        [TestMethod]
        public void AddUser()
        {
            ManagerUsers manager = new ManagerUsers();
            User user = new User("mushky7", "kkk", "0586191191", "lod", "02/02/1997", CodeOccupation.Customer);
            Assert.IsTrue(manager.AddUser(user));
        }
        [TestMethod]
        public void RemoveUser()
        {
            ManagerUsers manager = new ManagerUsers();
            User user = new User("mushky9", "kkk", "0586191191", "lod", "02/02/1997", CodeOccupation.Customer);
            manager.AddUser(user);

            Assert.IsTrue(manager.RemoveUser(user.UserName));
        }
        [TestMethod]
        public void Login()
        {
            ManagerUsers manager = new ManagerUsers();
            User user = new User("mushky9", "kkk", "0586191191", "lod", "02/02/1997", CodeOccupation.Customer);
            manager.AddUser(user);
            Assert.IsTrue(manager.Login("mushky9", "kkk").UserName== "mushky9");
        }


        [TestMethod]
        public void RentItem2()//ctor
        {
            Journal journal = new Journal("mark", 7, "sfari", 56, "04/07/2010", "lll", 0);
            RentItem rent = new RentItem(journal);
            Assert.IsNotNull(rent.RentedItem);
        }
    }
}
