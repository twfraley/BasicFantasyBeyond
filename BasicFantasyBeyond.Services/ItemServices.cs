using BasicFantasyBeyond.Data;
using BasicFantasyBeyond.Models;
using BasicFantasyBeyond.Models.CharacterModels;
using BasicFantasyBeyond.Models.EquipmentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicFantasyBeyond.Services
{
    public class ItemServices
    {
        private Guid _userID;

        public ItemServices(Guid userID)
        {
            _userID = userID;
        }

        public bool CreateEquipment(ItemCreate model)
        {
            var usableBy = GenerateUsableBy(model);

            var entity = new Item()
            {
                ItemName = model.ItemName,
                UsableBy = usableBy,
                ItemType = model.EquipmentType,
                WeaponType = model.WeaponType,
                Damage = model.Damage,
                DamageType = model.DamageType,
                ArmorClassBonus = model.ArmorClassBonus,
                ArmorType = model.ArmorType,
                ItemNotes = model.ItemNotes
            };

            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                ctx.Items.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<ItemListItem> GetItems()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Items.Select(e => new ItemListItem
                {
                    ItemID = e.ItemID,
                    ItemName = e.ItemName,
                    UsableBy = e.UsableBy,
                    ItemType = e.ItemType,
                    WeaponType = e.WeaponType,
                    Damage = e.Damage,
                    DamageType = e.DamageType,
                    ArmorClassBonus = e.ArmorClassBonus,
                    ArmorType = e.ArmorType,
                    ItemNotes = e.ItemNotes
                });
                return query.ToArray();
            }
        }

        public ItemDetails GetItemByID(int itemID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Items.Single(e => e.ItemID == itemID);
                return
                    new ItemDetails
                    {
                        ItemID = entity.ItemID,
                        ItemName = entity.ItemName,
                        UsableByHuman = entity.UsableBy.HasFlag(UsableBy.Human),
                        UsableByElf = entity.UsableBy.HasFlag(UsableBy.Elf),
                        UsableByHalfling = entity.UsableBy.HasFlag(UsableBy.Halfling),
                        UsableByDwarf = entity.UsableBy.HasFlag(UsableBy.Dwarf),
                        UsableByFighter = entity.UsableBy.HasFlag(UsableBy.Fighter),
                        UsableByCleric = entity.UsableBy.HasFlag(UsableBy.Cleric),
                        UsableByThief = entity.UsableBy.HasFlag(UsableBy.Thief),
                        UsableByMagicUser = entity.UsableBy.HasFlag(UsableBy.MagicUser),
                        UsableBy = entity.UsableBy,
                        ItemType = entity.ItemType,
                        IsEquipped = entity.IsEquipped,
                        WeaponType = entity.WeaponType,
                        Damage = entity.Damage,
                        DamageType = entity.DamageType,
                        ArmorClassBonus = entity.ArmorClassBonus,
                        ArmorType = entity.ArmorType,
                        ItemNotes = entity.ItemNotes
                    };
            }
        }

        public bool UpdateItem(ItemDetails model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var usableBy = GenerateUsableBy(model);

                var entity = ctx.Items.FirstOrDefault(e => e.ItemID == model.ItemID);

                entity.ItemID = model.ItemID;
                entity.ItemName = model.ItemName;
                entity.UsableBy = usableBy;
                entity.ItemType = model.ItemType;
                entity.WeaponType = model.WeaponType;
                entity.IsEquipped = model.IsEquipped;
                entity.Damage = model.Damage;
                entity.DamageType = model.DamageType;
                entity.ArmorClassBonus = model.ArmorClassBonus;
                entity.ArmorType = model.ArmorType;
                entity.ItemNotes = model.ItemNotes;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteItem(int itemID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Items.Single(e => e.ItemID == itemID);

                ctx.Items.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

        private UsableBy GenerateUsableBy(ItemCreate model)
        {
            UsableBy usableBy = UsableBy.None;

            if (model.UsableByHuman) usableBy |= UsableBy.Human;
            if (model.UsableByElf) usableBy |= UsableBy.Elf;
            if (model.UsableByHalfling) usableBy |= UsableBy.Halfling;
            if (model.UsableByDwarf) usableBy |= UsableBy.Dwarf;
            if (model.UsableByFighter) usableBy |= UsableBy.Fighter;
            if (model.UsableByCleric) usableBy |= UsableBy.Cleric;
            if (model.UsableByThief) usableBy |= UsableBy.Thief;
            if (model.UsableByMagicUser) usableBy |= UsableBy.MagicUser;

            return usableBy;
        }

        private UsableBy GenerateUsableBy(ItemDetails model)
        {
            UsableBy usableBy = UsableBy.None;

            if (model.UsableByHuman) usableBy |= UsableBy.Human;
            if (model.UsableByElf) usableBy |= UsableBy.Elf;
            if (model.UsableByHalfling) usableBy |= UsableBy.Halfling;
            if (model.UsableByDwarf) usableBy |= UsableBy.Dwarf;
            if (model.UsableByFighter) usableBy |= UsableBy.Fighter;
            if (model.UsableByCleric) usableBy |= UsableBy.Cleric;
            if (model.UsableByThief) usableBy |= UsableBy.Thief;
            if (model.UsableByMagicUser) usableBy |= UsableBy.MagicUser;

            return usableBy;
        }

    }
}
