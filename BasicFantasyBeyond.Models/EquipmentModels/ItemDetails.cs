using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicFantasyBeyond.Models.EquipmentModels
{
    public class ItemDetails
    {
        public int ItemID { get; set; }
        [Required]
        public string ItemName { get; set; }
        public bool UsableByHuman { get; set; }
        public bool UsableByElf { get; set; }
        public bool UsableByHalfling { get; set; }
        public bool UsableByDwarf { get; set; }
        public bool UsableByFighter { get; set; }
        public bool UsableByCleric { get; set; }
        public bool UsableByThief { get; set; }
        public bool UsableByMagicUser { get; set; }
        public UsableBy UsableBy { get; set; }
        public ItemType ItemType { get; set; }
        public string Damage { get; set; }
        public DamageType? DamageType { get; set; }
        public int? ArmorClassBonus { get; set; }
        public string ItemNotes { get; set; }
        public bool IsEquipped { get; set; }
    }
}
