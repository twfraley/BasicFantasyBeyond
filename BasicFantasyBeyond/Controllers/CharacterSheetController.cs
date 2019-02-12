using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using BasicFantasyBeyond.Services;
using BasicFantasyBeyond.Models.CharcterSheetModels;

namespace BasicFantasyBeyond.Controllers
{
    public class CharacterSheetController : Controller
    {
        public ActionResult Index()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new CharacterServices(userID);
            var model = service.GetCharacters();

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CharacterSheetCreate model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var service = CharacterSheetServices();

            if (service.AddCharacterSheet(model))
            {
                TempData["SaveResult"] = "Your equipment was created.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Your equipment could not be created.");
            return View(model);
        }

        public ActionResult Details (int id)
        {
            var svc = CharacterSheetServices();
            var model = svc.GetCharacterSheetByCharacterID(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CharacterSheetServices();
            var detail = service.GetCharacterSheetByCharacterID(id);
            var model =
                new CharacterSheetModel
                {
                    CharacterItemsID = detail.CharacterItemsID,
                    ItemID = detail.ItemID,
                    CharacterID = detail.CharacterID
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CharacterSheetModel model)
        {
            if (!ModelState.IsValid) return View(model);

            if (
        }

        private CharacterSheetServices CharacterSheetServices()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new CharacterSheetServices(userID);
            return service;
        }
    }
}