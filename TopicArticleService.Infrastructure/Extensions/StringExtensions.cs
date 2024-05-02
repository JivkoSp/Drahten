using System.Text;

namespace TopicArticleService.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static string ToSnakeCase(this string value) 
        {
            if (string.IsNullOrEmpty(value))
                return value;

            StringBuilder builder = new StringBuilder();

            builder.Append(char.ToLower(value[0]));

            for (int i = 1; i < value.Length; i++)
            {
                if (char.IsUpper(value[i]))
                {
                    builder.Append('_');
                    builder.Append(char.ToLower(value[i]));
                    continue;
                }

                builder.Append(value[i]);
            }

            return builder.ToString();
        }
    }
}
