namespace INFOPC.Services
{
    public class ConfigurationService
    {
        private readonly IConfiguration _configuration;

        public ConfigurationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string? GetSectionString(string section, string setting)
        {
            return _configuration.GetSection(section).GetRequiredSection(setting).Value;
        }

        public string? GetSetting(string setting)
        {
            return _configuration.GetRequiredSection(setting).Value;
        }
    }

}
