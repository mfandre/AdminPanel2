namespace AdminPanel.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddListMenuItem9 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.seg_funcao", "Root", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.seg_funcao", "Root");
        }
    }
}
