using Bookinist.Services.Interfaces;

using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Bookinist.Services
{
   internal static class Registrator

    {
        internal static IServiceCollection RegisterServices(this IServiceCollection services) => services
            .AddTransient<IUserDialog, UserDialog>();
    }
}
