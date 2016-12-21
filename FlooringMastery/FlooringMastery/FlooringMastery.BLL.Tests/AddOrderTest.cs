using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Compatibility;
using NUnit.Framework;
using FlooringMastery.Models.Reponses;
using FlooringMastery.Models;

namespace FlooringMastery.BLL.Tests
{
    [TestFixture]
    public class AddOrderTest
    {
        [TestCase("NateBoJangles", "MN", "10112016", "Laminate", 100, true)]
        [TestCase("", "", "10112016", "Laminate", 100, false)]
        [TestCase("NateBoJangles", "MN", "10122016", "Wood", -100, false)]
        //[TestCase("NateBoJangles", "MN", "00000000", "Wood", -100, false)]
        //[TestCase("NateBoJangles", "MN", "11102016", "Cotton", -100, false)] future tests
        public void AddOrder(string customerName, string inputState, string inputDate, string productName, decimal area, bool expected)
        { 
            OrderManager manager = OrderManagerFactory.Create();
            //List<object> testInputs = new List<object>();
            //testInputs.Add(customerName);
            //testInputs.Add(state);
            //testInputs.Add(date);
            //testInputs.Add(productName);
            Order orderToAdd = new Order();
            orderToAdd.CustomerName = customerName;
            orderToAdd.State = inputState;
            orderToAdd.Date = inputDate;
            orderToAdd.ProductName = productName;
            orderToAdd.Area = area;

            OrderAddReponse response = manager.CreateNewOrder(orderToAdd);
            bool actual = response.Success;
            Assert.AreEqual(expected, actual);
        }
    }
}
