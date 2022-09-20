using BootcampLibraryAPI.Data;
using BootcampLibraryAPI.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<List<Autor>> Get()
        {
            return await context.Autores.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Autor autor)
        {
            //TODO: No existan nombres duplicados

            context.Add(autor);
            await context.SaveChangesAsync();
            return Ok();
        }

    }
}
