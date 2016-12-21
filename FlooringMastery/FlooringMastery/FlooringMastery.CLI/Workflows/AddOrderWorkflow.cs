using FlooringMastery.BLL;
using FlooringMastery.Models;
using FlooringMastery.Models.Reponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.CLI.Workflows
{
    public class AddOrderWorkflow
    {
        public void Execute()
        {
            OrderManager manager = OrderManagerFactory.Create();
            bool validName = true;
            bool validState = true;
            bool validDate = true;
            bool validProductName = true;
            bool validArea = true;
            string customerName;
            string state;
            string date;
            string productName;
            decimal area;

            Console.Clear();
            Console.WriteLine("Add an order");
            Console.WriteLine("-------------------");

            do
            {
                Console.WriteLine("Please enter a customer name: ");
                customerName = Console.ReadLine();

                if (customerName == string.Empty)
                {
                    validName = false;
                    Console.WriteLine("Customer must have a name");
                }
                else
                {
                    validName = true;
                }
            } while (!validName);

            do
            {
                Console.WriteLine("Please enter your State: ");
                state = Console.ReadLine().ToUpper();
                bool stateCheck = UserInputValidation.StateValidation(state);
                if (stateCheck == false)
                {
                    validState = false;
                    Console.WriteLine("Please enter state abbreviation (e.g. WI, MN)");
                }
                else if (state.Length == 2)
                {
                    validState = true;
                }
            } while (!validState);

            do
            {
                Console.WriteLine("Please enter the order date in format MMDDYYYY: ");
                date = Console.ReadLine();
                int garbage = 0;
                bool orderDate = Int32.TryParse(date, out garbage);

                if (orderDate && date.Length == 8)
                {
                    validDate = true;
                }
                else
                {
                    validDate = false;
                    Console.WriteLine("Error: Invalid Date Format");
                }
            } while (!validDate);

            do
            {
                Console.WriteLine("Please enter the product type: Carpet, Laminate, Tile, Wood, or Premium Heated Tile. ");

                productName = Console.ReadLine();
                bool productCheck = UserInputValidation.ValidateProduct(productName);
                if (productName == string.Empty)
                {
                    validProductName = false; ;
                    Console.WriteLine("Error you must enter a product name: ");
                }
                else if (productCheck == false)
                {
                    validProductName = false;
                    Console.WriteLine("You must enter a valid Product Name: Carpet, Laminate, Tile, Wood, or Premium Heated Tile");
                }
                else
                {
                    validProductName = true;
                }
            } while (!validProductName);

            do
            {
                Console.WriteLine("Please enter the area of the floor: ");
                string userInput = Console.ReadLine();
                bool validNumber = decimal.TryParse(userInput, out area);

                if (validNumber == false)
                {
                    validArea = false;
                    Console.WriteLine("Please enter a valid number");
                }
                else if (area <= 0)
                {
                    validArea = false;
                    Console.WriteLine("Area must be a positive number");
                }
                else
                {
                    validArea = true;
                }
            } while (!validArea);

            Console.Clear();

            Console.WriteLine($"Customer Name: {customerName}");
            Console.WriteLine($"Order Date: {date}");
            Console.WriteLine($"State: {state}");
            Console.WriteLine($"Product Name: {productName}");
            Console.WriteLine($"Flooring Area {area}");
            Console.WriteLine("--------------------------");
            Console.WriteLine("Would you like to commit order? Y/N");
            bool validKey = false;
            string userInputCommit = string.Empty;
            while (validKey == false)
            {
                userInputCommit = Console.ReadLine().ToUpper();
                if (userInputCommit != "Y" && userInputCommit != "Yes" && userInputCommit != "N" && userInputCommit != "NO")
                {
                    Console.WriteLine("Please select Yes or No");
                }
                else
                {
                    validKey = true;
                }
            }

            if (userInputCommit == "Y" || userInputCommit == "Yes")
            {
                //List<object> userInputs = new List<object>();
                //userInputs.Add(customerName);
                //userInputs.Add(state);
                //userInputs.Add(date);
                //userInputs.Add(productName);
                //userInputs.Add(area);

                Order orderToAdd = new Order();
                orderToAdd.CustomerName = customerName;
                orderToAdd.State = state;
                orderToAdd.Date = date;
                orderToAdd.ProductName = productName;
                orderToAdd.Area = area;

                OrderAddReponse response = manager.CreateNewOrder(orderToAdd);

                if (response.Success)
                {
                    ConsoleIO consoleIO = new ConsoleIO();
                    consoleIO.DisplayNewOrder(response.Order);
                }
                else
                {
                    Console.WriteLine("An error occurred: ");
                    Console.WriteLine(response.Message);
                }
            }
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
    }
}

