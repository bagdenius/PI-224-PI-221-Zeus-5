using AutoMapper;
using BLL.Models;
using DAL.Entities;
using UI.Services.Abstract;
using UnitOfWorkSpace.Abstract;

namespace UI.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ApplicationService(IUnitOfWork unitOfWork, IMapper mapper) =>
            (_unitOfWork, _mapper) = (unitOfWork, mapper);

        public async Task<IEnumerable<string>> GetAsync()
        {
            return from application in await _unitOfWork.Applications.GetAsync()
                   select $"{application.Id}," +
                          $"{application.ApplicantId}," +
                          $"{application.Name}," +
                          $"{application.CreationDate}," +
                          $"{application.IsApproved}," +
                          //$"{application.College}," +
                          $"{application.Vacancy.Title}," +
                          $"{application.Vacancy.CreationDate}," +
                          $"{application.Vacancy.EmployerId}";
        }

        public IEnumerable<ApplicationModel> Get()
        {
            return _mapper.Map<IEnumerable<ApplicationModel>>(_unitOfWork.Applications.Get());
        }

        public async Task<IEnumerable<ApplicationModel>> FindByUserId(string userId)
        {
            return _mapper.Map<IEnumerable<ApplicationModel>>(await _unitOfWork.Applications.FindByUserId(userId));
        }

        public async Task<ApplicationModel> Get(string id)
        {
            return _mapper.Map<ApplicationModel>(await _unitOfWork.Applications.Get(id));
        }

        public async Task Add(ApplicationModel application)
        {
            await _unitOfWork.Applications.Add(_mapper.Map<Application>(application));
            await _unitOfWork.SaveAsync();
        }

        public async Task Update(ApplicationModel application)
        {
            await _unitOfWork.Applications.Update(_mapper.Map<Application>(application));
            await _unitOfWork.SaveAsync();
        }

        public bool CheckApplication(string userId, string vacancyId)
        {
            return _unitOfWork.Applications.CheckApplication(userId, vacancyId);
        }
    }
}
