using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Models;
using System.IO;

namespace FlooringMastery.Models
{
    public class TestProductRepository
    {
        private static List<Product> _allProducts = new List<Product>();

        public List<Product> LoadProducts()
        {
            string File_Path = @"C:\_repos\bitbucket\nate-betz-individual-work\FlooringMastery\FlooringMastery\FlooringMastery.Data\DataFolder\Products.txt";

            string[] products = File.ReadAllLines(File_Path);

            foreach (var line in products)
            {
                if (!line.StartsWith("ProductType"))
                {
                    string[] splitLines = line.Split(',');
                    Product product = new Product();
                    product.Type = splitLines[0];
                    product.CostPerSquareFoot = decimal.Parse(splitLines[1]);
                    product.LaborCostPerSquareFoot = decimal.Parse(splitLines[2]);
                    _allProducts.Add(product);
                }
            }
            return _allProducts;
        }
    }
}
