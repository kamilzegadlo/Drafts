using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace AdPortalWebAPI
{
    public static class AutoMapperWebConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new MapperProfile());
            });
        }
    }
}