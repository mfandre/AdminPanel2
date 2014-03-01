namespace AdminPanel.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddListMenuItem2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.seg_funcao", "MenuItem_MenuItemId", "dbo.seg_funcao");
            DropIndex("dbo.seg_funcao", new[] { "MenuItem_MenuItemId" });
            AddColumn("dbo.seg_funcao", "ParentId", c => c.Int());
            AddColumn("dbo.seg_funcao", "Parent_MenuItemId", c => c.Int());
            CreateIndex("dbo.seg_funcao", "Parent_MenuItemId");
            AddForeignKey("dbo.seg_funcao", "Parent_MenuItemId", "dbo.seg_funcao", "MenuItemId");
            DropColumn("dbo.seg_funcao", "MenuItem_MenuItemId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.seg_funcao", "MenuItem_MenuItemId", c => c.Int());
            DropForeignKey("dbo.seg_funcao", "Parent_MenuItemId", "dbo.seg_funcao");
            DropIndex("dbo.seg_funcao", new[] { "Parent_MenuItemId" });
            DropColumn("dbo.seg_funcao", "Parent_MenuItemId");
            DropColumn("dbo.seg_funcao", "ParentId");
            CreateIndex("dbo.seg_funcao", "MenuItem_MenuItemId");
            AddForeignKey("dbo.seg_funcao", "MenuItem_MenuItemId", "dbo.seg_funcao", "MenuItemId");
        }
    }
}
