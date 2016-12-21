using FlooringMastery.CLI.Workflows;
using System;

namespace FlooringMastery.UI
{
    public static class Menu
    {
        internal static void Start()
        {
            bool quit = true;

            do
            {
                Console.Clear();
                Console.WriteLine("Flooring Order Application");
                Console.WriteLine("------------------------------");
                Console.WriteLine("1. Display Orders");
                Console.WriteLine("2. Add Order");
                Console.WriteLine("3. Edit Order");
                Console.WriteLine("4. Remove Order");
                Console.WriteLine("5. Exit Flooring Order Application");

                Console.WriteLine();
                Console.WriteLine("Enter a selection");
                
                string userInput = Console.ReadLine();
                
                switch (userInput)
                {
                    case "1":
                        OrderLookUpWorkflow orderDisplayWorkflow = new OrderLookUpWorkflow();
                        orderDisplayWorkflow.Execute();
                        break;
                    case "2":
                        AddOrderWorkflow orderAddWorkflow = new AddOrderWorkflow();
                        orderAddWorkflow.Execute();
                        break;
                    case "3":
                        EditOrderWorkflow orderEditWorkflow = new EditOrderWorkflow();
                        orderEditWorkflow.Execute();
                        break;
                    case "4":
                        DeleteOrderWorkflow orderDeleteWorkflow = new DeleteOrderWorkflow();
                        orderDeleteWorkflow.Execute();
                        break;
                    case "5":
                        quit = false;
                        break;
                    default:
                        break;
                }

            } while (quit);
        }
    }
}