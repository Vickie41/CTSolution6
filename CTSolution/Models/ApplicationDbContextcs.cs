using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace CTSolution.Models
{
    public class ApplicationDbContextcs : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContextcs(DbContextOptions<ApplicationDbContextcs> options)
            : base(options)
        {
        }

        
        public DbSet<TaxPayerInfo> TaxPayerInfo { get; set; }
        public DbSet<PurchaseImportMaster> PurchaseImportMaster { get; set; }
        public DbSet<PurchaseImportDetail> PurchaseImportDetail { get; set; }

        public DbSet<PurchaseTransaction> PurchaseTransaction { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TaxPayerInfo>()
                .HasKey(t => t.TaxPayerPkid);

            modelBuilder.Entity<PurchaseImportMaster>()
                .HasKey(p => p.PurchaseImportPkid); 

            modelBuilder.Entity<PurchaseImportMaster>()
                .HasIndex(p => p.TransactionID) 
                .IsUnique();

            modelBuilder.Entity<PurchaseImportMaster>()
                .Property(p => p.PurchaseImportPkid)
                .ValueGeneratedOnAdd(); 

            

            modelBuilder.Entity<PurchaseImportDetail>()
            .HasOne(d => d.PurchaseImportMaster)
            .WithMany(p => p.PurchaseImportDetail)
            .HasForeignKey(d => d.TransactionID)
            .HasPrincipalKey(p => p.TransactionID);


            modelBuilder.Entity<PurchaseImportMaster>()
          .HasOne(p => p.TaxPayerInfo)
          .WithMany() // Adjust if needed
          .HasForeignKey(p => p.ImporterPkid);

            base.OnModelCreating(modelBuilder);
        }

        /*public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContextcs>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
                       .EnableSensitiveDataLogging()); // Enable sensitive data logging
        }*/

    }
}
