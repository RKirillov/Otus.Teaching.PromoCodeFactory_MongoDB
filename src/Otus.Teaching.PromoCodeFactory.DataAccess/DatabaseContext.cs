using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;

namespace Otus.Teaching.PromoCodeFactory.DataAccess
{
    public class Student
    {
        //public int Id { get; set; }
        public string Name { get; set; }
        public Grade Grade { get; set; }
    }

    public class Grade
    {
        public int GradeId { get; set; }
        public string GradeName { get; set; }
        public string Section { get; set; }

        public ICollection<Student> Students { get; set; }
    }
    /// <summary>
    /// Контекст.
    /// </summary>
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        /// <summary>
        /// Клиенты.
        /// </summary>
        public DbSet<Customer> Customers { get; set; }

        /// <summary>
        /// Предпочтения.
        /// </summary>
        public DbSet<Preference> Preferences { get; set; }

        /// <summary>
        /// Сущность для Many-To-Many Customer/Preference
        /// </summary>
        public DbSet<CustomerPreference> CustomerPreferences { get; set; }

        /// <summary>
        /// Промокоды.
        /// </summary>
        public DbSet<PromoCode> PromoCodes { get; set; }


        /// <summary>
        /// Роли.
        /// </summary>
        public DbSet<Role> Roles { get; set; }


        /// <summary>
        /// Роли.
        /// </summary>
        public DbSet<Employee> Employees { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //TODO ???
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CustomerPreference>().HasKey(sc => new { sc.CustomerId, sc.PreferenceId });

            modelBuilder.Entity<CustomerPreference>()
                .HasOne<Customer>(sc => sc.Customer)
                .WithMany(s => s.CustomerPreference)
                .HasForeignKey(sc => sc.CustomerId);

            modelBuilder.Entity<CustomerPreference>()
                .HasOne<Preference>(sc => sc.Preference)
                .WithMany(s => s.CustomerPreference)
                .HasForeignKey(sc => sc.PreferenceId);


            modelBuilder.Entity<PromoCode>()
                .HasOne<Customer>(s => s.Customer)
                .WithMany(g => g.Promocodes)
                .HasForeignKey(s => s.Id);


            modelBuilder.Entity<Customer>().Property(c => c.FirstName).HasMaxLength(100);
            modelBuilder.Entity<Preference>().Property(c => c.Name).HasMaxLength(100);


        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
        }
    }
}