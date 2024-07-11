using System.Text;
using System.Text.RegularExpressions;

namespace DrahtenWeb.Extensions
{
    public static class StringExtensions
    {
        //Splits snake_case string to separate words. The first word begins with upper letter.
        //For example: Given the word: cyber_attacks the result would be: Cyber attacks.
        public static string SplitSnakeCase(this string value) 
        {
            if (string.IsNullOrEmpty(value))
                return value;

            StringBuilder builder = new StringBuilder();

            builder.Append(char.ToUpper(value[0]));

            for (int i = 1; i < value.Length; i++)
            {
                if (value[i] == '_')
                {
                    builder.Append(' ');
                    continue;
                }

                builder.Append(value[i]);
            }

            return builder.ToString();
        }

        public static string FromSnakeToPascalCase(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            StringBuilder builder = new StringBuilder();

            builder.Append(char.ToUpper(value[0]));

            for (int i = 1; i < value.Length; i++)
            {
                if (value[i] == '_')
                {
                    builder.Append(char.ToUpper(value[i+1]));
                    i++;
                    continue;
                }

                builder.Append(value[i]);
            }

            return builder.ToString();
        }

        public static string PascalCaseWithSpaces(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            StringBuilder builder = new StringBuilder();

            builder.Append(value[0]);

            for (int i = 1; i < value.Length; i++)
            {
                if (char.IsUpper(value[i]))
                {
                    builder.Append(" ");
                }

                builder.Append(value[i]);
            }

            return builder.ToString();
        }

        public static string ExtractDomainFromUrl(this string url)
        {
            if (string.IsNullOrEmpty(url))
                return url;

            // Regular expression to capture the HTTP protocol and domain
            string pattern = @"^(?:https?:\/\/)?(?:www\.)?([^\/]+)";

            Regex regex = new Regex(pattern);

            // Match the URL
            Match match = regex.Match(url);

            if (match.Success)
            {
                // Extract the domain with protocol
                string domain = match.Groups[1].Value;

                return domain;
            }

            // Return the original URL if no match is found
            return url;
        }

        public static string ExtractDomainWithProtocolFromUrl(this string url)
        {
            if (string.IsNullOrEmpty(url))
                return url;

            // Regular expression to capture the HTTP protocol and domain
            string pattern = @"^(https?:\/\/(?:www\.)?[^\/]+)";

            Regex regex = new Regex(pattern);

            // Match the URL
            Match match = regex.Match(url);

            if (match.Success)
            {
                // Extract the domain with protocol
                string domain = match.Groups[1].Value;

                return domain;
            }

            // Return the original URL if no match is found
            return url;
        }
    }
}
