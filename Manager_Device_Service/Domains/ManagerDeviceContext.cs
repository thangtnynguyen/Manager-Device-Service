using Manager_Device_Service.Core.Constant;
using Manager_Device_Service.Domains.Data.Borrow;
using Manager_Device_Service.Domains.Data.Identity;
using Manager_Device_Service.Domains.Data.Relate_Device;
using Manager_Device_Service.Domains.Data.University;
using Manager_Device_Service.Domains.Data.User;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Reflection;

namespace Manager_Device_Service.Domains
{
    public class ManagerDeviceContext:IdentityDbContext<User, Role, int>
    {
        public ManagerDeviceContext()
        {
        }
        public ManagerDeviceContext(DbContextOptions options) : base(options)
        {
        }
        //identity
        public virtual DbSet<Permission>? Permissions { get; set; }
        public virtual DbSet<RolePermission>? RolePermissions { get; set; }

        // univesity
        public virtual DbSet<Building> Buildings { get; set; } = null!;
        public virtual DbSet<Floor> Floors { get; set; } = null!;
        public virtual DbSet<Room> Rooms { get; set; } = null!;

        //device
        public virtual DbSet<DeviceCategory> DeviceCategories { get; set; } = null!;
        public virtual DbSet<Device> Devices { get; set; } = null!;
        public virtual DbSet<DeviceLog> DeviceLogs { get; set; } = null!;

        //borrow
        public virtual DbSet<BorrowRequest> BorrowRequests { get; set; } = null!;


        //other

        public virtual DbSet<AccountRequest> AccountRequests { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>().ToTable("Users");

            builder.Entity<Role>().ToTable("Roles");

            builder.Entity<Microsoft.AspNetCore.Identity.IdentityUserClaim<int>>().ToTable("UserClaims").HasKey(x => x.Id);

            builder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaims")
            .HasKey(x => x.Id);

            builder.Entity<Microsoft.AspNetCore.Identity.IdentityUserLogin<int>>().ToTable("UserLogins").HasKey(x => x.UserId);

            builder.Entity<Microsoft.AspNetCore.Identity.IdentityUserRole<int>>().ToTable("UserRoles")
            .HasKey(x => new { x.RoleId, x.UserId });

            builder.Entity<IdentityUserToken<int>>().ToTable("UserTokens")
               .HasKey(x => new { x.UserId });

            builder.Entity<Microsoft.AspNetCore.Identity.IdentityUserRole<int>>()
               .HasOne<User>()
               .WithMany(u => u.UserRoles)
               .HasForeignKey(ur => ur.UserId)
               .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Microsoft.AspNetCore.Identity.IdentityUserRole<int>>()
                .HasOne<Role>()
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
               .Entries()
               .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entityEntry in entries)
            {
                var dateCreatedProp = entityEntry.Entity.GetType().GetProperty(SystemConstant.DateCreatedField);
                if ((entityEntry.State == EntityState.Added || entityEntry.State == EntityState.Modified)
                    && dateCreatedProp != null)
                {
                    dateCreatedProp.SetValue(entityEntry.Entity, DateTime.Now);
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

    }
}
