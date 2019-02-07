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
        private readonly Guid _userID;

        public EquipmentServices(Guid userID)
        {
            userID = _userID;
        }

        public bool CreateEquipment(EquipmentCreate model)
        {
            var entity =
                new Equipment()
                {
                    ItemName = model.ItemName,

                };

            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                ctx.Equipment.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<Equipment> GetEquipment()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx.
                    Equipment
                    .Select(
                        e =>
                        new Equipment
                        {
                            ItemID = e.ItemID,
                            ItemName = e.ItemName,
                            EquipmentType = e.EquipmentType,
                            IsEquipped = e.IsEquipped,
                            Damage = e.Damage,
                            DamageType = e.DamageType,
                            ArmorClassBonus = e.ArmorClassBonus,
                            ItemNotes = e.ItemNotes
                        });
                return query.ToArray();
            }
        }

        public Equipment GetEquipmentByID(int equipmentID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Equipment
                    .Single(e => e.ItemID == equipmentID);
                return
                    new Equipment
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

        public bool UpdateEquipment(Equipment model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Equipment
                    .Single(e => e.ItemID == model.ItemID);

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
