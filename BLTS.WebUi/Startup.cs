using BLTS.WebUi.Configuration;
using BLTS.WebUi.DataAccessLayer;
using BLTS.WebUi.Database;
using BLTS.WebUi.Logs;
using BLTS.WebUi.Models;
using BLTS.WebUi.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BLTS.WebUi
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
            services.AddRazorPages();

            services.AddDbContext<DefaultDbContext>(options => options.UseSqlServer(
                                                               Configuration.GetConnectionString("Default"),
                                                               b => b.MigrationsAssembly(typeof(DefaultDbContext).Assembly.FullName)));

            #region Application Services
            services.AddSingleton<AutoConfiguration>();
            services.AddTransient<ApplicationLogTools>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ReflectionTools>();
            services.AddTransient<StringUtilities>();
            #endregion

            #region Repositories
            services.AddTransient<IRepository<ApplicationLog, long>, Repository<ApplicationLog, long>>();
            services.AddTransient<IRepository<OperationalConfiguration, long>, Repository<OperationalConfiguration, long>>();
            #endregion 
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
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithRedirects("/Error?code={0}");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute("defaultWithArea", "{area}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
