using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace mssa_11._1
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Setup DI and EF Core
            var services = new ServiceCollection();
            services.AddDbContext<BooksContext>(options =>
                options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=BooksInventoryDb;Trusted_Connection=True;"));

            var serviceProvider = services.BuildServiceProvider();
            var dbContext = serviceProvider.GetRequiredService<BooksContext>();
            dbContext.Database.EnsureCreated(); // Ensure DB is created

            // Store service provider for use in Form1
            AppDomain.CurrentDomain.SetData("ServiceProvider", serviceProvider);

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}