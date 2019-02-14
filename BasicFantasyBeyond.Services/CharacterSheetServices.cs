using BasicFantasyBeyond.Data;
using BasicFantasyBeyond.Models;
using BasicFantasyBeyond.Models.CharcterSheetModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicFantasyBeyond.Services
{
    public class CharacterSheetServices
    {
        private Guid _userID;

        public CharacterSheetServices(Guid userID)
        {
            _userID = userID;
        }

        public bool AddCharacterSheet(CharacterSheetCreate model)
        {
            var entity =
                new CharacterSheet()
                {
                    CharacterID = model.CharacterID,
                    ItemID = model.ItemID
                };

            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                ctx.CharacterSheet.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<CharacterSheetModel> GetCharacterSheetByCharacterID(int characterID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                List<CharacterSheetModel> characterSheet = new List<CharacterSheetModel>();

                var query = ctx.CharacterSheet.Where(e => e.CharacterID == characterID);

                foreach (var model in query)
                {
                    var listItem = new CharacterSheetModel
                    {
                        CharacterID = model.CharacterID,
                        ItemID = model.ItemID
                    };

                    AddCharacterSheetToList(listItem);
                }

                return characterSheet;

                void AddCharacterSheetToList(CharacterSheetModel item)
                {
                    characterSheet.Add(item);
                }

            }
        }

        public bool UpdateCharacterSheet(CharacterSheetModel model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.CharacterSheet.Single(e => e.CharacterItemsID == model.CharacterItemsID);

                entity.ItemID = model.ItemID;
                entity.CharacterID = model.CharacterID;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool RemoveItemFromCharacter(int equipmentID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.CharacterSheet.Single(e => e.ItemID == equipmentID);

                ctx.CharacterSheet.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

    }
}
