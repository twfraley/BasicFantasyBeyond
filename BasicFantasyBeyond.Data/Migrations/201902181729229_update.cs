namespace BasicFantasyBeyond.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CharacterItem", "IsEquipped", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Character", "CharacterXP", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Character", "CharacterXP", c => c.Int());
            DropColumn("dbo.CharacterItem", "IsEquipped");
        }
    }
}
