using BetSmart.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetSmart.Models
{
    public class BetListItem
    {
        [ForeignKey(nameof(SportsBook))]
        public int SportsBookId { get; set; }
        public SportsBook SportsBook { get; set; }

        public int BetId { get; set; }

        public string Event { get; set; }

        public decimal Stake { get; set; }


        public int Odds { get; set; }

        [Display(Name = "Profit")]
        public decimal ToWin { get; set; }

        public decimal Total { get; set; }

        public bool DidWin { get; set; }
    }
}
