using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace BLTS.WebUi.Localization
{
    public static class WebUiLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(WebUiConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(WebUiLocalizationConfigurer).GetAssembly(),
                        "BLTS.WebUi.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
