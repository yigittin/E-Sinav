﻿using Microsoft.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Threading;

namespace SanalVaka.EntityFrameworkCore;

public static class SanalVakaEfCoreEntityExtensionMappings
{
    private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

    public static void Configure()
    {
        SanalVakaGlobalFeatureConfigurator.Configure();
        SanalVakaModuleExtensionConfigurator.Configure();

        OneTimeRunner.Run(() =>
        {
                /* You can configure extra properties for the
                 * entities defined in the modules used by your application.
                 *
                 * This class can be used to map these extra properties to table fields in the database.
                 *
                 * USE THIS CLASS ONLY TO CONFIGURE EF CORE RELATED MAPPING.
                 * USE SanalVakaModuleExtensionConfigurator CLASS (in the Domain.Shared project)
                 * FOR A HIGH LEVEL API TO DEFINE EXTRA PROPERTIES TO ENTITIES OF THE USED MODULES
                 *
                 * Example: Map a property to a table field:
                */
            ObjectExtensionManager.Instance
                .MapEfCoreProperty<IdentityUser, bool>(
                    "Ogrenci",
                    (entityBuilder, propertyBuilder) =>
                    {
                    }
                );
            ObjectExtensionManager.Instance
                .MapEfCoreProperty<IdentityUser, string>(
                    "OgrenciNo",
                    (entityBuilder, propertyBuilder) =>
                    {
                    }
                );
            ObjectExtensionManager.Instance
                .MapEfCoreProperty<IdentityUser, bool>(
                    "Yetkili",
                    (entityBuilder, propertyBuilder) =>
                    {
                    }
                );
            ObjectExtensionManager.Instance
                .MapEfCoreProperty<IdentityUser, string>(
                    "YetkiliNo",
                    (entityBuilder, propertyBuilder) =>
                    {
                    }
                );
            /*
         * See the documentation for more:
         * https://docs.abp.io/en/abp/latest/Customizing-Application-Modules-Extending-Entities
         */
        });
    }
}
