using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Drafts.ServiceLayer;
using Drafts.ModelLayer;
using AdPortalWebAPI.DTOs;


namespace AdPortalWebAPI
{
    public class MapperProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Advert, AdDTO>().ForMember(dest => dest.Seller, opts => opts.MapFrom(src => src.AspNetUser.UserName)).ReverseMap();

            Mapper.CreateMap<User, SellerDTO>().ReverseMap();

        }
    }
}