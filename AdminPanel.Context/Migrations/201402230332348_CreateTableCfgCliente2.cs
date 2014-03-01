namespace AdminPanel.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTableCfgCliente2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.cfg_cliente",
                c => new
                    {
                        ClienteId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DataReg = c.DateTime(nullable: false),
                        EstReg = c.String(),
                    })
                .PrimaryKey(t => t.ClienteId);
            
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
                .PrimaryKey(t => t.ArcgisServerId)
                .ForeignKey("dbo.cfg_cliente", t => t.Cliente_ClienteId)
                .Index(t => t.Cliente_ClienteId);
            
            AddColumn("dbo.cfg_servidor_sde", "Cliente_ClienteId", c => c.Int());
            CreateIndex("dbo.cfg_servidor_sde", "Cliente_ClienteId");
            AddForeignKey("dbo.cfg_servidor_sde", "Cliente_ClienteId", "dbo.cfg_cliente", "ClienteId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.cfg_servidor_sde", "Cliente_ClienteId", "dbo.cfg_cliente");
            DropForeignKey("dbo.ArcgisServer", "Cliente_ClienteId", "dbo.cfg_cliente");
            DropIndex("dbo.cfg_servidor_sde", new[] { "Cliente_ClienteId" });
            DropIndex("dbo.ArcgisServer", new[] { "Cliente_ClienteId" });
            DropColumn("dbo.cfg_servidor_sde", "Cliente_ClienteId");
            DropTable("dbo.ArcgisServer");
            DropTable("dbo.cfg_cliente");
        }
    }
}
