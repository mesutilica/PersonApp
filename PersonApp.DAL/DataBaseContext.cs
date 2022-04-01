using Microsoft.EntityFrameworkCore;
using PersonApp.Entities;
using System;

namespace PersonApp.DAL
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext()
        {

        }
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {

        }
        public DbSet<Person> People { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Report> Reports { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(LocalDB)\MSSQLLocalDB; Database=PersonApp; Trusted_Connection=True; MultipleActiveResultSets=true");
            //base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>().
                   HasData(new AppUser
                   {
                       Id = 1,
                       Username = "Admin",
                       Password = "123456",
                       Email = "admin@personapp.net",
                       Name = "demo",
                       Surname = "test",
                       Phone = "0216",
                       CreateDate = DateTime.Now,
                       IsActive = true,
                       IsAdmin = true
                   });
            base.OnModelCreating(modelBuilder);
        }
    }
}
//add-migration galleriesAdded -context DataBaseContext
//update-database -context DataBaseContext