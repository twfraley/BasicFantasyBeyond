using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BasicFantasyBeyond.Models.CharacterModels;

namespace BasicFantasyBeyond.Data
{
    public class Character
    {
        [Key]
        public int CharacterID { get; set; }

        [Required]
        public Guid OwnerID { get; set; }
        
        [Required]
        [MinLength(2, ErrorMessage = "Please enter at least 2 characters.")]
        [MaxLength(40, ErrorMessage = "There are too many characters in this field.")]
        public string CharacterName { get; set; }

        [Required]
        public short CharacterStr { get; set; }
        [Required]
        public short CharacterDex { get; set; }
        [Required]
        public short CharacterCon { get; set; }
        [Required]
        public short CharacterInt { get; set; }
        [Required]
        public short CharacterWis { get; set; }
        [Required]
        public short CharacterCha { get; set; }

        [Required]
        public CharacterRace CharacterRace { get; set; }

        [Required]
        public CharacterClass CharacterClass { get; set; }

        public CharacterAbilities CharacterAbilities { get; set; }

        public int CharacterXP { get; set; }

        public int? CharacterLevel { get; set; }

        public int? CharacterAC { get; set; }

        public int? CharacterHP { get; set; }

        public int? CharacterAttackBonus { get; set; }

        public string CharacterNotes { get; set; }

        public override string ToString() => CharacterName;

    }
}