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
//maybe need to put false in isformregistered when submit is succeeded//
//also write code for manager libary which want add his employee
namespace LibaryProject
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RegisteringUser : Page
    {
        private User _user;
        public RegisteringUser(User user)
        {
            this.InitializeComponent();
            _user = user;
            if (user == null)// if the navigaton is from register button, before any user entered to libary
            {
                //if from register button of MainPage - the possibility of choosing a codeOccupation is no relevant
                comBoxCodeOccupation.Visibility = Visibility.Collapsed;
                OccupationBl.Visibility = Visibility.Collapsed;

            }
            else
            {
                //if the navigaton is from addEmployee button, when LibaryManager entered to libary
                comBoxCodeOccupation.Items.Add("Librarian");
                comBoxCodeOccupation.Items.Add("Discount manager");
                comBoxCodeOccupation.SelectedIndex = 0;
            }
        }

        private void Sumbit_Click(object sender, RoutedEventArgs e)
        {
            if (CheckDetailsIsTyped())
            {
                if (_user == null)// if from register button, the code occupation is customer(0)
                {

                    AddUser((CodeOccupation)0);
                }
                else AddUser((CodeOccupation)comBoxCodeOccupation.SelectedIndex - 1);//because the index of combobox for values are less one from their index in codeoccupation
            }
            else //when user didn't type all details
            {
                MessageErrorDetailsMissing();
            }
        }
        private bool CheckDetailsIsTyped()
        {
            //check that all textbox is not empty
            List<UIElement> textBox = allTextBox.Children.ToList();
            for (int i = 0; i < textBox.Count; i++)
            {
                if ((textBox[i] as TextBox).Text == "")
                {
                    return false;
                }
            }
            //check that password box is empty
            if (Password.Password == "")
            {
                return false;
            }

            return true;//if user chosed and typed all the requested details
        }
        //add user to user collection
        private void AddUser(CodeOccupation code)
        {
            User user = new User(Username.Text, Password.Password, Phone.Text, City.Text, birthDate.Date.ToString("dd/MM/yyyy"), code);
            bool isAdded = MainPage.Users.AddUser(user);//check if adding of user Succeeded
            if (!isAdded)
            {
                MessageErrorUsername();
            }
            else
            {
                MessageSuccessRegistration();
            }
        }


        //error for username which is registered in users collection.
        private async void MessageErrorUsername()
        {
            await new MessageDialog("The username is already registered in system. Please try type another username").ShowAsync();
        }
        //message for sumbit registration
        private async void MessageSuccessRegistration()
        {
            await new MessageDialog("The registration process was successful").ShowAsync();
        }
        //details are missing
        private async void MessageErrorDetailsMissing()
        {
            await new MessageDialog("There are several data fields missing").ShowAsync();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (_user == null)
            {
                MainPage page = new MainPage();
                this.Content = page;
            }
            else
            {
                UsingLibary page = new UsingLibary(_user);
                this.Content = page;
            }
        }
    }
}
