namespace BasicFantasyBeyond.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.CharacterItems", "CharacterID");
            CreateIndex("dbo.CharacterItems", "ItemID");
            AddForeignKey("dbo.CharacterItems", "CharacterID", "dbo.Character", "CharacterID", cascadeDelete: true);
            AddForeignKey("dbo.CharacterItems", "ItemID", "dbo.Equipment", "ItemID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CharacterItems", "ItemID", "dbo.Equipment");
            DropForeignKey("dbo.CharacterItems", "CharacterID", "dbo.Character");
            DropIndex("dbo.CharacterItems", new[] { "ItemID" });
            DropIndex("dbo.CharacterItems", new[] { "CharacterID" });
        }
    }
}
