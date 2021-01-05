using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Capa_Datos;
using Capa_Logica;

namespace practica_I.loginTemplates
{
    public partial class RegisterPage : System.Web.UI.Page
    {
        string rutaImagenGuardar = "~/images/users/usuarios/";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["imagenOriginal"] = null;
                Session["rutaImagen"] = null;
                txtTelefono.Attributes.Add("onkeypress", "javascript:return Validletra(event);");
            }
        }

        private void mostrarToast(string titulo, string mensaje, string tipo, int duracion = 3000)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mostrarToast", "mostrarToast('" + titulo + "', '" + mensaje + "', '" + tipo + "', " + duracion + ");", true);
        }

        protected void btnMostrarImagen_Click(object sender, EventArgs e)
        {
            if (fuCargarImagen.HasFile)
            {
                Session["imagenOriginal"] = fuCargarImagen.PostedFile;
                Stream fs = fuCargarImagen.PostedFile.InputStream;
                BinaryReader br = new BinaryReader(fs);
                byte[] bytes = br.ReadBytes((Int32)fs.Length);
                string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                imgAvatar.ImageUrl = "data:image/png;base64," + base64String;
            }
            else
            {
                mostrarToast("Advertencia", "Seleccione una imagen para su Avatar", "Warning");
            }
        }

        protected string SaveAvatarLocal(string nomUsuario)
        {
            string regresarRuta = "";
            try
            {
                HttpPostedFile postedFile = (HttpPostedFile)Session["imagenOriginal"];
                //Session["rutaImagen"] = Server.MapPath(rutaImagenGuardar) + Path.GetFileName(postedFile.FileName);

                //Session["rutaImagen"] = rutaImagenGuardar + Path.GetFileName(postedFile.FileName);
                //Session["rutaImagen"] = rutaImagenGuardar + existeImagen(Path.GetFileName(postedFile.FileName));
                string rut = Path.GetExtension(postedFile.FileName);
                regresarRuta = rutaImagenGuardar + nomUsuario + "_imagen" + rut;
                string rutaGuardar = Server.MapPath(rutaImagenGuardar) + nomUsuario + "_imagen" + rut;
                postedFile.SaveAs(rutaGuardar);
                //Response.Redirect(Request.Url.AbsoluteUri);
                //mostrarToast("Imagen Guardada", "Imagen Guardada", "Info", 4000);
            }
            catch (Exception ex)
            {
                mostrarToast("Error", "Error al guardar la imagen: " + ex.Message, "Error", 8000);
            }
            return regresarRuta;
        }

        protected void btnRegresarLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("LoginPage.aspx");
        }

        protected void btnregistrar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text.Trim();
            string apellido = txtApellido.Text.Trim();
            string email = txtEmail.Text.Trim();
            string usuario = txtUsername.Text.Trim();
            string contrasenia = txtPassword.Text.Trim();
            string telefono = txtTelefono.Text.Trim();
            string pathImagen = "";

            ComprobarEmail();
            ComprobarUsuario();

            if (!string.IsNullOrEmpty(nombre) && !string.IsNullOrEmpty(apellido) && !string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(usuario) && !string.IsNullOrEmpty(contrasenia) && !string.IsNullOrEmpty(telefono))
            {

                pathImagen = SaveAvatarLocal(usuario);
                if (!string.IsNullOrEmpty(pathImagen))
                {
                    try
                    {
                        TBLUSUARIOS usu = new TBLUSUARIOS();
                        usu.NOMBRE_USUARIO = nombre;
                        usu.APELLIDO_USUARIO = apellido;
                        usu.EMAIL_USUARIO = email;
                        usu.USU_USUARIO = usuario;
                        usu.PASS_USUARIO = LogicaUsuarios.GenerateMD5(contrasenia);
                        usu.TELEFONO_USUARIO = telefono;
                        usu.FOTO_PATH_USUARIO = pathImagen;

                        LogicaUsuarios.guardarUsuario(usu);
                        Session["toastTitulo"] = "Usuario Registrado";
                        Session["toastMensaje"] = "Sus datos han sido registrados. Espere a que el administrador confirme su registro.";
                        Session["toastClass"] = "success";

                        Response.Redirect("LoginPage.aspx");
                        //mostrarToast("Usuario Registrado", "Sus datos han sido registrados. Espere a que el administrador confirme su registro.", "Success", 6000);
                    }
                    catch (Exception ex)
                    {
                        mostrarToast("Error", "Error al guardar el usuario: " + ex.Message, "Error", 8000);
                        return;
                    }
                }
                else
                {
                    mostrarToast("Imagen", "Presione en el boton 'Cargar Imagen' para mostrar el avatar a cargar. es posible que deba ingresar de nuevo su contraseña", "Warning", 8000);
                }
            }
            else
            {
                mostrarToast("Campos Vacíos", "No deje los campos vacíos", "Error");
            }
        }

        private void comprobarContrasenias()
        {
            if (!String.IsNullOrEmpty(txtPassword.Text) && !String.IsNullOrEmpty(txtPasswordConfirm.Text))
            {
                if (txtPassword.Text != txtPasswordConfirm.Text)
                {
                    txtPassword.Style.Add(HtmlTextWriterStyle.BorderColor, "red");
                    txtPasswordConfirm.Style.Add(HtmlTextWriterStyle.BorderColor, "red");
                }
                else
                {
                    txtPassword.Style.Add(HtmlTextWriterStyle.BorderColor, "transparent");
                    txtPasswordConfirm.Style.Add(HtmlTextWriterStyle.BorderColor, "transparent");
                }
            }
        }

        protected void txtEmail_TextChanged(object sender, EventArgs e)
        {
            ComprobarEmail();
        }

        private void ComprobarEmail()
        {
            if (LogicaUsuarios.correoExistente(txtEmail.Text))
            {
                mostrarToast("Correo Existente", "El correo elegido ya existe. Escoja otro correo.", "Warning");
                //txtEmail.Text = "";
                txtEmail.Style.Add(HtmlTextWriterStyle.BorderColor, "Red");
                txtEmail.Style.Add("box-shadow", "0px 0px 25px 1px #FF0000");
            }
            else
            {
                txtEmail.Style.Add(HtmlTextWriterStyle.BorderColor, "Transparent");
                txtEmail.Style.Add("box-shadow", "none");
            }
        }

        protected void txtUsername_TextChanged(object sender, EventArgs e)
        {
            ComprobarUsuario();
        }

        private void ComprobarUsuario()
        {
            if (LogicaUsuarios.usuarioExistente(txtUsername.Text))
            {
                mostrarToast("Usuario Existente", "El usuario elegido ya existe. Escoja otro nombre de usuario.", "Warning");
                //txtUsername.Text = "";
                txtUsername.Style.Add(HtmlTextWriterStyle.BorderColor, "Red");
                txtUsername.Style.Add("box-shadow", "0px 0px 25px 1px #FF0000");
            }
            else
            {
                txtUsername.Style.Add(HtmlTextWriterStyle.BorderColor, "Transparent");
                txtUsername.Style.Add("box-shadow", "none");
            }
        }
    }
}