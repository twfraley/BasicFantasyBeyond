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
    public class CharacterSheetModel
    {
        [Key]
        public int CharacterSheetID { get; set; }
        [Required]
        public int CharacterID { get; set; }
        public Guid OwnerID { get; set; }
        [Required]
        public string CharacterName { get; set; }
        [Required]
        public short CharacterStr { get; set; }
        public short StrMod { get; set; }
        [Required]
        public short CharacterDex { get; set; }
        public short DexMod { get; set; }
        [Required]
        public short CharacterCon { get; set; }
        public short ConMod { get; set; }
        [Required]
        public short CharacterInt { get; set; }
        public short IntMod { get; set; }
        [Required]
        public short CharacterWis { get; set; }
        public short WisMod { get; set; }
        [Required]
        public short CharacterCha { get; set; }
        public short ChaMod { get; set; }
        [Required]
        public CharacterRace CharacterRace { get; set; }
        [Required]
        public CharacterClass CharacterClass { get; set; }
        public CharacterAbilities? CharacterAbilities { get; set; }
        public int CharacterXP { get; set; }
        public short? CharacterLevel { get; set; }
        public short? CharacterAC { get; set; }
        public short? CharacterHP { get; set; }
        public short? CharacterAttackBonus { get; set; }
        public short? CharacterSpeed { get; set; }
        public string CharacterNotes { get; set; }
        public IEnumerable<CharacterItemListItem> Items { get; set; }
        public IEnumerable<CharacterItemListItem> EquippedWeapons { get; set; }
        public IEnumerable<CharacterItemListItem> Weapons { get; set; }
        public IEnumerable<CharacterItemListItem> EquippedArmor { get; set; }
        public IEnumerable<CharacterItemListItem> Armor { get; set; }
        public IEnumerable<CharacterItemListItem> Gear { get; set; }
    }
}
