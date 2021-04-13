using System.Security.Cryptography.X509Certificates;
using AspCoreRenderSample.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DataProtectionOptions = AspCoreRenderSample.Options.DataProtectionOptions;

namespace AspCoreRenderSample
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
            services.AddOptions<DatabaseOptions>()
                .Bind(Configuration.GetSection("Database"));
            services.AddOptions<DataProtectionOptions>()
                .Bind(Configuration.GetSection("DataProtection"));

            ConfigureDataProtection(services);

            services.AddControllersWithViews();
        }

        private void ConfigureDataProtection(IServiceCollection services)
        {
            // Obviously, when using this on production you should use a database that is actually persisted instead of
            // InMemoryDatabase
            services.AddDbContext<KeysDbContext>(options =>
                options.UseInMemoryDatabase("testDatabase"));

            var certPem = Configuration["DataProtection:Certificate"];
            var keyPem = Configuration["DataProtection:PrivateKey"];

            var cert = X509Certificate2.CreateFromPem(certPem, keyPem);
            services.AddDataProtection()
                .PersistKeysToDbContext<KeysDbContext>()
                .ProtectKeysWithCertificate(cert);
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
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
