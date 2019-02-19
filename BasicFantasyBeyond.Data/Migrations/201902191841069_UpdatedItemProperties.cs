namespace BasicFantasyBeyond.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedItemProperties : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Item", "AttackBonus", c => c.Int());
            AddColumn("dbo.Item", "WeaponType", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Item", "WeaponType");
            DropColumn("dbo.Item", "AttackBonus");
        }
    }
}
