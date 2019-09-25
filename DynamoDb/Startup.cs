﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using DynamoDb_Library.DynamoDb;
using DynamoDb_Library.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DynamoDb
{
    public class Startup
    {
        public Startup()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
        }
        public IConfigurationRoot Configuration { get; set; }



        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddMvc();
            services.AddDefaultAWSOptions(Configuration.GetAWSOptions());
            services.AddAWSService<IAmazonDynamoDB>();

            //Default: AWS reads the credentials from Credtentials file located in localsystem "C://Users/(user)/.aws"
            //The following Environnment variable are set only if you want to read the credentials from Appsettings.json file
            Environment.SetEnvironmentVariable("AWS_ACCESS_KEY_ID", Configuration["AWS:AccessKey"]);
            Environment.SetEnvironmentVariable("AWS_SECRET_ACCESS_KEY", Configuration["AWS:SecretKey"]);
            Environment.SetEnvironmentVariable("AWS_REGION", Configuration["AWS:Region"]);

            services.AddSingleton<IDynamoDb, DynamoDbServices>();
            services.AddCors(options => options.AddPolicy("CORSPolicy", builder => 
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMvc();
            app.UseCors("CORSPolicy");

            app.UseHttpsRedirection();
            app.UseMvc(routes =>
            {
                routes
                    //.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}")
                    //.MapRoute(name: "swagger_root", defaults:null, constraints:null, template: new RedirectResult("/swagger").Url)
                  . MapRoute(name: "create", template: "{controller}/{action}")
                  .MapRoute(name: "default", template: "{controller}/{action}/{id?}");
            }); 

        }
    }
}
