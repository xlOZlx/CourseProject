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

namespace FantasyLibrary
{
    /// <summary>
    /// Логика взаимодействия для BookinfoPage.xaml
    /// </summary>
    public partial class BookinfoPage : Page
    {
        string USER_NAME;
        Book THIS_BOOK;
        public BookinfoPage(Book thisBook, string userName)
        {
            InitializeComponent();
            THIS_BOOK = thisBook;
            USER_NAME = userName;

            NameBookInfo.Text += thisBook.BookName;
            NameAuthorInfo.Text += thisBook.AuthorName;
            SeriesNameInfo.Text += thisBook.SeriesName;
            SeriesNumberInfo.Text += thisBook.NumberInSeries;
            XDocument xDoc = XDocument.Load(thisBook.LinkToBook);
            FB2File fb2 = new FB2File();
            fb2.Load(xDoc, false);
            YearOfPublishinginfo.Text += fb2.PublishInfo.Year.ToString();

            var image = new BitmapImage();
            using (var mem = new MemoryStream(thisBook.CoverBook))
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
            coverBook.Source = image;
            foreach(var p in fb2.TitleInfo.Annotation.Content)
            {
                TextBlock textAnnotation = new TextBlock();
                textAnnotation.FontSize = 14;
                textAnnotation.TextWrapping = TextWrapping.Wrap;
                textAnnotation.Text = $"   {p.ToString()}";
                AnnotationOfBook.Children.Add(textAnnotation);
            }
        }

        private void ButtonReviewSend_Click(object sender, RoutedEventArgs e)
        {
            ListBoxItem listBoxItem = new ListBoxItem();
            StackPanel Review = new StackPanel();
            TextBlock textReview = new TextBlock();
            TextBlock authorReview = new TextBlock();
            authorReview.Text = USER_NAME;
            authorReview.FontWeight = FontWeights.Bold;
            Review.Children.Add(authorReview);
            textReview.TextWrapping = TextWrapping.Wrap;
            TextRange range = new TextRange(RTB.Document.ContentStart, RTB.Document.ContentEnd);
            textReview.Text = range.Text;
            Review.Children.Add(textReview);
            RTB.Document.Blocks.Clear();
            if(AllReview.Items.Count % 2 > 0)
            {
                listBoxItem.Background = new SolidColorBrush(Colors.White);
            }
            listBoxItem.Content = Review;
            AllReview.Items.Insert(0,listBoxItem);
        }

        private void OpenReaderButton_Click(object sender, RoutedEventArgs e)
        {
            BookReader BR = new BookReader(THIS_BOOK);
            BR.Show();
        }
    }
}
