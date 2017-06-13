namespace WordNator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Result_Entity_Added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Results",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AnswerCorrectness = c.Boolean(nullable: false),
                        UserAnswer = c.String(),
                        QuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Questions", t => t.QuestionId, cascadeDelete: true)
                .Index(t => t.QuestionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Results", "QuestionId", "dbo.Questions");
            DropIndex("dbo.Results", new[] { "QuestionId" });
            DropTable("dbo.Results");
        }
    }
}
