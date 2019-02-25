using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicFantasyBeyond.Models.CharacterModels
{
    public enum CharacterRace { Human, Elf, Dwarf, Halfling };
    public enum CharacterClass { Fighter, Thief, Cleric, MagicUser };
    [Flags]
    public enum CharacterAbilities
    {
        None = 0,
        Darkvision = 1 << 0,
        DetectConstruction = 1 << 1,
        DetectSecretDoors = 1 << 2,
        GhoulImmune = 1 << 3,
        ReduceSurprise = 1 << 4,
        HalflingAttackBonus = 1 << 5,
        HalflingACBonus = 1 << 6,
        HalflingInitiative = 1 << 7,
        HalflingHiding = 1 << 8,
        HumanXPBonus = 1 << 9,
        TurnUndead = 1 << 10,
        ThiefSkills = 1 << 11
    }

    public class CharacterCreate
    {
        [Required]
        [MinLength(2, ErrorMessage = "Please enter at least 2 characters.")]
        [MaxLength(40, ErrorMessage = "There are too many characters in this field.")]
        public string CharacterName { get; set; }

        [Required]
        [Range(3, 18, ErrorMessage = "Number must be between 3 and 18")]
        public short CharacterStr { get; set; }
        [Required]
        [Range(3, 18, ErrorMessage = "Number must be between 3 and 18")]
        public short CharacterDex { get; set; }
        [Required]
        [Range(3, 18, ErrorMessage = "Number must be between 3 and 18")]
        public short CharacterCon { get; set; }
        [Required]
        [Range(3, 18, ErrorMessage = "Number must be between 3 and 18")]
        public short CharacterInt { get; set; }
        [Required]
        [Range(3, 18, ErrorMessage = "Number must be between 3 and 18")]
        public short CharacterWis { get; set; }
        [Required]
        [Range(3, 18, ErrorMessage = "Number must be between 3 and 18")]
        public short CharacterCha { get; set; }

        [Required]
        public CharacterRace CharacterRace { get; set; }

        [Required]
        public CharacterClass CharacterClass { get; set; }

        public override string ToString() => CharacterName;

    }

}
