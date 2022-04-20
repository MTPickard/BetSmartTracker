using BetSmart.Data;
using BetSmart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetSmart.Services
{
    public class SportsBookService
    {
        private readonly Guid _userId;

        public SportsBookService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateSportsBook(SportsBookCreate sportsBook)
        {
            ApplicationDbContext _db = new ApplicationDbContext();

            var entity = new SportsBook
            {
                OwnerId = _userId,
                SportsBookId = sportsBook.SportsBookId,
                Name = sportsBook.Name,
            };

            _db.SportsBooks.Add(entity);
            return _db.SaveChanges() == 1;
        }

        public IEnumerable<SportsBookListItem> GetSportsBooks()
        {
            ApplicationDbContext _db = new ApplicationDbContext();
            var query = _db
                .SportsBooks
                .Where(e => e.OwnerId == _userId)
                .ToList()
                .Select(e => new SportsBookListItem
                {
                    SportsBookId = e.SportsBookId,
                    Name = e.Name,
                    Balance = e.Balance,
                    WinLossRatio = e.WinLossRatio
                });

            return query.ToArray();
        }

        public SportsBookDetails GetSportsBookById(int id)
        {
            ApplicationDbContext _db = new ApplicationDbContext();
            var entity = _db
                .SportsBooks
                .Where(e => e.OwnerId == _userId)
                .Single(e => e.SportsBookId == id);

            return new SportsBookDetails
            {
                SportsBookId = entity.SportsBookId,
                Name = entity.Name,
                Balance = entity.Balance,
                WinLossRatio = entity.WinLossRatio
            };
        }

        public bool UpdateSportsBook(SportsBookEdit model)
        {
            ApplicationDbContext _db = new ApplicationDbContext();
            var entity = _db
                .SportsBooks
                .Where(e => e.OwnerId == _userId)
                .Single(e => e.SportsBookId == model.SportsBookId);
            {
                entity.SportsBookId = model.SportsBookId;
                entity.Name = model.Name;
            }

            return _db.SaveChanges() == 1;
        }

        public bool DeleteSportsBook(int id)
        {
            ApplicationDbContext _db = new ApplicationDbContext();
            var entity = _db
                .SportsBooks
                .Where(e => e.OwnerId == _userId)
                .Single(e => e.SportsBookId == id);

            _db.SportsBooks.Remove(entity);
            return _db.SaveChanges() == 1;
        }
    }
}
