using apiservicio.Business.Contracts;
using apiservicio.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace apiservicio.Business.Clases
{
    //agregamos el using y el irolrepository
    //para agregar una conexion de base de datos
    //tambien agregamos la libreria using microsoft para seguir la conexion de base datos
    //un public rolrepository con iconfigrate _con y hacemos el llamdo de la coneccion de base de datos
    //
    public class RolRepository : IRolRepository
    {
        private readonly string conect;
        public RolRepository(IConfiguration _con)
        {
            conect = _con.GetConnectionString("conBase");
        }

        public Task<Rol> AgregarActualiza(Rol l, string t)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Rol>> GetList()
        {
            List<Rol> list = new List<Rol>();
            Rol l;
            using (SqlConnection con = new SqlConnection(conect))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("select * from Rol", con);
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        l = new Rol();
                        l.Id = Convert.ToInt32(reader["id"]);
                        l.NombreRol = Convert.ToString(reader["NombreRol"]);
                        list.Add(l);
                    }
                }
            }
            return list;

        }
        public async Task<Rol>AgregaActualiza(Rol l, string t)
        {
            using (SqlConnection con = new SqlConnection(conect))
            {
                string cadena = " ";
                if(t=="c")
                    cadena = "SET @l = (SELECT ISNULL(MAX(Id), 0) + 1 FROM Rol); " +
         "INSERT INTO Rol(Id, NombreRol) VALUES (@l, @NombreRol);";

                if (t == "u")
                {
                    cadena = "INSERT INTO Rol(NombreRol) VALUES (@NombreRol); " +
         "SET @l = SCOPE_IDENTITY();";

                }
                using (SqlCommand cmd = new SqlCommand(cadena, con))
                {
                    SqlParameter Result = new SqlParameter("@l", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(Result);
                    cmd.Parameters.AddWithValue("@Id", l.Id);
                    cmd.Parameters.AddWithValue("@NombreRol", l.NombreRol);
                    await con.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    if(t== "c")
                    {
                        l.Id=int.Parse(Result.Value.ToString());
                    }
                }
            }
            return l;
        }
    }
}

