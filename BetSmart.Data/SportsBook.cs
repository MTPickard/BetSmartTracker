using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetSmart.Data
{
    public class SportsBook
    {
        public Guid OwnerId { get; set; }

        [Key]
        public int SportsBookId { get; set; }

        public string Name { get; set; }

        public decimal Balance
        {
            get
            {
                decimal balance = 0;

                foreach (var bet in Wins)
                {
                    balance += bet.ToWin;
                }

                return balance;
            }
        }

        public decimal WinLossRatio { get; set; }

        public virtual List<Bet> Bets { get; set; }

        public virtual List<Bet> Wins { get; set; }

        public virtual List<Bet> Losses { get; set; }

    }
}
