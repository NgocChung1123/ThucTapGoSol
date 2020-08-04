<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SideBar.ascx.cs" Inherits="Com.Gosol.CMS.Web.Webapp.Frontend.SideBar" %>

<%--để suggest--%>
<link href="/AdminLte/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />

<style>
    .abc{
        width:100%;
        height:40px;
        border:1px solid black;
    }
</style>

<div class="panel panel-primary">
    <div class="panel-heading">
        TIN TỨC NỔI BẬT
    </div>
    <div class="panel-body">
        <asp:Repeater ID="rptTinNoiBat" runat="server" OnItemDataBound="rptTinNoiBat_ItemDataBound">
            <ItemTemplate>
                <div class="item-tin-noi-bat" style="width: 100%">
                    <div class="item-tin-noi-bat-children" style="width: 20%">
                        <asp:HyperLink runat="server" ID="hplImage" ForeColor="White">
                            <asp:Image runat="server" ID="imgTinNoiBat_Top4" class="tintuc-image-right" />
                        </asp:HyperLink>
                    </div>
                    <div class="item-tin-noi-bat-children item-tin-noi-bat-children-text" style="width: 80%">
                        <p class="item-tintuc-new-title">
                            <span>
                                <asp:HyperLink runat="server" ID="hylTin">
                                    <asp:Label ID="lblTitle" runat="server"></asp:Label>
                                </asp:HyperLink></h2>
                            </span>
                        </p>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</div>
<div class="panel panel-primary">
    <div class="panel-heading">
        TRA CỨU ĐƠN THƯ
    </div>
    <div class="panel-body">
        <p><a href="TraCuuTrangThaiHoSo.aspx">Trạng thái hồ sơ</a></p>
        <p><a href="TraCuuVBTraLoi.aspx">Văn bản trả lời</a></p>
        <p><a href="TraCuuQDGiaiQuyet.aspx">Quyết định giải quyết</a></p>
    </div>
</div>

<asp:Button ID="btnLichTiepDan" runat="server" PostBackUrl="LichTiepDan.aspx" CssClass="btn btn-lg btn-block" Text="LỊCH TIẾP DÂN"/>
<asp:Button ID="btnTrinhTuThuTuc" runat="server" PostBackUrl="TrinhTuThuTuc.aspx" CssClass="btn btn-lg btn-block" Text="TRÌNH TỰ THỦ TỤC"/>
<asp:Button ID="btnHoiDap" runat="server" PostBackUrl="HoiDap.aspx" CssClass="btn btn-lg btn-block" Text="HỎI ĐÁP"/>