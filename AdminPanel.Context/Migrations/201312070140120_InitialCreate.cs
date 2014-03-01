namespace AdminPanel.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.System_Users",
                c => new
                    {
                        IDdoUsuário = c.Int(name: "ID do Usuário", nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 32),
                        Picture = c.String(),
                        Login = c.String(nullable: false, maxLength: 64),
                        Senha = c.String(nullable: false, maxLength: 64),
                        Email = c.String(nullable: false, maxLength: 128),
                        DatadeRegistro = c.DateTime(name: "Data de Registro", nullable: false),
                        Ativo = c.String(name: "Ativo?", nullable: false, maxLength: 1),
                    })
                .PrimaryKey(t => t.IDdoUsuário);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.System_Users");
        }
    }
}
