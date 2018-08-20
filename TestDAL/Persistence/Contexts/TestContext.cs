using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using TestDAL.Core.Domains;

namespace TestDAL.Persistence.Contexts
{
    public partial class TestContext : DbContext
    {
        public TestContext() { }
        public TestContext(DbContextOptions options) : base(options) { }


        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Class> Classes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Class>().HasKey(c => c.Id);
            modelBuilder.Entity<Class>().Property(c => c.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Class>().Property(c => c.Name).HasMaxLength(250).IsRequired().IsUnicode(true);
            modelBuilder.Entity<Class>().Property(c => c.Location).HasMaxLength(1000).IsUnicode(true);
            modelBuilder.Entity<Class>().Property(c => c.TeacherName).HasMaxLength(250).IsUnicode(true);
            modelBuilder.Entity<Class>().Property(c => c.RowVersion).IsRowVersion();

            modelBuilder.Entity<Student>().HasKey(c => c.Id);
            modelBuilder.Entity<Student>().Property(c => c.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Student>().Property(c => c.Name).HasMaxLength(250).IsRequired().IsUnicode(true);
            modelBuilder.Entity<Student>().HasIndex(c => new { c.ClassId, c.Name }).IsUnique();
            modelBuilder.Entity<Student>().Property(c => c.Age);
            modelBuilder.Entity<Student>().Property(c => c.GPA);
            modelBuilder.Entity<Student>().Property(c => c.RowVersion).IsRowVersion();

        }
    }
}