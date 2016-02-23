using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Drafts.ModelLayer;
using Drafts.DataAccessLayer;

namespace Drafts.ServiceLayer
{
    public static class AutoMapperServiceConfiguration
    {
        public static void Configure()
        {
            Mapper.CreateMap<AspNetUser, User>();
            Mapper.CreateMap<User, AspNetUser>();

            Mapper.CreateMap<Ad, Advert>();
            Mapper.CreateMap<Advert, Ad>();
        }
    }
}
