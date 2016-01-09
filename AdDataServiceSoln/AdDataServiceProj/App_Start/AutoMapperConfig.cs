using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AdDataServiceProj.AdDataClientProxyService;
using AdDataServiceProj.Models;
using AdDataServiceProj.VM;
using AutoMapper;

namespace AdDataServiceProj
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(cfg => cfg.AddProfile(new AdDataProfile()));            
        }
    }

    public class AdDataProfile : Profile
    {
        protected override void Configure()
        {
            //base.CreateMap<AdDataClientServiceModel, Ad>();
            //base.CreateMap<AdDataClientServiceModel, AdDataClientServiceViewModel>();            
            //base.CreateMap<AdDataServiceProj.Models.AdBrand, AdDataClientProxyService.Brand>();
            //base.CreateMap<AdBrand, AdBrandVM>();

        }
    }
}