using System.Windows;
using static WpfApp6.MainWindow;

namespace WpfApp6
{
    public partial class EditPersonWindow : Window
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public MainWindow.EducationLevel Education { get; set; }

        public EditPersonWindow(Person person)
        {
            InitializeComponent();

            txtEditFirstName.Text = person.FirstName;
            txtEditLastName.Text = person.LastName;
            cmbEditEducation.SelectedIndex = (int)person.Education;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            FirstName = txtEditFirstName.Text;
            LastName = txtEditLastName.Text;
            Education = (MainWindow.EducationLevel)cmbEditEducation.SelectedIndex;

            DialogResult = true;
            Close();
        }
    }
}
