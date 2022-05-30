﻿using Database;
using Repositories.Abstract;
using UnitOfWorkSpace.Abstract;

namespace UnitOfWorkSpace
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;
        public IApplicationRepository Applications { get; }
        public IVacancyRepository Vacancies { get; }

        public UnitOfWork(DatabaseContext context, 
            IApplicationRepository applicationRepository, 
            IVacancyRepository vacancyRepository) =>
            (_context, Applications, Vacancies) = 
            (context, applicationRepository, vacancyRepository);
        public async Task SaveAsync() => await _context.SaveChangesAsync();

        #region Disposing

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                    _context.Dispose();
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
