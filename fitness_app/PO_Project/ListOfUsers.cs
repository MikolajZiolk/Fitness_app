using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PO_Project
{
    [Serializable]
    /// <summary>
    /// Klasa ListOfUsers reprezentuje listę użytkowników w systemie. 
    /// </summary>
    public class ListOfUsers : ICloneable
    {
         List<User> users;

        /// <summary>
        /// Właściwość pobiera lub ustawia listę użytkowników. 
        /// </summary>
        public  List<User> Users { get => users; set => users = value; }

        /// <summary>
        /// Konstruktor nieparametryczny, inicjalizuje nowy obiekt ListOfUsers z pustą listą użytkowników.
        /// </summary>
        public ListOfUsers()
        {
            Users = new();
        }

        /// <summary>
        /// Metoda dodaje nowego użytkownika do listy.
        /// </summary>
        /// <param name="newUser"></param>

        public void RegisterUser(User newUser) 
        {

                // Dodaj nowego użytkownika
                Users.Add(newUser);
            
        }

        /// <summary>
        /// Metoda GenerateNickname generuje unikalny nick dla podanego użytkownika, korzystając z metody GenerateNickname z klasy User.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string GenerateNickname(User user)
        {
            user.GenerateNickname(); 
            return user.Nick; 
        }

        /// <summary>
        /// Metoda sortuje listę użytkowników.
        /// </summary>
        public void SortUsers()
        {

            Users.Sort();

        }

        /// <summary>
        /// Metoda usuwa użytkownika z listy.
        /// </summary>
        /// <param name="us"></param>
        public void DeleteUsers(User us)
        {
            if (us is not null)
            {
                Users.Remove(us);
            }
        }

        /// <summary>
        /// Metoda zwraca reprezentację tekstową listy użytkowników.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Uzytkownicy: ");
            foreach (User user in Users)
            {
                sb.AppendLine(user.ToString());
            }
            return sb.ToString();
        }

        /// <summary>
        /// Metoda zapisuje obiekt ListOfUsers do pliku XML.
        /// </summary>
        /// <param name="nazwa"></param>
        public void ZapiszXML(string nazwa)
        {
            using StreamWriter sw = new(nazwa);
            XmlSerializer xs = new(typeof(ListOfUsers));
            xs.Serialize(sw, this);
        }

        /// <summary>
        /// Metoda odczytuje obiekt ListOfUsers z pliku XML.
        /// </summary>
        /// <param name="nazwa"></param>
        /// <returns>Nowy obiekt ListOfUsers odczytany z pliku XML.</returns>
        public static ListOfUsers? OdczytXml(string nazwa)
        {
            if (!File.Exists(nazwa))
            {
                return null;
            }
            using StreamReader sw = new(nazwa);
            XmlSerializer xs = new(typeof(ListOfUsers));
            return xs.Deserialize(sw) as ListOfUsers;
        }

        /// <summary>
        /// Metoda klonuje obiekt ListOfUsers (klonowanie głębokie).
        /// </summary>
        /// <returns></returns>
        public object Clone() 
        {
            ListOfUsers listOfUsers = new ListOfUsers();
            listOfUsers = (ListOfUsers)this.MemberwiseClone();
            listOfUsers.Users = new List<User>();
            foreach (User us in this.Users)
                listOfUsers.RegisterUser((User)us.Clone());

            return listOfUsers;
        }
    }
}
