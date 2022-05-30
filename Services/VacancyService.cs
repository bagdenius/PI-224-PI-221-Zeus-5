using AutoMapper;
using BLL.Models;
using DAL.Entities;
using UI.Services.Abstract;
using UnitOfWorkSpace.Abstract;

namespace UI.Services
{
    public class VacancyService : IVacancyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VacancyService(IUnitOfWork unitOfWork, IMapper mapper) =>
            (_unitOfWork, _mapper) = (unitOfWork, mapper);

        public async Task<IEnumerable<string>> GetStrings()
        {
            return from job in await _unitOfWork.Vacancies.Get()
                   select $"{job.Id}," +
                   $"{job.EmployerId}," +
                   $"{job.Title}," +
                   $"{job.Sector}," +
                   $"{job.CreationDate}," +
                   $"{job.Location},";
        }

        public async Task<IEnumerable<VacancyModel>> GetTopVacancies(int count)
        {
            return _mapper.Map<IEnumerable<VacancyModel>>(await _unitOfWork.Vacancies.GetTopVacancies(count));
        }

        public async Task<IEnumerable<VacancyModel>> GetByUserId(string userId)
        {
            return _mapper.Map<IEnumerable<VacancyModel>>(await _unitOfWork.Vacancies.GetByUserId(userId));
        }

        public async Task<IList<VacancyModel>> Search(string query)
        {
            return _mapper.Map<IList<VacancyModel>>(await _unitOfWork.Vacancies.Search(query));
        }

        public async Task<VacancyModel> Get(string id)
        {
            return _mapper.Map<VacancyModel>(await _unitOfWork.Vacancies.Get(id));
        }

        public async Task Add(VacancyModel vacancy)
        {
            await _unitOfWork.Vacancies.Add(_mapper.Map<Vacancy>(vacancy));
            await _unitOfWork.SaveAsync();
        }

        public async Task Update(VacancyModel vacancy)
        {
            var updateObj = await _unitOfWork.Vacancies.Get(vacancy.Id);
            updateObj.Id = vacancy.Id;
            updateObj.Title = vacancy.Title;
            updateObj.Picture=vacancy.Picture;
            updateObj.Description = vacancy.Description;
            updateObj.Location = vacancy.Location;
            updateObj.Sector = vacancy.Sector;
            updateObj.EmployerId = vacancy.EmployerId;
            updateObj.CreationDate = vacancy.CreationDate;
            updateObj.Applications = _mapper.Map<List<Application>>(vacancy.Applications);
            await _unitOfWork.SaveAsync();
        }

        public async Task Remove(string id)
        {
            await _unitOfWork.Vacancies.Remove(id);
            await _unitOfWork.SaveAsync();
        }
    }
}
