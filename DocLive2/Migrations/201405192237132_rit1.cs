namespace DocLive2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rit1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.QAs",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        question = c.String(),
                        ans = c.String(),
                        flag = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.QAs");
        }
    }
}
