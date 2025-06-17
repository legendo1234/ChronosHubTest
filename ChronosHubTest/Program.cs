using System.Data;                                   
using Microsoft.Data.SqlClient;                      
using ChronosHubTest.Data;                           
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ChronosHubTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            // register LocalDB connection
            builder.Services.AddTransient<IDbConnection>(sp =>
                new SqlConnection(
                    builder.Configuration.GetConnectionString("DefaultConnection")
                )
            );

            // register your Dapper‐backed repo
            builder.Services.AddScoped<IAcademicRepository, DapperAcademicRepository>();

            // Swagger/OpenAPI
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // (Optional) configure HTTPS port explicitly
            builder.Services.AddHttpsRedirection(opts =>
            {
                opts.HttpsPort = 5001;
            });

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
}
