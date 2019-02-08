using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicFantasyBeyond.Models.CharacterModels
{
    public class CharacterListItem
    {
        public int CharacterID { get; set; }
        public string CharacterName { get; set; }
        public CharacterRace CharacterRace { get; set; }
        public CharacterClass CharacterClass { get; set; }
        public short? CharacterLevel { get; set; }
    }
}
