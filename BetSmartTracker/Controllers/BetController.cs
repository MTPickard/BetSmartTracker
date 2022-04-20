using BetSmart.Models;
using BetSmart.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BetSmartTracker.Controllers
{
    [Authorize]
    public class BetController : Controller
    {
        // GET: Bet
        public ActionResult Index(int id)
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new BetService(userId);
            var model = service.GetBetBySportsBookId(id);
            TempData["SportsBookId"] = id;

            return View(model);
        }


        // GET BET CREATE
        public ActionResult Create(int id)
        {
            TempData["SportsBookId"] = id;
            return View();
        }


        // POST BET CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id, BetCreate bet)
        {
            if (!ModelState.IsValid)
            {
                return View(bet);
            }

            var service = CreateBetService();

            if (service.Create(id, bet))
            {
                TempData["SaveResult"] = "Bet was entered.";
                return RedirectToAction("Index", new { id = id });
            }

            ModelState.AddModelError("", "Bet was NOT entered.");
            return View(bet);
        }


        // UPDATE BET GET
        public ActionResult Edit(int id)
        {
            var service = CreateBetService();
            var detail = service.GetBetById(id);
            var model = new BetEdit()
            {
                BetId = detail.BetId,
                Event = detail.Event,
                Stake = detail.Stake,
                Odds = detail.Odds,
            };

            TempData["SportsBookId"] = detail.SportsBookId;

            return View(model);
        }


        // UPDATE BET POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BetEdit model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (id != model.BetId)
            {
                ModelState.AddModelError("", "ID MisMatch");
                return View(model);
            }

            var service = CreateBetService();

            if (service.UpdateBet(model))
            {
                TempData["SaveResult"] = "Bet was successfully updated.";
                return RedirectToAction("Index", new { id = id });
            }

            ModelState.AddModelError("", "ERROR: Bet was NOT updated.");
            return View(model);
        }


        // GET BET DELETE
        public ActionResult Delete(int id)
        {
            var service = CreateBetService();
            var model = service.GetBetById(id);

            TempData["SportsBookId"] = model.SportsBookId;

            return View(model);
        }


        // POST BET DELETE
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteBet(int id)
        {
            var service = CreateBetService();
            service.DeleteBet(id);

            TempData["SaveResult"] = "Bet was deleted.";

            return RedirectToAction("Index", new { id = id });
        }


        // GET DETAILS
        public ActionResult Details(int id)
        {
            var service = CreateBetService();
            var model = service.GetBetById(id);

            TempData["SportsBookId"] = model.SportsBookId;

            return View(model);
        }

        // HELPER METHOD
        public BetService CreateBetService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new BetService(userId);

            return service;
        }
    }
}