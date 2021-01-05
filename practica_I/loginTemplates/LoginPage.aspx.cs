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
    public partial class LoginPage : System.Web.UI.Page
    {
        int contador = 1;
        string intentos = "";
        string pathDefaultAvatar = "~/images/avatar-01.jpg";
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["count"] = Session["countOld"];
            if (!IsPostBack)
            {
                Session["nomUsuario"] = null;
                Session["tipoUsuario"] = null;
                Session["estadoUsuario"] = null;
                Session["passUsuario"] = null;
                Session["usuUsuario"] = null;
                Session["cambiarOlvideContrasenia"] = true;
                Limpiar();
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e) => ingresar();

        private void ingresar()
        {
            if (string.IsNullOrEmpty(txtUsername.Text))
            {
                mostrarToast("Advertencia", "No deje vácio el nombre de usuario", "Warning");
                return;
            }
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                mostrarToast("Advertencia", "No deje vácio el campo de contraseña", "Warning");
                return;
            }

            bool existeNombre = LogicaUsuarios.autenticaNombreUsuario(txtUsername.Text);
            bool existeUsuario = LogicaUsuarios.autenticar(txtUsername.Text, LogicaUsuarios.GenerateMD5(txtPassword.Text));

            TBLUSUARIOS usuarioExistente = new TBLUSUARIOS();

            if (existeNombre)
            {
                usuarioExistente = LogicaUsuarios.buscarXUsuario(txtUsername.Text);

                if (!string.IsNullOrEmpty(usuarioExistente.FOTO_PATH_USUARIO))
                {
                    string filePath = usuarioExistente.FOTO_PATH_USUARIO;
                    cambiarImagen(filePath);
                }
                else
                {
                    cambiarImagen(pathDefaultAvatar);
                }

                comprobarEstadoUsuario(usuarioExistente);

                if (existeUsuario)
                {
                    TBLUSUARIOS usuario = new TBLUSUARIOS();
                    try
                    {
                        usuario = LogicaUsuarios.autenticarXLogin(txtUsername.Text, LogicaUsuarios.GenerateMD5(txtPassword.Text));

                    }
                    catch (Exception ex)
                    {
                        mostrarToast("Error", "Error: " + ex.Message, "Error");
                        throw;
                    }
                    int rolUsuario = usuario.TBLROLE_ID;
                    char estadoUsuario = usuario.ESTADO_USUARIO;


                    Session["nomUsuario"] = usuario.NOMBRE_USUARIO.ToString();
                    Session["estadoUsuario"] = usuario.ESTADO_USUARIO.ToString();
                    Session["passUsuario"] = usuario.PASS_USUARIO.ToString();
                    Session["usuUsuario"] = usuario.USU_USUARIO.ToString();

                    if (estadoUsuario == 'A')
                    {
                        switch (rolUsuario)
                        {
                            case 1:
                                //mostrarToast("Administrador", "Este es administrador", "Success", 1000);
                                Session["tipoUsuario"] = "Administrador";
                                Response.Redirect("~/HomePage.aspx");
                                break;
                            case 2:
                                //mostrarToast("Usuario", "Este es usuario", "Success", 1000);
                                Session["tipoUsuario"] = "Usuario";
                                Response.Redirect("~/HomePage.aspx");
                                break;
                            default:
                                mostrarToast("Error", "No tiene - error", "Error", 1000);
                                break;
                        }
                    }
                }
                else
                {
                    intentos = (contador + (Convert.ToInt32(Session["count"]))).ToString();
                    Session["countOld"] = intentos.ToString();

                    if (Convert.ToInt32(intentos) >= 3)
                    {
                        try
                        {
                            LogicaUsuarios.bloquearUsu(usuarioExistente);
                            mostrarToast("Máximo de intentos", "Su usuario ha sido bloqueado por máximo de intentos permitidos (3). Consulte con el administrador.", "Error", 4000);
                            Session.Clear();
                            btnLogin.Visible = false;
                        }
                        catch (Exception ex)
                        {
                            mostrarToast("Error", "Error al bloquear usuario: " + ex.Message, "Error", 6000);
                        }
                    }
                    else
                    {
                        mostrarToast("Usuario o contraseña incorrectos", "Intentos restantes: " + intentos, "Warning", 2000);
                    }
                }
            }
            else
            {
                mostrarToast("El usuario no existe", "Usuario Inexistente", "Warning", 1000);
                cambiarImagen(pathDefaultAvatar);
            }

        }

        private void comprobarEstadoUsuario(TBLUSUARIOS usuarioExistente)
        {
            char estadoUsuPrincipal = usuarioExistente.ESTADO_USUARIO;
            if (!string.IsNullOrEmpty(estadoUsuPrincipal.ToString()))
            {
                switch (estadoUsuPrincipal)
                {
                    case 'B':
                        mostrarToast("Usuario Bloqueado", "Su usuario esta bloqueado. Consulte con el administrador para desbloquear", "Error", 5000);
                        break;
                    case 'I':
                        mostrarToast("Usuario Inactivo", "Su usuario esta Inactivo. Consulte con el administrador para desbloquear", "Error", 5000);
                        break;
                    case 'C':
                        mostrarToast("Por confirmar registro", "Su usuario esta por confirmar su registro. El administrador le enviará un correo al momento de aprobar su registro", "Warning", 6000);
                        break;
                    case 'R':
                        //mostrarToast("Recuperar contraseña", "Aqui reenvio a la pagina para recuperar contraseña", "Warning");
                        Response.Redirect("RecuperarPage.aspx");
                        break;
                    case 'A':
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "console", "console.log('Administrador');", true);
                        break;
                    default:
                        mostrarToast("Estado desconocido", "Sin estado", "Warning");
                        return;
                }
            }
        }

        private void cambiarImagen(string filePath)
        {
            FileStream fs = new FileStream(Server.MapPath(filePath), System.IO.FileMode.Open, System.IO.FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            byte[] bytes = br.ReadBytes((Int32)fs.Length);
            string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
            imgAvatar.ImageUrl = "data:image/png;base64," + base64String;
        }

        private void mostrarToast(string titulo, string mensaje, string tipo, int duracion = 3000) => ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "mostrarToast", "mostrarToast('" + titulo + "', '" + mensaje + "', '" + tipo + "', " + duracion + ");", true);


        protected void btnRegistrarse_Click(object sender, EventArgs e) => Response.Redirect("RegisterPage.aspx");

        protected async void btnRecuperarContrasenia_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEmail.Text))
            {
                TBLUSUARIOS usuXCorreo = LogicaUsuarios.buscarCorreoExistente(txtEmail.Text);

                bool existeCorreo = !usuXCorreo.EMAIL_USUARIO.Equals("");

                if (existeCorreo)
                {
                    string codigo = GenerateNewRandom();

                    LogicaCodigos.guardarCodigo(usuXCorreo.ID_USUARIO, codigo);
                    LogicaUsuarios.cambiarEstadoRecuperacion(usuXCorreo);

                    string subject = "Recuperación de contraseña";
                    string htmlString = @"
                          <html>
	                          <body>
	                              <h3>Estimad@ " + usuXCorreo.NOMBRE_USUARIO + @"</h3>
                                  <p><i>Pasos para la recuperación de contraseña</i></p>
                                  <p>Tu usuario es: <b style='font-size: 20px;'>" + usuXCorreo.USU_USUARIO + @"</b></p>
	                              <p>Ingresa el siguiente codigo: </p>
                                  </br>
                                  <code style='font-size: 20px;'>" + codigo.ToString() + @"</code >
                                  </br>
	                              <p>El código deberás ingresarlo en el siguiente enlace: </p>
                                  </br>
                                  <a href = 'https://localhost:44305/loginTemplates/RecuperarPage.aspx' target = '_blank'>https://localhost:44305/loginTemplates/RecuperarPage.aspx</a>
                                  </br>                                  
                                  <p>En caso de no poder ingresar a la direcion, ingrese al Login del sistema e ingrese su usuario y cualquier contraseña. Le redireccionará automaticamente a la recuperacion de contraseña.</p>
	                          </body>
	                      </html>";

                    EmailProvider ep = new EmailProvider(subject, htmlString, txtEmail.Text);

                    await ep.SendEmailAsync();
                    mostrarToast("Correo Enviado", "El correo ha sido enviado a: " + txtEmail.Text + ". Revise su correo y siga las instrucciones para recuperar sus credenciales.", "Success", 6000);
                    Limpiar();
                }
                else
                {
                    mostrarToast("Correo Inexistente", "El correo provisto no está registrado", "Error");
                }
            }
            else
            {
                mostrarToast("Error", "Esta vacio", "Error");

            }
        }

        private void Limpiar()
        {
            txtEmail.Text = "";
            txtUsername.Text = "";
            txtPassword.Text = "";
            btnOlvideContrasenia.Text = "Olvide mi contraseña / usuario";
            pnRecuperar.Visible = false;
            pnContrasenia.Visible = true;
            cambiarImagen(pathDefaultAvatar);
        }

        private static string GenerateNewRandom()
        {
            Random generator = new Random();
            String r = generator.Next(0, 1000000).ToString("D6");
            if (r.Distinct().Count() == 1)
            {
                r = GenerateNewRandom();
            }
            return r;
        }

        protected void btnOlvideContrasenia_Click(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(Session["cambiarOlvideContrasenia"]))
            {
                btnOlvideContrasenia.Text = "Iniciar sesion";
                pnRecuperar.Visible = true;
                pnContrasenia.Visible = false;
                Session["cambiarOlvideContrasenia"] = false;
                cambiarImagen(pathDefaultAvatar);
            }
            else
            {
                btnOlvideContrasenia.Text = "Olvide mi contraseña / usuario";
                pnRecuperar.Visible = false;
                pnContrasenia.Visible = true;
                Session["cambiarOlvideContrasenia"] = true;
            }
        }
    }
}