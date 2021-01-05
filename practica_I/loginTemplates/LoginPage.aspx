<%@ Page Async="true" Title="" Language="C#" MasterPageFile="~/loginTemplates/TemplateMaster.Master" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="practica_I.loginTemplates.LoginPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
    <link href="../fonts/material-icon/css/material-design-iconic-font.min.css" rel="stylesheet" />
    <style>
        .toggle-password {
            position: absolute;
            right: 15px;
            top: 16px;
            color: #0da2eb;
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
                <%
                    if (Session["toastTitulo"] != null)
                    {
                %>
                <div class="bd-example">
                    <div class="alert alert-<%=Session["toastClass"]%> alert-dismissible fade show" role="alert">
                        <strong><%=Session["toastTitulo"]%></strong> <%=Session["toastMensaje"]%>
                        <button type="button" class="btn-close" name="btnCerrar" data-bs-dismiss="alert" aria-label="Close" onclick="cerarAlert()"><i class="fa fa-close"></i></button>
                    </div>
                </div>
                <%
                        Session.Clear();
                    }
                %>
                <div class="login100-form-avatar">
                    <asp:Image ID="imgAvatar" ImageUrl="~/images/avatar-01.jpg" runat="server" />
                </div>
                <span class="login100-form-title p-t-20 p-b-45">Login Probando
                </span>
                <asp:Panel ID="pnRecuperar" runat="server">
                    <p style="line-height: 1.1; margin-bottom: 20px; color: white;"><small>Ingrese el email que utilizo al momento del registro de su usuario</small></p>
                    <div class="wrap-input100 validate-input m-b-10" data-validate="Username is required">
                        <asp:TextBox ID="txtEmail" CssClass="input100" TextMode="Email" placeholder="email@mail.com" runat="server"></asp:TextBox>
                        <span class="focus-input100"></span>
                        <span class="symbol-input100">
                            <i class="fa fa-at"></i>
                        </span>
                    </div>
                    <p style="line-height: 1.1; margin-bottom: 20px;">
                        <asp:RegularExpressionValidator ID="reEmail" CssClass="badge bg-warning text-dark badge-mensajes" Display="Dynamic" runat="server" ErrorMessage="Debe ingresar un email válido" ControlToValidate="txtEmail" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"></asp:RegularExpressionValidator>
                    </p>
                    <div class="container-login100-form-btn p-t-10">

                        <asp:Button ID="btnRecuperarContrasenia" runat="server" CssClass="login100-form-btn" Text="Recuperar" OnClick="btnRecuperarContrasenia_Click" />
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnContrasenia" runat="server">
                    <div class="wrap-input100 validate-input m-b-10" data-validate="Username is required">
                        <asp:TextBox ID="txtUsername" CssClass="input100" placeholder="Usuario..." runat="server"></asp:TextBox>
                        <span class="focus-input100"></span>
                        <span class="symbol-input100">
                            <i class="fa fa-user"></i>
                        </span>
                    </div>
                    <div class="wrap-input100 validate-input m-b-10" data-validate="Password is required">
                        <span toggle="#cph_content_txtPassword" class="zmdi zmdi-eye field-icon toggle-password" style="cursor: pointer;"></span>
                        <asp:TextBox ID="txtPassword" CssClass="input100" placeholder="Password..." runat="server" TextMode="Password"></asp:TextBox>
                        <span class="focus-input100"></span>
                        <span class="symbol-input100">
                            <i class="fa fa-lock"></i>
                        </span>
                    </div>
                    <div class="container-login100-form-btn p-t-10">
                        <asp:Button ID="btnLogin" runat="server" CssClass="login100-form-btn" Text="Login" OnClick="btnLogin_Click" />
                    </div>
                </asp:Panel>
                <div class="text-center w-full p-t-25 p-b-20">
                    <asp:LinkButton ID="btnOlvideContrasenia" CssClass="txt1" runat="server" OnClick="btnOlvideContrasenia_Click">Olvide mi contraseña / usuario</asp:LinkButton>
                </div>
                <div class="text-center w-full p-b-20">
                    <asp:LinkButton ID="btnRegistrarse" CssClass="txt1" runat="server" OnClick="btnRegistrarse_Click">Crear una cuenta <i class="fa fa-arrow-circle-right"></i></asp:LinkButton>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cph_scripts" runat="server">
    <script type="text/javascript">
        function cerarAlert() {
            var alertNode = $(".alert.alert-dismissible");
            alertNode.remove();
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
