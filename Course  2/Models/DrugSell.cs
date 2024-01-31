using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course__2.Classes.Models
{
    public  class DrugSell
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Disease { get; set; }
        public string Supplier { get; set; }
        public double PriceBuy { get; set; }
        public double PriceSell { get; set; }
        public int Count { get; set; }
        
        public string SellAt { get; set; }
    }
}
