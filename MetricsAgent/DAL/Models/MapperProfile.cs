using AutoMapper;
using MetricsAgent.DAL.Models;
using MetricsAgent.Responses;

namespace MetricsAgent.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<BaseMetricModel, MetricDto>();
        }
    }
}