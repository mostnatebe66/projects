using HrPortal.Models.Data;
using HrPortal.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrPortal.Data.Repositories
{
    public class PolicyRepository : IPolicyRepository
    {
        private static List<Policy> _policy;

        static PolicyRepository()
        {
            _policy = new List<Policy>
            {
            new Policy { ID = 1,  Name = "Benefits", Description = "Benefits Are Really Good", CategoryID = "Employee Benefits"},
            new Policy { ID = 2,  Name = "Disciplinary Actions", Description = "Bad Conduct is Bad", CategoryID = "Conduct"},
            new Policy { ID = 3,  Name = "Joining Our Team", Description = "People work here good", CategoryID = "Recruitment"},
            new Policy { ID = 4,  Name = "HR Across the World", Description = "We need money", CategoryID = "Diversity"},
            new Policy { ID = 5,  Name = "Time Cards", Description = "Time to leave and go places for fun", CategoryID = "Payroll"},
            };
        }

        public IEnumerable<Policy> GetAll()
        {
            return _policy;
        }

        public Policy Get(int ID)
        {
            return _policy.FirstOrDefault(p => p.ID == ID);
        }

        public void Add(Policy policy)
        {
            policy.ID = _policy.Max(p => p.ID) + 1;

            _policy.Add(policy);
                    }

        public void Delete(int ID)
        {
            _policy.RemoveAll(p => p.ID == ID);
        }

    }

}
