namespace BasicFantasyBeyond.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedEquipmentInData : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.CharacterSheet", newName: "CharacterItem");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.CharacterItem", newName: "CharacterSheet");
        }
    }
}
