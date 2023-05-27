using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.Context
{
    public class ContextDb : DbContext
    {
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Server=TEKNOKADİR\cabuk; Database=eReconciliationDb; Integrated Security=true");
        //}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432; Database=eReconciliationDb; UserName=postgres ; Password=1234");
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true); //burada postgre de hata verdiği için ekledim
            //AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        }

        public DbSet<AccountReconciliationDetail> AccountReconciliationDetails { get; set; }
        public DbSet<AccountReconciliation> AccountReconciliations { get; set; }
        public DbSet<BaBsReconciliation> BaBsReconciliations { get; set; }
        public DbSet<BaBsReconciliationDetail> BaBsReconciliationDetails { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<CurrencyAccount> CurrencyAccounts { get; set; }
        public DbSet<MailParameter> MailParameters { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserCompany> UserCompanys { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<MailTemplate> MailTemplates { get; set; }
    }
}
