using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Models;

namespace FlooringMastery.CLI
{
    public class ConsoleIO
    {
        public void DisplayOrders(List<Order> orderList)
        {
            Console.Clear();
            Console.WriteLine($" Orders on {orderList[0].Date}");
            Console.WriteLine("---------------------------");
            foreach (var order in orderList)
            {
                ShowOrder(order);
                Console.WriteLine();
            }
        }

        public void DisplayNewOrder(Order order)
        {
            Console.Clear();
            Console.WriteLine("Order addition successful");
            Console.WriteLine("-------------------------------");
            ShowOrder(order);
        }

        public void DisplayEditedOrder(Order order)
        {
            Console.Clear();
            Console.WriteLine("Order update successful");
            Console.WriteLine("-------------------------------");
            ShowOrder(order);
        }

        public void ShowOrder (Order order)
        {
            Console.WriteLine($" Order Number:  | {order.OrderId}");
            Console.WriteLine($" Order Date:    | {order.Date}");
            Console.WriteLine($" Customer Name: | {order.CustomerName}");
            Console.WriteLine($" Order State:   | {order.State}");      
            Console.WriteLine($" Product Name:  | {order.ProductName}");
            Console.WriteLine($" Flooring area: | {order.Area}");
            Console.WriteLine($" Material Cost: | {order.MaterialCost}");
            Console.WriteLine($" Labor Cost:    | {order.LaborCost}");
            Console.WriteLine($" Subtotal:      | {order.MaterialCost + order.LaborCost}\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($" State Tax @: {order.State} {(order.TaxRate*100).ToString("#.##")}%: {order.TotalTax.ToString("#.##")}");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($" Order Total: {order.Total}");
            Console.ResetColor();
        }                                                                
    }
}
