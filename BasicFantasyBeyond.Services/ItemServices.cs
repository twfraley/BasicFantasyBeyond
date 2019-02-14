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
            var entity = new Item()
                {
                    ItemName = model.ItemName,
                    UsableBy = model.UsableBy,
                    ItemType = model.EquipmentType,
                    Damage = model.Damage,
                    DamageType = model.DamageType,
                    ArmorClassBonus = model.ArmorClassBonus,
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
                            Damage = e.Damage,
                            DamageType = e.DamageType,
                            ArmorClassBonus = e.ArmorClassBonus,
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
                        UsableBy = entity.UsableBy,
                        ItemType = entity.ItemType,
                        IsEquipped = entity.IsEquipped,
                        Damage = entity.Damage,
                        DamageType = entity.DamageType,
                        ArmorClassBonus = entity.ArmorClassBonus,
                        ItemNotes = entity.ItemNotes
                    };
            }
        }

        public bool UpdateItem(ItemDetails model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Items.FirstOrDefault(e => e.ItemID == model.ItemID);

                entity.ItemID = model.ItemID;
                entity.ItemName = model.ItemName;
                entity.UsableBy = model.UsableBy;
                entity.ItemType = model.ItemType;
                entity.IsEquipped = model.IsEquipped;
                entity.Damage = model.Damage;
                entity.DamageType = model.DamageType;
                entity.ArmorClassBonus = model.ArmorClassBonus;
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

    }
}
