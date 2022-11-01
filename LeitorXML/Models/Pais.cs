using System.ComponentModel.DataAnnotations;

namespace LeitorXML.Models
{
    public class Pais
    {
        [Key]
        public int? PaisId { get; set; }
        public string? Codigo { get; set; } = null;
        public string? Nome { get; set; } = null;
    }

    public class PaisContainer
    {
        public Pais[]? Paises { get; set; }
    }
}
