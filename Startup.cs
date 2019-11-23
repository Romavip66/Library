using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryCourse.Data;
using LibraryCourse.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LibraryCourse
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<LibraryContext>(options =>
            {
                options.UseSqlite("Filename=library.db");

            });

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<LibraryContext>();
            
            services.AddMvc();
            services.AddControllers();

        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Books}/{action=Index}/{id?}");
            });
        }
    }
}
