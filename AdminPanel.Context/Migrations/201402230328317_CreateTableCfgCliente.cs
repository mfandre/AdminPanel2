namespace AdminPanel.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTableCfgCliente : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.cfg_servidor_sde",
                c => new
                    {
                        SdeId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Login = c.String(nullable: false),
                        Server = c.String(nullable: false),
                        Service = c.String(),
                        Version = c.String(),
                        Instance = c.String(nullable: false),
                        ConnDirect = c.String(),
                        ArcgisVersion = c.String(),
                        DataReg = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        EstReg = c.String(nullable: false, maxLength: 1),
                    })
                .PrimaryKey(t => t.SdeId);
            
            DropTable("dbo.cfg_servidores_sde");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.cfg_servidores_sde",
                c => new
                    {
                        ServidorSdeId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Server = c.String(nullable: false),
                        Instance = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Login = c.String(nullable: false),
                        DataReg = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        EstReg = c.String(nullable: false, maxLength: 1),
                    })
                .PrimaryKey(t => t.ServidorSdeId);
            
            DropTable("dbo.cfg_servidor_sde");
        }
    }
}
