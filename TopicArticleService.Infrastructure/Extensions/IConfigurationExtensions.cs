using Microsoft.Extensions.Configuration;

namespace TopicArticleService.Infrastructure.Extensions
{
    internal static class IConfigurationExtensions
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
