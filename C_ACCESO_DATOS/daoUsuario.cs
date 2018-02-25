using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C_ENTIDAD;
using System.Data;
using System.Data.SqlClient;
namespace C_ACCESO_DATOS
{
    public class daoUsuario
    {
        #region patron singleton
        public static readonly daoUsuario _instancia = new daoUsuario();
        public static daoUsuario Instancia { get { return daoUsuario._instancia; } }
        #endregion

        #region metodos
        public Usuario VerificarAcceso(String Usuario, String Password)
        {
            SqlCommand cmd = null;
            Usuario u = null;
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spVerificarAcceso", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@prmstrUsuario", Usuario);
                cmd.Parameters.AddWithValue("@prmstrPassword", Password);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    u = new Usuario();
                    u.idUsuario = Convert.ToInt16(dr["id"]);
                    u.nombreUsuario = dr["nombreUsuario"].ToString();
                    u.estadoActivo = Convert.ToBoolean(dr["estadoActivo"]);
                    u.fechaCreacion = Convert.ToDateTime(dr["fechaCreacion"]);
                    Trabajador t = new Trabajador();
                    t.nombre = dr["nombre"].ToString();
                    t.apellidos = dr["apellidos"].ToString();
                    u.trabajador = t;
                }
            } 
            catch (Exception e)
            {
                throw e;
            }
            finally { cmd.Connection.Close(); }
            return u;                
            }

        public Boolean InsertarTrabajador(Usuario u)
        {
            SqlCommand cmd = null;
            Boolean Inserto = false;
            try
            {
                SqlConnection cn = Conexion.Instancia.conectar();
                cmd = new SqlCommand("spInsertarTrabajador", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@prsmstrNombre", u.trabajador.nombre);
                cmd.Parameters.AddWithValue("@prmstrApellido", u.trabajador.apellidos);
                cmd.Parameters.AddWithValue("@prmstrDireccion", u.trabajador.direccion);
                cmd.Parameters.AddWithValue("@prmstrCorreo", u.trabajador.correo);
                cmd.Parameters.AddWithValue("@prmstrTelefono", u.trabajador.telefono);
                cmd.Parameters.AddWithValue("@prmstrSisgedo", u.trabajador.documentoSisgedo);
                cmd.Parameters.AddWithValue("@prmstrTipoTrabajador", u.trabajador.tipoTrabajador);
                cmd.Parameters.AddWithValue("@prmstrFacultad", u.trabajador.facultad);
                cmd.Parameters.AddWithValue("@prmstrUsuario", u.nombreUsuario);
                cn.Open();
                int i = cmd.ExecuteNonQuery();
                if (i > 0) { Inserto = true; }
            } catch (Exception e)
            {
                throw e;
            }
            finally { cmd.Connection.Close(); }
            return Inserto;
        }
    }
        #endregion
 }
