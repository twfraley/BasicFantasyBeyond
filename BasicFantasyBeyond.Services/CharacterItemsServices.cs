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
                var query =
                    ctx.
                    CharacterItems
                    .Select(
                        e =>
                        new CharacterItems
                        {
                            CharacterID = e.CharacterID,
                            ItemID = e.ItemID
                        });
                return query.ToArray();
            }
        }

        public CharacterItems GetCharacterItemsByCharacterID(int characterID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .CharacterItems
                    .Single(e => e.CharacterID == characterID);
                return
                    new CharacterItems
                    {
                        CharacterID = entity.CharacterID,
                        ItemID = entity.ItemID
                    };
            }
        }

        public bool UpdateCharacterItems(CharacterItems model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .CharacterItems
                    .Single(e => e.CharacterID == model.CharacterID);

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
