using AutoMapper;
using BootcampLibraryAPI.Data;
using BootcampLibraryAPI.DTO;
using BootcampLibraryAPI.Entidades;
using BootcampLibraryAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BootcampLibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        private readonly IRepository<Libro> repository;
        private readonly IMapper mapper;

        public LibrosController(IRepository<Libro> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<LibroDTO>>> Get()
        {
            var libros = await repository.GetAsync();
            return mapper.Map<List<LibroDTO>>(libros);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<LibroDTO>> Get(int id)
        {
            var libro = await repository.GetAsync(id);

            if (libro == null)
                return NotFound($"El libro con id {id} no existe.");
            return mapper.Map<LibroDTO>(libro);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] LibroCreacionDTO libroCreacionDTO)
        {
            Expression<Func<Libro, bool>> filtroTitulo = m => m.Titulo.Contains(libroCreacionDTO.Titulo);

            var LibroMismoTitulo = await repository.GetAsync(filtroTitulo);
            if (LibroMismoTitulo.Count > 0)
                return BadRequest($"Ya existe un libro con el título {libroCreacionDTO.Titulo}");

            //validación de no fecha publicación futura
            if(libroCreacionDTO.FechaPublicacion > DateTime.Now)
                return BadRequest($"No se puede insertar un libro con fecha de publicación a futuro");

            var libro = mapper.Map<Libro>(libroCreacionDTO);

            await repository.PostAsync(libro);
            return Ok();
        }
    }
}
