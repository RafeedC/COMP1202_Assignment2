// Authors: Rafeed Choudhury, Ben Claridad

using System;
using System.IO;

namespace VideoGameShop
{
    internal class Game
    {
        // Properties
        private int itemNumber;
        private string itemName;
        private double price;
        private double userRating;
        private int quantity;

        // Default constructor
        public Game() { }

        // Parameterized constructor assigning all properties
        public Game(int itemNumber, string itemName, double price, double userRating, int quantity)
        {
            this.itemNumber = itemNumber;
            this.itemName = itemName;
            this.price = price;
            this.userRating = userRating;
            this.quantity = quantity;
        }

        // Accessors (getters)
        public int GetItemNumber() { return this.itemNumber; }
        public string GetItemName() { return this.itemName; }
        public double GetPrice() { return this.price; }
        public double GetUserRating() { return this.userRating; }
        public int GetQuantity() { return this.quantity; }

        // Mutators (setters)
        public void SetItemNumber(int itemNumber)
        {
            if (itemNumber >= 1000 && itemNumber <= 9999) {
                this.itemNumber = itemNumber;
            }
            else
            {
                Console.WriteLine("Item number must be 4 digits");
            }
        }

        public void SetItemName(string itemName)
        {
            if (itemName.Length > 0)
            {
                this.itemName = itemName;
            }
            else
            {
                Console.WriteLine("Item must have a name");
            }
        }

        public void SetPrice(double price)
        {
            if (price >= 0)
            {
                this.price = price;
            }
            else
            {
                Console.WriteLine("Price cannot be negative");
            }
        }

        public void SetUserRating(double userRating)
        {
            if (userRating >=0 && userRating <= 5)
            {
                this.userRating = userRating;
            }
            else
            {
                Console.WriteLine("User rating must be between 0 and 5");
            }
        }

        public void SetQuantity(int quantity)
        {
            if (quantity >= 0)
            {
                this.quantity = quantity;
            }
            else
            {
                Console.WriteLine("Quantity cannot be negative");
            }
        }

        // Override ToString() method
        public override string ToString()
        {
            return $"Name: {this.itemName} | Item Number: {this.itemNumber} | Price: {this.price} | User Rating: {this.userRating} | Quantity: {this.quantity}";
        }
    }

    // Input validation class
    internal class Validate
    {
        // Validates the user's numerical choice against a possible range
        public static bool AsMenuInput(string input, int menuRange)
        {
            int choice;

            // Perform initial validations
            if (input == "" || input == null || input.Length > 1) return false;
            if (!int.TryParse(input, out choice)) return false;

            // Check if the choice does not fall within the range
            if (choice < 1 || choice > menuRange) return false;

            return true;
        }

        // Validates a user's string input
        public static bool AsString()
        {
            // todo
            return true;
        }

        // Validates a user's input as a number
        public static bool AsNumber(string input, int digits)
        {
            // Check if input is a number
            if (!int.TryParse(input, out int n)) return false;
            if (n < 0) return false;

            // Do not enforce digit validation if -1 is provided as argument
            if (digits == -1) return true;

            // Lastly, check if the input has a valid amount of digits
            if (input.Length != digits) return false;

            return true;
        }
    }

    // todo perhaps split the gane data-dealing functions into their own Inventory class?

    // Utility class - contains reusable helper methods
    // todo: Note: place any utility and file-related functions here
    internal class Utility
    {
        // Returns all Games within the inventory file
        public static Game[] ReadFileData()
        {
            Game[] games = { };

            // Get the inventory file size
            int size = 0;
            try
            {
                StreamReader reader = new StreamReader("VideoGames.txt");
                string line = "";
                while (line != null)
                {
                    line = reader.ReadLine();
                    if (line == null) break;
                    size++;
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
            }

            // Then, initialize the array with the proper size
            games = new Game[size];

            // Perform a second file pass to extract data into the array
            try
            {
                StreamReader reader = new StreamReader("VideoGames.txt");
                string line = "";
                int i = 0;

                // Loop through the file to read each line
                while (line != null)
                {
                    line = reader.ReadLine();
                    if (line == null) break;

                    // Extract data then add it to the array
                    string[] data = line.Split(",");
                    games[i] = new Game(Convert.ToInt32(data[0]), data[1], Convert.ToDouble(data[2]), Convert.ToDouble(data[3]), Convert.ToInt32(data[4]));
                    i++;
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
            }

            return games;
        }

        // todo: Place a function to write to the inventory file here
        public static void WriteToFile(Game gameData)
        {
            // todo
        }

        // Reusable function that prompts with number validation
        public static string Prompt(int digits = 4, string errorMsg = "")
        {
            // Capture and verify user input
            string input;
            do
            {
                Console.Write("> ");
                input = Console.ReadLine();

                // Return if user cancelled operation
                if (input == "!q")
                {
                    return "";
                }
                // Otherwise, validate the input as a number
                if (!Validate.AsNumber(input, digits))
                {
                    // If fail, warn user with the appropriate message
                    if (errorMsg == "") { Console.WriteLine("[Error]: Please enter a {0}-digit number. To cancel, type !q", digits); }
                    else { Console.WriteLine(errorMsg, digits); }
                }
            } while (!Validate.AsNumber(input, digits));

            return input;
        }
    }

    // Program flow class - interacts with the user with major program branches
    internal class ProgramFlow
    {
        // Add a product to the inventory file
        public static void AddProduct()
        {
            // Get product information from the user
            // todo add validation, add call to Utility.SetGameData() to write to inventory file
            Console.WriteLine("[Add Product]: Please enter the product details.");
            Console.Write("\tProduct name: ");
            string name = Console.ReadLine();
            Console.Write("\tProduct ID (leave blank to auto-generate): ");
            string id = Console.ReadLine();
            Console.Write("\tProduct price: ");
            string price = Console.ReadLine();
            Console.Write("\tProduct rating: ");
            string userRating = Console.ReadLine();
            Console.Write("\tProduct quantity: ");
            string quantity = Console.ReadLine();

            Console.Write("Are you sure you want to add this product? [y/n]");
            string confirmationInput = Console.ReadLine();

            Console.WriteLine();

            // Validate the inputs
            // todo

            // Write (append) to inventory data file
            // todo
        }

        // Search the store inventory based on item number
        public static void SearchByItemNumber()
        {
            Console.WriteLine("[Search] Please enter a product number: ");

            // Capture and verify user input. Return if user has cancelled the operation.
            string input = Utility.Prompt();
            if (input == "") return;

            // Get all game data
            Game[] games = Utility.ReadFileData();

            // Search through the game data based on the input to obtain a match
            for (int i = 0; i < games.Length; i++)
            {
                // If a match was found, display the results and exit this method
                if (games[i].GetItemNumber() == Convert.ToInt32(input))
                {
                    Console.WriteLine("[Result]: " + games[i]);
                    Console.WriteLine();
                    return;
                }
            }

            // If no match found, display an error message, then loop again
            Console.WriteLine("[Error]: Game not found. Please try again later with a valid product ID from our inventory.");
            Console.WriteLine();
        }

        // Search video games in the inventory that are lesser than or equal to the specified price
        // todo, complete this function
        public static void SearchByMaxPrice()
        {
            Console.WriteLine("[Search] Please enter a maximum price to search for: ");

            // Capture and verify user input. Return if user has cancelled.
            string input = Utility.Prompt(-1, "[Error]: Please enter a valid number.");
            if (input == "") return;

            // Print all games that fall within the price range
            Console.WriteLine();
            Console.WriteLine("Results for all games lesser than or equal to ${0}:", input);
            Game[] games = Utility.ReadFileData();
            bool matchFound = false;
            for (int i = 0; i < games.Length; i++)
            {
                if (games[i].GetPrice() <= Convert.ToDouble(input))
                {
                    Console.WriteLine("\t[Result #{0}]: " + games[i], i);
                    matchFound = true;
                }
            }

            // If no match found, then inform the user with an appropriate message
            if (!matchFound) Console.WriteLine("[Info]: No match found. Please try a higher price.");
            Console.WriteLine();
        }

        // todo
        public static void GetInventoryStatistics()
        {
            Console.WriteLine("GetInventoryStatistics()");
            // todo
        }
    }

    // Main execution and entry point
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine($"Hello! Welcome to our video game store!");
            // Program loop
            while (true)
            {
                // Prompt user with the start menu
                Console.WriteLine("[Main Menu]: Please select one of the 5 options:");
                Console.WriteLine("\t1) Add a new product");
                Console.WriteLine("\t2) Search for a product by item number");
                Console.WriteLine("\t3) Search for a product by price");
                Console.WriteLine("\t4) Store statistics");
                Console.WriteLine("\t5) Exit Application");

                Console.Write("> ");
                string input = Console.ReadLine();
                Console.WriteLine();

                // Repeat this loop iteration if the input is invalid
                if (!Validate.AsMenuInput(input, 5))
                {
                    Console.WriteLine("[Error]: Please enter a correct input from 1 through 5.");
                    Console.WriteLine();
                    continue;
                }

                // Branch the program based on user input
                int choice = Convert.ToInt16(input);
                switch (choice)
                {
                    case 1:
                        ProgramFlow.AddProduct();
                        break;
                    case 2:
                        ProgramFlow.SearchByItemNumber();
                        break;
                    case 3:
                        ProgramFlow.SearchByMaxPrice();
                        break;
                    case 4:
                        ProgramFlow.GetInventoryStatistics();
                        break;
                    case 5:
                        Console.WriteLine("Thanks for visiting our store!");
                        Console.ReadKey();

                        // Jump out of the main execution to exit the program
                        return;
                }
            }
        }
    }
}
