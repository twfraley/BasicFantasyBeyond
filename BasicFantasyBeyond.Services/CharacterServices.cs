using BasicFantasyBeyond.Data;
using BasicFantasyBeyond.Models;
using BasicFantasyBeyond.Models.CharacterModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicFantasyBeyond.Services
{
    public class CharacterServices
    {
        private readonly Guid _userID;

        public CharacterServices(Guid userID)
        {
            _userID = userID;
        }

        public bool CreateCharacter(CharacterCreate model)
        {
            var characterAbilities = GenerateCharacterAbilities(model.CharacterClass, model.CharacterRace);
            var level = GetLevelFromXP(model.CharacterClass,0);
            var attackBonus = GetAttackBonus(model.CharacterClass, 0);
            var entity =
                new Character()
                {
                    OwnerID = _userID,
                    CharacterName = model.CharacterName,
                    CharacterStr = model.CharacterStr,
                    CharacterDex = model.CharacterDex,
                    CharacterCon = model.CharacterCon,
                    CharacterInt = model.CharacterInt,
                    CharacterWis = model.CharacterWis,
                    CharacterCha = model.CharacterCha,
                    CharacterRace = model.CharacterRace,
                    CharacterClass = model.CharacterClass,
                    CharacterAbilities = characterAbilities,
                    CharacterXP = 0,
                    CharacterLevel = level
                };

            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                ctx.Characters.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<CharacterListItem> GetCharacters()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Characters.Where(e => e.OwnerID == _userID).Select(e => new CharacterListItem
                {
                    CharacterID = e.CharacterID,
                    CharacterName = e.CharacterName,
                    CharacterRace = e.CharacterRace,
                    CharacterClass = e.CharacterClass,
                    CharacterLevel = e.CharacterLevel,
                });
                return query.ToArray();
            }
        }

        public CharacterDetails GetCharacterByID(int characterID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Characters.Single(e => e.CharacterID == characterID && e.OwnerID == _userID);
                return
                    new CharacterDetails
                    {
                        CharacterID = entity.CharacterID,
                        CharacterName = entity.CharacterName,
                        CharacterStr = entity.CharacterStr,
                        CharacterDex = entity.CharacterDex,
                        CharacterCon = entity.CharacterCon,
                        CharacterInt = entity.CharacterInt,
                        CharacterWis = entity.CharacterWis,
                        CharacterCha = entity.CharacterCha,
                        CharacterRace = entity.CharacterRace,
                        CharacterClass = entity.CharacterClass,
                        CharacterAbilities = entity.CharacterAbilities,
                        CharacterXP = entity.CharacterXP,
                        CharacterLevel = entity.CharacterLevel,
                        CharacterAC = entity.CharacterAC,
                        CharacterHP = entity.CharacterHP,
                        CharacterAttackBonus = entity.CharacterAttackBonus,
                        CharacterNotes = entity.CharacterNotes
                    };
            }
        }

        public int GetLastCharacterIDFromUser(Guid guid)
        {
            using (var ctx = new ApplicationDbContext())
            {
                int characterID = 0;
                foreach (Character character in ctx.Characters)
                {
                    if (character.OwnerID == guid)
                    {
                        characterID = character.CharacterID;
                    }
                }
                return characterID;
            }
        }

        public bool UpdateCharacter(CharacterDetails model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var characterAbilities = GenerateCharacterAbilities(model.CharacterClass, model.CharacterRace);
                var characterLevel = GetLevelFromXP(model.CharacterClass, model.CharacterXP);
                var attackBonus = GetAttackBonus(model.CharacterClass, Convert.ToInt32(characterLevel));

                var entity = ctx.Characters.Single(e => e.CharacterID == model.CharacterID && e.OwnerID == _userID);

                entity.CharacterName = model.CharacterName;
                entity.CharacterStr = model.CharacterStr;
                entity.CharacterDex = model.CharacterDex;
                entity.CharacterCon = model.CharacterCon;
                entity.CharacterInt = model.CharacterInt;
                entity.CharacterWis = model.CharacterWis;
                entity.CharacterCha = model.CharacterCha;
                entity.CharacterRace = model.CharacterRace;
                entity.CharacterClass = model.CharacterClass;
                entity.CharacterAbilities = characterAbilities;
                entity.CharacterXP = model.CharacterXP;
                entity.CharacterLevel = characterLevel;
                entity.CharacterAC = model.CharacterAC;
                entity.CharacterHP = model.CharacterHP;
                entity.CharacterAttackBonus = Convert.ToInt16(attackBonus);
                entity.CharacterNotes = model.CharacterNotes;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteCharacter(int characterID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Characters.Single(e => e.CharacterID == characterID && e.OwnerID == _userID);

                var cleanup = ctx.CharacterItems.Where(e => e.CharacterID == characterID);

                ctx.Characters.Remove(entity);

                foreach (var item in cleanup) ctx.CharacterItems.Remove(item);

                return ctx.SaveChanges() == 1;
            }
        }

        private CharacterAbilities GenerateCharacterAbilities(CharacterClass characterClass, CharacterRace characterRace)
        {
            CharacterAbilities characterAbilities = CharacterAbilities.None;

            if (characterRace == CharacterRace.Dwarf)
            {
                characterAbilities |= CharacterAbilities.Darkvision;
                characterAbilities |= CharacterAbilities.DetectConstruction;
            }
            if (characterRace == CharacterRace.Elf)
            {
                characterAbilities |= CharacterAbilities.Darkvision;
                characterAbilities |= CharacterAbilities.GhoulImmune;
                characterAbilities |= CharacterAbilities.DetectSecretDoors;
                characterAbilities |= CharacterAbilities.ReduceSurprise;
            }
            if (characterRace == CharacterRace.Halfling)
            {
                characterAbilities |= CharacterAbilities.HalflingACBonus;
                characterAbilities |= CharacterAbilities.HalflingAttackBonus;
                characterAbilities |= CharacterAbilities.HalflingHiding;
                characterAbilities |= CharacterAbilities.HalflingInitiative;
            }
            if (characterRace == CharacterRace.Human)
            {
                characterAbilities |= CharacterAbilities.HumanXPBonus;
            }
            if (characterClass == CharacterClass.Thief)
            {
                characterAbilities |= CharacterAbilities.ThiefSkills;
            }
            if (characterClass == CharacterClass.Cleric)
            {
                characterAbilities |= CharacterAbilities.TurnUndead;
            }

            return characterAbilities;
        }

        private short GetLevelFromXP(CharacterClass characterClass, int xp)
        {
            short characterLevel = 0;
            if (characterClass == CharacterClass.Fighter)
            {
                if (xp < 2000) characterLevel = 1;
                if (xp >= 2000 && xp < 4000) characterLevel = 2;
                if (xp >= 4000 && xp < 8000) characterLevel = 3;
                if (xp >= 8000 && xp < 16000) characterLevel = 4;
                if (xp >= 16000 && xp < 32000) characterLevel = 5;
                if (xp >= 32000 && xp < 64000) characterLevel = 6;
                if (xp >= 64000 && xp < 120000) characterLevel = 7;
                if (xp >= 120000 && xp < 240000) characterLevel = 8;
                if (xp >= 240000 && xp < 360000) characterLevel = 9;
                if (xp >= 360000 && xp < 480000) characterLevel = 10;
                if (xp >= 480000 && xp < 600000) characterLevel = 11;
                if (xp >= 600000 && xp < 720000) characterLevel = 12;
                if (xp >= 720000 && xp < 840000) characterLevel = 13;
                if (xp >= 840000 && xp < 960000) characterLevel = 14;
                if (xp >= 960000 && xp < 1080000) characterLevel = 15;
                if (xp >= 1080000 && xp < 1200000) characterLevel = 16;
                if (xp >= 1200000 && xp < 1320000) characterLevel = 17;
                if (xp >= 1320000 && xp < 1440000) characterLevel = 18;
                if (xp >= 1440000 && xp < 1560000) characterLevel = 19;
                if (xp >= 1560000) characterLevel = 20;
            }
            if (characterClass == CharacterClass.Cleric)
            {
                if (xp < 1500) characterLevel = 1;
                if (xp >= 1500 && xp < 3000) characterLevel = 2;
                if (xp >= 3000 && xp < 6000) characterLevel = 3;
                if (xp >= 6000 && xp < 12000) characterLevel = 4;
                if (xp >= 12000 && xp < 24000) characterLevel = 5;
                if (xp >= 24000 && xp < 48000) characterLevel = 6;
                if (xp >= 48000 && xp < 90000) characterLevel = 7;
                if (xp >= 90000 && xp < 180000) characterLevel = 8;
                if (xp >= 180000 && xp < 270000) characterLevel = 9;
                if (xp >= 270000 && xp < 360000) characterLevel = 10;
                if (xp >= 360000 && xp < 450000) characterLevel = 11;
                if (xp >= 450000 && xp < 540000) characterLevel = 12;
                if (xp >= 540000 && xp < 630000) characterLevel = 13;
                if (xp >= 630000 && xp < 720000) characterLevel = 14;
                if (xp >= 720000 && xp < 810000) characterLevel = 15;
                if (xp >= 810000 && xp < 900000) characterLevel = 16;
                if (xp >= 900000 && xp < 990000) characterLevel = 17;
                if (xp >= 990000 && xp < 1080000) characterLevel = 18;
                if (xp >= 1080000 && xp < 1170000) characterLevel = 19;
                if (xp >= 1170000) characterLevel = 20;
            }
            if (characterClass == CharacterClass.Thief)
            {
                if (xp < 1250) characterLevel = 1;
                if (xp >= 1250 && xp < 2500) characterLevel = 2;
                if (xp >= 2500 && xp < 5000) characterLevel = 3;
                if (xp >= 5000 && xp < 10000) characterLevel = 4;
                if (xp >= 10000 && xp < 20000) characterLevel = 5;
                if (xp >= 20000 && xp < 40000) characterLevel = 6;
                if (xp >= 40000 && xp < 75000) characterLevel = 7;
                if (xp >= 75000 && xp < 15000) characterLevel = 8;
                if (xp >= 150000 && xp < 225000) characterLevel = 9;
                if (xp >= 225000 && xp < 300000) characterLevel = 10;
                if (xp >= 300000 && xp < 375000) characterLevel = 11;
                if (xp >= 375000 && xp < 450000) characterLevel = 12;
                if (xp >= 450000 && xp < 525000) characterLevel = 13;
                if (xp >= 525000 && xp < 600000) characterLevel = 14;
                if (xp >= 600000 && xp < 675000) characterLevel = 15;
                if (xp >= 675000 && xp < 750000) characterLevel = 16;
                if (xp >= 750000 && xp < 825000) characterLevel = 17;
                if (xp >= 825000 && xp < 900000) characterLevel = 18;
                if (xp >= 900000 && xp < 975000) characterLevel = 19;
                if (xp >= 975000) characterLevel = 20;
            }
            if (characterClass == CharacterClass.MagicUser)
            {
                if (xp < 2500) characterLevel = 1;
                if (xp >= 2500 && xp < 4000) characterLevel = 2;
                if (xp >= 5000 && xp < 8000) characterLevel = 3;
                if (xp >= 10000 && xp < 16000) characterLevel = 4;
                if (xp >= 20000 && xp < 32000) characterLevel = 5;
                if (xp >= 40000 && xp < 64000) characterLevel = 6;
                if (xp >= 80000 && xp < 120000) characterLevel = 7;
                if (xp >= 150000 && xp < 240000) characterLevel = 8;
                if (xp >= 300000 && xp < 360000) characterLevel = 9;
                if (xp >= 450000 && xp < 480000) characterLevel = 10;
                if (xp >= 600000 && xp < 600000) characterLevel = 11;
                if (xp >= 750000 && xp < 720000) characterLevel = 12;
                if (xp >= 900000 && xp < 840000) characterLevel = 13;
                if (xp >= 1050000 && xp < 960000) characterLevel = 14;
                if (xp >= 1200000 && xp < 1080000) characterLevel = 15;
                if (xp >= 1350000 && xp < 1200000) characterLevel = 16;
                if (xp >= 1500000 && xp < 1320000) characterLevel = 17;
                if (xp >= 1650000 && xp < 1440000) characterLevel = 18;
                if (xp >= 1800000 && xp < 1560000) characterLevel = 19;
                if (xp >= 1950000) characterLevel = 20;
            }

            return characterLevel;
        }

        private int GetAttackBonus(CharacterClass characterClass, int characterLevel)
        {
            int attackBonus = 1;

            if (characterClass == CharacterClass.Fighter)
            {
                if (characterLevel == 1) attackBonus = 1;
                if (characterLevel == 2 || characterLevel == 3) attackBonus = 2;
                if (characterLevel == 4) attackBonus = 3;
                if (characterLevel == 5 || characterLevel == 6) attackBonus = 4;
                if (characterLevel == 7) attackBonus = 5;
                if (characterLevel >= 8 && characterLevel < 11) attackBonus = 6;
                if (characterLevel >= 11 && characterLevel < 13) attackBonus = 7;
                if (characterLevel >= 13 && characterLevel < 16) attackBonus = 8;
                if (characterLevel >= 16 && characterLevel < 18) attackBonus = 9;
                if (characterLevel >= 18) attackBonus = 10;
            }
            if (characterClass == CharacterClass.Cleric || characterClass == CharacterClass.Thief)
            {
                if (characterLevel >= 1 && characterLevel < 3) attackBonus = 1;
                if (characterLevel >= 3 && characterLevel < 5) attackBonus = 2;
                if (characterLevel >= 5 && characterLevel < 7) attackBonus = 3;
                if (characterLevel >= 7 && characterLevel < 9) attackBonus = 4;
                if (characterLevel >= 9 && characterLevel < 12) attackBonus = 5;
                if (characterLevel >= 12 && characterLevel < 15) attackBonus = 6;
                if (characterLevel >= 15 && characterLevel < 18) attackBonus = 7;
                if (characterLevel >= 18) attackBonus = 8;
            }
            if (characterClass == CharacterClass.MagicUser)
            {
                if (characterLevel >= 1 && characterLevel < 4) attackBonus = 1;
                if (characterLevel >= 4 && characterLevel < 6) attackBonus = 2;
                if (characterLevel >= 6 && characterLevel < 9) attackBonus = 3;
                if (characterLevel >= 9 && characterLevel < 13) attackBonus = 4;
                if (characterLevel >= 13 && characterLevel < 16) attackBonus = 5;
                if (characterLevel >= 16 && characterLevel < 19) attackBonus = 6;
                if (characterLevel >= 19) attackBonus = 7;
            }

            return attackBonus;
        }
    }
}
