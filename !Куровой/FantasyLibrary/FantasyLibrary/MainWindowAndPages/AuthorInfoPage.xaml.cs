using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для AuthorInfoPage.xaml
    /// </summary>
    public partial class AuthorInfoPage : Page
    {
        MainWindow MWIN;
        public AuthorInfoPage(string thisAuthorInfo, MainWindow mWin)
        {
            InitializeComponent();
            MWIN = mWin;
            ThisAuthorNameInfo.Text = thisAuthorInfo;
            using (FantasyLibraryDataEntities1 db = new FantasyLibraryDataEntities1())
            {
                var Books = db.Books.OrderBy(b=>b.SeriesName).ThenBy(b=>b.NumberInSeries);
                foreach (Book book in Books)
                {
                    if (thisAuthorInfo == book.AuthorName)
                    {
                        var image = new BitmapImage();
                        using (var mem = new MemoryStream(book.CoverBook))
                        {
                            mem.Position = 0;
                            image.BeginInit();
                            image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                            image.CacheOption = BitmapCacheOption.OnLoad;
                            image.UriSource = null;
                            image.StreamSource = mem;
                            image.EndInit();
                        }
                        image.Freeze();

                        StackPanel SP_buttonContent = new StackPanel();
                        SP_buttonContent.Background = null;
                        Image imageBook = new Image();
                        imageBook.Source = image;
                        imageBook.Margin = new Thickness(0, 5, 0, 0);
                        imageBook.Height = 180;
                        imageBook.Width = 150;
                        SP_buttonContent.Children.Add(imageBook);

                        TextBlock nameBook = new TextBlock();
                        nameBook.Text = $"{book.BookName}";
                        nameBook.Foreground = Brushes.Black;
                        nameBook.FontSize = 12;
                        nameBook.Height = 20;
                        nameBook.Width = 150;
                        nameBook.TextAlignment = TextAlignment.Center;
                        SP_buttonContent.Children.Add(nameBook);

                        TextBlock nameAuthor = new TextBlock();
                        nameAuthor.Text = $"{book.AuthorName}";
                        nameAuthor.Foreground = Brushes.Black;
                        nameAuthor.FontSize = 10;
                        nameAuthor.TextAlignment = TextAlignment.Center;
                        SP_buttonContent.Children.Add(nameAuthor);

                        Button buttonBook = new Button();
                        buttonBook.Margin = new Thickness(20, 10, 0, 10);
                        buttonBook.Height = 240;
                        buttonBook.Background = Brushes.White;
                        buttonBook.Click += Button_Click;

                        var bc = new BrushConverter();
                        buttonBook.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xC1, 0xC1, 0xC1));
                        buttonBook.BorderBrush = null;
                        buttonBook.Content = SP_buttonContent;
                        WP_CatalogThisAuthorBook.Children.Add(buttonBook);

                        //ListBoxItem authorsBook = new ListBoxItem();
                        //authorsBook.Content =$"{b.SeriesName} - {b.BookName}" ;
                        //authorsBook.FontSize = 22;
                        //if (AuthorsBooksList.Items.Count % 2 > 0)
                        //{
                        //    authorsBook.Background = new SolidColorBrush(Colors.Purple);
                        //    authorsBook.Foreground = new SolidColorBrush(Colors.White);
                        //}
                        //else
                        //{
                        //    authorsBook.Foreground = new SolidColorBrush(Colors.Purple);
                        //}
                        //AuthorsBooksList.Items.Add(authorsBook);
                    }
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button buttonBook = (Button)sender;
            StackPanel SP = (StackPanel)buttonBook.Content;
            var nameBookFromButton = SP.Children.OfType<TextBlock>().FirstOrDefault();

            Book ThisBook = null;
            using (FantasyLibraryDataEntities1 db = new FantasyLibraryDataEntities1())
            {
                var allBook = db.Books;
                foreach (Book book in allBook)
                {
                    if (nameBookFromButton.Text == book.BookName)
                    {
                        ThisBook = book;
                        break;
                    }
                }
            }
            BookinfoPage bookInfoPage = new BookinfoPage(ThisBook, MWIN.UserName.Text);
            MWIN.FrameForPages.Content = bookInfoPage;
        }
    }
}
