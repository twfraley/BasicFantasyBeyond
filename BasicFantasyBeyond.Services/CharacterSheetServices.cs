using BasicFantasyBeyond.Data;
using BasicFantasyBeyond.Models;
using BasicFantasyBeyond.Models.CharacterSheetModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicFantasyBeyond.Services
{
    public class CharacterSheetServices
    {
        private Guid _userID;

        public CharacterSheetServices(Guid userID)
        {
            _userID = userID;
        }

        public bool AddCharacterSheet(Models.CharacterSheetModels.CharacterItem model)
        {
            var entity =
                new Data.CharacterItem()
                {
                    CharacterID = model.CharacterID,
                    ItemID = model.ItemID
                };

            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                ctx.CharacterSheet.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<Item> GetItemsByCharacterID(int characterID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                List<Item> characterSheet = new List<Item>();

                var query = ctx.CharacterSheet.Where(e => e.CharacterID == characterID);

                foreach (var model in query)
                {
                    var listItem = new Item
                    {
                        ItemID = model.ItemID,
                        ItemName = model.Equipment.ItemName,
                        UsableBy = model.Equipment.UsableBy,
                        ItemType = model.Equipment.ItemType,
                        IsEquipped = model.Equipment.IsEquipped,
                        Damage = model.Equipment.Damage,
                        DamageType = model.Equipment.DamageType,
                        ArmorClassBonus = model.Equipment.ArmorClassBonus,
                        ItemNotes = model.Equipment.ItemNotes
                    };

                    AddCharacterItemToList(listItem);
                }

                return characterSheet;

                void AddCharacterItemToList(Item item)
                {
                    characterSheet.Add(item);
                }

            }
        }

        public bool UpdateCharacterItem(Models.CharacterSheetModels.CharacterItem model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.CharacterSheet.Single(e => e.CharacterItemsID == model.CharacterItemsID);

                entity.ItemID = model.ItemID;
                entity.CharacterID = model.CharacterID;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool RemoveItemFromCharacter(int equipmentID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.CharacterSheet.Single(e => e.ItemID == equipmentID);

                ctx.CharacterSheet.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

    }
}
