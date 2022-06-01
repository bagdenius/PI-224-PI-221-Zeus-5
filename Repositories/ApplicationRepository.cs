using Database;
using Microsoft.EntityFrameworkCore;
using Repositories.Abstract;
using DAL.Entities;

namespace Repositories
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly DatabaseContext _context;
        public ApplicationRepository(DatabaseContext context) =>
            _context = context;

        public async Task<IEnumerable<Application>> GetAsync() =>
            await _context.Applications.AsNoTracking().Include(p => p.Vacancy).ToListAsync();

        public IEnumerable<Application> Get() =>
             _context.Applications.AsNoTracking().Include(p => p.Vacancy);

        public async Task<IEnumerable<Application>> FindByUserId(string userid) =>
            from application in await GetAsync()
            where application.ApplicantId == userid
            select application;

        public async Task<Application> GetAsync(string id) =>
            await _context.Applications/*.AsNoTracking()*/.FirstOrDefaultAsync(v => v.Id == id);

        public Application Get(string id) =>
            _context.Applications.AsNoTracking().FirstOrDefault(a => a.Id == id);

        public async Task Add(Application jobApplication) =>
            await _context.Applications.AddAsync(jobApplication);

        public async Task Update(Application jobApplication) =>
            _context.Applications.Update(jobApplication);

        public bool CheckApplication(string userid, string jobid)
        {
            var jobApplications = from application in Get()
                                  where application.Vacancy.Id == jobid
                                  && application.ApplicantId == userid
                                  select application;
            return jobApplications.Any();
        }
    }
}
