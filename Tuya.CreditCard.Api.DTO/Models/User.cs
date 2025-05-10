using System.ComponentModel.DataAnnotations;

namespace Tuya.CreditCard.Api.DTO.Models
{
    public class User
    {
        public string UserName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }

    public class UserManage
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "El NOMBRE es obligatorio")]
        [RegularExpression(@"\S+", ErrorMessage = "El NOMBRE es obligatorio")]
        [StringLength(200, ErrorMessage = "El NOMBRE no puede exceder los 200 caracteres")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "El APELLIDO es obligatorio")]
        [StringLength(200, ErrorMessage = "El APELLIDO no puede exceder los 200 caracteres")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "El TELÉFONO es obligatorio")]
        [StringLength(50, ErrorMessage = "El TELÉFONO no puede exceder los 200 caracteres")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "La DIRECCIÓN es obligatoria")]
        [StringLength(200, ErrorMessage = "La DIRECCIÓN no puede exceder los 200 caracteres")]
        public string Adrress { get; set; } = string.Empty;

        [Required(ErrorMessage = "El CORREO es obligatorio")]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Debe enviar un EMAIL válido en el campo NOMBRE DE USUARIO")]
        [StringLength(400, ErrorMessage = "El CORREO no puede exceder los 200 caracteres")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "La CONTRASEÑA es obligatoria")]
        [StringLength(400, ErrorMessage = "La CONTRASEÑA no puede exceder los 400 caracteres")]
        public string Password { get; set; } = string.Empty;
    }

    public class UserLogin
    {
        [Required(ErrorMessage = "El NOMBRE DE USUARIO es obligatorio")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "La CONTRASEÑA es obligatoria")]
        public string Password { get; set; } = string.Empty;
    }

    public class UserData : User
    {
        public Guid Id { get; set; }
        public string Password { get; set; } = string.Empty;
    }
}
