using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySQLWebApp.Models
{
	public class SchoolDbContext : DbContext
	{
        /// <summary>
        /// Optionつきコンストラクタ
        /// </summary>
        /// <param name="options">Options.</param>
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options)
            : base(options) { }

        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        //public SchoolDbContext() { }

        #region テーブルの宣言
        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollmetns { get; set; }
        public DbSet<Course> Courses { get; set; }

        #endregion


    }
}
