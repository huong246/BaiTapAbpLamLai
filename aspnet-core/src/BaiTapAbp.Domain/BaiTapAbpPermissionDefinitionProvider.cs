using Volo.Abp.Authorization.Permissions;
using BaiTapAbp.Localization;
using Microsoft.Extensions.Localization;
using Volo.Abp.Localization;
namespace BaiTapAbp;

public class BaiTapAbpPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var group = context.AddGroup(RolePermissions.GroupName, L ("Permission: BaiTapAbp"));
        
        var userPermission = group.AddPermission(RolePermissions.Users.Default, L("Permission:Users.Default"));
        userPermission.AddChild(RolePermissions.Users.Create, L("Permission:Users.Create"));
        userPermission.AddChild(RolePermissions.Users.Edit, L("Permission:Users.Edit"));
        userPermission.AddChild(RolePermissions.Users.Delete, L("Permission:Users.Delete"));

        var shopPermission = group.AddPermission(RolePermissions.Shops.Default, L("Permission:Shops.Default"));
        shopPermission.AddChild(RolePermissions.Shops.Create, L("Permission:Shops.Create"));
        shopPermission.AddChild(RolePermissions.Shops.Edit, L("Permission:Shops.Edit"));
        shopPermission.AddChild(RolePermissions.Shops.Delete, L("Permission:Shops.Delete"));
        
        var productPermission = group.AddPermission(RolePermissions.Products.Default, L("Permission:Products.Default"));
        productPermission.AddChild(RolePermissions.Products.Create, L("Permission:Products.Create"));
        productPermission.AddChild(RolePermissions.Products.Edit, L("Permission:Products.Edit"));
        productPermission.AddChild(RolePermissions.Products.Delete, L("Permission:Products.Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<BaiTapAbpResource>(name);
    }
}