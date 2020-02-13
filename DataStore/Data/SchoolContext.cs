using ConsoleApp.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Data
{
    public class SchoolContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=SchoolDb;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // データのシードを定義
            // 親データ
            modelBuilder.Entity<Student>().HasData(
                new Student{ StudentId = 1, LastName = "HogeHoge", FirstName = "Wick" },
                new Student{ StudentId = 2, LastName = "FugoFugo", FirstName = "Queen" }
            );

            // 子データ
            modelBuilder.Entity<Course>().HasData(
                new Course{ CourseId = 1, CourseName = "ふー"},
                new Course{ CourseId = 2, CourseName = "ばー"}
            );
        }

    }
}
