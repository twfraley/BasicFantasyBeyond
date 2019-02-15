using BasicFantasyBeyond.Data;
using BasicFantasyBeyond.Models;
using BasicFantasyBeyond.Models.CharacterModels;
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

        public bool AddCharacterItem(int characterID, int itemID)
        {
            var entity =
                new CharacterItem()
                {
                    CharacterID = characterID,
                    ItemID = itemID
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

        public AddCharacterItemsModel GetAvailableEquipment(int characterID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var character = ctx.Characters.Single(c => c.CharacterID == characterID);
                List<ItemListItem> itemList = new List<ItemListItem>();

                if (character.CharacterClass == CharacterClass.Fighter)
                {
                    var entity = ctx.Items.Where(e => e.UsableBy.HasFlag(UsableBy.Fighter));
                    foreach (var item in entity)
                    {
                        var listItem = new ItemListItem
                        {
                            ItemID = item.ItemID,
                            ItemName = item.ItemName,
                            UsableBy = item.UsableBy,
                            ItemType = item.ItemType,
                            IsEquipped = item.IsEquipped,
                            Damage = item.Damage,
                            DamageType = item.DamageType,
                            ArmorClassBonus = item.ArmorClassBonus,
                            ItemNotes = item.ItemNotes
                        };
                        AddItemToList(listItem);
                    }
                }
                if (character.CharacterClass == CharacterClass.Cleric)
                {
                    var entity = ctx.Items.Where(e => e.UsableBy.HasFlag(UsableBy.Cleric));
                    foreach (var item in entity)
                    {
                        var listItem = new ItemListItem
                        {
                            ItemID = item.ItemID,
                            ItemName = item.ItemName,
                            UsableBy = item.UsableBy,
                            ItemType = item.ItemType,
                            IsEquipped = item.IsEquipped,
                            Damage = item.Damage,
                            DamageType = item.DamageType,
                            ArmorClassBonus = item.ArmorClassBonus,
                            ItemNotes = item.ItemNotes
                        };
                        AddItemToList(listItem);
                    }
                }
                if (character.CharacterClass == CharacterClass.Thief)
                {
                    var entity = ctx.Items.Where(e => e.UsableBy.HasFlag(UsableBy.Thief));
                    foreach (var item in entity)
                    {
                        var listItem = new ItemListItem
                        {
                            ItemID = item.ItemID,
                            ItemName = item.ItemName,
                            UsableBy = item.UsableBy,
                            ItemType = item.ItemType,
                            IsEquipped = item.IsEquipped,
                            Damage = item.Damage,
                            DamageType = item.DamageType,
                            ArmorClassBonus = item.ArmorClassBonus,
                            ItemNotes = item.ItemNotes
                        };
                        AddItemToList(listItem);
                    }
                }
                if (character.CharacterClass == CharacterClass.MagicUser)
                {
                    var entity = ctx.Items.Where(e => e.UsableBy.HasFlag(UsableBy.MagicUser));
                    foreach (var item in entity)
                    {
                        var listItem = new ItemListItem
                        {
                            ItemID = item.ItemID,
                            ItemName = item.ItemName,
                            UsableBy = item.UsableBy,
                            ItemType = item.ItemType,
                            IsEquipped = item.IsEquipped,
                            Damage = item.Damage,
                            DamageType = item.DamageType,
                            ArmorClassBonus = item.ArmorClassBonus,
                            ItemNotes = item.ItemNotes
                        };
                        AddItemToList(listItem);
                    }
                }

                if (character.CharacterRace == CharacterRace.Halfling)
                {
                    foreach (var item in itemList)
                    {
                        if (!item.UsableBy.HasFlag(UsableBy.Halfling))
                        {
                            RemoveItemFromList(item);
                        }
                    }
                }
                if (character.CharacterRace == CharacterRace.Dwarf)
                {
                    foreach (var item in itemList)
                    {
                        if (!item.UsableBy.HasFlag(UsableBy.Dwarf))
                        {
                            RemoveItemFromList(item);
                        }
                    }
                }
                if (character.CharacterRace == CharacterRace.Elf)
                {
                    foreach (var item in itemList)
                    {
                        if (!item.UsableBy.HasFlag(UsableBy.Elf))
                        {
                            RemoveItemFromList(item);
                        }
                    }
                }
                if (character.CharacterRace == CharacterRace.Human)
                {
                    foreach (var item in itemList)
                    {
                        if (!item.UsableBy.HasFlag(UsableBy.Human))
                        {
                            RemoveItemFromList(item);
                        }
                    }
                }

                AddCharacterItemsModel model = new AddCharacterItemsModel
                {
                    CharacterID = character.CharacterID,
                    OwnerID = character.OwnerID,
                    CharacterName = character.CharacterName,
                    CharacterStr = character.CharacterStr,
                    CharacterDex = character.CharacterDex,
                    CharacterCon = character.CharacterCon,
                    CharacterInt = character.CharacterInt,
                    CharacterWis = character.CharacterWis,
                    CharacterCha = character.CharacterCha,
                    CharacterRace = character.CharacterRace,
                    CharacterClass = character.CharacterClass,
                    CharacterAbilities = character.CharacterAbilities,
                    CharacterXP = character.CharacterXP,
                    CharacterLevel = character.CharacterLevel,
                    CharacterAC = character.CharacterAC,
                    CharacterHP = character.CharacterHP,
                    CharacterAttackBonus = character.CharacterAttackBonus,
                    CharacterNotes = character.CharacterNotes,
                    Items = itemList
                };

                return model;

                void AddItemToList(ItemListItem item)
                {
                    if (!itemList.Contains(item)) itemList.Add(item);
                }

                void RemoveItemFromList(ItemListItem item)
                {
                    if (itemList.Contains(item)) itemList.Remove(item);
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
