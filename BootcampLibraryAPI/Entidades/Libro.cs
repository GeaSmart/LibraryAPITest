using System.ComponentModel.DataAnnotations;

namespace BootcampLibraryAPI.Entidades
{
    public class Libro : BaseEntity
    {
        [Required]
        [StringLength(75)]
        public string Titulo { get; set; }
        public DateTime FechaPublicacion { get; set; }

        //Propiedades de navegación
        public List<Comentario> Comentarios { get; set; }
    }
}
