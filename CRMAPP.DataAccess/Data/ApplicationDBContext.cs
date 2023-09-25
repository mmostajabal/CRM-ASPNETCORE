using CRMAPP.Models;
using CRMAPP.Utility.DateTimeConvertor;
using CRMAPP.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMAPP.DataAccess.Data
{
    public  class ApplicationDBContext  : IdentityDbContext
    {   
        
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) :base(options) 
        {
            
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Call> Calls { get; set; }

        public DbSet<CustomerNo> customerNos { get; set; }

        public DbSet<Language> languages { get; set; }

        
        public DbSet<CustomerCall> customerCalls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CustomerCall>().HasNoKey();

            modelBuilder.Entity<Call>(builder =>
            {
                // Date is a DateOnly property and date on database
                builder.Property(x => x.CallDate)
                    .HasConversion<DateOnlyConverter, DateOnlyComparer>();

                // Time is a TimeOnly property and time on database
                builder.Property(x => x.CallTime)
                    .HasConversion<TimeOnlyConverter, TimeOnlyComparer>();
            });

            modelBuilder.Entity<Customer>(builder =>
            {
                // Date is a DateOnly property and date on database
                builder.Property(x => x.DateOfBirth)
                    .HasConversion<DateOnlyConverter, DateOnlyComparer>();

            });

            modelBuilder.Entity<Language>()
         .HasKey(m => new { m.LanguageCode, m.Statmentkey});

        }
    }
}
