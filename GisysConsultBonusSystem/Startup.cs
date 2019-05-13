using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using GisysConsultBonusSystem.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;

namespace GisysConsultBonusSystem
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IServiceCollection _services { get; private set; }
        public string MyAllowSpecificOrigins { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("DevelopmentCorsPolicy", builder =>
                {
                    builder.WithOrigins("http://localhost:4200")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            services.AddMvc(options => options.EnableEndpointRouting = true).SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
            .AddJsonOptions(options =>
            {
                var resolver = options.SerializerSettings.ContractResolver;
                if (resolver != null)
                    (resolver as DefaultContractResolver).NamingStrategy = null;
            });

            services.AddDbContext<ConsultContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DevConnection")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            var defaultDateCulture = "sv-SE";
            var ci = new CultureInfo(defaultDateCulture);
            ci.NumberFormat.NumberDecimalSeparator = "-";
            ci.NumberFormat.CurrencyDecimalSeparator = ".";

            // Configure the Localization middleware
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(ci),
                SupportedCultures = new List<CultureInfo>
            {
                ci,
            },
                        SupportedUICultures = new List<CultureInfo>
            {
                ci,
            }
            });
            app.UseCors("DevelopmentCorsPolicy");

            DefaultFilesOptions options = new DefaultFilesOptions();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "Bonuses",
                    template: "Bonuses",
                    defaults: new { controller = "Bonuses", action = "Index" });

                routes.MapRoute(
                    name: "Consult",
                    template: "two/{id?}",
                    defaults: new { controller = "Consult", action = "Index" });

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }


    }
}
