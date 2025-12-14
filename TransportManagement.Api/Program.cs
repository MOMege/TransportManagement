using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;
using TransportManagement.Api.Configurations;
using TransportManagement.Api.Logging;
using TransportManagement.Domain.Entites;
using TransportManagement.Infrastructure.Persistence;
using TransportManagement.Infrastructure.SeedData;

var builder = WebApplication.CreateBuilder(args);

// Identity Table
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // هنا نقدر نخصص سياسة كلمات المرور لاحقاً
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
})
.AddEntityFrameworkStores<TransportDbContext>()
.AddDefaultTokenProviders();



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


// قراءة إعدادات JWT من appsettings
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);

// تسجيل Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        
        ValidateIssuer = true,
        ValidIssuer = jwtSettings["Issuer"],

        ValidateAudience = true,
        ValidAudience = jwtSettings["Audience"],

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!)),

        ValidateLifetime = true, // يرفض التوكن المنتهي
        ClockSkew = TimeSpan.Zero // بدون تأخير انتهاء
    };
});
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await IdentitySeeder.SeedAsync(services);
}

app.UseAuthentication();  // أولاً Authentication
app.UseAuthorization();   // ثانياً Authorization

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
