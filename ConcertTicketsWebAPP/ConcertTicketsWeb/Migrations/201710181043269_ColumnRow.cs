namespace ConcertTicketsWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ColumnRow : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Seats", "Row", c => c.Int(nullable: false));
            AddColumn("dbo.Seats", "Column", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Seats", "Column");
            DropColumn("dbo.Seats", "Row");
        }
    }
}
