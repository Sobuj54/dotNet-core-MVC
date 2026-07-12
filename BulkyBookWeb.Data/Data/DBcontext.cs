using Microsoft.EntityFrameworkCore;
using BulkyBook.Models;

namespace BulkyBook.DataAccess
{
    public class DBcontext : DbContext
    {
        public DBcontext(DbContextOptions<DBcontext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder); 
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Clothing", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Computer" , DisplayOrder = 2},
                new Category { Id = 3, Name = "Shoes", DisplayOrder = 3 }
                );
        }
    }
}
