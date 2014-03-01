namespace AdminPanel.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionandoLocalidadeNaEntidadeCliente : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.cfg_cliente", "Localidade", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.cfg_cliente", "Localidade");
        }
    }
}
