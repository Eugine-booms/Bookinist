using Bookinist.DAL.Entities;
using Bookinist.Interfaces;

using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Text;

namespace Bookinist.DAL
{
  public static  class RepositoryRegistrator
    {
        public static IServiceCollection RegisterRepositoryesInDB(this IServiceCollection services) => services
            .AddTransient<IRepository<Book>, BooksRepository>()
            .AddTransient<IRepository<Category>, CategoryRepository>()
            .AddTransient<IRepository<Buyer>, DbRepository<Buyer>>()
            .AddTransient<IRepository<Seller>, DbRepository<Seller>>()
            .AddTransient<IRepository<Deal>, DealRepository>();
    }
}
