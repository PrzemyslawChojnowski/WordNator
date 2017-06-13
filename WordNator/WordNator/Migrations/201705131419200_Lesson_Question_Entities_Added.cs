namespace WordNator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Lesson_Question_Entities_Added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Lessons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EnglishWord = c.String(),
                        PolishWord = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.QuestionLessons",
                c => new
                    {
                        Question_Id = c.Int(nullable: false),
                        Lesson_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Question_Id, t.Lesson_Id })
                .ForeignKey("dbo.Questions", t => t.Question_Id, cascadeDelete: true)
                .ForeignKey("dbo.Lessons", t => t.Lesson_Id, cascadeDelete: true)
                .Index(t => t.Question_Id)
                .Index(t => t.Lesson_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lessons", "User_Id", "dbo.Users");
            DropForeignKey("dbo.QuestionLessons", "Lesson_Id", "dbo.Lessons");
            DropForeignKey("dbo.QuestionLessons", "Question_Id", "dbo.Questions");
            DropIndex("dbo.QuestionLessons", new[] { "Lesson_Id" });
            DropIndex("dbo.QuestionLessons", new[] { "Question_Id" });
            DropIndex("dbo.Lessons", new[] { "User_Id" });
            DropTable("dbo.QuestionLessons");
            DropTable("dbo.Questions");
            DropTable("dbo.Lessons");
        }
    }
}
