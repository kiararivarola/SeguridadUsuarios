using infraestructure.model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using services.Services;

namespace seguridad.usuarios.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class UsuarioController : ControllerBase
    {
        private UsuarioService usuarioService;
        private IConfiguration configuration;

        public UsuarioController(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.usuarioService = new UsuarioService(configuration.GetConnectionString("postgresDB"));
        }

        [HttpGet("ListarUsuario")]
        public ActionResult<List<UsuarioModel>> ListarUsuario()
        {
            var resultado = this.usuarioService.listarUsuario;
            return Ok(resultado);
        }

        [HttpGet("ConsultarUsuario/{id}")]
        public ActionResult<UsuarioModel> ConsultarUsuario(int id)
        {
            var resultado = this.usuarioService.consultarUsuario(id);
            return Ok(resultado);
        }

        [HttpPost("InsertarUsuario")]
        public ActionResult<string> InsertarUsuario(UsuarioModel usuario)
        {
            var resultado = this.usuarioService.insertarUsuario(new infraestructure.model.UsuarioModel
            {
                login = usuario.login,
                password = usuario.password,
                activo = usuario.activo,
            });
            return Ok(resultado);
        }

        [HttpPut("ModificarUsuario/{id}")]
        public ActionResult<string> ModificarUsuario(UsuarioModel usuario, int id)
        {
            var resultado = this.usuarioService.modificarUsuario(new infraestructure.model.UsuarioModel
            {
                login = usuario.login,
                password = usuario.password,
                activo = usuario.activo,
            }, id);
            return Ok(resultado);
        }

        [HttpDelete("EliminarUsuario/{id}")]
        public ActionResult EliminarUsuario(int id)
        {
            var resultado = this.usuarioService.eliminarUsuario(id);
            return Ok(resultado);
        }
    }
}
