// Authors: Rafeed Choudhury, Ben Claridad

using System;

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
        public int GetItemNumber()
        {
            return this.itemNumber;
        }
        public string GetItemName()
        {
            return this.itemName;
        }
        public double GetPrice() {
            return this.price;
        }
        public double GetUserRating() {
            return this.userRating;
        }
        public int GetQuantity() {
            return this.quantity;
        }

        // Mutators (setters) - contains base-level validation
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

        // Override of the ToString() method
        public override string ToString()
        {
            return $"Name: {this.itemName} | Item Number: {this.itemNumber} | Price: ${this.price} | User Rating: {this.userRating} | Quantity: {this.quantity}";
        }
    }

    // Program class
    internal class Program
    {
        /**** Validation methods ****/

        // Validates a user's input as an integer
        static bool ValidateAsInt(string input, out int num, int digits = -1)
        {
            // Check if input is a number
            if (!int.TryParse(input, out num)) return false;
            if (num < 0) return false;

            // Do not enforce digit length validation if default parameter is used
            if (digits == -1) return true;

            // Lastly, check if the input has a valid amount of digits
            if (input.Length != digits) return false;

            return true;
        }

        // Validates the user's numerical choice against a possible range
        static bool ValidateAsMenuInput(string input, out int choice, int menuRange)
        {
            // Perform initial validations
            if (!ValidateAsInt(input, out choice)) return false;

            // Check if the choice does not fall within the range
            if (choice < 1 || choice > menuRange) return false;

            return true;
        }

        // Reusable method that prompts the user for a number, then calls the validation method to check the input
        static int PromptAsInt(int digits = -1)
        {
            string input = "";
            int num;

            while (!ValidateAsInt(input, out num, digits))
            {
                Console.Write("> ");
                input = Console.ReadLine();

                // Exit this function if user cancelled operation
                if (input == "q" || input == "Q")
                {
                    return -1;
                }

                // Otherwise, validate the input as a number
                if (!ValidateAsInt(input, out num, digits))
                {
                    // If fail, warn user with the appropriate message
                    Console.WriteLine("[Error]: Please enter a valid number. To cancel, type 'q':");
                }
            }
            return num;
        }

        /**** File methods ****/

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
                reader.Close();
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
                reader.Close();
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
            }

            return games;
        }

        static void WriteToFile(Game game)
        {
            try
            {
                StreamWriter writer = new StreamWriter("VideoGames.txt", true);

                // Format a string containing the game information, then append it to file
                string line = $"\n{game.GetItemNumber()},{game.GetItemName()},{game.GetPrice()},{game.GetUserRating()},{game.GetQuantity()}";
                writer.WriteLine(line);
                writer.Close();
            } catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
        }

        /**** Program flow methods ****/

        // Add a product to the inventory file
        static void AddProduct()
        {
            // Declare required variables
            bool ok;
            string name;
            int id;
            double price;
            double rating;
            int quantity;

            // Load the games inventory
            Game[] games = ReadFileData();

            // Loop until the user enters everything valid
            do
            {
                // Gather user input
                Console.WriteLine("[Add Product]: Please enter the product details.");
                Console.Write("\tProduct name: ");
                name = Console.ReadLine();
                Console.Write("\tProduct ID (leave blank to auto-generate): ");
                string idStr = Console.ReadLine();
                Console.Write("\tProduct price: ");
                string priceStr = Console.ReadLine();
                Console.Write("\tProduct rating: ");
                string userRatingStr = Console.ReadLine();
                Console.Write("\tProduct quantity: ");
                string qtyStr = Console.ReadLine();

                Console.WriteLine();

                // Obtain user confirmation
                string confirmation = "";
                while (confirmation != "y")
                {
                    Console.Write("Are you sure you want to add this product? [y/n]: ");
                    confirmation = Console.ReadLine();

                    // If user has cancelled, quit the operation and return
                    if (confirmation == "n")
                    {
                        Console.WriteLine("Quitting operation...\n");
                        return;
                    }
                }

                // Start performing validation on inputs
                ok = true;
                // Name validation - must expect non-empty string
                if (name == "")
                {
                    Console.WriteLine("\t[Error]: Enter a product name. Press 'q' anytime to cancel.");
                    ok = false;
                }
                // ID validation - optional, but when entered must expect int with 4 digits
                if (!ValidateAsInt(idStr, out id, 4) && idStr != "")
                {
                    Console.WriteLine("[Error]: Enter a valid 4-digit ID between 1000 through 9999.");
                    ok = false;
                }
                // Price validation - must expect double equal to or above 0
                if (!Double.TryParse(priceStr, out price) || price < 0)
                {
                    Console.WriteLine("[Error]: Enter a valid number for price");
                    ok = false;
                }
                // Rating validation - must expect double between [0.0, 5.0]
                Double.TryParse(userRatingStr, out rating);
                if (rating < 0 || rating > 5)
                {
                    Console.WriteLine("[Error]: Enter a valid rating between 0.0 through 5.0.");
                    ok = false;
                }
                // Quantity validation - must expect int
                if (!int.TryParse(qtyStr, out quantity) || quantity < 0)
                {
                    Console.WriteLine("[Error]: Quantity must be a whole number above 0.");
                    ok = false;
                }

                // Check if user-inputted id is unique
                for (int i = 0; i < games.Length; i++)
                {
                    if (games[i].GetItemNumber() == id)
                    {
                        Console.WriteLine("[Error]: Game ID {0} is not unique. Please enter another ID.", id);
                        ok = false;
                    }
                }
                Console.WriteLine();

            } while (!ok);

            // Auto-generate an id if none was inputted
            if (id == 0)
            {
                bool isUnique = true;
                do
                {
                    // Generate a new id
                    id = new Random().Next(1000, 9999);

                    // Check for uniqueness
                    for (int i = 0; i < games.Length; i++)
                    {
                        if (games[i].GetItemNumber() == id)
                        {
                            isUnique = false;
                        }
                    }
                } while (!isUnique);
            }

            // Create a new game Object
            Game newGame = new Game(
                Convert.ToInt32(id),
                name,
                Convert.ToDouble(price),
                Convert.ToDouble(rating),
                Convert.ToInt32(quantity)
            );
            Console.WriteLine("New game created.");
            Console.WriteLine(newGame + "\n");

            // Add the new game as a record to the inventory file
            WriteToFile(newGame);
        }

        // Search the store inventory based on item number
        static void SearchByItemNumber()
        {
            Console.WriteLine("[Search] Please enter a 4-digit product number. Press 'q' anytime to cancel:");

            // Capture and verify user input. Return if user has cancelled the operation.
            int input = PromptAsInt(4);
            if (input == -1) return;

            // Get all game data
            Game[] games = ReadFileData();

            // Search through the game data based on the input to obtain a match
            for (int i = 0; i < games.Length; i++)
            {
                // If a match was found, display the results and exit this method
                if (games[i].GetItemNumber() == input)
                {
                    Console.WriteLine("\n[Result]: " + games[i] + "\n");
                    return;
                }
            }

            // If no match found, display an error message, then loop again
            Console.WriteLine("[Error]: Game not found. Please try again next time with a valid product ID from our inventory.\n");
        }

        // Search video games in the inventory that are lesser than or equal to the specified price
        public static void SearchByMaxPrice()
        {
            Console.WriteLine("[Search] Please enter a maximum price (without decimals) to search for. Press 'q' anytime to cancel:");

            // Capture and verify user input. Return if user has cancelled.
            int input = PromptAsInt();
            if (input == -1) return;

            // Print all games that fall within the price range
            Console.WriteLine("\nResults for all games lesser than or equal to ${0}:", input);
            Game[] games = ReadFileData();
            bool matchFound = false;
            for (int i = 0; i < games.Length; i++)
            {
                if (games[i].GetPrice() <= input)
                {
                    Console.WriteLine("\t[Result #{0}]: " + games[i], i + 1);
                    matchFound = true;
                }
            }

            // If no match found, then inform the user with an appropriate message
            if (!matchFound) Console.WriteLine("[Error]: No match found. Please try again with a higher price.");
            Console.WriteLine();
        }

        // todo
        public static void GetInventoryStatistics()
        {
            Console.WriteLine("GetInventoryStatistics()");

            Game[] games = ReadFileData();
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
                if (!ValidateAsMenuInput(input, out int choice, 5))
                {
                    Console.WriteLine("[Error]: Please enter a correct input from 1 through 5.\n");
                    continue;
                }

                // Branch the program based on user input
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
                        Console.WriteLine("Thanks for visiting our store! Press any key to quit application...");
                        Console.ReadKey();

                        // Jump out of the main execution to exit the program
                        return;
                }
            }
        }
    }
}
