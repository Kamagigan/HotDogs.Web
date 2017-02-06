using AutoMapper;
using HotDogs.Web.Models;
using HotDogs.Web.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace HotDogs.Web
{
    using System.Web.Mvc;
    using System.Web.Optimization;

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Init WebAPI Routes
            GlobalConfiguration.Configure(WebApiConfig.Register);

            // Setting JSON formatter
            var jsonFormatterSettings = GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings;
            jsonFormatterSettings.Formatting = Formatting.Indented;
            jsonFormatterSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            jsonFormatterSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            // AutoMapper
            Mapper.Initialize(config =>
            {
                config.CreateMap<HotDogViewModel, HotDog>().ReverseMap();
                //config.CreateMap<HotDogStoreViewModel, HotDogStore>().ReverseMap();

                config.CreateMap<HotDogStore, HotDogStoreViewModel>()
                .ForMember("OwnerName", m => m.MapFrom(s => s.Owner.UserName))
                .ReverseMap();
            });

            // Init MVC 5
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
