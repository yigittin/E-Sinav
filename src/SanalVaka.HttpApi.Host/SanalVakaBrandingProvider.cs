using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace SanalVaka;

[Dependency(ReplaceServices = true)]
public class SanalVakaBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "SanalVaka";
}
