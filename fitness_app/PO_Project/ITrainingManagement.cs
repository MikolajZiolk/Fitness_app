using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO_Project
{
    /// <summary>
    /// Interfejs ITrainingManagement definiuje metody związane z dodawaniem ćwiczeń do treningu . 
    /// </summary>
    public interface ITrainingManagement
        {
            void AddExerciseCardio(CardioExercise cardioEx); 
            void AddExerciseGym(GymExercise gymEx);
           
        }
    
}
