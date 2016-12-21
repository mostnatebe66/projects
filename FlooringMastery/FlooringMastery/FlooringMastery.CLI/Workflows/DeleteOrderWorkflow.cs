using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.BLL;
using FlooringMastery.Models.Reponses;

namespace FlooringMastery.CLI.Workflows
{
    public class DeleteOrderWorkflow
    {
        public void Execute()
        {
            OrderManager manager = OrderManagerFactory.Create();

            Console.Clear();
            Console.WriteLine("Delete an order");
            Console.WriteLine("-------------------");

            bool correctDateFormat = false;
            string orderDate = string.Empty;
            string orderID = string.Empty;
            bool validOrderId = false;

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

            bool areYouSureDelete = false;
            string userInputDelete = string.Empty;
            while (areYouSureDelete == false)
            {
                Console.WriteLine("Are you sure you want to delete this order? Y/N");

                userInputDelete = Console.ReadLine().ToUpper();
                if(userInputDelete != "Y" && userInputDelete != "YES" && userInputDelete != "N" && userInputDelete != "NO")
                {
                    Console.WriteLine("Please choose Yes or No");
                }
                else
                {
                    areYouSureDelete = true; 
                }
            }

            if (userInputDelete == "Y" || userInputDelete == "YES")
            {
                OrderDeleteResponse response = manager.RemoveOrder(orderDate, orderID);

                if (response.Success)
                {
                    Console.WriteLine("Order removed successfully!");
                }
                else
                {
                    Console.WriteLine("An error occurred");
                    Console.WriteLine(response.Message);
                }
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
    }
}