using AutoMapper;
using BootcampLibraryAPI.DTO;
using BootcampLibraryAPI.Entidades;

namespace BootcampLibraryAPI.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //Reglas de mapeo
            CreateMap<Autor, AutorDTO>();
            CreateMap<AutorCreacionDTO, Autor>();
            CreateMap<Libro, LibroDTO>();
            CreateMap<LibroCreacionDTO, Libro>();
        }
    }
}
