using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.BLL;
using FlooringMastery.Models.Reponses;



namespace FlooringMastery.CLI.Workflows
{
    public class OrderLookUpWorkflow
    {
        public void Execute()
        {
            //Make config files and based on the mode choose the correct orderRepository - see SGBank for details account manager
            OrderManager manager = OrderManagerFactory.Create();

            Console.Clear();
            Console.WriteLine("Display orders by date");
            Console.WriteLine("--------------------------");
            bool correctDateFormat = false;
            string orderDate = string.Empty;

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

            LookUpAllOrdersResponse response = manager.LookUpOrdersByDate(orderDate);

            if (response.Success)
            {
                ConsoleIO consoleIO = new ConsoleIO();
                consoleIO.DisplayOrders(response.Order);
            }
            else
            {
                Console.WriteLine("An error occurred: ");
                Console.WriteLine(response.Message);
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

        }
    }
}
