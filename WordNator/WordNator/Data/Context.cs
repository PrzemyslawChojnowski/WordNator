using SQLite.CodeFirst;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WordNator.Models;

namespace WordNator.Data
{
    public class Context : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<LessonQuestion> LessonQuestions { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<ResultsSummary> ResultsSummary { get; set; }

        public Context()
            : base("WordNatorDB")
        {
            var path = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName)
                + "\\..\\..\\Data";
            AppDomain.CurrentDomain.SetData("DataDirectory", path);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var sqliteConnectionInitializer = new SqliteDropCreateDatabaseWhenModelChanges<Context>(modelBuilder);
            Database.SetInitializer(sqliteConnectionInitializer);   
        }
    }
}
