using Alura.Challenge.Domain.Abstracts;

namespace Alura.Challenge.BackEnd.Api.Configurations
{
    public class MapperConfiguration : AbstractMapper<MapperConfiguration>
    {
        public MapperConfiguration()
        {
            AddProfile<DataMappings>();
        }
    }
}