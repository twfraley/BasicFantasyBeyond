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
            userID = _userID;
        }

		public bool CreateCharacter(CharacterCreate model)
        {
            CharacterAbilities characterAbilities = GenerateCharacterAbilities(model.CharacterClass, model.CharacterRace);

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
                    CharacterAbilities = characterAbilities
                };

			using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                ctx.Characters.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public CharacterAbilities GenerateCharacterAbilities(CharacterClass characterClass, CharacterRace characterRace)
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

            return characterAbilities;
        }

		public IEnumerable<CharacterListItem> GetCharacters()
        {
			using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx.
                    Characters
                    .Where(e => e.OwnerID == _userID)
                    .Select(
                        e =>
                        new CharacterListItem
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
                var entity =
                    ctx
                    .Characters
                    .Single(e => e.CharacterID == characterID && e.OwnerID == _userID);
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

		public bool UpdateCharacter(CharacterEdit model)
        {
			using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Characters
                    .Single(e => e.CharacterID == model.CharacterID && e.OwnerID == _userID);

                entity.CharacterName = model.CharacterName;
                entity.CharacterStr = model.CharacterStr;
                entity.CharacterDex = model.CharacterDex;
                entity.CharacterCon = model.CharacterDex;
                entity.CharacterInt = model.CharacterInt;
                entity.CharacterWis = model.CharacterWis;
                entity.CharacterCha = model.CharacterCha;
                entity.CharacterXP = model.CharacterXP;

                return ctx.SaveChanges() == 1;
            }
        }

		public bool DeleteCharacter(int characterID)
        {
			using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Characters.Single(e => e.CharacterID == characterID && e.OwnerID == _userID);

                ctx.Characters.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

    }
}
