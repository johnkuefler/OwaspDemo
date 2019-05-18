using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OwaspDemo.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OwaspDemo.Filters;
using Swashbuckle.AspNetCore.Swagger;

namespace OwaspDemo
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>()
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromDays(5);
            });

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            });

            //services.ConfigureApplicationCookie(options =>
            //{
            //    options.Cookie.HttpOnly = true;
            //    options.Cookie.Expiration = TimeSpan.FromMinutes(20);
            //    options.SlidingExpiration = true;
            //});

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Demo API", Version = "v1" });
            });

            services.AddMvc(options =>
            {
                // options.Filters.Add(typeof(AuditFilter));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApplicationDbContext context)
        {
            if (env.IsProduction())
            {
                context.Database.Migrate();
            }

            app.UseWhen(ctx => !ctx.Request.Path.Value.Contains("/securitymisconfigbefore"), appBuilder =>
            {
                appBuilder.UseExceptionHandler("/Home/Error");
            });
            app.UseWhen(ctx => ctx.Request.Path.Value.Contains("/securitymisconfigbefore"), appBuilder =>
            {
                appBuilder.UseDeveloperExceptionPage();
                appBuilder.UseDatabaseErrorPage();
            });

            //// X-Content-Type-Options header
            //app.UseHsts(hsts => hsts.MaxAge(365).IncludeSubdomains());
            //app.UseXContentTypeOptions();
            //// Referrer-Policy header.
            //app.UseReferrerPolicy(opts => opts.NoReferrer());
            //// X-Xss-Protection header
            //app.UseXXssProtection(options => options.EnabledWithBlockMode());
            //// X-Frame-Options header
            //app.UseXfo(options => options.Deny());
            //// Content-Security-Policy header
            //app.UseCsp(opts => opts
            //    .BlockAllMixedContent()
            //    .StyleSources(s => s.Self())
            //    .StyleSources(s => s.UnsafeInline())
            //    .FontSources(s => s.Self())
            //    .FormActions(s => s.Self())
            //    .FrameAncestors(s => s.Self())
            //    .ImageSources(s => s.Self())
            //    .ScriptSources(s => s.Self())
            //);

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();

            app.UseAuthentication();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/v1/swagger.json", "Demo API");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
