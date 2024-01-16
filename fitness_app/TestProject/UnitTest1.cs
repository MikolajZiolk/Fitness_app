using PO_Project;



namespace TestProject
  
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void UserConstructTest()
        {
            //Arrange
            User u1;
            string name = "Anna";
            string lastName = "Golak";
            double weight = 55;
            double height = 1.7;
            EnumGender gender = EnumGender.Female;
 

            //Act
            u1 = new User(name, lastName, weight, height, gender);

            //Assert
            Assert.AreEqual(name, u1.Name);
            Assert.AreEqual(lastName, u1.LastName);
            Assert.AreEqual(weight, u1.Weight);
            Assert.AreEqual(height, u1.Height);
            Assert.AreEqual(gender, u1.Gender);
         
        }

        [TestMethod]
        public void BmiMethodTest()
        {

            //Arrange
            User u1;
            string name = "Anna";
            string lastName = "Golak";
            double weight = 55;
            double height = 1.7;
            EnumGender gender = EnumGender.Female;
  

            //Act
            u1 = new User(name, lastName, weight, height, gender);

            double expectedBmi = weight / (height * height);
            double calculatedBmi = u1.BMI(weight, height);

            //Assert
            Assert.AreEqual(expectedBmi, calculatedBmi);

        }

       [TestMethod]
       public void GenerateNickTest()
       {
           //Arrange
           User u1;
           string name = "Anna";
           string lastName = "Golak";
           double weight = 55;
           double height = 1.7;
           EnumGender gender = EnumGender.Female;
          

            //Arrange
            User u2;
           string name2 = "Anna";
           string lastName2 = "Golak";
           double weight2 = 50;
           double height2 = 1.5;
           EnumGender gender2 = EnumGender.Female;
           

            //Act
            u1 = new User(name, lastName, weight, height, gender);
           u2= new User(name2, lastName2, weight2, height2, gender2);

           //string expectedNick = "anngol";

           string generatedNick =  u1.Nick;
           string generatedNick2 =  u2.Nick;

           //Assert
           Assert.AreNotEqual(generatedNick, generatedNick2);
       }


        [TestMethod]
        public void GenerateNickTest2()
        {
            //Arrange
            User u1;
            string name = "Karolina";
            string lastName = "Wrobel";
            double weight = 55;
            double height = 1.7;
            EnumGender gender = EnumGender.Female;
         

            //Act
            u1 = new User(name, lastName, weight, height, gender);

            string generatedNick = u1.Nick;
            string expectedNick = "karwro";

            //Assert
            Assert.AreEqual(expectedNick,generatedNick);

        }

        [TestMethod]

        public void WrongDataExceptionTest()
        {
            //Arrange 
            User u1 = new User();
            string wrongName = "Józef";

            //Act,Assert
            Assert.ThrowsException<WrongData>(() => u1.Name = wrongName);

        }

        [TestMethod]
        public void MeanPaceTest()
        {
            //Arrange
            CardioExercise c1;
            User u1 = new User();
            string exerciseName = "running";
            int time = 32;
            double distance = 5;

            //Act
            c1 = new CardioExercise(exerciseName, time, distance, u1);  
            double expectedMeanPace = time / distance;
            double calculatedMeanPace = c1.MeanPace(time, distance);

            //Assert
            Assert.AreEqual(expectedMeanPace,calculatedMeanPace);
        }

        [TestMethod]
        public void GymExerciseScoreTest()
        {
            // Arrange
            string exerciseName = "Bench Press";
            int kilograms = 100;
            int reps = 10;
            int sets = 3;
            User user = new User("Mikolaj", "Ziolkowski", 80, 1.75, EnumGender.Male);

            // Act
            GymExercise gymExercise = new GymExercise(exerciseName, kilograms, reps, sets, user);
            double expectedScore = 0.4 * kilograms + 0.3 * reps + 0.3 * sets;
            double calculatedScore = gymExercise.ExerciseScore();

            // Assert
            Assert.AreEqual(expectedScore, calculatedScore);
        }

        [TestMethod]
        public void CardioExerciseScoreTest()
        {
            // Arrange
            User user = new User("Jan", "Dom", 80, 1.75, EnumGender.Male);
            CardioExercise cardioExercise = new CardioExercise("Running", 5, 30, user);

            // Act
            double expectedScore = 0.4 * cardioExercise.Distance + 0.4 * cardioExercise.Time + 0.2 * cardioExercise.CalculateCaloriesBurned(cardioExercise.Time, cardioExercise.Distance, user);
            double actualScore = cardioExercise.ExerciseScore();

            // Assert
            Assert.AreEqual(expectedScore, actualScore, 0.001);
        }
    }


}