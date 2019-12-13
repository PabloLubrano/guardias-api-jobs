using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GuardiasAPI.Jobs.Models.Context;

namespace GuardiasAPI_Jobs
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
            //Obtenemos los datos correspondientes a la DB
            string server = Environment.GetEnvironmentVariable("GUARDIASAPI_Server");
            string database = Environment.GetEnvironmentVariable("GUARDIASAPI_Database");
            string userName = Environment.GetEnvironmentVariable("GUARDIASAPI_User");
            string password = Environment.GetEnvironmentVariable("GUARDIASAPI_Password");

            //Creamos el connectionString
            string connectionString = 
                String.Format("Server={0};Database={1};User Id={2};Password={3};", server, database, userName, password);

            //Asosciamos el connectionString al DbContext
            services.AddDbContext<JobDbContext>(options => options.UseSqlServer(connectionString));

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc();
        }
    }
}
