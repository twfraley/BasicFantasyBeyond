using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using BasicFantasyBeyond.Services;
using BasicFantasyBeyond.Models.CharacterSheetModels;

namespace BasicFantasyBeyond.WebAPI.Controllers
{
    [RoutePrefix("api/CharacterSheet")]
    public class CharacterSheetController : ApiController
    {
        // No need to view several character sheets at once, so only GetById is available

        [Route("Single/{id:int}")]
        public IHttpActionResult GetbyID(int id)
        {
            var service = CharacterSheetServices();
            var character = service.GenerateCharacterSheet(id);
            return Ok(character);
        }

        [HttpPost]
        public IHttpActionResult Post(int characterID, int itemID)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CharacterSheetServices();

            if (!service.AddCharacterItem(characterID, itemID))
                return InternalServerError();

            return Ok();
        }

        [HttpPut]
        public IHttpActionResult Put(CharacterItemListItem model, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CharacterSheetServices();

            if(!service.UpdateCharacterItem(model, id))
                return InternalServerError();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var service = CharacterSheetServices();

            if (!service.RemoveItemFromCharacter(id))
                return InternalServerError();

            return Ok();
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
