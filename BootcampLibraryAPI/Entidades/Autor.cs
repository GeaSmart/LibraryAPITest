using System.ComponentModel.DataAnnotations;

namespace BootcampLibraryAPI.Entidades
{
    public class Autor : BaseEntity
    {
        [Required]
        [StringLength(150)]
        public string NombreCompleto { get; set; }
    }
}
