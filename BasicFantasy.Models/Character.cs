using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BasicFantasyBeyond.Models
{
    public enum CharacterRace { Human, Elf, Dwarf, Halfling};
    public enum CharacterClass { Fighter, Thief, Cleric, MagicUser};

    public class Character
    {
        [Key]
        public int CharacterID { get; set; }
        [Required]
        public string CharacterName { get; set; }
        [Required]
        public CharacterRace CharacterRace { get; set; }
        [Required]
        public CharacterClass CharacterClass { get; set; }
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

        public string CharacterNotes { get; set; }

        
    }
}