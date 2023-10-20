using Autofac.Core;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            serviceCollection.AddSingleton<ICacheManager,MemoryCacheManager>();
            serviceCollection.AddMemoryCache(); //IMemoryCache _memoryCache; burdaki microsoft tarafı interface karşılığını bu metot ile otomatik verir.
            //bunu aslında belleğe o inerface in karşılığını yükler ancak biz kullanacağımız yerde kendimiz aslında çekeriz bellekten o sınıfı.
            serviceCollection.AddSingleton<Stopwatch>();
        }
    }
}
