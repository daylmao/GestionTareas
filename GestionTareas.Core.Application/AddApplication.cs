using GestionTareas.Core.Application.Interfaces.Service;
using GestionTareas.Core.Application.Service;
using GestionTareas.Mapper;
using Microsoft.Extensions.DependencyInjection;


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
            services.AddScoped<TareaValidadorService>();
            #endregion

            
        }

    }
}
