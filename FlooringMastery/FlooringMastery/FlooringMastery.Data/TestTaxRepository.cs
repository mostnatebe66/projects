using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Models;
using FlooringMastery.Models.Interfaces;

namespace FlooringMastery.Data
{
    public class TestTaxRepository : ITaxRepository
    {
        public static List<Tax> _allTaxes = new List<Tax>();

        private static Tax _minnesotaTax = new Tax
        {
            taxRate = .06875M,
            stateAbbreviation = "MN",
            stateName = "Minnesota",
            
        };

        private static Tax _wisconsinTax = new Tax
        {
            taxRate = .05M,
            stateAbbreviation = "WI",
            stateName = "Wisconsin",
        };

        public List<Tax> AddTaxesToList()
        {
            _allTaxes.Add(_minnesotaTax);
            _allTaxes.Add(_wisconsinTax);
            return _allTaxes;
        }
                
        public Tax LoadTax(string state)
        {
            if (state == "MN")
            {
                return _minnesotaTax;
            }
            else
            {
                return _wisconsinTax;
            }
        }
    }
}
