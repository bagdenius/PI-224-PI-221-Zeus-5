using AutoMapper;
using DAL;
using Models;

namespace Mappers
{
    public class ResumeMapper : Profile
    {
        public ResumeMapper()
        {
            CreateMap<Resume, ResumeModel>().ReverseMap();
        }
    }
}
