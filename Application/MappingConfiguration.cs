using Application.Point.AddPoints;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class MappingConfiguration : Profile
    {
        public MappingConfiguration()
        {
            CreateMap<AddPointsCommand.Command, Domain.Model.Transaction>();
        }
    }
}
