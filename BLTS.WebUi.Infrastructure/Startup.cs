﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BLTS.WebUi.Infrastructure
{
    public class Startup
    {
        IServiceCollection _services;
        IConfiguration _configuration;

        public Startup(IServiceCollection services, IConfiguration configuration)
        {
            _services = services;
            _configuration = configuration;
        }

        public void Initialize()
        {

            DependencyInjectionContainer dependencyInjectionStartup = new DependencyInjectionContainer(_services);
            dependencyInjectionStartup.Initialize();
        }
    }
}
