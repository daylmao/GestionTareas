using GestionTareas.Core.Application.DTOs;


namespace GestionTareas.Core.Application.Interfaces.Service
{
    public interface ITareaService: IGenericTareaService<TareaDTO,CreateTareaDTO,UpdateTareaDTO>
    {

    }
}
