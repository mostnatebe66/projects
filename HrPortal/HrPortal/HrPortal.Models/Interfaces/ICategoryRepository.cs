using HrPortal.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrPortal.Models.Interfaces
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAll();
        Category Get(string categoryName);
        void Add(string categoryName);
        void Delete(string categoryName);
    };
}
