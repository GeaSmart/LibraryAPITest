using System.ComponentModel.DataAnnotations;

namespace BootcampLibraryAPI.Entidades
{
    public class Autor
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(150)]
        public string NombreCompleto { get; set; }
    }
}
