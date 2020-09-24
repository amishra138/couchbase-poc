using Couchbase;
using Couchbase.Authentication;
using Couchbase.Configuration.Client;
using CouchBaseStandardLib;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;

namespace SampleCouchbaseAPI
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
            //couchbase 
            ClusterHelper.Initialize(new ClientConfiguration()
            {
                Servers = new List<Uri>()
                {
                    new Uri("http://localhost:8091")
                }
            }, new PasswordAuthenticator("Administrator", "123456"));

            services.AddTransient<ITravelBucketRepository, TravelBucketRepository>(x =>
            {
                return new TravelBucketRepository("travel-sample");
            });

            //swagger
            services.AddSwaggerGen();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime hostApplicationLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //use swagger
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CouchBase CRUD Demo API");
                c.RoutePrefix = string.Empty;
            });

            //close couchbase cluster connection
            hostApplicationLifetime.ApplicationStopped.Register(() =>
            {
                ClusterHelper.Close();
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
