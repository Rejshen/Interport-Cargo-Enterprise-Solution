// Data/InterportCargoDBContext.cs
using Microsoft.EntityFrameworkCore;
using InterportCargo.Models;

namespace InterportCargo.Data
{
    public class InterportCargoDBContext : DbContext
    {
        public InterportCargoDBContext(DbContextOptions<InterportCargoDBContext> options)
            : base(options)
        {
        }

        // --- Tables ---
        public DbSet<Customer> Customers { get; set; } 
        public DbSet<Employee> Employees { get; set; } 
        public DbSet<QuotationRequest> QuotationRequests { get; set; } 
        public DbSet<RateItem> RateItems { get; set; } 
        public DbSet<Quotation> Quotations { get; set; }
        public DbSet<OfficerNotification> OfficerNotifications { get; set; } = default!;
        public DbSet<CustomerMessage> CustomerMessages { get; set; } = default!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Optional: map table names explicitly (nice for clarity)
            modelBuilder.Entity<Customer>().ToTable("Customers");
            modelBuilder.Entity<Employee>().ToTable("Employees");
            modelBuilder.Entity<QuotationRequest>().ToTable("QuotationRequests");
            modelBuilder.Entity<RateItem>().ToTable("RateItems");
            modelBuilder.Entity<Quotation>().ToTable("Quotations");

            // Simple indexes (emails are used as usernames)
            modelBuilder.Entity<Customer>()
                        .HasIndex(c => c.Email)
                        .IsUnique(false); // allow duplicates in prototype if you prefer

            modelBuilder.Entity<Employee>()
                        .HasIndex(e => e.Email)
                        .IsUnique(false);

            // Seed the Rate Schedule (GST handled as 10% in calculation, not stored)
            modelBuilder.Entity<RateItem>().HasData(
                new RateItem { Id = 1, FeeType = "Wharf Booking fee", TwentyFt = 60m, FortyFt = 70m },
                new RateItem { Id = 2, FeeType = "Lift on/Lift Off", TwentyFt = 80m, FortyFt = 120m },
                new RateItem { Id = 3, FeeType = "Fumigation", TwentyFt = 220m, FortyFt = 280m },
                new RateItem { Id = 4, FeeType = "LCL Delivery Depot", TwentyFt = 400m, FortyFt = 500m },
                new RateItem { Id = 5, FeeType = "Tailgate Inspection", TwentyFt = 120m, FortyFt = 160m },
                new RateItem { Id = 6, FeeType = "Storage Fee", TwentyFt = 240m, FortyFt = 300m },
                new RateItem { Id = 7, FeeType = "Facility Fee", TwentyFt = 70m, FortyFt = 100m },
                new RateItem { Id = 8, FeeType = "Wharf Inspection", TwentyFt = 60m, FortyFt = 90m }
            );
        }
    }
}
