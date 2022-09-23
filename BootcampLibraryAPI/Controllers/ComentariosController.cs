using AutoMapper;
using BootcampLibraryAPI.Data;
using BootcampLibraryAPI.DTO;
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
    }
}
