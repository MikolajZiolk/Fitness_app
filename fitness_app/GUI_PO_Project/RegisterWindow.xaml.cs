using PO_Project;
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
using System.Windows.Shapes;

namespace GUI_PO_Project
{
    /// <summary>
    /// Klasa RegisterWindow reprezentuje okno rejestracji użytkownika.
    /// </summary>
    public partial class RegisterWindow : Window
    {
        User user = new User();

        /// <summary>
        /// Konstruktor domyślny inicjalizuje nowe okno rejestracji.
        /// </summary>
        public RegisterWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Konstruktor parametryczny inicjalizuje nowe okno rejestracji z podanym użytkownikiem.
        /// </summary>
        /// <param name="us"></param>
        public RegisterWindow(User us):this()
        {
            user = us;
        }

        /// <summary>
        /// Metoda obsługuje zdarzenie kliknięcia przycisku "Zapisz". Sprawdza poprawność wprowadzonych danych i zamyka okno rejestracji.
        /// </summary>
        private void btnZapisz_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (NameTextBox.Text != "" && SurnameTextBox.Text != "")
                {
                    user.Name = NameTextBox.Text;
                    user.LastName = SurnameTextBox.Text;
                    user.Weight = double.Parse(WeighTextBox.Text); // Upewnij się, że wartość jest poprawna
                    user.Height = double.Parse(Heigh_TextBox.Text); // Upewnij się, że wartość jest poprawna
                    user.Gender = GetSelectedGender();


                    DialogResult = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Proszę wprowadzić poprawne dane.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Podano niepoprawny format danych.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }

        /// <summary>
        /// Metoda pobiera wybraną płeć z RadioButtonów.
        /// </summary>
        /// <returns></returns>
        private EnumGender GetSelectedGender()
        {
            if (RBFemale.IsChecked == true)
            {
                return EnumGender.Female;
            }
            else if (RBMale.IsChecked == true)
            {
                return EnumGender.Male;
            }
            else
            {
               
                return EnumGender.Female; 
            }
        }

        /// <summary>
        /// Metoda obsługuje zdarzenie kliknięcia przycisku "Zamknij".
        /// </summary>
        private void btnExit22_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
