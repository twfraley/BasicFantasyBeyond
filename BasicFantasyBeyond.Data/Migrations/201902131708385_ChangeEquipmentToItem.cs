namespace BasicFantasyBeyond.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeEquipmentToItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Equipment", "ItemType", c => c.Int(nullable: false));
            DropColumn("dbo.Equipment", "EquipmentType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Equipment", "EquipmentType", c => c.Int(nullable: false));
            DropColumn("dbo.Equipment", "ItemType");
        }
    }
}
