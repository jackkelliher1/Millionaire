using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace millionaireAssessment
{
    struct Student
    {
        public string fName;
        public string lName;
        public string interest;
    }
    class Program
    {
        //Main Method, creates student array and menu
        static void Main(string[] args)
        {
            bool exit = false;
            int count = 0; //keeps track of studentArray pos
            Student[] studentArray = new Student[30]; //All students stored
            Student[] finalists = new Student[10]; //10 Finalist students stored
            StreamReader sr = new StreamReader(@"millionaire.txt"); //Reads from file
            while (!sr.EndOfStream) //Fills Array
            {
                studentArray[count].fName = sr.ReadLine();
                studentArray[count].lName = sr.ReadLine();
                studentArray[count].interest = sr.ReadLine();
                count++;
            }

            while (!exit) //Menu System
            {
                Console.Clear();
                Console.Write("Who Wants to Be a Millionaire\n\t1.Contestants\n\t2.Update Interests\n\t3.Generate Finalists\n\t4.Select Player\n\t5.Play\n\nEnter the relevent number:");
                switch (Convert.ToInt32(Console.ReadLine()))
                {
                    case 1:
                        Method1(studentArray);
                        Console.ReadLine();
                        break;
                    case 2:
                        Method2(studentArray);
                        break;
                    case 3:
                        Method3(studentArray, finalists);
                        Console.ReadLine();
                        break;
                }
            }
        }

        //Lists all contestants sorted by last name
        public static void Method1(Student[] array)
        {
            int count = 1; //numbers each student
            Console.Clear();
            //Bubble sort sorts the array based on last name.
            Student temp;
            for (int i = 0; i < array.Length; i++)
            {
                for (int pos = 0; pos < array.Length - 1; pos++)
                {
                    if (array[pos + 1].lName.CompareTo(array[pos].lName) < 0)
                    {
                        temp = array[pos + 1];
                        array[pos + 1] = array[pos];
                        array[pos] = temp;
                    }
                }
            }

            //Writes each student to Screen.
            Console.WriteLine("Num".PadRight(7) + "First Name".PadRight(15) + "Last Name".PadRight(30) + "Intrest");
            foreach (Student i in array)
            {
                Console.WriteLine(Convert.ToString(count).PadRight(7) + i.fName.PadRight(15) + i.lName.PadRight(30) + i.interest);
                count++;
            }
        }

        //Update a students interest feild.
        static void Method2(Student[] array)
        {
            bool exit = false;
            int temp;
            while (!exit) //repeats the loop until the user wants to return to menu
            {
                Method1(array); //Lists all students
                Console.Write("Enter the Number of the student you want to update: ");
                temp = Convert.ToInt32(Console.ReadLine()); //converts string input into integer
                if (temp <= array.Length) //Makes sure temp is not greater than the student array
                {
                    Console.Write($"Enter {array[temp - 1].fName}'s new interest: "); //Finds the name for the selected number
                    array[temp - 1].interest = Console.ReadLine(); //enters input into interst feild of selected student
                    Console.WriteLine("Change successful");
                }
                else //if temp is larger than the array length
                {
                    Console.WriteLine("Invalid Number");
                    Console.WriteLine("Change unsuccessful");
                }
                Console.Write("Do you want to change another interest feild? (y, n): "); //checks whether to remain in the for loop or not
                if (Console.ReadLine().Contains('n'))
                {
                    exit = true;
                }
            }
        }

        //Generates 10 finalists
        static void Method3(Student[] array, Student[] finalists)
        {
            Student temp;
            Random rand = new Random();
            Console.Clear();
            for(int i = 0; i < finalists.Length; i++)
            {
                temp = array[rand.Next(30)];
                for (int a = 0; a < finalists.Length; a++)
                {
                    if (temp.lName.CompareTo(array[a].lName) == 1)
                    {
                        i = i - 1;
                    }
                }
            }

            Console.WriteLine("First Name".PadRight(15) + "Last Name".PadRight(30) + "Interest".PadRight(15));
            foreach (Student i in finalists)
            {
                Console.WriteLine(i.fName.PadRight(15) + i.lName.PadRight(30) + i.interest.PadRight(15));
            }
        }

        /* static void Method4()
        {

        }

        static void Method5()
        {

        } */
    }
}