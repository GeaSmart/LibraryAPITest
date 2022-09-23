using System.ComponentModel.DataAnnotations;

namespace BootcampLibraryAPI.DTO
{
    public class ComentarioCreacionDTO
    {
        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }
        [StringLength(100)]
        public string Contenido { get; set; }
    }
}
