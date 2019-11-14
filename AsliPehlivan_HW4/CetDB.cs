using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AsliPehlivan_HW4
{
    public class CetDB : DbContext
    {
        string connectionString = @"Server=.\SQLEXPRESS;Database=CetDb;Trusted_Connection=True;";
        public DbSet<Student> Students { get; set; }

        public DbSet<Course> Courses { get; set; }
        public CetDB() : base()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
