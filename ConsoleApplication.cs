// P03_StudentSystem.ConsoleApp

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StudentSystem.Data.Models;
using StudentSystem.Data.Seeding;
using System;

namespace StudentSystem.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<StudentSystemContext>();
                    context.Database.Migrate();

                    DatabaseSeeder.Seed(services);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred while seeding the database.");
                    Console.WriteLine(ex.Message);
                }
            }

            // Your console application logic here
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddDbContext<StudentSystemContext>(options =>
                        options.UseSqlServer(
                            hostContext.Configuration.GetConnectionString("DefaultConnection")));

                    // Other services...

                });
    }
}
