using System.Collections.Generic;
using AutoMapper;

namespace Allura.Challenge.Domain.Interfaces
{
    public interface IAbstractMapper
    {
        IMapper Mapper { get; }
        IReadOnlyList<Profile> Profiles { get; }
        void AddProfile<TProfile>() where TProfile : Profile, new();
    }
}