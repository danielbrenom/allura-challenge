using Allura.Challenge.Domain.Models.Data;
using Allura.Challenge.Domain.Models.Requests;
using Allura.Challenge.Domain.Models.Responses;
using AutoMapper;

namespace Allura.Challenge.BackEnd.Configurations
{
    public class DataMappings : Profile
    {
        public DataMappings()
        {
            CreateMap<MovieRequest, Movie>().ForMember(m => m.Category, ctor => ctor.Ignore())
                                            .ReverseMap();
            CreateMap<Movie, Database.Models.Movie>().ReverseMap();
            CreateMap<Movie, MovieResponse>().ReverseMap();
            CreateMap<CategoryRequest, Category>().ReverseMap();
            CreateMap<Category, Database.Models.Category>().ReverseMap();
            CreateMap<Category, CategoryResponse>().ReverseMap();
            CreateMap<Category, string>().ConvertUsing(c => c.Id);
        }
    }
}