using System;
using System.Collections.Generic;
using System.Text;
using Serilog;


namespace TaxCalculator.ConsoleApp.Utilities
{
    internal static class LogInfo
    {
        public static void WriteLog(string path, Exception exception)
        {
            var traceStringBuilder = new StringBuilder();
            traceStringBuilder.AppendLine("-------------------------------------------------------------");
            traceStringBuilder.AppendLine(DateTime.Now.ToString());
            traceStringBuilder.AppendLine($"Method: {exception.TargetSite}");
            traceStringBuilder.AppendLine("Error:");
            traceStringBuilder.AppendLine(exception.StackTrace);

            Log.Logger = new LoggerConfiguration().WriteTo.File(path).CreateLogger();
            Log.Information(traceStringBuilder.ToString());
        }
    }
}
