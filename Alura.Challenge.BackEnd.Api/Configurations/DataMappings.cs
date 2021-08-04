using Alura.Challenge.Domain.Models.Data;
using Alura.Challenge.Domain.Models.Requests;
using Alura.Challenge.Domain.Models.Responses;
using AutoMapper;

namespace Alura.Challenge.BackEnd.Api.Configurations
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
            CreateMap<User, Database.Models.User>().ReverseMap();
            CreateMap<User, UserRequest>().ReverseMap();
            CreateMap<User, UserCreatedResponse>().ReverseMap();
            CreateMap<User, LoginResponse>();
            CreateMap<GrantToken, Database.Models.GrantToken>().ReverseMap();
        }
    }
}