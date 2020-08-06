<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SideBarDanhMucTinTuc.ascx.cs" Inherits="Com.Gosol.CMS.Web.Webapp.Frontend.SideBar2" %>

<link href="/AdminLte/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
<link href="/AdminLte/dist/css/AdminLTE.min.css" rel="stylesheet" />
<%-- <link href="/AdminLte/jquery.formvalidation/css/formValidation.min.css" rel="stylesheet" />--%>


<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.5.0/css/font-awesome.min.css">
<a href="/AdminLte/fonts/fontawesome-webfont.woff2"></a>
<script>
    var today = new Date();
    var thisYear = today.getFullYear();
    $(document).ready(function () {
        renderItemSideBar();

        var urlHS = window.location.origin + '/Webapp/Frontend/TraCuuTrangThaiHoSo.aspx';
        var urlTL = window.location.origin + '/Webapp/Frontend/TraCuuVBTraLoi.aspx';
        var urlQD = window.location.origin + '/Webapp/Frontend/TraCuuQDGiaiQuyet.aspx';

        $("#divTraCuuUrl").append('<p style="border-bottom: 1px solid #cecece; padding: 10px;"><a href="' + urlHS + '">Trạng thái hồ sơ</a></p>'
            + '<p style="border-bottom: 1px solid #cecece; padding: 10px;"><a href="' + urlTL + '">Văn bản trả lời</a></p>'
            + '<p style="border-bottom: 1px solid #cecece; padding: 10px;"><a href="' + urlQD + '">Quyết định giải quyết</a></p>'
        );

        $("#lblNam").html(`SỐ LIỆU THỐNG KÊ TOÀN TỈNH NĂM ${thisYear}`);
        GetDataThongKe();
    });

    function TiepCongDan() {
        window.location.href = "/Webapp/Frontend/LichTiepDan.aspx";
    };

    function TrinhTuThuTuc() {
        window.location.href = "/Webapp/Frontend/TrinhTuThuTuc.aspx";
    }

    function HoiDap() {
        window.location.href = "/Webapp/Frontend/HoiDap.aspx";
    }

    function GetDataThongKe() {
        var tuNgay = '01-01-' + thisYear;
        var denNgay = '31-12-' + thisYear;
        var url = '<%= System.Configuration.ConfigurationManager.AppSettings["APIUrl_GetSoLieuTongHop"].ToString() %>';
        url += tuNgay + "/" + denNgay + "/2/0";
        $.ajax({
            url: url,
            type: "GET",
            success: function (data) {
                $("#lblLuotTiep").html(data.TongTiepDan);
                $("#lblXuLyDon").html(data.TongXuLyDon);
                $("#lblKhieuNai").html(data.TongDonThuBHGQ_KN);
                $("#lblToCao").html(data.TongDonThuBHGQ_TC);
                $("#lblKNPA").html(data.TongDonThuBHGQ_KNPA);
                $("#lblDongNguoi").html(data.TongTiepDanNhomKN);
            },
            error: function (data) {

            }
        });
    }

    function refTraCuuDonThu() {
        window.location.href = "/Webapp/Frontend/TraCuu.aspx";
    }

    function refTraCuuCoQuan() {
        window.location.href = "/Webapp/Frontend/TraCuu.aspx?tracuucoquan=1";
    }

    function renderItemSideBar() {
        $.ajax({
            url: "Home.aspx/GetDataSideBar",
            type: "POST",
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (data) {
                var data = JSON.parse(data.d);
                var dataSideBar = data.filter(item => item.Module == 2);
                //console.log('ajax', dataSideBar);
                $("#contentSidebar").html("");
                for (let i = 0; i < dataSideBar.length; i++) {
                    if (dataSideBar[i].TrangThaiHienThi) {
                        var boxTemp = $("#" + dataSideBar[i].MaModule).html();
                        $("#contentSidebar").append(boxTemp);
                    }
                }
            },
            error: function (error) {

            }
        });
    }


</script>

<style>
    .abc {
        width: 100%;
        height: 40px;
        border: 1px solid black;
    }

    .btn-height {
        height: 60px;
        font-size: 14pt;
        text-align: left;
        padding-left: 40px;
    }

    .btn-suffix {
        font-weight: bold;
        border-radius: 8px;
    }

    .img {
        width: 45px;
        height: 45px;
        margin-right: 10px;
    }

    .img22 {
        width: 15px;
        height: 15px;
        position: relative;
        left: 50px;
    }

    #lblNam {
        font-size: 12pt;
        font-weight: bold;
    }

    .info-table {
        width: 100%;
    }

        .info-table tr {
            height: 35px;
        }

    .box.box-primary {
        border-top: none;
    }
</style>

<div id="contentSidebar">
</div>

<%--<div class="panel panel-primary" style="display:none;">
    <div class="panel-heading">
        DANH MỤC TIN TỨC

    </div>
    <div class="panel-body">
   
    </div>
</div>

<div class="panel panel-primary" style="display:none;">
    <div class="panel-heading">
        TRA CỨU ĐƠN THƯ
    </div>
    <div class="panel-body" id="divTraCuuUrl2">
    </div>
</div>--%>

<%--<asp:Button ID="btnLichTiepDan" runat="server" PostBackUrl="/Webapp/Frontend/LichTiepDan.aspx" CssClass="btn btn-lg btn-block btn-primary" Text="LỊCH TIẾP DÂN" />
<asp:Button ID="btnTrinhTuThuTuc" runat="server" PostBackUrl="/Webapp/Frontend/TrinhTuThuTuc.aspx" CssClass="btn btn-lg btn-block btn-primary" Text="TRÌNH TỰ THỦ TỤC" />
<asp:Button ID="btnHoiDap" runat="server" PostBackUrl="/Webapp/Frontend/HoiDap.aspx" CssClass="btn btn-lg btn-block btn-primary" Text="HỎI ĐÁP" />--%>

<div id="Sidebar_DanhMucTinTuc" style="display: none">
    <div class="box box-primary">
        <div class="box-header" style="text-align: center">
            <h1 class="box-title">DANH MỤC TIN TỨC</h1>
        </div>
        <div class="box-body">
            <asp:Repeater ID="rptDanhMucTinTuc" runat="server" OnItemDataBound="rptDanhMucTinTuc_ItemDataBound">
                <ItemTemplate>
                    <div class="" style="width: 100%">
                        <p>
                            <%--<img src="../../images/btn_next1_kntc.png" />--%>
                            <asp:HyperLink runat="server" ID="hplLoaiTin"><%# Eval("TenLoaiTin") %></asp:HyperLink>
                            <hr style="margin-bottom: 4px; margin-top: 1px" />
                        </p>
                    </div>
                </ItemTemplate>
                <SeparatorTemplate>
                </SeparatorTemplate>
            </asp:Repeater>
        </div>
    </div>
</div>

<div id="Sidebar_LienKetTrang" style="display: none">
    <div class="box box-primary">
        <div class="box-body">
            <a href="http://thanhtra.baria-vungtau.gov.vn/web/guest/hoi-dap-truc-tuyen" target="_blank">
                <img src="../../images/frontend/sidebar/u33.png" style="outline: none; width: 300px; cursor: pointer">
            </a>
            <a href="https://bariavungtau.vnptigate.vn/" target="_blank">
                <img src="../../images/frontend/sidebar/u34.png" style="outline: none; width: 300px; height: 63px; margin-top: 10px; cursor: pointer">
            </a>
        </div>
    </div>
</div>

<div id="Sidebar_TraCuuDonThu" style="display: none">
    <div class="box box-primary">
        <div class="box-header" style="text-align: center">
            <i class="fc fc-grid"></i>
            <h1 class="box-title">TRA CỨU ĐƠN THƯ</h1>
        </div>
        <div class="box-body">
            <button class="btn btn-success btn-block btn-suffix" onclick="refTraCuuDonThu();return false;">
                Theo số đơn thư &emsp;&emsp;&ensp;&ensp;
            <img class="img22" src="../../images/frontend/sidebar/u22.png" style="outline: none;">
            </button>
            <button class="btn btn-warning btn-block btn-suffix" onclick="refTraCuuCoQuan();return false;">
                Theo cơ quan tiếp nhận
            <img class="img22" src="../../images/frontend/sidebar/u22.png" style="outline: none;">
            </button>
        </div>
    </div>
</div>

<div id="Sidebar_LichTiepCongDan" style="display: none">
    <button class="btn btn-primary btn-block btn-height" onclick="TiepCongDan();return false;">
        <img class="img " src="../../images/frontend/sidebar/u30.png" style="outline: none;">
        LỊCH TIẾP CÔNG DÂN
    </button>
</div>

<div id="Sidebar_TrinhTuThuTuc" style="display: none">
    <button class="btn btn-success btn-block btn-height" onclick="TrinhTuThuTuc();return false;">
        <img class="img " src="../../images/frontend/sidebar/u32.png" style="outline: none;">
        TRÌNH TỰ THỦ TỤC
    </button>
</div>

<div id="Sidebar_HoiDap" style="display: none">
    <button class="btn btn-danger btn-block btn-height" onclick="HoiDap();return false;">
        <img class="img " src="../../images/frontend/sidebar/u31.png" style="outline: none;">
        HỎI ĐÁP
    </button>
</div>

<div id="Sidebar_SoLieuToanTinh" style="display: none">
    <div class="box box-primary" style="border-top: none; margin-top: 10px" id="dulieuthongke">
        <div class="box-header-long" style="text-align: center">
            <label id="lblNam"></label>
        </div>
        <div class="box-body">
            <table class="info-table">
                <tr>
                    <td style="width: 75%">Lượt tiếp:</td>
                    <td style="width: 25%">
                        <label id="lblLuotTiep"></label>
                    </td>
                </tr>
                <tr>
                    <td>Xử lý đơn:</td>
                    <td>
                        <label id="lblXuLyDon"></label>
                    </td>
                </tr>
                <tr>
                    <td>Đơn khiếu nại:</td>
                    <td>
                        <label id="lblKhieuNai"></label>
                    </td>
                </tr>
                <tr>
                    <td>Đơn tố cáo:</td>
                    <td>
                        <label id="lblToCao"></label>
                    </td>
                </tr>
                <tr>
                    <td>Đơn kiến nghị, phản ảnh:</td>
                    <td>
                        <label id="lblKNPA"></label>
                    </td>
                </tr>
                <tr>
                    <td>Vụ việc đông người:</td>
                    <td>
                        <label id="lblDongNguoi"></label>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</div>

<%--<div id="Sidebar_ThongKeTruyCap" style="display: none">
    <div class="box box-primary" style="border-top: none">
        <div class="box-body">
            <table class="info-table">
                <tr>
                    <td style="width: 75%">Lượt tiếp:</td>
                    <td style="width: 25%">
                        <label id="lblTruyCapHienTai"></label>
                    </td>
                </tr>
                <tr>
                    <td>Xử lý đơn:</td>
                    <td>
                        <label id="lblTongLuotTruyCap"></label>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</div>--%>