namespace AdminPanel.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateFieldsToDateTime2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.cfg_cliente", "DataReg", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.cfg_arcgis_server", "DataReg", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.cfg_arcgis_server", "DataReg", c => c.DateTime(nullable: false));
            AlterColumn("dbo.cfg_cliente", "DataReg", c => c.DateTime(nullable: false));
        }
    }
}
