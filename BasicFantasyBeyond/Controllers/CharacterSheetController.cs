using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using BasicFantasyBeyond.Services;
using BasicFantasyBeyond.Models.CharacterSheetModels;

namespace BasicFantasyBeyond.Controllers
{
    public class CharacterSheetController : Controller
    {
        public ActionResult Index()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var characterService = new CharacterServices(userID);
            var itemService = new ItemServices(userID);
            var characterSheetService = new CharacterSheetServices(userID);
            var model = characterService.GetCharacters();

            return View(model);
        }

        //public ActionResult Create()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]

        [ActionName("Create")]
        public ActionResult Create(int characterID, int itemID)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("AddItems", new { id = characterID });
            }

            var service = CharacterSheetServices();

            if (service.AddCharacterItem(characterID, itemID))
            {
                TempData["SaveResult"] = "Item added to character";
                return RedirectToAction("Details", new {id = characterID});
            }
            ModelState.AddModelError("", "Item could not be added to character.");
            return RedirectToAction("AddItems");
        }

        public ActionResult Details(int id)
        {
            var svc = CharacterSheetServices();
            var model = svc.GenerateCharacterSheet(id);

            return View(model);
        }

        public ActionResult AddItems(int id)
        {
            var svc = CharacterSheetServices();
            var model = svc.GetAvailableEquipment(id);

            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var service = CharacterSheetServices();
            var model = service.GetItemsByCharacterID(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCharacterItem(int id)
        {
            var service = CharacterSheetServices();

            service.RemoveItemFromCharacter(id);

            TempData["SaveResult"] = "Item removed from character.";

            return RedirectToAction("Index");
        }

        private CharacterSheetServices CharacterSheetServices()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var characterService = new CharacterServices(userID);
            var itemService = new ItemServices(userID);
            var characterSheetService = new CharacterSheetServices(userID);
            return characterSheetService;
        }
    }
}
