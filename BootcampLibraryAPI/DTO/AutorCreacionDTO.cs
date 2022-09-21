using System.ComponentModel.DataAnnotations;

namespace BootcampLibraryAPI.DTO
{
    public class AutorCreacionDTO
    {
        [Required]
        [StringLength(150)]
        public string NombreCompleto { get; set; }
    }
}
