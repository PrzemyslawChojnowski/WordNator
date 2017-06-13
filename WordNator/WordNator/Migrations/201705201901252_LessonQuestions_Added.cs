namespace WordNator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LessonQuestions_Added : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.QuestionLessons", "Question_Id", "dbo.Questions");
            DropForeignKey("dbo.QuestionLessons", "Lesson_Id", "dbo.Lessons");
            DropIndex("dbo.QuestionLessons", new[] { "Question_Id" });
            DropIndex("dbo.QuestionLessons", new[] { "Lesson_Id" });
            CreateTable(
                "dbo.LessonQuestions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Lesson_Id = c.Int(),
                        Question_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lessons", t => t.Lesson_Id)
                .ForeignKey("dbo.Questions", t => t.Question_Id)
                .Index(t => t.Lesson_Id)
                .Index(t => t.Question_Id);
            
            DropTable("dbo.QuestionLessons");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.QuestionLessons",
                c => new
                    {
                        Question_Id = c.Int(nullable: false),
                        Lesson_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Question_Id, t.Lesson_Id });
            
            DropForeignKey("dbo.LessonQuestions", "Question_Id", "dbo.Questions");
            DropForeignKey("dbo.LessonQuestions", "Lesson_Id", "dbo.Lessons");
            DropIndex("dbo.LessonQuestions", new[] { "Question_Id" });
            DropIndex("dbo.LessonQuestions", new[] { "Lesson_Id" });
            DropTable("dbo.LessonQuestions");
            CreateIndex("dbo.QuestionLessons", "Lesson_Id");
            CreateIndex("dbo.QuestionLessons", "Question_Id");
            AddForeignKey("dbo.QuestionLessons", "Lesson_Id", "dbo.Lessons", "Id", cascadeDelete: true);
            AddForeignKey("dbo.QuestionLessons", "Question_Id", "dbo.Questions", "Id", cascadeDelete: true);
        }
    }
}
