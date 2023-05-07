using System.ComponentModel.DataAnnotations;

namespace Calculadora.Models
{
    public class Operaciones
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La expresión matemática es requerida.")]
        [StringLength(100, ErrorMessage = "La expresión matemática debe tener un máximo de 100 caracteres.")]
        public string ExpresionMatematica { get; set; }

        public string Resultado { get; set; }
        
        public DateTime fechaHora { get; set; }=DateTime.Now;

        public int UsuarioId { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
