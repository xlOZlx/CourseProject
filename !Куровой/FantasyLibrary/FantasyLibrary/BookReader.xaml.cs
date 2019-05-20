using FB2Library;
using FB2Library.Elements;
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
using System.Windows.Shapes;
using System.Xml.Linq;

namespace FantasyLibrary
{
    /// <summary>
    /// Логика взаимодействия для BookReader.xaml
    /// </summary>
    public partial class BookReader : Window
    {
        Dictionary<string, StackPanel> ContentBook;

        public BookReader(Book thisBook)
        {
            InitializeComponent();

            NameBookInReader.Text = $"{thisBook.AuthorName} - {thisBook.BookName}";

            Dictionary<string, StackPanel> contentBook = new Dictionary<string, StackPanel>();
            XDocument xDoc = XDocument.Load(thisBook.LinkToBook);
            FB2File fb2 = new FB2File();
            fb2.Load(xDoc, false);

            foreach (var body in fb2.Bodies)
            {
                TreeViewItem itemBody = new TreeViewItem();
                itemBody.Selected += TreeViewItem_Selected;
                string buff = "";
                StackPanel SP_Body = new StackPanel();
                byte[] cover = fb2.Images.First().Value.BinaryData;
                var image = new BitmapImage();
                using (var mem = new MemoryStream(cover))
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
                Image coverImg = new Image();
                coverImg.Source = image;
                coverImg.Width = image.Width;
                coverImg.Height = image.Height;
                coverImg.HorizontalAlignment = HorizontalAlignment.Center;
                SP_Body.Children.Add(coverImg);

                if (body.Title != null)
                {
                    foreach (var p in body.Title.TitleData)
                    {
                        buff += p;
                        itemBody.Header = buff;
                        SP_Body.Children.Add(new TextBlock
                        {
                            Text = p.ToString(),
                            FontSize = 24,
                            FontWeight = FontWeights.Bold,
                            HorizontalAlignment = HorizontalAlignment.Center,
                            TextWrapping = TextWrapping.WrapWithOverflow
                        });
                    }
                }

                contentBook.Add(buff, SP_Body);

                TV_ContentBook.Items.Add(itemBody);

                foreach (var sect in body.Sections)
                {
                    TreeViewItem TVI_partBook = new TreeViewItem();
                    if (sect.Title != null)
                    {
                        TVI_partBook.Header = sect.Title;
                    }
                    else
                    {
                        TVI_partBook.Header = "    ";
                    }
                    Console.WriteLine();
                    if (sect.SubSections.Count != 0)
                    {
                        int i = 1;

                        foreach (var sect2 in sect.SubSections)
                        {
                            TreeViewItem TVI_chapterBook = new TreeViewItem();
                            TVI_chapterBook.Selected += TreeViewItem_Selected;
                            if (sect2.Title != null)
                            {
                                TVI_chapterBook.Header = sect2.Title;
                            }
                            else
                            {
                                TVI_chapterBook.Header = sect.Title + $"{i}";
                                i++;
                            }
                            StackPanel chapterBook = new StackPanel();
                            foreach (var p in sect2.Content)
                            {
                                if (p.GetType() == typeof(ImageItem))
                                {
                                    ImageItem II_InText = (ImageItem)p;
                                    Image imageInText = new Image();
                                    foreach (var im in fb2.Images)
                                    {
                                        if (II_InText.ID == im.Key)
                                        {
                                            byte[] imageByte = im.Value.BinaryData;
                                            var imageSource = new BitmapImage();
                                            using (var mem = new MemoryStream(imageByte))
                                            {
                                                mem.Position = 0;
                                                imageSource.BeginInit();
                                                imageSource.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                                                imageSource.CacheOption = BitmapCacheOption.OnLoad;
                                                imageSource.UriSource = null;
                                                imageSource.StreamSource = mem;
                                                imageSource.EndInit();
                                            }
                                            imageSource.Freeze();
                                            imageInText.Source = imageSource;
                                            imageInText.Height = imageSource.Height;
                                            imageInText.Width = imageSource.Width;
                                            imageInText.HorizontalAlignment = HorizontalAlignment.Center;
                                            chapterBook.Children.Add(imageInText);
                                        }
                                    }
                                }
                                if (p.GetType() == typeof(CiteItem))
                                {
                                    CiteItem citeItem = (CiteItem)p;
                                    foreach (var el in citeItem.CiteData)
                                    {
                                        chapterBook.Children.Add(new TextBlock
                                        {
                                            Text = "        " + el.ToString(),
                                            FontSize = 16,
                                            FontStyle = FontStyles.Italic,
                                            Margin = new Thickness(15, 0, 0, 0),
                                            HorizontalAlignment = HorizontalAlignment.Left,
                                            TextWrapping = TextWrapping.WrapWithOverflow
                                        });
                                    }
                                }
                                chapterBook.Children.Add(new TextBlock
                                {
                                    Text = "        " + p.ToString(),
                                    FontSize = 16,
                                    Margin = new Thickness(15, 5, 10, 7),
                                    HorizontalAlignment = HorizontalAlignment.Left,
                                    TextWrapping = TextWrapping.WrapWithOverflow
                                });
                            }
                            if (sect2.Title == null)
                            {
                                contentBook.Add(sect.Title + $"{i}", chapterBook);
                            }
                            else
                            {
                                contentBook.Add(sect.Title.ToString() + " " + sect2.Title.ToString(), chapterBook);
                            }
                            TVI_partBook.Items.Add(TVI_chapterBook);
                        }

                    }
                    else
                    {
                        TVI_partBook.Selected += TreeViewItem_Selected;
                        StackPanel partbook = new StackPanel();
                        foreach (var p in sect.Content)
                        {
                            if (sect.Epigraphs != null)
                            {
                                foreach (var epig in sect.Epigraphs)
                                {
                                    foreach (var e in epig.EpigraphData)
                                    {
                                        partbook.Children.Add(new TextBlock
                                        {
                                            Text = e.ToString(),
                                            FontSize = 16,
                                            HorizontalAlignment = HorizontalAlignment.Center,
                                            TextWrapping = TextWrapping.WrapWithOverflow
                                        });
                                    }
                                }
                            }
                            if (p.GetType() == typeof(ImageItem))
                            {
                                ImageItem II_InText = (ImageItem)p;
                                Image imageInText = new Image();
                                foreach (var im in fb2.Images)
                                {
                                    if (II_InText.ID == im.Key)
                                    {
                                        byte[] imageByte = im.Value.BinaryData;
                                        var imageSource = new BitmapImage();
                                        using (var mem = new MemoryStream(imageByte))
                                        {
                                            mem.Position = 0;
                                            imageSource.BeginInit();
                                            imageSource.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                                            imageSource.CacheOption = BitmapCacheOption.OnLoad;
                                            imageSource.UriSource = null;
                                            imageSource.StreamSource = mem;
                                            imageSource.EndInit();
                                        }
                                        imageSource.Freeze();
                                        imageInText.Source = imageSource;
                                        imageInText.Height = imageSource.Height;
                                        imageInText.Width = imageSource.Width;
                                        imageInText.HorizontalAlignment = HorizontalAlignment.Center;
                                        partbook.Children.Add(imageInText);
                                    }
                                }
                                continue;
                            }
                            if (p.GetType() == typeof(CiteItem))
                            {
                                CiteItem citeItem = (CiteItem)p;
                                foreach (var el in citeItem.CiteData)
                                {
                                    partbook.Children.Add(new TextBlock
                                    {
                                        Text = "        " + el.ToString(),
                                        FontSize = 16,
                                        FontStyle = FontStyles.Italic,
                                        Margin = new Thickness(15, 0, 0, 0),
                                        HorizontalAlignment = HorizontalAlignment.Left,
                                        TextWrapping = TextWrapping.WrapWithOverflow
                                    });
                                }
                                continue;
                            }
                            partbook.Children.Add(new TextBlock
                            {
                                Text = "        " + p.ToString(),
                                FontSize = 16,
                                Margin = new Thickness(10, 5, 10, 5),
                                HorizontalAlignment = HorizontalAlignment.Left,
                                TextWrapping = TextWrapping.WrapWithOverflow
                            });
                        }
                        if (sect.Title != null)
                        {
                            contentBook.Add(sect.Title.ToString(), partbook);
                        }
                        else
                        {
                            contentBook.Add("    ", partbook);
                        }

                    }
                    TV_ContentBook.Items.Add(TVI_partBook);
                }
            }
            ContentBook = contentBook;
        }

        private void CloseWindowButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TreeViewItem_Selected(object sender, RoutedEventArgs e)
        {
            TreeViewItem TVI = (TreeViewItem)sender;
            StackPanel buffBlock = new StackPanel();

            var thisParentItem = TVI.Parent;
            if (thisParentItem.GetType() == typeof(TreeViewItem))
            {
                TreeViewItem parent = (TreeViewItem)thisParentItem;
                string PartCheck = parent.Header.ToString() + " ";
                string check = PartCheck + TVI.Header.ToString();
                foreach (var cont in ContentBook)
                {
                    if (check == cont.Key)
                    {
                        buffBlock = cont.Value;
                        FormForBook.Children.Clear();
                        FormForBook.Children.Add(buffBlock);
                    }
                }
                SVForBook.ScrollToHome();
            }
            else
            {
                string check = TVI.Header.ToString();
                foreach (var cont in ContentBook)
                {
                    if (check == cont.Key)
                    {
                        buffBlock = cont.Value;
                        FormForBook.Children.Clear();
                        FormForBook.Children.Add(buffBlock);
                    }
                }
                SVForBook.ScrollToHome();
            }
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

        private void FullScreenCloseButton_Click(object sender, RoutedEventArgs e)
        {
            FullScreenOpenButton.Visibility = Visibility.Visible;
            FullScreenCloseButton.Visibility = Visibility.Collapsed;
            this.WindowState = WindowState.Normal;
        }

        private void FullScreenOpenButton_Click(object sender, RoutedEventArgs e)
        {
            FullScreenOpenButton.Visibility = Visibility.Collapsed;
            FullScreenCloseButton.Visibility = Visibility.Visible;
            this.WindowState = WindowState.Maximized;
        }
    }
}
