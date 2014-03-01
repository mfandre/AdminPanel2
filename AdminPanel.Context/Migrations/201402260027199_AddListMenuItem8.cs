namespace AdminPanel.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddListMenuItem8 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.seg_funcao", "ParentItemId", "dbo.seg_funcao");
            DropIndex("dbo.seg_funcao", new[] { "ParentItemId" });
            AddColumn("dbo.seg_funcao", "MenuItem_MenuItemId", c => c.Int());
            CreateIndex("dbo.seg_funcao", "MenuItem_MenuItemId");
            AddForeignKey("dbo.seg_funcao", "MenuItem_MenuItemId", "dbo.seg_funcao", "MenuItemId");
            DropColumn("dbo.seg_funcao", "ParentItemId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.seg_funcao", "ParentItemId", c => c.Int());
            DropForeignKey("dbo.seg_funcao", "MenuItem_MenuItemId", "dbo.seg_funcao");
            DropIndex("dbo.seg_funcao", new[] { "MenuItem_MenuItemId" });
            DropColumn("dbo.seg_funcao", "MenuItem_MenuItemId");
            CreateIndex("dbo.seg_funcao", "ParentItemId");
            AddForeignKey("dbo.seg_funcao", "ParentItemId", "dbo.seg_funcao", "MenuItemId");
        }
    }
}
