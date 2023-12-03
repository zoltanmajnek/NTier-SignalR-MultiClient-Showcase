using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using CABTEX_HFT_2023241.Logic;
using CABTEX_HFT_2023241.Models;
using CABTEX_HFT_2023241.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Diagnostics;

namespace CABTEX_HFT_2023241.Endpoint
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<MyDisksDbContext>();

            services.AddTransient<IRepository<MyDisk>, MyDiskRepository>();
            services.AddTransient<IRepository<DiskType>, DiskTypeRepository>();
            services.AddTransient<IRepository<Software>, SoftwareRepository>();
            services.AddTransient<IRepository<Friend>, FriendRepository>();
            services.AddTransient<IRepository<DiskDirectory>, DiskDirectoryRepository>();

            services.AddTransient<IMyDiskLogic, MyDiskLogic>();
            services.AddTransient<IDiskTypeLogic, DiskTypeLogic>();
            services.AddTransient<ISoftwareLogic, SoftwareLogic>();
            services.AddTransient<IFriendLogic, FriendLogic>();
            services.AddTransient<IDiskDirectoryLogic, DiskDirectoryLogic>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CABTEX_HFT_2023231.Endpoint", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MovieDbApp.Endpoint v1"));
            }

            app.UseExceptionHandler(c => c.Run(async context =>
            {
                var exception = context.Features
                    .Get<IExceptionHandlerPathFeature>()
                    .Error;
                var response = new { Msg = exception.Message };
                await context.Response.WriteAsJsonAsync(response);
            }));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
