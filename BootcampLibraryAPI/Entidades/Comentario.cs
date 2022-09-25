using System.ComponentModel.DataAnnotations;

namespace BootcampLibraryAPI.Entidades
{
    public class Comentario : BaseEntity
    {        
        [Required]
        [Range(1,5)]
        public int Rating { get; set; }
        [StringLength(100)]
        public string Contenido { get; set; }
        public int LibroId { get; set; }

        //Propiedad(es) de navegación
        public Libro Libro { get; set; }
    }
}
