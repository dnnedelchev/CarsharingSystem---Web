
using AutoMapper;
namespace CarsharingSystem.Web.Infrastructure.Mapping
{
    public interface IHaveCustomMappings
    {
        void CreateMappings(IConfiguration configuration);
    }
}