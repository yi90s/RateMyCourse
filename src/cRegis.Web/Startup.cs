using cRegis.Core.Data;
using cRegis.Core.Identities;
using cRegis.Core.Interfaces;
using cRegis.Core.Services;
using cRegis.Web.Services;
using cRegis.Web.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace cRegis.Web
{
    public class 
        Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<DataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Local")));
            services.AddControllersWithViews();
            services.AddIdentity<StudentUser, IdentityRole>()
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<DataContext>();
            services.ConfigureApplicationCookie(options =>
                {
                    options.AccessDeniedPath = "/Auth/AccessDenied";
                    options.LoginPath = "/Auth/Index";
                }
            );

            //injecting custom Core services
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IEnrollService, EnrollService>();
            services.AddScoped<IFacultyService, FacultyService>();
            services.AddScoped<IWishlistService, WishlistService>();

            //injecting custom Web services
            services.AddScoped<IViewModelService, ViewModelService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Auth}/{action=index}/{id?}");
            });
        }
    }
}
