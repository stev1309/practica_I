using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Datos;

namespace Capa_Logica
{
    public class LogicaCodigos
    {
        #region DBML
        //Instanciar la CapaDatos para usar el DBML para hacer consultas a la Base de Datos
        private static BDEcommerceDataContext dc = new BDEcommerceDataContext();
        #endregion

        #region Methods

        //Insertar datos
        public static void guardarCodigo(int idUsuario, string codigo)
        {
            try
            {
                TBLCODRECUPERACION re = new TBLCODRECUPERACION();
                re.TBLUSUARIO_ID = idUsuario;
                re.CODIGO_RECUPERACION = codigo;
                re.FECHA_CREACION_COD_RECUPERACION = DateTime.Now;
                dc.TBLCODRECUPERACION.InsertOnSubmit(re);
                dc.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Los datos no fueron guardados: </br> " + ex.Message);
            }
        }

        //Consultar por ultimo codigo segun cliente
        public static TBLCODRECUPERACION ultimoCodigo(int idUsuario, string codigo) => dc.TBLCODRECUPERACION.OrderByDescending(s => s.ID_COD_RECUPERACION).First(cod => cod.CODIGO_RECUPERACION == codigo);

        #endregion

    }
}
