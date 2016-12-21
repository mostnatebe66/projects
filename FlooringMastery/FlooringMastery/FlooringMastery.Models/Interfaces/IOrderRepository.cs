using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Models;

namespace FlooringMastery.Models.Interfaces
{
    public interface IOrderRepository
    {
        Order LoadOrder(string date, string orderId);
        List<Order> LoadAllOrders(string date);
        Order AddOrder(Order orderToAdd);
        bool DeleteOrder(string date, string OrderID);
        Order ChangeOrderContents(Order newOrder, Order oldOrder, string oldDate);
    }
}
