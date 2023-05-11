using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Calculadora.Models
{
    public class Usuario
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "El campo Email es necesario.")]
        [EmailAddress(ErrorMessage = "El campo Email no tiene un formato válido.Debe contener un @ y un .")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El Nombre de usuario no puede estar vacio.")]
        [StringLength(32, MinimumLength = 3, ErrorMessage = "El Nombre de Usuario debe tener entre 3 y 32 caracteres.")]
        public string NombreUsuario { get; set; }

        [Required(ErrorMessage = "El campo Password es requerido.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "El campo Password debe tener al menos 8 caracteres.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$", ErrorMessage = "La contraseña debe contener al menos una letra minúscula, una letra mayúscula, un número y un carácter especial.")]
        public string Password { get; set; }

        [MaxLength(10)]
        public virtual ICollection<Operaciones> Operaciones { get; set; }

    }
}
