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

        [Required]
        [StringLength(10, MinimumLength = 6)]
        [RegularExpression(@"^[a-zA-Z0-9!@#$%^&*()_+=\[{\]};:<>|./?,-]+$",
   ErrorMessage = "La contraseña debe contener al menos 6 caracteres, incluyendo al menos una letra mayúscula, una letra minúscula, un número y un caracter especial (!@#$%^&*()_+=[]{};:<>|./?,-)")]

        public string Password { get; set; }

        [MaxLength(10)]
        public virtual ICollection<Operaciones> Operaciones { get; set; }

    }
}
