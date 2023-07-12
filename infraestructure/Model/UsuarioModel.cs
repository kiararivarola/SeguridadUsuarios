using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infraestructure.model
{
    public class UsuarioModel
    {
        public int id { get; set; }

        public string login { get; set; }

        public string password { get; set; }

        public bool activo { get; set; }

    }
}
