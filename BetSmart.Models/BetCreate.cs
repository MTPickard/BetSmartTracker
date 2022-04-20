using BetSmart.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetSmart.Models
{
    public class BetCreate
    {
        [ForeignKey(nameof(SportsBook))]
        public int SportsBookId { get; set; }
        public SportsBook SportsBook { get; set; }

        public string Event { get; set; }

        public decimal Stake { get; set; }

        public int Odds { get; set; }

        public bool DidWin { get; set; }
    }
}
