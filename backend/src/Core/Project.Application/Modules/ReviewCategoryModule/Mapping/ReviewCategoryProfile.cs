using AutoMapper;
using Project.Application.Modules.ReviewCategoryModule.Queries.ReviewCategoryGetAllQuery;
using Project.Domain.Models.Entities;


namespace Project.Application.Modules.ReviewCategoryModule.Mapping
{
    public class ReviewCategoryProfile : Profile
    {
        public ReviewCategoryProfile()
        {
            CreateMap<ReviewCategory, ReviewCategoryDto>();
        }
    }
}
