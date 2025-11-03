using BaiTapAbp.Entities;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace BaiTapAbp.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class BaiTapAbpDbContext :
    AbpDbContext<BaiTapAbpDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */ 
    public virtual DbSet<ProductEntity> Products { get; set; }
    public virtual DbSet<ShopEntity> Shops { get; set; }
    public virtual DbSet<SellerRequestEntity> SellerRequests { get; set; }
    public virtual DbSet<CategoryEntity>  Categories { get; set; }
    //Identity
 
    public DbSet<IdentityUser> Users { get;set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }
    public DbSet<IdentitySession> Sessions { get; set; }
    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }
    public BaiTapAbpDbContext(DbContextOptions<BaiTapAbpDbContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */
        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();
 
        builder.Entity<UserEntity>(b =>
        {
            b.ToTable("AbpUsers");
            b.ConfigureByConvention();  
            b.Property(x => x.FullName).IsRequired().HasMaxLength(100);
            b.Property(x => x.Address).HasMaxLength(100);
            b.Property(x => x.Gender);
            
        });

        builder.Entity<ShopEntity>(s =>
        {
            s.ToTable("Shop");
            s.ConfigureByConvention();
            s.Property(x => x.Name).IsRequired().HasMaxLength(100);
            s.Property(x => x.Address).HasMaxLength(100);
            s.HasOne<UserEntity>()
                .WithOne()
                .HasForeignKey<ShopEntity>(x => x.SellerId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        builder.Entity<ProductEntity>(p =>
        {
            
            p.ToTable("Product");
            p.ConfigureByConvention();
            p.Property(x => x.Name).IsRequired().HasMaxLength(100);
            p.Property(x => x.Price)
                .HasColumnType("decimal(18,2)");
            p.Property(x => x.Stock);
   
            p.HasOne<ShopEntity>()
                .WithMany()
                .HasForeignKey(p=> p.ShopId)
                .OnDelete(DeleteBehavior.Restrict);
            p.HasOne<CategoryEntity>()
                .WithMany()
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        builder.Entity<CategoryEntity>(c =>
        {
            c.ToTable("Category");
            c.ConfigureByConvention();
            c.Property(x => x.Name).IsRequired().HasMaxLength(100);
            c.HasIndex(x => x.Name).IsUnique();
        });
    }
}
