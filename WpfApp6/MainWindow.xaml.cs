using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp6
{
    public partial class MainWindow : Window
    {
        public enum Class
        {
            Pierwsza = 0,
            Druga = 1,
            Trzecia = 2,
            Czwarta = 3,
        }

        public class Book
        {
            public string Title { get; set; }
            public string Author { get; set; }
            public int Class { get; set; }

            public override string ToString()
            {
                return $"{Title}, {Author}, {Class}";
            }
        }

        public ObservableCollection<Book> Books { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Books = new ObservableCollection<Book>();
            lstBooks.ItemsSource = Books;
            LoadDataFromFile("C:\\Users\\Admin\\Source\\Repos\\WpfApp6\\WpfApp6\\BooksData.txt");
        }

        private void LoadDataFromFile(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    using (StreamReader sr = new StreamReader(filePath))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            string[] parts = line.Split(',');

                            if (parts.Length == 3)
                            {
                                string title = parts[0];
                                string author = parts[1];

                                if (int.TryParse(parts[2], out int classValue))
                                {
                                    Books.Add(new Book
                                    {
                                        Title = title,
                                        Author = author,
                                        Class = classValue
                                    });
                                }
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Plik nie istnieje.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas wczytywania danych z pliku: {ex.Message}");
            }
        }

        private void EditBook_Click(object sender, RoutedEventArgs e)
        {
            if (lstBooks.SelectedItem != null)
            {
                Book selectedBook = (Book)lstBooks.SelectedItem;
                EditBookWindow editWindow = new EditBookWindow(selectedBook);

                if (editWindow.ShowDialog() == true)
                {
                    selectedBook.Title = editWindow.Title;
                    selectedBook.Author = editWindow.Author;
                    selectedBook.Class = (int)editWindow.Class;

                    lstBooks.Items.Refresh();
                }
            }
        }

        private void AddBook_Click(object sender, RoutedEventArgs e)
        {
            EditBookWindow addWindow = new EditBookWindow(new Book());

            if (addWindow.ShowDialog() == true)
            {
                string title = addWindow.Title;
                string author = addWindow.Author;
                int classValue = (int)addWindow.Class;

                Book newBook = new Book
                {
                    Title = title,
                    Author = author,
                    Class = classValue
                };

                Books.Add(newBook);
            }
        }

        private void RemoveBook_Click(object sender, RoutedEventArgs e)
        {
            if (lstBooks.SelectedItem != null)
            {
                Books.Remove((Book)lstBooks.SelectedItem);
            }
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            LoadDataFromFile();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveDataToFile("C:\\Users\\Admin\\Desktop\\BooksData.txt");
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void LoadDataFromFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openFileDialog.Filter = "Pliki tekstowe (*.txt)|*.txt|Wszystkie pliki (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    using (StreamReader sr = new StreamReader(openFileDialog.FileName))
                    {
                        Books.Clear();

                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            string[] parts = line.Split(',');

                            if (parts.Length == 3)
                            {
                                string title = parts[0];
                                string author = parts[1];

                                if (int.TryParse(parts[2], out int classValue))
                                {
                                    Books.Add(new Book
                                    {
                                        Title = title,
                                        Author = author,
                                        Class = classValue
                                    });
                                }
                            }
                        }

                        MessageBox.Show("Dane zostały wczytane z pliku.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Błąd podczas wczytywania danych z pliku: {ex.Message}");
                }
            }
        }

        private void SaveDataToFile(string filePath)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8))
                {
                    foreach (Book book in Books)
                    {
                        sw.WriteLine($"{book.Title},{book.Author},{book.Class}");
                    }
                }
                MessageBox.Show("Dane zostały zapisane do pliku.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas zapisywania danych do pliku: {ex.Message}");
            }
        }
    }
}
