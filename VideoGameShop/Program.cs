using System;
using System.IO;

namespace VideoGameShop
{
    internal class Game
    {
        // Properties of the Game class
        private int itemNumber;
        private string itemName;
        private double price;
        private double userRating;
        private int quantity;

        // Default constructor
        public Game()
        {
        }

        // Constructor assigning all properties
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

        public double GetPrice()
        {
            return this.price;
        }

        public double GetUserRating()
        {
            return this.userRating;
        }

        public int GetQuantity()
        {
            return this.quantity;
        }

        // Mutators (setters)
        public void SetItemNumber(int itemNumber)
        {
            if (itemNumber >= 1000 && itemNumber <= 9999)
            {
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
            return $"Item Number: {this.itemNumber}\n";
        }
    }


    internal class Program
    {
        public static void Main(string[] args)
        {
            Game g1 = new Game(2493, "Minecraft", 24.95, 4.60, 5);

            Console.WriteLine(g1.ToString());
            int userChoice = 0;

            while (userChoice != 5)
            {
                // Display of the start menu
                Console.WriteLine($"Hello! Welcome to our video game store!\n Please select one of the 5 options:");
                Console.WriteLine("1) Add a new product");
                Console.WriteLine("2) Search for a product by Item#");
                Console.WriteLine("3) Search for a product by price");
                Console.WriteLine("4) Store statistics");
                Console.WriteLine("5) Exit Application");

                userChoice = MenuInputValidator();

                if (userChoice == 1)
                {
                    AddProduct();
                }

            }
            Console.WriteLine("Thanks for visiting the store!");
        }

        public static int MenuInputValidator()
        {
            int userChoice;

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Enter your choice: ");
                string userInput = Console.ReadLine();

                if (int.TryParse(userInput, out userChoice))
                {
                    if (userChoice >= 1 && userChoice <= 5)
                    {
                        return userChoice;
                    }
                    else
                    {
                        Console.WriteLine("Please enter a number between 1 and 5");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input! Please enter a number between 1 and 5.");
                }
            }
        }

        public static void AddProduct()
        {
            try
            {
                StreamReader myFile = new StreamReader("VideoGames.txt");
                // Reads the first line of text
                string line = myFile.ReadLine();

                // Continues to read until you reach the end of file
                while (line != null)
                {
                    Console.WriteLine(line);
                    line = myFile.ReadLine();
                }

                // Close the file
                myFile.Close();
                Console.ReadLine();
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
            
        }
        
    }
}


