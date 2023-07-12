using Dapper;
using infraestructure.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infraestructure.Repository
{
    public class UsuarioRepository
    {
        private string _connectionString;
        private Npgsql.NpgsqlConnection connection;
        public UsuarioRepository(string connectionString)
        {
            this._connectionString = connectionString;
            this.connection = new Npgsql.NpgsqlConnection(this._connectionString);
        }

        public UsuarioRepository()
        {
        }

        public string insertarUsuario(UsuarioModel usuario)
        {
            try
            {
                connection.Execute(" insert into usuario "
                    + "(login, password, activo) "
                    + " values (@login, @password, @activo)", usuario);
                return "Se insertó el usuarioo correctamente...";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string modificarUsuario(UsuarioModel usuario, int id)
        {
            try
            {
                connection.Execute($" UPDATE usuario SET " +
                    " login = @login, " +
                    " password = @password, " +
                    " activo = @activo " +
                    $" WHERE id = {id}", usuario);
                return "Se modificaron los datos correctamente...";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string eliminarUsuario(int id)
        {
            try
            {
                connection.Execute($" DELETE FROM usuario WHERE id = {id}");
                return "Se eliminó el usuario correctamente...";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public UsuarioModel consultarUsuario(int id)
        {
            try
            {
                return connection.QueryFirst<UsuarioModel>($"SELECT * FROM usuario WHERE id = {id}");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


            public IEnumerable<UsuarioModel> listarUsuario()
            {
                try
                {
                    return connection.Query<UsuarioModel>($"SELECT * FROM usuario order by id asc; ");
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
