using ConsumeAPI.Interface;
using ConsumeAPI.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsumeAPI.Configuration
{
    public static class Configuration
    {
        public static void UseServices(this IServiceCollection services)
        {
            services.AddHttpClient<ITodoService, TodoService>();

        }


    }
}
