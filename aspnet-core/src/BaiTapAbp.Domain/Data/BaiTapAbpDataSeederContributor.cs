using System;
using System.Linq;
using System.Threading.Tasks;
using BaiTapAbp.Authorization;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.Uow;

namespace BaiTapAbp.Data;

public class BaiTapAbpDataSeederContributor(
    IdentityRoleManager roleManager,
    IdentityUserManager userManager,
    IPermissionManager permissionManager)
    : IDataSeedContributor, ITransientDependency
{
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