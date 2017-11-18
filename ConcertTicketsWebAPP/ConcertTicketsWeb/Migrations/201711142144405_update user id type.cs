namespace ConcertTicketsWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateuseridtype : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Reservations", new[] { "User_Id" });
            DropIndex("dbo.CreditCards", new[] { "User_Id" });
            DropColumn("dbo.Reservations", "UserID");
            DropColumn("dbo.CreditCards", "UserID");
            RenameColumn(table: "dbo.Reservations", name: "User_Id", newName: "UserID");
            RenameColumn(table: "dbo.CreditCards", name: "User_Id", newName: "UserID");
            AlterColumn("dbo.Reservations", "UserID", c => c.String(maxLength: 128));
            AlterColumn("dbo.CreditCards", "UserID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Reservations", "UserID");
            CreateIndex("dbo.CreditCards", "UserID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.CreditCards", new[] { "UserID" });
            DropIndex("dbo.Reservations", new[] { "UserID" });
            AlterColumn("dbo.CreditCards", "UserID", c => c.Int(nullable: false));
            AlterColumn("dbo.Reservations", "UserID", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.CreditCards", name: "UserID", newName: "User_Id");
            RenameColumn(table: "dbo.Reservations", name: "UserID", newName: "User_Id");
            AddColumn("dbo.CreditCards", "UserID", c => c.Int(nullable: false));
            AddColumn("dbo.Reservations", "UserID", c => c.Int(nullable: false));
            CreateIndex("dbo.CreditCards", "User_Id");
            CreateIndex("dbo.Reservations", "User_Id");
        }
    }
}
