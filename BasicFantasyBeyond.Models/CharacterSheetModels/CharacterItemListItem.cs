using BasicFantasyBeyond.Models.EquipmentModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicFantasyBeyond.Models.CharacterSheetModels
{
    public class CharacterItemListItem
    {
        public int ItemID { get; set; }
        public int CharacterItemID { get; set; }
        [Required]
        public string ItemName { get; set; }
        [Required]
        public UsableBy UsableBy { get; set; }
        [Required]
        public ItemType ItemType { get; set; }
        public string Damage { get; set; }
        public DamageType? DamageType { get; set; }
        public Size? Size { get; set; }
        public int? ArmorClassBonus { get; set; }
        public int AttackBonus { get; set; }
        public ArmorType ArmorType { get; set; }
        public string ItemNotes { get; set; }
        public bool IsEquipped { get; set; }
    }
}
