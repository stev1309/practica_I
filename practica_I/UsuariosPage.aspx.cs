using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Capa_Datos;
using Capa_Logica;

namespace practica_I
{
    public partial class UsuariosPage : System.Web.UI.Page
    {
        string pathDefaultAvatar = "~/images/avatar-01.jpg";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDatos();
                Session["usuConfirmar"] = null;
                Session["estadoUsuarioConf"] = null;
                btnConfirmarRegistro.Visible = false;
            }
        }

        public string CambiarImagen(string filePath)
        {
            FileStream fs = new FileStream(Server.MapPath(filePath), System.IO.FileMode.Open, System.IO.FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            byte[] bytes = br.ReadBytes((Int32)fs.Length);
            string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
            return "data:image/png;base64," + base64String;
        }

        public void CargarDatos()
        {
            var usuariosConfirmar = LogicaUsuarios.ListarUsuarios('C');

            lvUsuariosConfirmar.DataSource = usuariosConfirmar;
            lvUsuariosConfirmar.DataBind();
        }

        protected void btnRegVerDatosUsu_Click(object sender, EventArgs e)
        {
            int codUsu = Convert.ToInt32((sender as LinkButton).CommandArgument);

            TBLUSUARIOS usuario = LogicaUsuarios.usuarioXID(codUsu);

            lblNombreConf.Text = usuario.NOMBRE_USUARIO + " " + usuario.APELLIDO_USUARIO;
            lblUsuarioConf.Text = usuario.USU_USUARIO;
            lblEmailConf.Text = usuario.EMAIL_USUARIO;
            Session["estadoUsuarioConf"] = usuario.ESTADO_USUARIO;
            lblFechaConf.Text = usuario.FECHA_CREACION_USUARIO.ToString();
            lblTelefonoConf.Text = usuario.TELEFONO_USUARIO;
            Session["usuConfirmar"] = usuario.ID_USUARIO;

            btnConfirmarRegistro.Visible = true;
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void Limpiar()
        {
            lblNombreConf.Text = "";
            lblUsuarioConf.Text = "";
            lblEmailConf.Text = "";
            //lblEstadoConf.Text = "";
            lblFechaConf.Text = "";
            lblTelefonoConf.Text = "";
            Session["usuConfirmar"] = null;
            Session["estadoUsuarioConf"] = null;
            btnConfirmarRegistro.Visible = false;
        }
    }
}