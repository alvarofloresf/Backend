using Backend.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Data
{
    public class UserDBContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        
        public UserDBContext(DbContextOptions<UserDBContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserEntity>().ToTable("Users");
            modelBuilder.Entity<UserEntity>().Property(u => u.Id).ValueGeneratedOnAdd();
        }

        //dotnet tool install --global dotnet-ef --version 3.1.4
        //dotnet ef --help
        //dotnet ef migrations add {InitialCreate}
        //dotnet ef database update
    }
}
