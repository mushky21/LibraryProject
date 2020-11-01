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
using BookLib;
using Windows.UI.Popups;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace LibaryProject
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 

    public sealed partial class MainPage : Page
    {
        User test;
        private static ManagerUsers users;
        private static BookLib.ItemCollection items;//(because has same name of class in ui)
        static MainPage()
        {
            //The static fields are intilaized once, in the first time which this page is called
            //and throuhout the runtime of app, the fields are changed by the user
            users = new ManagerUsers();
            items = new BookLib.ItemCollection();
            users.AddUser(new User("mushky", "kkk", "0586191191", "lod", "02/02/1997", CodeOccupation.Customer));
            users.AddUser(new User("mushky2", "kkk", "0586191191", "lod", "02/02/1997", CodeOccupation.Librarian));
            users.AddUser(new User("mushky5", "kkk", "0586191191", "lod", "02/02/1997", CodeOccupation.DiscounManager));
            users.AddUser(new User("mushky4", "kkk", "0586191191", "lod", "02/02/1997", CodeOccupation.LibraryManager));
            List<Genre> genres = new List<Genre>();
            genres.Add(0);
            items.AddItem(new Book("marko", 3, "sfarim", 56, 2018, "ari", 1, genres));
            items.AddItem(new Book("marko", 3, "sfarim", 56, 2018, "ari", 1, genres));
            items.AddItem(new Book("marko", 3, "sfarim", 56, 2018, "ari", 1, genres));
            items.AddItem(new Book("marko", 3, "sfarim", 56, 2018, "ari", 1, genres));
            items.AddItem(new Journal("marko", 3, "sfarim", 56,"04/07/2010","lll",0));
            items.AddItem(new Journal("marko", 3, "sfarim", 56, "04/07/2010", "lll", 0));
            items.AddItem(new Journal("marko", 3, "sfarim", 56, "04/07/2010", "lll", 0));
            items.AddItem(new Journal("marko", 3, "sfarim", 56, "04/07/2010", "lll", 0));
            items.AddItem(new Journal("marko", 3, "sfarim", 56, "04/07/2010", "lll", 0));
        }
        public MainPage()
        {
            this.InitializeComponent();

        }
        //properties for static fields which are saved throughout all runtime of app
        public static ManagerUsers Users
        {
            get { return users; }
        }
        public static BookLib.ItemCollection Items
        {
            get { return items; }
        }

        private void EnterToLibary(object sender, RoutedEventArgs e)
        {
            if (nameBox.Text != "" && passwordBox.Password != "")// if all details are typed
            {
                User user = users.Login(nameBox.Text, passwordBox.Password);
                if (user != null)// if user is belong to ManagerUsers
                {
                    UsingLibary page = new UsingLibary(user);
                    this.Content = page;
                }
                else
                {
                    MessageErrorLogin();// error for user  which is not belong...
                }
            }
            else MessageErrorMissingData();// error for missing details

        }

        //when username or password is not appropriated
        private async void MessageErrorLogin()
        {
            await new MessageDialog("The username or password you typed is incorrect. Unable to login").ShowAsync();
        }
        //when there are missing details
        private async void MessageErrorMissingData()
        {
            await new MessageDialog("There are missing detalis. Please try again").ShowAsync();
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            RegisteringUser r = new RegisteringUser(null);
            this.Content = r;
        }
    }
}
