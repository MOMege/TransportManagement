using Serilog;
using TransportManagement.Api.Configurations;
using TransportManagement.Api.Logging;

var builder = WebApplication.CreateBuilder(args);

// 🔥 Apply Serilog config (Console + File)
        SerilogConfiguration.ConfigureSerilog(builder);

// Register Application services (Db, UoW, MediatR, Validation, Repo)
       builder.Services.AddApplicationServices(builder.Configuration);

// Controllers + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Authentication audiltsystem
builder.Services.AddHttpContextAccessor();
var app = builder.Build();

// Custom Error Middleware
app.UseMiddleware<TransportManagement.Api.Middlewares.ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRequestIdLogging();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
