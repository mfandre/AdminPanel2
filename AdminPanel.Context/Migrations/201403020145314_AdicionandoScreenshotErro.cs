namespace AdminPanel.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionandoScreenshotErro : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.log_javascript_error", "Screenshot", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.log_javascript_error", "Screenshot");
        }
    }
}
