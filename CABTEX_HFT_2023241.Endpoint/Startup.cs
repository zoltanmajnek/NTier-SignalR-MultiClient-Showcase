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
using CABTEX_HFT_2023241.Endpoint.Services;

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
            services.AddSingleton<MyDisksDbContext>();

            services.AddSingleton<IRepository<MyDisk>, MyDiskRepository>();
            services.AddSingleton<IRepository<DiskType>, DiskTypeRepository>();
            services.AddSingleton<IRepository<Software>, SoftwareRepository>();
            services.AddSingleton<IRepository<Friend>, FriendRepository>();
            services.AddSingleton<IRepository<DiskDirectory>, DiskDirectoryRepository>();

            services.AddSingleton<IMyDiskLogic, MyDiskLogic>();
            services.AddSingleton<IDiskTypeLogic, DiskTypeLogic>();
            services.AddSingleton<ISoftwareLogic, SoftwareLogic>();
            services.AddSingleton<IFriendLogic, FriendLogic>();
            services.AddSingleton<IDiskDirectoryLogic, DiskDirectoryLogic>();

            services.AddSignalR();
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
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CABTEX_HFT_2023231.Endpoint v1"));
            }

            app.UseExceptionHandler(c => c.Run(async context =>
            {
                var exception = context.Features
                    .Get<IExceptionHandlerPathFeature>()
                    .Error;
                var response = new { Msg = exception.Message };
                await context.Response.WriteAsJsonAsync(response);
            }));

            app.UseCors(x => x
                .AllowCredentials()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithOrigins("http://localhost:44860"));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<SignalRHub>("/hub");
            });
        }
    }
}
