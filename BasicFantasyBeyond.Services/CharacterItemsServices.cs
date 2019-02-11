using BasicFantasyBeyond.Data;
using BasicFantasyBeyond.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicFantasyBeyond.Services
{
    class CharacterItemsServices
    {
        private readonly Guid _userID;

        public CharacterItemsServices(Guid userID)
        {
            userID = _userID;
        }

        public bool AddCharacterItems(CharacterItemsCreate model)
        {
            var entity =
                new CharacterItems()
                {
                    CharacterID = model.CharacterID,
                    ItemID = model.ItemID
                };

            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                ctx.CharacterItems.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<CharacterItems> GetCharacterItems()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.CharacterItems.Select(e => new CharacterItems
                {
                    CharacterID = e.CharacterID,
                    ItemID = e.ItemID
                });

                return query.ToArray();
            }
        }

        public IEnumerable<CharacterItemsCreate> GetCharacterItemsByCharacterID(int characterID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                List<CharacterItemsCreate> characterItems = new List<CharacterItemsCreate>();

                var query = ctx.CharacterItems.Where(e => e.CharacterID == characterID);

                foreach (var model in query)
                {
                    var itemList = new CharacterItemsCreate
                    {
                        CharacterID = model.CharacterID,
                        ItemID = model.ItemID
                    };

                    AddCharacterItems(itemList);
                }
                return characterItems;

                void AddCharacterItems(CharacterItemsCreate item)
                {
                    characterItems.Add(item);
                }
            }
        }

        public bool UpdateCharacterItems(CharacterItemsCreate model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.CharacterItems.Single(e => e.CharacterID == model.CharacterID);

                entity.ItemID = model.ItemID;
                entity.CharacterID = model.CharacterID;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool RemoveEquipmentfromCharacter(int equipmentID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.CharacterItems.Single(e => e.ItemID == equipmentID);

                ctx.CharacterItems.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

    }
}
