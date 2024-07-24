using System.ComponentModel.DataAnnotations;

namespace Verifarma.Models
{
    public class Farmacia
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; } = null!;

        [Required]
        public string Direccion { get; set; } = null!;

        [Required]
        public float Latitud { get; set; }

        [Required]
        public float Longitud { get; set; }
    }
}
