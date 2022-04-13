using BarSystem.WebApi.Data;
using BarSystem.WebApi.DTOs;
using BarSystem.WebApi.Interfaces.Data;
using BarSystem.WebApi.Models;
using FluentValidation.AspNetCore;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["Auth0:Domain"];
    options.Audience = builder.Configuration["Auth0:Audience"];
});

builder.Services.AddControllers()
                .AddFluentValidation(options =>
                {
                    options.ImplicitlyValidateChildProperties = true;
                    options.ImplicitlyValidateRootCollectionElements = true;
                    options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
                });


builder.Services.AddDbContext<BarSystemDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Database"));
});

builder.Services.AddMediatR(typeof(Program));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "BarSystem Api",
        Description = "A simple ASP.NET Core web API with educational purpose",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "PRocaspana",
            Url = new Uri("https://github.com/PatoRocaspana")
        }
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

    var securitySchema = new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows()
        {
            AuthorizationCode = new OpenApiOAuthFlow
            {
                AuthorizationUrl = new Uri($"{builder.Configuration["Auth0:Domain"]}authorize"),
                TokenUrl = new Uri($"{builder.Configuration["Auth0:Domain"]}oauth/token")
            }
        }
    };

    options.AddSecurityDefinition("oauth2", securitySchema);

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
          {
              { new OpenApiSecurityScheme
                  {
                       Reference = new OpenApiReference
                       {
                         Type = ReferenceType.SecurityScheme,
                         Id = "oauth2"
                       },
                       Scheme = "oauth2",
                       Name = "oauth2",
                       In = ParameterLocation.Header
                  },
                new List<string>()
              }
          }
    );
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CreateAccess", policy =>
                      policy.RequireClaim("permissions", "create:dish"));
    options.AddPolicy("UpdateAccess", policy =>
                      policy.RequireClaim("permissions", "update:dish"));
    options.AddPolicy("DeleteAccess", policy =>
                      policy.RequireClaim("permissions", "delete:dish"));
});

var config = new TypeAdapterConfig();

config.NewConfig<Table, TableInfoDto>()
            .Map(dest => dest.DishesInfoDto, src => src.Dishes)
            .Map(dest => dest.DrinksInfoDto, src => src.Drinks);

builder.Services.AddSingleton(config);
builder.Services.AddScoped<IMapper, ServiceMapper>();

builder.Services.AddTransient<IDishRepository, DishRepository>();
builder.Services.AddTransient<IDrinkRepository, DrinkRepository>();
builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddTransient<ITableRepository, TableRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");

        if (builder.Configuration["SwaggerUISecurityMode"]?.ToLower() == "oauth2")
        {
            options.OAuthClientId(builder.Configuration["Auth0:ClientId"]);
            options.OAuthClientSecret(builder.Configuration["Auth0:ClientSecret"]);
            options.OAuthAppName("BarSystemClient");
            options.OAuthAdditionalQueryStringParams(new Dictionary<string, string> { { "audience", builder.Configuration["Auth0:Audience"] } });
            options.OAuthUsePkce();
        }
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
