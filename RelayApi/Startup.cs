using Google.Cloud.Diagnostics.AspNetCore;
using Google.Cloud.Diagnostics.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ObservabilitySampleApp.RelayApi
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
            // Stackdriver configuration start
            services.AddOptions();
            services.Configure<StackdriverOptions>(
                Configuration.GetSection("Stackdriver"));
            services.AddGoogleExceptionLogging(options =>
            {
                options.ProjectId = Configuration["Stackdriver:ProjectId"];
                options.ServiceName = Configuration["Stackdriver:ServiceName"];
                options.Version = Configuration["Stackdriver:Version"];
            });

            // Add trace service.
            services.AddGoogleTrace(options =>
            {
                options.ProjectId = Configuration["Stackdriver:ProjectId"];
                options.Options = TraceOptions.Create(
                    bufferOptions: BufferOptions.NoBuffer());
            });
            // Stackdriver configuration end
            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // Stackdriver configuration start
            // Configure logging service.
            loggerFactory.AddGoogle(Configuration["Stackdriver:ProjectId"]);
            var logger = loggerFactory.CreateLogger("testStackdriverLogging");
            // Write the log entry.
            logger.LogInformation("Stackdriver sample started. This is a log message.");
            // Configure error reporting service.
            app.UseGoogleExceptionLogging();
            // Configure trace service.
            app.UseGoogleTrace();
            // Stackdriver configuration end
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }

    public class StackdriverOptions
    {
        public string ProjectId { get; set; }
        public string ServiceName { get; set; }
        public string Version { get; set; }
    }
}