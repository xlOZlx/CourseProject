using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FantasyLibrary
{
    /// <summary>
    /// Логика взаимодействия для SingInRegister.xaml
    /// </summary

    //public class UserAccounts
    //{
    //    public UserAccounts(string userLogin, string userPassword)
    //    {
    //        UserLogin = userLogin;
    //        UserPassword = userPassword;
    //    }
    //    public string UserLogin { get; private set; }
    //    public string UserPassword { get; private set; }
    //}
    public partial class SingInRegister : Page
    {
        public SingInRegister()
        {
            InitializeComponent();
        }

        private void ButtonRegister_Click(object sender, RoutedEventArgs e)
        {
            if (UserLogin_TB.Text == "" || UserPassword_PB.Password == "" || RepeatUserPassword_PB.Password == "")
            {
                MessageBox.Show("Введите данные.");
            }
            else
            {
                bool repeatLogin = false;
                bool repeatPassword = false;
                if(UserPassword_PB.Password == RepeatUserPassword_PB.Password)
                {
                    repeatPassword = true;
                }
                using (FantasyLibraryDataEntities1 db = new FantasyLibraryDataEntities1())
                {
                    var Users = db.UserAccounts;
                    foreach(UserAccount account in Users)
                    {
                        if(UserLogin_TB.Text == account.UserLogin)
                        {
                            repeatLogin = true;
                            break;
                        }
                    }
                    if (repeatLogin == false)
                    {
                        if(repeatPassword == true)
                        {
                            UserAccount account = new UserAccount
                            {
                                UserLogin = UserLogin_TB.Text,
                                UserPassword = UserPassword_PB.Password
                            };
                            db.UserAccounts.Add(account);
                            db.SaveChanges();

                            SingInPage singInPage = new SingInPage();
                            SingIn wSingIn = (SingIn)App.Current.MainWindow;
                            wSingIn.MainFrame.Content = singInPage;
                        }
                        else
                        {
                            MessageBox.Show("Пароли не соответствуют дру другу.\nПроверьте введеные данные.");
                            RepeatUserPassword_PB.Clear();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Пользователь с таким именем уже существуе.\nВведите другое имя.");
                        UserLogin_TB.Clear();
                        UserPassword_PB.Clear();
                        RepeatUserPassword_PB.Clear();
                    }
                }
            }
        }

        private void ButtonCancellation_Click(object sender, RoutedEventArgs e)
        {
            SingInPage singInPage = new SingInPage();
            SingIn wSingIn = (SingIn)App.Current.MainWindow;
            wSingIn.MainFrame.Content = singInPage;
        }
    }
}
