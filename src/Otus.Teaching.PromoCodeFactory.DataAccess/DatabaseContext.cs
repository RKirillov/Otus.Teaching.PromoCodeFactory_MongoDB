using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Xml;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.Logging;
using Otus.Teaching.PromoCodeFactory.Core.Domain.Administration;
using Otus.Teaching.PromoCodeFactory.Core.Domain.PromoCodeManagement;
using Otus.Teaching.PromoCodeFactory.Core.Options;
using Otus.Teaching.PromoCodeFactory.DataAccess.Data;

namespace Otus.Teaching.PromoCodeFactory.DataAccess
{
    /// <summary>
    /// Контекст.
    /// </summary>
    public class DatabaseContext : DbContext
    {
        private ConnectionOptions _connectionOptions { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options, ConnectionOptions connectionOptions) : base(options)
        {
            _connectionOptions = connectionOptions;
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        //TODO не все нужно объявлять в DBSet, стр 446
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
        //public DbSet<Role> Roles { get; set; }

        /// <summary>
        /// Роли.
        /// </summary>
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());//Your assembly here

            //TODO ???
            base.OnModelCreating(modelBuilder);
//ошибка
/*            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var propertyInfo in entityType.ClrType.GetProperties())
                {
                    if (propertyInfo.PropertyType == typeof(string))
                    {
                        entityType.GetProperty(propertyInfo.Name)
                            .SetMaxLength(1000);
                    }
                }
            }*/
            modelBuilder.Entity<CustomerPreference>().HasKey(sc => new { sc.CustomerId, sc.PreferenceId });//

            modelBuilder.Entity<CustomerPreference>()//
                .HasOne<Customer>(sc => sc.Customer)
                .WithMany(s => s.CustomerPreference)
                .HasForeignKey(sc => sc.CustomerId);

            modelBuilder.Entity<CustomerPreference>()//
                .HasOne<Preference>(sc => sc.Preference)
                .WithMany(s => s.CustomerPreference)
                .HasForeignKey(sc => sc.PreferenceId);

            modelBuilder.Entity<PromoCode>()//
                .HasOne<Customer>(s => s.Customer)
                .WithMany(g => g.PromoCodes)
                .HasForeignKey(s => s.CustomerId);

                 modelBuilder.Entity<PromoCode>()//
                .HasOne<Preference>(s => s.Preference)
                .WithOne(g => g.PromoCode)
                .HasForeignKey<Preference>(s => s.PromoCodeId);

            modelBuilder.Entity<Employee>()//
                .HasOne<Role>(s => s.Role)
                .WithOne(g => g.Employee)
                .HasForeignKey<Role>(g => g.EmployeeId);

            modelBuilder.Entity<PromoCode>()//
                .HasOne<Employee>(s => s.PartnerManager)
                .WithMany(g => g.PromoCodes)
                .HasForeignKey(s => s.PartnerManagerId);

            //modelBuilder.Entity<Customer>().Property(c => c.FirstName).HasMaxLength(100);
            //modelBuilder.Entity<Preference>().Property(c => c.Name).HasMaxLength(100);

            //Инициализация начальных данных
            modelBuilder.Entity<Role>().HasData(FakeDataFactory.Roles);
            modelBuilder.Entity<Employee>().HasData(FakeDataFactory.Employees);
            modelBuilder.Entity<CustomerPreference>().HasData(FakeDataFactory.CustomerPreferences);
            modelBuilder.Entity<Preference>().HasData(FakeDataFactory.Preferences);
            modelBuilder.Entity<Customer>().HasData(FakeDataFactory.Customers);
            modelBuilder.Entity<PromoCode>().HasData(FakeDataFactory.PromoCodes);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlite(_connectionOptions.ConnectionString);
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
        }

        //не передать в sqllite
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder
                .Properties<string>()
                //.AreUnicode(false)
                .HaveMaxLength(12);
        }
    }

}