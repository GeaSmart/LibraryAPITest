using AutoMapper;
using BootcampLibraryAPI.Data;
using BootcampLibraryAPI.DTO;
using BootcampLibraryAPI.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BootcampLibraryAPI.Controllers
{
    [Route("api/libros/{libroId:int}/[controller]")]
    [ApiController]
    public class ComentariosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ComentariosController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ComentarioDTO>>> Get(int libroId)
        {
            //validaciones
            var libro = await context.Libros
                .Include(x=>x.Comentarios)
                .FirstOrDefaultAsync(x => x.Id == libroId);
            if (libro == null)
                return NotFound($"El libro con id {libroId} no existe");

            var comentarios = libro.Comentarios;
            return mapper.Map<List<ComentarioDTO>>(comentarios);
        }

        [HttpPost]
        public async Task<ActionResult> Post(int libroId, ComentarioCreacionDTO comentarioCreacionDTO)
        {
            //valida si libro existe
            var libroExiste = await context.Libros.AnyAsync(x => x.Id == libroId);
            if (!libroExiste)
                return BadRequest($"No se puede añadir el comentario porque el libro con id {libroId} no existe.");

            var comentario = mapper.Map<Comentario>(comentarioCreacionDTO);
            comentario.LibroId = libroId;
            context.Comentarios.Add(comentario);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
