using HrPortal.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrPortal.Models.Interfaces
{
    public interface IPolicyRepository
    {
        IEnumerable<Policy> GetAll();
        Policy Get(int ID);
        void Add(Policy policy);
        void Delete(int ID);
    }
}
