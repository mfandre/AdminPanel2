namespace AdminPanel.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddListMenuItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.seg_funcao", "MenuItem_MenuItemId", c => c.Int());
            CreateIndex("dbo.seg_funcao", "MenuItem_MenuItemId");
            AddForeignKey("dbo.seg_funcao", "MenuItem_MenuItemId", "dbo.seg_funcao", "MenuItemId");
            DropColumn("dbo.seg_funcao", "Parent");
        }
        
        public override void Down()
        {
            AddColumn("dbo.seg_funcao", "Parent", c => c.Int(nullable: false));
            DropForeignKey("dbo.seg_funcao", "MenuItem_MenuItemId", "dbo.seg_funcao");
            DropIndex("dbo.seg_funcao", new[] { "MenuItem_MenuItemId" });
            DropColumn("dbo.seg_funcao", "MenuItem_MenuItemId");
        }
    }
}
