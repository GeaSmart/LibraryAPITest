using System.ComponentModel.DataAnnotations;

namespace BootcampLibraryAPI.Entidades
{
    public class Libro
    {
        public int Id { get; set; }
        [Required]
        [StringLength(75)]
        public string Titulo { get; set; }

        public DateTime FechaPublicacion { get; set; }

    }
}
