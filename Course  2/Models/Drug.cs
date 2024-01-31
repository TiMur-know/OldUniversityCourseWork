using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course__2.Classes
{
    public class Drug
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public double PriceBuy { get; set; }
        public double PriceSell { get; set; }
        public int Count { get; set; }
        public string Disease { get; set; }
        public string Recipe { get; set; }
        public string Supplier { get; set; }
        public string ExpiryData { get; set; }
        public string CreatedAt { get; set; }
        public string UpdateAt { get; set; }

    }
}
