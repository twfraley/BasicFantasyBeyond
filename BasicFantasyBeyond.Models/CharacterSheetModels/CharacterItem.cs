﻿using BasicFantasyBeyond.Models.CharacterModels;
using BasicFantasyBeyond.Models.EquipmentModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicFantasyBeyond.Models.CharacterSheetModels
{
    public class CharacterItem
    {
        [Key]
        public int CharacterItemsID { get; set; }
        [Required]
        public int CharacterID { get; set; }
        public virtual CharacterDetails Character { get; set; }
        [Required]
        public int ItemID { get; set; }
        public virtual ItemDetails Equipment { get; set; }
    }
}
