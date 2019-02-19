namespace BasicFantasyBeyond.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Character", "CharacterLevel", c => c.Int());
            AlterColumn("dbo.Character", "CharacterAC", c => c.Int());
            AlterColumn("dbo.Character", "CharacterHP", c => c.Int());
            AlterColumn("dbo.Character", "CharacterAttackBonus", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Character", "CharacterAttackBonus", c => c.Short());
            AlterColumn("dbo.Character", "CharacterHP", c => c.Short());
            AlterColumn("dbo.Character", "CharacterAC", c => c.Short());
            AlterColumn("dbo.Character", "CharacterLevel", c => c.Short());
        }
    }
}
