namespace BasicFantasyBeyond.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Character", "CharacterAbilities", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Character", "CharacterAbilities", c => c.Int());
        }
    }
}
