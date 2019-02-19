using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicFantasyBeyond.Models.CharacterModels
{
    public class CharacterListItem
    {
        [Required]
        public int CharacterID { get; set; }
        [Required]
        public string CharacterName { get; set; }
        [Required]
        public CharacterRace CharacterRace { get; set; }
        [Required]
        public CharacterClass CharacterClass { get; set; }
        public int? CharacterLevel { get; set; }

        public override string ToString() => CharacterName;
    }
}
