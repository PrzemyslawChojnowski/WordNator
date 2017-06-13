namespace WordNator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Result_Entity_Modified : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Results", "LessonId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Results", "LessonId");
        }
    }
}
