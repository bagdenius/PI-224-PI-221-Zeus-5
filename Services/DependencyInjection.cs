using UI.Services;
using UI.Services.Abstract;
using Mappers;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<ApplicationService, ApplicationService>();
            services.AddTransient<VacancyService, VacancyService>();
            services.AddAutoMapper(typeof(UserMapper).GetTypeInfo().Assembly);
            return services;
        }
    }
}
