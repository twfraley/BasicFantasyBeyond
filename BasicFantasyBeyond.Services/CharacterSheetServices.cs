using BasicFantasyBeyond.Data;
using BasicFantasyBeyond.Models;
using BasicFantasyBeyond.Models.CharacterSheetModels;
using BasicFantasyBeyond.Models.EquipmentModels;
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

        public bool AddCharacterItem(CharacterItemModel model)
        {
            var entity =
                new CharacterItem()
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

        public CharacterSheetModel GenerateCharacterSheet(int characterID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var detail = ctx.Characters.Single(e => e.CharacterID == characterID);
                var items = GetItemsByCharacterID(characterID);

                CharacterSheetModel characterSheet = new CharacterSheetModel()
                {
                    OwnerID = detail.OwnerID,
                    CharacterID = detail.CharacterID,
                    CharacterName = detail.CharacterName,
                    CharacterStr = detail.CharacterStr,
                    CharacterDex = detail.CharacterDex,
                    CharacterCon = detail.CharacterCon,
                    CharacterInt = detail.CharacterInt,
                    CharacterWis = detail.CharacterWis,
                    CharacterCha = detail.CharacterCha,
                    CharacterRace = detail.CharacterRace,
                    CharacterClass = detail.CharacterClass,
                    CharacterAbilities = detail.CharacterAbilities,
                    CharacterXP = detail.CharacterXP,
                    CharacterLevel = detail.CharacterLevel,
                    CharacterAC = detail.CharacterAC,
                    CharacterHP = detail.CharacterHP,
                    CharacterAttackBonus = detail.CharacterAttackBonus,
                    CharacterNotes = detail.CharacterNotes,
                    Items = items
                };

                return characterSheet;
            }
        }

        public IEnumerable<ItemListItem> GetItemsByCharacterID(int characterID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                List<ItemListItem> itemList = new List<ItemListItem>();

                var query = ctx.CharacterSheet.Where(e => e.CharacterID == characterID);

                foreach (var model in query)
                {
                    var listItem = new ItemListItem
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

                return itemList;

                void AddCharacterItemToList(ItemListItem item)
                {
                    itemList.Add(item);
                }

            }
        }


        public bool UpdateCharacterItem(CharacterItemModel model)
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
                var entity = ctx.CharacterSheet.Single(e => e.CharacterItemsID == equipmentID);

                ctx.CharacterSheet.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

    }
}
