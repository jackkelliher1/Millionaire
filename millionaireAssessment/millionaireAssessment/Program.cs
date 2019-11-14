using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace millionaireAssessment
{
    struct Student
    {
        public string fName;
        public string lName;
        public string interest;
    }

    struct Question
    {
        public string q;
        public string tAnswer;
        public string fAnswer1;
        public string fAnswer2;
        public string fAnswer3;
        public int used;
    }
    class Program
    {
        //Main Method, creates student array and menu
        static void Main(string[] args)
        {
            bool exit = false;
            int count = 0, num, money; //keeps track of studentArray pos
            string numString;
            Student player = new Student(); //saves the player data
            Student[] studentArray = new Student[30]; //All students stored
            Student[] finalists = new Student[10]; //10 Finalist students stored
            Question[] questionList = new Question[32]; //Questions Stored
            StreamReader sr = new StreamReader(@"millionaire.txt"); //Reads from file
            while (!sr.EndOfStream) //Fills Array
            {
                studentArray[count].fName = sr.ReadLine();
                studentArray[count].lName = sr.ReadLine();
                studentArray[count].interest = sr.ReadLine();
                count++;
            }
            sr.Close();
            count = 0;
            StreamReader sr1 = new StreamReader(@"questions.txt");
            while (!sr1.EndOfStream)
            {
                questionList[count].q = sr1.ReadLine();
                questionList[count].tAnswer = sr1.ReadLine();
                questionList[count].fAnswer1 = sr1.ReadLine();
                questionList[count].fAnswer2 = sr1.ReadLine();
                questionList[count].fAnswer3 = sr1.ReadLine();
                questionList[count].used = 0;
                count++;
            }
            sr1.Close();
            while (!exit) //Menu System
            {
                Console.Clear();
                Console.Write("Who Wants to Be a Millionaire\n\t1.Contestants\n\t2.Update Interests\n\t3.Generate Finalists\n\t4.Select Player\n\t5.Play\n\nEnter the relevent number:");
                numString = Console.ReadLine();
                foreach (char i in numString)
                {
                    if (i > '0' && i < '6')
                    {
                        num = Convert.ToInt32(numString);


                        switch (num)
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
                            case 4:
                                Method4(finalists, ref player);
                                Console.ReadLine();
                                break;
                            case 5:
                                Method5(questionList, player);
                                break;
                        }
                    }
                }
            }
        }

        //Lists all contestants sorted by last name. Complete
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

        //Update a students interest feild. Complete
        static void Method2(Student[] array)
        {
            bool exit = false, number = true;
            int temp;
            string chkString;
            while (!exit) //repeats the loop until the user wants to return to menu
            {
                number = false;
                Method1(array); //Lists all students
                Console.Write("Enter the Number of the student you want to update: ");
                chkString = Console.ReadLine(); //holds the input value in order to check validity
                foreach (char i in chkString) //goes through each char and confirms it is a valid integer
                {
                    if (i > '0' && i <= '9' && number == true) //if i is larger than 0 and smaller than/equal to 9 it is a valid integer
                    {
                        number = true;
                    }
                    else
                    {
                        number = false;
                    }
                }
                if (number == true) //if the foreach loop validated the string as a number
                {
                    temp = Convert.ToInt32(chkString); //converts string input into integer
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
                }
                else //if the string was not validated as an integer
                {
                    Console.WriteLine("Invalid Number");
                    Console.WriteLine("Change Unsuccessful");
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
            bool repeat = false; //Variable for the status of a repeating Student
            Student temp; //Temporary variable for storing student
            Random rand = new Random();

            Console.Clear();
            for (int i = 0; i < finalists.Length; i++) //Finds a unique student for each slot of the finalists array
            {
                temp = array[rand.Next(30)]; //Finds a random student
                for (int a = 0; a < finalists.Length; a++) //checks the student does not already exsist in the array
                {
                    if (temp.lName.CompareTo(finalists[a].lName) == 0) //Checks to see whether the last name of temp is equal to any already in the finalists array
                    {
                        repeat = true; //Sets repeat to true if the student is a repeat
                    }
                }

                if (!repeat) //assigns temp to finalists if it is not a repeated student.
                {
                    finalists[i] = temp;
                }
                else
                {
                    i = i - 1;
                }

                repeat = false; //sets repeat back to false
            }

            Method1(finalists); //Uses method1 to write the 10 finalists to screen.
        }

        static void Method4(Student[] finalists, ref Student player) //Randomly select a student to be the player
        {
            Random rand = new Random();
            player = finalists[rand.Next(10)];
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(player.fName + " " + player.lName + " was selected as the player.");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nPress ENTER to return to menu");
        }

        static void Method5(Question[] qArray, Student player)
        {
            int[] prizes = { 100, 200, 300, 500, 1000, 2000, 4000, 8000, 16000, 32000, 64000, 125000, 250000, 500000, 1000000 }; //amount of winnings per question
            int[] qShuffle = new int[4]; //shuffles all questions
            int[] numArray = { 0, 0, 0, 0 };
            int count = 0, pos = 0, temp = 0, countRepeat = 0, question, guessNum, correctNum = 0;
            bool exit = false, number = true;
            Random rand = new Random();
            string guess;


            do
            {
                Console.Clear();
                count = 0;
                foreach (int i in prizes)
                {
                    if (count == pos)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }

                    Console.Write("$" + i + "  ");

                    if (count == pos)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    count++;
                }

                Console.WriteLine("\nYou are playing as " + player.fName + " " + player.lName + "\n");
                question = rand.Next(0, 9);

                while (qArray[question].used != 0) //checks whether the question has already been used in the game, if so, find a new question
                {
                    question = rand.Next(0, 9);
                }

                Console.WriteLine(qArray[question].q);

                for (int i = 0; i < numArray.Length; i++) //resets all values of numArray to 0
                {
                    numArray[i] = 0;
                }

                for (int i = 0; i < numArray.Length; i++) //Randomly shuffles questions and checks for repeats in answers. 
                {
                    countRepeat = 0;
                    temp = rand.Next(4);
                    foreach (int a in numArray)
                    {
                        if (temp + 1 == a)
                        {
                            countRepeat++;
                        }
                    }

                    if (countRepeat > 0)
                    {
                        i--;
                    }
                    else
                    {
                        numArray[i] = temp + 1;
                    }
                }                                                                                                       // }

                count = 1; //resets the count variable
                foreach (int i in numArray)
                {
                    Console.Write(count + ". ");
                    switch (i)
                    {
                        case 1:
                            Console.WriteLine(qArray[question].tAnswer);
                            correctNum = count;
                            break;
                        case 2:
                            Console.WriteLine(qArray[question].fAnswer1);
                            break;
                        case 3:
                            Console.WriteLine(qArray[question].fAnswer2);
                            break;
                        case 4:
                            Console.WriteLine(qArray[question].fAnswer3);
                            break;
                    }
                    count++;
                }
                qArray[question].used = 1;

                //Allows the user to enter a guess and then check whether the guess is correct.

                Console.Write("Answer >");
                guess = Console.ReadLine();
                foreach (char i in guess)
                {
                    if (i > '0' && i < '5' && number == true)
                    {
                        number = true;
                    } else
                    {
                        number = false;
                    }
                }
                guessNum = Convert.ToInt32(guess);

                Thread.Sleep(2000);
                if (correctNum == guessNum)
                {
                    Console.WriteLine("\nCorrect!");
                    pos++;
                }
                else
                {
                    Console.WriteLine("\nIncorrect!");
                    exit = true;
                }
                Thread.Sleep(2000);
            } while (!exit);
        }
    }
}
