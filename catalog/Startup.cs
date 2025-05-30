using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using catalog.Data;
using Microsoft.EntityFrameworkCore;

namespace catalog
{
    public class Startup
    {
        // CHANGE THIS TO false WHEN MIGRATING TO AZURE SQL
        private bool useInMemory = true;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();

            if (useInMemory)
            {
                services.AddDbContext<BookContext>(options => options.UseInMemoryDatabase(databaseName: "test"));                
            }
            else
            {
                services.AddDbContext<BookContext>(options => options.UseSqlServer(Configuration.GetConnectionString("BooksDB")));
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();           

            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}