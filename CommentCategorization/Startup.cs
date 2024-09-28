using FinalGroupProject.SQLRepository;
using GenericLibrary.Database;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

[assembly: FunctionsStartup(typeof(FinalGroupProject.Startup))]

namespace FinalGroupProject
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));
            builder.Services.AddTransient<ISqlDbConnection, SqlDbConnection>();
            builder.Services.AddTransient<ISqlRepository, SqlRepository>();
        }
    }
}

