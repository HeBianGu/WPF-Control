using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Diagnostics
{
    public static class TraceExtension
    {
        public static async void WriteLine(this string message)
        {
            Trace.WriteLine(message);
            await message.Write();
        }

        public static async Task TraceWrite(this string message)
        {
            Trace.Write(message);
            await message.Write();
        }

        public static async Task TraceWrite(this string format, params object[] args)
        {
            await TraceWrite(Format(format, args));
        }

        public static async Task Info(this string message)
        {
            Trace.TraceInformation(message);
            await message.Write();
        }

        public static async Task Info(this string format, params object[] args)
        {
            await Info(Format(format, args));
        }

        public static async Task Warning(this string message)
        {
            Trace.TraceWarning(message);
            await message.Write();
        }

        public static async Task Warning(this string format, params object[] args)
        {
            await Warning(Format(format, args));
        }

        public static async Task Fail(this string message)
        {
            Trace.Fail(message);
            await message.Write();
        }

        public static async Task Fail(this string format, params object[] args)
        {
            await Fail(Format(format, args));
        }

        public static async Task Error(this string message)
        {
            Trace.TraceError(message);
            await message.Error();
        }

        public static async Task Error(this string format, params object[] args)
        {
            await Error(Format(format, args));
        }

        public static async Task Error(this Exception exception, string message = null)
        {
            if (exception == null)
            {
                await Error(message);
                return;
            }
            var details = string.IsNullOrWhiteSpace(message) ? exception.ToString() : $"{message}{Environment.NewLine}{exception}";
            await Error(details);
        }

        public static async Task WriteIf(this bool condition, string message)
        {
            if (condition)
                await TraceWrite(message);
        }

        public static async Task InfoIf(this bool condition, string message)
        {
            if (condition)
                await Info(message);
        }

        public static async Task WarningIf(this bool condition, string message)
        {
            if (condition)
                await Warning(message);
        }

        public static async Task ErrorIf(this bool condition, string message)
        {
            if (condition)
                await Error(message);
        }

        public static IDisposable Measure(this string operationName)
        {
            return new MeasureScope(operationName);
        }

        private static string Format(this string format, object[] args)
        {
            return args == null || args.Length == 0 ? format : string.Format(format, args);
        }

        private sealed class MeasureScope : IDisposable
        {
            private readonly string _operationName;
            private readonly Stopwatch _stopwatch = Stopwatch.StartNew();

            public MeasureScope(string operationName)
            {
                _operationName = operationName;
            }

            public void Dispose()
            {
                _stopwatch.Stop();
                Info("{0} completed in {1} ms.", _operationName, _stopwatch.ElapsedMilliseconds);
            }
        }
    }
}
