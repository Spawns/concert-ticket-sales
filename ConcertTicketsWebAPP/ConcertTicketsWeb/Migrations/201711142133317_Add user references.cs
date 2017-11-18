namespace ConcertTicketsWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Adduserreferences : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservations", "UserID", c => c.Int(nullable: false));
            AddColumn("dbo.Reservations", "User_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.CreditCards", "UserID", c => c.Int(nullable: false));
            AddColumn("dbo.CreditCards", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Reservations", "User_Id");
            CreateIndex("dbo.CreditCards", "User_Id");
            AddForeignKey("dbo.Reservations", "User_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.CreditCards", "User_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CreditCards", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Reservations", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.CreditCards", new[] { "User_Id" });
            DropIndex("dbo.Reservations", new[] { "User_Id" });
            DropColumn("dbo.CreditCards", "User_Id");
            DropColumn("dbo.CreditCards", "UserID");
            DropColumn("dbo.Reservations", "User_Id");
            DropColumn("dbo.Reservations", "UserID");
        }
    }
}
