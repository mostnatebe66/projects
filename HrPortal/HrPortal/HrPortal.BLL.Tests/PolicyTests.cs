using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using HrPortal.Models.Data;
using HrPortal.Models.Responses;
using HrPortal.BLL;

namespace HrPortal.BLL.Tests
{
    [TestFixture]
    public class PolicyTests
    {
        [Test]
        public void LoadPolicies()
        {
            HrManager manager = PolicyManagerFactory.Create();
            IEnumerable<Policy> policies = manager.LookUpPolicies();
            Assert.AreEqual(5, policies.Count());
        }
    }
}
