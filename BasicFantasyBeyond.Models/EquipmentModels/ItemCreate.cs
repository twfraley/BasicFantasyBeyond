using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicFantasyBeyond.Models.EquipmentModels
{
    public enum ItemType { Weapon, Armor, Shield, Gear }
    public enum DamageType { Piercing, Bludgeoning, Slashing }
    [Flags]
    public enum UsableBy
    {
        None = 0,
        Fighter = 1 << 0,
        Cleric = 1 << 1,
        Thief = 1 << 2,
        MagicUser = 1 << 3
    }

    public class ItemCreate
    {
        [Required]
        public string ItemName { get; set; }
        public UsableBy UsableBy { get; set; }
        public ItemType EquipmentType { get; set; }
        public string Damage { get; set; }
        public DamageType? DamageType { get; set; }
        public int? ArmorClassBonus { get; set; }
        public string ItemNotes { get; set; }
    }
}
