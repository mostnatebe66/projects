using HrPortal.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HrPortal.Models.Data;
using System.IO;

namespace HrPortal.Data.Repositories
{
    public class FileCategoryRepository : ICategoryRepository
    {
        private const string File_Path = @"C:\_repos\bitbucket\nate-betz-individual-work\HrPortal\HrPortal\HrPortal\DataFiles\Categories\Categories.txt";

        public void Add(string categoryName)
        {
            List<Category> categories = GetAll().ToList();
            Category category = new Category();
            category.Name = categoryName;
            categories.Add(category);

            using (StreamWriter sw = new StreamWriter(File_Path, false))
            {
                foreach (var item in categories)
                {
                    sw.WriteLine(item.Name);
                }
            }
        }

        public IEnumerable<Category> GetAll()
        {
            string[] getCategory = File.ReadAllLines(File_Path);
            foreach (var categories in getCategory)
            {
                Category category = new Category();
                category.Name = (categories).Trim();

                yield return category;
            }
        }

        public Category Get(string categoryName) //use get all
        {
            IEnumerable<Category> categories = GetAll();
            foreach (var catItem in categories)
            {
                if (categoryName == catItem.Name)
                {
                    return catItem;
                }
            }
            return null;
        }

        public void Delete(string categoryName)
        {
            List<string> list = File.ReadAllLines(File_Path).ToList();
            File.Delete(File_Path);

            foreach (var line in list)
            {
                if (categoryName == line)
                {
                    list.Remove(line);
                    break;
                }
            }
            string[] lines = list.ToArray();
            File.WriteAllLines(File_Path, lines);
        }
    }
}
