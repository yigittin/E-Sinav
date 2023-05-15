using SanalVaka.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace SanalVaka.Permissions;

public class SanalVakaPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(SanalVakaPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(SanalVakaPermissions.MyPermission1, L("Permission:MyPermission1"));
        //var bolumGroup = context.AddGroup(SanalVakaPermissions.GroupName, L("Permission:Bolumler"));


        // BÖLÜM PERMISSION 
        var bolumPermission = myGroup.AddPermission(SanalVakaPermissions.Bolumler.Default, L("Permission:Bolumler"));
        bolumPermission.AddChild(SanalVakaPermissions.Bolumler.Create, L("Permission:Bolumler.Create"));
        bolumPermission.AddChild(SanalVakaPermissions.Bolumler.Edit, L("Permission:Bolumler.Edit"));
        bolumPermission.AddChild(SanalVakaPermissions.Bolumler.Delete, L("Permission:Bolumler.Delete"));

        // DERS PERMISSION
        var dersPermission = myGroup.AddPermission(SanalVakaPermissions.Dersler.Default, L("Permission:Dersler"));
        dersPermission.AddChild(SanalVakaPermissions.Dersler.Create, L("Permission:Dersler.Create"));
        dersPermission.AddChild(SanalVakaPermissions.Dersler.Edit, L("Permission:Dersler.Edit"));
        dersPermission.AddChild(SanalVakaPermissions.Dersler.Delete, L("Permission:Dersler.Delete"));

        // SINIF PERMISSION
        var sinifPermission = myGroup.AddPermission(SanalVakaPermissions.Siniflar.Default, L("Permission:Siniflar"));
        sinifPermission.AddChild(SanalVakaPermissions.Siniflar.Create, L("Permission:Siniflar.Create"));
        sinifPermission.AddChild(SanalVakaPermissions.Siniflar.Edit, L("Permission:Siniflar.Edit"));
        sinifPermission.AddChild(SanalVakaPermissions.Siniflar.Delete, L("Permission:Siniflar.Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<SanalVakaResource>(name);
    }
}
