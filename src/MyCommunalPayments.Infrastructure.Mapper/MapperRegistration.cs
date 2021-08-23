using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCommunalPayments.Infrastructure.Mapper
{
    public static class MapperRegistration
    {

            public static void AddCustomAutoMapper(this IServiceCollection services)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new AutoMapperProfiles());
                });

                IMapper mapper = mappingConfig.CreateMapper();
                services.AddSingleton(mapper);
            }

    }
}
