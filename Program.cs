using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ReminderAPI.Contexts;
using ReminderAPI.Mappers;
using ReminderAPI.Repositories.Abstract;
using ReminderAPI.Repositories.Concrete;
using ReminderAPI.Services.Abstract;
using ReminderAPI.Services.Concrete;
using ReminderAPI.Services.EmailService;
using ReminderAPI.Validators.Reminder;
using System.Text;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
               Reference = new OpenApiReference
               {
                  Type=ReferenceType.SecurityScheme,
                  Id="Bearer"
               }
            }, new string[]{}
        }
    });
});  // Jwt Authorization with Swagger

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL"));
}, ServiceLifetime.Singleton); // Database Registration
builder.Services.AddAutoMapper(typeof(MappingProfiles));

builder.Services.AddSingleton<IReminderRepository, ReminderRepository>();
builder.Services.AddSingleton<IReminderMappingService, ReminderMappingService>();

builder.Services.AddSingleton(provider =>
{
    var botToken = builder.Configuration["TelegramBotToken:ReminderToken"];
    return new TelegramService(botToken) as ITelegramService;
});  // Telegram Service Registration
builder.Services.AddSingleton<IEmailService, EmailService>(); // Email Service Registration

builder.Services.AddHostedService<SendReminderService>();  // Background Service Registration

builder.Services.AddFluentValidationAutoValidation();  // Fluent Validation
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<AddValidator>();

builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = 429;
    options.AddFixedWindowLimiter("Fixed", _options =>
    {
        _options.Window = TimeSpan.FromSeconds(30);
        _options.PermitLimit = 4;
        _options.QueueLimit = 1;
        _options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
    });
});  // Rate Limit

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = "https://localhost",
        ValidAudience = "https://localhost",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("reminderapijwttoken")),
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
}); // JWT

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Use(async (ctx, next) =>
{
    try
    {
        await next();
    }
    catch (Exception)
    {
        ctx.Response.StatusCode = 500;
        await ctx.Response.WriteAsync("An error occurred");
    }
});  // Global Exception Handling

app.UseRateLimiter();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
