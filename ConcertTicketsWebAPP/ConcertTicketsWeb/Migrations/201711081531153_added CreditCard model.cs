namespace ConcertTicketsWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedCreditCardmodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CreditCards",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Number = c.String(),
                        ValidThru = c.String(),
                        CVV = c.String(),
                        ReservationID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Reservations", t => t.ReservationID, cascadeDelete: true)
                .Index(t => t.ReservationID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CreditCards", "ReservationID", "dbo.Reservations");
            DropIndex("dbo.CreditCards", new[] { "ReservationID" });
            DropTable("dbo.CreditCards");
        }
    }
}
