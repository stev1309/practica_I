using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Datos;
using System.Data.Linq;
using System.Security.Cryptography;

namespace Capa_Logica
{
    public class LogicaUsuarios
    {
        #region DBML
        ///Instanciar la CapaDatos para usar el DBML para hacer consultas a la Base de Datos
        private static BDEcommerceDataContext dc = new BDEcommerceDataContext();
        #endregion

        #region Metodos

        ///METODO PARA RETORNAR TODOS LOS DATOS EN FORMA DE LISTA
        public static List<TBLUSUARIOS> obtenerUsuario() => dc.TBLUSUARIOS.Where(usu => usu.ESTADO_USUARIO == 'A').ToList();

        ///METODO PARA VERIFICAR CREDENCIALES
        public static bool autenticar(string nombre, string pass) => dc.TBLUSUARIOS.Any(usu => usu.USU_USUARIO.Equals(nombre) && usu.PASS_USUARIO.Equals(pass));

        ///METODO PARA VERIFICAR NOMBRE LOGIN
        public static bool autenticaNombreUsuario(string nombre) => dc.TBLUSUARIOS.Any(usu => usu.USU_USUARIO.Equals(nombre));

        ///METODO PARA VERIFICAR USUARIO EN ESPECIFICO (CREDENCIALES)
        public static TBLUSUARIOS autenticarXLogin(string nombre, string pass) => dc.TBLUSUARIOS.Single(usu => usu.ESTADO_USUARIO.Equals('A') && usu.USU_USUARIO.Equals(nombre) && usu.PASS_USUARIO.Equals(pass));

        public static TBLUSUARIOS buscarXUsuario(string nombre) => dc.TBLUSUARIOS.Single(usu => usu.USU_USUARIO.Equals(nombre));

        public static bool usuarioExistente(string usuario) => dc.TBLUSUARIOS.Any(usu => usu.USU_USUARIO.Equals(usuario));
        public static bool correoExistente(string correo) => dc.TBLUSUARIOS.Any(usu => usu.EMAIL_USUARIO.Equals(correo));

        ///METODO PARA GUARDAR DATOS
        public static void guardarUsuario(TBLUSUARIOS usuario)
        {
            try
            {
                usuario.ESTADO_USUARIO = 'C';
                usuario.FECHA_CREACION_USUARIO = DateTime.Now;
                usuario.TBLROLE_ID = 2;
                dc.TBLUSUARIOS.InsertOnSubmit(usuario);
                dc.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Los datos no fueron guardados: </br> " + ex.Message);
            }
        }

        ///BLOQUEAR USUARIO
        public static void bloquearUsu(TBLUSUARIOS usuario)
        {
            try
            {
                usuario.ESTADO_USUARIO = 'B';
                dc.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Los datos no fueron guardados: </br> " + ex.Message);
            }
        }

        ///Buscar usuario por correo
        public static TBLUSUARIOS buscarCorreoExistente(string email) => dc.TBLUSUARIOS.Single(usu => usu.EMAIL_USUARIO.Equals(email));

        ///Consultar 
        public static void cambiarContrasenia(TBLUSUARIOS usuario, string newPass)
        {
            try
            {
                usuario.PASS_USUARIO = newPass;
                usuario.ESTADO_USUARIO = 'A';
                dc.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Los datos no fueron guardados: </br> " + ex.Message);
            }
        }

        #endregion

        ///CAMBIAR ESTADO A RECUPERACION
        public static void cambiarEstadoRecuperacion(TBLUSUARIOS usuario)
        {
            try
            {
                usuario.ESTADO_USUARIO = 'R';
                dc.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Los datos no fueron guardados: </br> " + ex.Message);
            }
        }

        #region Encript Method

        ///Metodo para la encriptacion de la contraseña
        public static string GenerateMD5(string yourString) => string.Join("", MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(yourString)).Select(s => s.ToString("x2")));

        ///METODO PARA TRAER USUARIOS SEGUN SU ESTADO
        public static List<TBLUSUARIOS> ListarUsuarios(char estado) => dc.TBLUSUARIOS.Where(usu => usu.ESTADO_USUARIO == estado).ToList();

        ///METODO BUSCAR USUARIO POR ID - CONFIRMAR USUARIO
        public static TBLUSUARIOS usuarioXID(int idCliente) => dc.TBLUSUARIOS.Single(usu => usu.ID_USUARIO == idCliente);

        #endregion
    }
}
