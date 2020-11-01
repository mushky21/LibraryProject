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
    public sealed partial class AddingItems : Page
    {
        private User _user;
        public AddingItems(User user)
        {
            this.InitializeComponent();
            _user = user;
            LoadListBoxes();
            Book.IsChecked = true;
        }

        //when load the page
        private void LoadListBoxes()
        {
            //add genres to comGenre
            string[] genres = Enum.GetNames(typeof(Genre));
            foreach (string genre in genres)
            {
                listBoxGenres.Items.Add(genre);
            }
            //default value for genres
            listBoxGenres.SelectedIndex = 0;
            string[] categories = Enum.GetNames(typeof(Category));
            foreach (string category in categories)
            {
                listBoxCtegories.Items.Add(category);
            }
            //default value for genres
           listBoxCtegories.SelectedIndex = 0;

        }

        private bool CheckDetailsIsValid()
        {
            int a;
            if (Edition.Text!=""&&!int.TryParse(Edition.Text, out a))
            {
                return false;
            }
            if (CopyNum.Text!=""&&!int.TryParse(CopyNum.Text, out a))
            {
                return false;
            }
            if (Part.Text!=""&&!int.TryParse(Part.Text, out a))
            {
                return false;
            }
            double b=-5;
            if (PriceOfRent.Text!=""&&!double.TryParse(PriceOfRent.Text, out b) || b < 0)
            {
                return false;
            }
            return true;
        }
        private bool CheckAllDetailedTyped(string choice)
        {
            List<UIElement> textBoxes = common.Children.ToList().FindAll(x => x is TextBox);
            if (choice=="addBook")
            {
                textBoxes.AddRange(bookCanvas.Children.ToList().FindAll(x => x is TextBox));
            }
            else
            {
                textBoxes.AddRange(JournalCanvas.Children.ToList().FindAll(x => x is TextBox));
            }
            foreach (var item in textBoxes)
            {
             if((item as TextBox).Text=="")
                {
                    return false;
                }
            }
            if(DatePrint.Date==null)
            {
                return false;
            }
                return true;

        }

        private async void MessageErrorDetailsMissing()
        {
            await new MessageDialog("There are several data fields missing").ShowAsync();
        }
        private async void MessageErrorDetailsInvalid()
        {
            await new MessageDialog("There are several Invalid values").ShowAsync();
        }
        private async void MessageSuccess()
        {
            await new MessageDialog("The process of adding an item was successful ").ShowAsync();
        }

        //add item by click on button
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            string nameButton = (sender as Button).Name.ToString();
            if (!CheckAllDetailedTyped(nameButton))
            {
                MessageErrorDetailsMissing();
            }
            else if (!CheckDetailsIsValid())
            {
                MessageErrorDetailsInvalid();
            }
            else
            {
                AddItem(nameButton);
                MessageSuccess();
            }
        }
        //add item to list of items (static field in main page)
        private void AddItem(string nameButton)
        {
            if (nameButton == "addBook")
            {

                AbstractItem book = new Book(Name.Text, int.Parse(CopyNum.Text), Publisher.Text, double.Parse(PriceOfRent.Text), int.Parse(Edition.Text), Author.Text, int.Parse(Part.Text), GetGenresSelected());
                MainPage.Items.AddItem(book);
                ClearTextCanvas(common);
                ClearTextCanvas(bookCanvas);
            }
            else
            {
                AbstractItem journal = new Journal(Name.Text, int.Parse(CopyNum.Text), Publisher.Text, double.Parse(PriceOfRent.Text), DatePrint.Date.ToString("dd/MM/yyy"), Topic.Text,(Category)listBoxCtegories.SelectedIndex);
                MainPage.Items.AddItem(journal);
                ClearTextCanvas(common);
                ClearTextCanvas(JournalCanvas);
            }
        }
        //return list of all selected  (values) by user
        private List<Genre> GetGenresSelected()
        {
            List<Genre> genres = new List<Genre>();
            foreach (object genre in listBoxGenres.SelectedItems)
            {
                genres.Add((Genre)(listBoxGenres.Items.IndexOf(genre)));
            }
            return genres;
        }

        // for navigaton
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

        private void ChangeChoice_Checked(object sender, RoutedEventArgs e)
        {
            string choice = (sender as RadioButton).Name;
            if (choice == "Book")
            {
                ClearTextCanvas(common);
                ClearTextCanvas(JournalCanvas);
                JournalCanvas.Visibility = Visibility.Collapsed;
                bookCanvas.Visibility = Visibility.Visible;
            }
            else
            {
                ClearTextCanvas(common);
                ClearTextCanvas(bookCanvas);
                listBoxGenres.SelectedItems.Clear();
                listBoxGenres.SelectedIndex = 0;
                bookCanvas.Visibility = Visibility.Collapsed;
                JournalCanvas.Visibility = Visibility.Visible;
            }
        }
        //clear text of textBoxe of a canvas
        private void ClearTextCanvas(Canvas canvasForClear)
        {
            List<UIElement> textBoxes = canvasForClear.Children.ToList().FindAll(x => x is TextBox);
            foreach (var item in textBoxes)
            {
                (item as TextBox).Text = "";
            }
            

        }


    }


}
