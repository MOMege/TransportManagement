using Serilog;

namespace TransportManagement.Api.Configurations
{
    public static class RequestIdConfiguration
    {
        public static IApplicationBuilder UseRequestIdLogging(this IApplicationBuilder app)
        {
            return app.Use(async (context, next) =>
            {
                var requestId = Guid.NewGuid().ToString("N");

                Serilog.Context.LogContext.PushProperty("RequestId", requestId);

                Log.Information("=============== REQUEST START ================");

                await next();

                Log.Information("=============== REQUEST END ==================");
            });
        }
    }
}
