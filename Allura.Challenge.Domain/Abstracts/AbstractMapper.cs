using System.Collections.Generic;
using Allura.Challenge.Domain.Interfaces;
using AutoMapper;

namespace Allura.Challenge.Domain.Abstracts
{
    public abstract class AbstractMapper<T> : IAbstractMapper where T : IAbstractMapper, new()
    {
        public static IAbstractMapper Instance { get; set; }
        public IMapper Mapper { get; private set; }
        private readonly List<Profile> _profileList = new List<Profile>();
        public IReadOnlyList<Profile> Profiles => _profileList.AsReadOnly();

        public static void Initialize()
        {
            if (Instance != null)
                return;
            Instance = new T();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.ShouldMapProperty = p => p.GetGetMethod(true).IsPrivate || p.GetGetMethod(true).IsHideBySig;
                foreach (var p in Instance.Profiles)
                {
                    cfg.AddProfile(p);
                }
            });

            ((AbstractMapper<T>) Instance).Mapper = config.CreateMapper();
        }

        public void AddProfile<TProfile>() where TProfile : Profile, new()
        {
            _profileList.Add(new TProfile());
        }
    }
}