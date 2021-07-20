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
            CreateMap<MovieRequest, Movie>().ReverseMap();
            CreateMap<Movie, Database.Models.Movie>().ReverseMap();
            CreateMap<Movie, MovieResponse>().ReverseMap();
        }
    }
}