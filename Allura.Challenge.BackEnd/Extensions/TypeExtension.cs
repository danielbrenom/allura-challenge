using System;
using System.Reflection;
using Allura.Challenge.BackEnd.Configurations;
using AutoMapper;

namespace Allura.Challenge.BackEnd.Extensions
{
    public static class TypeExtension
    {
        public static T GetAs<T>(this object obj, bool useMapper = true,
            T defaultObject = default, bool allowExceptions = false)
        {
            try
            {
                if (obj is null)
                    return defaultObject;
                if (obj.GetType() == typeof(T))
                    return (T) obj;
                if (!useMapper) return (T) obj;
                var mapper = ApplicationConfigurator.Instance.GetService<IMapper>();
                return mapper.Map<T>(obj);
            }
            catch (Exception)
            {
                if (allowExceptions)
                    throw;
                if (typeof(T) == obj?.GetType() || typeof(T) == obj?.GetType().GetTypeInfo().BaseType)
                    return (T) obj;
                return defaultObject;
            }
        }
    }
}