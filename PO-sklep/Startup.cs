using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PO_sklep.Helpers;
using PO_sklep.Repositories.Implementations;
using PO_sklep.Repositories.Interfaces;
using PO_sklep.Services.Implementations;
using PO_sklep.Services.Interfaces;

namespace PO_sklep
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
            services.AddCors(options =>
                options.AddPolicy("CorsPolicy",
                                  builder => builder.WithOrigins("http://localhost:4200")
                                                    .AllowAnyMethod()
                                                    .AllowAnyHeader()
                                                    .AllowCredentials()));

            SqlMapperInitializer.InitializeColumnMappings();

            services.AddAutoMapper(typeof(Startup));

            services.AddSingleton(new ConnectionConfig(Configuration.GetConnectionString("PO-sklep-ef")));

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
