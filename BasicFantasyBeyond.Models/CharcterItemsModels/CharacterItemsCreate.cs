using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicFantasyBeyond.Models
{
    public class CharacterItemsCreate
    {
        [Key]
        public int CharacterItemsID { get; set; }
        [Required]
        public virtual int CharacterID { get; set; }
        [Required]
        public virtual int ItemID { get; set; }
    }
}