using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DraftsMVC5.ViewModels;
using AutoMapper;
using Drafts.ServiceLayer;
using Drafts.ModelLayer;

namespace DraftsMVC5
{
    public class MapperProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<AdSearchViewModel, AdSearchParams>().ReverseMap();
            Mapper.CreateMap<Advert, AdViewModel>().ForMember(dest => dest.Seller, opts => opts.MapFrom(src => src.AspNetUser.UserName)).ReverseMap();
            Mapper.CreateMap<EditAdViewModel, Advert>().ReverseMap();
        }
    }
}