﻿using Core.Services;
using Groovy.Data;
using Groovy.Services;
using Groovy.Services.Contracts;
using Groovy.Services.Helpers;
using Groovy.Services.Repository;
using Microsoft.Extensions.Logging;

namespace Groovy
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

            builder.Services.AddServices();

            return builder.Build();
        }
    }
}