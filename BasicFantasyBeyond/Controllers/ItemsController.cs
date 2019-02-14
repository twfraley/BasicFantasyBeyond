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
    public class ItemsController : Controller
    {
        public ActionResult Index()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new ItemServices(userID);
            var model = service.GetItems();

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ItemCreate model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var service = ItemServices();

            if (service.CreateEquipment(model))
            {
                TempData["SaveResult"] = "The equipment was created.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "The equipment could not be created.");
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = ItemServices();
            var model = svc.GetItemByID(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = ItemServices();
            var detail = service.GetItemByID(id);
            var model =
                new ItemDetails
                {
                    ItemID = detail.ItemID,
                    ItemName=detail.ItemName,
                    ItemType = detail.ItemType,
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
        public ActionResult Edit(int id, ItemDetails model)
        {
            if (!ModelState.IsValid) return View(model);

            if(model.ItemID != id)
            {
                ModelState.AddModelError("", "Id Missmatch");
                return View(model);
            }

            var service = ItemServices();

            if (service.UpdateItem(model))
            {
                TempData["SaveResult"] = "The equipment was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "The equipment could not be updated.");
            return View(model);
        }


        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = ItemServices();
            var model = svc.GetItemByID(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteEquipment(int id)
        {
            var service = ItemServices();
            
            service.DeleteItem(id);

            TempData["SaveResult"] = "The equipment was deleted";

            return RedirectToAction("Index");
        }

        private ItemServices ItemServices()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new ItemServices(userID);
            return service;
        }    }
}
