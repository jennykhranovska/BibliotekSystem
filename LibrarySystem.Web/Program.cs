

using LibrarySystem.Core.Interfaces;
using LibrarySystem.Data.Context;
using LibrarySystem.Data.Repositories;
using LibrarySystem.Web.Components;
using Microsoft.EntityFrameworkCore;



namespace LibrarySystem.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddDbContext<LibraryContext>(options =>
    options.UseSqlite("Data Source=library.db"));
            builder.Services.AddScoped<IBookRepository, BookRepository>();

            builder.Services.AddScoped<IMemberRepository, MemberRepository>();

            builder.Services.AddScoped<ILoanRepository, LoanRepository>();


            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseAntiforgery();

            app.MapStaticAssets();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
