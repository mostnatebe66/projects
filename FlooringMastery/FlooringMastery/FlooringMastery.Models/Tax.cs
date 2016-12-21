using System;

namespace FlooringMastery.Models
{
    public class Tax
    {
        public decimal taxRate { get; set; }
        public string stateAbbreviation { get; set; }
        public string stateName { get; set; }
    }
}