using infraestructure.model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace seguridad.usuarios.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        readonly byte[] key = Encoding.ASCII.GetBytes("E@!knadkjbad45678ad.ci@456akjd|!45a");

        //[Authorize]
        [HttpPost]
        public IActionResult Autenticar([FromBody] UsuarioModel loginModel)
        {
            if (loginModel.activo == false)
                return Unauthorized();
            if (!UsuarioAutenticado(loginModel.login, loginModel.password))
                return Unauthorized();

            var token = crearToken(loginModel.login);

            return Ok(token);

            //return null;
        }

        private bool UsuarioAutenticado(string user, string password)
        {
            UsuarioModel usu = new UsuarioModel();
            var connectionString = "Server=127.0.0.1;Port=5432;Database=parcial-2-optativo;User Id=postgres;Password=kiara";
            using (var connection = new Npgsql.NpgsqlConnection(connectionString))
            {
                connection.Open();
                var query = $"SELECT * FROM usuario WHERE login = '{user}'";
                using (var command = new Npgsql.NpgsqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var storedPassword = reader["password"].ToString();
                            if (storedPassword == password)
                                return true;
                        }
                        return false;

                    }
                }
            }
        }

        private string crearToken(string user)
        {
            var handlerToken = new JwtSecurityTokenHandler();
            var descriptorToken = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };
            var token = handlerToken.CreateToken(descriptorToken);
            return handlerToken.WriteToken(token);
        }
    }

    public class LoginModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
