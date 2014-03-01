namespace AdminPanel.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AtualizandoReferencias : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ArcgisServer", "Cliente_ClienteId", "dbo.cfg_cliente");
            DropForeignKey("dbo.cfg_servidor_sde", "Cliente_ClienteId", "dbo.cfg_cliente");
            DropIndex("dbo.ArcgisServer", new[] { "Cliente_ClienteId" });
            DropIndex("dbo.cfg_servidor_sde", new[] { "Cliente_ClienteId" });
            CreateIndex("dbo.cfg_servidor_sde", "Cliente_ClienteId");
            AddForeignKey("dbo.cfg_servidor_sde", "Cliente_ClienteId", "dbo.cfg_cliente", "ClienteId");
            DropTable("dbo.ArcgisServer");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ArcgisServer",
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
                .PrimaryKey(t => t.ArcgisServerId);
            
            DropForeignKey("dbo.cfg_servidor_sde", "Cliente_ClienteId", "dbo.cfg_cliente");
            DropIndex("dbo.cfg_servidor_sde", new[] { "Cliente_ClienteId" });
            CreateIndex("dbo.cfg_servidor_sde", "Cliente_ClienteId");
            CreateIndex("dbo.ArcgisServer", "Cliente_ClienteId");
            AddForeignKey("dbo.cfg_servidor_sde", "Cliente_ClienteId", "dbo.cfg_cliente", "ClienteId");
            AddForeignKey("dbo.ArcgisServer", "Cliente_ClienteId", "dbo.cfg_cliente", "ClienteId");
        }
    }
}
