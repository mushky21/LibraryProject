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
    public sealed partial class SearchItem : Page
    {
        private List<AbstractItem> itemsOfSearch;
        private User _user;
        private string typeOfSearch;

        public SearchItem(User user)
        {
            this.InitializeComponent();
            _user = user;
            LoadComboboxes();
            typeOfSearch = "Book";
            searchForJournal.Visibility = Visibility.Collapsed;
            rentItem.Visibility = Visibility.Collapsed;
            itemsOfSearch = new List<AbstractItem>();
            rBook.IsChecked = true;
        }
        private void LoadComboboxes()
        {
            //add genres to Genres combobox
            string[] genres = Enum.GetNames(typeof(Genre));
            foreach (string genre in genres)
            {
                Genres.Items.Add(genre);
            }
            string[] categories = Enum.GetNames(typeof(Category));
            foreach (string category in categories)
            {
                Cateegories.Items.Add(category);
            }
        }
        private void ClearPreviousSearch()
        {
            listBox.Items.Clear();
            itemsOfSearch.Clear();
            listBox.Visibility = Visibility.Collapsed;
        }

        private void searchByName_Click(object sender, RoutedEventArgs e)
        {
            ClearPreviousSearch();
            if (Name.Text != "")
            {
                if (typeOfSearch == "Book")
                {
                    itemsOfSearch = MainPage.Items[x => x.Name == Name.Text && (x is Book)];// by indexer of predicate
                }
                else
                {
                    itemsOfSearch = MainPage.Items[x => x.Name == Name.Text && (x is Journal)];// by indexer of predicate
                }

                AddItemsToListView();
            }
        }
        private void searchByAuthor_Click(object sender, RoutedEventArgs e)
        {
            ClearPreviousSearch();
            if (Author.Text != "")
            {

                itemsOfSearch = MainPage.Items[x =>(x is Book)&& (x as Book).Author == Author.Text];// by indexer of predicate
                AddItemsToListView();
            }
        }
        private void searchByPublisher_Click(object sender, RoutedEventArgs e)
        {
            ClearPreviousSearch();
            if (Publisher.Text != "")
            {
                if (typeOfSearch == "Book")
                {
                    itemsOfSearch = MainPage.Items[x => x.Publisher == Publisher.Text && (x is Book)];// by indexer of predicate
                }
                else
                {
                    itemsOfSearch = MainPage.Items[x => x.Publisher == Publisher.Text && (x is Journal)];// by indexer of predicate
                }
                AddItemsToListView();
            }
        }
        private void searchByGenre_Click(object sender, RoutedEventArgs e)
        {
            ClearPreviousSearch();
            if (Genres.SelectedItem != null)
            {
                Genre genre = (Genre)Genres.SelectedIndex;
                itemsOfSearch = MainPage.Items[((x => (x is Book) && ((x as Book).Geners.Contains(genre))))];
                AddItemsToListView();
            }

        }
        private void searchByCategory_Click(object sender, RoutedEventArgs e)
        {
            ClearPreviousSearch();
            if (Cateegories.SelectedItem != null)
            {
                Predicate<AbstractItem> predicate = new Predicate<AbstractItem>(x => (x is Journal)  && (x as Journal).Category == (Category)Cateegories.SelectedIndex);
                itemsOfSearch = MainPage.Items[predicate];
                AddItemsToListView();
            }
        }
        private void searchByDate_Click(object sender, RoutedEventArgs e)
        {
            ClearPreviousSearch();
            if (DateOfPrint.Date != null)
            {
                Predicate<AbstractItem> predicate = new Predicate<AbstractItem>(x => (x as Journal) != null && (x as Journal).DatePrint == DateOfPrint.Date.ToString("dd/MM/yyy"));
                itemsOfSearch = MainPage.Items[predicate];
                AddItemsToListView();
            }
        }
        private void searchByiSBN_Click(object sender, RoutedEventArgs e)
        {
            ClearPreviousSearch();
            if (ISBN.Text != "")
            {
                int _ISBN;
                if (!int.TryParse(ISBN.Text, out _ISBN))
                {
                    MessagISBNNotValid();
                }

                else
                {
                    AbstractItem item = MainPage.Items[int.Parse(ISBN.Text)];
                    if (item != null)
                    {
                        itemsOfSearch.Add(MainPage.Items[int.Parse(ISBN.Text)]);
                        AddItemsToListView();
                    }
                    else
                    {
                        MessageNotFoundItems();
                    }
                }
            }
        }

        private void AddItemsToListView()
        {
            if (itemsOfSearch.Count != 0)
            {
                for (int i = 0; i < itemsOfSearch.Count; i++)
                {
                    listBox.Items.Insert(i, itemsOfSearch[i].ToString());
                }
                listBox.Visibility = Visibility.Visible;
                rentItem.Visibility = Visibility.Visible;
            }
            else
            {
                MessageNotFoundItems();
            }
        }

        private async void MessageNotFoundItems()
        {
            await new MessageDialog("No items found ").ShowAsync();
        }
        private async void MessageNotChooseItems()
        {
            await new MessageDialog("Please select an item for rent").ShowAsync();
        }
        private async void MessageNotAvailableForRent()
        {
            await new MessageDialog("The selected item is not available for rent ").ShowAsync();
        }
        private async void MessageSuccessRent()
        {
            await new MessageDialog("The rental process succeeded").ShowAsync();
        }
        private async void MessagISBNNotValid()
        {
            await new MessageDialog("The chars must be numbers").ShowAsync();
        }


        private void UserChoice_Checked(object sender, RoutedEventArgs e)
        {
            ClearTextBoxes();
            if ((sender as RadioButton).Name == "rJournal")
            {
                ClearPreviousSearch();
                searchForBook.Visibility = Visibility.Collapsed;
                typeOfSearch = "Journal";
                searchForJournal.Visibility = Visibility.Visible;
            }
            else
            {
                ClearPreviousSearch();
                searchForJournal.Visibility = Visibility.Collapsed;
                searchForBook.Visibility = Visibility.Visible;
                typeOfSearch = "Book";
            }
        }
        private void ClearTextBoxes()
        {
            Name.Text = "";
            Publisher.Text = "";
            Author.Text = "";
        }
        private void rentItem_Click(object sender, RoutedEventArgs e)
        {
            if (listBox.SelectedItem != null) //if user choosed item from listBox
            {
                AbstractItem itemForRent = MainPage.Items[itemsOfSearch[listBox.SelectedIndex].ISBN];
                if (MainPage.Items.RentItem(itemForRent))
                {
                    RentItem rent = new RentItem(itemForRent);
                    _user.UpdateRent(rent);
                    MessageSuccessRent();
                    ClearPreviousSearch();
                }
                else //item is not available for rengt
                {
                    MessageNotAvailableForRent();
                }
            }
            else MessageNotChooseItems();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
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
