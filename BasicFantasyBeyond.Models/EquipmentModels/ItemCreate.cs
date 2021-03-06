﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicFantasyBeyond.Models.EquipmentModels
{
    public enum ItemType { Weapon, Armor, Shield, Gear }
    public enum DamageType { Piercing, Bludgeoning, Slashing }
    public enum Size { Small, Medium, Large }
    public enum ArmorType { None, Leather, Chain, Plate }
    public enum WeaponType { None, Ranged, Melee}
    [Flags]
    public enum UsableBy
    {
        None = 0,
        Fighter = 1 << 0,
        Cleric = 1 << 1,
        Thief = 1 << 2,
        MagicUser = 1 << 3,
        Human = 1 << 4,
        Elf = 1 << 5,
        Halfling = 1 << 6,
        Dwarf = 1 << 7
    }

    public class ItemCreate
    {
        [Key]
        public int ItemID { get; set; }
        [Required]
        public string ItemName { get; set; }
        public bool UsableByHuman { get; set; }
        public bool UsableByElf { get; set; }
        public bool UsableByHalfling { get; set; }
        public bool UsableByDwarf { get; set; }
        public bool UsableByFighter { get; set; }
        public bool UsableByCleric { get; set; }
        public bool UsableByThief { get; set; }
        public bool UsableByMagicUser { get; set; }
        public UsableBy? UsableBy { get; set; }
        [Required]
        public ItemType EquipmentType { get; set; }
        public WeaponType? WeaponType { get; set; }
        public string Damage { get; set; }
        public DamageType? DamageType { get; set; }
        public ArmorType? ArmorType { get; set; }
        public int? ArmorClassBonus { get; set; }
        public string ItemNotes { get; set; }
    }
}
