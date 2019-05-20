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
    /// Логика взаимодействия для AuthorPage.xaml
    /// </summary>
    public partial class AuthorPage : Page
    {
        MainWindow MWIN;
        public AuthorPage(MainWindow mWin)
        {
            InitializeComponent();
            MWIN = mWin;
            using (FantasyLibraryDataEntities1 db = new FantasyLibraryDataEntities1())
            {
                var Authors = db.Authors;
                foreach(Author a in Authors)
                {
                    ListBoxItem author = new ListBoxItem();
                    author.Content = a.AuthorName;
                    author.FontSize = 22;
                    if (AuthorList.Items.Count % 2 > 0)
                    {
                        author.Background = new SolidColorBrush(Colors.Purple);
                        author.Foreground = new SolidColorBrush(Colors.White);
                    }
                    else
                    {
                        author.Foreground = new SolidColorBrush(Colors.Purple);
                    }
                    author.PreviewMouseLeftButtonDown += AuthorInfo_Selected;
                    AuthorList.Items.Add(author);
                }
            }
        }

        private void AuthorInfo_Selected(object sender, MouseButtonEventArgs e)
        {
            ListBoxItem thisAuthor = (ListBoxItem)sender;
            AuthorInfoPage authorInfoPage = new AuthorInfoPage(thisAuthor.Content.ToString(), MWIN);
            MWIN.FrameForPages.Content = authorInfoPage;
        }

        private void OpenAuthorInfoPage()
        {
            
        }
    }
}
