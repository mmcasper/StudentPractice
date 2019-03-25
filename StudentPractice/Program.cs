using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StudentPractice;

namespace StudentPracticeLog
{
    class Program

    {
        //From Daniel Schroeder's (aka deadlydog) Programming blog-"Saving and loading a c# Object's Data to a Json file.
        //<summary>
        //Writes the given object instance to a Json file
        //<para>Object type must have a parametrless constructor.</para>
        //<para>Only Public properties and variables will be written to the file. These can be any type, though, even other classes.
        //<para> If there are public properties/variables that you do not want written to the file, decorate them with the [JsonInore] attribute.
        //<typeparam> name ="T">The type of object being written to the file.
        //<param name="filePath>The file path to write the object instance to.
        //<param name="objectToWrite">The object instance to write to the file.
        //<param name="append">If false the file will be overwritten if it already exists. If true the contents will be appended to file.

        public static void WriteToJsonfile<T>(string filePath, T objectToWrite, bool append = false) where T : new()
        {
            TextWriter writer = null;
            try
            {
                var contentsToWriteToFile = JsonConvert.SerializeObject(objectToWrite, Formatting.Indented);
                writer = new StreamWriter(filePath, append);
                writer.Write(contentsToWriteToFile);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }

        ////<summary>
        ////Reads an object instance from an Json file.
        ////<para>Object typ must hae a paramenteress constructor.
        ////<typeparam name="T">THe type of object to read from the file.
        ////<param name ="filePath'> THe file path to read the object instance from.
        ////<returns>Returns a new instance of the object read from the Joson file.
        public static List<Student> ReadFromJsonFile<T>(string filePath) where T : new()
        {
            TextReader reader = null;
            try
            {
                reader = new StreamReader(filePath);
                var fileContents = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<List<Student>>(fileContents);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }



        static void Main(string[] args)
        {
            // initialize array of objects in default constructor to generate sample data
            //var students = new List<Student>
            //{
            //    new Student { Id = 101, Name = "Adam",LastName="Trask", TotalMinutes=0},
            //    new Student { Id = 102, Name = "Boden",LastName="Pough", TotalMinutes=0},
            //    new Student { Id = 103, Name = "Sierra", LastName = "MidCalf", TotalMinutes=0},
            //    new Student {Id = 104, Name= "Luke", LastName="Perry", TotalMinutes=0},
            //    new Student{Id=105, Name="Allie", LastName="Bopper", TotalMinutes=0}
            //};



            //WriteToJsonfile<List<Student>>("C:\\StudentName.Json", students, false);







            var students = ReadFromJsonFile<Student>("C:\\StudentName.Json");
            foreach (var student in students)
            {
                student.Print();

            }








            //Console.WriteLine(students.Last().Id);
            //Console.ReadLine();



            //Ask for Student ID
            Console.WriteLine("Please enter your Student Id#. Type q to quit at any time.");

            //Retrieve student name from StudentName.Json.
            int input = Convert.ToInt32(Console.ReadLine());
            var studentGreeting = (from s in students
                                   where s.Id == input
                                   select s).FirstOrDefault();
            //check for valid Id
            if (studentGreeting == null)
            {
                Console.WriteLine("Student not found.");
                //Create loop to return to input

            }

            else
            {
                //Console.WriteLine(string.Format("Hi {Name}, ....(start asking for input.)
                Console.WriteLine(string.Format("Hello " +
                    studentGreeting.Name + "! Lat week you practiced " +
                    studentGreeting.TotalMinutes + " minutes. Let's start adding up your practice for the week!"));



                //Console.WriteLine(studentGreeting);
                //Console.ReadLine();


                //var input =Console.ReadLine();
                //int.Parse(input);

                //if(input=students.First().Id)
                //{ Console.WriteLine(string.Format("Hello "+ students.First().Name+"! Let's start adding up your practice for the week!")};




                //var StudentId = Console.ReadLine();
                //ReadFromJsonFile<List<Student>>("C:\\StudentName.Json");








                // Prompt user for minutes practiced on each day
                string[] dayOfTheWeek = new string[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
                int minutes = 0;
                int weeklyTotal = 0;
                bool keepGoing = true;
                while (keepGoing)
                {
                    for (int i = 0; i < dayOfTheWeek.Length; i++)
                    {
                        Console.WriteLine(string.Format("Enter how many minutes you practiced on " + dayOfTheWeek[i] + " ?"));
                        string entry = Console.ReadLine();
                        //create exit from weekly log
                        if (entry == "q")
                        {
                            keepGoing = false;
                            break;
                        }
                        else
                        {

                            minutes = int.Parse(entry);

                            //insert try catch for non-integer entries

                            if (minutes < 0)
                            {
                                Console.WriteLine(minutes + " is not an acceptable value. Pleas enter the number of minutes you practiced.");
                                continue;
                            }

                            else if (minutes == 0)
                            {
                                Console.WriteLine("Thanks for being honest. Give me a solid 20 tomorrow.");
                            }
                            else if (minutes <= 10)
                            {
                                Console.WriteLine("Better than nothing, am I right?");
                            }
                            else if (minutes <= 30)
                            {
                                Console.WriteLine("That is a great day!");
                            }
                            else if (minutes <= 60)
                            {
                                Console.WriteLine("You are a rocket ship on your way to Mars!");
                            }
                            else
                            {
                                Console.WriteLine("Wow! Way to go!!!! When are you playing at carnegie hall?!!!!");
                            }


                        }
                        // Add minutes practice to total
                        weeklyTotal = weeklyTotal + minutes;

                    }

                    //Record weekly total to PracticeLog for the week. 

                    // Display total minutes exercised to the screen 
                    Console.WriteLine("You've practiced " + weeklyTotal
                        + " minutes this week. Press enter to exit.");
                    Console.ReadLine();
                    keepGoing = false;

                }
            }

            // Repeat until user finishes or quits
            Console.WriteLine("Goodbye");
            Console.ReadLine();

            //write weeklyTotal to StudentName.Json

        }


    }
}




