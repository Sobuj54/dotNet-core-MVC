using Microsoft.EntityFrameworkCore;
using BulkyBookWeb.Models;

namespace BulkyBookWeb.Data
{
    public class DBcontext : DbContext
    {
        public DBcontext(DbContextOptions<DBcontext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder); 
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Clothing" },
                new Category { Id = 2, Name = "Computer" },
                new Category { Id = 3, Name = "Shoes" }
                );
        }
    }
}
