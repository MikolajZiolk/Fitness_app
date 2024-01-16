using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO_Project
{
    /// <summary>
    /// Interfejs IUserTrainingManagement definiuje metody związane z zarządzaniem treningami użytkownika: dodawanie i usuwanie treningów użytkownika.
    /// </summary>
    public interface IUserTrainingManagement
    {
        void AddTraining(Training training);
        void RemoveTraining(Training training);

    }
}
