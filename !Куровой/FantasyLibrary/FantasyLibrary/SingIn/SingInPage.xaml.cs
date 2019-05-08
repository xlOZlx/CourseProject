using System;
using System.Collections.Generic;
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
        public SingInPage()
        {
            InitializeComponent();
        }

        private void ButtonSingIn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mWin = new MainWindow();
            SingIn wSingIn = (SingIn)App.Current.MainWindow;
            mWin.Show();
            wSingIn.Close();
        }

        private void ButtonRegister_Click(object sender, RoutedEventArgs e)
        {
            SingInRegister singInRegister = new SingInRegister();
            SingIn wSingIn = (SingIn)App.Current.MainWindow;
            wSingIn.MainFrame.Content = singInRegister;
        }
    }
}
