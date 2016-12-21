using FlooringMastery.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Models;
using System.IO;

namespace FlooringMastery.Data
{
    public class FileOrderRepository : IOrderRepository
    {
        private static List<Order> _allOrders = new List<Order>();
        private static string File_Path = @"C:\_repos\bitbucket\nate-betz-individual-work\FlooringMastery\FlooringMastery\FlooringMastery.Data\OrderDate\";
        private static string ErrorLog_Path = @"C:\_repos\bitbucket\nate-betz-individual-work\FlooringMastery\FlooringMastery\FlooringMastery.Data\ErrorLog\";

        public Order AddOrder(Order orderToAdd)
        {           
            try
            {
                List<Order> currentOrderList = LoadAllOrders((orderToAdd.Date));
                bool fileExists = File.Exists($"{File_Path}Orders_{orderToAdd.Date}.txt");


                orderToAdd.OrderId = orderToAdd.GenerateNewId(currentOrderList, orderToAdd.Date);
                orderToAdd.Product = GetProduct(orderToAdd.ProductName);
                orderToAdd.TaxRate = GetTaxObject(orderToAdd.State).taxRate;
                orderToAdd.MaterialCost = orderToAdd.CalculateMaterialCost(orderToAdd.Area, orderToAdd.Product.CostPerSquareFoot);
                orderToAdd.LaborCost = orderToAdd.CalculateLaborCost(orderToAdd.Area, orderToAdd.Product.LaborCostPerSquareFoot);
                orderToAdd.TotalTax = (orderToAdd.MaterialCost + orderToAdd.LaborCost) * orderToAdd.TaxRate;
                orderToAdd.Total = orderToAdd.CalculateTotal(orderToAdd.Product, orderToAdd.Area, GetTaxObject(orderToAdd.State));

                string listJoin = string.Join("|", orderToAdd.OrderId, orderToAdd.Date, orderToAdd.CustomerName, orderToAdd.State, orderToAdd.TaxRate, orderToAdd.ProductName, orderToAdd.Area, orderToAdd.MaterialCost, orderToAdd.LaborCost, orderToAdd.TotalTax, orderToAdd.Total);


                if (fileExists == true)
                {
                    string[] orders = File.ReadAllLines($"{File_Path}Orders_{orderToAdd.Date}.txt");
                    int size = orders.Length;
                    File.Delete($"{File_Path}Orders_{orderToAdd.Date}.txt");
                    string[] bothOrders = new string[size + 1];
                    for (int i = 0; i < size; i++)
                    {
                        bothOrders[i] = orders[i];
                    }
                    bothOrders[size] = listJoin;
                    File.WriteAllLines($"{File_Path}Orders_{orderToAdd.Date}.txt", bothOrders);
                }
                else if (fileExists == false)
                {
                    string[] toWrite = new string[1];
                    toWrite[0] = listJoin;
                    File.WriteAllLines($"{File_Path}Orders_{orderToAdd.Date}.txt", toWrite);
                }
            }
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("{0}: {1}", DateTime.Now.ToString(), ex.ToString());
                File.WriteAllText($"{ErrorLog_Path}Errors.txt", sb.ToString());
                return null;
            }
            return orderToAdd;
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

        public Order ChangeOrderContents(Order orderToEdit, Order oldOrder, string oldDate)
        {
            try
            {
                orderToEdit.OrderId = oldOrder.OrderId;
                orderToEdit.Product = GetProduct(orderToEdit.ProductName);
                orderToEdit.TaxRate = GetTaxObject(orderToEdit.State).taxRate;
                orderToEdit.MaterialCost = orderToEdit.CalculateMaterialCost(orderToEdit.Area, orderToEdit.Product.CostPerSquareFoot);
                orderToEdit.LaborCost = orderToEdit.CalculateLaborCost(orderToEdit.Area, orderToEdit.Product.LaborCostPerSquareFoot);
                orderToEdit.TotalTax = (orderToEdit.MaterialCost + orderToEdit.LaborCost) * orderToEdit.TaxRate;
                orderToEdit.Total = orderToEdit.CalculateTotal(orderToEdit.Product, orderToEdit.Area, GetTaxObject(orderToEdit.State));
                string editedOrder = string.Join("|", orderToEdit.OrderId, orderToEdit.Date, orderToEdit.CustomerName, orderToEdit.State, orderToEdit.TaxRate, orderToEdit.ProductName, orderToEdit.Area, orderToEdit.MaterialCost, orderToEdit.LaborCost, orderToEdit.TotalTax, orderToEdit.Total);
                bool fileExists = File.Exists($"{File_Path}Orders_{orderToEdit.Date}.txt");
                bool oldAndNewDatesEqual = string.Equals(orderToEdit.Date, oldDate);
                if (oldAndNewDatesEqual == true)
                {
                    string[] currentOrders = File.ReadAllLines($"{File_Path}Orders_{orderToEdit.Date}.txt");
                    for (int i = 0; i < currentOrders.Length; i++)
                    {
                        if (currentOrders[i].StartsWith(orderToEdit.OrderId) == true)
                        {
                            currentOrders[i] = editedOrder;
                            File.Delete($"{File_Path}Orders_{orderToEdit.Date}.txt");
                            File.WriteAllLines($"{File_Path}Orders_{orderToEdit.Date}.txt", currentOrders);
                            break;
                        }
                    }
                }
                else
                {
                    DeleteOrderFromOldFileAndWriteNewFile(oldDate, orderToEdit.OrderId);
                    bool newFileExists = File.Exists($"{File_Path}Orders_{orderToEdit.Date}.txt");
                    if (newFileExists == true)
                    {
                        List<Order> currentOrderList = LoadAllOrders(orderToEdit.Date);
                        orderToEdit.OrderId = orderToEdit.GenerateNewId(currentOrderList, orderToEdit.Date);
                        editedOrder = string.Join("|", orderToEdit.OrderId, orderToEdit.Date, orderToEdit.CustomerName, orderToEdit.State, orderToEdit.TaxRate, orderToEdit.ProductName, orderToEdit.Area, orderToEdit.MaterialCost, orderToEdit.LaborCost, orderToEdit.TotalTax, orderToEdit.Total);
                        string[] currentOrders = File.ReadAllLines($"{File_Path}Orders_{orderToEdit.Date}.txt");
                        int size = currentOrders.Length;
                        File.Delete($"{File_Path}Orders_{orderToEdit.Date}.txt");
                        string[] currentAndNewOrders = new string[size + 1];
                        for (int i = 0; i < size; i++)
                        {
                            currentAndNewOrders[i] = currentOrders[i];
                        }
                        currentAndNewOrders[size] = editedOrder;
                        File.WriteAllLines($"{File_Path}Orders_{orderToEdit.Date}.txt", currentAndNewOrders);
                    }
                    else
                    {
                        string[] toWrite = new string[1];
                        toWrite[0] = editedOrder;
                        File.WriteAllLines($"{File_Path}Orders_{orderToEdit.Date}.txt", toWrite);
                    }
                }
            }
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("{0}: {1}", DateTime.Now.ToString(), ex.ToString());
                File.WriteAllText($"{ErrorLog_Path}Errors.txt", sb.ToString());
                return null;
            }
            return orderToEdit;
        }

        private bool DeleteOrderFromOldFileAndWriteNewFile(string date, string orderID)
        {
            string[] orders = File.ReadAllLines($"{File_Path}Orders_{date}.txt");
            string[] temp = new string[orders.Length - 1];
            for (int i = 0; i < orders.Length; i++)
            {
                if (orders[i].StartsWith(orderID) == true)
                {
                    for (int j = 0; j < orders.Length - 1; j++)
                    {
                        if (j >= i)
                        {
                            temp[j] = orders[j + 1];
                        }
                        else
                        {
                            temp[j] = orders[j];
                        }
                    }
                    File.Delete($"{File_Path}Orders_{date}.txt");
                    File.WriteAllLines($"{File_Path}Orders_{date}.txt", temp);
                    return true;
                }
            }
            return false;
        }

        public bool DeleteOrder(string date, string OrderID)
        {
            try
            {
                bool fileExists = File.Exists($"{File_Path}Orders_{date}.txt");

                if (fileExists)
                {
                    bool success = DeleteOrderFromOldFileAndWriteNewFile(date, OrderID);
                    if (success)
                    {                        
                        return true;
                    }     
                }
            }
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("{0}: {1}", DateTime.Now.ToString(), ex.ToString());
                File.WriteAllText($"{ErrorLog_Path}Errors.txt", sb.ToString());
            }
            return false;
        }

        public List<Order> LoadAllOrders(string date)
        {
            List<Order> orderList = new List<Order>();

            try
            {
                bool fileExists = File.Exists($"{File_Path}Orders_{date}.txt");

                if (fileExists)
                {
                    string[] orders = File.ReadAllLines($"{File_Path}Orders_{date}.txt");

                    foreach (var line in orders)
                    {
                        Order order = new Order();
                        string[] splitLines = line.Split('|');
                        order.OrderId = splitLines[0];
                        order.Date = splitLines[1];
                        order.CustomerName = splitLines[2];
                        order.State = splitLines[3];
                        order.TaxRate = decimal.Parse(splitLines[4]);
                        order.ProductName = splitLines[5];
                        order.Product = GetProduct(order.ProductName);
                        order.Area = decimal.Parse(splitLines[6]);
                        order.MaterialCost = decimal.Parse(splitLines[7]);
                        order.LaborCost = decimal.Parse(splitLines[8]);
                        order.TotalTax = decimal.Parse(splitLines[9]);
                        order.Total = decimal.Parse(splitLines[10]);
                        orderList.Add(order);
                    }
                    return orderList;
                }
                else
                {
                    return orderList;
                }
            }
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("{0}: {1}", DateTime.Now.ToString(), ex.ToString());
                File.WriteAllText($"{ErrorLog_Path}Errors.txt", sb.ToString());
            }
            return null;
        }

        public Order LoadOrder(string date, string orderId)
        {
            try
            {
                bool fileExists = File.Exists($"{File_Path}Orders_{date}.txt");

                if (fileExists)
                {
                    string[] orders = File.ReadAllLines($"{File_Path}Orders_{date}.txt");
                    Order order = new Order();
                    foreach (var line in orders)
                    {
                        string[] splitLines = line.Split('|');
                        if (splitLines[0] == orderId)
                        {
                            order.OrderId = splitLines[0];
                            order.Date = splitLines[1];
                            order.CustomerName = splitLines[2];
                            order.State = splitLines[3];
                            order.TaxRate = decimal.Parse(splitLines[4]);
                            order.ProductName = splitLines[5];
                            order.Product = GetProduct(order.ProductName);
                            order.Area = decimal.Parse(splitLines[6]);
                            order.MaterialCost = decimal.Parse(splitLines[7]);
                            order.LaborCost = decimal.Parse(splitLines[8]);
                            order.TotalTax = decimal.Parse(splitLines[9]);
                            order.Total = decimal.Parse(splitLines[10]);
                        }
                    }
                    return order;
                }
            }
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("{0}: {1}", DateTime.Now.ToString(), ex.ToString());
                File.WriteAllText($"{ErrorLog_Path}Errors.txt", sb.ToString());
                return null;
            }
            Order emptyOrder = new Order();
            return emptyOrder;
        }

        private static Product GetProduct(string productName)
        {
            TestProductRepository productRepo = new TestProductRepository();
            List<Product> productList = productRepo.LoadProducts();

            for (int i = 0; i < productList.Count; i++)
            {
                if (productName == productList[i].Type)
                {
                    return productList[i];
                }
            }
            throw new Exception("Invalid Product Name...");
        }
    }
}
