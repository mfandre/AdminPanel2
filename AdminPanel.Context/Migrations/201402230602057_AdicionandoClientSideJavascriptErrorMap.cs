namespace AdminPanel.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionandoClientSideJavascriptErrorMap : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.log_javascript_error",
                c => new
                    {
                        ClientSideJavaScriptExceptionId = c.Int(nullable: false, identity: true),
                        ErrorMsg = c.String(),
                        Url = c.String(),
                        Line = c.String(),
                        DataReg = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.ClientSideJavaScriptExceptionId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.log_javascript_error");
        }
    }
}
