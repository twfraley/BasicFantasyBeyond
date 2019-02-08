using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicFantasyBeyond.Models.CharacterModels
{
    public class CharacterEdit
    {
        public int CharacterID { get; set; }
        public string CharacterName { get; set; }
        public short CharacterStr { get; set; }
        public short CharacterDex { get; set; }
        public short CharacterCon { get; set; }
        public short CharacterInt { get; set; }
        public short CharacterWis { get; set; }
        public short CharacterCha { get; set; }
        public int? CharacterXP { get; set; }

        public override string ToString() => CharacterName;

    }
}
