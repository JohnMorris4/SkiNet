using API.Extensions;
using API.Helpers;
using API.Middleware;
using AutoMapper;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace API
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
            services.AddAutoMapper(typeof(MappingProfiles));
            
            //This is the Base Connection String 
            services.AddDbContext<StoreContext>(x => 
                x.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            
            //Identity DbContext Connection String
            services.AddDbContext<AppIdentityDbContext>(x =>
                x.UseSqlite(Configuration.GetConnectionString("IdentityConnection")));
            
            //Redis ONLY Connection String
            services.AddSingleton<IConnectionMultiplexer>(c =>
            {
                var config = ConfigurationOptions
                    .Parse(Configuration.GetConnectionString("Redis"), true);
                return ConnectionMultiplexer.Connect(config);
            });
            services.AddApplicationServices();
            services.AddIdentityServices(Configuration);
            services.AddSwaggerDocumentation();
            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
                });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseStaticFiles();
            app.UseCors("CorsPolicy");

            //Make sure this is above the Authorization or you will get an error
            app.UseAuthentication();
            // CORS has to go above the UseAuthorization()     
            app.UseAuthorization();
            app.UseSwaggerDocumention();
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
