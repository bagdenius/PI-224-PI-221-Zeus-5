using Microsoft.Extensions.DependencyInjection;
using Repositories;
using Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using UnitOfWorkSpace.Abstract;
using Database;

namespace UnitOfWorkSpace
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            services.AddTransient<IApplicationRepository, ApplicationRepository>();
            services.AddTransient<IVacancyRepository, VacancyRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Resumes&Vacancies"));

            return services;
        }
    }
}
