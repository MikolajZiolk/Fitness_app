using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO_Project
{

    public class CardioExercise : Exercise 
    {
        /// <summary>
        /// Klasa CardioExercise dziedziczy po klasie Exercise i reprezentuje ćwiczenie kardio.
        /// Przechowuje informacje o czasie trwania ćwiczenia (w minutach) oraz odległości pokonanej podczas ćwiczenia (w kilometrach). 
        /// </summary>
        int time;
        double distance;

        /// <summary>
        /// Właściwość pobiera lub ustawia czas trwania ćwiczenia(w minutach), czas musi być większy niż 0 i nie większy niż 1000 minut.
        /// </summary>
        public int Time
        {
            get => time;
            init
            {
                if (value <= 0 || value > 1000)
                {
                    throw new WrongData();
                }
                time = value;
            }
        }

        /// <summary>
        /// Właściwość pobiera lub ustawia odległość pokonaną podczas ćwiczenia(w kilometrach), odległość musi być większa niż 0 i nie większa niż 1000 kilometrów.
        /// </summary>
        public double Distance
        {
            get => distance;
            init
            {
                if (value <= 0 || value > 1000)
                {
                    throw new WrongData();
                }
                distance = value;
            }
        }

        /// <summary>
        /// Konstruktor parametryczny, inicjalizuje ćwiczenie kardio z określoną nazwą, czasem trwania, odległością, oraz użytkownikiem.
        /// </summary>
        /// <param name="exerciseName"></param>
        /// <param name="time"></param>
        /// <param name="distance"></param>
        /// <param name="user"></param>
        public CardioExercise(string exerciseName, int time, double distance, User user) : base(exerciseName, user)
        {
            Time = time;
            Distance = distance;
        }

        /// <summary>
        /// Metoda MeanPace oblicza średnie tempo treningu na podstawie jego czasu trwania i przebytej odległości.
        /// </summary>
        /// <param name="time"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        public double MeanPace(int time, double distance) => time / distance; //srednie tempo

        /// <summary>
        /// Metoda oblicza liczbę spalonych kalorii podczas ćwiczenia kardio, uwzględniając przy tym płeć użytkownika, wartość MET i wagę użytkownika.
        /// </summary>
        /// <param name="timeInMinutes"></param>
        /// <param name="distanceInKilometers"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public double CalculateCaloriesBurned(double timeInMinutes, double distanceInKilometers, User user) 
        {
            // Przykładowy wzór na obliczanie spalonych kalorii (Harris-Benedict)
            double caloriesPerMinute = (user.Gender == EnumGender.Male) ? 0.035 : 0.045;
          
            double metabolicEquivalent = 8.0; // Wartość MET dla intensywnego treningu kardio

            double caloriesBurned = (caloriesPerMinute * metabolicEquivalent * user.Weight) * distanceInKilometers;

            return caloriesBurned;
        }

        /// <summary>
        /// Przesłonięta metoda, oblicza wynik ćwiczenia kardio na podstawie odległości, czasu trwania i ilości spalonych kalorii.
        /// </summary>
        /// <returns></returns>
        public override double ExerciseScore()
        {
            // Uwzględnienie różnych wag dla różnych kryteriów
            double cardioScore = 0.4 * Distance + 0.4 * Time + 0.2 * CalculateCaloriesBurned(this.time, this.distance, User);

            return cardioScore;
        }

        /// <summary>
        /// Przesłonięta metoda ToString, informację o ćwiczeniu kardio: nazwę ćwiczenia, czas trwania, odległość, średnie tempo, ilość spalonych kalorii oraz wynik ćwiczenia.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            
            return base.ToString() + " " +"Czas: " +this.time + ", " + "Dystans: " + this.distance + ", " + "Średnie tempo: "+MeanPace(this.time, this.distance).ToString("F2") + "\n" +
               "Spalone kalorie: " + CalculateCaloriesBurned(this.time, this.distance, User).ToString("F2") + " \n" +
               "Progres: "+ ExerciseScore().ToString("F2");
        }

        
    }
}
