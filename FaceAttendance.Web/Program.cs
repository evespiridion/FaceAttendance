using FaceAttendance.Application.Interfaces;
using FaceAttendance.Application.Services;
using FaceAttendance.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FaceAttendance.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //Register DbContext
            builder.Services.AddMemoryCache();
            builder.Services.AddDbContext<ApplicationDbContext>(option =>
                option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Register the Face Embeddings service
            builder.Services.AddScoped<IFaceEmbeddingsService, FaceEmbeddingsService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            //app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                ServeUnknownFileTypes = true, // Allows serving files without extensions
                DefaultContentType = "application/octet-stream" // Default content type if none is specified
            });

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
