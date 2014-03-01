namespace AdminPanel.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddListMenuItem13 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.seg_funcao", "Parent_MenuItemId", "dbo.seg_funcao");
            DropIndex("dbo.seg_funcao", new[] { "Parent_MenuItemId" });
            AlterColumn("dbo.seg_funcao", "Parent_MenuItemId", c => c.Int(nullable: false));
            CreateIndex("dbo.seg_funcao", "Parent_MenuItemId");
            AddForeignKey("dbo.seg_funcao", "Parent_MenuItemId", "dbo.seg_funcao", "MenuItemId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.seg_funcao", "Parent_MenuItemId", "dbo.seg_funcao");
            DropIndex("dbo.seg_funcao", new[] { "Parent_MenuItemId" });
            AlterColumn("dbo.seg_funcao", "Parent_MenuItemId", c => c.Int());
            CreateIndex("dbo.seg_funcao", "Parent_MenuItemId");
            AddForeignKey("dbo.seg_funcao", "Parent_MenuItemId", "dbo.seg_funcao", "MenuItemId");
        }
    }
}
