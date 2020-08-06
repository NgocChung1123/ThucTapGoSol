<%@ Page Title="" Language="C#" MasterPageFile="~/Frontend.Master" AutoEventWireup="true" CodeBehind="LoaiTinTucDetailDemo.aspx.cs" Inherits="Com.Gosol.CMS.Web.Webapp.Frontend.LoaiTinTucDetailDemo" %>

<%@ Register Src="~/Webapp/Frontend/SideBarDanhMucTinTuc.ascx" TagPrefix="uc1" TagName="SideBarDanhMucTinTuc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="/AdminLte/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="/AdminLte/dist/css/AdminLTE.min.css" rel="stylesheet" />
    <link href="../../AdminLte/dist/css/cssTinTuc.css" rel="stylesheet" />
    <div class="col-lg-9">
        <div class="row">
            <div class="box box-primary">
                <div class="box-header" style="font-family: 'Varela Round Regular', 'Varela Round'; font-size: 20px; padding-left: 40px; padding-bottom: 0px;">
                    <asp:Label ID="lblTenLoaiTin" runat="server"></asp:Label>
                </div>
                <div class="box-body">
                    <asp:Repeater runat="server" ID="rptLoaiTinTuc" OnItemDataBound="rptLoaiTinTuc_ItemDataBound">
                        <ItemTemplate>
                            <div class="list-loaitin">
                                <div class="item-loaitin">
                                    <div class="item-loaitin-children col-lg-4 col-md-4 col-sm-4 col-sm-4">
                                        <asp:HyperLink runat="server" ID="hplImage" ForeColor="White">
                                            <asp:Image runat="server" ID="imgTinTuc" class="tintuc-image-right" />
                                        </asp:HyperLink>
                                    </div>
                                    <div class="item-loaitin-children item-loaitin-children-text col-lg-8 col-md-8 col-sm-8 col-sm-12">
                                        <p class="item-tintuc-title-mang-tin">
                                            <span>
                                                <asp:HyperLink runat="server" ID="hylTin" Style="font-size: 14px; font-weight: 700; font-family: 'Varela Round Bold', 'Varela Round'; color: #333333 !important;">
                                                    <asp:Label ID="lblTitle" runat="server"></asp:Label>
                                                </asp:HyperLink></h2>
                                            </span>
                                        </p>
                                        <p style="font-size: 12px; color: #999999;">
                                            <asp:Label ID="lblNgayTao" runat="server" CssClass="span-date"></asp:Label>
                                            <span>
                                                <asp:HyperLink runat="server" ID="hplXemChiTiet" Style="font-style: italic">
                                                    <span style="font-weight: normal;font-size: 12px;color: #999999;">Xem chi tiết</span>
                                                </asp:HyperLink>
                                            </span>
                                        </p>
                                        <span style="font-family: 'Varela Round Regular', 'Varela Round';">
                                            <asp:Label ID="lblTomTat" runat="server"></asp:Label>
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <div class="clearfix"></div>
                    <div class="paginations" style="margin-top: 15px; margin-bottom: 15px">
                        <asp:PlaceHolder ID="plhPaging" runat="server"></asp:PlaceHolder>
                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>
    <div class="col-lg-3">
        <uc1:SideBarDanhMucTinTuc runat="server" ID="SideBarDanhMucTinTuc" />
    </div>
</asp:Content>
