using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicFantasyBeyond.Data
{
    public class CharacterItem
    {
        [Key]
        public int CharacterItemsID { get; set; }
        [Required]
        public int CharacterID { get; set; }
        public virtual Character Character { get; set; }
        [Required]
        public int ItemID { get; set; }
        public virtual Item Equipment {get;set;}
        [Required]
        public bool IsEquipped { get; set; }
    }
}
