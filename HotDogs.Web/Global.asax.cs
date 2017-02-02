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
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            // Setting JSON formatter
            var jsonFormatterSettings = GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings;
            jsonFormatterSettings.Formatting = Formatting.Indented;
            jsonFormatterSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // AutoMapper
            Mapper.Initialize(config =>
            {
                config.CreateMap<HotDogViewModel, HotDog>().ReverseMap();
                config.CreateMap<HotDogStoreViewModel, HotDogStore>().ReverseMap();
            });
        }
    }
}
