using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Media;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using MaterialDesignThemes.Wpf;
using PO_Project;

namespace GUI_PO_Project
{
    /// <summary>
    /// Klasa StartWindow reprezentuje okno aplikacji służące do logowania.
    /// </summary>
    public partial class StartWindow : Window
    {

        ListOfUsers listOfUsers = new ListOfUsers();

        /// <summary>
        /// Konstruktor inicjalizuje nowe wystąpienie klasy <see cref="StartWindow"/>.
        /// </summary>
        public StartWindow()
        {
            InitializeComponent();

        }

        /// <summary>
        /// Właściwość pobiera lub ustawia wartość określającą, czy aplikacja korzysta z motywu ciemnego.
        /// </summary>
        public bool IsDarkTheme { get; set; }

        /// <summary>
        /// Obiekt pomocniczy do obsługi palety kolorów aplikacji.
        /// </summary>
        private readonly PaletteHelper paletteHelper = new PaletteHelper();

        /// <summary>
        /// Metoda obsługuje zdarzenie kliknięcia przycisku do zmiany kolorów aplikacji.
        /// </summary>
        private void toggleTheme(object sender, RoutedEventArgs e) 
        {
            if (OperatingSystem.IsWindows()) //sprawdzenie czy system jest WIndows
            {
                ITheme theme = paletteHelper.GetTheme();

                if (IsDarkTheme = theme.GetBaseTheme() == BaseTheme.Dark)
                {
                    IsDarkTheme = false;
                    theme.SetBaseTheme(Theme.Light);
                }
                else
                {
                    IsDarkTheme = true;
                    theme.SetBaseTheme(Theme.Dark);
                }
                paletteHelper.SetTheme(theme); //ustawia dla całej aplikacji
            }
        }

        /// <summary>
        /// Metoda obsługuje zdarzenie kliknięcia przycisku do wyłączenia aplikacji.
        /// </summary>
        private void exitApp(object sender, RoutedEventArgs e) //przycisk do wyłączenia aplikacji
        {
            System.Windows.Application.Current.Shutdown();
        }

        /// <summary>
        /// Metoda obsługuje zdarzenie przesunięcia okna za pomocą lewego przycisku myszy.
        /// </summary>
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e) //przesuwanie okna za pomocą lewego przycisku
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }

        /// <summary>
        /// Metoda obsługuje zdarzenie kliknięcia przycisku do logowania.
        /// </summary>
        private void LoginBtn_Click(object sender, RoutedEventArgs e) 
        {
            string enteredUsername = txtUsername.Text;
            string enteredPassword = txtPassword.Password;

            if (string.IsNullOrEmpty(enteredUsername) || string.IsNullOrEmpty(enteredPassword))   
            {
                MessageBox.Show("Niepoprawny login lub hasło");
                return;
            }

            if (enteredUsername == "admin" && enteredPassword == "admin")
            {

                MessageBox.Show("Zalogowano pomyślnie!");

                
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Niepoprawny login lub hasło");
            }

        }
    }
}
                 