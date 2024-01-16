using System;

namespace PO_Project
{ 
    internal class Program
    {
        static void Main(string[] args)
        {
            User user = new User("Andrzej", "Przybysz", 70, 1.80, EnumGender.Male);
            //User user1 = new User("Andrzej", "Przybysz", 70, 1.80, EnumGender.Male);
            User user2 = new User("Karol", "Przybysz", 70, 1.80, EnumGender.Male);
            //Console.WriteLine(user.ToString());
            //Console.WriteLine(user1.ToString());
            //Console.WriteLine(user2.ToString());

            //Training t1 = new Training(user1, DateTime.Now,EnumType.Swimming) ;
            //Console.WriteLine(t1.ToString());

           ListOfUsers users = new ListOfUsers();
            users.RegisterUser(user);
            users.RegisterUser(user2);
           // Console.WriteLine(users);

            users.ZapiszXML("users.xml");
            ListOfUsers users2 = ListOfUsers.OdczytXml("users.xml");

           // Console.WriteLine(users2);

            CardioExercise exercise = new CardioExercise("Running",50, 20.5, user);
            CardioExercise ce = new CardioExercise("Running", 30, 15.5, user);

            Training tr = new Training(user,DateTime.Now, EnumType.Cardio);
            Training tr2 = new Training(user,DateTime.Now, EnumType.Cardio);

            tr.AddExerciseCardio(exercise);
            tr2.AddExerciseCardio(ce);




            UserTrainings userTrainings = new UserTrainings(user);
            userTrainings.AddTraining(tr);
            userTrainings.AddTraining(tr2);

            CardioExercise exer = new CardioExercise("Running", 10, 20.5, user2);
            CardioExercise ceee = new CardioExercise("Running", 100, 5.5, user2);

            Training tr12 = new Training(user2,DateTime.Now,  EnumType.Cardio);
            Training tr22 = new Training(user2, DateTime.Now, EnumType.Cardio);

            tr12.AddExerciseCardio(exer);
            tr22.AddExerciseCardio(ceee);

            UserTrainings ut = new UserTrainings(user2);
            ut.AddTraining(tr12);
            ut.AddTraining(tr22);


            UsersRanking usersRanking = new UsersRanking();
            usersRanking.AddUsTrainings(ut);
            usersRanking.AddUsTrainings(userTrainings);

            Console.WriteLine(usersRanking.ToString());

            usersRanking.CalculateAndSortTotalProgress();

            Console.WriteLine(usersRanking.ToString());


            GymExercise g1 = new GymExercise("Deadlift", 47, 8, 3, user);
            GymExercise g2 = new GymExercise("Deadlift", 97, 8, 3, user);
            GymExercise g3 = new GymExercise("Deadlift", 77, 8, 3, user2);
            GymExercise g4 = new GymExercise("Deadlift", 87, 8, 3, user2);


            Training tren1 = new Training(user,DateTime.Now ,EnumType.Strength);
            Training tren2 = new Training(user,DateTime.Now, EnumType.Strength);

            tren1.AddExerciseGym(g1);
            tren1.AddExerciseGym(g2);
            tren2.AddExerciseGym(g3);
            tren2.AddExerciseGym(g4);

            userTrainings.AddTraining(tren1);
            ut.AddTraining(tren2);



        }
    }
}