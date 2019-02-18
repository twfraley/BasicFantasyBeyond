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

        public ActionResult Details(int id)
        {
            var svc = CharacterSheetServices();
            var model = svc.GenerateCharacterSheet(id);

            return View(model);
        }

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

        public ActionResult AddItems(int id)
        {
            var svc = CharacterSheetServices();
            var model = svc.GetAvailableEquipment(id);

            return View(model);
        }

        public ActionResult UpdateCharacterItem(CharacterItemListItem model, int characterID)
        {
            var service = CharacterSheetServices();

            service.UpdateCharacterItem(model, characterID);

            TempData["SaveResult"] = "Items Updated.";

            return RedirectToAction("Details", new { id = characterID });
        }

        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteCharacterItem(int characterID, int characterItemID)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Details", new { id = characterID });
            }

            var service = CharacterSheetServices();

            service.RemoveItemFromCharacter(characterItemID);

            TempData["SaveResult"] = "Item removed from character.";

            return RedirectToAction("Details",new { id = characterID });
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
