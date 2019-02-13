using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicFantasyBeyond.Models.EquipmentModels
{
    public class ItemListItem
    {
        public int ItemID { get; set; }
        [Required]
        public string ItemName { get; set; }
        public UsableBy UsableBy { get; set; }
        public ItemType ItemType { get; set; }
        public string Damage { get; set; }
        public DamageType? DamageType { get; set; }
        public int? ArmorClassBonus { get; set; }
        public string ItemNotes { get; set; }
    }
}
