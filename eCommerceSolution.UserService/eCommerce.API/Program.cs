using eCommerce.Infrastructure;
using eCommerce.Core;
using eCommerce.API.Middleware;
using System.Text.Json.Serialization;
using eCommerce.Core.Mapper;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

//Add Infrastructure services
builder.Services.AddInfrastructure();

//Add Core services
builder.Services.AddCore();

//Add controllers to the service collection
builder.Services.AddControllers().AddJsonOptions(
    options => {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

//Add AutoMapper Profile
builder.Services.AddAutoMapper(typeof(ApplicationUserMappingProfile).Assembly);

builder.Services.AddFluentValidationAutoValidation();

//Add API explorer services
builder.Services.AddEndpointsApiExplorer();

//Add swagger generation services to  create swagger specification
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options =>
options.AddDefaultPolicy(builder =>
{
    builder.WithOrigins("http://localhost:4200")
    .AllowAnyMethod()
    .AllowAnyHeader();
}));

var app = builder.Build();

app.UseExceptionHandlingMiddleware();
//Add Routing
app.UseRouting();

//Adds endpoint that can serve the swagger.json
app.UseSwagger();

//Adds swagger UI (interactive page to explorre the test API endpoints)
app.UseSwaggerUI();

app.UseCors();

//Add Authentication
app.UseAuthentication();

//Add Authorization
app.UseAuthorization();

//Add Controller Routes
app.MapControllers();

app.Run();
