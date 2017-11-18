namespace ConcertTicketsWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addstatusfieldtoreservations : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservations", "status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reservations", "status");
        }
    }
}
