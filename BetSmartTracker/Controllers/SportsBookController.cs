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
    public class SportsBookController : Controller
    {
        // GET: SportsBook
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new SportsBookService(userId);
            var model = service.GetSportsBooks();

            return View(model);
        }

        // GET CREATE
        public ActionResult Create()
        {
            return View();
        }

        // POST CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SportsBookCreate model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var service = CreateSportsBookService();

            if (service.CreateSportsBook(model))
            {
                TempData["SaveResult"] = $"{model.Name} was added.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", $"{model.Name} was NOT added.");
            return View(model);
        }

        // GET EDIT
        public ActionResult Edit(int id)
        {
            var service = CreateSportsBookService();
            var detail = service.GetSportsBookById(id);
            var model = new SportsBookEdit
            {
                SportsBookId = detail.SportsBookId,
                Name = detail.Name,
            };

            return View(model);
        }

        // POST EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, SportsBookEdit model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.SportsBookId != id)
            {
                ModelState.AddModelError("", "ID not found.");
                return View(model);
            }

            var service = CreateSportsBookService();

            if (service.UpdateSportsBook(model))
            {
                TempData["SaveData"] = $"{model.Name} was updated!";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", $"{model.Name} was NOT updated.");
            return View(model);
        }

        // GET DELETE
        public ActionResult Delete(int id)
        {
            var service = CreateSportsBookService();
            var model = service.GetSportsBookById(id);

            return View(model);
        }

        // POST DELETE
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteSportsBook(int id)
        {
            var service = CreateSportsBookService();
            var model = service.GetSportsBookById(id);
            service.DeleteSportsBook(id);

            TempData["SaveResult"] = $"{model.Name} was deleted.";

            return RedirectToAction("Index");
        }

        // GET DETAILS
        public ActionResult Details(int id)
        {
            var service = CreateSportsBookService();
            var model = service.GetSportsBookById(id);

            return View(model);
        }

        // HELPER METHOD
        public SportsBookService CreateSportsBookService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new SportsBookService(userId);

            return service;
        }
    }
}