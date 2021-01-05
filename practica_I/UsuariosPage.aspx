<%@ Page Title="" Language="C#" MasterPageFile="~/HomeTemplate.Master" AutoEventWireup="true" CodeBehind="UsuariosPage.aspx.cs" Inherits="practica_I.UsuariosPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_head" runat="server">
    <style>
        .imgAvatarUsuario {
            max-width: 120px;
        }

        .cont_datos i {
            color: #6d6d6d;
        }

        .cont_datos_usuario h5, .cont_datos_usuario p {
            display: inline-block;
            margin: 0;
        }

        .cont_datos {
            border: 1px solid #dcdcdc;
            padding: 20px 35px;
            box-shadow: 10px 10px 20px #d6d6d6;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_message" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_content" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="ps-section pt-80 pb-80">
                <div class="ps-container">
                    <div class="ps-section__header mb-50 text-center">
                        <h2 class="ps-section__title" data-mask="Usuarios Registrados">Usuarios Registrados</h2>
                    </div>
                    <div class="ps-section__content">
                        <div class="row">
                            <div class="col-md-8 col-sm-12 col-lg-8 col-xs-12 col-12">
                                <div class="row">
                                    <asp:ListView ID="lvUsuariosConfirmar" runat="server" GroupItemCount="4">
                                        <LayoutTemplate>
                                            <div class="col-12">
                                                <div>
                                                    <asp:DataPager ID="dpTop" runat="server" PageSize="5">
                                                        <Fields>
                                                            <asp:NextPreviousPagerField ButtonCssClass="ps-morelink" ButtonType="Link" />
                                                        </Fields>
                                                    </asp:DataPager>
                                                </div>
                                                <table border="1">
                                                    <asp:PlaceHolder runat="server" ID="groupPlaceHolder" />
                                                </table>
                                                <div>
                                                    <asp:DataPager ID="dtBottom" runat="server" PageSize="5">
                                                        <Fields>
                                                            <asp:NextPreviousPagerField ButtonCssClass="ps-morelink" ButtonType="Link" />
                                                        </Fields>
                                                    </asp:DataPager>
                                                </div>
                                            </div>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <div class="col-md-6 col-sm-12 col-lg-3 col-xs-12 col-12">
                                                <div class="card">
                                                    <div class="card-body text-center">
                                                        <h5 class="card-title">
                                                            <asp:Label ID="lblNomUsuario" runat="server" Text='<%#Eval("NOMBRE_USUARIO") + " " + Eval("APELLIDO_USUARIO") %>'></asp:Label></h5>
                                                        <asp:Image ID="imgAvatarUsu" CssClass="imgAvatarUsuario" Width="100%" ImageUrl='<%# CambiarImagen(Eval("FOTO_PATH_USUARIO").ToString()) %>' runat="server" />
                                                        <asp:LinkButton ID="btnRegVerDatosUsu" CommandArgument='<%#Eval("ID_USUARIO").ToString() %>' CssClass="ps-morelink" runat="server" OnClick="btnRegVerDatosUsu_Click">Ver Cliente <i class="fa fa-long-arrow-right"></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                        <GroupTemplate>
                                            <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
                                        </GroupTemplate>
                                        <LayoutTemplate>
                                            <asp:PlaceHolder runat="server" ID="groupPlaceHolder" />
                                        </LayoutTemplate>
                                        <LayoutTemplate>
                                            <div class="col-12">
                                                <div>
                                                    <asp:DataPager ID="dpTop" runat="server" PageSize="5">
                                                        <Fields>
                                                            <asp:NextPreviousPagerField ButtonCssClass="ps-morelink" ButtonType="Link" />
                                                        </Fields>
                                                    </asp:DataPager>
                                                </div>
                                                <table border="1">
                                                    <asp:PlaceHolder runat="server" ID="groupPlaceHolder" />
                                                </table>
                                                <div>
                                                    <asp:DataPager ID="dtBottom" runat="server" PageSize="5">
                                                        <Fields>
                                                            <asp:NextPreviousPagerField ButtonCssClass="ps-morelink" ButtonType="Link" />
                                                        </Fields>
                                                    </asp:DataPager>
                                                </div>
                                            </div>
                                        </LayoutTemplate>
                                    </asp:ListView>
                                </div>
                            </div>
                            <div class="col-md-4 col-sm-12 col-lg-4 col-xs-12 col-12 cont_datos">
                                <div class="ps-section__header mb-20">
                                    <h3 data-mask="DatosUsuario">Datos Usuario</h3>
                                </div>
                                <div class="cont_limpiar mb-20">
                                    <asp:LinkButton ID="btnLimpiar" CssClass="ps-morelink" OnClick="btnLimpiar_Click" runat="server">Limpiar <i class="fa fa-eraser"></i></asp:LinkButton>
                                </div>
                                <div class="mb-10 cont_datos_usuario">
                                    <h5><i class="fa fa-user mr-10"></i>Nombre: </h5>
                                    <p>
                                        <asp:Label ID="lblNombreConf" runat="server" Text=""></asp:Label>
                                    </p>
                                </div>
                                <div class="mb-10 cont_datos_usuario">
                                    <h5><i class="fa fa-smile-o mr-10"></i>Usuario: </h5>
                                    <p>
                                        <asp:Label ID="lblUsuarioConf" runat="server" Text=""></asp:Label>
                                    </p>
                                </div>
                                <div class="mb-10 cont_datos_usuario">
                                    <h5><i class="fa fa-at mr-10"></i>Email: </h5>
                                    <p>
                                        <asp:Label ID="lblEmailConf" runat="server" Text=""></asp:Label>
                                    </p>
                                </div>
                                <div class="mb-10 cont_datos_usuario">
                                    <h5><i class="fa fa-calendar mr-10"></i>Fecha de Creación: </h5>
                                    <p>
                                        <asp:Label ID="lblFechaConf" runat="server" Text=""></asp:Label>
                                    </p>
                                </div>
                                <div class="mb-10 cont_datos_usuario">
                                    <h5><i class="fa fa-phone mr-10"></i>Teléfono: </h5>
                                    <p>
                                        <asp:Label ID="lblTelefonoConf" runat="server" Text=""></asp:Label>
                                    </p>
                                </div>
                                <div class="mb-10 cont_datos_usuario">
                                    <h5><i class="fa fa-check mr-10"></i>Estado: </h5>
                                    <p>
                                        <%if (Session["estadoUsuarioConf"] != null)
                                            {
                                                char usuClass = (char)Session["estadoUsuarioConf"];
                                                switch (usuClass)
                                                {
                                                    case 'A':
                                        %>
                                        <p><span class="badge bg-primary">Activo</span></p>
                                        <%
                                                break;
                                            case 'I':
                                        %>
                                        <p><span class="badge bg-danger">Inactivo</span></p>
                                        <%
                                                break;
                                            case 'B':
                                        %>
                                        <p><span class="badge bg-danger">Bloqueado</span></p>
                                        <%
                                                break;
                                            case 'C':
                                        %>
                                        <p><span class="badge bg-warning text-dark">Confirmar Registro</span></p>
                                        <%
                                                break;
                                            case 'R':
                                        %>
                                        <p><span class="badge bg-info text-dark">Recuperando Contraseña</span></p>
                                        <%
                                                break;
                                            default:
                                        %>
                                        <p><span class="badge bg-dark">Sin Estado</span></p>
                                        <%
                                                    break;
                                                }
                                            } %>
                                        <%--<asp:Label ID="lblEstadoConf" runat="server" Text=""></asp:Label>--%>
                                    </p>
                                </div>
                                <div class="form-group text-center mt-30">
                                    <asp:LinkButton ID="btnConfirmarRegistro" CssClass="ps-btn" runat="server">Confirmar Registro <i class="fa fa-angle-right"></i></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cph_scripts" runat="server">
</asp:Content>
