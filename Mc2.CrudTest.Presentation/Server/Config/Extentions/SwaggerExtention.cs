using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace Mc2.CrudTest.Presentation.Server.Config.Extentions
{
    public static class SwaggerExtention
    {
        public static IServiceCollection AddOurSwaager(this IServiceCollection services)
        {
            // Swagger service properties
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "PresentationAPI",
                    Version = "v1",
                    Description = "PresentationAPI",
                    Contact = new OpenApiContact()
                    {
                        Name = "Amir Mahboob",
                        Email = "infomahboob81@gmail.com",
                    },
                });
            });
            return services;
        }
    }
}
