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
        public string CharacterName { get; set; }
        public short CharacterStr { get; set; }
        public short CharacterDex { get; set; }
        public short CharacterCon { get; set; }
        public short CharacterInt { get; set; }
        public short CharacterWis { get; set; }
        public short CharacterCha { get; set; }
        public CharacterRace CharacterRace { get; set; }
        public CharacterClass CharacterClass { get; set; }
        public CharacterAbilities? CharacterAbilities { get; set; }
        public int? CharacterXP { get; set; }
        public short? CharacterLevel { get; set; }
        public short? CharacterAC { get; set; }
        public short? CharacterHP { get; set; }
        public short? CharacterAttackBonus { get; set; }
        public string CharacterNotes { get; set; }

        public override string ToString() => CharacterName;

    }
}
