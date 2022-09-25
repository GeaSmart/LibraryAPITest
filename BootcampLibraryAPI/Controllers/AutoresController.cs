using AutoMapper;
using BootcampLibraryAPI.Data;
using BootcampLibraryAPI.DTO;
using BootcampLibraryAPI.Entidades;
using BootcampLibraryAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BootcampLibraryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutoresController : ControllerBase
    {
        private readonly IRepository<Autor> repository;
        private readonly IMapper mapper;

        public AutoresController(IRepository<Autor> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<AutorDTO>>> Get()
        {
            var autores = await repository.GetAsync(); //context.Autores.ToListAsync();
            return mapper.Map<List<AutorDTO>>(autores);
        }

        [HttpGet("{id:int}")] //localhost/api/autores/1
        public async Task<ActionResult<AutorDTO>> Get(int id)
        {
            var autor = await repository.GetAsync(id); //context.Autores.FirstOrDefaultAsync(x => x.Id == id);

            if (autor == null)
                return NotFound($"El autor con id {id} no existe.");

            return mapper.Map<AutorDTO>(autor);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AutorCreacionDTO autorCreacionDTO)
        {
            Expression < Func<Autor, bool> > filtroNombre = m => m.NombreCompleto.Contains(autorCreacionDTO.NombreCompleto);
            var autoresMismoNombre = await repository.GetAsync(filtroNombre); //context.Autores.AnyAsync(x => x.NombreCompleto == autorCreacionDTO.NombreCompleto);
            if (autoresMismoNombre.Count > 0)
                return BadRequest($"Ya existe un autor con el nombre {autorCreacionDTO.NombreCompleto}");

            var autor = mapper.Map<Autor>(autorCreacionDTO);

            await repository.PostAsync(autor);
            return Ok();
        }

    }
}
