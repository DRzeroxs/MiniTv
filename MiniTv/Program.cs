using Application.Services;
using Database;
using Microsoft.EntityFrameworkCore;
using MiniTv.Controllers;

namespace MiniTv
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //conexion con sql
            builder.Services.AddDbContext<Context>(opciones =>
                opciones.UseInMemoryDatabase(builder.Configuration.GetConnectionString("Default")));

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<Repository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
