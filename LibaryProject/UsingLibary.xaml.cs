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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LibaryProject
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UsingLibary : Page
    {
        private static User userLogin;/*= MainPage.UserLogin;*/
        public UsingLibary(User user)
        {
            userLogin = user;
            this.InitializeComponent();
            if(userLogin.CodeOccupation==CodeOccupation.Customer)
            {
                addItem.Visibility = Visibility.Collapsed;
                addEmployee.Visibility = Visibility.Collapsed;
                reports.Visibility = Visibility.Collapsed;
                Adddiscount.Visibility = Visibility.Collapsed;
            }
            else if(userLogin.CodeOccupation == CodeOccupation.Librarian)
            {
                reports.Visibility = Visibility.Collapsed;
                Adddiscount.Visibility = Visibility.Collapsed;
                addEmployee.Visibility = Visibility.Collapsed;
            }
            else if(userLogin.CodeOccupation == CodeOccupation.DiscounManager)
            {
                reports.Visibility = Visibility.Collapsed;
                addEmployee.Visibility = Visibility.Collapsed;
            }
        }

        private void addItem_Click(object sender, RoutedEventArgs e)
        {
            AddingItems page = new AddingItems(userLogin);
            this.Content = page;
        }

        private void addEmployee_Click(object sender, RoutedEventArgs e)
        {
            RegisteringUser page = new RegisteringUser(userLogin);
            this.Content = page;
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            MainPage a = new MainPage();
            this.Content = a;
        }

        private void reports_Click(object sender, RoutedEventArgs e)
        {
            AllReports page = new AllReports(userLogin);
            this.Content = page;
        }
        private void searchBook_Click(object sender, RoutedEventArgs e)
        {
            SearchItem page = new SearchItem(userLogin);
            this.Content = page;
        }

        private void returnBtn_Click(object sender, RoutedEventArgs e)
        {
            ReturnItem page = new ReturnItem(userLogin);
            this.Content = page;
        }

        private void Adddiscount_Click(object sender, RoutedEventArgs e)
        {
            AddingDiscount page = new AddingDiscount(userLogin);
            this.Content = page;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            User user = e.Parameter as User;
        }
    }
}
