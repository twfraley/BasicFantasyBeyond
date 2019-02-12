namespace BasicFantasyBeyond.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.CharacterItems", newName: "CharacterSheet");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.CharacterSheet", newName: "CharacterItems");
        }
    }
}
