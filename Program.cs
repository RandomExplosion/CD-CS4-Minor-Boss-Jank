using System;
using System.IO;
using System.Collections.Generic;

namespace CSharp_MinorBoss_ConsoleApp
{

    public enum Race { Human, Alien, Animal, Supernatural }

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
            //Check If File Exists and if not create it
            if (!File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MarvelDB", "Database.csv")))
            {
                Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MarvelDB"));
                File.Create(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MarvelDB", "Database.csv"));
            }

            //Instantiate 
            HeroTable dbTable = new HeroTable();
            dbTable.LoadFromCSV("Database.csv");

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
                        Console.WriteLine(dbTable.GetPrintableTable());
                        WaitForEnterPress();
                        break;

                    case 2:
                        Console.Clear();
                        bool validStats = false;
                        string _heroName = "";
                        string _realName = "";
                        double _weight = 0d;
                        double _height = 0d;
                        Race _race = Race.Human;
                        bool _inMovie = false;

                        while (!validStats)
                        {
                            Console.Clear();


                            try
                            {
                                //Hero Name
                                Console.WriteLine("Enter New Superhero's Hero Alias");
                                _heroName = Console.ReadLine();
                                if (dbTable.heroesByName.ContainsKey(_heroName))
                                {
                                    Console.WriteLine("A hero with that name already exists.\nTry editing it instead.");
                                    WaitForEnterPress();

                                    continue; //Invalid
                                }

                                //Real Name
                                Console.WriteLine("Enter New Superhero's Real Name");
                                _realName = Console.ReadLine();

                                //Weight
                                Console.WriteLine("Enter New Superhero's Weight In KG (Numeric, Rounded to two decimal places)");
                                _weight = Convert.ToDouble(Console.ReadLine());

                                //Height
                                Console.WriteLine("Enter New Superhero's Height in M (Numeric, Rounded to two decimal places)");
                                _height = Convert.ToDouble(Console.ReadLine());

                                //Race
                                Console.WriteLine("Enter New Superhero's Race (not case sensitive)\nValid Races: Human, Alien, Animal, Supernatural");
                                if (!Enum.TryParse<Race>(Console.ReadLine(), true, out _race))    //Attempt to parse Race and go back to the start if invalid
                                {
                                    continue;
                                }

                                //In Movie?
                                Console.WriteLine("Has the new SuperHero been in a movie yet?");
                                ConsoleKey nextKey = Console.ReadKey().Key;

                                if (nextKey == ConsoleKey.Y)
                                {
                                    _inMovie = true;
                                }
                                else if (nextKey == ConsoleKey.N)
                                {
                                    _inMovie = false;
                                }

                                dbTable.heroesByName.Add(_heroName, new SuperHero(_heroName, _realName, _weight, _height, _race, _inMovie));
                                dbTable.SaveToCSV("Database.csv");
                                validStats = true;
                                Console.WriteLine();
                                WaitForEnterPress();
                            }
                            catch (Exception)
                            {
                                continue;
                            }
                        }
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
        public Race race = Race.Human;
        public bool inMovie = false;

        public SuperHero(string _heroName, string _realName, double _weight, double _height, Race _race, bool _inMovie)
        {
            heroName = _heroName;
            realName = _realName;
            weight = _weight;
            height = _height;
            race = _race;
            inMovie = _inMovie;
        }

        public void ModifyData(string _heroName, string _realName, double _weight, double _height, Race _race, bool _inMovie)
        {
            heroName = _heroName;
            realName = _realName;
            weight = _weight;
            height = _height;
            race = _race;
            inMovie = _inMovie;
        }


    }
    public class HeroTable
    {
        public readonly Dictionary<string, SuperHero> heroesByName = new Dictionary<string, SuperHero>();

        public string GetPrintableTable()
        {
            return "Table printing not yet implimented";
        }

        //Load the data from the csv database
        public void LoadFromCSV(string fileName)
        {

            // Read sample data from CSV file
            using (CsvFileReader reader = new CsvFileReader(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MarvelDB", fileName)))
            {
                heroesByName.Clear();
                List<string> tempList = new List<string>();
                CsvRow row = new CsvRow();
                while (reader.ReadRow(row))
                {
                    tempList.Clear();

                    //Load row data into list
                    foreach (string value in row)
                    {
                        tempList.Add(value);
                    }

                    Race tempRace;
                    tempRace = Enum.Parse<Race>(tempList[4]);

                    //Add the row's hero to th
                    heroesByName.Add(tempList[0], new SuperHero(tempList[0], tempList[1], Convert.ToDouble(tempList[2]), Convert.ToDouble(tempList[3]), tempRace, Convert.ToBoolean(tempList[5])));

                    Console.WriteLine();
                }
            } 
            
        }

        //Save the data to the csv database
        public void SaveToCSV(string fileName)
        {
            // Write sample data to CSV file
            using (CsvFileWriter writer = new CsvFileWriter(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MarvelDB", fileName)))   //CHECK IF THIS IS MEANT TO BE THE FULL PATH!!!!
            {

                foreach (SuperHero heroValue in heroesByName.Values)
                {
                    CsvRow row = new CsvRow();

                    row.Add(heroValue.heroName);
                    row.Add(heroValue.realName);
                    row.Add(heroValue.weight.ToString());
                    row.Add(heroValue.weight.ToString());
                    row.Add(heroValue.race.ToString());
                    row.Add(heroValue.inMovie.ToString());


                    writer.WriteRow(row);
                }
                
            }
        }

    }
}
