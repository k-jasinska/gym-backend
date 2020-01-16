using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.StartupConfiguration
{
    public static class MvcExtensions
    {
        public static void ConfigureMvc(this IServiceCollection services)
        {
            services
                .AddMvc(options =>
                {
                    //                    options.ModelBinderProviders.Insert(0, new ModelBinderProvider());
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(options =>
                {
                    //                    options.SerializerSettings.RegisterAllConverters();
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                }); ;
        }
        public static void ConfigureRouteOptions(this IServiceCollection services)
        {
            services.Configure<RouteOptions>(options => { options.LowercaseUrls = true; });
        }
    }
}
