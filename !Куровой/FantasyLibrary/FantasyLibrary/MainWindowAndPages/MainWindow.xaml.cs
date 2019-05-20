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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Catalog CATALOG;
        AuthorPage AUTHOR_PAGE;
        public MainWindow(string userLogin)
        {
            InitializeComponent();
            UserName.Text = userLogin;

            MainWindow parentWindow = Window.GetWindow(this) as MainWindow;
            Catalog catalog = new Catalog(parentWindow);
            AuthorPage author_Page = new AuthorPage(parentWindow);
            CATALOG = catalog;
            AUTHOR_PAGE = author_Page;
            FrameForPages.Content = CATALOG;
        }

        private void ButtonPopUpLogout_Click(object sender, RoutedEventArgs e)
        {
            SingIn singIn = new SingIn();
            this.Close();
            singIn.Show();
            
        }

        private void ButtonPopUpExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
        }

        private void Catalog_Selected(object sender, RoutedEventArgs e)
        {
            FrameForPages.Content = CATALOG;
        }

        private void Author_Selected(object sender, MouseButtonEventArgs e)
        {
            FrameForPages.Content = AUTHOR_PAGE;
        }
    }
}
