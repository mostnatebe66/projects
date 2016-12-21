using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Models;
using FlooringMastery.Models.Interfaces;

namespace FlooringMastery.Data
{
    public class TestOrderRepository : IOrderRepository
    {
        private static List<Order> _allOrders = new List<Order>();
        //{
        //    new Order
        //    {
        //         OrderId = "1",
        //         Date = "11102016",
        //         CustomerName = "JeffBoJangles",
        //         ProductName = "Laminate",
        //         Area = 100,
        //         State = "MN",
        //         MaterialCost = 175,
        //         LaborCost = 210,
        //         Total = 411.47M,
        //    },

        //    new Order
        //    {
        //         OrderId = "2",
        //         Date = "12102016",
        //         CustomerName = "NathanBoJangles",
        //         ProductName = "Premium Heated Tile",
        //         Area = 10000,
        //         State = "WI",
        //         MaterialCost = 18.75M,
        //         LaborCost = 3.00M,
        //         Total = 228375M,
        //    }
        //};

        static TestOrderRepository()
        {
            Order orderOne = new Order();

            orderOne.OrderId = "1";
            orderOne.Date = "10112016";
            orderOne.CustomerName = "";
            orderOne.ProductName = "Laminate";
            orderOne.Product = GetProduct(orderOne.ProductName);
            orderOne.Area = 100;
            orderOne.State = "MN";
            orderOne.TaxRate = GetTaxObject(orderOne.State).taxRate;
            orderOne.MaterialCost = orderOne.CalculateMaterialCost(orderOne.Area, orderOne.Product.CostPerSquareFoot);
            orderOne.LaborCost = orderOne.CalculateLaborCost(orderOne.Area, orderOne.Product.LaborCostPerSquareFoot);
            orderOne.TotalTax = (orderOne.MaterialCost + orderOne.LaborCost) * orderOne.TaxRate;
            orderOne.Total = orderOne.CalculateTotal(orderOne.Product, orderOne.Area, GetTaxObject(orderOne.State));
            _allOrders.Add(orderOne);


            Order orderTwo = new Order();

            orderTwo.OrderId = "2";
            orderTwo.Date = "10122016";
            orderTwo.CustomerName = "JeffBoJangles";
            orderTwo.ProductName = "Heated Premium Tile";
            orderTwo.Product = GetProduct(orderTwo.ProductName);
            orderTwo.Area = 10000;
            orderTwo.State = "WI";
            orderTwo.TaxRate = GetTaxObject(orderTwo.State).taxRate;
            orderTwo.MaterialCost = orderTwo.CalculateMaterialCost(orderTwo.Area, orderTwo.Product.CostPerSquareFoot);
            orderTwo.LaborCost = orderTwo.CalculateLaborCost(orderTwo.Area, orderTwo.Product.LaborCostPerSquareFoot);
            orderTwo.TotalTax = (orderTwo.MaterialCost + orderTwo.LaborCost) * orderTwo.TaxRate;
            orderTwo.Total = orderTwo.CalculateTotal(orderTwo.Product, orderTwo.Area, GetTaxObject(orderTwo.State));
            _allOrders.Add(orderTwo);

        }

        private static Tax GetTaxObject(string state)
        {
            TestTaxRepository taxRepository = new TestTaxRepository();
            List<Tax> taxesList = taxRepository.AddTaxesToList();

            if (state == "MN")
            {
                return taxesList[0];
            }
            else
            {
                return taxesList[1];
            }
        }

        private static Product GetProduct(string productName)
        {
            TestProductRepository productRepo = new TestProductRepository();
            List<Product> productList = productRepo.LoadProducts();

            if (productName == "Carpet")
            {
                return productList[0];
            }
            if (productName == "Laminate")
            {
                return productList[1];
            }
            if (productName == "Tile")
            {
                return productList[2];
            }
            if (productName == "Wood")
            {
                return productList[3];
            }
            if (productName == "Heated Premium Tile")
            {
                return productList[4];
            }
            else
            {
                throw new Exception("Invalid Product Name...");
            }
        }

        public Order AddOrder(Order orderToAdd)
        {          
            var ordersOnSelectDate = _allOrders.Where(o => o.Date == orderToAdd.Date).ToList(); ;

            //newOrder.CustomerName = (string)newOrderInputs[0];
            //newOrder.State = (string)newOrderInputs[1];
            //newOrder.Date = (string)newOrderInputs[2];
            //newOrder.ProductName = (string)newOrderInputs[3];
            //newOrder.Area = (decimal)newOrderInputs[4];
            orderToAdd.OrderId = orderToAdd.GenerateNewId(ordersOnSelectDate, orderToAdd.Date);
            orderToAdd.Product = GetProduct(orderToAdd.ProductName);
            orderToAdd.TaxRate = GetTaxObject(orderToAdd.State).taxRate;
            orderToAdd.MaterialCost = orderToAdd.CalculateMaterialCost(orderToAdd.Area, orderToAdd.Product.CostPerSquareFoot);
            orderToAdd.LaborCost = orderToAdd.CalculateLaborCost(orderToAdd.Area, orderToAdd.Product.LaborCostPerSquareFoot);
            orderToAdd.TotalTax = (orderToAdd.MaterialCost + orderToAdd.LaborCost) * orderToAdd.TaxRate;
            orderToAdd.Total = orderToAdd.CalculateTotal(orderToAdd.Product, orderToAdd.Area, GetTaxObject(orderToAdd.State));

            _allOrders.Add(orderToAdd);

            return orderToAdd;
        }

        public bool DeleteOrder(string date, string orderID)
        {
            foreach (var order in _allOrders)
            {
                if (order.OrderId == orderID)
                {
                    _allOrders.Remove(order);
                    return true;
                }
            }
            return false;
        }

        //For the display method
        public List<Order> LoadAllOrders(string date)
        {
            List<Order> newList = new List<Order>();
            foreach (var order in _allOrders)
            {
                if (order.Date == date)
                {
                    newList.Add(order);
                }
            }
            return newList;
        }

        public Order ChangeOrderContents(Order newOrder, Order oldOrder, string oldDate)
        {

            newOrder.Product = GetProduct(newOrder.ProductName);
            newOrder.TaxRate = GetTaxObject(newOrder.State).taxRate;
            newOrder.MaterialCost = newOrder.CalculateMaterialCost(newOrder.Area, newOrder.Product.CostPerSquareFoot);
            newOrder.LaborCost = newOrder.CalculateLaborCost(newOrder.Area, newOrder.Product.LaborCostPerSquareFoot);
            newOrder.TotalTax = (newOrder.MaterialCost + newOrder.LaborCost) * newOrder.TaxRate;
            newOrder.Total = newOrder.CalculateTotal(newOrder.Product, newOrder.Area, GetTaxObject(newOrder.State));

            bool newDateAndOldDateEqual = string.Equals(oldDate, newOrder.Date);
            if(newDateAndOldDateEqual == false)
            {
                var ordersOnSelectDate = _allOrders.Where(o => o.Date == newOrder.Date).ToList<Order>();
                newOrder.OrderId = newOrder.GenerateNewId(ordersOnSelectDate, newOrder.Date);
            }
            return newOrder;
        }

        public Order LoadOrder(string date, string orderId)
        {
            foreach (var order in _allOrders)
            {
                if (orderId == order.OrderId && date == order.Date)
                {
                    return order;
                }
            }
            return null;
        }
    }
}

