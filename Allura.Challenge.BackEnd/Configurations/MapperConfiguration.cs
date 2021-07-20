using Allura.Challenge.Domain.Abstracts;

namespace Allura.Challenge.BackEnd.Configurations
{
    public class MapperConfiguration : AbstractMapper<MapperConfiguration>
    {
        public MapperConfiguration()
        {
            AddProfile<DataMappings>();
        }
    }
}