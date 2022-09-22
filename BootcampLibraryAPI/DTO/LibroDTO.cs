using System.ComponentModel.DataAnnotations;

namespace BootcampLibraryAPI.DTO
{
    public class LibroDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public DateTime FechaPublicacion { get; set; }
    }
}
