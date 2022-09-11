using Core.Security.Entities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Contexts
{
    public class BaseDbContext : DbContext
    {
        protected IConfiguration Configuration { get; set; }
        public DbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }
        public DbSet<Framework> Frameworks { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<GitAccount> GitAccountS { get; set; }




        public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //    base.OnConfiguring(
            //        optionsBuilder.UseSqlServer(Configuration.GetConnectionString("SomeConnectionString")));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProgrammingLanguage>(a =>
            {
                a.ToTable("ProgrammingLanguages").HasKey(k => k.Id);
                a.Property(a => a.Id).HasColumnName("Id");
                a.Property(a => a.Name).HasColumnName("Name");
            });

            modelBuilder.Entity<Framework>(a =>
            {
                a.ToTable("Frameworks").HasKey(k => k.Id);
                a.Property(a => a.Id).HasColumnName("Id");
                a.Property(a => a.ProgrammingLanguageId).HasColumnName("ProgrammingLanguageId");
                a.Property(a => a.Name).HasColumnName("Name");
                

                a.HasOne(a => a.ProgrammingLanguage);
            });

            modelBuilder.Entity<OperationClaim>(a =>
            {
                a.ToTable("OperationClaims").HasKey(k => k.Id);
                a.Property(a => a.Name).HasColumnName("Name");
            });

            modelBuilder.Entity<User>(a =>
            {
                a.ToTable("Users").HasKey(k => k.Id);
                a.Property(a => a.FirstName).HasColumnName("FirstName");
                a.Property(a => a.LastName).HasColumnName("LastName");
                a.Property(a => a.Email).HasColumnName("Email");
                a.Property(a => a.PasswordSalt).HasColumnName("PasswordSalt");
                a.Property(a => a.PasswordHash).HasColumnName("PasswordHash");
                a.Property(a => a.Status).HasColumnName("Status").HasDefaultValue(true);
                a.HasMany(a => a.RefreshTokens);
                a.HasMany(a => a.UserOperationClaims);
            });

            modelBuilder.Entity<UserOperationClaim>(a =>
            {
                a.ToTable("UserOperationClaims").HasKey(k => k.Id);
                a.Property(a => a.UserId).HasColumnName("UserId");
                a.Property(a => a.OperationClaimId).HasColumnName("OperationClaimId");

                a.HasOne(a => a.User);
                a.HasOne(a => a.OperationClaim);
            });

            modelBuilder.Entity<Account>(a =>
            {
                a.ToTable("Accounts");
                a.HasMany(a => a.GitProfiles);
                
            });

            modelBuilder.Entity<GitAccount>(p =>
            {
                p.ToTable("GitAccounts").HasKey(k => k.Id);
                p.Property(p => p.Id).HasColumnName("Id");
                p.Property(p => p.AccountId).HasColumnName("AccountId");
                p.Property(p => p.AddressLink).HasColumnName("ProfileUrl");
                p.HasOne(p => p.Account);
            });



            Framework[] frameworkSeeds = { new(1,1, "WPF"), new(2,1, "ASP.NET") };
            modelBuilder.Entity<Framework>().HasData(frameworkSeeds);

            ProgrammingLanguage[] programmingLanguageSeeds = { new(1, "C#"), new(2, "Java") };
            modelBuilder.Entity<ProgrammingLanguage>().HasData(programmingLanguageSeeds);


        }


    }
}

