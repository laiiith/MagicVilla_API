using Microsoft.EntityFrameworkCore;
using Serilog;
using MagicVilla_DataAccess.API_DataAccess.Repository;
using MagicVilla_DataAccess.API_DataAccess.Repository.IRepository;
using MagicVilla_Utility.API_Utility.Logging;
using MagicVilla_DataAccess;
using MagicVilla_Utility.API_Utility;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDbContext>(options=>options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// Add services to the container.
Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.File("log/villaLogs.txt" , rollingInterval:RollingInterval.Day).CreateLogger();
builder.Services.AddAutoMapper(typeof(MappingConfig));
builder.Host.UseSerilog();
builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IVillaRepository , VillaRepository>();
builder.Services.AddScoped<IVillaNumberRepository , VillaNumberRepository>();
builder.Services.AddSingleton<ILogging, Logging>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
