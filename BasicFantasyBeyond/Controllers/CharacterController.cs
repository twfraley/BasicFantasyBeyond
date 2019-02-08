using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BasicFantasyBeyond.Data;
using BasicFantasyBeyond.Models.CharacterModels;
using BasicFantasyBeyond.Services;
using Microsoft.AspNet.Identity;

namespace BasicFantasyBeyond.Controllers
{
    public class CharacterController : Controller
    {
        // GET: Characters
        public ActionResult Index()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new CharacterServices(userID);
            var model = service.GetCharacters();

            return View(model);
        }

        // GET: Characters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Characters/Create
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
                TempData["SaveResult"] = "Your Character was created.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your Character could not be created.");
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CharacterServices();
            Character model = svc.GetCharacterByID(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CharacterServices();
            var detail = service.GetCharacterByID(id);
            var model =
                new CharacterEdit
                {
                    CharacterID = detail.CharacterID,
                    CharacterName = detail.CharacterName,
                    CharacterStr = detail.CharacterStr,
                    CharacterDex = detail.CharacterDex,
                    CharacterCon = detail.CharacterCon,
                    CharacterInt = detail.CharacterInt,
                    CharacterWis = detail.CharacterWis,
                    CharacterCha = detail.CharacterCha,
                    CharacterXP = detail.CharacterXP
                };
            return View(model);
        }

        // POST: Characters/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CharacterEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if(model.CharacterID != id)
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

            TempData["SaveResult"] = "Your note was deleted";

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
