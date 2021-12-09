using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Text;

namespace Bookinist.ViewModels
{
   public class ViewModelLocator
    {
        public MainViewModel GetMainViewModel => App.Services.GetRequiredService<MainViewModel>();
    }
}
