<%@ Page Title="" Language="C#" MasterPageFile="~/Frontend.Master" AutoEventWireup="true" CodeBehind="TinTucDetailDemoAjax.aspx.cs" Inherits="Com.Gosol.CMS.Web.Webapp.Frontend.TinTucDetailDemoAjax" %>

<%@ Register Src="~/Webapp/Frontend/SideBarDanhMucTinTuc.ascx" TagPrefix="uc1" TagName="SideBarDanhMucTinTuc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        #MainContent_lblNoiDung p a img {
            max-width: 100%;
        }

        #MainContent_lblNoiDung p {
            font-family: 'Varela Round Bold', 'Varela Round';
        }

        @media (max-width:1500px) {
            .tin-cung-chuyen-muc-img {
                width: 60%;
            }
        }

        @media (min-width:1550px) {
            .tin-cung-chuyen-muc-img {
                width: 68%;
            }
        }
    </style>
    <input type="hidden" runat="server" id="idLoaiTin" value="0" />
    <div class="col-md-9">
        <div class="row">
            <div class="box-primary" style="text-align: justify">
                <div style="font-family: 'Varela Round Regular', 'Varela Round'; font-size: 20px; padding-left: 10px; padding-bottom: 0px;">
                    <asp:Label ID="lblLoaiTin" CssClass="box-title" runat="server"></asp:Label>
                </div>
                <div class="box-header">
                    <div>
                        <asp:HyperLink runat="server" ID="hplLoaiTin" Visible="false">
                        </asp:HyperLink>

                        <p style="color: #0094ff; margin-top: 0px; font-size: 14px; font-weight: 700; font-family: 'Varela Round Bold', 'Varela Round'; color: #333333 !important;">
                            <asp:Label ID="lblTitle" runat="server"></asp:Label>
                        </p>
                        <p style="font-size: 12px; color: #999999;">
                            <asp:Label ID="lblNgayDang" runat="server"></asp:Label>
                        </p>
                        <p>
                            <asp:Label ID="lblTomTat" runat="server" Style="font-family: 'Varela Round Regular', 'Varela Round';"></asp:Label>
                        </p>
                    </div>
                </div>
                <div class="box-body">
                    <asp:Image runat="server" ID="imgTinTuc" class="item-tinnoichinh-image hidden" />
                    <div class="clearfix"></div>
                    <div class="clearfix"></div>
                    <asp:Label ID="lblNoiDung" runat="server"></asp:Label>
                    <div class="clearfix"></div>
                </div>
            </div>
            <div class="box box-primary">
                <div class="box-header">
                    <span style="font-family: 'Varela Round Regular', 'Varela Round'; font-size: 20px; padding-right: 15px">Tin cùng chuyên mục</span>
                    <span>
                        <img src="/images/tintuc/tintuc_1183.png" class="tin-cung-chuyen-muc-img" /></span>
                    <span style="float: right; padding-top: 2px;">
                        <img src="/images/tintuc/tintuc_1186.png" /></span>
                    <span>
                        <a href="_LINKXEMTHEM_">
                            <span style="font-family: 'Varela Round Regular', 'Varela Round'; font-size: 14px; float: right; padding-top: 5px; padding-right: 10px; font-weight: normal">Xem thêm</span>
                        </a>
                    </span>
                </div>
                <div class="box-body" style="background-color: rgba(242, 242, 242, 0.756862745098039);">
                    <ul class="tin-tuc-chitiet-khac-ul" id="ulTinLienQuan">
                        <p id="itemTinLienQuan">
                            <li  style="display: none">
                                <div class="item-tin-noi-bat-img">
                                    <img src="/images/tintuc/tintuc_1175.png" />
                                </div>
                                <a href="_LINKTINTUCLIENQUAN_">
                                    <label class="item-tin-noi-bat-children-text font-varela">_TIEUDETINLIENQUAN_</label>
                                </a>
                                <span style="font-family: 'Varela Round Regular', 'Varela Round'; font-style: italic; color: #999999;">
                                    <label>_NGAYTAO_</label>
                                </span>
                            </li>
                        </p>

                    </ul>
                </div>
            </div>
        </div>
    </div>
    <div class="col-md-3">
        <uc1:SideBarDanhMucTinTuc runat="server" ID="SideBarDanhMucTinTuc" />
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            console.log($("#MainContent_idLoaiTin").val());
            var id = $("#MainContent_idLoaiTin").val();
            $.ajax({
                type: "POST",
                url: "TinTucDetailDemoAjax.aspx/GetTinLienQuanByIDLoaiTin",
                data: '{LoaiTin:"' + id + '"}',
                dataType: "json",
                async: "false",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    var allTnCungLoai = JSON.parse(data.d);
                    console.log(allTnCungLoai[1].TieuDe);
                    for (let i = 0; i <= allTnCungLoai.length; i++) {
                        console.log(allTnCungLoai[i].TieuDe);
                        var temp = $("#itemTinLienQuan").html();
                        temp = temp.replace(/_TIEUDETINLIENQUAN_/g, allTnCungLoai[i].TieuDe);
                        temp = temp.replace(/_LINKTINTUCLIENQUAN_/g, "/Webapp/Frontend/TinTucDetailDemo.aspx?tintuc=" + allTnCungLoai[i].IDTinTuc);
                        temp = temp.replace(/_NGAYTAO_/g, formatDate(allTnCungLoai[i].CreateDate));
                        $("#ulTinLienQuan").append(temp);
                    }
                },
                error: function () {
                    alert("false");
                },
            });
        });
        function formatDate(dateStr) {
            if (dateStr == "/Date(-62135596800000)/") {
                var currentDate = new Date();
                var dd = currentDate.getDate();
                var mm = currentDate.getMonth() + 1; //January is 0!
                var yyyy = currentDate.getFullYear();
                var date = dd + '/' + mm + '/' + yyyy;
                return date;
            }
            else {
                dateStr = dateStr.replace(/\//g, '');
                dateStr = dateStr.replace(/Date/g, '');
                dateStr = dateStr.replace(/\(/g, '');
                dateStr = dateStr.replace(/\)/g, '');
                var milisec = parseFloat(dateStr);
                var date = new Date(milisec);
                return date.getDate() + "/" + (date.getMonth() + 1) + "/" + date.getFullYear();
            }
        }
    </script>
</asp:Content>
