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

        public bool AddCharacterItems(CharacterItemsModel model)
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

        public IEnumerable<CharacterItemsModel> GetCharacterItems()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.CharacterItems.Select(e => new CharacterItemsModel
                {
                    CharacterID = e.CharacterID,
                    ItemID = e.ItemID
                });

                return query.ToArray();
            }
        }

        public IEnumerable<CharacterItemsModel> GetCharacterItemsByCharacterID(int characterID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                List<CharacterItemsModel> characterItems = new List<CharacterItemsModel>();

                var query = ctx.CharacterItems.Where(e => e.CharacterID == characterID);

                foreach (var model in query)
                {
                    var itemList = new CharacterItemsModel
                    {
                        CharacterID = model.CharacterID,
                        ItemID = model.ItemID
                    };

                    AddCharacterItems(itemList);
                }
                return characterItems;

                void AddCharacterItems(CharacterItemsModel item)
                {
                    characterItems.Add(item);
                }
            }
        }

        public bool UpdateCharacterItems(CharacterItemsModel model)
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
