using System;
using System.IO;
using System.Collections.Generic;

namespace CSharp_MinorBoss_ConsoleApp
{
    class Program
    {
        /// <summary>
        /// Source: https://www.codeproject.com/Articles/415732/Reading-and-Writing-CSV-Files-in-Csharp
        /// </summary>
        static void WriteTest()
        {

        }

        static void ReadTest()
        {

        }

        static void Main(string[] args)
        {
            while (true)
            {
                #region Scope:RootMenu
                int input = 0;  //Reused throughout entire program
                string[] options = { "View Marvel DB", "Add Superhero", "Edit Superhero", "Remove Superhero", "Exit System" };  //Reused throughout the program

                input = QueryUserAction("Welcome To MarvelDB Superhero Database Management!\nWhat would you like to do?", options);
                switch (input)
                {
                    case 1:
                        //Console.WriteLine("Not Yet Implimented");
                        CSVParser.GetCSV("Database.csv");
                        WaitForEnterPress();
                        break;

                    case 2:
                        Console.WriteLine("Not Yet Implimented");
                        WaitForEnterPress();
                        break;

                    case 3:
                        Console.WriteLine("Not Yet Implimented");
                        WaitForEnterPress();
                        break;

                    case 4:
                        Console.WriteLine("Not Yet Implimented");
                        WaitForEnterPress();
                        break;

                    case 5:
                        return;

                        //No need for default state as validation is handled in QueryUserAction()

                }
                #endregion
            }
        }

        static int QueryUserAction(string promptText, string[] optionsList)
        {
            bool GotValidInput = false;
            string inputStr = "";
            int inputInt = 0;

            while (!GotValidInput)
            {
                Console.Clear();                                                //Flush the console
                Console.WriteLine(promptText);                                  //Print Prompt  
                for (int i = 0; i < optionsList.Length; i++)                    //Print Options
                {
                    Console.WriteLine($"{i + 1}. {optionsList[i]}");
                }

                try                                                             //Gather Input and Verify that it is in range
                {
                    inputStr = Console.ReadLine();                          //Collect input
                    inputInt = Convert.ToInt32(inputStr);                   //Convert to int
                    if (inputInt <= optionsList.Length && inputInt > 0)     //Return Valid
                    {
                        GotValidInput = true;
                    }
                    else
                    {
                        throw new IndexOutOfRangeException($"Number must be between 1 and {optionsList.Length} (Indexing from 1 for idiot proofing)");
                    }
                }
                catch (Exception)
                {
                    continue;                                               //Allow the user to try again
                }

            }

            return inputInt;

        }

        static void WaitForEnterPress()
        {
            Console.WriteLine("Press ENTER to continue...");
            bool hasPressedEnter = false;
            while (!hasPressedEnter)
            {
                ConsoleKeyInfo keyPress = Console.ReadKey(); //Get the next keypress

                if (keyPress.Key == ConsoleKey.Enter)
                {
                    hasPressedEnter = true;
                }
            }
        }
    }

    public class SuperHero
    {
        public string heroName = "";
        public string realName = "";
        public double weight = 0d;
        public double height = 0d;

        SuperHero(string _heroName, string _realName, double _weight, double _height)
        {
            heroName = _heroName;
            realName = _realName;
            weight = _weight;
            height = _height;
        }

        void ModifyData(string _heroName, string _realName, double _weight, double _height)
        {
            heroName = _heroName;
            realName = _realName;
            weight = _weight;
            height = _height;
        }
    }
    public class HeroTable
    {
        protected SortedDictionary<string, SuperHero> heroesByName;

        public static HeroTable _tableInstance;

        static void OnDbUpdate()
        {

        }

        static string GetTableAsString()
        {
            return "Table printing not yet implimented";
        }

    }
}
