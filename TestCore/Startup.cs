using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestDAL.Persistence;
using Microsoft.EntityFrameworkCore;
using TestDAL.Core;
using TestDAL.Persistence.Contexts;
using TestAPI.Helpers;

namespace TestAPI
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
            services.AddDbContext<TestContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            var autoMapper = new AutoMapper.MapperConfiguration(cfg => cfg.AddProfile(new AutoMapperProfileConfiguration()));
            var mapper = autoMapper.CreateMapper();
            services.AddSingleton(mapper);

            // Add framework services.
            services
                .AddMvc()
                .AddJsonOptions(o => o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddCors();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
