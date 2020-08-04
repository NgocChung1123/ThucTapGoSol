<%@ Page Title="" EnableEventValidation="false" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SystemLog.aspx.cs" Inherits="Com.Gosol.CMS.Web.SystemLog" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="IndexContent" runat="server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="script1" runat="server" EnablePartialRendering="true">
    </asp:ScriptManager>
    <form action="" method="post">
        <link id="Link1" rel="stylesheet" type="text/css" media="all" href="styles/cap.css" runat="server" />
        <div class="content-header">
            <h1>Nhật ký hệ thống
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                <li><a href="#">Hệ thống</a></li>
                <li class="active">Nhật ký hệ thống</li>
            </ol>
        </div>

        <div class="content">
            <div class="row">
                <div class="col-xs-12">
                    <div class="box">
                        <div class="box-header">
                            <%--<button type="button" class="btn btn-primary" id="btnThemNhom" onclick="showAddForm(); return false">
                            <span class="glyphicon glyphicon glyphicon-plus-sign" style="margin-right:5px"></span>Thêm nhóm người dùng
                        </button>--%>
                        </div>

                        <div class="box-body table-responsive">
                            <div class="col-lg-12">
                                <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnTimKiem">
                                    <asp:Button ID="btnTimKiem" CssClass="btn btn-default btn-sm" Text="Tìm kiếm" runat="server" Style="float: right; margin-right: 10px; margin-bottom: 10px" CausesValidation="false" OnClick="btnTimKiem_Click" />


                                </asp:Panel>
                                <asp:TextBox ID="txtSearch" CssClass="form-control" placeholder="Nhập nội dung cần tìm kiếm" Style="float: right; margin-right: 10px; margin-bottom: 10px; width: 30%" runat="server" />
                            </div>
                            <table id="table" class="table table-bordered table-hover" style="margin-top: 15px">
                                <thead>
                                    <tr>
                                        <th>STT</th>
                                        <th style="width: 10%;">Tên cán bộ</th>
                                        <th>Thao tác</th>
                                        <th style="text-align: center; width: 10%;">Thời gian </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="rptSystemLog" runat="server" OnItemDataBound="rptSystemLog_ItemDataBound">
                                        <ItemTemplate>
                                            <tr class="<%# Container.ItemIndex % 2 == 0 ? "even" : "odd" %>">
                                                <td style="width: 70px;">
                                                    <asp:Label ID="lblSTT" runat="server" Text=''></asp:Label>
                                                </td>
                                                <td style="text-align: left; width: 100px;">
                                                    <asp:Label ID="lblTenCanBo" runat="server" Text='<%# Eval("TenCanBo") %>'></asp:Label>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:Label ID="lblLogInfo" runat="server" Text='<%# Eval("LogInfo") %>'></asp:Label>
                                                </td>
                                                <td style="text-align: center;">
                                                    <asp:Label ID="lblLogTime" runat="server" Text='<%# Eval("LogTime") %>'></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>

                                    </asp:Repeater>
                                </tbody>
                            </table>
                            <div class="paginations" style="margin-top: 15px">
                                <asp:PlaceHolder ID="plhPaging" runat="server"></asp:PlaceHolder>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
            <!-- end #dashboard -->
        </div>

        <div id="sidebar" class="right">
            <asp:Literal ID="ltrSideMenu" runat="server"></asp:Literal>
        </div>
    </form>
</asp:Content>
