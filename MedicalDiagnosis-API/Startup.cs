using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;

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
        // Add services to the container.
        services.AddControllersWithViews();

        // Add DbContext using SQL Server Provider
        services.AddDbContext<MedicalDiagnosisContext>(options =>
            options.UseMySql(Configuration.GetConnectionString("DefaultConnection"),
            ServerVersion.AutoDetect(Configuration.GetConnectionString("DefaultConnection"))));

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Configure the HTTP request pipeline.
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
        }

        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}
