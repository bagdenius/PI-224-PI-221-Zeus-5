using AutoMapper;
using BLL.Models;
using DAL.Entities;

namespace Mappers
{
    public class VacancyMapper : Profile
    {
        public VacancyMapper()
        {
            CreateMap<Vacancy, VacancyModel>().ReverseMap();
        }
    }
}
