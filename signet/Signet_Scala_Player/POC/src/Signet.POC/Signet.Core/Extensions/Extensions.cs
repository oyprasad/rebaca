namespace Signet.Core.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using Signet.Core.Utils;
    using System.Text;
    using System.Globalization;

    public static class Extensions
    {
        private static readonly Regex HexColorCodeExpression = new Regex("^#?([a-f]|[A-F]|[0-9]){3}(([a-f]|[A-F]|[0-9]){3})?$", RegexOptions.Singleline | RegexOptions.Compiled);

        public static TimeSpan Benchmark(this Action action)
        {
            return Util.Benchmark(action);
        }

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (T item in enumerable)
            {
                action(item);
            }
        }

        public static string FormatWith(this string target, params object[] args)
        {
            return string.Format(target, args);
        }

        public static bool IsValidColorCode(this string colorCode)
        {
            return (!string.IsNullOrEmpty(colorCode) && HexColorCodeExpression.IsMatch(colorCode));
        }

        public static T ToEnum<T>(this string target, T defaultValue) where T : IComparable, IFormattable
        {
            T convertedValue = defaultValue;
            if (!string.IsNullOrEmpty(target))
            {
                try
                {
                    convertedValue = (T)Enum.Parse(typeof(T), target.Trim(), true);
                }
                catch (ArgumentException)
                {
                }
            }
            return convertedValue;
        }

        public static DateTime ParseAsDateTime(this string input)
        {
            DateTime result = DateTime.MinValue;
            if (DateTime.TryParseExact(input, Util.GetAllDateFormats(), CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
            {
                return result;
            }
            else
            {
                if (input.Contains("24"))
                {
                    input = input.Replace("24", "00");
                    result = input.ParseAsDateTime().AddDays(1);
                }
            }

            return result;
        }

        public static string DisplayAsDateAndTime(this DateTime dateTime)
        {
            return (dateTime != DateTime.MinValue) ? dateTime.ToString("MM/dd/yyyy hh:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture) : "null";
        }

        public static string DisplayAsDate(this DateTime dateTime)
        {
            return (dateTime != DateTime.MinValue) ? dateTime.ToString("MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture) : "null";
        }

        public static string DisplayAsTime(this DateTime dateTime)
        {
            return (dateTime != DateTime.MinValue) ? dateTime.ToString("hh:mm:ss.ff", System.Globalization.CultureInfo.InvariantCulture) : "null";
        }

        public static string GetTimeInterval(this TimeSpan interval)
        {
            return interval.ToString(@"hh\:mm\:ss\.ff");
        }

        public static string CreateExceptionString(this Exception e)
        {
            StringBuilder sb = new StringBuilder();
            CreateExceptionString(sb, e, string.Empty);

            return sb.ToString();
        }

        private static void CreateExceptionString(StringBuilder sb, Exception e, string indent)
        {
            if (indent == null)
            {
                indent = string.Empty;
            }
            else if (indent.Length > 0)
            {
                sb.AppendFormat("{0}Inner ", indent);
            }

            sb.AppendFormat("Exception Found:\n{0}Type: {1}", indent, e.GetType().FullName);
            sb.AppendFormat("\n{0}Message: {1}", indent, e.Message);
            sb.AppendFormat("\n{0}Source: {1}", indent, e.Source);
            sb.AppendFormat("\n{0}Stacktrace: {1}", indent, e.StackTrace);

            if (e.InnerException != null)
            {
                sb.Append("\n");
                CreateExceptionString(sb, e.InnerException, indent + "  ");
            }
        }
    }
}