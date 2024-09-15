using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Shoes.Bussines;
using Shoes.Bussines.CustomLaunguageManager;
using Shoes.Bussines.DependencyResolver;
using Shoes.Core.Entities.Concrete;
using Shoes.Core.Helpers;
using Shoes.DataAccess.Concrete.SqlServer;
using Shoes.Entites;
using Shoes.WebAPI.Services;
using System.Globalization;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigurationHelper.Initialize(builder.Configuration);

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    List<CultureInfo> supportedCultures = ConfigurationHelper.config.GetSection("SupportedLanguage:Launguages").Get<string[]>()
    .Select(x=>new CultureInfo(x)).ToList();
        options.DefaultRequestCulture = new RequestCulture(ConfigurationHelper.config.GetSection("SupportedLanguage:Default").Get<string>());
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});
builder.Services.AddControllers().AddDataAnnotationsLocalization().AddViewLocalization(); ;
builder.Services.AddDbContext<AppDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});
builder.Services.AddIdentity<AppUser, AppRole>()
      .AddEntityFrameworkStores<AppDBContext>()
        .AddDefaultTokenProviders()
    .AddErrorDescriber<MultilanguageIdentityErrorDescriber>();
builder.Services.AddAllScoped();
builder.Services.AddTransient<ErrorMessageService>();
builder.Services.AddTransient<IdentityErrorDescriber, MultilanguageIdentityErrorDescriber>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc("v1", new OpenApiInfo { Title = "KarlShoes", Version = "v1", Description = "Identity Service API swagger client." });
    x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Example: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\\"
    });
    x.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }

    });
});
// JWT Auth
#region JWT Auth
builder.Services.AddAuthentication(auth =>
{
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.TokenValidationParameters = new()
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidAudience = builder.Configuration["Token:Audience"],
        ValidIssuer = builder.Configuration["Token:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
        LifetimeValidator = (notBefore, expires, securityToken, validationParameters) =>
            expires != null ? expires > DateTime.Now : false,

        NameClaimType = ClaimTypes.Email
    };
});
#endregion

#region Fluent Validation Registration add services to the container.
builder.Services/*.AddControllers(options => options.Filters.Add<ValidationFilters>())*/
    .AddFluentValidation(configuration =>
    {
        //configuration.RegisterValidatorsFromAssemblyContaining<LoginDTOValidation>();
        configuration.DisableDataAnnotationsValidation = true;
        configuration.LocalizationEnabled = true;
        configuration.AutomaticValidationEnabled = false;
        configuration.DisableDataAnnotationsValidation = true;
        configuration.ValidatorOptions.LanguageManager = new ValidationLanguageManager();
        configuration.ValidatorOptions.LanguageManager.Culture = new CultureInfo("az");
    })
 ;

#endregion
var corsRuls = builder.Configuration.GetValue<string>("Domain:Front");


builder.Services.AddCors(o =>
{
    o.AddPolicy(corsRuls,
         p =>
         {
             p.AllowAnyHeader();
             p.AllowAnyMethod();
             p.AllowAnyOrigin();
         }
        );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors(corsRuls);
//get in ui culture Accept-Language: 
app.UseRequestLocalization();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
