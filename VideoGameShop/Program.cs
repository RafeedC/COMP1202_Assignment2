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
        // todo add the validation method comparisons as a condition!!
        public bool SetItemNumber(int itemNumber)
        {
            if (itemNumber >= 1000 && itemNumber <= 9999) {
                this.itemNumber = itemNumber;
                return true;
            }
            return false;
        }

        public bool SetItemName(string itemName)
        {
            if (itemName.Length > 0)
            {
                this.itemName = itemName;
                return true;
            }
            return false;
        }

        public bool SetPrice(double price)
        {
            if (price >= 0)
            {
                this.price = price;
                return true;
            }
            return false;
        }

        public bool SetUserRating(double userRating)
        {
            if (userRating >= 0 && userRating <= 5)
            {
                this.userRating = userRating;
                return true;
            }
            return false;
        }

        public bool SetQuantity(int quantity)
        {
            if (quantity >= 0)
            {
                this.quantity = quantity;
                return true;
            }
            return false;
        }

        // Override ToString() method
        public override string ToString()
        {
            return $"Name: {this.itemName} | Item Number: {this.itemNumber} | Price: {this.price} | User Rating: {this.userRating} | Quantity: {this.quantity}";
        }
    }

    // Program class
    internal class Program
    {
        // Validation methods
        // Validates the user's numerical choice against a possible range
        static bool ValidateAsMenuInput(string input, int menuRange)
        {
            int choice;

            // Perform initial validations
            if (input == "" || input == null || input.Length > 1) return false;
            if (!int.TryParse(input, out choice)) return false;

            // Check if the choice does not fall within the range
            if (choice < 1 || choice > menuRange) return false;

            return true;
        }

        // Validates a user's input as a number
        static bool ValidateAsNumber(string input, int digits = -1)
        {
            // Check if input is a number
            if (!int.TryParse(input, out int n) || !double.TryParse(input, out double m)) return false;
            if (n < 0) return false;

            // Do not enforce digit length validation if default parameter is used
            if (digits == -1) return true;

            // Lastly, check if the input has a valid amount of digits
            if (input.Length != digits) return false;

            return true;
        }

        // Reusable method that prompts the user for a number, then calls the validation method to check the input
        static int PromptAsNumber(int digits = 4)
        {
            string input = "";
            int num = 0;

            while (!ValidateAsNumber(input, digits))
            {
                Console.Write("> ");
                input = Console.ReadLine();

                // Exit this function if user cancelled operation
                if (input == "q")
                {
                    return num;
                }

                // Otherwise, validate the input as a number
                if (!ValidateAsNumber(input, digits))
                {
                    // If fail, warn user with the appropriate message
                    Console.WriteLine("[Error]: Please enter a valid number with proper length. To cancel, type 'q':", digits);
                }
            }
            num = Convert.ToInt32(input);
            return num;
        }

        // Utility methods
        // Returns all Games within the inventory file
        static Game[] ReadFileData()
        {
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
            Game[] games = new Game[size];

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
                    games[i] = new Game(
                        Convert.ToInt32(data[0]),
                        data[1],
                        Convert.ToDouble(data[2]),
                        Convert.ToDouble(data[3]),
                        Convert.ToInt32(data[4])
                    );
                    i++;
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
            }

            return games;
        }

        // todo - fix this
        static void WriteToFile(Game game)
        {
            try
            {
                StreamWriter writer = new StreamWriter("VideoGames.txt", true);

                // Format a string containing the game information, then append it to file
                string line = $"{game.GetItemNumber()},{game.GetItemName()},{game.GetPrice()},{game.GetUserRating()},{game.GetQuantity()}";
                writer.WriteLine(line);
            } catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
        }

        // Add a product to the inventory file
        static void AddProduct()
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

            Console.Write("Are you sure you want to add this product? [y/n]: ");
            string confirmationInput = Console.ReadLine();

            Console.WriteLine();

            // Validate the inputs
            int invalidInputs = 0;
            if (name == "")
            {
                Console.WriteLine("\t[Invalid input]: Please enter a name");
                invalidInputs++;
            }
            if (!ValidateAsNumber(id))
            {
                Console.WriteLine("\t[Invalid input]: Enter a 4-digit valid ID at or above 1000");
                invalidInputs++;
            }
            if (!ValidateAsNumber(price))
            {
                Console.WriteLine("\t[Invalid input]: Enter a valid price above 0.");
                invalidInputs++;
            }
            // todo make sure that the user can input doubles!!
            if (!ValidateAsNumber(userRating))
            {
                Console.WriteLine("\t[Invalid input]: Enter a valid rating from 0.0 through 5.0");
                invalidInputs++;
            }
            if (invalidInputs > 0)
            {
                Console.WriteLine("[Errors]: {0} errors found. Please try again.", invalidInputs);
                Console.WriteLine();
                return;
            }

            // Create the new game, then write (append) it to inventory data file
            // todo make sure arguments line up with constructor signature
            Game newGame = new Game(
                Convert.ToInt32(id),
                name,
                Convert.ToDouble(price),
                Convert.ToDouble(userRating),
                Convert.ToInt32(quantity)
            );
            Console.WriteLine(newGame);
            //WriteToFile(newGame);
        }

        // Search the store inventory based on item number
        static void SearchByItemNumber()
        {
            Console.WriteLine("[Search] Please enter a 4-digit product number:");

            // Capture and verify user input. Return if user has cancelled the operation.
            int input = PromptAsNumber();
            if (input == 0) return;

            // Get all game data
            Game[] games = ReadFileData();

            // Search through the game data based on the input to obtain a match
            for (int i = 0; i < games.Length; i++)
            {
                // If a match was found, display the results and exit this method
                if (games[i].GetItemNumber() == input)
                {
                    Console.WriteLine("[Result]: " + games[i]);
                    Console.WriteLine();
                    return;
                }
            }

            // If no match found, display an error message, then loop again
            Console.WriteLine("[Error]: Game not found. Please try again next time with a valid product ID from our inventory.");
            Console.WriteLine();
        }

        // Search video games in the inventory that are lesser than or equal to the specified price
        // todo, complete this function
        public static void SearchByMaxPrice()
        {
            Console.WriteLine("[Search] Please enter a maximum price to search for:");

            // Capture and verify user input. Return if user has cancelled.
            int input = PromptAsNumber(-1);
            if (input == 0) return;

            // Print all games that fall within the price range
            Console.WriteLine();
            Console.WriteLine("Results for all games lesser than or equal to ${0}:", input);
            Game[] games = ReadFileData();
            bool matchFound = false;
            for (int i = 0; i < games.Length; i++)
            {
                if (games[i].GetPrice() <= input)
                {
                    Console.WriteLine("\t[Result #{0}]: " + games[i], i);
                    matchFound = true;
                }
            }

            // If no match found, then inform the user with an appropriate message
            if (!matchFound) Console.WriteLine("[Error]: No match found. Please try a higher price.");
            Console.WriteLine();
        }

        // todo
        public static void GetInventoryStatistics()
        {
            Console.WriteLine("GetInventoryStatistics()");
            // todo
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("Hello! Welcome to our video game store!");
            // Program loop
            while (true)
            {
                // Prompt user with the start menu
                Console.WriteLine("[Main Menu]: Please select one of the 5 options:");
                Console.WriteLine("\t1) Add a new product");
                Console.WriteLine("\t2) Search for a product by item number");
                Console.WriteLine("\t3) Search for a product by max price");
                Console.WriteLine("\t4) Store statistics");
                Console.WriteLine("\t5) Exit Application");

                Console.Write("> ");
                string input = Console.ReadLine();
                Console.WriteLine();

                // Repeat this loop iteration if the input is invalid
                if (!ValidateAsMenuInput(input, 5))
                {
                    Console.WriteLine("[Error]: Please enter a correct input from 1 through 5.");
                    Console.WriteLine();
                    continue;
                }

                // Branch the program based on user input
                int choice = Convert.ToInt32(input);
                switch (choice)
                {
                    case 1:
                        AddProduct();
                        break;
                    case 2:
                        SearchByItemNumber();
                        break;
                    case 3:
                        SearchByMaxPrice();
                        break;
                    case 4:
                        GetInventoryStatistics();
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
