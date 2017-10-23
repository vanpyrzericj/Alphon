using Microsoft.Extensions.DependencyInjection;
using System;

namespace WebApp.Infrastructure.FeatureFolders
{
    public static class ServiceCollectionExtensions
    {
        public static IMvcBuilder AddFeatureFolders(this IMvcBuilder services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddRazorOptions(o =>
            {
                o.ViewLocationExpanders.Add(new FeatureViewLocationExpander());
            });

            return services;
        }
    }
}
