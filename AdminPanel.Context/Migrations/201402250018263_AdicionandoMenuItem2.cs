namespace AdminPanel.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionandoMenuItem2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.seg_funcao",
                c => new
                    {
                        MenuItemId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Url = c.String(),
                        Icon = c.String(),
                        Leaf = c.Boolean(nullable: false),
                        Parent = c.Int(nullable: false),
                        DataReg = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        EstReg = c.String(),
                    })
                .PrimaryKey(t => t.MenuItemId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.seg_funcao");
        }
    }
}
