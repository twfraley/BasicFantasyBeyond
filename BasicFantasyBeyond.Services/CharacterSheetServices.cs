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
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                var entity =
                    new CharacterItem()
                    {
                        CharacterID = characterID,
                        ItemID = itemID
                    };

                ctx.CharacterItems.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public CharacterSheetModel GenerateCharacterSheet(int characterID)
        {

            using (var ctx = new ApplicationDbContext())
            {
                var detail = ctx.Characters.Single(e => e.CharacterID == characterID);
                var items = GetItemsByCharacterID(characterID);
                var weaponList = new List<CharacterItemListItem>();
                var equippedWeaponList = new List<CharacterItemListItem>();
                var armorList = new List<CharacterItemListItem>();
                var equippedArmorList = new List<CharacterItemListItem>();
                var gearList = new List<CharacterItemListItem>();

                var strMod = CalculateAbilityModifier(detail.CharacterStr);
                var dexMod = CalculateAbilityModifier(detail.CharacterDex);
                var conMod = CalculateAbilityModifier(detail.CharacterCon);
                var intMod = CalculateAbilityModifier(detail.CharacterInt);
                var wisMod = CalculateAbilityModifier(detail.CharacterWis);
                var chaMod = CalculateAbilityModifier(detail.CharacterCha);

                short characterSpeed = 40;

                foreach (var item in items)
                {
                    if (item.ItemType == ItemType.Weapon)
                    {
                        weaponList.Add(item);
                        if (item.IsEquipped) equippedWeaponList.Add(item);
                    }
                    if (item.ItemType == ItemType.Armor || item.ItemType == ItemType.Shield)
                    {
                        armorList.Add(item);
                        if (item.IsEquipped)
                        {
                            equippedArmorList.Add(item);
                            if (item.ArmorType.Equals(ArmorType.Leather)) characterSpeed = 30;
                            if (item.ArmorType.Equals(ArmorType.Chain)) characterSpeed = 20;
                            if (item.ArmorType.Equals(ArmorType.Plate)) characterSpeed = 20;
                        }
                    }
                    if (item.ItemType == ItemType.Gear)
                    {
                        gearList.Add(item);
                    }
                }

                short characterAC = CalculateAC(equippedArmorList, dexMod);

                int attackBonus = Convert.ToInt32(detail.CharacterAttackBonus);

                foreach (var item in items)
                {
                    if (item.ItemType == ItemType.Weapon)
                    {
                        item.AttackBonus += attackBonus;

                        if (item.WeaponType == WeaponType.Melee)
                        {
                            item.AttackBonus += strMod;
                        }

                        if (item.WeaponType == WeaponType.Ranged)
                        {
                            item.AttackBonus += dexMod;
                        }
                    }
                }

                CharacterSheetModel characterSheet = new CharacterSheetModel()
                {
                    OwnerID = detail.OwnerID,
                    CharacterID = detail.CharacterID,
                    CharacterName = detail.CharacterName,
                    CharacterStr = detail.CharacterStr,
                    StrMod = strMod,
                    CharacterDex = detail.CharacterDex,
                    DexMod = dexMod,
                    CharacterCon = detail.CharacterCon,
                    ConMod = conMod,
                    CharacterInt = detail.CharacterInt,
                    IntMod = intMod,
                    CharacterWis = detail.CharacterWis,
                    WisMod = wisMod,
                    CharacterCha = detail.CharacterCha,
                    ChaMod = chaMod,
                    CharacterRace = detail.CharacterRace,
                    CharacterClass = detail.CharacterClass,
                    CharacterAbilities = detail.CharacterAbilities,
                    CharacterXP = detail.CharacterXP,
                    CharacterLevel = detail.CharacterLevel,
                    CharacterAC = characterAC,
                    CharacterHP = detail.CharacterHP,
                    CharacterAttackBonus = detail.CharacterAttackBonus,
                    CharacterSpeed = characterSpeed,
                    CharacterNotes = detail.CharacterNotes,
                    Items = items,
                    Weapons = weaponList,
                    EquippedWeapons = equippedWeaponList,
                    Armor = armorList,
                    EquippedArmor = equippedArmorList,
                    Gear = gearList
                };

                return characterSheet;
            }
        }


        public IEnumerable<CharacterItemListItem> GetItemsByCharacterID(int characterID)
        {
            List<CharacterItemListItem> itemList = new List<CharacterItemListItem>();

            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.CharacterItems.Where(e => e.CharacterID == characterID);

                foreach (var item in query)
                {
                    var listItem = new CharacterItemListItem
                    {
                        CharacterItemID = item.CharacterItemsID,
                        ItemID = item.ItemID,
                        ItemName = item.Equipment.ItemName,
                        UsableBy = item.Equipment.UsableBy,
                        ItemType = item.Equipment.ItemType,
                        IsEquipped = item.IsEquipped,
                        Damage = item.Equipment.Damage,
                        DamageType = item.Equipment.DamageType,
                        WeaponType = item.Equipment.WeaponType,
                        Size = item.Equipment.Size,
                        ArmorClassBonus = item.Equipment.ArmorClassBonus,
                        ItemNotes = item.Equipment.ItemNotes
                    };

                    itemList.Add(listItem);
                }
            }
            return itemList;
        }

        public bool UpdateCharacterItem(CharacterItemListItem model, int characterID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.CharacterItems.Single(e => e.CharacterItemsID == model.CharacterItemID);

                entity.CharacterItemsID = model.CharacterItemID;
                entity.CharacterID = characterID;
                entity.ItemID = model.ItemID;
                entity.IsEquipped = model.IsEquipped;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool EquipItem(int characterItemID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.CharacterItems.Single(e => e.CharacterItemsID == characterItemID);

                entity.CharacterItemsID = characterItemID;
                entity.IsEquipped = true;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool UnequipItem(int characterItemsID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.CharacterItems.Single(e => e.CharacterItemsID == characterItemsID);

                entity.CharacterItemsID = characterItemsID;
                entity.IsEquipped = false;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool RemoveItemFromCharacter(int characterItemID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.CharacterItems.Single(e => e.CharacterItemsID == characterItemID);

                ctx.CharacterItems.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

        public AddCharacterItemsModel GetAvailableEquipment(int characterID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var character = ctx.Characters.Single(c => c.CharacterID == characterID);
                List<ItemListItem> itemList = new List<ItemListItem>();
                List<ItemListItem> itemsToBeRemoved = new List<ItemListItem>();

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
                            WeaponType = item.WeaponType,
                            Size = item.Size,
                            ArmorClassBonus = item.ArmorClassBonus,
                            ItemNotes = item.ItemNotes
                        };
                        if (!itemList.Contains(listItem)) itemList.Add(listItem);
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
                            WeaponType = item.WeaponType,
                            IsEquipped = item.IsEquipped,
                            Damage = item.Damage,
                            DamageType = item.DamageType,
                            Size = item.Size,
                            ArmorClassBonus = item.ArmorClassBonus,
                            ItemNotes = item.ItemNotes
                        };
                        if (!itemList.Contains(listItem)) itemList.Add(listItem);
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
                            WeaponType = item.WeaponType,
                            IsEquipped = item.IsEquipped,
                            Damage = item.Damage,
                            DamageType = item.DamageType,
                            Size = item.Size,
                            ArmorClassBonus = item.ArmorClassBonus,
                            ItemNotes = item.ItemNotes
                        };
                        if (!itemList.Contains(listItem)) itemList.Add(listItem);
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
                            WeaponType = item.WeaponType,
                            IsEquipped = item.IsEquipped,
                            Damage = item.Damage,
                            DamageType = item.DamageType,
                            Size = item.Size,
                            ArmorClassBonus = item.ArmorClassBonus,
                            ItemNotes = item.ItemNotes
                        };
                        if (!itemList.Contains(listItem)) itemList.Add(listItem);
                    }
                }

                if (character.CharacterRace == CharacterRace.Halfling)
                {
                    foreach (var item in itemList)
                    {
                        if (!item.UsableBy.HasFlag(UsableBy.Halfling))
                        {
                            itemsToBeRemoved.Add(item);
                        }
                    }
                }
                if (character.CharacterRace == CharacterRace.Dwarf)
                {
                    foreach (var item in itemList)
                    {
                        if (!item.UsableBy.HasFlag(UsableBy.Dwarf))
                        {
                            itemsToBeRemoved.Add(item);
                        }
                    }
                }
                if (character.CharacterRace == CharacterRace.Elf)
                {
                    foreach (var item in itemList)
                    {
                        if (!item.UsableBy.HasFlag(UsableBy.Elf))
                        {
                            itemsToBeRemoved.Add(item);
                        }
                    }
                }
                if (character.CharacterRace == CharacterRace.Human)
                {
                    foreach (var item in itemList)
                    {
                        if (!item.UsableBy.HasFlag(UsableBy.Human))
                        {
                            itemsToBeRemoved.Add(item);
                        }
                    }
                }

                foreach (var item in itemsToBeRemoved)
                {
                    itemList.Remove(item);
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

            }
        }

        private short CalculateAbilityModifier(short ability)
        {
            short abilityMod = 0;
            if (ability <= 3) abilityMod = -3;
            if (ability > 3 && ability <= 5) abilityMod = -2;
            if (ability > 5 && ability <= 8) abilityMod = -1;
            if (ability > 8 && ability <= 12) abilityMod = 0;
            if (ability > 12 && ability <= 15) abilityMod = 1;
            if (ability > 15 && ability <= 17) abilityMod = 2;
            if (ability > 17) abilityMod = 3;
            return abilityMod;
        }

        private short CalculateAC(List<CharacterItemListItem> equippedArmorList, short dexMod)
        {
            short ac = 11;
            ac += dexMod;

            List<CharacterItemListItem> noCheatingList = new List<CharacterItemListItem>();

            foreach (var item in equippedArmorList)
            {
                if (!noCheatingList.Contains(item) && item.ArmorClassBonus != null)
                    noCheatingList.Add(item);
            }

            foreach (var item in noCheatingList)
            {
                ac += Convert.ToInt16(item.ArmorClassBonus);
            }

            return ac;
        }

    }
}
