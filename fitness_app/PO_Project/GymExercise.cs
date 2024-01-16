using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO_Project
{
    public class GymExercise : Exercise
    {
        /// <summary>
        /// Klasa GymExercise dziedziczy po klasie Exercise i reprezentuje ćwiczenie wykonywane na siłowni. 
        /// Przechowuje informacje o ilości kilogramów, liczbie powtórzeń (reps) oraz liczbie serii (sets).
        /// </summary>
        int kilograms;
        int reps;
        int sets;

        /// <summary>
        /// Właściwość pobiera lub ustawia ilość kilogramów użytych podczas ćwiczenia, ilość kilogramów musi być większa niż 0 i nie większa niż 500.
        /// </summary>
        public int Kilograms { get => kilograms;
            init
            {
                if (value <= 0 || value > 500)
                {
                    throw new WrongData();
                }
                kilograms = value;
            }
        }

        /// <summary>
        /// Właściwość pobiera lub ustawia liczbę powtórzeń podczas ćwiczenia, liczba powtórzeń musi być większa niż 0 i nie większa niż 500.
        /// </summary>
        public int Reps { get => reps;
            init
            {
                if (value <= 0 || value > 500)
                {
                    throw new WrongData();
                }
                reps = value;
            }
        }

        /// <summary>
        /// Właściwość pobiera lub ustawia liczbę serii podczas ćwiczenia, liczba serii musi być większa niż 0 i nie większa niż 300.
        /// </summary>
        public int Sets { get => sets;
            init
            {
                if (value <= 0 || value > 300)
                {
                    throw new WrongData();
                }
                sets = value;
            }
        }

        /// <summary>
        /// Konstruktor nieparametryczny, tworzy obiekt ćwiczenia na siłowni.
        /// </summary>
        public GymExercise() {}

        /// <summary>
        /// Konstruktor parametryczny, inicjalizuje ćwiczenie na siłowni z określoną nazwą, ilością kilogramów, liczbą powtórzeń, liczbą serii oraz użytkownikiem.
        /// </summary>
        /// <param name="exerciseName"></param>
        /// <param name="kilograms"></param>
        /// <param name="reps"></param>
        /// <param name="sets"></param>
        /// <param name="user"></param>
        public GymExercise(string exerciseName, int kilograms, int reps, int sets, User user) : base(exerciseName, user) //czy jest potrzebna nazwa cwiczenia
        {
            Kilograms = kilograms;
            Reps = reps;
            Sets = sets;
        }

        /// <summary>
        /// Przesłonięta metoda, oblicza wynik ćwiczenia na siłowni, uwzględniając różne wagi dla kilogramów, liczby powtórzeń i liczby serii.
        /// </summary>
        /// <returns></returns>
        public override double ExerciseScore()
        {

            // Uwzględnienie różnych wag dla różnych kryteriów
            double strengthScore = 0.4 * Kilograms + 0.3 * Reps + 0.3 * Sets;  

            return strengthScore;
        }

        /// <summary>
        /// Przesłonięta metoda ToString, zwraca informację o ćwiczeniu na siłowni: nazwę ćwiczenia, ilość kilogramów, liczbę powtórzeń, liczbę serii oraz wynik ćwiczenia.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString() + "kilogramy: " + this.kilograms + ",Powtórzenia: " + this.reps + ",Serie: " + this.sets + " \n" + "Progres: "+ExerciseScore().ToString("F2");
        }

     


    }
}
