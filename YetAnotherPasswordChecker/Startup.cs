using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using YetAnotherPasswordChecker.BLL;
using YetAnotherPasswordChecker.DAL;
using YetAnotherPasswordChecker.Infrastructure.Authentication;
using YetAnotherPasswordChecker.Services;
using UserManagementService = YetAnotherPasswordChecker.BLL.UserManagementService;


namespace YetAnotherPasswordChecker
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

            services.AddMemoryCache();
            
            services.AddScoped<IPasswordRuleService, PasswordRuleService>();
            services.AddScoped<IPasswordRuleManager, PasswordRuleManager>();
            services.AddScoped<IPasswordCheckerService, PasswordCheckerService>();
            services.AddScoped<PasswordContext>();
            services.AddScoped<IPwnedChecker, PwnedChecker>();
            services.AddScoped<IHaveIBeenPwnedService, HaveIBeenPwnedService>();
            services.AddScoped<IPwdCheckService, PwdCheckService>();
            services.AddScoped<BLL.IUserManagementService, BLL.UserManagementService>();
            services.AddScoped<DAL.IUserManagementService, DAL.UserManagementService>();

            services.AddAuthentication("BasicAuthentication").AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
