using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для SingInPage.xaml
    /// </summary>

    public partial class SingInPage : Page
    {
        List<UserAccount> USER_ACCOUNT = new List<UserAccount>();
        public SingInPage()
        {
            InitializeComponent();

            using (FantasyLibraryDataEntities1 db =  new FantasyLibraryDataEntities1())
            {
                var Users = db.UserAccounts;
                foreach(UserAccount account in Users)
                {
                    USER_ACCOUNT.Add(account);
                }
            }
        }

        private void ButtonSingIn_Click(object sender, RoutedEventArgs e)
        {
            bool fidelityLogin = false;
            bool fidelityPassword = false;
            foreach(UserAccount account in USER_ACCOUNT)
            {
                if (UserLogin_TB.Text == account.UserLogin)
                {
                    fidelityLogin = true;
                }
                if(UserPasswor_TB.Password == account.UserPassword)
                {
                    fidelityPassword = true;
                    break;
                }
            }
            if (fidelityLogin == true)
            {
                if(fidelityPassword ==  true)
                {
                    MainWindow mWin = new MainWindow(UserLogin_TB.Text);
                    SingIn wSingIn = (SingIn)App.Current.MainWindow;
                    mWin.Show();
                    wSingIn.Close();
                }
                else
                {
                    MessageBox.Show("Неаерный пароль.\nПроверьте введеные данные.");
                    UserPasswor_TB.Clear();
                }
            }
            else
            {
                MessageBox.Show("Пользователя с таким именем не существует.\nПроверьте введеные данные.");
                UserLogin_TB.Clear();
                UserPasswor_TB.Clear();
            }
        }

        private void ButtonRegister_Click(object sender, RoutedEventArgs e)
        {
            SingInRegister singInRegister = new SingInRegister();
            SingIn wSingIn = (SingIn)App.Current.MainWindow;
            wSingIn.MainFrame.Content = singInRegister;
        }
    }
}
