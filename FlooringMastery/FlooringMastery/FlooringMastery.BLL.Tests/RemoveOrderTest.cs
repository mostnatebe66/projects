using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit;
using FlooringMastery.Models.Reponses;
using NUnit.Framework;
using FlooringMastery.Models;

namespace FlooringMastery.BLL.Tests
{
    [TestFixture]
    public class RemoveOrderTest
    {
        [Test]
        public void DeleteExistingOrder()
        {
            string date = "10122016";
            string orderID = "1";
            OrderManager manager = OrderManagerFactory.Create();
            OrderDeleteResponse response = new OrderDeleteResponse();
            response = manager.RemoveOrder(date, orderID);
            bool actual = response.Success;
            bool expected = true;

            //OrderManager manager2 = OrderManagerFactory.Create();
            //List<object> testInputs = new List<object>();
            //testInputs.Add("NathanBoJangles");
            //testInputs.Add("MN");
            //testInputs.Add("10122016");
            //testInputs.Add("Wood");
            //testInputs.Add(100);
            //manager2.CreateNewOrder(testInputs); //used for FileDataTest

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CannotDeleteExistingOrder()
        {
            string date = "10122016";
            string orderID = "2111111111111111112";
            OrderManager manager = OrderManagerFactory.Create();
            OrderDeleteResponse response = new OrderDeleteResponse();

            response = manager.RemoveOrder(date, orderID);
            bool actual = response.Success;
            bool expected = false;
            Assert.AreEqual(actual, expected);
        }

        [Test]
        public void DeleteNewlyAddedOrder()
        {
            OrderManager manager = OrderManagerFactory.Create();
            //List<object> testInputs = new List<object>();
            //testInputs.Add("Hank Fleck");
            //testInputs.Add("CA");
            //testInputs.Add("10132016");
            //testInputs.Add("Wood");
            //testInputs.Add(150M);
            Order order = new Order();
            order.CustomerName = "Hank Fleck";
            order.State = "CA";
            order.Date = "10132016";
            order.ProductName = "Wood";
            order.Area = 150M;
            OrderAddReponse response = manager.CreateNewOrder(order);

            string date = response.Order.Date;
            string orderId = response.Order.OrderId;
            
            OrderManager manager1 = OrderManagerFactory.Create();
            OrderDeleteResponse response1 = new OrderDeleteResponse();
            response1 = manager1.RemoveOrder(date, orderId);
            bool actual = response1.Success;
            bool expected = true;
            Assert.AreEqual(actual, expected);
        }
    }
}
