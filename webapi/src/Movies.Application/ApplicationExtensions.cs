using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using Movies.Application.Mappings;
using Movies.Application.Validator;

namespace Movies.Application
{
    public static class ApplicationExtensions
    {
        /// <summary>
        ///  Adds Application dependency
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // DI for Mediator
            services.AddMediatR(cf => { 

                cf.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());

                // register validation behavior
                cf.AddOpenBehavior(typeof(ValidationBehavior<,>));


            });



            // DI for mapster

            MappingConfig.Configure();
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(Assembly.GetExecutingAssembly());
            services.AddSingleton(config);           

            // DI for validator
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
