using Core.Services;
using Groovy.Data;
using Groovy.Services;
using Groovy.Services.Contracts;
using Groovy.Services.Helpers;
using Groovy.Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Groovy
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<AudioBuilder>();
#if WINDOWS
            services.AddSingleton<INotificationHelper, WindowsNotificationHelper>();
#else
            services.AddScoped<INotificationHelper, AndroidNotificationHelper>();
#endif
            services.AddSingleton<IAudioPlayerService, MauiAudioPlayerService>();
            services.AddSingleton<WeatherForecastService>();
            services.AddScoped<JavaScriptInterop>();
            services.AddSingleton<Library>();
            return services;
        }
    }
}
