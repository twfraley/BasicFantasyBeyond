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
    public class EquipmentServices
    {
        private Guid _userID;

        public EquipmentServices(Guid userID)
        {
            _userID = userID;
        }

        public bool CreateEquipment(EquipmentCreate model)
        {
            var entity = new Equipment()
                {
                    ItemName = model.ItemName,
                    EquipmentType = model.EquipmentType,
                    Damage = model.Damage,
                    DamageType = model.DamageType,
                    ArmorClassBonus = model.ArmorClassBonus,
                    ItemNotes = model.ItemNotes
                };

            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                ctx.Equipment.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<EquipmentListItem> GetEquipment()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Equipment.Select(e => new EquipmentListItem
                        {
                            ItemID = e.ItemID,
                            ItemName = e.ItemName,
                            EquipmentType = e.EquipmentType,
                            Damage = e.Damage,
                            DamageType = e.DamageType,
                            ArmorClassBonus = e.ArmorClassBonus,
                            ItemNotes = e.ItemNotes
                        });
                return query.ToArray();
            }
        }

        public EquipmentDetails GetEquipmentByID(int itemID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Equipment.Single(e => e.ItemID == itemID);
                return
                    new EquipmentDetails
                    {
                        ItemID = entity.ItemID,
                        ItemName = entity.ItemName,
                        EquipmentType = entity.EquipmentType,
                        IsEquipped = entity.IsEquipped,
                        Damage = entity.Damage,
                        DamageType = entity.DamageType,
                        ArmorClassBonus = entity.ArmorClassBonus,
                        ItemNotes = entity.ItemNotes
                    };
            }
        }

        public bool UpdateEquipment(EquipmentDetails model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Equipment.FirstOrDefault(e => e.ItemID == model.ItemID);

                entity.ItemID = model.ItemID;
                entity.ItemName = model.ItemName;
                entity.EquipmentType = model.EquipmentType;
                entity.IsEquipped = model.IsEquipped;
                entity.Damage = model.Damage;
                entity.DamageType = model.DamageType;
                entity.ArmorClassBonus = model.ArmorClassBonus;
                entity.ItemNotes = model.ItemNotes;
                
                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteEquipment(int equipmentID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Equipment.Single(e => e.ItemID == equipmentID);

                ctx.Equipment.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

    }
}
