using BasicFantasyBeyond.Models.EquipmentModels;
using BasicFantasyBeyond.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BasicFantasyBeyond.WebAPI.Controllers
{
    [RoutePrefix("api/Items")]
    public class ItemController : ApiController
    {
        [Route("All")]
        public IHttpActionResult GetAll()
        {
            var service = new ItemServices(Guid.Parse(User.Identity.GetUserId()));

            var items = service.GetItems();

            return Ok(items);
        }

        [Route("Single/{id:int}")]
        public IHttpActionResult GetByID(int id)
        {
            var service = new ItemServices(Guid.Parse(User.Identity.GetUserId()));

            var item = service.GetItemByID(id);

            return Ok(item);
        }

        [HttpPost]
        public IHttpActionResult Post(ItemCreate item)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = new ItemServices(Guid.Parse(User.Identity.GetUserId()));

            if (!service.CreateEquipment(item))
                return InternalServerError();

            return Ok();
        }

        [HttpPut]
        public IHttpActionResult Put(ItemDetails item)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = new ItemServices(Guid.Parse(User.Identity.GetUserId()));

            if (!service.UpdateItem(item))
                return InternalServerError();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var service = new ItemServices(Guid.Parse(User.Identity.GetUserId()));

            if (!service.DeleteItem(id))
                return InternalServerError();

            return Ok();
        }
    }
}
