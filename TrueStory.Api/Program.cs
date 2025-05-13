using FluentValidation.AspNetCore;
using TrueStory.Api.Middlewares;
using TrueStory.Application.Helpers;
using TrueStory.Application.Services;
using TrueStory.Domain.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddFluentValidation(fv =>
    {
        fv.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>();
    }); ;
builder.Services.Configure<ProductServiceOptions>(
    builder.Configuration.GetSection("ProductService"));
builder.Services.AddHttpClient<ProductApiService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseGlobalExceptionMiddleware();
app.UseAuthorization();

app.MapControllers();

app.Run();
