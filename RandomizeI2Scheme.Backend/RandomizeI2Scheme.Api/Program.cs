using System.Reflection;
using System.Text.Json.Serialization;
using I2Scheme.Application;
using I2Scheme.Application.Common.Mapping;
using I2Scheme.Persistece;
using I2Scheme.Persistece.DI.Interfaces;
using Microsoft.AspNetCore.Http.Features;
using RandPhoneNumbers;
using RegularExpressionsRandomizer;
using FullnameRandomizer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(I2SchemeDbContext).Assembly));
});
builder.Services.AddApplication();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddRandPhoneNumbers();
builder.Services.AddRegexRandomizer();
builder.Services.AddRandomizeName();

builder.Services.Configure<FormOptions>(x =>
{
    x.ValueLengthLimit = int.MaxValue;
    x.MultipartBodyLengthLimit = int.MaxValue;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseHttpsRedirection();
app.UseCors("AllowAll");

//app.UseAuthorization();
app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
app.MapControllers();

app.Run();