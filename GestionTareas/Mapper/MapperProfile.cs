using AutoMapper;
using GestionTareas.DTOs;
using GestionTareas.Enum;
using GestionTareas.Models;
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
