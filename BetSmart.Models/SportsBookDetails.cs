using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetSmart.Models
{
    public class SportsBookDetails
    {
        public int SportsBookId { get; set; }

        public string Name { get; set; }

        public decimal Balance { get; set; }

        public decimal WinLossRatio { get; set; }
    }
}
