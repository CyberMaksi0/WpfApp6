using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp6
{
    public partial class MainWindow : Window
    {

        public enum EducationLevel
        {
            Podstawowe = 0,
            Średnie = 1,
            Wyższe = 2,
            Profesor_Nadzwyczajny = 3,
        }

        public class Person
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public EducationLevel Education { get; set; }

            public override string ToString()
            {
                return $"{FirstName} {LastName} - {Education}";
            }
        }

        public ObservableCollection<Person> Persons { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Persons = new ObservableCollection<Person>();
            lstPersons.ItemsSource = Persons;
            LoadDataFromFile("C:\\Users\\glowacki.maksymilian\\source\\repos\\WpfApp6\\WpfApp6\\PersonsData.txt");
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
                                string firstName = parts[0];
                                string lastName = parts[1];

                                if (int.TryParse(parts[2], out int educationValue))
                                {
                                    EducationLevel education = (EducationLevel)educationValue;

                                    Persons.Add(new Person
                                    {
                                        FirstName = firstName,
                                        LastName = lastName,
                                        Education = education
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
        private void EditPerson_Click(object sender, RoutedEventArgs e)
        {
            if (lstPersons.SelectedItem != null)
            {
                Person selectedPerson = (Person)lstPersons.SelectedItem;

                EditPersonWindow editWindow = new EditPersonWindow(selectedPerson);

                if (editWindow.ShowDialog() == true)
                {
                    selectedPerson.FirstName = editWindow.FirstName;
                    selectedPerson.LastName = editWindow.LastName;
                    selectedPerson.Education = editWindow.Education;

                    lstPersons.Items.Refresh();
                }
            }
        }

        private void AddPerson_Click(object sender, RoutedEventArgs e)
        {
            string firstName = txtFirstName.Text;
            string lastName = txtLastName.Text;

            if (cmbEducation.SelectedItem != null && cmbEducation.SelectedItem is ComboBoxItem selectedItem)
            {
                if (int.TryParse(selectedItem.Tag.ToString(), out int educationValue))
                {
                    EducationLevel education = (EducationLevel)educationValue;

                    Person newPerson = new Person
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        Education = education
                    };

                    Persons.Add(newPerson);
                    txtFirstName.Clear();
                    txtLastName.Clear();
                    cmbEducation.SelectedIndex = -1;
                }
            }
        }


        private void RemovePerson_Click(object sender, RoutedEventArgs e)
        {
            if (lstPersons.SelectedItem != null)
            {
                Persons.Remove((Person)lstPersons.SelectedItem);
            }
        }
    }
}