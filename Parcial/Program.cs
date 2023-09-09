using data;

using Parcial;
using model;
using System.Configuration;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        var connection = new mySqlConfig(builder.Configuration.GetConnectionString("mysqlConnection"));
        builder.Services.AddSingleton(connection);
        string connectionString = builder.Configuration.GetConnectionString("mysql");
        builder.Services.AddScoped<IClientesRepository>(_ => new ClienteRepositorio(connectionString));

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
    }
}