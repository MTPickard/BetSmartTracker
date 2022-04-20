﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetSmart.Data
{
    class Withdraw
    {
        [ForeignKey(nameof(SportsBook))]
        public int SportsBookId { get; set; }
        public SportsBook SportsBook { get; set; }


        [ForeignKey(nameof(Transaction))]
        public int TransactionId { get; set; }
        public Transaction Transaction { get; set; }


        public Guid OwnerId { get; set; }

        [Key]
        public int WithdrawID { get; set; }

        public decimal Amount { get; set; }

        public DateTime Date { get; set; }

        public int Card { get; set; }
    }
}
