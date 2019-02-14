using BasicFantasyBeyond.Models.CharacterModels;
using BasicFantasyBeyond.Models.EquipmentModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicFantasyBeyond.Models.CharacterSheetModels
{
    public class CharacterSheetModel
    {
        [Key]
        public int CharacterItemsID { get; set; }
        [Required]
        public int CharacterID { get; set; }
        public virtual CharacterDetails Character { get; set; }
        public IEnumerable<ItemListItem> Items { get; set; }
    }
}
