namespace AdminPanel.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddListMenuItem15 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.seg_funcao", "Parent_MenuItemId", "dbo.seg_funcao");
            DropIndex("dbo.seg_funcao", new[] { "Parent_MenuItemId" });
            DropColumn("dbo.seg_funcao", "ParentId");
            RenameColumn(table: "dbo.seg_funcao", name: "Parent_MenuItemId", newName: "ParentId");
            CreateIndex("dbo.seg_funcao", "ParentId");
            AddForeignKey("dbo.seg_funcao", "ParentId", "dbo.seg_funcao", "MenuItemId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.seg_funcao", "ParentId", "dbo.seg_funcao");
            DropIndex("dbo.seg_funcao", new[] { "ParentId" });
            RenameColumn(table: "dbo.seg_funcao", name: "ParentId", newName: "Parent_MenuItemId");
            AddColumn("dbo.seg_funcao", "ParentId", c => c.Int());
            CreateIndex("dbo.seg_funcao", "Parent_MenuItemId");
            AddForeignKey("dbo.seg_funcao", "Parent_MenuItemId", "dbo.seg_funcao", "MenuItemId");
        }
    }
}
