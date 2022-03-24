using System;
using System.Collections.Generic;
using System.Text;

namespace TaxCalculator.Pl.FormatException
{
    public static class ExceptionExtension
    {
        public static string GetExceptionFootprints(this Exception exception)
        {
            var traceStringBuilder = new StringBuilder();
            traceStringBuilder.AppendLine("-------------------------------------------------------------");
            traceStringBuilder.AppendLine(DateTime.Now.ToString());
            traceStringBuilder.AppendLine($"Method: {exception.TargetSite}");
            traceStringBuilder.AppendLine("Error:");
            traceStringBuilder.AppendLine(exception.StackTrace);
            
            return traceStringBuilder.ToString();
        }
    }
}
