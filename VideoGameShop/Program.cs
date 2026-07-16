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

            // Then, check if the input has a valid amount of digits
            if (input.Length != digits) return false;

            return true;
        }
    }

    // Utility class
    internal class Utility
    {
        // Returns a Game that matches from the file based on a search term and search column
        public static Game GetGameData(int searchField, string searchTerm)
        {
            Game gameData = new Game();

            try {
                // Open the inventory file
                StreamReader reader = new StreamReader("VideoGames.txt");

                // Read and process each line until the end
                string line = "";
                while (line != null)
                {
                    // Get data for each line
                    line = reader.ReadLine();

                    // Break the loop if file does contain a line
                    if (line == null) break;

                    // Check if data in the specified field matches the search term
                    string[] data = line.Split(",");
                    if (data[searchField] == searchTerm)
                    {
                        // If game found
                        gameData = new Game(
                            Convert.ToInt32(data[0]),
                            data[1],
                            Convert.ToDouble(data[2]),
                            Convert.ToDouble(data[3]),
                            Convert.ToInt32(data[4])
                        );
                        break;
                    }
                }

                reader.Close();
                return gameData;
            } catch(Exception err)
            {
                Console.WriteLine(err);
                return gameData;
            }
        }
    }

    // Program flow class
    internal class Action
    {
        public static void AddProduct()
        {
            // Get product information from the user
            // todo add validation
            Console.WriteLine("[Add Product]: Please enter the product details, seperated by commas.");
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
            String confirmationInput = Console.ReadLine();

            Console.WriteLine();

            // Validate the inputs
            // todo
        }

        // Search the store inventory based on item number
        public static void SearchByItemNumber()
        {
            // Prompt the user
            Console.WriteLine("[Search] Please enter a product number:");

            // Capture and verify user input
            string input;
            do
            {
                Console.Write("> ");
                input = Console.ReadLine();

                // Return if user cancelled operation
                if (input == "q") {
                    return;
                }
                // Otherwise, validate the input as a number
                if (!Validate.AsNumber(input, 4))
                {
                    Console.WriteLine("[Error]: Please enter a four-digit number (e.g. 0000). To cancel, type 'q'.");
                }
            } while (!Validate.AsNumber(input, 4));

            // Get game data. Check if game has been found.
            Game gameData = Utility.GetGameData(0, input);
            if (gameData.GetItemNumber() == 0)
            {
                Console.WriteLine("[Error]: Game not found. Please try again with a valid product ID from our inventory.");
                Console.WriteLine();
                return;
            }

            // Print the game data if found
            Console.WriteLine("[Result]: " + gameData);
            Console.WriteLine();
        }

        public static void SearchByMaxPrice()
        {
            Console.WriteLine("SearchByMaxPrice()");
            // todo
            
        }

        public static void GetInventoryStatistics()
        {
            Console.WriteLine("GetInventoryStatistics()");
            // todo
        }
    }

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
                        Action.AddProduct();
                        break;
                    case 2:
                        Action.SearchByItemNumber();
                        break;
                    case 3:
                        Action.SearchByMaxPrice();
                        break;
                    case 4:
                        Action.GetInventoryStatistics();
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


