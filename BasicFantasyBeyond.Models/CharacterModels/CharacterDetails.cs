using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicFantasyBeyond.Models.CharacterModels
{
    public class CharacterDetails
    {
        [Required]
        public int CharacterID { get; set; }
        public Guid OwnerID { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Please enter at least 2 characters.")]
        [MaxLength(40, ErrorMessage = "There are too many characters in this field.")]
        public string CharacterName { get; set; }
        [Required]
        [Range(3, 18, ErrorMessage = "Number is too high or too low")]
        public short CharacterStr { get; set; }
        [Required]
        [Range(3, 18, ErrorMessage = "Number is too high or too low")]
        public short CharacterDex { get; set; }
        [Required]
        [Range(3, 18, ErrorMessage = "Number is too high or too low")]
        public short CharacterCon { get; set; }
        [Required]
        [Range(3, 18, ErrorMessage = "Number is too high or too low")]
        public short CharacterInt { get; set; }
        [Required]
        [Range(3, 18, ErrorMessage = "Number is too high or too low")]
        public short CharacterWis { get; set; }
        [Required]
        [Range(3, 18, ErrorMessage = "Number is too high or too low")]
        public short CharacterCha { get; set; }

        [Required]
        public CharacterRace CharacterRace { get; set; }
        [Required]
        public CharacterClass CharacterClass { get; set; }

        public CharacterAbilities? CharacterAbilities { get; set; }
        public int CharacterXP { get; set; }
        public int? CharacterLevel { get; set; }
        public IEnumerable<int> HitPointRange { get; set; }
        public int? CharacterAC { get; set; }
        public int? CharacterHP { get; set; }
        public int? CharacterAttackBonus { get; set; }
        public string CharacterNotes { get; set; }

        public override string ToString() => CharacterName;

    }
}
