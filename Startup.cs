using eTicket.Data;
using eTicket.Data.Cart;
using eTicket.Data.Services;
using eTicket.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTicket
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            //Configuring page reload
            services.AddRazorPages().AddRazorRuntimeCompilation();

            // Adding the Dbcontext from the entity framework core to the startup (all part of configuration purposes).
            // Adding the sql connection string to the startup configuration.
            // This line also connects to the database.
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
               Configuration.GetConnectionString("DefaultConnection")
                ));


/*            services.AddScoped<Actor_Movie>()*/

            //Service Configuration
            services.AddScoped<IActionService, ActorsService>();
            services.AddScoped<IProducerService, ProducerService>();
            services.AddScoped<ICinemaService, CinemaService>();
            services.AddScoped<IMoviesService, MoviesService>();
            services.AddScoped<IOrderService, OrdersService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(sc => ShoppingCart.GetShoppingCart(sc));

            //Authorization and Authentication
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddMemoryCache();
            services.AddSession();
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();
           

            //Authentication and Authorization
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            ApplicationDbInitialiser.Seed(app);
            ApplicationDbInitialiser.SeedUsersAndRolesAsync(app).Wait();
        }
    }
}
