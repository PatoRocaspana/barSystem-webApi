using BarSystem.WebApi.Data;
using BarSystem.WebApi.DTOs;
using BarSystem.WebApi.Interfaces.Data;
using BarSystem.WebApi.Models;
using FluentValidation.AspNetCore;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

builder.Services.AddSwaggerDocument(config =>
{
    config.PostProcess = document =>
    {
        document.Info.Version = "v1";
        document.Info.Title = "BarSystem Api";
        document.Info.Description = "A simple ASP.NET Core web API with educational purpose";
        document.Info.TermsOfService = "None";
        document.Info.Contact = new NSwag.OpenApiContact
        {
            Name = "PRocaspana",
            Email = string.Empty,
            Url = "https://github.com/PatoRocaspana"
        };
    };
});


//TypeAdapterConfig<TableDto, Table>.NewConfig()
//    .PreserveReference(true);
//TypeAdapterConfig<DrinkDto, Drink>.NewConfig()
//    .PreserveReference(true);
//TypeAdapterConfig<DishDto, Dish>.NewConfig()
//    .PreserveReference(true);
//TypeAdapterConfig<EmployeeDto, Employee>.NewConfig()
//    .PreserveReference(true);


var config = new TypeAdapterConfig();

config.NewConfig<TableDto, Table>()
            .Map(dest => dest.Dishes, src => src.DishesDto)
            .Map(dest => dest.Drinks, src => src.DrinksDto)
            .Map(dest => dest.Employee, src => src.EmployeeDto);

config.NewConfig<Table, TableDto>()
            .Map(dest => dest.DishesDto, src => src.Dishes)
            .Map(dest => dest.DrinksDto, src => src.Drinks)
            .Map(dest => dest.EmployeeDto, src => src.Employee);

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
    app.UseOpenApi();
    app.UseSwaggerUi3();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
