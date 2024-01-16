using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO_Project
{
    public class UsersRanking 
    {
        /// <summary>
        /// Klasa reprezentuje ranking użytkowników na podstawie ich treningów. 
        /// </summary>
        List<UserTrainings> ranking = new List <UserTrainings> ();

        /// <summary>
        /// Właściwość pobiera lub ustawia listę obiektów UserTrainings. 
    /// </summary>
        public List<UserTrainings> Ranking { get => ranking; set => ranking = value; }


        /// <summary>
        /// Konstruktor nieparametryczny, inicjalizuje nowy obiekt UsersRanking z pustą listą.
        /// </summary>
        public UsersRanking()
        {
            ranking = new List <UserTrainings> ();
        }

        /// <summary>
        /// Metoda oblicza postęp całkowity dla każdego użytkownika w rankingu i sortuje go na podstawie domyślnych kryteriów porównawczych określonych w klasie UserTrainings.
        /// </summary>
        public void CalculateAndSortTotalProgress()
        {
            // Sortuje ranking na podstawie interfejsu IComparable
            Ranking.Sort();
        }

        /// <summary>
        /// Metoda dodaje obiekt UserTrainings do rankingu.
        /// </summary>
        /// <param name="c"></param>
        public void AddUsTrainings(UserTrainings c)
        {
            ranking.Add(c);
        }
    

        /// <summary>
        /// Przesłonięta metoda ToString() wypisuje informację o treningach użytkownika. 
        /// </summary>
        /// <returns></returns>

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

           foreach(UserTrainings c in ranking)
            {
                sb.AppendLine(c.GetRankingInfo().ToString());
            }
           return sb.ToString();
        }
    }
}
