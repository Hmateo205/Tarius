using System.ComponentModel.DataAnnotations;

namespace Tarius.Models
{
    public class Admin
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "Correo no v�lido")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "La contrase�a es obligatoria")]
        [MinLength(8, ErrorMessage = "La contrase�a debe tener al menos 8 caracteres")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*]).+$",
            ErrorMessage = "Debe tener al menos una may�scula, un n�mero y un car�cter especial")]
        public string Contrase�a { get; set; }

        public string Rol { get; set; }

    }
}
