using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using System.IdentityModel.Tokens.Jwt;

namespace BLTS.WebUi.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

            services.AddMicrosoftIdentityWebAppAuthentication(Configuration)
                .EnableTokenAcquisitionToCallDownstreamApi(new string[] { Configuration["AzureAdPermissions:ApiCmsScope"] })
                .AddInMemoryTokenCaches();

            services.AddHttpContextAccessor();

            services.AddControllersWithViews(options =>
            {
                //var policy = new AuthorizationPolicyBuilder()
                //    .RequireAuthenticatedUser()
                //    .Build();
                //options.Filters.Add(new AuthorizeFilter(policy));
            }).AddMicrosoftIdentityUI();

            services.AddRazorPages();
            services.AddServerSideBlazor();


            //services.AddDistributedMemoryCache();

            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.Unspecified;
            //    // Handling SameSite cookie according to https://docs.microsoft.com/en-us/aspnet/core/security/samesite?view=aspnetcore-3.1
            //    options.HandleSameSiteCookieCompatibility();
            //});

            //services.AddOptions();

            ////services.AddMicrosoftIdentityWebAppAuthentication(Configuration)
            ////    .EnableTokenAcquisitionToCallDownstreamApi(new string[] { Configuration["AzureAdPermissions:ApiCmsScope"]})
            ////    .AddInMemoryTokenCaches();

            //services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
            //    .AddMicrosoftIdentityWebApp(Configuration.GetSection("AzureAd"))
            //    .EnableTokenAcquisitionToCallDownstreamApi(new string[] { Configuration["AzureAdPermissions:ApiCmsScope"]/*, Configuration["AzureAdPermissions:ApiDataScope"]*/ })
            //    .AddInMemoryTokenCaches();

            ////services.AddControllersWithViews()
            ////    .AddMicrosoftIdentityUI();

            //services.AddControllersWithViews(options =>
            //{
            //    var policy = new AuthorizationPolicyBuilder()
            //        .RequireAuthenticatedUser()
            //        .Build();
            //    options.Filters.Add(new AuthorizeFilter(policy));
            //    //options.Filters.Add(new MsalUiRequiredExceptionFilter()); //this is the custom exception filter
            //}).AddMicrosoftIdentityUI();


            //services.AddAuthorization(options =>
            //{
            //    // By default, all incoming requests will be authorized according to the default policy
            //    options.FallbackPolicy = options.DefaultPolicy;
            //});

            services.AddRazorPages();
            services.AddServerSideBlazor();
                //.AddMicrosoftIdentityConsentHandler();


            Core.Startup applicationServicesStartup = new Core.Startup(services, Configuration);
            applicationServicesStartup.Initialize();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            //app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
