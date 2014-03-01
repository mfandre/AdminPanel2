namespace AdminPanel.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeDatetime2Datetime2OnUserModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.System_Users", "Data de Registro", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.System_Users", "Data de Registro", c => c.DateTime(nullable: false));
        }
    }
}
