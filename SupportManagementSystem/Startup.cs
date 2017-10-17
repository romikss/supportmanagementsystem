using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SupportManagementSystem.Models;

namespace SupportManagementSystem
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSingleton<IEngineerRepository, InMemoryEngineerRepository>(); // Singleton: I need to keep the data in memory between requests
            services.AddSingleton<ISupportSlotRepository, InMemorySupportSlotRepository>(); // Singleton: I need to keep the data in memory between requests
            services.AddTransient<ISupportCycleFactory>(sp => 
            {
                var engeneerRepository = sp.GetRequiredService<IEngineerRepository>();
                var supportSlotRepository = sp.GetRequiredService<ISupportSlotRepository>();
                return new SlidingSupportCycleFactory(int.Parse(Configuration.GetSection("SupportCycle:periodInDays").Value), int.Parse(Configuration.GetSection("SupportCycle:slotsInADay").Value), supportSlotRepository, engeneerRepository);
            });
            
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
