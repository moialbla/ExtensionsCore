using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExtensionsCoreUtils.CustomValidation.Extensions.Configurations
{

    public static class IServiceValidationExtension
    {

        public static IServiceCollection ScanValidations(this IServiceCollection services, params string[] assemblies)
        {
            return services;
        }
    }
}
