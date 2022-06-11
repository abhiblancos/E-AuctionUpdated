using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using EAuction.Service.ProductService;
using EAuction.Service.BuyerService;
using EAuction.Service.SellerService;
using EAuction.Service.BidsService;
using System.IO;
using System.Reflection;
using System;

namespace EAuction.API.Write
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Use a SQLite database
            var sqlConnectionString = Configuration.GetConnectionString("DataAccessSqliteProvider");

            services.AddDbContext<EAuction.DataAccessSqlite.Provider.DomainModelSqliteContext>(options =>
                options.UseSqlServer(
                    sqlConnectionString,
                    b => b.MigrationsAssembly("EAuction.API.Write")
                )
            );

            services.AddScoped<EAuction.Service.IDataAccessProvider, EAuction.DataAccessSqlite.Provider.DataAccessSqliteProvider>();

            //Use a MS SQL Server database
            //var sqlConnectionString = Configuration.GetConnectionString("DataAccessMsSqlServerProvider");

            //services.AddDbContext<DomainModelMsSqlServerContext>(options =>
            //    options.UseSqlServer(
            //        sqlConnectionString,
            //        b => b.MigrationsAssembly("AspNetCoreMultipleProject")
            //    )
            //);

            //services.AddScoped<IDataAccessProvider, DataAccessMsSqlServerProvider.DataAccessMsSqlServerProvider>();

            //Use a PostgreSQL database
            //var sqlConnectionString = Configuration.GetConnectionString("DataAccessPostgreSqlProvider");

            //services.AddDbContext<DomainModelPostgreSqlContext>(options =>
            //    options.UseNpgsql(
            //        sqlConnectionString,
            //        b => b.MigrationsAssembly("AspNetCoreMultipleProject")
            //    )
            //);

            //services.AddScoped<IDataAccessProvider, DataAccessPostgreSqlProvider.DataAccessPostgreSqlProvider>();

            //Use a MySQL database
            //var sqlConnectionString = Configuration.GetConnectionString("DataAccessMySqlProvider");

            //services.AddDbContext<DomainModelMySqlContext>(options =>
            //    options.UseMySql(
            //        sqlConnectionString,
            //        b => b.MigrationsAssembly("AspNetCoreMultipleProject")
            //    )
            //);

            //services.AddScoped<IDataAccessProvider, DataAccessMySqlProvider.DataAccessMySqlProvider>();

            services.AddTransient<BidService>();
            services.AddTransient<ProductServ>();
            services.AddTransient<BuyerServ>();
            services.AddTransient<SellerService>();

            services.AddControllers()
              .AddNewtonsoftJson(options =>
              {
                  options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
              });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "E-Auction",
                    Description = "An ASP.NET Core Web API for managing E-Action.\n Created by: Abhishek J Deshmukh and Manish Sharma",
                    Version = "v1",                    
                    
                });
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            //app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseDeveloperExceptionPage();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
            });
        }
    }
}
