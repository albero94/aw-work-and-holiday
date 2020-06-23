using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WorkAndHolidayScraper.Models;
using WorkAndHolidayScraper.Models.Scraper;
using JobsLibrary;

namespace WorkAndHolidayScraper
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
            services.AddControllers();
            services.AddLogging();
            //services.AddSingleton<IRepository, MockRepository>();
            services.AddScoped<IRepository, DatabaseRepository>();
            services.AddScoped<ScraperFactory>();
            AddScraperServices(services);

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("PostgresDatabase")));
        }

        private static void AddScraperServices(IServiceCollection services)
        {
            services.AddScoped<IndeedScraper>()
                    .AddScoped<Scraper, IndeedScraper>(s => s.GetService<IndeedScraper>());
            services.AddScoped<JoobleScraper>()
                    .AddScoped<Scraper, JoobleScraper>(s => s.GetService<JoobleScraper>());
            services.AddScoped<JoraScraper>()
                    .AddScoped<Scraper, JoraScraper>(s => s.GetService<JoraScraper>());
            services.AddScoped<SeekScraper>()
                    .AddScoped<Scraper, SeekScraper>(s => s.GetService<SeekScraper>());
            services.AddScoped<WorkingHolidayJobsScraper>()
                    .AddScoped<Scraper, WorkingHolidayJobsScraper>(s => s.GetService<WorkingHolidayJobsScraper>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
