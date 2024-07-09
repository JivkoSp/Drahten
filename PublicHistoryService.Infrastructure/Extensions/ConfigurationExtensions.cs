using Microsoft.Extensions.Configuration;

namespace PublicHistoryService.Infrastructure.Extensions
{
    internal static class ConfigurationExtensions
    {
        public static TOptions GetOptions<TOptions>(this IConfiguration configuration, string sectionName)
            where TOptions : new()
        {
            var options = new TOptions();

            configuration.GetSection(sectionName).Bind(options);

            return options;
        }
    }
}
