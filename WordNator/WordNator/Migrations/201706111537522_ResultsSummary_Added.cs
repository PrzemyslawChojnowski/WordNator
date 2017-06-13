namespace WordNator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ResultsSummary_Added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ResultsSummaries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LessonType = c.Int(nullable: false),
                        LessonLanguage = c.Int(nullable: false),
                        Try = c.Int(nullable: false),
                        CorrectAnswersCount = c.Int(nullable: false),
                        QuestionsCount = c.Int(nullable: false),
                        LessonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lessons", t => t.LessonId, cascadeDelete: true)
                .Index(t => t.LessonId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ResultsSummaries", "LessonId", "dbo.Lessons");
            DropIndex("dbo.ResultsSummaries", new[] { "LessonId" });
            DropTable("dbo.ResultsSummaries");
        }
    }
}
