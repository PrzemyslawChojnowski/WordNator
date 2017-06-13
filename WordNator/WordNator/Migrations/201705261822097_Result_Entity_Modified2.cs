namespace WordNator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Result_Entity_Modified2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Results", "LessonType", c => c.Int(nullable: false));
            AddColumn("dbo.Results", "LessonLanguage", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Results", "LessonLanguage");
            DropColumn("dbo.Results", "LessonType");
        }
    }
}
