using System.ComponentModel.DataAnnotations;

namespace seguridad.usuarios.Model
{
    public class UsuarioModel
    {
        public int id { get; set; }

        [Required]
        public string login { get; set; }

        [Required]
        public string password { get; set; }

        public bool activo { get; set; }

    }
}
