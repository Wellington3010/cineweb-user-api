using AutoMapper;
using cineweb_user_api.Context;
using cineweb_user_api.DTO;
using cineweb_user_api.Models;
using cineweb_user_api.Repositories;
using cineweb_user_api.Util;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace cineweb_user_api
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

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserLoginDTO, Usuario>().ReverseMap();
                cfg.CreateMap<UserRegisterDTO, Usuario>().ReverseMap();
            });

            IMapper mapper = configuration.CreateMapper();
            services.AddSingleton(mapper);

            services.AddDbContext<UserContext, UserContext>(builder =>
            {
                builder.UseMySQL(Configuration.GetConnectionString("DefaultConnection"),
                options =>
                {
                    options.CommandTimeout(680);
                    options.EnableRetryOnFailure(5);
                });
            });

            services.AddCors(setup => {
                    setup.AddPolicy("CorsPolicy", builder => {
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    builder.AllowAnyOrigin();
                });
            });

             services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.WriteIndented = false;
            });

            services.AddScoped<ICriptography, Criptography>();
            services.AddScoped<IBaseRepository<UserRegisterDTO>, UserRepository>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "cineweb_user_api", Version = "v1" });
            });

            services.AddHealthChecks();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "cineweb_user_api v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors("CorsPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}
