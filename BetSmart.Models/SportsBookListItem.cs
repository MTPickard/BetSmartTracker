using BetSmart.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetSmart.Models
{
    public class SportsBookListItem
    {
        public int SportsBookId { get; set; }

        public string Name { get; set; }

        public decimal Balance { get; set; }

        public decimal WinLossRatio { get; set; }
    }
}
