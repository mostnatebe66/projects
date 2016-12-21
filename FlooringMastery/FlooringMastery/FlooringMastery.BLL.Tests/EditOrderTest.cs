using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using FlooringMastery.Models.Reponses;
using FlooringMastery.Models;

namespace FlooringMastery.BLL.Tests
{
    [TestFixture]
    public class EditOrderTest
    {
        [TestCase("JeffBoJangles", "WI", "10122016", "Heated Premium Tile", 100, "2", "10122016", true)]
        [TestCase("", "", "10112016", "Laminate", 100, "2", "10112016", false)]
        [TestCase("", "", "10112016", "Laminate", -100, "2", "10112016", false)]     

        //[TestCase("NathanBoJangles", "MN", "00000000", "Wood", 100, "1", "00000000", true)]
        //[TestCase("", "", "00000000", "Laminate", 100, "2", "00000000", false)]
        //[TestCase("NathanBoJangles", "MN", "00000000", "Wood", -100, "1", "00000000", false)] USED FOR FILE TESTING
        public void AddOrder(string customerName, string state, string newDate, string productName, decimal area, string orderId, string date, bool expected)
        {
            OrderManager manager = OrderManagerFactory.Create();
            SingleOrderLookUpResponse response1 = manager.LookUpSingleOrder(date, orderId);

            //List<object> testInputs = new List<object>();
            //testInputs.Add(customerName);
            //testInputs.Add(state);
            //testInputs.Add(newDate);
            //testInputs.Add(productName);
            //testInputs.Add(area);
            //testInputs.Add(date);

            Order newOrder = new Order();
            newOrder.CustomerName = customerName;
            newOrder.State = state;
            newOrder.Date = newDate;
            newOrder.ProductName = productName;
            newOrder.Area = area;
            newOrder.Date = date;


            OrderEditResponse response2 = manager.EditOrder(newOrder, response1.order, date);
            bool actual = response2.Success;
            Assert.AreEqual(actual, expected);
        }
    }
}