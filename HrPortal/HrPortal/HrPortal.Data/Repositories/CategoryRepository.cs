using HrPortal.Models.Data;
using HrPortal.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrPortal.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private static List<Category> _category;

        static CategoryRepository()
        {
            _category = new List<Category>
            {
            new Category { ID = 1, Name = "Payroll"},
            new Category { ID = 2, Name = "Employee Benefits" },
            new Category { ID = 3, Name = "Recruitment" },
            new Category { ID = 4, Name = "Diversity" },
            new Category { ID = 5, Name = "Conduct" },
            };
        }

        public IEnumerable<Category> GetAll()
        {
            return _category;
        }

        public Category Get(string categoryName)
        {
            return _category.FirstOrDefault(c => c.Name == categoryName);
        }

        public void Add(string categoryName)
        {
            Category category = new Category();
            category.Name = categoryName;
            _category.Add(category);
        }

        public void Delete(string categoryName)
        {
            _category.RemoveAll(c => c.Name == categoryName);
        }
    }
}
