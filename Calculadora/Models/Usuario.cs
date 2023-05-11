using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Calculadora.Models
{
    public class Usuario
    {
        public int ID { get; set; }

        
        public string Email { get; set; }


        public string NombreUsuario { get; set; }

        [StringLength(10, MinimumLength = 8, ErrorMessage = "El campo Password debe tener al menos 8 caracteres.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$", ErrorMessage = "La contraseña debe contener al menos una letra minúscula, una letra mayúscula, un número y un carácter especial.")]
        public string Password { get; set; }

        [MaxLength(10)]
        public virtual ICollection<Operaciones> Operaciones { get; set; }

    }
}
