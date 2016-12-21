using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using FlooringMastery.Models.Reponses;
using FlooringMastery.BLL;

namespace FlooringMastery.BLL.Tests
{
    [TestFixture]
    public class DisplayOrdersTest
    {
        [Test]
        public void CanDisplayOrdersForValidDate()
        {
            OrderManager manager = OrderManagerFactory.Create();
            LookUpAllOrdersResponse response = manager.LookUpOrdersByDate("10112016");
            Assert.IsNotNull(response.Order);
            Assert.IsTrue(response.Success);
            Assert.AreEqual("10112016", response.Order[0].Date);
        }

        [Test]
        public void CanNotDisplayOrdersForValidDate()
        {
            OrderManager manager = OrderManagerFactory.Create();
            LookUpAllOrdersResponse response = manager.LookUpOrdersByDate("11111111");
            Assert.IsEmpty(response.Order);
            Assert.IsFalse(response.Success);
        }
    }
}
