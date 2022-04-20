using BetSmart.Data;
using BetSmart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetSmart.Services
{
    public class BetService
    {
        private readonly Guid _userId;

        public BetService(Guid userId)
        {
            _userId = userId;
        }


        // CREATE
        public bool Create(int id, BetCreate model)
        {

            var bet = new Bet()
            {
                OwnerId = _userId,
                SportsBookId = id,
                Event = model.Event,
                Stake = model.Stake,
                Odds = model.Odds,
                DidWin = model.DidWin
            };

            ApplicationDbContext _db = new ApplicationDbContext();

            _db.Bets.Add(bet);
            var sportsbook = GetSportsBookByBetSportsBookId(id);

            var betList = sportsbook.Bets;
            betList.Add(bet);
            return _db.SaveChanges() == 1;
        }


        // GET BET BY ID
        public BetDetails GetBetById(int id)
        {
            ApplicationDbContext _db = new ApplicationDbContext();
            var entity = _db
                .Bets
                .Where(e => e.OwnerId == _userId)
                .Single(e => e.BetId == id);

            return new BetDetails
            {
                SportsBookId = entity.SportsBookId,
                BetId = entity.BetId,
                Event = entity.Event,
                Stake = entity.Stake,
                Odds = entity.Odds,
                ToWin = entity.ToWin,
                DidWin = entity.DidWin
            };
        }


        // GET BET BY SPORTSBOOK ID
        public IEnumerable<BetListItem> GetBetBySportsBookId(int id)
        {
            ApplicationDbContext _db = new ApplicationDbContext();
            var model = GetSportsBookByBetSportsBookId(id);

            var query = _db
                .Bets
                .Where(e => e.OwnerId == _userId && e.SportsBookId == id)
                .ToList()
                .Select(
                e => new BetListItem
                {
                    SportsBook = model,
                    SportsBookId = e.SportsBookId,
                    BetId = e.BetId,
                    Event = e.Event,
                    Stake = e.Stake,
                    Odds = e.Odds,
                    ToWin = e.ToWin,
                    Total = e.Total,
                    DidWin = e.DidWin
                });

            return query.ToArray();
        }

        // GET SPORTSBOOK BY BET SPORTSBOOK ID
        public SportsBook GetSportsBookByBetSportsBookId(int id)
        {
            ApplicationDbContext _db = new ApplicationDbContext();
            var sportsBook = _db
                .SportsBooks
                .Single(e => e.OwnerId == _userId && e.SportsBookId == id);

            return sportsBook;
        }


        // UPDATE
        public bool UpdateBet(BetEdit model)
        {
            ApplicationDbContext _db = new ApplicationDbContext();

            var entity = _db
                .Bets
                .Where(e => e.OwnerId == _userId)
                .Single(e => e.BetId == model.BetId);

            var sportsBook = GetSportsBookByBetSportsBookId(entity.SportsBookId);
            sportsBook.Bets.Remove(entity);

            entity.BetId = model.BetId;
            entity.Event = model.Event;
            entity.Stake = model.Stake;
            entity.Odds = model.Odds;

            if (entity.DidWin == true)
            {
                sportsBook.Wins.Add(entity);
            }
            else
            {
                sportsBook.Losses.Add(entity);
            }

            sportsBook.Bets.Add(entity);

            return _db.SaveChanges() == 1;
        }


        // DELETE
        public bool DeleteBet(int id)
        {
            ApplicationDbContext _db = new ApplicationDbContext();

            var entity = _db
                .Bets
                .Where(e => e.OwnerId == _userId)
                .Single(e => e.BetId == id);

            var sportsBook = GetSportsBookByBetSportsBookId(entity.SportsBookId);

            if (entity.DidWin == true)
            {
                sportsBook.Wins.Remove(entity);
            }
            else
            {
                sportsBook.Losses.Remove(entity);
            }

            _db.Bets.Remove(entity);
            sportsBook.Bets.Remove(entity);

            return _db.SaveChanges() == 1;
        }
    }
}
