﻿using FUNFOX.NET5.Application.Interfaces.Services;
using FUNFOX.NET5.Server.Hubs;
using FUNFOX.NET5.Server.Middlewares;
using FUNFOX.NET5.Shared.Constants.Application;
using FUNFOX.NET5.Shared.Constants.Localization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Globalization;
using System.Linq;

namespace FUNFOX.NET5.Server.Extensions
{
    internal static class ApplicationBuilderExtensions
    {
        internal static IApplicationBuilder UseExceptionHandling(
            this IApplicationBuilder app,
            IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }

            return app;
        }

        internal static void ConfigureSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", typeof(Program).Assembly.GetName().Name);
                options.RoutePrefix = "swagger";
                options.DisplayRequestDuration();
            });
        }

        internal static IApplicationBuilder UseEndpoints(this IApplicationBuilder app)
            => app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
                endpoints.MapHub<SignalRHub>(ApplicationConstants.SignalR.HubUrl);
            });

        //internal static IApplicationBuilder UseRequestLocalizationByCulture(this IApplicationBuilder app)
        //{
        //    var supportedCultures = LocalizationConstants.SupportedLanguages.Select(l => new CultureInfo(l.Code)).ToArray();
        //    app.UseRequestLocalization(options =>
        //    {
        //        options.SupportedUICultures = supportedCultures;
        //        options.SupportedCultures = supportedCultures;
        //        options.DefaultRequestCulture = new RequestCulture(supportedCultures.First());
        //        options.ApplyCurrentCultureToResponseHeaders = true;
        //    });

        //    app.UseMiddleware<RequestCultureMiddleware>();

        //    return app;
        //}

        internal static IApplicationBuilder Initialize(this IApplicationBuilder app, Microsoft.Extensions.Configuration.IConfiguration _configuration)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();

            var initializers = serviceScope.ServiceProvider.GetServices<IDatabaseSeeder>();

            foreach (var initializer in initializers)
            {
                initializer.Initialize();
            }

            return app;
        }
    }
}