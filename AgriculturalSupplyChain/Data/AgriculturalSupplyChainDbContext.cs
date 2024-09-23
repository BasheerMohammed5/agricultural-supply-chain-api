using AgriculturalSupplyChain.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;


namespace AgriculturalSupplyChain.Data
{
    public class AgriculturalSupplyChainDbContext : DbContext
    {
        public AgriculturalSupplyChainDbContext(DbContextOptions<AgriculturalSupplyChainDbContext> options) : base(options)
        {
        }

        public DbSet<Farmer> Farmers { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<Batch> Batches { get; set; }
        public DbSet<Storage> Storages { get; set; }
        public DbSet<QualityTest> QualityTests { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<QualityEmployee> QualityEmployees { get; set; }
        public DbSet<Retailer> Retailers { get; set; }
        public DbSet<Harvest> Harvests { get; set; }
        public DbSet<Packaging> Packagings { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Storage> Storage { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Composite keys and relationships
            modelBuilder.Entity<RolePermission>()
                .HasKey(rp => new { rp.RoleId, rp.PermissionId }); // Correct composite key

            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Role)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(rp => rp.RoleId);

            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Permission)
                .WithMany(p => p.RolePermissions)
                .HasForeignKey(rp => rp.PermissionId);

            // System Manager permissions
            modelBuilder.Entity<Permission>().HasData(
                new Permission { Id = 1, PermissionName = "ManageUsers" },
                new Permission { Id = 2, PermissionName = "ViewDashboard" },
                new Permission { Id = 3, PermissionName = "GenerateReports" }
            );

            // Assign roles
            modelBuilder.Entity<Role>().HasData(
                new Role { RoleId = 1, RoleName = "SystemManager" },
                new Role { RoleId = 2, RoleName = "DataEntry" },
                new Role { RoleId = 3, RoleName = "QualityControlInspector" },
                new Role { RoleId = 4, RoleName = "SupplyChainTrader" },
                new Role { RoleId = 5, RoleName = "RetailTrader" }
            );

            // Assign permissions to System Manager
            modelBuilder.Entity<RolePermission>().HasData(
                new RolePermission { RoleId = 1, PermissionId = 1 }, // Correct RoleId property
                new RolePermission { RoleId = 1, PermissionId = 2 },
                new RolePermission { RoleId = 1, PermissionId = 3 }
            );

            base.OnModelCreating(modelBuilder);
        }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    // Composite keys and relationships
        //    modelBuilder.Entity<RolePermission>()
        //        .HasKey(rp => new { rp.RolePermissionId, rp.PermissionId });

        //    // System Manager permissions
        //    modelBuilder.Entity<Permission>().HasData(
        //        new Permission { Id = 1, PermissionName = "ManageUsers" },
        //        new Permission { Id = 2, PermissionName = "ViewDashboard" },
        //        new Permission { Id = 3, PermissionName = "GenerateReports" }
        //    );

        //    // Assign roles
        //    modelBuilder.Entity<Role>().HasData(
        //        new Role { RoleId = 1, RoleName = "SystemManager" },
        //        new Role { RoleId = 2, RoleName = "DataEntry" },
        //        new Role { RoleId = 3, RoleName = "QualityControlInspector" },
        //        new Role { RoleId = 4, RoleName = "SupplyChainTrader" },
        //        new Role { RoleId = 5, RoleName = "RetailTrader" }
        //    );

        //    // Assign permissions to System Manager
        //    modelBuilder.Entity<RolePermission>().HasData(
        //        new RolePermission { RolePermissionId = 1, PermissionId = 1 },
        //        new RolePermission { RolePermissionId = 1, PermissionId = 2 },
        //        new RolePermission { RolePermissionId = 1, PermissionId = 3 }
        //    );

        //    base.OnModelCreating(modelBuilder);
        //}

    }
}
