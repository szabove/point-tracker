using Application.Point.AddPoints;
using AutoMapper;
using Domain.Model;

namespace Application
{
    public class MappingConfiguration : Profile
    {
        public MappingConfiguration()
        {
            CreateMap<AddPointsCommand.Command, Transaction>();
        }
    }
}
