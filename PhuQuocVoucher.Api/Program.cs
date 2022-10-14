using System.Globalization;
using System.Text;
using CrudApiTemplate.CustomBinding;
using CrudApiTemplate.ExceptionFilter;
using CrudApiTemplate.Repository;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Localization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PhuQuocVoucher.Business.Dtos;
using PhuQuocVoucher.Business.Dtos.MailDto;
using PhuQuocVoucher.Business.Services;
using PhuQuocVoucher.Data.Models;
using PhuQuocVoucher.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers(option =>
{
    option.Filters.Add<CrudExceptionFilterAttribute>();
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Database
builder.Services.AddDbContext<PhuQuocDataContext>();
builder.Services.AddScoped<IUnitOfWork ,PqUnitOfWork>();

//Services
builder.Services.InitServices();
builder.Services.AddLocalization(o => o.ResourcesPath = "Resources");
builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
});
builder.Services.AddCors(o => o.AddPolicy("AllowAnyOrigin",
    policyBuilder => policyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
));

//Authenticate
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        ValidateIssuerSigningKey = true,
        ValidIssuer = configuration["JwtSetting:Issuer"],
        ValidAudience = configuration["JwtSetting:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSetting:SecurityKey"]))
    };
});

//Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo {Title = "PhuQuocVoucher", Version = "v1"});
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        Scheme = "bearer",
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "Put **_ONLY_** your JWT Bearer token on text box below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {jwtSecurityScheme, Array.Empty<string>()}
    });
    var xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly).ToList();
    xmlFiles.ForEach(xmlFile => c.IncludeXmlComments(xmlFile));
    c.OperationFilter<OpenApiParameterIgnoreFilter>();
    c.EnableAnnotations();
});

//Authorization
builder.Services.AddAuthorization(o =>
{
    o.AddPolicy("Admin", policy => policy.RequireClaim("role", Role.Admin.ToString()));
});

builder.Services.AddLogging(config =>
{
    config.AddDebug();
    config.AddConsole();
});

//Firebase
FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential.FromFile(configuration["FireBaseConfig"])
});

//Mapper
DtoConfig.ConfigMapper();

//Custom Binding
builder.Services.AddControllersWithViews(options => options.ValueProviderFactories.Add(new ClaimValueProviderFactory()));

builder.Services.AddLocalization(o => { o.ResourcesPath = "Resources"; });
builder.Services.AddSingleton(new MailSetting()
{
    Host = configuration["MailSettings:Host"],
    Mail = configuration["MailSettings:Mail"],
    DisplayName = configuration["MailSettings:DisplayName"],
    Password = configuration["MailSettings:Password"],
    Port = int.Parse(configuration["MailSettings:Port"])
});
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors("AllowAnyOrigin");
var supportCultures = new[] {new CultureInfo("en-US")};
app.UseRequestLocalization(options =>
{
    options.DefaultRequestCulture = new RequestCulture("en-US");
    options.SupportedCultures = supportCultures;
    options.SupportedUICultures = supportCultures;
});
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});
app.UseAuthentication();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();