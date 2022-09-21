using BootcampLibraryAPI.Data;
using BootcampLibraryAPI.DTO;
using BootcampLibraryAPI.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BootcampLibraryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutoresController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public AutoresController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<List<AutorDTO>> Get()
        {
            //mapeo manual
            List<AutorDTO> listaResponse = new List<AutorDTO>();
            var listaAutores = await context.Autores.ToListAsync();
            foreach (var item in listaAutores)
            {
                var dto = new AutorDTO { Id = item.Id, NombreCompleto = item.NombreCompleto };
                listaResponse.Add(dto);
            }
            return listaResponse;
            //return await context.Autores.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AutorCreacionDTO autorCreacionDTO)
        {
            //TODO: No existan nombres duplicados

            var existe = await context.Autores.AnyAsync(x => x.NombreCompleto == autorCreacionDTO.NombreCompleto);
            if (existe)
                return BadRequest($"Ya existe un autor con el nombre {autorCreacionDTO.NombreCompleto}");

            //Expression<Func<Autor,bool>> filter = x => x.NombreCompleto == autor.NombreCompleto;
            //var listaAutoresFiltrados = await context.Autores.Where(filter).ToListAsync();
            //if (await context.Autores.FirstOrDefaultAsync(x => x.NombreCompleto == autor.NombreCompleto) != null)
            //{
            //    return BadRequest($"Ya existe un autor con el nombre {autor.NombreCompleto}");
            //}

            //mapeo manual
            var autor = new Autor { NombreCompleto = autorCreacionDTO.NombreCompleto };

            context.Add(autor);
            await context.SaveChangesAsync();
            return Ok();
        }

    }
}
