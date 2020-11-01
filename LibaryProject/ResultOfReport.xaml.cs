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
    public sealed partial class ResultOfReport : Page
    {
        private List<AbstractItem> _items;
        private User _user;
        public ResultOfReport(List<AbstractItem> itemsForRepot,User user )
        {
            this.InitializeComponent();
            _user = user;
            _items = itemsForRepot;
            for (int i = 0; i < _items.Count; i++)
            {
                listBox.Items.Insert(i, _items[i].ToString());
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            AllReports page = new AllReports(_user);
            this.Content = page;
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            MainPage page = new MainPage();
            this.Content = page;
        }


    }
}
