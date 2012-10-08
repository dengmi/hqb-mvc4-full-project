namespace Tsingbo.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addimagefieldforarticle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "Image", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Articles", "Image");
        }
    }
}
