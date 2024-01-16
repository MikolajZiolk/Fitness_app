using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PO_Project
{
    public abstract class Exercise
    {

        /// <summary>
        /// Abstrakcyjna klasa reprezentuje pojedyncze ćwiczenie, przechowując informacje o nazwie ćwiczenia oraz użytkowniku, który je wykonał.
        /// </summary>

        string exerciseName;
        User user;

        /// <summary>
        /// Właściwość pobiera lub ustawia nazwę ćwiczenia, nazwa musi zawierać tylko litery i mieć co najmniej 3 znaki.
        /// </summary>
        public string ExerciseName { get => exerciseName;
            init
            {
                // Wyrażenie regularne sprawdzające, czy nazwisko zawiera tylko litery
                if (!Regex.IsMatch(value, @"^[a-zA-Z ]+$") || value.Length < 3)
                {
                    throw new WrongData();
                }

                exerciseName = value;
            }
        }

        /// <summary>
        /// Właściwość pobiera lub ustawia użytkownika, który wykonał ćwiczenie.
        /// </summary>
        public User User { get => user; set => user = value; }


        /// <summary>
        /// Konstruktor nieparametryczny, tworzy obiekt ćwiczenia.
        /// </summary>
        public Exercise()
        {
            
        }

        /// <summary>
        ///  Metoda wirtualna, oblicza wynik ćwiczenia. Domyślnie zwraca 1, jest przesłaniana w klasach dziedziczących.
        /// </summary>
        /// <returns></returns>
        public virtual double ExerciseScore()
        {
            return 1;
        }

        /// <summary>
        /// Konstruktor parametryczny, inicjalizuje ćwiczenie z określoną nazwą i użytkownikiem.
        /// </summary>
        /// <param name="exerciseName"></param>
        /// <param name="user"></param>
        public Exercise(string exerciseName, User user)
        {
            User = user;
            ExerciseName = exerciseName;
        }

        /// <summary>
        /// Przesłonięta metoda ToString, zwraca nazwę ćwiczenia.
        /// </summary>
        /// <returns></returns>

        public override string ToString()
        {
            return $"{exerciseName}";
        }

       

        
    }
}
