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
    public partial class HomeTemplate : System.Web.UI.MasterPage
    {
        string pathDefaultAvatar = "~/images/avatar-01.jpg";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["nomUsuario"] != null && Session["tipoUsuario"] != null)
                {
                    lblNombreUsuario.Text = Session["nomUsuario"].ToString();
                    lblRolUsuario.Text = Session["tipoUsuario"].ToString();
                    BuscarUsuario();
                }
                else
                {
                    Session["toastTitulo"] = "No ha iniciado sesión";
                    Session["toastMensaje"] = "Vuelva a iniciar sesión para comprabar su identidad";
                    Session["toastClass"] = "danger";
                    Response.Redirect("~/loginTemplates/LoginPage.aspx");
                }
            }
        }

        private void BuscarUsuario()
        {
            string usu = Session["usuUsuario"].ToString();
            string pass = Session["passUsuario"].ToString();

            TBLUSUARIOS usuario = LogicaUsuarios.autenticarXLogin(usu, pass);
            if (!string.IsNullOrEmpty(usuario.FOTO_PATH_USUARIO))
            {
                CambiarImagen(usuario.FOTO_PATH_USUARIO);
            }
            else
            {
                CambiarImagen(pathDefaultAvatar);
            }
        }

        protected void btnUsuarios_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UsuariosPage.aspx");
        }

        private void CambiarImagen(string filePath)
        {
            FileStream fs = new FileStream(Server.MapPath(filePath), System.IO.FileMode.Open, System.IO.FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            byte[] bytes = br.ReadBytes((Int32)fs.Length);
            string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
            imgAvatar.ImageUrl = "data:image/png;base64," + base64String;
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("~/loginTemplates/LoginPage.aspx");
        }

        protected void btnLoginRegister_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/loginTemplates/LoginPage.aspx");
        }
    }
}