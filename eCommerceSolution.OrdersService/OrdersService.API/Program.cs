using FluentValidation;
using OrderService.API.Middleware;
using OrderService.BusinessLogicLayer;
using OrderService.BusinessLogicLayer.HttpClients;
using OrderService.BusinessLogicLayer.Policies;
using OrderService.BusinessLogicLayer.PolicyContracts;
using OrderService.BusinessLogicLayer.Validators;
using OrderService.DataAccessLayer;
using Polly;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddBusinessLogicLayer(builder.Configuration);
builder.Services.AddDataAccessLayer(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddValidatorsFromAssemblyContaining<OrderAddRequestValidator>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

builder.Services.AddTransient<IUsersMicroservicePolicies, UsersMicroservicePolicies>();
builder.Services.AddTransient<IProductsMicroservicePolicies, ProductsMicroservicePolicies>();

builder.Services.AddHttpClient<UserMicroserviceClient>(client =>
{
    client.BaseAddress = new Uri($"http://{Environment.GetEnvironmentVariable("UsersMicroserviceURI")}:{Environment.GetEnvironmentVariable("UsersMicroservicePort")}");
}).AddPolicyHandler
(
    builder.Services.BuildServiceProvider().GetRequiredService<IUsersMicroservicePolicies>().GetCombinedPolicy()
);

builder.Services.AddHttpClient<ProductMicroserviceClient>(client =>
{
    client.BaseAddress = new Uri($"http://{Environment.GetEnvironmentVariable("ProductsMicroserviceURI")}:{Environment.GetEnvironmentVariable("ProductsMicroservicePort")}");
}).AddPolicyHandler
(
    builder.Services.BuildServiceProvider().GetRequiredService<IProductsMicroservicePolicies>().GetFallbackPolicy()
).AddPolicyHandler
(
    builder.Services.BuildServiceProvider().GetRequiredService<IProductsMicroservicePolicies>().GetBulkheadIsolationPolicy()
);

var app = builder.Build();

app.UseExceptionHandlingMiddleware();

app.UseRouting();

app.UseCors();

app.UseSwagger();
app.UseSwaggerUI();

//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
