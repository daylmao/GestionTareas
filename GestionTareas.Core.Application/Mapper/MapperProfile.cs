using AutoMapper;
using GestionTareas.Core.Application.DTOs;
using GestionTareas.Core.Domain.Entities;

namespace GestionTareas.Mapper
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {

            CreateMap<Tarea, TareaDTO>();

            CreateMap<CreateTareaDTO, Tarea>();

            CreateMap<UpdateTareaDTO, Tarea>();
        }
    }
}
