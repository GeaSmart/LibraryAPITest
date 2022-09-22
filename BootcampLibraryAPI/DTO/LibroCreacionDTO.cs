using System.ComponentModel.DataAnnotations;

namespace BootcampLibraryAPI.DTO
{
    public class LibroCreacionDTO
    {
        [Required]
        [StringLength(75)]
        public string Titulo { get; set; }
        public DateTime FechaPublicacion { get; set; }
    }
}
