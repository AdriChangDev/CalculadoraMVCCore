using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Calculadora.Models
{
    public class Usuario
    {
        public int ID { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string NombreUsuario { get; set; }

        public string Password { get; set; }

        [MaxLength(10)]
        public virtual ICollection<Operaciones> Operaciones { get; set; }

    }
}
