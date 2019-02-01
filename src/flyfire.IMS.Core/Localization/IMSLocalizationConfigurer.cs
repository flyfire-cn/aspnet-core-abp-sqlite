using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace flyfire.IMS.Localization
{
    public static class IMSLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(IMSConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(IMSLocalizationConfigurer).GetAssembly(),
                        "flyfire.IMS.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
