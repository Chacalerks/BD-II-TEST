﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="adminClientes.aspx.cs" Inherits="indioSupermercado.adminClientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="bootstrap/js/jquery-3.6.1.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            //$(document).ready(function () {
            //$('.table').DataTable();
            // });
            $(".table").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable();
            // Code that uses other library's $ can follow here.

        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-4">
                <br>
                <br />
            </div>
            <div align="center">

                <div class="col-md-7">
                    <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <div class="col">
                                    <center>
                                        <h4>Lista Clientes</h4>
                                    </center>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col">
                                    <hr>
                                </div>
                            </div>
                            <div class="row">

                                <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                                    ConnectionString="" SelectCommand="exec verClientes"></asp:SqlDataSource>
                                <div class="col">
                                    <asp:ScriptManager ID="ScriptManagerCliente" runat="server">
                                    </asp:ScriptManager>
                                    <asp:UpdatePanel ID="UpdatePanelCliente" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:GridView class="table table-striped table-bordered" ID="GridViewCliente" runat="server"
                                                AutoGenerateColumns="False" DataKeyNames="idCliente" DataSourceID="SqlDataSource1">
                                                <Columns>
                                                    <asp:BoundField DataField="idCliente" HeaderText="ID" ReadOnly="True" SortExpression="clienteID">
                                                        <ControlStyle Font-Bold="True" />
                                                        <ItemStyle Font-Bold="True" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" ReadOnly="True" SortExpression="Nombre"></asp:BoundField>
                                                    <asp:BoundField DataField="Apellido" HeaderText="Apellido" ReadOnly="True" SortExpression="Apellido"></asp:BoundField>
                                                    <%--<asp:BoundField DataField="Ubicacion" HeaderText="Ubicacion" ReadOnly="True" SortExpression="Ubicacion"></asp:BoundField>--%>
                                                </Columns>
                                            </asp:GridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
