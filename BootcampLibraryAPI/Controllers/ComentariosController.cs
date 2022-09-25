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
    [Route("api/libros/{libroId:int}/[controller]")]
    [ApiController]
    public class ComentariosController : ControllerBase
    {
        private readonly IRepository<Comentario> repository;
        private readonly IRepository<Libro> repositoryLibro;
        private readonly IMapper mapper;

        public ComentariosController(IRepository<Comentario> repository,IRepository<Libro> repositoryLibro, IMapper mapper)
        {
            this.repository = repository;
            this.repositoryLibro = repositoryLibro;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ComentarioDTO>>> Get(int libroId)
        {
            //valida libro existe
            var libro = await repositoryLibro.GetAsync(libroId);
            if(libro == null)
                return NotFound($"El libro con id {libroId} no existe");

            Expression<Func<Comentario, bool>> filtroIdLibro = m => m.LibroId == libroId;

            var comentarios = await repository.GetAsync(filtroIdLibro);
            return mapper.Map<List<ComentarioDTO>>(comentarios);
        }

        [HttpPost]
        public async Task<ActionResult> Post(int libroId, ComentarioCreacionDTO comentarioCreacionDTO)
        {
            //valida libro existe
            var libro = await repositoryLibro.GetAsync(libroId);
            if (libro == null)
                return NotFound($"El libro con id {libroId} no existe");

            var comentario = mapper.Map<Comentario>(comentarioCreacionDTO);
            comentario.LibroId = libroId;
            await repository.PostAsync(comentario);
            return Ok();
        }
    }
}
