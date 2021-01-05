<%@ Page Title="" Language="C#" MasterPageFile="~/loginTemplates/TemplateMaster.Master" AutoEventWireup="true" CodeBehind="RegisterPage.aspx.cs" Inherits="practica_I.loginTemplates.RegisterPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
    <link href="../fonts/material-icon/css/material-design-iconic-font.min.css" rel="stylesheet" />
    <style>
        .columna_foto img {
            max-width: 150px;
            border-radius: 50%;
            border: 1px solid #808080;
        }

        #contenedorCargarImagen > div:first-child input {
            padding-top: 12px;
            cursor: pointer;
        }

        .container p small {
            color: white;
            font-style: italic;
        }

        .container input[type="text"], .container input[type="password"] {
            color: #7d7d7d;
        }

        .badge-mensajes {
            white-space: break-spaces;
        }

        .toggle-password {
            position: absolute;
            right: 15px;
            top: 16px;
            color: #0da2eb;
        }

        @media screen and (min-width: 769px) {
            #contenedorCargarImagen > div:first-child {
                padding-right: 0;
            }

            #contenedorCargarImagen > div:last-child {
                padding-left: 0;
            }

            #contenedorCargarImagen > div:first-child input {
                border-radius: 25px 0 0 25px;
            }

            #contenedorCargarImagen > div:last-child input {
                border-radius: 0 25px 25px 0;
            }
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_message" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_content" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container">
                <div class="row">
                    <div class="col-12">
                        <span class="login100-form-title p-t-20 p-b-45">Registro</span>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-8 col-md-8 col-sm-12 col-12">
                        <div class="row">
                            <div class="col-lg-6 col-md-6 col-sm-12 col-12 text-center columna_foto">
                                <span class="login100-form-title p-b-10">Foto</span>
                                <asp:Image ID="imgAvatar" ImageUrl="~/images/avatar-01.jpg" Width="100%" CssClass="m-b-20" runat="server" />
                                <p style="line-height: 1.1;"><small><b>Tamaño recomendado:</b> 200px x 200px</small></p>
                                <p style="line-height: 1.1; margin-bottom: 20px;"><small><b>Formato recomendado:</b> jpg, png</small></p>
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-12 col-12 text-center">
                                <div class="wrap-input100 validate-input m-b-20" data-validate="Nombre es requerido">
                                    <asp:TextBox ID="txtNombre" CssClass="input100" placeholder="Nombre" runat="server"></asp:TextBox>
                                    <span class="focus-input100"></span>
                                    <span class="symbol-input100">
                                        <i class="fa fa-user-circle"></i>
                                    </span>
                                </div>
                                <div class="wrap-input100 validate-input m-b-20" data-validate="Apellido es requerido">
                                    <asp:TextBox ID="txtApellido" CssClass="input100" placeholder="Apellido" runat="server"></asp:TextBox>
                                    <span class="focus-input100"></span>
                                    <span class="symbol-input100">
                                        <i class="fa fa-user-circle-o"></i>
                                    </span>
                                </div>
                                <p style="line-height: 1.1; margin-bottom: 20px;"><small>El email provisto, servirá para que el Administrador le envíe correos de confirmación o para recuperación de cuenta</small></p>
                                <div class="wrap-input100 validate-input m-b-20" data-validate="Email es requerido">
                                    <asp:TextBox ID="txtEmail" CssClass="input100" AutoPostBack="true" OnTextChanged="txtEmail_TextChanged" TextMode="Email" placeholder="email@email.com" runat="server"></asp:TextBox>
                                    <span class="focus-input100"></span>
                                    <span class="symbol-input100">
                                        <i class="fa fa-at"></i>
                                    </span>
                                </div>
                                <p style="line-height: 1.1; margin-bottom: 20px;">
                                    <asp:RegularExpressionValidator ID="reEmail" CssClass="badge bg-warning text-dark badge-mensajes" Display="Dynamic" runat="server" ErrorMessage="Debe ingresar un email válido" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" ControlToValidate="txtEmail"></asp:RegularExpressionValidator>
                                </p>
                            </div>
                        </div>
                        <div id="contenedorCargarImagen" class="row">
                            <div class="col-lg-8 col-md-8 col-sm-12 col-12">
                                <div class="wrap-input100 validate-input m-b-20" data-validate="Apellido es requerido">
                                    <asp:FileUpload ID="fuCargarImagen" CssClass="input100" accept=".png,.jpg,.jpeg" runat="server" />
                                    <span class="focus-input100"></span>
                                    <span class="symbol-input100">
                                        <i class="fa fa-image"></i>
                                    </span>
                                </div>
                            </div>
                            <div class="col-lg-4 col-md-4 col-sm-12 col-12">
                                <div class="container-login100-form-btn">
                                    <asp:Button ID="btnMostrarImagen" runat="server" CssClass="login100-form-btn" OnClick="btnMostrarImagen_Click" Text="Cargar imagen" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-12 col-12 text-center colContrasenia">
                        <div class="wrap-input100 validate-input m-b-20" data-validate="Username is required">
                            <asp:TextBox ID="txtUsername" CssClass="input100" AutoPostBack="true" OnTextChanged="txtUsername_TextChanged" placeholder="Usuario" runat="server"></asp:TextBox>
                            <span class="focus-input100"></span>
                            <span class="symbol-input100">
                                <i class="fa fa-user"></i>
                            </span>
                        </div>
                        <%--<p style="line-height: 1.1; margin-bottom: 20px;" id="txtCondicionContraseña"><small>La contraseña debe cumplir los siguientes parámetros: 1 mayúscula, 1 signo, mínimo 8 caracteres</small></p>--%>
                        <p style="line-height: 1.1; margin-bottom: 20px;" id="txtCondicionContraseña">
                            <asp:RegularExpressionValidator runat="server" CssClass="badge bg-warning text-dark badge-mensajes" ErrorMessage="La contraseña debe cumplir los siguientes parámetros: 1 mayúscula, 1 signo, mínimo 8 - máximo 15 caracteres" Display="Dynamic" ValidationExpression="(?=^.{8,15}$)(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*()_+}{&quot;:;'?/>.<,])(?!.*\s).*$" ControlToValidate="txtPassword"></asp:RegularExpressionValidator>
                        </p>
                        <div class="wrap-input100 validate-input" data-validate="Password is required">
                            <span toggle="#cph_content_txtPassword" class="zmdi zmdi-eye field-icon toggle-password" style="cursor: pointer;"></span>
                            <asp:TextBox ID="txtPassword" CssClass="input100 txtPassword1" placeholder="Contraseña" runat="server" TextMode="Password" name="txtPassword">
                            </asp:TextBox>
                            <span class="focus-input100"></span>
                            <span class="symbol-input100">
                                <i class="fa fa-lock"></i>
                            </span>
                        </div>
                        <p style="line-height: 1.1; margin: 10px 0 10px 0;">
                            <asp:CompareValidator ID="cmpContrasenias" CssClass="badge bg-danger badge-mensajes" ControlToCompare="txtPasswordConfirm" ControlToValidate="txtPassword" runat="server" ErrorMessage="Las contraseñas no coinciden" Type="String" Operator="Equal"></asp:CompareValidator>
                        </p>
                        <div class="wrap-input100 validate-input m-b-20" data-validate="Password is required">
                            <asp:TextBox ID="txtPasswordConfirm" CssClass="input100 txtPassword2" placeholder="Confirmar Contraseña" runat="server" TextMode="Password" name="txtPasswordConfirm" />
                            <span class="focus-input100"></span>
                            <span class="symbol-input100">
                                <i class="fa fa-lock"></i>
                            </span>
                        </div>
                        <div class="wrap-input100 validate-input m-b-20" data-validate="Telefono es requerido">
                            <asp:TextBox ID="txtTelefono" CssClass="input100" placeholder="022222222" runat="server" />
                            <span class="focus-input100"></span>
                            <span class="symbol-input100">
                                <i class="fa fa-phone-square"></i>
                            </span>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12 col-sm-12 col-md-3 col-lg-3"></div>
                    <div class="col-12 col-sm-12 col-md-6 col-lg-6">
                        <div class="container-login100-form-btn p-t-10">
                            <asp:Button ID="btnregistrar" runat="server" CssClass="login100-form-btn" Text="Registrarme" OnClick="btnregistrar_Click" />
                        </div>
                        <div class="text-center w-full p-t-25 p-b-20">
                            <asp:LinkButton ID="btnRegresarLogin" CssClass="txt1" runat="server" OnClick="btnRegresarLogin_Click">Ya dispongo de una cuenta <i class="fa fa-arrow-circle-right"></i></asp:LinkButton>
                        </div>
                    </div>
                    <div class="col-12 col-sm-12 col-md-3 col-lg-3"></div>
                </div>
            </div>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnMostrarImagen" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cph_scripts" runat="server">
    <script type="text/javascript">
        //$(document).ready(function () {
        //    var psw1 = $(".colContrasenia .txtPassword1");
        //    var psw2 = $(".colContrasenia .txtPassword1");
        //    psw1.keypress(comprobarContrasenia());
        //    psw2.keypress(comprobarContrasenia());
        //});

        //function comprobarContrasenia() {
        //    var psw1 = $(".colContrasenia .txtPassword1");
        //    var psw2 = $(".colContrasenia .txtPassword1");
        //    console.log("tipeando");
        //    if (psw1.val != '' && psw2.val != '') {
        //        if (psw1.val != psw2.val) {
        //            psw1.css("border", "1px solid red");
        //            psw2.css("border", "1px solid red");
        //            console.log("no coinciden");
        //        } else {
        //            psw1.css("border", "1px solid transparent");
        //            psw2.css("border", "1px solid transparent");
        //        }
        //    }
        //}

        //$(document).ready(function () {
        //    //variables
        //    var pass1 = $('[name=txtPassword]');
        //    var pass2 = $('[name=txtPasswordConfirm]');
        //    var confirmacion = "Las contraseñas si coinciden";
        //    var longitud = "La contraseña debe estar formada entre 8-15 carácteres (ambos inclusive)";
        //    var negacion = "No coinciden las contraseñas";
        //    var vacio = "La contraseña no puede estar vacía";
        //    //oculto por defecto el elemento span
        //    var span = $('<span></span>').insertAfter(pass2);
        //    span.hide();
        //    //función que comprueba las dos contraseñas
        //    function coincidePassword() {
        //        var valor1 = pass1.val();
        //        var valor2 = pass2.val();
        //        //muestro el span
        //        span.show().removeClass();
        //        //condiciones dentro de la función
        //        if (valor1 != valor2) {
        //            span.text(negacion).addClass('negacion');
        //        }
        //        if (valor1.length == 0 || valor1 == "") {
        //            span.text(vacio).addClass('negacion');
        //        }
        //        if (valor1.length < 8 || valor1.length > 15) {
        //            span.text(longitud).addClass('negacion');
        //        }
        //        if (valor1.length != 0 && valor1 == valor2) {
        //            span.text(confirmacion).removeClass("negacion").addClass('confirmacion');
        //        }
        //    }
        //    //ejecuto la función al soltar la tecla
        //    pass2.keyup(function () {
        //        coincidePassword();
        //    });
        //});

        function ValidNum(e) {
            var tecla = document.all ? tecla = e.keyCode : tecla = e.which;
            if (tecla > 47 && tecla < 58 || tecla == 46) {
                alert("Solo Letras")
                return false
            } else {
                return true
            }
        }
        function Validletra(e) {
            var tecla = document.all ? tecla = e.keyCode : tecla = e.which;
            if (tecla > 64 && tecla < 91 || tecla > 96 && tecla < 123) {
                //alert("Solo Numeros")
                return false
            } else {
                return true
            }
        }

        (function ($) {

            $(".toggle-password").click(function () {

                $(this).toggleClass("zmdi-eye zmdi-eye-off");
                var input = $($(this).attr("toggle"));
                if (input.attr("type") == "password") {
                    input.attr("type", "text");
                } else {
                    input.attr("type", "password");
                }
            });

        })(jQuery);
    </script>
</asp:Content>
