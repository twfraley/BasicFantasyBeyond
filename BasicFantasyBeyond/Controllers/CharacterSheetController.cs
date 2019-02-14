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
                TempData["SaveResult"] = "Item added to character";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Item could not be added to character.");
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CharacterSheetServices();
            var model = svc.GetCharacterSheetByCharacterID(id);

            foreach (var item in model)
            {

            }

            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var service = CharacterSheetServices();
            var model = service.GetCharacterSheetByCharacterID(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCharacterSheet(int id)
        {
            var service = CharacterSheetServices();

            service.RemoveItemFromCharacter(id);

            TempData["SaveResult"] = "Your equipment was deleted";

            return RedirectToAction("Index");
        }

        private CharacterSheetServices CharacterSheetServices()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new CharacterSheetServices(userID);
            return service;
        }
    }
}
