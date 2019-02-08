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
                    CharacterName = detail.CharacterName,
                    CharacterStr = detail.CharacterStr,
                    CharacterDex = detail.CharacterDex,
                    CharacterCon = detail.CharacterCon,
                    CharacterInt = detail.CharacterInt,
                    CharacterWis = detail.CharacterWis,
                    CharacterCha = detail.CharacterCha
                };
            return View(model);
        }

        // POST: Characters/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CharacterID,OwnerID,CharacterName,CharacterStr,CharacterDex,CharacterCon,CharacterInt,CharacterWis,CharacterCha,CharacterRace,CharacterClass,CharacterAbilities,CharacterXP,CharacterLevel,CharacterAC,CharacterHP,CharacterAttackBonus,CharacterNotes")] Character character)
        {
            if (ModelState.IsValid)
            {
                db.Entry(character).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(character);
        }

        // GET: Characters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Character character = db.Characters.Find(id);
            if (character == null)
            {
                return HttpNotFound();
            }
            return View(character);
        }

        // POST: Characters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Character character = db.Characters.Find(id);
            db.Characters.Remove(character);
            db.SaveChanges();
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
