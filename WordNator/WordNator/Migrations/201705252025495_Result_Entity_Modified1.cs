namespace WordNator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Result_Entity_Modified1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Results", "Try", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Results", "Try");
        }
    }
}
