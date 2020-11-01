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
    public sealed partial class AllReports : Page
    {
        private User _user;
        public AllReports(User user)
        {
            this.InitializeComponent();
            _user = user;
            LoadAllComboboxes();
        }
        private void LoadAllComboboxes()
        {
            comboBoxSign.Items.Add("Higher than");
            comboBoxSign.Items.Add("Lower than");
            comboBoxSign.Items.Add("Equal to");
            comboBoxSign.SelectedIndex = 0;

            comboBoxType.Items.Add("All items");
            comboBoxType.Items.Add("All books");
            comboBoxType.Items.Add("All journals");
            comboBoxType.SelectedIndex = 0;

            comboBoxIsAvailable.Items.Add("Available");
            comboBoxIsAvailable.Items.Add("Not available");
            comboBoxIsAvailable.SelectedIndex = 0;
        }
        public bool DiscountIsTyped()
        {
            if (DiscountVal.Text == "")
            {
                return false;
            }
            return true;
        }

        private void buttonReportDiscount_Click(object sender, RoutedEventArgs e)
        {
            if (DiscountVal.Text != "")
            {
                double k;
                if (double.TryParse(DiscountVal.Text, out k))
                {
                    List<AbstractItem> items = ExecuteReportDiscount();
                    NavigateOrNot(items);
            
                }
                else MessagValueNotValid();
            }
            else MessageNotAllDetailsTyped();
        }

        private List<AbstractItem> ExecuteReportDiscount()
        {
            Predicate<AbstractItem>[] predicates = new Predicate<AbstractItem>[2];
            int index = comboBoxType.SelectedIndex;
            switch (index)
            {
                case 0:
                    predicates[0] = (x => (x.Publisher == Publisher.Text));
                    break;
                case 1:
                    predicates[0] = x => (x as Book) != null && (x.Publisher == Publisher.Text);
                    break;
                default:
                    predicates[0] = x => (x as Journal) != null && (x.Publisher == Publisher.Text);
                    break;
            }// for check the type which user choosed
            index = comboBoxSign.SelectedIndex;
            switch (index)
            {
                case 0:
                    predicates[1] = (x => x.DiscountPrecent > double.Parse(DiscountVal.Text));
                    break;
                case 1:
                    predicates[1] = (x => x.DiscountPrecent < double.Parse(DiscountVal.Text));
                    break;
                default:
                    predicates[1] = (x => x.DiscountPrecent == double.Parse(DiscountVal.Text));
                    break;
            } //check the sigm which appropriate to choice of user
            return MainPage.Items.IntersectDelegatesForSearch(predicates);// using the method for intersect some delegates
        }

        private async void MessageNotFoundItems()
        {
            await new MessageDialog("No items found ").ShowAsync();
        }
        private async void MessagValueNotValid()
        {
            await new MessageDialog("The system recognized invalid values").ShowAsync();
        }
        private async void MessageNotAllDetailsTyped()
        {
            await new MessageDialog("You nees to type more details ").ShowAsync();
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
        private void buttonReporAvailable_Click(object sender, RoutedEventArgs e)
        {
            List<AbstractItem> items = new List<AbstractItem>();
            if(comboBoxIsAvailable.SelectedIndex==0)
            {
                items=MainPage.Items[x => x.IsAvailableForRent()];
            }
            else items = MainPage.Items[x => !x.IsAvailableForRent()];
            NavigateOrNot(items);
        }
        private void NavigateOrNot(List<AbstractItem> items)
        {
            if (items.Count != 0)
            {
                ResultOfReport page = new ResultOfReport(items, _user);
                this.Content = page;
            }
            else MessageNotFoundItems();
        }
    }
}
