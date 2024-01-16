using MaterialDesignThemes.Wpf;
using PO_Project;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml.Linq;

namespace GUI_PO_Project
{
    /// <summary>
    /// Klasa MainWindow reprezentuje główne okno aplikacji.
    /// </summary>
    public partial class MainWindow : Window
    {

        ListOfUsers listOfUsers;
        private DispatcherTimer timer;

        UserTrainings userTrainings = new UserTrainings();

        // Delegat do aktualizacji interfejsu użytkownika
        private delegate void UpdateUIDelegate();

        // Delegat do odtwarzania dźwięku
        private delegate void PlaySoundDelegate();

        private UpdateUIDelegate updateUIDelegate;
        private PlaySoundDelegate playSoundDelegate;

        // <summary>
        /// Konstruktor klasy MainWindow. Inicjalizuje komponenty okna, uruchamia timer, wczytuje listę użytkowników z pliku XML lub tworzy nową listę.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            // Inicjalizacja timera
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1); // Aktualizacja co sekundę
            timer.Tick += Timer_Tick;

            // Uruchomienie timera
            timer.Start();

            listOfUsers = ListOfUsers.OdczytXml("users.xml") ?? new ListOfUsers();

            if (listOfUsers is object)
            {
                lstUsers.ItemsSource = new ObservableCollection<User>(listOfUsers.Users);
            }

            updateUIDelegate = UpdateUIHandler;
            playSoundDelegate = PlaySound;

        }

        /// <summary>
        /// Właściwość do przechowywania i aktualizacji etykiety z nazwą aktualnie wybranego użytkownika.
        /// </summary>

        public string SelectedUserNameLabel
        {
            get { return (string)GetValue(SelectedUserNameLabelProperty); }
            set { SetValue(SelectedUserNameLabelProperty, value); }
        }

        /// <summary>
        /// Właściwość DependencyProperty reprezentująca etykietę z nazwą aktualnie wybranego użytkownika.
        /// </summary>

        public static readonly DependencyProperty SelectedUserNameLabelProperty =
        DependencyProperty.Register("SelectedUserNameLabel", typeof(string), typeof(MainWindow), new PropertyMetadata("Admin"));


        /// <summary>
        /// Metoda obsługująca aktualizację interfejsu użytkownika.
        /// </summary>
        private void UpdateUIHandler()
        {
      
            lstUsers.Items.Refresh();
      
            MessageBox.Show("Interfejs został zaktualizowany!");
        }

        /// <summary>
        /// Metoda obsługująca odtwarzanie dźwięku.
        /// </summary>
        private void PlaySound()
        {
         
            SystemSounds.Beep.Play();
        }

        /// <summary>
        /// Metoda obsługująca kliknięcie przycisku dźwięku. Symuluje operacje na innym wątku, a następnie aktualizuje interfejs i odtwarza dźwięk.
        /// </summary>
        private void btnSounds_Click(object sender, RoutedEventArgs e)
        {
            // Symulacja wywołania delegatów z poziomu innego wątku (np. gdy dane zostaną zaktualizowane)
            Task.Run(() =>
            {
                // Symulacja czasochłonnej operacji
                System.Threading.Thread.Sleep(600);

                // Wywołaj delegaty w głównym wątku, aby zaktualizować interfejs użytkownika i odtworzyć dźwięk
                Dispatcher.Invoke(updateUIDelegate);
                Dispatcher.Invoke(playSoundDelegate);
                Dispatcher.Invoke(() => ResetInputFields());
            });
        }

        /// <summary>
        /// Metoda resetuje pola wprowadzania danych w interfejsie użytkownika do wartości domyślnych.
        /// </summary>
        private void ResetInputFields()
        {
            
            txtNameExCardio.Text = "Nazwa ćwiczenia";
            txtTime.Text = "Czas";
            txtDistance.Text = "Dystans";
            txtNameExGym.Text = "Nazwa ćwiczenia";
            txtSets.Text = "Serie";
            txtReps.Text = "Powtórzenia";
            txtKilo.Text = "Kilogramy";
        }

        /// <summary>
        /// Metoda obsługuje zdarzenie Tick timera, aktualizując etykiety czasu i daty co sekundę.
        /// </summary>
        private void Timer_Tick(object sender, EventArgs e)
        {
            // Aktualizacja czasu i daty co sekundę
            DateTime nowTime = DateTime.Now;
            lblTime.Content = nowTime.ToLongTimeString();

            DateTime nowDate = DateTime.Now;
            lblDate.Content = nowDate.ToLongDateString();
        }

        /// <summary>
        /// Metoda obsługuje zdarzenie kliknięcia przycisku Dodaj nowego użytkownika.
        /// Tworzy nowego użytkownika i dodaje go do listy zarejestrowanych użytkowników.
        /// </summary>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            User us = new User();
            RegisterWindow rg = new RegisterWindow(us);
            bool? result = rg.ShowDialog();
            if (result == true)
            {
                listOfUsers.RegisterUser(us);
                lstUsers.ItemsSource = new
                                ObservableCollection<User>(listOfUsers.Users);
            }

        }

        /// <summary>
        /// Pobiera lub ustawia wartość określającą, czy aplikacja korzysta z motywu ciemnego.
        /// </summary>
        public bool IsDarkTheme { get; set; }

        /// <summary>
        /// Obiekt pomocniczy do obsługi palety kolorów aplikacji.
        /// </summary>
        private readonly PaletteHelper paletteHelper = new PaletteHelper();

        /// <summary>
        /// Metoda obsługuje zdarzenie kliknięcia przycisku zmiany motywu aplikacji (jasny/ciemny).
        /// </summary>
        private void toggleThem2(object sender, RoutedEventArgs e)  //zmiana motywu/theme
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
        /// Metoda obsługuje zdarzenie kliknięcia przycisku zamknięcia aplikacji.
        /// </summary>

        private void btnexit2_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        /// <summary>
        /// Metoda obsługuje zdarzenie przesunięcia okna za pomocą lewego przycisku myszy.
        /// </summary>
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }


        /// <summary>
        /// Metoda obsługuje zdarzenie kliknięcia przycisku sortowania listy użytkowników.
        /// Sortuje listę użytkowników alfabetycznie według nazwiska.
        /// </summary>
        private void btnSort_Click(object sender, RoutedEventArgs e)
        {
            this.lstUsers.Items.SortDescriptions.Clear();
            this.lstUsers.Items.SortDescriptions.Add(
               new System.ComponentModel.SortDescription(nameof(User.LastName), System.ComponentModel.ListSortDirection.Ascending));
        }

        /// <summary>
        /// Metoda obsługuje zdarzenie kliknięcia przycisku usuwania zaznaczonych użytkowników.
        /// Usuwa zaznaczonych użytkowników z listy zarejestrowanych użytkowników.
        /// </summary>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lstUsers.SelectedIndex > -1)
            {
                foreach (var item in lstUsers.SelectedItems)
                {
                    listOfUsers.DeleteUsers(((User)item));
                }
                lstUsers.ItemsSource = new
                ObservableCollection<User>(listOfUsers.Users);
            }
        }

        // <summary>
        /// Metoda obsługuje zdarzenie kliknięcia przycisku dodawania treningu cardio.
        /// Dodaje nowy trening cardio do zaznaczonego użytkownika.
        /// </summary>
        private void btnAddCardioTraining_Click(object sender, RoutedEventArgs e)
        {

            if (lstUsers.SelectedIndex > -1)
            {
                if (txtNameExCardio.Text != "" || txtTime.Text != "" || txtDistance.Text != "")
                {

                    User selectedUser = (User)lstUsers.SelectedItem;

                    
                    string exerciseName = txtNameExCardio.Text;
                    int time = int.Parse(txtTime.Text);
                    double distance = double.Parse(txtDistance.Text);

                    
                    CardioExercise cardioExercise = new CardioExercise(exerciseName, time, distance, selectedUser);

              
                    selectedUser.UserTrainings ??= new UserTrainings(selectedUser); // Ustaw, jeśli null
                    Training tr = new Training(selectedUser, DateTime.Today, EnumType.Cardio);
                    tr.AddExerciseCardio(cardioExercise);
                    selectedUser.UserTrainings.AddTraining(tr);

                    lstUsers.ItemsSource = new ObservableCollection<User>(listOfUsers.Users);

                    MessageBox.Show("Dodano Trening!");
                    return;
                }
                else
                {
                    MessageBox.Show("Niepoprawne dane treningu!");
                }
            }
        }

        /// <summary>
        /// Metoda obsługuje zdarzenie kliknięcia przycisku dodawania treningu siłowego.
        /// Dodaje nowy trening siłowy do zaznaczonego użytkownika.
        /// </summary>
        private void btnAddGymTraining_Click(object sender, RoutedEventArgs e)
        {
            if (lstUsers.SelectedIndex > -1)
            {
                if (txtNameExCardio.Text != "" || txtTime.Text != "" || txtDistance.Text != "")
                {

                    User selectedUser = (User)lstUsers.SelectedItem;

                   
                    string exerciseName = txtNameExGym.Text;
                    int kilograms = int.Parse(txtKilo.Text);
                    int reps = int.Parse(txtReps.Text);
                    int sets = int.Parse(txtSets.Text);


                  
                    GymExercise gymExercise = new GymExercise(exerciseName, kilograms, reps, sets, selectedUser);

                 
                    selectedUser.UserTrainings ??= new UserTrainings(selectedUser); // Ustaw, jeśli null
                    Training tr = new Training(selectedUser, DateTime.Today, EnumType.Strength);
                    tr.AddExerciseGym(gymExercise);
                    selectedUser.UserTrainings.AddTraining(tr);

                   
                    lstUsers.ItemsSource = new ObservableCollection<User>(listOfUsers.Users);

                    MessageBox.Show("Dodano Trening!");
                    return;
                }
                else
                {
                    MessageBox.Show("Niepoprawne dane treningu!");
                }
            }
        }

        /// <summary>
        /// Metoda obsługuje zdarzenie kliknięcia przycisku wyświetlającego treningi wszystkich zaznaczonych użytkowników.
        /// Wyświetla okno dialogowe z listą treningów dla wybranych użytkowników.
        /// </summary>
        private void AllTrainingsBtn_Click(object sender, RoutedEventArgs e)
        {

            if (lstUsers.SelectedIndex > -1)
            {
                foreach (User item in lstUsers.SelectedItems)
                {
                    UserTrainings userTrainings = item.UserTrainings; 

                    List<Training> trainings = userTrainings?.Trainings;

                    StringBuilder stringBuilder = new StringBuilder();

                    if (trainings != null && trainings.Count > 0)
                    {
                        stringBuilder.AppendLine($"Treningi użytkownika: {item.Name} {item.LastName}");
                        foreach (Training training in trainings)
                        {
                            stringBuilder.AppendLine(training.ToString());
                        }

                        MessageBox.Show(stringBuilder.ToString(), "Treningi użytkownika", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Brak treningów dla wybranego użytkownika.", "Brak treningów", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("Nie wybrano użytkownika.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }


        /// <summary>
        /// Metoda obsługuje zdarzenie kliknięcia przycisku wyświetlającego statystyki zaznaczonych użytkowników.
        /// Wyświetla okno dialogowe z statystykami BMI i całkowitym progresem dla wybranych użytkowników.
        /// </summary>
        private void StatisticsBtn_Click(object sender, RoutedEventArgs e)
        {
            if (lstUsers.SelectedIndex > -1)
            {
                foreach (User item in lstUsers.SelectedItems)
                {

                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.AppendLine($"Statystyki użytkownika: {item.Name} {item.LastName}");
                    stringBuilder.AppendLine($"BMI: {item.BMI(item.Weight, item.Height).ToString("F2")}");
                    stringBuilder.AppendLine($"Total Score: {item.TotalScore().ToString("F2")}");
                    

                    MessageBox.Show(stringBuilder.ToString(), "Statystyki użytkownika", MessageBoxButton.OK, MessageBoxImage.Information);

                }
            }
            else
            {
                MessageBox.Show("Nie wybrano użytkownika.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        /// <summary>
        /// Metoda obsługuje zdarzenie kliknięcia przycisku wyświetlającego ranking zaznaczonych użytkowników.
        /// Wyświetla okno dialogowe z rankingiem postępów użytkowników.
        /// </summary>
        private void btnRanking_Click(object sender, RoutedEventArgs e)
        {
           
            UsersRanking usersRanking = new UsersRanking();

            List<User> selectedUsers = lstUsers.SelectedItems.Cast<User>().ToList();

            List<UserTrainings> selectedUserTrainings = selectedUsers
                .Select(user => new UserTrainings(user))
                .ToList();

            usersRanking.Ranking = selectedUserTrainings;

            usersRanking.CalculateAndSortTotalProgress();

            StringBuilder rankingStringBuilder = new StringBuilder();
            foreach (var userTrainings in usersRanking.Ranking)
            {
                rankingStringBuilder.AppendLine(userTrainings.GetRankingInfo());
            }

            MessageBox.Show(rankingStringBuilder.ToString(), "Ranking");
        }

        /// <summary>
        /// Metoda obsługuje zdarzenie kliknięcia przycisku zapisu treningów zaznaczonego użytkownika do pliku.
        /// Zapisuje treningi użytkownika do pliku tekstowego na pulpicie.
        /// </summary>
        private void btnSaveFIle_Click(object sender, RoutedEventArgs e) 
        {
            if (lstUsers.SelectedIndex > -1)
            {
                User selectedUser = (User)lstUsers.SelectedItem;

                if (selectedUser.UserTrainings != null && selectedUser.UserTrainings.Trainings.Count > 0)
                {
                    try
                    {
                        
                        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                       
                        string fileName = $"{selectedUser.Name}_{selectedUser.LastName}_Trainings.txt";

                        string filePathOnDesktop = System.IO.Path.Combine(desktopPath, fileName);

                        using (StreamWriter file = new StreamWriter(filePathOnDesktop))
                        {
                            file.WriteLine($"Treningi użytkownika: {selectedUser.Name} {selectedUser.LastName}");
                            file.WriteLine();

                            foreach (Training training in selectedUser.UserTrainings.Trainings)
                            {
                                file.WriteLine(training.ToString());
                                file.WriteLine();
                            }
                        }

                        MessageBox.Show($"Treningi zostały zapisane do pliku: {filePathOnDesktop} \nLokalizacja: Pulpit", "Zapisano", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Wystąpił błąd podczas zapisu pliku: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Brak treningów dla wybranego użytkownika.", "Brak treningów", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Nie wybrano użytkownika.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Obsługuje zdarzenie zmiany zaznaczenia użytkownika na liście, aktualizując etykietę z nazwą użytkownika.
        /// </summary>
        private void lstUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstUsers.SelectedIndex > -1)
            {
                if (txtNameExCardio.Text != "" || txtTime.Text != "" || txtDistance.Text != "")
                {

                    User selectedUser = (User)lstUsers.SelectedItem;
                    SelectedUserNameLabel = $"{selectedUser.Name} {selectedUser.LastName}";
                }
            }
        }
    }
}