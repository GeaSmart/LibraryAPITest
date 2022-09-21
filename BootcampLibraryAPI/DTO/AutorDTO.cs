using System.ComponentModel.DataAnnotations;

namespace BootcampLibraryAPI.DTO
{
    public class AutorDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string NombreCompleto { get; set; }
    }
}
