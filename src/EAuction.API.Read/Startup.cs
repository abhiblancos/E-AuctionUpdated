using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
//using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using EAuction.Service.ProductService;
using EAuction.Service.BuyerService;
using EAuction.Service.SellerService;
using EAuction.Service.BidsService;
using System.IO;
using System.Reflection;
using System;
using Microsoft.EntityFrameworkCore;
using EAuction.Service.MongoDb.Interface;
using Microsoft.Extensions.Options;
using EAuction.Service.MongoDb;

namespace EAuction.API.Read
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
            // services.AddRazorPages();

            //var sqlConnectionString = Configuration.GetConnectionString("DataAccessSqliteProvider");

            //services.AddDbContext<EAuction.DataAccessSqlite.Provider.DomainModelSqliteContext>(options =>
            //    options.UseSqlite(
            //        sqlConnectionString                   
            //    )
            //);
            //services.AddScoped<Service.IDataAccessProvider, EAuction.DataAccessSqlite.Provider.DataAccessSqliteProvider>();
            //services.AddScoped<BidService>();
            //services.AddScoped<ProductServ>();
            //services.AddScoped<BuyerServ>();
            //services.AddScoped<SellerService>();

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
                    Description = "An ASP.NET Core Web API for managing E-Action.\n Created by: Abhishek Deshmukh and Manish Sharma",
                    Version = "v1",

                });
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            services.Configure<MongoDbSettings>(Configuration.GetSection("MongoDbSettings"));

            services.AddSingleton<IMongoDbSettings>(serviceProvider =>
                serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value);

           services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));
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

            app.UseCors(builder => builder
           .AllowAnyHeader()
           .AllowAnyMethod()
           .SetIsOriginAllowed((host) => true)
           .AllowCredentials()
           );

            //app.UseHttpsRedirection();
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
