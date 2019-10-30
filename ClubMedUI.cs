using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using Amotels.POCO;
using System.Threading.Tasks;

namespace Amotels
{
    /// <summary>
    /// Main class for dealing with the user interface (UI) also some חווית הממשק (UX)
    /// </summary>
    public class ClubMedUI
    {

        /// <summary>
        /// Displays the most annoying visual one can see (after an emmy schumer performance), it is very annoying,
        /// mostly because it's totally useless. the idea is that when I give a software update I can remove the Thread.Sleep
        /// and then tell people I did massive optimization and lowered the startup time by thousands of milliseconds.
        /// I can get a payraise or somethin y'know, living out of the hood, in the suburbs and shiz
        /// </summary>
        public void Loading()
        {
            Random random = new Random();
            Console.SetWindowSize(100, 50);
            Console.Clear();

            Console.SetCursorPosition(44, 25);
            Console.WriteLine("Loading...");

            Console.ForegroundColor = ConsoleColor.Green;
            for (double theta = 0; theta < 2 * Math.PI; theta += Math.PI / 90)
            {

                for (double i = 21; i <= 24; i += 0.5)
                {

                    Console.SetCursorPosition(2 * (int)(25 + i * Math.Sin(theta)), (int)(25 + i * Math.Cos(theta)));
                    Console.Write("██");
                }
                if (random.Next(10) == 0) Thread.Sleep(100);
                Thread.Sleep(5);
            }

            Console.Clear();
        }
        /// <summary>
        /// Pretty much just opens the main menu, then processes the results by calling Menua Selector and calling the input methods.
        /// what mate, you expected some joke here? well here's a joke for ya mate: what did the paley say to the innocent person: he said "ayy"
        /// </summary>
        public void Start()
        {

            // pretty nostalgic considering it's the only thing remaining from the first UI class we wrote
            Console.WriteLine("Greetings user! what would you like to view:");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(// now this is what I call a logo
        @"



           ,____    
           \\|_|\
            \---/
         __|\|`|                            __                __           
        / \\\|-|\                          |\\\              |\\\          
       /  /^-##-\\ ______ ____    ______  _|\$$_     ______  | $$  _______    
      /  //$__|_$\\\\\\\\\\\\\\  / \\\\\\|\\\$$\\   /\\\\\\\ | $$ /\\\\\\\\   
       \//$$  | $$\\$$$$$$\$$$$\|  $$$$$$\\$$$$$$  |  $$$$$$\| $$|  $$$$$$$   
        | $$$$$$$$|/$$_|_$$_|_$$\ $$  | $$ | $$ __ | $$   \$$| $$ \$$   \\      
  __ __ | $$|*| $$| $$ | $$ | $$|\$$__/ $$ | $$|\\\| $$$$$$$$| $$ _\$$$$$$\     
  ||=||=| $$| | $$| $$ | $$ | $$ \$$\\\\$$  \$$  $$ \$$     \| $$|\      $$     
  ||=|\==\$$___\$$ \$$  \$$  \$$  \$$$$$$    \$$$$   \$$$$$$$ \$$ \$$$$$$$      
");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.SetWindowSize(100, 50);
            Console.WriteLine(@" 
                               _-=--(0)--=-_
                       2019 (c) An Amit Nave Company
                            <      -  +  -      >
                      Also Gil U-delevitch, Dan Binnun
                               ``--____--``
");
            Console.ResetColor();
            MenuSelector selector = new MenuSelector(35, 26,
                "1. Insert Hotel",
                "2. Search Hotel",
                "3. Order Room",
                "4. Get Report"
                );


            // this thread is more like, *thread*mill am I right?
            Thread t = new Thread(() =>
            {
                Random random = new Random();
                {
                    Thread.Sleep(200);
                    int ind = 0;
                    while (true)
                    {


                        Console.SetCursorPosition(0, 1);
                        Console.ForegroundColor = random.Next(2) == 0 ? ConsoleColor.DarkGray : ConsoleColor.Gray;
                        Console.Write(SmokeFrames[ind]);
                        Thread.Sleep(200);
                        ind = (ind + 1) % SmokeFrames.Length;
                    }
                }
            });

            t.Start();


            int i = selector.Select();

            t.Abort();
            Console.Clear();
            switch (i)
            {
                // insert
                case 0:
                    InsertVillageInput();
                    break;
                // search
                case 1:
                    SearchVillageInput();
                    break;
                // order  
                case 2:
                    OrderRoomInput();
                    break;
                // report
                case 3:
                    YearlyReport();
                    break;
            }
        }

        /// <summary>
        /// 
        /// Sends the Yearly Report.
        /// 
        /// </summary>
        public static void YearlyReport()
        {
            ClubMedDBAccess.YearlyReport().ForEach(report =>
            {
                Console.WriteLine(report);
            });
        }
        /// <summary>
        /// 
        /// asks user for input, then searches the database for matching villages
        /// 
        /// </summary>
        private static void SearchVillageInput()
        {
            string country = InputString("Enter the villages country").ToLower();
            Console.WriteLine(@"Room Types are as following:");

            for (int i = 1; i <= 3; i++) Console.WriteLine($" {i} - {ClubMedDBAccess.GetRoomType(i)}");

            int roomType = InputInt("Please Enter Room Type");

            int week = InputInt("Please enter week of stay (week is from 1 to 51)");

            List<Village> list = ClubMedDBAccess.SearchVillage(country, week, roomType);

            Console.WriteLine($@"
Room Type - {ClubMedDBAccess.GetRoomType(roomType)}
Week Of Stay - {week}
Country - {country}
");

            if (list.Count == 0)
            {
                Console.WriteLine("No Rooms Found");
                DanHelper.Say("lonim1tsehuchadarim1");
            }
            else foreach (Village village in list)
                {
                    Console.WriteLine(village);
                }
        }

        /// <summary>
        /// I mean come the hell on, this is very explanatory, well I guess I gotta put some effort here for a good grade:
        /// well this is to order a room, asks user for input and uses the DBAccess layer to order it
        /// </summary>
        private static void OrderRoomInput()
        {
            bool wrong = true;
            while (wrong) try
                {
                    Console.WriteLine(@"Room Types are as following:");

                    for (int i = 1; i <= 3; i++) Console.WriteLine($" {i} - {ClubMedDBAccess.GetRoomType(i)}");

                    int roomType = InputInt("Please Enter Room Type");

                    int id = InputInt("Please Enter Customer ID");

                    if (!ClubMedDBAccess.IsCustomer(id))
                    {
                        if (InputBool("Seems like you're not an existing customer, would you like to add yourself to the database?"))
                        {
                            Console.WriteLine("Great, we need a few details first");
                            string first = InputString("What is your first name?");
                            string last = InputString("What is your last name?");

                            ClubMedDBAccess.InsertCustomer(new Customer(id, first, last));
                            Console.WriteLine("Customer Inserted Successfully! let's move on.");
                        }
                        else
                        {
                            Console.WriteLine("Oh well, come back when you can.");
                            return;
                        }
                    } else
                    {
                        Console.WriteLine("Good! I see you're registered."); 
                    }

                    ClubMedDBAccess.OrderRoom(InputInt("Please Enter Village ID"), id, InputInt("Please Enter Week"), roomType);

                    Console.WriteLine("Your room has been successfully ordered!");
                    DanHelper.Say("chad1re2ahuz1man1behats1lacha");


                    wrong = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
        }

        /// <summary>
        /// Asks user input for villageDetails
        /// </summary>
        /// <param name="roomType">type of room to give details for</param>
        /// <param name="id">village id</param>
        /// <returns>Village Details POCO</returns>
        private static VillageDetails GetDetails(int roomType, int id)
        {
            Console.WriteLine("Please enter the following details for the rooms of type \"" + ClubMedDBAccess.GetRoomType(roomType) + "\"");
            int rooms = InputInt("Please enter number of rooms");
            int price = InputInt("Please enter price of room (per week)");

            VillageDetails details = new VillageDetails(id, roomType, rooms, price);
            return details;
        }

        /// <summary>
        /// This is once again pretty self explanatory
        /// </summary>
        private static void InsertVillageInput()
        {
            bool wrong = true;
            while (wrong)
                try
                {
                    int villageID = InputInt("Please enter the Village's ID");

                    string villageName = InputString("Please enter the villages name.");
                    string country = InputString("Enter the villages country").ToLower();
                    string city = InputString("Enter the villages city");
                    string street = InputString("Enter the villages street");
                    int houseNumber = InputInt("Enter the house number");
                    Console.WriteLine(ClubMedDBAccess.GetActivities());
                    int mainActivityID = InputInt("Enter the villages main activity");
                    int startingTime = InputInt("Enter Starting Time");
                    if (startingTime < 1 || startingTime > 51) throw new InvalidTimeExeption();
                    int endingTime = InputInt("Enter Ending Time");
                    if ((endingTime > 52 || endingTime < 2) || endingTime < startingTime) throw new InvalidTimeExeption();
                    double rating = InputDouble("Enter Village Rating");
                    if (rating > 5 || rating < 1) throw new InvalidRatingExeption();
                    wrong = false;

                    VillageDetails[] details = new VillageDetails[3];

                    for (int i = 1; i <= 3; i++)
                    {
                        details[i - 1] = GetDetails(i, villageID);
                    }

                    ClubMedDBAccess.InsertVillage(new Village(villageID,
                        villageName,
                        country,
                        city,
                        street,
                        houseNumber,
                        mainActivityID,
                        startingTime,
                        endingTime,
                        rating),

                        details);


                    DanHelper.Say("k1far1chahit1vasef1behats1lacha");
                    Console.WriteLine("Village Added Successfully");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

        }

        /// <summary>
        /// This method is used to get a user input of a bool. If the string entered is not a Y or N it will ask the user to enter a string again.
        /// </summary>
        /// <param name="ask">The messege written to the user tellings him what the bool he is entering represents</param>
        /// <returns>The bool entered, if it is a bool</returns>
        public static bool InputBool(string ask)
        {
            Console.Write(ask);
            Console.WriteLine(" (Y / N)");
            ConsoleKeyInfo info = Console.ReadKey();
            while (true)
            {
                if (info.Key == ConsoleKey.Y) return true;
                if (info.Key == ConsoleKey.N) return false;

                Console.WriteLine("Please enter a valid answer (Y / N)");

                info = Console.ReadKey();
            }
        }

        /// <summary>
        /// This method is used to get a user input of a string. If the string is empty it will ask the user to enter a string again.
        /// </summary>
        /// <param name="ask">The messege written to the user tellings him what the string he is entering represents</param>
        /// <returns>The string entered so long as its not empty</returns>
        public static string InputString(string ask)
        {
            Console.WriteLine(ask);
            string output = Console.ReadLine();
            while (output == "")
            {
                Console.WriteLine("You cannot enter an empty string you particularly unsmart individual");
                output = Console.ReadLine();
            }
            return output.Replace('"', '\'');
            
        }

        /// <summary>
        /// This method is used to get a user input of a int. If the string he entered does not represent an 
        /// int it will ask the user to enter a double again.
        /// </summary>
        /// <param name="ask">The messege written to the user tellings him what the int he is entering represents</param>
        /// <returns>The int entered so long as it is an int</returns>
        public static int InputInt(string ask)
        {
            Console.WriteLine(ask);
            int output = 0;
            for (; true;)
            {
                if (int.TryParse(Console.ReadLine(), out output))
                {
                    return output;
                }
                else Console.WriteLine("Invalid number mate, try again.");
            }
        }

        /// <summary>
        /// This method is used to get a user input of a double. If the string he entered does not represent a 
        /// double it will ask the user to enter a double again.
        /// </summary>
        /// <param name="ask">The messege written to the user tellings him what the double he is entering represents</param>
        /// <returns>The double entered so long as it is an double</returns>
        public static double InputDouble(string ask)
        {
            Console.WriteLine(ask);
            double output = 0;
            for (; true;)
            {
                if (double.TryParse(Console.ReadLine(), out output))
                {
                    return output;
                }
                else Console.WriteLine("Invalid number.");
            }
        }
        private static
            string[] SmokeFrames = new string[]
            {
                @"
              _-#
             ,%!   
           |/,
",
                @"
              `%!
             ,|   
           |*&,
",
                @"
               *|\
             /*&   
           _-#
",
                @"
               *|&
             _-#   
          %!^^
",

            };


    }
}







