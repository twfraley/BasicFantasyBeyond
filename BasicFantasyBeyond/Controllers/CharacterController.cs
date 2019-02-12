using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BasicFantasyBeyond.Models;
using BasicFantasyBeyond.Models.CharacterModels;
using BasicFantasyBeyond.Services;
using Microsoft.AspNet.Identity;

namespace BasicFantasyBeyond.Controllers
{
    public class CharacterController : Controller
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
        public ActionResult Create(CharacterCreate model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var service = CharacterServices();

            if (service.CreateCharacter(model))
            {
                var id = Guid.Parse(User.Identity.GetUserId());
                int characterID = service.GetLastCharacterIDFromUser(id);

                TempData["SaveResult"] = "Your Character was created.";
                return RedirectToAction("Edit","Character", new { id = characterID});
            }

            ModelState.AddModelError("", "Your Character could not be created.");
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CharacterServices();
            var model = svc.GetCharacterByID(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CharacterServices();
            var detail = service.GetCharacterByID(id);
            var model =
                new CharacterDetails
                {
                    OwnerID = detail.OwnerID,
                    CharacterID = detail.CharacterID,
                    CharacterName = detail.CharacterName,
                    CharacterStr = detail.CharacterStr,
                    CharacterDex = detail.CharacterDex,
                    CharacterCon = detail.CharacterCon,
                    CharacterInt = detail.CharacterInt,
                    CharacterWis = detail.CharacterWis,
                    CharacterCha = detail.CharacterCha,
                    CharacterRace = detail.CharacterRace,
                    CharacterClass = detail.CharacterClass,
                    CharacterAbilities = detail.CharacterAbilities,
                    CharacterXP = detail.CharacterXP,
                    CharacterLevel = detail.CharacterLevel,
                    CharacterAC = detail.CharacterAC,
                    CharacterHP = detail.CharacterHP,
                    CharacterAttackBonus = detail.CharacterAttackBonus,
                    CharacterNotes = detail.CharacterNotes
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CharacterDetails model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.CharacterID != id)
            {
                ModelState.AddModelError("", "Id Missmatch");
                return View(model);
            }

            var service = CharacterServices();

            if (service.UpdateCharacter(model))
            {
                TempData["SaveResult"] = "Your character was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your character could not be updated.");
            return View(model);
        }


        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CharacterServices();
            var model = svc.GetCharacterByID(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCharacter(int id)
        {
            var service = CharacterServices();

            service.DeleteCharacter(id);

            TempData["SaveResult"] = "Your character was deleted";

            return RedirectToAction("Index");
        }

        private CharacterServices CharacterServices()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new CharacterServices(userID);
            return service;
        }
    }
}
