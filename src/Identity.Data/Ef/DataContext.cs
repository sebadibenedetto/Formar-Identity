using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using Identity.Entities;

namespace Identity.Data.Ef
{
    public partial class DataContext : IdentityDbContext<User, Entities.IdentityRole, string, IdentityUserClaim<string>, IdentityUserRole, IdentityUserLogin, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public DataContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>(t =>
            {
                t.ToTable("Users");
                t.Property(b => b.Id).HasColumnName("UserId");
            });

            builder.Entity<IdentityUserClaim<string>>(t =>
            {
                t.ToTable("UserClaims");
                t.Property(b => b.Id).HasColumnName("UserClaimsId");
            });

            builder.Entity<IdentityRoleClaim<string>>(t =>
            {
                t.ToTable("RoleClaims");
            });

            builder.Entity<IdentityUserLogin>(t =>
            {
                t.ToTable("UserLogins");
                t.HasKey("ProviderKey", "LoginProvider");
            });

            builder.Entity<IdentityUserToken<string>>(t =>
            {
                t.ToTable("UserTokens");
                t.HasKey(hk => new { hk.UserId, hk.LoginProvider, hk.Name });
            });

           
            builder.Entity<Entities.IdentityRole>(t =>
            {
                t.ToTable("Roles");
                t.Property(b => b.Id).HasColumnName("RolesId");
                t.HasMany(r => r.Claims).WithOne().HasForeignKey(c => c.RoleId).IsRequired().OnDelete(DeleteBehavior.Cascade);
                t.HasMany(r => r.UserRoles).WithOne().HasForeignKey(r => r.RoleId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<IdentityUserRole>(t =>
            {
                t.ToTable("UserRoles");
                t.HasKey(hk => new { hk.UserId, hk.RoleId });
                t.HasOne(p => p.Role).WithMany(b => b.UserRoles).HasForeignKey(p => p.RoleId).HasConstraintName("FK_UserRoles_Roles_RoleId");
            });            
        }
    }
}
