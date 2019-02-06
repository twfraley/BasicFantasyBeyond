using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicFantasyBeyond.Models
{
    public class CharacterAbilities
    {
        [Key]
        public int CharAbilityID { get; set; }
        public string FirstAbility { get; set; }
        public string SecondAbility { get; set; }
        public string ThirdAbility { get; set; }
        public string FourthAbility { get; set; }

    }
}
