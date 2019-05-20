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
using System.Xml.Linq;
using FB2Library;
using Microsoft.Win32;

namespace FantasyLibrary
{
    /// <summary>
    /// Логика взаимодействия для Catalog.xaml
    /// </summary>
    public partial class Catalog : Page
    {
        MainWindow MWIN;

        public Catalog(MainWindow mWin)
        {
            InitializeComponent();
            MWIN = mWin;

            BookInitialization();
        }

        private void BookInitialization()
        {
            WP_Catalog.Children.Clear();
            using (FantasyLibraryDataEntities1 db = new FantasyLibraryDataEntities1())
            {
                var Books = db.Books;

                foreach (Book book in Books)
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

                    Button deleteBookButton = new Button();
                    deleteBookButton.Padding = new Thickness(0);
                    deleteBookButton.Height = 15;
                    deleteBookButton.Width = 15;
                    deleteBookButton.HorizontalAlignment = HorizontalAlignment.Right;
                    deleteBookButton.VerticalAlignment = VerticalAlignment.Top;
                    deleteBookButton.PreviewMouseDown += deleteBookButton_MouseDown;
                    MaterialDesignThemes.Wpf.PackIcon icon = new MaterialDesignThemes.Wpf.PackIcon();
                    icon.Kind = MaterialDesignThemes.Wpf.PackIconKind.Close;
                    icon.Height = 10;
                    icon.Width = 10;
                    icon.Foreground = new SolidColorBrush(Colors.White);
                    deleteBookButton.Content = icon;
                    SP_buttonContent.Children.Add(deleteBookButton);
                    
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
                    buttonBook.Padding = new Thickness(0);
                    buttonBook.Margin = new Thickness(10,5,10,5);
                    buttonBook.Height = 240;
                    buttonBook.Background = Brushes.White;
                    buttonBook.Click += Button_Click;
                    
                    buttonBook.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xC1, 0xC1, 0xC1));
                    buttonBook.BorderBrush = null;
                    buttonBook.Content = SP_buttonContent;
                    WP_Catalog.Children.Add(buttonBook);
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

        private void AddBookButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "FiktionBook(*.fb2)|*.fb2|All files(*.*)|*.*";

            string begin = null;
            string end = @"Books";
            if (openFileDialog.ShowDialog() == true)
            {
                openFileDialog.CheckPathExists = true;
                begin = openFileDialog.FileName;
                string nameFile = begin.Substring(begin.LastIndexOf('\\'), begin.Length - begin.LastIndexOf('\\'));
                end = end + nameFile;
            }

            XDocument xDoc = XDocument.Load(begin);
            FB2File fb2 = new FB2File();
            fb2.Load(xDoc, false);
            string authorName = null;
            string bookName = null;
            string SequenceName = null;
            int SequenceNumber = 1;
            byte[] coverBook;

            bool repeatBook = false;
            bookName = fb2.TitleInfo.BookTitle.Text;
            foreach (var autName in fb2.TitleInfo.BookAuthors)
            {
                authorName = $"{autName.FirstName.Text} {autName.LastName.Text}";
            }
            using (FantasyLibraryDataEntities1 db = new FantasyLibraryDataEntities1())
            {
                var Books = db.Books;
                foreach (Book b in Books)
                {
                    if (bookName == b.BookName)
                    {
                        repeatBook = true;
                        break;
                    }
                }
                var Authors = db.Authors;
                bool repeatAuthor = false;
                Author newAuthor = new Author { AuthorName = authorName };
                foreach (Author a in Authors)
                {
                    if (newAuthor.AuthorName == a.AuthorName)
                    {
                        repeatAuthor = true;
                        break;
                    }
                }
                if (repeatAuthor == false)
                {
                    db.Authors.Add(newAuthor);
                    db.SaveChanges();
                }
                if (repeatBook == false)
                {
                    File.Copy(begin, end, true);
                    foreach (var inf in fb2.TitleInfo.Sequences)
                    {
                        if (inf.Number.HasValue == true)
                        {
                            SequenceNumber = inf.Number.Value;
                        }
                        SequenceName = inf.Name;
                    }
                    coverBook = fb2.Images.First().Value.BinaryData;

                    //MessageBox.Show($"{bookName}\n{authorName}\n{SequenceName}\n{SequenceNumber}\n{end}");

                    Book newBook = new Book
                    {
                        BookName = bookName,
                        AuthorName = authorName,
                        SeriesName = SequenceName,
                        NumberInSeries = SequenceNumber,
                        CoverBook = coverBook,
                        LinkToBook = end
                    };
                    db.Books.Add(newBook);
                    db.SaveChanges();
                    BookInitialization();
                }
                else
                {
                    MessageBox.Show("Данная книга уже храниться в базе данных.");
                }
            }
        }
        //////////////////////////////////////
        private void deleteBookButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Удаление...");
        }
    }
}
