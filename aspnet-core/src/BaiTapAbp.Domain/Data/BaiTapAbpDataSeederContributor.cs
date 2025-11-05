using System;
using System.Linq;
using System.Threading.Tasks;
using BaiTapAbp.Authorization;
using Microsoft.Extensions.Configuration;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.Uow;
using Volo.Abp.Validation;

namespace BaiTapAbp.Data;

public class BaiTapAbpDataSeederContributor(
    IdentityRoleManager roleManager,
    IdentityUserManager userManager,
    IPermissionManager permissionManager,
    IConfiguration configuration)
    : IDataSeedContributor, ITransientDependency
{
    
   
    public class AdminUserConfig
    {
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
    [UnitOfWork]
    public async Task SeedAsync(DataSeedContext context)
    {
        await CreateRoleAsync(UserRole.Admin);
        await CreateRoleAsync(UserRole.Seller);
        await CreateRoleAsync(UserRole.Customer);
        await GrantAllPermissionsToRoleAsync(UserRole.Admin);
        await permissionManager.SetForRoleAsync(UserRole.Seller, RolePermissions.Shops.Default, true);
        await permissionManager.SetForRoleAsync(UserRole.Seller, RolePermissions.Shops.Create, true);
        await permissionManager.SetForRoleAsync(UserRole.Seller, RolePermissions.Shops.Edit, true);
        await  permissionManager.SetForRoleAsync(UserRole.Seller, RolePermissions.Shops.Delete, true);
        await permissionManager.SetForRoleAsync(UserRole.Seller, RolePermissions.Products.Default, true);
        await permissionManager.SetForRoleAsync(UserRole.Seller, RolePermissions.Products.Create, true);
        await permissionManager.SetForRoleAsync(UserRole.Seller, RolePermissions.Products.Edit, true);
        await permissionManager.SetForRoleAsync(UserRole.Seller, RolePermissions.Products.Delete, true);
        await permissionManager.SetForRoleAsync(UserRole.Admin, RolePermissions.Categories.Default, true);
        await permissionManager.SetForRoleAsync(UserRole.Admin, RolePermissions.Categories.Create, true);
        await permissionManager.SetForRoleAsync(UserRole.Admin, RolePermissions.Categories.Edit, true);
        await permissionManager.SetForRoleAsync(UserRole.Admin, RolePermissions.Categories.Delete, true);

        await CreateAdminUserAsync();
    }

 
    private async Task CreateAdminUserAsync()
    {
        var adminConfig = configuration.GetSection("Identity:Users:Admin").Get<AdminUserConfig>();

        if (adminConfig == null)
        {
            return;
        }
        var existingAdmin = await userManager.FindByNameAsync(adminConfig.UserName);
        IdentityUser adminUser;
        if (existingAdmin == null)
        { 
                adminUser = new IdentityUser(
                Guid.NewGuid(),
                adminConfig.UserName,
                adminConfig.Email
            )
            {
                Name = adminConfig.FullName 
            };
            var result = await userManager.CreateAsync(adminUser, adminConfig.Password);
            if (!result.Succeeded)
            {
                throw new AbpValidationException("Could not create the admin user.");
            }
            var roleResult = await userManager.AddToRoleAsync(adminUser, UserRole.Admin);
        
            if (!roleResult.Succeeded)
            {
                throw new AbpValidationException("Could not assign Admin role to the admin user.");
            }
        }
        else
        {
            
            adminUser = existingAdmin;
        }
        if (!await userManager.IsInRoleAsync(adminUser, UserRole.Admin))
        {
           
            var roleResult = await userManager.AddToRoleAsync(adminUser, UserRole.Admin);
            if (!roleResult.Succeeded)
            {
                throw new AbpValidationException("Could not assign Admin role to the admin user.");
            }
        }
        
    }
    private async Task CreateRoleAsync(string roleName)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new IdentityRole(Guid.NewGuid(), roleName));
        }
    }
    private async Task GrantAllPermissionsToRoleAsync(string roleName)
    {
        var allPermissions = typeof(RolePermissions)
            .GetNestedTypes()
            .SelectMany(t => t.GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.FlattenHierarchy))
            .Where(fi => fi.IsLiteral && !fi.IsInitOnly && fi.FieldType == typeof(string))
            .Select(x => (string)x.GetRawConstantValue()!)
            .ToList();
                
        foreach (var permission in allPermissions)
        {
            await permissionManager.SetForRoleAsync(roleName, permission, true);
        }
    }
}
