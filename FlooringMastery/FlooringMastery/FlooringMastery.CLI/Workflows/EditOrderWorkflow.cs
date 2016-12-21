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
    public class EditOrderWorkflow
    {
        public void Execute()
        {
            OrderManager manager = OrderManagerFactory.Create();

            Console.Clear();
            Console.WriteLine("Edit an order");
            Console.WriteLine("-------------------");

            Order userInputs = new Order();
            bool validState = true;
            bool validDate = true;
            bool validProductName = true;
            bool validArea = true;
            Order lookUpOrder = new Order();
            string customerName;
            string state;
            string date;
            string productName;
            decimal area;
            bool correctDateFormat = false;
            string orderDate = string.Empty;
            string orderID = string.Empty;
            bool validOrderId = false;
            bool validOrder = false;

            while (validOrder == false)
            {
                while (correctDateFormat == false)
                {
                    Console.WriteLine("Enter in a date in MMDDYYYY");
                    int needSomething = 0;
                    orderDate = Console.ReadLine();
                    bool correctFormat = Int32.TryParse(orderDate, out needSomething);

                    if (correctFormat && orderDate.Length == 8)
                    {
                        correctDateFormat = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid Date Format - Please follow simple instructions.");
                    }
                }
                do
                {
                    Console.WriteLine("Enter in order ID: ");
                    orderID = Console.ReadLine();
                    int garbage = 0;
                    bool valid = Int32.TryParse(orderID, out garbage);

                    if (valid == true && garbage > 0)
                    {
                        validOrderId = true;
                    }
                    else
                    {
                        Console.WriteLine("Enter a valid order ID");
                    }
                } while (!validOrderId);

                SingleOrderLookUpResponse response1 = manager.LookUpSingleOrder(orderDate, orderID);
                if (response1.Success)
                {
                    lookUpOrder = response1.order;
                    validOrder = true;
                    Console.WriteLine("Order found. Press any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine($"Error {response1.Message}");
                    validOrder = false;
                }
            }

            Console.WriteLine($"The current customer name is: {lookUpOrder.CustomerName}");
            Console.WriteLine("Update customer name? Press enter to skip: ");
            customerName = Console.ReadLine();

            if (customerName == string.Empty)
            {
                userInputs.CustomerName = (lookUpOrder.CustomerName);
            }
            else
            {
                userInputs.CustomerName = customerName;
            }
            do
            {
                Console.WriteLine($"Current state is {lookUpOrder.State}");
                Console.WriteLine("Update your State or press enter to continue: ");
                state = Console.ReadLine().ToUpper();
                bool stateCheck = UserInputValidation.StateValidation(state);

                if (state == string.Empty)
                {
                    userInputs.State = lookUpOrder.State;
                    validState = true;
                }

                if (stateCheck == false)
                {
                    validState = false;
                    Console.WriteLine("Please enter state abbreviation (e.g. WI, MN)");
                }
                else
                {
                    userInputs.State = state;
                    validState = true;
                }
            } while (!validState);

            do
            {
                Console.WriteLine($"The current order date is {lookUpOrder.Date}");
                Console.WriteLine("Update order date or press enter to continue... ");
                date = Console.ReadLine();
                int garbage = 0;
                bool orderDates = Int32.TryParse(date, out garbage);
                if (date == string.Empty)
                {
                    userInputs.Date = lookUpOrder.Date; 
                    validDate = true;
                }
                else if (orderDates && date.Length == 8)
                {
                    userInputs.Date = date;
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
                Console.WriteLine($"The current product type is: {lookUpOrder.ProductName}");
                Console.WriteLine("Update current product to Carpet, Wood, Tile, Laminate, or Premium Heated Tile. Press enter to continue...");
                productName = Console.ReadLine();
                bool productCheck = UserInputValidation.ValidateProduct(productName);

                if (productName == string.Empty)
                {
                    userInputs.ProductName = lookUpOrder.ProductName;
                    validProductName = true;
                }
                else if (productCheck == false)
                {
                    validProductName = false;
                    Console.WriteLine("Update current product to Carpet, Wood, Tile, Laminate, or Premium Heated Tile. Press enter to continue...");
                }
                else
                {
                    userInputs.ProductName = productName;
                    validProductName = true;
                }
            } while (!validProductName);

            do
            {
                Console.WriteLine($"The current area is {lookUpOrder.Area}: ");
                Console.WriteLine("Update current area or press enter to continue...");
                string userInput = Console.ReadLine();
                bool validNumber = decimal.TryParse(userInput, out area);

                if (userInput == string.Empty)
                {
                    userInputs.Area = lookUpOrder.Area;
                    validArea = true;
                }
                else if (validNumber == false)
                {
                    validArea = false;
                    Console.WriteLine("Please enter a valid number");
                }
                else
                {
                    userInputs.Area = area;
                    validArea = true;
                }
            } while (!validArea);

             //adds old date to newly edited item
            Console.Clear();

            OrderEditResponse response = manager.EditOrder(userInputs, lookUpOrder, orderDate);

            if (response.Success)
            {
                ConsoleIO consoleIO = new ConsoleIO();
                consoleIO.DisplayEditedOrder(response.Order);
            }
            else
            {
                Console.WriteLine("An error occurred: ");
                Console.WriteLine(response.Message);
            }
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
    }
}


