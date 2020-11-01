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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LibaryProject
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddingDiscount : Page
    {
        private User _user;

        public AddingDiscount(User user)
        {
            this.InitializeComponent();
            _user = user;
            LoadCombboxes();
        }
        private void LoadCombboxes()
        {
            //add genres to comGenre
            string[] genres = Enum.GetNames(typeof(Genre));
            foreach (string genre in genres)
            {
                comboBoxGenre.Items.Add(genre);
            }
            //default value for genres
            comboBoxGenre.SelectedIndex = 0;
            string[] categories = Enum.GetNames(typeof(Category));
            foreach (string category in categories)
            {
                comboBoxCategory.Items.Add(category);
            }
            //default value for genres
            comboBoxCategory.SelectedIndex = 0;
        }
        private async void MessageErrorNameIsExist()
        {
            await new MessageDialog("The name for discount is already exist").ShowAsync();
        }
        private async void MessageSuccess()
        {
            await new MessageDialog("The process succeeded").ShowAsync();
        }

        private void discountForPublisher_Click(object sender, RoutedEventArgs e)
        {
            List<AbstractItem> items = MainPage.Items[x => x.Publisher == publisherForDiscount.Text];
            AddDiscount(items);

        }
        private void discountForAuthor_Click(object sender, RoutedEventArgs e)
        {
            List<AbstractItem> items = MainPage.Items[x => (x as Book) != null && (x as Book).Author == authorrForDiscount.Text];
            AddDiscount(items);
        }
        private void discountForGenre_Click(object sender, RoutedEventArgs e)
        {
            List<AbstractItem> items = MainPage.Items[x => (x as Book) != null && (x as Book).Geners.Contains((Genre)comboBoxGenre.SelectedIndex)];
            AddDiscount(items);
        }
        private void discountForCategory_Click(object sender, RoutedEventArgs e)
        {
                List<AbstractItem> items = MainPage.Items[x => (x as Journal) != null && (x as Journal).Category == (Category)comboBoxGenre.SelectedIndex];
                AddDiscount(items);
        }

        private void AddDiscount(List<AbstractItem> items)
        {
            if (!MainPage.Items.AddDiscount(double.Parse(PrecentForDiscount.Text), NameForDiscount.Text, items))
            {
                MessageErrorNameIsExist();
            }
            else
            {
                MessageSuccess();
            }
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(UsingLibary));
            UsingLibary page = new UsingLibary(_user);
            this.Content = page;
        }
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            MainPage page = new MainPage();
            this.Content = page;
        }
    }
}
