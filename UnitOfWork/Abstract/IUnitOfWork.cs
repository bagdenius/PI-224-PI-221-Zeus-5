using Repositories.Abstract;

namespace UnitOfWorkSpace.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IApplicationRepository Applications { get; }
        IVacancyRepository Vacancies { get; }
        Task SaveAsync();
    }
}
