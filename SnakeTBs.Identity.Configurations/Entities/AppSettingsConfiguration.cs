using Microsoft.Extensions.Configuration;
using SnakeTBs.Configurations.Entities;
using SnakeTBs.Configurations.Statics.Entities;
using SnakeTBs.Identity.Configurations.Models.AppSettings.Entities;

namespace SnakeTBs.Identity.Configurations.Entities
{
    public class AppSettingsConfiguration : SingletonConfiguration<AppSettingsConfiguration>
    {
        protected override string _nameFile => NameFileStatic.AppSettings;
        public RootAppSetting Root { get; private set; }
        protected override void Load(IConfigurationRoot root)
        {
            Root = new RootAppSetting();
            root.Bind(Root);
        }
    }
}
