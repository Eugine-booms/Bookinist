using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Text;

namespace Bookinist.ViewModels
{
   internal static  class Registrator

    {
        internal static IServiceCollection RegisterViewModels(this IServiceCollection services) =>
            services.AddSingleton<MainViewModel>()
            .AddTransient<BookEditorWindowViewModel>();

        
    }
}
