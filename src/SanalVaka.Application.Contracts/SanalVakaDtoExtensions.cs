﻿using Volo.Abp.Identity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Threading;

namespace SanalVaka;

public static class SanalVakaDtoExtensions
{
    private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

    public static void Configure()
    {
        OneTimeRunner.Run(() =>
        {
            /* You can add extension properties to DTOs
             * defined in the depended modules.
             *
             * Example:
             *
             * ObjectExtensionManager.Instance
             *   .AddOrUpdateProperty<IdentityRoleDto, string>("Title");
             *
             * See the documentation for more:
             * https://docs.abp.io/en/abp/latest/Object-Extensions
             */
            ObjectExtensionManager.Instance.AddOrUpdateProperty<IdentityUserDto, bool>("Ogrenci");
            ObjectExtensionManager.Instance.AddOrUpdateProperty<IdentityUserCreateOrUpdateDtoBase, bool>("Ogrenci");
            ObjectExtensionManager.Instance.AddOrUpdateProperty<IdentityUserDto, string>("OgrenciNo");
            ObjectExtensionManager.Instance.AddOrUpdateProperty<IdentityUserCreateOrUpdateDtoBase, string>("OgrenciNo");
            ObjectExtensionManager.Instance.AddOrUpdateProperty<IdentityUserDto, bool>("Yetkili");
            ObjectExtensionManager.Instance.AddOrUpdateProperty<IdentityUserCreateOrUpdateDtoBase, bool>("Yetkili");
            ObjectExtensionManager.Instance.AddOrUpdateProperty<IdentityUserDto, string>("YetkiliNo");
            ObjectExtensionManager.Instance.AddOrUpdateProperty<IdentityUserCreateOrUpdateDtoBase, string>("YetkiliNo");
        });
    }
}
