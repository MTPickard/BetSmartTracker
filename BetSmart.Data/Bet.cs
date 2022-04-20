using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetSmart.Data
{
    public class Bet
    {
        [ForeignKey(nameof(SportsBook))]
        public int SportsBookId { get; set; }
        public SportsBook SportsBook { get; set; }

        public Guid OwnerId { get; set; }

        [Key]
        public int BetId { get; set; }

        public string Event { get; set; }

        public decimal Stake { get; set; }

        public int Odds { get; set; }

        public decimal ToWin
        {
            get
            {
                if (Odds >= 100)
                {
                    decimal realOdds = (Convert.ToDecimal(Odds) / 100);
                    decimal profit = (Stake * realOdds);
                    return profit;
                }
                else if (Odds <= -100)
                {
                    decimal calc = (Stake * (-1));
                    decimal realOdds = (Convert.ToDecimal(Odds) / 100);
                    decimal profit = (calc / realOdds);
                    return profit;
                }
                else
                    return 0;
            }
        }

        public decimal Total
        {
            get
            {
                return (Stake + ToWin);
            }
        }

        public bool DidWin { get; set; }
    }
}
