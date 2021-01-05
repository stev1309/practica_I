<%@ Page Title="" Language="C#" MasterPageFile="~/loginTemplates/TemplateMaster.Master" AutoEventWireup="true" CodeBehind="RecuperarPage.aspx.cs" Inherits="practica_I.loginTemplates.RecuperarPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
    <link href="../fonts/material-icon/css/material-design-iconic-font.min.css" rel="stylesheet" />
    <style>
        .toggle-password {
            position: absolute;
            right: 15px;
            top: 16px;
            color: #0da2eb;
        }

        p {
            color: white;
            text-align: center;
        }

        .txtCondicionContraseña span {
            word-break: break-all;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_message" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_content" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="wrap-login100 p-t-150 p-b-30">
                <div class="login100-form-avatar">
                    <asp:Image ID="imgAvatar" ImageUrl="~/images/avatar-01.jpg" runat="server" />
                </div>
                <span class="login100-form-title p-t-20 p-b-45">Recuperar Contrasenia
                </span>
                <asp:Panel ID="pnConfirmarCorreo" runat="server">
                    <p style="line-height: 1.1; margin-bottom: 20px;"><small>Ingrese el email que utilizo al momento del registro de su usuario</small></p>
                    <div class="wrap-input100 validate-input m-b-10" data-validate="Email es requerido">
                        <asp:TextBox ID="txtEmail" CssClass="input100" TextMode="Email" placeholder="email@mail.com" runat="server"></asp:TextBox>
                        <span class="focus-input100"></span>
                        <span class="symbol-input100">
                            <i class="fa fa-at"></i>
                        </span>
                    </div>
                    <p style="line-height: 1.1; margin-bottom: 20px;">
                        <asp:RegularExpressionValidator ID="reEmail" CssClass="badge bg-warning text-dark badge-mensajes" Display="Dynamic" runat="server" ErrorMessage="Debe ingresar un email válido" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" ControlToValidate="txtEmail"></asp:RegularExpressionValidator>
                    </p>
                    <p style="line-height: 1.1; margin-bottom: 20px;"><small>Ingrese el código que recibió en su email</small></p>
                    <div class="wrap-input100 validate-input m-b-10" data-validate="Codigo de confirmación es requerido">
                        <asp:TextBox ID="txtCodigo" CssClass="input100" TextMode="SingleLine" placeholder="123456789" runat="server"></asp:TextBox>
                        <span class="focus-input100"></span>
                        <span class="symbol-input100">
                            <i class="fa fa-lock"></i>
                        </span>
                    </div>
                    <div class="container-login100-form-btn p-t-10">
                        <asp:Button ID="btnConfirmarCodigo" runat="server" CssClass="login100-form-btn" Text="Confirmar" OnClick="btnConfirmarCodigo_Click" />
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnCambiarContrasenia" runat="server">
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
                    <div class="container-login100-form-btn p-t-10">
                        <asp:Button ID="btnCambiarContrasenia" runat="server" CssClass="login100-form-btn" Text="Cambiar Contraseña" OnClick="btnCambiarContrasenia_Click" />
                    </div>
                </asp:Panel>
                <div class="text-center w-full p-b-20 p-t-30">
                    <asp:LinkButton ID="btnRegresarLogin" CssClass="txt1" runat="server" OnClick="btnRegresarLogin_Click">Regresar al Login <i class="fa fa-arrow-circle-right"></i></asp:LinkButton>
                </div>
            </div>
        </ContentTemplate>

    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cph_scripts" runat="server">
    <script type="text/javascript">
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
