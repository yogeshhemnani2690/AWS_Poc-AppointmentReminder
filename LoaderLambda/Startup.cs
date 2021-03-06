using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentReminder.Services;
using DynamoDb_Library.DynamoDb;
using DynamoDb_Library.Interfaces;
using LoaderLambda.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LoaderLambda
{
    public class Startup
    {
        public const string AppS3BucketKey = "AppS3Bucket";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Add S3 to the ASP.NET Core dependency injection framework.
            services.AddAWSService<Amazon.S3.IAmazonS3>();
            services.AddSingleton<ILoaderLambda, LamdaServices >();
            services.AddSingleton<IDynamoDb, DynamoDbServices>();

            services.AddCors(options => options.AddPolicy("CORSPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseCors("CORSPolicy");
            app.UseMvc(routes =>
            {
                routes
                  //.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}")
                  //.MapRoute(name: "swagger_root", defaults:null, constraints:null, template: new RedirectResult("/swagger").Url)
                  .MapRoute(name: "create", template: "{controller}/{action}")
                  .MapRoute(name: "default", template: "{controller}/{action}/{id?}");
            });
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
