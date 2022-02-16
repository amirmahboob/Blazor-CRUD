using AutoMapper;
using Mc2.CrudTest.Presentation.Data;
using Mc2.CrudTest.Presentation.Server.AutoMapper;
using Mc2.CrudTest.Presentation.Server.Config.Extentions;
using Mc2.CrudTest.Presentation.Service.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StructureMap;
using System;

namespace Mc2.CrudTest.Presentation.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddOurSwaager();
            services.AddDbContext<PresentationContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            #region Auto Mapper Configurations

            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            #endregion
            services.AddControllersWithViews();
            services.AddRazorPages();
            return ConfigureIoC(services);
        }

        #region using StructureMap
        public IServiceProvider ConfigureIoC(IServiceCollection services)
        {
            Container container = new Container(config =>
            {
                config.Scan(s =>
                {
                    s.TheCallingAssembly();
                    s.WithDefaultConventions();
                    s.AddAllTypesOf<ICustomerService>();
                    s.LookForRegistries();
                    s.AssembliesAndExecutablesFromApplicationBaseDirectory();
                });

                var optionsBuilder = new DbContextOptionsBuilder<PresentationContext>();
                optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                config.For<IUnitOfWork>().Use(_ => new PresentationContext(optionsBuilder.Options)).ContainerScoped();

                config.Populate(services);
            });

            return container.GetInstance<IServiceProvider>();
        }
        #endregion

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var swaggerEndPoint = "/swagger/v1/swagger.json";
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
                swaggerEndPoint = $"/api/web{swaggerEndPoint}";
            //else
            //{
            //    app.UseExceptionHandler("/Error");
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseSwagger();
            app.UseSwaggerUI(s => s.SwaggerEndpoint(swaggerEndPoint, "PresentationAPI v1"));


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapRazorPages();
            //    endpoints.MapControllers();
            //    endpoints.MapFallbackToFile("index.html");
            //});
        }
    }
}
