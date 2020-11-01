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
    public sealed partial class ReturnItem : Page
    {
        private User _user;
        public ReturnItem(User user)
        {
            this.InitializeComponent();
            _user = user;
            LoadCombobox();
        }
        private async void MessageNoItemChoosen()
        {
            await new MessageDialog("Please choose item for return").ShowAsync();
        }
        private async void MessageSuccess()
        {
            await new MessageDialog("The item is remove from your rented items").ShowAsync();
        }

        private void buttonForReturn_Click(object sender, RoutedEventArgs e)
        {
            if (listBox.SelectedItem != null)
            {
                RentItem rentItem = _user.RentedItems[listBox.SelectedIndex];
                _user.UpdateReturn(rentItem);
                MainPage.Items.ReturnItem(rentItem.RentedItem);
                MessageSuccess();
                LoadCombobox();
            }
            else
            {
                MessageNoItemChoosen();
            }
        }
        private void LoadCombobox()
        {
            listBox.Items.Clear();
            for (int i = 0; i < _user.RentedItems.Count; i++)
            {
                listBox.Items.Insert(i, _user.RentedItems[i]);
            }
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
