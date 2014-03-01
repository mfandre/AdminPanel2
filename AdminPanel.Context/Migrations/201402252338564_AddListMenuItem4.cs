namespace AdminPanel.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddListMenuItem4 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.seg_funcao", "ParentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.seg_funcao", "ParentId", c => c.Int());
        }
    }
}
