namespace BasicFantasyBeyond.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Item", "ArmorType", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Item", "ArmorType");
        }
    }
}
