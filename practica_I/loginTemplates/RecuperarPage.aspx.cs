using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Capa_Logica;
using Capa_Datos;
using System.IO;

namespace practica_I.loginTemplates
{
    public partial class RecuperarPage : System.Web.UI.Page
    {
        string pathDefaultAvatar = "~/images/avatar-01.jpg";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pnConfirmarCorreo.Visible = true;
                pnCambiarContrasenia.Visible = false;
                Session["contrseniaUsuario"] = null;
                Session["usuarioRecuperar"] = null;
            }
        }

        protected void btnConfirmarCodigo_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                mostrarToast("Email vacío", "Ingrese su email", "Warning");
                return;
            }
            if (string.IsNullOrEmpty(txtCodigo.Text))
            {
                mostrarToast("Código vacío", "Ingrese el código para confirmar la autorización de cambio de contraseña", "Warning", 5000);
                return;
            }
            TBLUSUARIOS usu = new TBLUSUARIOS();
            TBLCODRECUPERACION cod = new TBLCODRECUPERACION();
            try
            {
                usu = LogicaUsuarios.buscarCorreoExistente(txtEmail.Text);
                cod = LogicaCodigos.ultimoCodigo(usu.ID_USUARIO, txtCodigo.Text);
                cambiarImagen(usu.FOTO_PATH_USUARIO);
                Session["contrseniaUsuario"] = usu.PASS_USUARIO;
                Session["usuarioRecuperar"] = usu;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("error al recuperar informacion: </br> " + ex.Message);
            }

            if (usu != null)
            {
                if (cod != null)
                {
                    if (cod.CODIGO_RECUPERACION == txtCodigo.Text)
                    {
                        mostrarToast("Codigo Correcto", "Ingrese una nueva contraseña para su usuario. Siga las politicas de seguridad para su nueva contraseña. NO INGRESE LA MISMA CONTRASEÑA ANTERIOR!", "Info", 10000);
                        pnConfirmarCorreo.Visible = false;
                        pnCambiarContrasenia.Visible = true;
                    }
                    else
                    {
                        mostrarToast("Codigo Incorrecto", "El código ingresado no es el correcto, copie sin espacios el código e ingreselo nuevamente. Si persiste el error consulte con el administrador.", "Error", 8000);
                    }
                }
                else
                {
                    mostrarToast("Error en Codigo", "Error en codigo, esta vacio", "Error");
                    mostrarToast("Error en Codigo", "Error en codigo, esta vacio", "Error");
                }
            }
            else
            {
                mostrarToast("Error en Usuario", "Error en Usuario por correo, no encontrado", "Error");
            }
        }

        protected void btnCambiarContrasenia_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPassword.Text) && !string.IsNullOrEmpty(txtPasswordConfirm.Text))
            {
                if (txtPassword.Text == txtPasswordConfirm.Text)
                {
                    if (LogicaUsuarios.GenerateMD5(txtPassword.Text) != Session["contrseniaUsuario"].ToString())
                    {
                        TBLUSUARIOS usuarioCambiar = (TBLUSUARIOS)Session["usuarioRecuperar"];
                        LogicaUsuarios.cambiarContrasenia(usuarioCambiar, LogicaUsuarios.GenerateMD5(txtPassword.Text));
                        Session["toastTitulo"] = "Recuperación de contraseña exitosa.";
                        Session["toastMensaje"] = "Ingrese nuevamente con su nueva contraseña.";
                        Session["toastClass"] = "success";
                        Response.Redirect("LoginPage.aspx");
                    }
                    else
                    {
                        mostrarToast("Misma contrasenia", "La contraseña nueva no puede ser la misma a la que desea recuperar", "Error", 5000);
                    }
                }
            }
        }

        protected void btnRegresarLogin_Click(object sender, EventArgs e) => Response.Redirect("LoginPage.aspx");

        private void mostrarToast(string titulo, string mensaje, string tipo, int duracion = 3000) => ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mostrarToast", "mostrarToast('" + titulo + "', '" + mensaje + "', '" + tipo + "', " + duracion + ");", true);

        private void cambiarImagen(string filePath)
        {
            FileStream fs = new FileStream(Server.MapPath(filePath), System.IO.FileMode.Open, System.IO.FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            byte[] bytes = br.ReadBytes((Int32)fs.Length);
            string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
            imgAvatar.ImageUrl = "data:image/png;base64," + base64String;
        }
    }
}