namespace Signet.Core.Utils
{
    using System;
    using System.Diagnostics;
    using System.Globalization;

    public static class Util
    {
        public static TimeSpan Benchmark(Action action)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            action();
            stopwatch.Stop();
            return stopwatch.Elapsed;
        }

        public static string[] GetAllDateFormats()
        {
            CultureInfo ci = CultureInfo.CurrentCulture;
            string[] fmts = ci.DateTimeFormat.GetAllDateTimePatterns();
            return ci.DateTimeFormat.GetAllDateTimePatterns();
        }
    }
}