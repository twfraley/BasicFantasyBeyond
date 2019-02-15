namespace BasicFantasyBeyond.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedItemSizeToItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Item", "Size", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Item", "Size");
        }
    }
}
