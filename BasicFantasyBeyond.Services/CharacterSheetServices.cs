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

        public IEnumerable<CharacterSheetModel> GetCharacterSheet()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.CharacterSheet.Select(e => new CharacterSheetModel
                {
                    CharacterID = e.CharacterID,
                    ItemID = e.ItemID
                });

                return query.ToArray();
            }
        }

        //public IEnumerable<CharacterSheetModel> GetCharacterSheetByCharacterID(int characterID)
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        List<CharacterSheetModel> characterSheet = new List<CharacterSheetModel>();

        //        var query = ctx.CharacterSheet.Where(e => e.CharacterID == characterID);

        //        foreach (var model in query)
        //        {
        //            var itemList = new CharacterSheetModel
        //            {
        //                CharacterID = model.CharacterID,
        //                ItemID = model.ItemID
        //            };

        //            AddCharacterSheet(itemList);
        //        }
        //        return characterSheet;

        //        void AddCharacterSheet(CharacterSheet item)
        //        {
        //            characterSheet.Add(item);
        //        }
        //    }
        //}

        public bool UpdateCharacterSheet(CharacterSheetModel model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.CharacterSheet.Single(e => e.CharacterID == model.CharacterID);

                entity.ItemID = model.ItemID;
                entity.CharacterID = model.CharacterID;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool RemoveEquipmentfromCharacter(int equipmentID)
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
