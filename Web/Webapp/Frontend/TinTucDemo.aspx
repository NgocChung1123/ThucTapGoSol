<%@ Page Title="" Language="C#" MasterPageFile="~/Frontend.Master" AutoEventWireup="true" CodeBehind="TinTucDemo.aspx.cs" Inherits="Com.Gosol.CMS.Web.Webapp.Frontend.TinTucDemo" %>

<%@ Register Src="~/Webapp/Frontend/SideBarDanhMucTinTuc.ascx" TagPrefix="uc1" TagName="SideBarDanhMucTinTuc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="/AdminLte/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="/AdminLte/dist/css/AdminLTE.min.css" rel="stylesheet" />
    <link href="../../AdminLte/dist/css/cssTinTuc.css" rel="stylesheet" />
    <style>
        .u1175_img {
            /*border-width:0px;
          position:absolute;
          left:0px;
          top:0px;*/
            width: 8px;
            height: 8px;
        }

        @media (max-width:1500px) {
            .tin-cung-chuyen-muc-img {
                width: 50%;
            }
        }

        @media (min-width:1600px) {
            .tin-cung-chuyen-muc-img {
                width: 60%;
            }
        }
    </style>

    <div class="col-lg-9" style="padding-right: 0px;">
        <div class="box-header">
            <span style="font-size: 18px; font-weight: bold; padding-right: 15px">Tin tức nổi bật</span>
            <%--<span><img src="/images/tintuc/tintuc_1183.png" class="tin-cung-chuyen-muc-img"/></span>--%>
        </div>
        <div class="row">
            <div class="">
                <div class="col-md-8 content-left body-backgroud" style="">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="gallery" id="TinTop1">
                                <%--<div class="bottom-center">
                                    ImageBig+titleBig
                                 </div>--%>
                                <a id="linkTinTop1" href="">
                                    <img id="imageTinTop1" alt="image" src="/UploadFiles/FileWF/20180131095807_hinh-anh-hinh-nen-arsenal-dep-moi-2016-56.jpg" />
                                    <h3 id="TitletinTop1"></h3>
                                </a>
                            </div>
                        </div>
                        <div id="tinHot2_3">
                            <div class="col-md-6" style="display: block" id="tinHot2">
                                <div class="item-tintuc-new">
                                    <a href="/Webapp/Frontend/TinTucDetail.aspx?tintuc=4104" id="linktinHot2">
                                        <img id="imageTinHot2" style="width: 100%;" alt="image" src="/UploadFiles/FileWF/20180131095807_hinh-anh-hinh-nen-arsenal-dep-moi-2016-56.jpg" />
                                        <h3 id="TitletinTop2">Title</h3>
                                    </a>
                                    <p id="tomtatHot2">tom Tat</p>
                                </div>

                            </div>
                            <div class="col-md-6" style="display: block">
                                <div class="item-tintuc-new">
                                    <a href="/Webapp/Frontend/TinTucDetail.aspx?tintuc=4104" id="linktinHot3">
                                        <img id="imageTinHot3" style="width: 100%;" alt="image" src="/UploadFiles/FileWF/20180131095807_hinh-anh-hinh-nen-arsenal-dep-moi-2016-56.jpg" />
                                        <h3 id="TitletinTop3">Title</h3>
                                    </a>
                                    <p id="tomtatHot3">tom Tat</p>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
                <div class="col-md-4 side-bar" style="">
                    <ul id="allTinHot">
                    </ul>
                </div>
            </div>
            <div id="tinCungLoai"></div>
            <div id="temp" style="display: none">
                <div class="col-md-12" runat="server" id="divNoiDungTin">
                    <div class="content-left body-backgroud">
                        <div class="box-primary">
                            <div class="box-header">
                                <span>
                                    <a id="hplLoaiTin" href="aaa" class="box-title" style="color: #333; padding-right: 10px">
                                        <label id="lblTenLoaiTin">_TENLOAITIN_</label>
                                    </a>
                                </span>
                                <span>
                                    <img src="/images/tintuc/tintuc_1183.png" class="tin-cung-chuyen-muc-img" /></span>
                                <span style="float: right;">
                                    <img src="/images/tintuc/tintuc_1186.png" /></span>
                                <span>
                                    <a id="hplXemThem" href="_LINKXEMTHEM_">
                                        <span style="font-family: 'Varela Round Regular', 'Varela Round'; font-size: 14px; float: right; padding-top: 2px; padding-right: 10px; font-weight: normal">Xem thêm</span>
                                    </a>
                                </span>
                            </div>
                            <div class="box-body">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="item-loaitin">
                                            <div class="item-loaitin-children" style="width: 100%">
                                                <a id="hplImage" href="_LINKTINTUC_">
                                                    <img src="_TINTUCCHINHIMAGE_" class="tintuc-image-right" />
                                                </a>
                                            </div>

                                        </div>
                                        <p class="item-tintuc-title-mang-tin">
                                            <span class="title_news">
                                                <a id="hylTin" href="linkTieuDe">
                                                      <label id="lblTitle"">_TIEUDETINCHINH_</label>
                                                </a>
                                            </span>

                                        </p>
                                        <p style="font-size: 12px; color: #999999;">
                                            <label id="lblNgayTao">_NGAYTAOTINCHINH_</label>
                                            <span>
                                                <a id="hplXemChiTiet" href="_LINKTINTUC_" style="font-style: italic;">
                                                    <span style="font-weight: normal; font-size: 12px; color: #999999;">Xem chi tiết</span>
                                                </a>
                                            </span>
                                        </p>
                                        <div class="item-loaitin-children item-loaitin-children-text" style="width: 100%; padding-left: 0px;">
                                            <span>
                                                <label id="lblTomTat">_TOMTATTINCHINH_</label>
                                            </span>
                                        </div>
                                    </div>
                                    <div class="col-md-6" style="border-left: 1px solid #eee;">
                                        <ul class="list-group list-group-flush" id="listtinCungLoai__IDTINTUC_">
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="col-lg-3" style="padding-right: 0px; float: right; padding-top: 0px;">
        <uc1:SideBarDanhMucTinTuc runat="server" ID="SideBarDanhMucTinTuc" />
    </div>
    <div id="itemTinCungLoai" style="display: none">
        <table style="margin-bottom: 10px;">
            <tr>
                <td style="width: 25%">
                    <a href="_LINKCHITIET_">
                        <img src="_IMAGETINCUNGLOAI_" class="tintuc-image-tin-lien-quan" />
                    </a>
                </td>
                <td style="padding-left: 10px">
                    <p>
                        <a href="_LINKCHITIET_">
                            <label>_TIEUDETNCUNGLOAI_</label>
                        </a>
                    </p>
                    <p style="font-size: 12px; color: #999999;">
                        <label>_NGAYTAO_</label>
                        <span>
                            <a href="_LINKCHITIET_" style="font-style: italic">
                                                                                <span style="font-weight: normal;font-size: 12px;color: #999999;">Xem chi tiết</span>
                            </a>
                        </span>
                    </p>
                </td>
            </tr>
        </table>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            getTop3TinHot();
            get10TinHot();
            getAllLoaiTin();
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
        function getTop3TinHot() {
            $.ajax({
                type: "POST",
                url: "TinTucDemo.aspx/getTop3TinHot",
                dataType: "json",
                async: "false",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    var Top3Hot = JSON.parse(data.d);
                    var link1 = '/Webapp/Frontend/TinTucDetail.aspx?tintuc=' + Top3Hot[0].IDTinTuc;
                    $("#linkTinTop1").attr('href', link1);
                    $("#imageTinTop1").attr('src', '/' + Top3Hot[0].ImageUrl);
                    $("#TitletinTop1").text(Top3Hot[0].TieuDe);
                    var link2 = '/Webapp/Frontend/TinTucDetail.aspx?tintuc=' + Top3Hot[1].IDTinTuc;
                    $("#linktinHot2").attr('href', link2);
                    $("#imageTinHot2").attr('src', '/' + Top3Hot[1].ImageUrl);
                    $("#TitletinTop2").text(Top3Hot[1].TieuDe);
                    $("#tomtatHot2").text(Top3Hot[1].TomTat);
                    var link3 = '/Webapp/Frontend/TinTucDetail.aspx?tintuc=' + Top3Hot[2].IDTinTuc;
                    $("#linktinHot3").attr('href', link3);
                    $("#imageTinHot3").attr('src', '/' + Top3Hot[2].ImageUrl);
                    $("#TitletinTop3").text(Top3Hot[2].TieuDe);
                    $("#tomtatHot3").text(Top3Hot[2].TomTat);
                    //for (let i = 0; i < Top3Hot.length; i++) {
                    //}
                }
            });
        }

        function get10TinHot() {
            $.ajax({
                type: "POST",
                url: "TinTucDemo.aspx/getAllTinHot",
                dataType: "json",
                async: "false",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    var allTopHot = JSON.parse(data.d);
                    for (let i = 0; i < allTopHot.length; i++) {
                        var link = '/Webapp/Frontend/TinTucDetail.aspx?tintuc=' + allTopHot[i].IDTinTuc;
                        var item = "<a style='color:black' href='" + link + "'><li style='font-size:16px;text-decoration: underline;color:#3C8DBC;'>" + allTopHot[i].TieuDe + "</li></a></br>";
                        $("#allTinHot").append(item);
                    }
                }
            });
        }

        function getAllLoaiTin() {
            $.ajax({
                type: "POST",
                url: "TinTucDemo.aspx/getAllLoaiTinTuc",
                dataType: "json",
                async: "false",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    var allLoaiTin = JSON.parse(data.d);
                    var test = formatDate(allLoaiTin[0].ChiTietTinTuc[0].CreateDate);
                    console.log("aaaaaaaaa: "+test);
                    //console.log(allLoaiTin[0].ChiTietTinTuc[0]);
                    for (let i = 0; i < allLoaiTin.length; i++) {
                        var temp = $("#temp").html();
                        temp = temp.replace(/_TENLOAITIN_/g, allLoaiTin[i].LoaiTinTuc.TenLoaiTin);
<<<<<<< HEAD
                        temp = temp.replace(/_LINKXEMTHEM_/g, "/Webapp/Frontend/LoaiTinTucDetailDemo.aspx?mangtinid=" + allLoaiTin[i].ChiTietTinTuc[0].IDLoaiTin);
=======
                        temp = temp.replace(/_LINKXEMTHEM_/g, "/Webapp/Frontend/LoaiTinTucDetailDemoAjax.aspx?mangtinid=" + allLoaiTin[i].ChiTietTinTuc[0].IDLoaiTin);
>>>>>>> origin/CongTy
                        temp = temp.replace(/_LINKTINTUC_/g, "/Webapp/Frontend/TinTucDetail.aspx?tintuc=" + allLoaiTin[i].ChiTietTinTuc[0].IDTinTuc);
                        temp = temp.replace(/_TINTUCCHINHIMAGE_/g, "/" + allLoaiTin[i].ChiTietTinTuc[0].ImageUrl);
                        temp = temp.replace(/_TIEUDETINCHINH_/g, allLoaiTin[i].ChiTietTinTuc[0].TieuDe);
                        temp = temp.replace(/_NGAYTAOTINCHINH_/g, "Ngày :" + formatDate(allLoaiTin[i].ChiTietTinTuc[0].CreateDate));
                        temp = temp.replace(/_TOMTATTINCHINH_/g, allLoaiTin[i].ChiTietTinTuc[0].TomTat);
                        temp = temp.replace(/_IDTINTUC_/g, allLoaiTin[i].LoaiTinTuc.IDLoaiTin);
                        $("#tinCungLoai").append(temp);
                        for (let j = 1; j < 6; j++) {
                            //console.log(allLoaiTin[i].ChiTietTinTuc[j]);
                            var temp_2 = $("#itemTinCungLoai").html();
                            temp_2 = temp_2.replace(/_IMAGETINCUNGLOAI_/g, "/" + allLoaiTin[i].ChiTietTinTuc[j].ImageUrl);
                            temp_2 = temp_2.replace(/_LINKCHITIET_/g, "/Webapp/Frontend/TinTucDetail.aspx?tintuc=" + allLoaiTin[i].ChiTietTinTuc[j].IDTinTuc);
                            temp_2 = temp_2.replace(/_TIEUDETNCUNGLOAI_/g, allLoaiTin[i].ChiTietTinTuc[j].TieuDe);
                            temp_2 = temp_2.replace(/_NGAYTAO_/g,"Ngày: "+ formatDate(allLoaiTin[i].ChiTietTinTuc[j].CreateDate));
                            $("#listtinCungLoai_" + allLoaiTin[i].LoaiTinTuc.IDLoaiTin).append(temp_2);
                        }
                    }

                }
            });
        }

    </script>
</asp:Content>

