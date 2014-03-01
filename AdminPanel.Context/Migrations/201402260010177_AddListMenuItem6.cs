namespace AdminPanel.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddListMenuItem6 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.seg_funcao", "Parent_MenuItemId", "dbo.seg_funcao");
            DropIndex("dbo.seg_funcao", new[] { "Parent_MenuItemId" });
            AddColumn("dbo.seg_funcao", "ParentItemId", c => c.Int());
            CreateIndex("dbo.seg_funcao", "ParentItemId");
            AddForeignKey("dbo.seg_funcao", "ParentItemId", "dbo.seg_funcao", "MenuItemId");
            DropColumn("dbo.seg_funcao", "Parent_MenuItemId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.seg_funcao", "Parent_MenuItemId", c => c.Int());
            DropForeignKey("dbo.seg_funcao", "ParentItemId", "dbo.seg_funcao");
            DropIndex("dbo.seg_funcao", new[] { "ParentItemId" });
            DropColumn("dbo.seg_funcao", "ParentItemId");
            CreateIndex("dbo.seg_funcao", "Parent_MenuItemId");
            AddForeignKey("dbo.seg_funcao", "Parent_MenuItemId", "dbo.seg_funcao", "MenuItemId");
        }
    }
}
