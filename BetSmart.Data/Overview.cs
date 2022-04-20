using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetSmart.Data
{
    public class Overview
    {
        [Key]
        public int OverallId { get; set; }

        public decimal Balance { get; set; }

        public decimal WinLoss { get; set; }

        public virtual List<decimal> Withdraws { get; set; }

        public virtual List<decimal> Deposits { get; set; }

        public decimal ProfitLoss { get; set; }
    }
}
