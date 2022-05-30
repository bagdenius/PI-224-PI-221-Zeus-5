using AutoMapper;
using BLL.Models;
using DAL.Entities;

namespace Mappers
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Application, ApplicationModel>().ReverseMap();
            CreateMap<Vacancy, VacancyModel>().ReverseMap();
        }
    }
}
