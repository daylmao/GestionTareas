using GestionTareas.Context;
using GestionTareas.Core.Application.Interfaces.Repository;
using GestionTareas.Infraestructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionTareas.Infraestructure.Persistence
{
    public static class AddPersistence
    {
        public static void AddPersistenceMethod(this IServiceCollection services, IConfiguration configuration)
        {
            #region connection
            services.AddDbContext<GestorTareasContext>(b =>
            {
                b.UseSqlServer(configuration.GetConnectionString("GestorTareasConnection"),
                c => c.MigrationsAssembly(typeof(GestorTareasContext).Assembly.FullName));
            });
            #endregion

            #region repository
            services.AddTransient<ITareaRepository, TareaRepository>();

            #endregion
        }
    }
}
