namespace BasicFantasyBeyond.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedEquipment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Equipment", "UsableBy", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Equipment", "UsableBy");
        }
    }
}
