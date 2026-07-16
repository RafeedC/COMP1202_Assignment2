using System;
using System.IO;
// testing git
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

    internal class Validate
    {
        // Validates the user's numerical choice against a possible range
        public static bool AsMenuInput(string input, int menuRange)
        {
            int choice;

            // Perform initial validations
            if (input == "" || input == null) return false;
            if (!int.TryParse(input, out choice)) return false;

            // Check if the choice does not fall within the range
            if (choice < 1 || choice > menuRange) return false;

            return true;
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
                Console.WriteLine("Please select one of the 5 options:");
                Console.WriteLine("\t1) Add a new product");
                Console.WriteLine("\t2) Search for a product by item number");
                Console.WriteLine("\t3) Search for a product by price");
                Console.WriteLine("\t4) Store statistics");
                Console.WriteLine("\t5) Exit Application");

                Console.Write("> ");
                string input = Console.ReadLine();

                // Jump out of this iteration if the input is invalid
                if (!Validate.AsMenuInput(input, 5))
                {
                    Console.WriteLine("Please enter a correct input from 1 through 5.");
                    continue;
                }

                // Branch the program based on user input
                int choice = Convert.ToInt16(input);
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
        public static void AddProduct()
        {
            // todo put this somewhere else to read from the video games file
            //try
            //{
            //    StreamReader myFile = new StreamReader("VideoGames.txt");
            //    // Reads the first line of text
            //    string line = myFile.ReadLine();

            //    // Continues to read until you reach the end of file
            //    while (line != null)
            //    {
            //        Console.WriteLine(line);
            //        line = myFile.ReadLine();
            //    }

            //    // Close the file
            //    myFile.Close();
            //    Console.ReadLine();
            //}
            //catch(Exception e)
            //{
            //    Console.WriteLine("Exception: " + e.Message);
            //}
            //finally
            //{
            //    Console.WriteLine("Executing finally block.");
            //}

            // Get product information from the user
            // todo add validation
            Console.WriteLine("Please enter the product details below.");
            Console.Write("\tProduct name: ");
            string name = Console.ReadLine();
            Console.Write("\tProduct ID: ");
            string id = Console.ReadLine();
            Console.Write("\tProduct price: ");
            string price = Console.ReadLine();
            Console.Write("\tProduct rating: ");
            string userRating = Console.ReadLine();
            Console.Write("\tProduct quantity: ");
            string quantity = Console.ReadLine();

            try
            {
                
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }

        public static void SearchByItemNumber()
        {
            Console.WriteLine("SearchByItemNumber()");
            // todo
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
}


