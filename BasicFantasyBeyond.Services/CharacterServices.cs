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
                    CharacterClass = model.CharacterClass
                };

			using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                ctx.Characters.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

		public IEnumerable<Character> GetCharacters()
        {
			using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx.
                    Characters
                    .Where(e => e.OwnerID == _userID)
                    .Select(
                        e =>
                        new Character
                        {
                            CharacterID = e.CharacterID,
                            CharacterName = e.CharacterName,
                            CharacterStr = e.CharacterStr,
                            CharacterDex = e.CharacterDex,
                            CharacterCon = e.CharacterCon,
                            CharacterInt = e.CharacterInt,
                            CharacterWis = e.CharacterWis,
                            CharacterCha = e.CharacterCha,
                            CharacterRace = e.CharacterRace,
                            CharacterClass = e.CharacterClass,
							CharacterAbilities = e.CharacterAbilities,
							CharacterXP = e.CharacterXP,
							CharacterLevel = e.CharacterLevel,
							CharacterAC = e.CharacterAC,
							CharacterHP = e.CharacterHP,
							CharacterAttackBonus = e.CharacterAttackBonus,
							CharacterNotes = e.CharacterNotes
                        });
                return query.ToArray();
            }
        }

		public Character GetCharacterByID(int characterID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Characters
                    .Single(e => e.CharacterID == characterID && e.OwnerID == _userID);
                return
                    new Character
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

		public bool UpdateCharacter(Character model)
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
                entity.CharacterRace = model.CharacterRace;
                entity.CharacterClass = model.CharacterClass;
                entity.CharacterAbilities = model.CharacterAbilities;
                entity.CharacterXP = model.CharacterXP;
                entity.CharacterLevel = model.CharacterLevel;
                entity.CharacterAC = model.CharacterAC;
                entity.CharacterHP = model.CharacterHP;
                entity.CharacterAttackBonus = model.CharacterAttackBonus;
                entity.CharacterNotes = model.CharacterNotes;

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
