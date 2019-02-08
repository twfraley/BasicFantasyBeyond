using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BasicFantasyBeyond.Models;
using BasicFantasyBeyond.Models.EquipmentModels;
using BasicFantasyBeyond.Services;
using Microsoft.AspNet.Identity;

namespace BasicFantasyBeyond.Controllers
{
    public class EquipmentsController : Controller
    {
        public ActionResult Index()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new EquipmentServices(userID);
            var model = service.GetEquipment();

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EquipmentCreate model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var service = EquipmentServices();

            if (service.CreateEquipment(model))
            {
                TempData["SaveResult"] = "Your equipment was created.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your equipment could not be created.");
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = EquipmentServices();
            var model = svc.GetEquipmentByID(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = EquipmentServices();
            var detail = service.GetEquipmentByID(id);
            var model =
                new EquipmentDetails
                {
                    ItemID = detail.ItemID,
                    ItemName=detail.ItemName,
                    EquipmentType = detail.EquipmentType,
                    IsEquipped = detail.IsEquipped,
                    Damage = detail.Damage,
                    DamageType = detail.DamageType,
                    ArmorClassBonus = detail.ArmorClassBonus,
                    ItemNotes = detail.ItemNotes
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EquipmentDetails model)
        {
            if (!ModelState.IsValid) return View(model);

            if(model.ItemID != id)
            {
                ModelState.AddModelError("", "Id Missmatch");
                return View(model);
            }

            var service = EquipmentServices();

            if (service.UpdateEquipment(model))
            {
                TempData["SaveResult"] = "Your equipment was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your equipment could not be updated.");
            return View(model);
        }


        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = EquipmentServices();
            var model = svc.GetEquipmentByID(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteEquipment(int id)
        {
            var service = EquipmentServices();
            
            service.DeleteEquipment(id);

            TempData["SaveResult"] = "Your equipment was deleted";

            return RedirectToAction("Index");
        }

        private EquipmentServices EquipmentServices()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new EquipmentServices(userID);
            return service;
        }    }
}
