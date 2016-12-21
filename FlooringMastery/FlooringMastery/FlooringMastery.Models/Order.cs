using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Models
{
    public class Order
    {
        public Product product;

        public string OrderId { get; set; }
        public string ProductName { get; set; }
        public string CustomerName { get; set; }
        public string Date { get; set; }
        public string State { get; set; }
        public decimal Total { get; set; }
        public Product Product { get; set; }
        public decimal Area { get; set; }
        public decimal TotalTax { get; set; }
        public decimal TaxRate { get; set; }
        public decimal MaterialCost { get; set; }
        public decimal LaborCost { get; set; }

        public decimal CalculateTotal(Product product, decimal area, Tax tax)
        {
            MaterialCost = CalculateMaterialCost(area, product.CostPerSquareFoot);
            LaborCost = CalculateLaborCost(area, product.LaborCostPerSquareFoot);
            decimal totalBeforeTax = MaterialCost + LaborCost;
            decimal totalTax = CalculateTax(totalBeforeTax, tax);

            return Total = totalBeforeTax + totalTax;
        }

        public decimal CalculateLaborCost(decimal area, decimal laborCostPerSquareFoot)
        {
            return area * laborCostPerSquareFoot;
        }

        public decimal CalculateMaterialCost(decimal area, decimal costPerSquareFoot)
        {
            return area * costPerSquareFoot;
        }

        public decimal CalculateTax(decimal totalBeforeTax, Tax tax)
        {
            decimal taxRate = tax.taxRate;
            decimal totalTax = totalBeforeTax * taxRate;
            decimal roundedMoneyValueFormat = decimal.Parse(totalTax.ToString("#.##"));
            return roundedMoneyValueFormat;
        }

        public string GenerateNewId(List<Order> currentOrderList, string date)
        {
            List<int> IdListForDate = new List<int>();
            Random random = new Random();
            foreach (var order in currentOrderList)
            {
                if (order.Date == date)
                {
                    IdListForDate.Add(Int32.Parse(order.OrderId));
                }
            }
            if (IdListForDate.Count != 0)
            {
                int maxId = IdListForDate.Max();
                return (maxId + 1).ToString();
            }
            else
            {
                return "1";
            }
        }
    }
}
