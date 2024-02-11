using System.Windows;
using static WpfApp6.MainWindow;

namespace WpfApp6
{
    public partial class EditBookWindow : Window
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public MainWindow.Class Class { get; set; }

        public EditBookWindow(Book book)
        {
            InitializeComponent();

            txtTitle.Text = book.Title;
            txtAuthor.Text = book.Author;
            cmbClass.SelectedIndex = (int)book.Class;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Title = txtTitle.Text;
            Author = txtAuthor.Text;
            Class = (MainWindow.Class)cmbClass.SelectedIndex;

            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
