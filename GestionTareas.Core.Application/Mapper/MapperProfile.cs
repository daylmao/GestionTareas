using AutoMapper;
using GestionTareas.DTOs;
using GestionTareas.Core.Domain.Entities;
using System;

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
