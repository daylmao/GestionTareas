using GestionTareas.Core.Application.Interfaces.Service;
using GestionTareas.Core.Application.Service;
using GestionTareas.Mapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionTareas.Core.Application
{
    public static class AddApplication
    {
        public static void AddApplicationMethod(this IServiceCollection services)
        {
            #region mapper
            services.AddAutoMapper(typeof(MapperProfile));
            #endregion

            #region services
            services.AddScoped<ITareaService, TareaService>();
            #endregion

            
        }

    }
}
