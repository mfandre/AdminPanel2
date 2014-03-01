namespace AdminPanel.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addServidoresSde2 : DbMigration
    {
        public override void Up()
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.cfg_servidores_sde");
        }
    }
}
