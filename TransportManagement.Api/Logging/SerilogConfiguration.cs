using Serilog;

namespace TransportManagement.Api.Logging
{
    public static class SerilogConfiguration
    {
        public static void ConfigureSerilog(WebApplicationBuilder builder)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console(
                   outputTemplate:
                  "{Timestamp:yyyy-MM-dd HH:mm:ss} | {Level:u3} | RequestId={RequestId} | {Message:lj}{NewLine}{Exception}")
                   .WriteTo.File(
                    "Logs/log-.txt",
                     rollingInterval: RollingInterval.Day,
                     outputTemplate:
                      "{Timestamp:yyyy-MM-dd HH:mm:ss} | {Level:u3} | RequestId={RequestId} | {Message:lj}{NewLine}{Exception}"
                       + "------------------------------------------------------------------------" + "{NewLine}")

                .CreateLogger();

            builder.Host.UseSerilog();
        }
    }
}
