using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace PO_Project
{

   public enum EnumType { Cardio, Strength} 
    public class Training : ITrainingManagement
    {
        /// <summary>
        /// Klasa Training reprezentuje sesję treningową, zawierając informacje o użytkowniku, dacie treningu, typie treningu oraz liście ćwiczeń wykonanych podczas tego treningu.
        /// </summary>
        User user;
        DateTime trainingDate;
        EnumType type;
        List<Exercise> exercises;

        /// <summary>
        /// Właściwość pobiera lub ustawia użytkownika powiązanego z treningiem.
        /// </summary>
        public virtual User User { get => user; set => user = value; }

        /// <summary>
        /// Właściwość pobiera lub ustawia datę treningu, która ma się mieścić w zakresie od 1 stycznia 1999 roku do aktualnej daty.
        /// </summary>
        public DateTime TrainingDate { get => trainingDate;
            init
            {
                DateTime minDate = new DateTime(1999, 1, 1);
                DateTime maxDate = DateTime.Now;

                // Sprawdzenie, czy data mieści się w zakresie
                if (value < minDate || value > maxDate)
                {
                    throw new WrongData();
                }

                trainingDate = value;
            }
        }

        /// <summary>
        /// Właściwość pobiera lub ustawia typ treningu.
        /// </summary>
        public EnumType Type { get => type; set => type = value; }

        /// <summary>
        /// Właściwość pobiera lub ustawia listę ćwiczeń związanych z treningiem.
        /// </summary>
        public List<Exercise> Exercises { get => exercises; set => exercises = value; }


        /// <summary>
        /// Konstruktor nieparametryczny inicjalizujący listę ćwiczeń.
        /// </summary>
        public Training()
        {
            Exercises = new List<Exercise>();
           
        }

        /// <summary>
        /// Konstruktor przyjmujący użytkownika, datę treningu i rodzaj treningu, inicjalizujący również listę ćwiczeń.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="trainingDate"></param>
        /// <param name="type"></param>
        public Training(User user, DateTime trainingDate,EnumType type):this()
        {
            User = user;
            TrainingDate = trainingDate;
            Type = type;
        

        }

        /// <summary>
        /// Metoda AddExerciseCardio dodaje ćwiczenie cardio do listy ćwiczeń treningu.
        /// </summary>
        /// <param name="exercise"></param>
        public void AddExerciseCardio(CardioExercise cardioEx)
        {
            
            Exercises.Add(cardioEx);
        }

        /// <summary>
        /// Metoda AddExerciseGym dodaje ćwiczenie siłowe do listy ćwiczeń treningu.
        /// </summary>
        /// <param name="exercise"></param>
        public void AddExerciseGym(GymExercise gymEx)
        {
            Exercises.Add(gymEx);
        }

        /// <summary>
        /// Przesłonięta metoda ToString, wypisuje informacje o treningu: datę, rodzaj treningu i informacje o ćwiczeniach.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{TrainingDate.ToString("dd-MM-yyyy")}");
            sb.AppendLine($"{Type.ToString()}");
            foreach (var item in Exercises)
            {
                sb.AppendLine(item.ToString());

            }
            return sb.ToString();
        }
       

    }
}
