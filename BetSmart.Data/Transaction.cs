using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetSmart.Data
{
    class Transaction
    {
        public Guid OwnerId { get; set; }

        public Decimal Amount { get; set; }

        public SportsBook SportsBook { get; set; }
    }
}
