namespace AdminPanel.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTableCfgArcgisServer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.cfg_arcgis_server",
                c => new
                    {
                        ArcgisServerId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Password = c.String(),
                        Login = c.String(),
                        Url = c.String(),
                        ArcgisVersion = c.String(),
                        DataReg = c.DateTime(nullable: false),
                        EstReg = c.String(),
                        Cliente_ClienteId = c.Int(),
                    })
                .PrimaryKey(t => t.ArcgisServerId)
                .ForeignKey("dbo.cfg_cliente", t => t.Cliente_ClienteId)
                .Index(t => t.Cliente_ClienteId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.cfg_arcgis_server", "Cliente_ClienteId", "dbo.cfg_cliente");
            DropIndex("dbo.cfg_arcgis_server", new[] { "Cliente_ClienteId" });
            DropTable("dbo.cfg_arcgis_server");
        }
    }
}
