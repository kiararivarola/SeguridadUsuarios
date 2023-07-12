using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using infraestructure.model;
using infraestructure.Repository;

namespace services.Services
{
    public class UsuarioService
    {
        private UsuarioRepository repositoryUsuario;

        public UsuarioService(string connectionString)
        {
            this.repositoryUsuario = new UsuarioRepository(connectionString);
        }

        public string insertarUsuario(UsuarioModel usuario)
        {
            return validarDatosUsuario(usuario) ? repositoryUsuario.insertarUsuario(usuario) : throw new Exception("Error en la validación");
        }

        public string modificarUsuario(UsuarioModel usuario, int id)
        {
            if (repositoryUsuario.consultarUsuario(id) != null)
            {
                return validarDatosUsuario(usuario) ? repositoryUsuario.modificarUsuario(usuario, id) : throw new Exception("Error en la validación");
            }
            else
            {
                return "No se encontraron los datos de esta cuenta";
            }
        }

        public string eliminarUsuario(int id)
        {
            return repositoryUsuario.eliminarUsuario(id);
        }

        public UsuarioModel consultarUsuario(int id)
        {
            return repositoryUsuario.consultarUsuario(id);
        }

        public IEnumerable<UsuarioModel> listarUsuario()
        {
            return repositoryUsuario.listarUsuario();
        }

        private bool validarDatosUsuario(UsuarioModel usuario)
        {
            if (usuario.login.Trim().Length < 1)
            {
                return false;
            }
            return true;
        }
    }
}
