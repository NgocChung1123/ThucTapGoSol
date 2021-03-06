﻿<%@ Page Title="Tra cứu văn bản trả lời" Language="C#" MasterPageFile="~/Frontend.Master" AutoEventWireup="true" CodeBehind="TraCuuVBTraLoi.aspx.cs" Inherits="Com.Gosol.CMS.Web.Webapp.Frontend.TraCuuVBTraLoi" EnableEventValidation="false" %>

<%@ Register Src="~/Webapp/Frontend/SideBarTinNoiBat.ascx" TagPrefix="uc1" TagName="SideBarTinNoiBat" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="/AdminLte/plugins/select2/select2.min.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .table-sm td:first-child {
            font-weight: bold;
        }

        .table-sm > tbody > tr > td {
            padding: 5px !important;
        }
    </style>
    <script type="text/javascript" src="/AdminLte/plugins/select2/select2.full.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $(".select2").select2({
                placeholder: "",
                width: '100% !important'
            });

            //getCoQuanParent();
            getCoQuan_C1();
            $(".cancelClick a").click(function (e) {

                e.stopPropagation();
            });
            $(".liTraCuu").addClass("active");
        });

        function getCoQuanParent() {
            var url = '<%= System.Configuration.ConfigurationManager.AppSettings["APIUrl_GetCoQuanParent"].ToString() %>';
            $("#MainContent_ddlCoQuanParent").html("");
            var arrLS = [];
            var listChil_Defalut = {};
            listChil_Defalut.id = 0;
            listChil_Defalut.text = "Chọn cơ quan parent";
            arrLS.push(listChil_Defalut);
            $.ajax({
                url: url,
                type: "GET",
                success: function (data) {

                    if (data != null) {
                        for (var i = 0; i < data.length; i++) {
                            var id = data[i].CoQuanID;
                            var text = data[i].TenCoQuan;
                            var listChil = {};
                            listChil.id = id;
                            listChil.text = text;
                            arrLS.push(listChil);
                        }
                        $("#MainContent_ddlCoQuanParent").select2({ data: arrLS });
                    }
                },
                error: function (data) {
                }
            });
        }

        function getCoQuan_C1() {
            //var url = '<%= System.Configuration.ConfigurationManager.AppSettings["APIUrl_GetCoQuan_By_ParentID"].ToString() %>';
            //var coQuanChaID = $("#MainContent_ddlCoQuanParent").val();
            var url = 'TraCuuVBTraLoi.aspx/GetListCoQuanByParentID';
            $("#MainContent_ddlCoQuan_C1").html("");
            var arrLS = [];
            var listChil_Defalut = {};
            listChil_Defalut.id = 0;
            listChil_Defalut.text = "Chọn cơ quan";
            arrLS.push(listChil_Defalut);
            //if (coQuanChaID > 0 && coQuanChaID != null && coQuanChaID != "") {
            $.ajax({
                url: url,
                type: "POST",
                data: '{"ParentID":"1"}',
                dataType: "json",
                async: "true",
                contentType: "application/json; charset=utf-8",
                success: function (data) {

                    if (data != null && data != "") {
                        for (var i = 0; i < data.d.length; i++) {
                            var id = data.d[i].CoQuanID;
                            var text = data.d[i].TenCoQuan;
                            var listChil = {};
                            listChil.id = id;
                            listChil.text = text;
                            arrLS.push(listChil);
                        }
                        $("#MainContent_ddlCoQuan_C1").select2({ data: arrLS });
                        $(".ddlCoQuan_C1").show();
                        var coQuanID = $('#<%= hdfCoQuanID.ClientID%>').val();
                        $("#MainContent_ddlCoQuan_C1").val(coQuanID).trigger("change");
                    }
                    else {
                        $(".ddlCoQuan_C1").hide();
                        $(".ddlCoQuan_C2").hide();
                    }
                },
                error: function (data) {
                    $(".ddlCoQuan_C1").hide();
                    $(".ddlCoQuan_C2").hide();
                }
            });
            //} else {
            //    $(".ddlCoQuan_C1").hide();
            //    $(".ddlCoQuan_C2").hide();
            //}
        }

        function getCoQuan_C2() {
      <%--      var url = '<%= System.Configuration.ConfigurationManager.AppSettings["APIUrl_GetCoQuan_By_ParentID"].ToString() %>';--%>
            var url = 'TraCuuVBTraLoi.aspx/GetListCoQuanByParentID';
            var coQuanChaID = $("#MainContent_ddlCoQuan_C1").val();
            $("#MainContent_ddlCoQuan_C2").html("");
            var arrLS = [];
            var listChil_Defalut = {};
            listChil_Defalut.id = 0;
            listChil_Defalut.text = "Tất cả";
            arrLS.push(listChil_Defalut);

            if (coQuanChaID > 0 && coQuanChaID != null && coQuanChaID != "") {
                $.ajax({
                    url: url,
                    type: "POST",
                    data: '{"ParentID":"' + coQuanChaID + '"}',
                    dataType: "json",
                    async: "true",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {

                        if (data != null && data != "" && data.d.length > 0) {
                            for (var i = 0; i < data.d.length; i++) {
                                var id = data.d[i].CoQuanID;
                                var text = data.d[i].TenCoQuan;
                                var listChil = {};
                                listChil.id = id;
                                listChil.text = text;
                                arrLS.push(listChil);
                            }

                            $("#MainContent_ddlCoQuan_C2").select2({ data: arrLS });
                            var coQuanID = $('#<%= hdfCoQuanID2.ClientID%>').val();
                            $("#MainContent_ddlCoQuan_C2").val(coQuanID).trigger("change");
                            $(".ddlCoQuan_C2").show();

                        }
                        else {
                            $(".ddlCoQuan_C2").hide();
                        }
                    },
                    error: function (data) {
                        $(".ddlCoQuan_C2").hide();
                    }
                });
            }
            else {
                $(".ddlCoQuan_C2").hide();
            }
        }



        <%--function getData() {
            var url = '<%= System.Configuration.ConfigurationManager.AppSettings["APIUrl_GetVanBanTraLoi"].ToString() %>';
            var soDonThu = $("#MainContent_txtSoDonThu").val();
            var coQuanID = $("#MainContent_ddlCoQuanParent").val();
            var coQuanID_C1 = $("#MainContent_ddlCoQuan_C1").val();
            var coQuanID_C2 = $("#MainContent_ddlCoQuan_C2").val();
            var coQuanID_Param = 0;
            if (coQuanID_C2 > 0) {
                coQuanID_Param = coQuanID_C2;
            }
            else if (coQuanID_C1 > 0) {
                coQuanID_Param = coQuanID_C1;
            }
            else if (coQuanID > 0) {
                coQuanID_Param = coQuanID;
            }
            if (soDonThu != null && soDonThu != '') {
                $(".loadding_qrcode").show();
                $("#divMsgError").html("");
                $.ajax({
                    url: url + soDonThu + "/" + coQuanID_Param,
                    type: "GET",
                    success: function (data) {
                        $(".loadding_qrcode").hide();
                        fillData(data);
                        $(".div-content").show();
                    },
                    error: function (data) {
                        $(".loadding_qrcode").hide();
                        $("#divMsgError").html("Không có dữ liệu tìm kiếm!");
                        $(".div-content").hide();
                    }
                });
            } else {
                $("#divMsgError").html("Không có dữ liệu tìm kiếm!");
                $(".div-content").hide();
            }
        }--%>

        function getData() {
            var url = '<%= System.Configuration.ConfigurationManager.AppSettings["APIUrl_GetVanBanTraLoi"].ToString() %>';
            var soDonThu = $("#MainContent_txtSoDonThu").val();
            var coQuanID = $("#MainContent_ddlCoQuanParent").val();
            var coQuanID_C1 = $("#MainContent_ddlCoQuan_C1").val();
            var coQuanID_C2 = $("#MainContent_ddlCoQuan_C2").val();
            var coQuanID_Param = 0;
            if (coQuanID_C2 > 0) {
                coQuanID_Param = coQuanID_C2;
            }
            else if (coQuanID_C1 > 0) {
                coQuanID_Param = coQuanID_C1;
            }
            else if (coQuanID > 0) {
                coQuanID_Param = coQuanID;
            }
            if (soDonThu != null && soDonThu != '') {
                $(".loadding_qrcode").show();
                $("#divMsgError").html("");
                $.ajax({
                    url: "TraCuuQDGiaiQuyet.aspx/GetDonThuBySoDonThu",
                    type: "POST",
                    data: '{soDonThu:"' + soDonThu + '",coQuanID:"' + coQuanID_Param + '"}',
                    dataType: "json",
                    async: "true",
                    contentType: "application/json; charset=utf-8",

                    success: function (data) {
                        console.log("succes!");
                        $(".loadding_qrcode").hide();

                        if (data.d.XuLyDonID != 0) {
                            $.ajax({
                                url: url + soDonThu + "/" + coQuanID_Param,
                                type: "GET",
                                success: function (data) {
                                    $(".loadding_qrcode").hide();
                                    fillData(data);
                                    $(".div-content").show();
                                },
                                error: function (data) {
                                    $(".loadding_qrcode").hide();
                                    $("#divMsgError").html("Không có dữ liệu tìm kiếm!");
                                    $(".div-content").hide();
                                }
                            });
                            $(".div-content").show();
                        } else {
                            $(".loadding_qrcode").hide();
                            $("#divMsgError").html("Không có dữ liệu tìm kiếm!");
                            $(".div-content").hide();
                        }

                    },
                    error: function (data) {
                        $(".loadding_qrcode").hide();
                        $("#divMsgError").html("Không có dữ liệu tìm kiếm!");
                        $(".div-content").hide();
                    }
                });
            }
            else {
                $("#divMsgError").html("Không có dữ liệu tìm kiếm!");
                $(".div-content").hide();
            }
        }

        function fillData(data) {
            var urlFile_Download = '<%= System.Configuration.ConfigurationManager.AppSettings["APIUrl_File_DownLoad"].ToString() %>';
            /*thong tin chung*/
            if (data.SoDonThu != "" && data.SoDonThu != null) {
                $("#lblSoDonThu").html(data.SoDonThu);
                $(".lblSoDonThu").show();
            }
            else {
                $(".lblSoDonThu").hide();
            }
            if (data.NgayNhapDonStr != "" && data.NgayNhapDonStr != null) {
                $("#lblNgayTiepNhan").html(data.NgayNhapDonStr);
                $(".lblNgayTiepNhan").show();
            } else {
                $(".lblNgayTiepNhan").hide();
            }
            if (data.TenCanBoTiepNhan != "" && data.TenCanBoTiepNhan != null) {
                $("#lblCanBoTiepNhan").html(data.TenCanBoTiepNhan);
                $(".lblCanBoTiepNhan").show();
            } else {
                $(".lblCanBoTiepNhan").hide();
            }
            if (data.TenCoQuanTiepNhan != "" && data.TenCoQuanTiepNhan != null) {
                $("#lblCoQuanTiepNhan").html(data.TenCoQuanTiepNhan);
                $(".lblCoQuanTiepNhan").show();
            }
            else {
                $(".lblCoQuanTiepNhan").hide();
            }

            if (data.TenLoaiKhieuTo1 != "" && data.TenLoaiKhieuTo1 != null) {
                $("#lblPhanLoaiDon").html(data.TenLoaiKhieuTo1);
                if (data.TenLoaiKhieuTo2 != "") {
                    $("#lblPhanLoaiDon").html($("#lblPhanLoaiDon").html() + " > " + data.TenLoaiKhieuTo2);
                }
                if (data.TenLoaiKhieuTo3 != "") {
                    $("#lblPhanLoaiDon").html($("#lblPhanLoaiDon").html() + " > " + data.TenLoaiKhieuTo3);
                }
                $(".lblPhanLoaiDon").show();
            }
            else {
                $(".lblPhanLoaiDon").hide();
            }

            if (data.NoiDungDon != "" && data.NoiDungDon != null) {
                $("#lblNoiDungDon").html(data.NoiDungDon);
                $(".lblNoiDungDon").show();
            }
            else {
                $(".lblNoiDungDon").hide();
            }

            /*thong tin xu lý*/
            if (data.TenCoQuanXL != "" && data.TenCoQuanXL != null) {
                $("#lblCoQuanXuLy").html(data.TenCoQuanXL);
                $(".lblCoQuanXuLy").show();
            } else {
                $(".lblCoQuanXuLy").hide();
            }
            if (data.TenPhongBanXuLy != "" && data.TenPhongBanXuLy != null) {
                $("#lblPhongBanXuLy").html(data.TenPhongBanXuLy);
                $(".lblPhongBanXuLy").show();
            } else {
                $(".lblPhongBanXuLy").hide();
            }
            if (data.TenCanBoXuLy != "" && data.TenCanBoXuLy != null) {
                $("#lblCanBoXuLy").html(data.TenCanBoXuLy);
                $(".lblCanBoXuLy").show();
            } else {
                $(".lblCanBoXuLy").hide();
            }
            if (data.TrangThaiDonThu != "" && data.TrangThaiDonThu != null) {
                $("#lblTrangThaiXuLy").html(data.TrangThaiDonThu);
                $(".lblTrangThaiXuLy").show();
            } else {
                $(".lblTrangThaiXuLy").hide();
            }
            if (data.NgayXuLyStr != "" && data.NgayXuLyStr != null) {
                $("#lblNgayXuLy").html(data.NgayXuLyStr);
                $(".lblNgayXuLy").show();
            } else {
                $(".lblNgayXuLy").hide();
            }
            if (data.HuongXuLy != "" && data.HuongXuLy != null) {
                $("#lblHuongXuLy").html(data.HuongXuLy);
                $(".lblHuongXuLy").show();
            }
            else {
                $(".lblHuongXuLy").hide();
            }

            if (data.lsDoiTuongKN != null && data.lsDoiTuongKN.length > 0) {
                $("#ltrNguoiDaiDien").html("");
                for (var i = 0; i < data.lsDoiTuongKN.length; i++) {
                    var gioitinh = "Nữ";
                    if (data.lsDoiTuongKN[i].GioiTinh == 0) {
                        gioitinh = "Nam";
                    }
                    var divNguoiDaiDien = "";
                    if (data.lsDoiTuongKN[i].HoTen != null && data.lsDoiTuongKN[i].HoTen != "") {
                        divNguoiDaiDien += "<div class='col-md-2'></div><div class='col-md-10'><b>Người đại diện " + (i + 1) + "</b><br/><br/><label>Họ tên: </label><span class='info'>" + data.lsDoiTuongKN[i].HoTen + "</span><br/><br/>";
                    }
                    if (data.lsDoiTuongKN[i].CMND != null && data.lsDoiTuongKN[i].CMND != "") {
                        divNguoiDaiDien += " <label>CMND: </label><span class='info'>" + data.lsDoiTuongKN[i].CMND + "</span><br/><br/> <label>Giới tính: </label><span class='info'>" + gioitinh + "</span><br/><br/>";
                    }
                    if (data.lsDoiTuongKN[i].NgheNghiep != null && data.lsDoiTuongKN[i].NgheNghiep != "") {
                        divNguoiDaiDien += " <label>Nghề nghiệp: </label><span class='info'>" + data.lsDoiTuongKN[i].NgheNghiep + "</span><br/><br/>";
                    }
                    if (data.lsDoiTuongKN[i].TenQuocTich != null && data.lsDoiTuongKN[i].TenQuocTich != "") {
                        divNguoiDaiDien += " <label>Quốc tịch: </label><span class='info'>" + data.lsDoiTuongKN[i].TenQuocTich + "</span><br/><br/>";
                    }
                    if (data.lsDoiTuongKN[i].TenDanToc != null && data.lsDoiTuongKN[i].TenDanToc != "") {
                        divNguoiDaiDien += "  <label>Dân tộc: </label> <span class='info'>" + data.lsDoiTuongKN[i].TenDanToc + "</span> <br /> <br />";
                    }
                    if (data.lsDoiTuongKN[i].DiaChiCT != null && data.lsDoiTuongKN[i].DiaChiCT != "") {
                        divNguoiDaiDien += " <label>Địa chỉ: </label> <span class='info'>" + data.lsDoiTuongKN[i].DiaChiCT + "</span> <br /></div > ";
                    }
                    $("#ltrNguoiDaiDien").html($("#ltrNguoiDaiDien").html() + divNguoiDaiDien);
                }
                $(".ltrNguoiDaiDien").show();
            }
            else {
                $(".ltrNguoiDaiDien").hide();
            }

            if (data.NhomKNInfo != null) {
                if (data.NhomKNInfo.StringLoaiDoiTuongKN == "CaNhan") {
                    $("#lblDoiTuongKhieuNai").html("Cá nhân");
                }
                if (data.NhomKNInfo.StringLoaiDoiTuongKN == "CoQuan") {
                    $("#lblDoiTuongKhieuNai").html("Cơ quan, tổ chức");
                    $("#ltrDoiTuongKhieuNai").html("<label class='col-md-2'>Tên cơ quan</label><span class='col-md-10'>" + data.NhomKNInfo.TenCQ + "</span><br/>" + "<label class='col-md-2'>Địa chỉ cơ quan:</label><span class='col-md-10'>" + data.NhomKNInfo.DiaChiCQ + "</span><br/>");
                }
                if (data.NhomKNInfo.StringLoaiDoiTuongKN == "TapThe") {
                    $("#lblDoiTuongKhieuNai").html("Tập thể");
                    $("#ltrDoiTuongKhieuNai").html("<label class='col-md-2'>Số lượng:</label><span class='col-md-10'>" + data.NhomKNInfo.SoLuong + "</span><br/>");
                }
                $(".lblDoiTuongKhieuNai").show();
            }
            else {
                $(".lblDoiTuongKhieuNai").show();
            }
            if (data.lsFileYKienXuLy != null) {
                for (var i = 0; i < data.lsFileYKienXuLy.length; i++) {
                    var hiendownload = "";
                    var fileUrl = "", text = "";
                    if (data.lsFileYKienXuLy[i].FileURL != "") {

                        fileUrl = urlFile_Download + "UploadFiles/encrypt/" + data.lsFileYKienXuLy[i].FileURL;
                        hiendownload = " <a  href='" + fileUrl + "' target='_blank'><img src='/images/download.png' class='image' style='float:left;padding-top: 4px;'/>Tải về</a>";
                        $("#tbfileykienxuly_CT ").html(hiendownload);
                    }

                }
                $("#divFileYKienXL").show();
            }
            else {
                $("#divFileYKienXL").hide();
            }
        }

        function downloadFileDonThuCT(url) {
            window.location.href = "../../DowloadFileDonThuSync.aspx?url=" + url;
        } 

        function showDetail(SoDonThu, NgayTiepNhan, CoQuanTiepNhan, NguoiDaiDien, DiaChi, NoiDungDon, CoQuanXuLy, CoQuanGiaiQuyet, HuongXuLy, FileQuyetDinh, TenQuyetDinh, XemTruoc, DonThuID) {
            $("#dtSoDonThu").html("<p>" + SoDonThu + "</p>");
            $("#dtTenQuyetDinh").html("<p>" + TenQuyetDinh + "</p>");
            $("#dtNgayTiepNhan").html("<p>" + NgayTiepNhan + "</p>");
            $("#dtCoQuanTiepNhan").html("<p>" + CoQuanTiepNhan + "</p>");
            $("#dtHoTen").html("<p>" + NguoiDaiDien + "</p>");
            $("#dtDiaChi").html("<p>" + DiaChi + "</p>");
            $("#dtNoiDung").html("<p>" + NoiDungDon + "</p>");
            $("#dtCoQuanXuLy").html("<p>" + CoQuanXuLy + "</p>");
            $("#dtCoQuanGiaiQuyet").html("<p>" + CoQuanGiaiQuyet + "</p>");
            $("#dtHuongXuLy").html("<p>" + HuongXuLy + "</p>");

            $.ajax({
                type: "POST",
                url: "TraCuuVBTraLoi.aspx/GetFile",
                data: '{id:"' + DonThuID + '"}',
                dataType: "json",
                async: true,
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    var json = eval('(' + data.d + ')');

                    if (json.length > 0) {
                        for (var i = 0; i < json.length; i++) {
                            var ngayCapNhat = json[i].NgayUp_Str;
                            var tenHoSo = json[i].TenFile;
                            var fileUrl = json[i].FileURL;
                            var index = $("#fileTable tr").length;
                            var newchar = '*';
                            fileUrl = fileUrl.split(' ').join(newchar);

                            $("#fileTable >tbody").append("<tr class='newFile' id='" + index + "'><td style='text-align:center;'><input type='hidden' value='" + fileUrl + "' /> <input type='hidden' value='' />" + index + "</td><td>" + tenHoSo + "</td><td style='text-align:center;'>" + ngayCapNhat + "</td><td style='display:none;'></td><td style='text-align:center;'><a onclick=downloadFileDonThuCT('" + fileUrl + "')><img style='cursor: pointer;' id='imgDownLoad' title='Download file đính kèm' width='22' height='20' src='../../images/cloud_download.png'></a></td></tr>");
                        }
                    }
                }
            });

            if (FileQuyetDinh != '') {
                $("#dtVanBanTraLoi").html((XemTruoc == "1" ? "<a target=\"_blank\" href =\"/Handler/PreviewFile.ashx?filename=" + FileQuyetDinh + "\" >Xem trước </a> - " : "") + " <a href =\"/UploadFiles/" + FileQuyetDinh + "\" download >Tải xuống </a>");
            }
            else {
                $("#dtVanBanTraLoi").html("");
            }
            $(".dsVBTraLoi").hide();
            $(".panelDetail").show();
        }


        function hideDetail() {
            $(".panelDetail").hide();
            $(".dsVBTraLoi").show();

        }
        function logthisrows(row) {
            console.log(row);
        }
    </script>
    <div class="row">
        <div class="col-lg-12 col-md-12 col-xs-12 col-sm-12">
            <div class="col-lg-12">
                <div class="clear-both"></div>
                <div class="col-md-12 content-left body-backgroud">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <br />
                            <label class="control-label col-lg-2 col-md-2 col-xs-12 col-sm-3">Chọn cơ quan: </label>
                            <%--    <div class="col-md-4">
                                <asp:DropDownList ID="ddlCoQuanParent" CssClass="select2 form-control" runat="server" DataTextField="TenCoQuan" DataValueField="CoQuanID" Width="100%" onchange="getCoQuan_C1();return false"></asp:DropDownList>
                            </div>--%>

                            <div class="col-lg-8 col-md-8 col-xs-12 col-sm-8 ddlCoQuan_C1">
                                <asp:DropDownList ID="ddlCoQuan_C1" CssClass="select2 form-control" runat="server" DataTextField="TenCoQuan" DataValueField="CoQuanID" Width="100%" onchange="getCoQuan_C2();return false"></asp:DropDownList>
                            </div>
                            <asp:HiddenField ID="hdfCoQuanID" runat="server" />

                        </div>
                        <div class="form-group ddlCoQuan_C2" style="display: none">
                            <label class="control-label col-lg-2 col-md-2 col-xs-12 col-sm-3">Chọn cơ quan: </label>
                            <div class="col-lg-8 col-md-8 col-xs-12 col-sm-8">
                                <asp:DropDownList ID="ddlCoQuan_C2" CssClass="select2 form-control" runat="server" DataTextField="TenCoQuan" DataValueField="CoQuanID" Width="100%"></asp:DropDownList>
                            </div>

                            <asp:HiddenField ID="hdfCoQuanID2" runat="server" />
                        </div>
                        <div class="form-group">
                            <label class="control-label col-lg-2 col-md-2 col-xs-12 col-sm-3">Số đơn thư</label>
                            <div class="col-lg-8 col-md-8 col-xs-12 col-sm-8">
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtSoDonThu"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="control-label col-lg-2 col-md-2 col-xs-12 col-sm-3">Tên quyết định</label>
                            <div class="col-lg-8 col-md-8 col-xs-12 col-sm-8">
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtTenQuyetDinh"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="control-label col-lg-2 col-md-2 col-xs-12 col-sm-3">Thời gian ban hành</label>
                            <div class="col-md-4 col-md-4 col-xs-6 col-sm-4">
                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control datepicker" ID="txtTgBanHanhFrom"></asp:TextBox>
                            </div>
                            <div class="col-md-4 col-md-4 col-xs-6 col-sm-4">
                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control datepicker" ID="txtTgBanHanhTo"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-12 text-center">
                                <%-- <asp:Button ID="btnSearch" runat="server" Text="Tra cứu" CssClass="btn btn-sm" OnClick="btnSearch_Click"  CausesValidation="false" />\--%>
                                <asp:Button ID="btnSearch2" runat="server" Text="Tra cứu" CssClass="btn btn-sm btn-primary" OnClick="btnSearch2_Click" />
                                <asp:Button ID="btnQRCode" runat="server" Text="Quét QR Code" CssClass="btn btn-sm btn-warning hidden" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="loadding_qrcode" style="display: none"></div>
            <div class="col-lg-12" id="divMsgError" style="text-align: center; color: red"></div>
            <div class="div-content " style="display: none">
                <div class="clear-both"></div>
                <div class="col-lg-12 body-backgroud">
                    <div class="col-lg-12 tracuu-title">
                        <b>Thông tin chung</b>
                    </div>
                </div>
                <div class="clear-both"></div>
                <div class="col-lg-12">
                    <div class="form-horizontal" style="margin-left: 20px">
                        <div class="form-group lblSoDonThu">
                            <label class="col-lg-2">Số đơn :</label>
                            <label id="lblSoDonThu" class="col-lg-10"></label>
                        </div>
                        <div class="form-group lblNgayTiepNhan">
                            <label class="col-lg-2">Ngày tiếp nhận :</label>
                            <label id="lblNgayTiepNhan" class="col-lg-10"></label>
                        </div>
                        <div class="form-group lblCoQuanTiepNhan">
                            <label class="col-lg-2">Cơ quan tiếp nhân :</label>
                            <label id="lblCoQuanTiepNhan" class="col-lg-10"></label>
                        </div>
                        <div class="form-group lblCanBoTiepNhan">
                            <label class="col-lg-2">Cán bộ tiếp nhận :</label>
                            <label id="lblCanBoTiepNhan" class="col-lg-10"></label>
                        </div>
                        <div class="form-group lblPhanLoaiDon">
                            <label class="col-lg-2">Phân loại đơn:</label>
                            <label id="lblPhanLoaiDon" class="col-lg-10"></label>
                        </div>
                        <div class="form-group lblNoiDungDon">
                            <label class="col-lg-2">Nội dung đơn:</label>
                            <label id="lblNoiDungDon" class="col-lg-10"></label>
                        </div>
                        <div class="form-group lblDoiTuongKhieuNai">
                            <label class="col-lg-2">
                                Đối tượng khiếu nại:
                            </label>
                            <label id="lblDoiTuongKhieuNai" class='col-md-10' style="font-weight: inherit"></label>
                            <br />
                            <div id="ltrDoiTuongKhieuNai">
                            </div>
                        </div>

                        <div class="form-group ltrNguoiDaiDien">
                            <label class="col-lg-2">
                                Người đại diện:
                            </label>
                            <div id="ltrNguoiDaiDien">
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-12 body-backgroud">
                    <div class="col-md-12 tracuu-title">
                        <b>Thông tin xử lý</b>
                    </div>
                </div>
                <div class="clear-both"></div>
                <div class="col-lg-12">
                    <div class="form-horizontal" style="margin-left: 20px">
                        <div class="form-group">
                            <label class="col-lg-2 lblCoQuanXuLy">Cơ quan xử lý :</label>
                            <label id="lblCoQuanXuLy" class="col-lg-10"></label>
                        </div>
                        <div class="form-group lblPhongBanXuLy">
                            <label class="col-lg-2">Phòng ban xử lý :</label>
                            <label id="lblPhongBanXuLy" class="col-lg-10"></label>
                        </div>
                        <div class="form-group lblCanBoXuLy">
                            <label class="col-lg-2">Cán bộ xử lý :</label>
                            <label id="lblCanBoXuLy" class="col-lg-10"></label>
                        </div>
                        <div class="form-group lblTrangThaiXuLy">
                            <label class="col-lg-2" style="font-weight: bold">Trạng thái đơn thư:</label>
                            <label id="lblTrangThaiXuLy" style="font-weight: bold" class="col-lg-10"></label>
                        </div>
                        <div class="form-group lblNgayXuLy">
                            <label class="col-lg-2">Ngày xử lý :</label>
                            <label id="lblNgayXuLy" class="col-lg-10"></label>
                        </div>
                        <div class="form-group lblHuongXuLy">
                            <label class="col-lg-2">Hướng xử lý :</label>
                            <label id="lblHuongXuLy" class="col-lg-10"></label>
                        </div>
                        <div id="divFileYKienXL" class="form-group ">
                            <label class="col-lg-2">Văn bản trả lời :</label>
                            <div class="col-lg-10" id="tbfileykienxuly_CT"></div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="content-body col-md-12">
                <div class="box-body">
                    <div class="table-responsive">
                        <div class="box box-primary dsVBTraLoi">
                            <div class="box-header">

                                <span style="font-size: 24px; font-family: inherit; font-weight: 500; line-height: 1.1;">Danh sách văn bản trả lời</span>

                            </div>
                            <div class="box-body">
                                <table id="table" class="table table-bordered table-hover" style="width: 100%">
                                    <thead>
                                        <tr>
                                            <th style="width: auto; text-align: center">STT
                                            </th>
                                            <th style="width: auto; text-align: center">Số đơn
                                            </th>
                                            <th style="width: auto; text-align: center">Ngày tiếp nhận
                                            </th>
                                            <th style="width: auto; text-align: center">Cơ quan tiếp nhận
                                            </th>
                                            <th style="width: auto; text-align: center">Người đại diện
                                            </th>
                                            <th style="width: auto; text-align: center; display: none">Cán bộ tiếp nhận
                                            </th>
                                            <th style="width: auto; text-align: center; display: none">Phân loại đơn
                                            </th>
                                            <th style="width: auto; text-align: center">Nội dung đơn
                                            </th>

                                            <th style="width: auto; text-align: center; display: none;">Cơ quan xử lý
                                            </th>
                                            <th style="width: auto; text-align: center; display: none;">Trạng thái đơn thư
                                            </th>
                                            <th style="width: auto; text-align: center; display: none;">Ngày ban hành
                                            </th>
                                            <th style="width: auto; text-align: center;">Ngày ban hành
                                            </th>
                                            <th style="width: auto; text-align: center;">Văn bản trả lời
                                            </th>

                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="rptDonThu" runat="server" OnItemDataBound="rptDonThu_ItemDataBound1">
                                            <ItemTemplate>
                                                <tr onclick='<%# "showDetail(\"" + Eval("SoDonThu") + "\",\"" +  Eval("NgayTiepNhan") + "\",\"" +  Eval("CoQuanTiepNhan") + "\",\"" +  Eval("NguoiDaiDien") + "\",\"" +  Eval("DiaChi") + "\",\"" +  Eval("NoiDungDon") + "\",\"" +  Eval("CoQuanXuLy") + "\",\"" +  Eval("CoQuanGiaiQuyet") + "\",\"" +  Eval("HuongXuLy") + "\",\"" +  Eval("FileQuyetDinh") + "\",\"" +  Eval("TenQuyetDinh").ToString()+ "\",\"" +  Eval("XemTruocFile").ToString() + "\",\"" + Eval("ID").ToString() + "\"); return false;" %>'>
                                                    <%--   <tr onclick="logthisrows(this);">--%>
                                                    <td style="text-align: center;">
                                                        <asp:Label runat="server" ID="lblSTT"></asp:Label>
                                                    </td>
                                                    <td style="text-align: center;">
                                                        <b><%# Eval("SoDonThu") %></b>
                                                    </td>
                                                    <td style="text-align: center;">
                                                        <asp:Label runat="server" ID="lblNgayTiepNhan"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <%# Eval("CoQuanTiepNhan").ToString().Length>=100?Eval("CoQuanTiepNhan").ToString().Substring(0,99) + " ... .":Eval("CoQuanTiepNhan").ToString() %>
                                                    </td>
                                                    <td style="text-align: left; display: none;">
                                                        <%#Eval("CanBoTiepNhan").ToString().Length>=100?Eval("CanBoTiepNhan").ToString().Substring(0,99) + " ... .":Eval("CanBoTiepNhan").ToString() %>
                                                    </td>
                                                    <td style="text-align: left; display: none;">
                                                        <%# Eval("PhanLoaiDon") %>
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <%#Eval("NguoiDaiDien").ToString().Length>=100?Eval("NguoiDaiDien").ToString().Substring(0,99) + " ... .":Eval("NguoiDaiDien").ToString() %>
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <%#Eval("NoiDungDon").ToString().Length>=100?Eval("NoiDungDon").ToString().Substring(0,99) + " ... .":Eval("NoiDungDon").ToString() %>
                                                    </td>

                                                    <td style="text-align: left; display: none;">
                                                        <%# Eval("CoQuanXuLy") %>
                                                    </td>
                                                    <td style="text-align: left; display: none;">
                                                        <%# Eval("TrangThaiDonThu") %>
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <%# Eval("NgayBanHanh") %>
                                                    </td>
                                                    <td style="text-align: left;" class="cancelClick" onclick="return false;">
                                                        <a target="_blank" style="text-decoration: underline;" class='<%# Eval("XemTruocFile").ToString() == "1"?"":"hidden" %>' href='<%# Eval("XemTruocFile").ToString() == "0"? "#": "/Handler/PreviewFile.ashx?filename="+Eval("FileQuyetDinh")  %>'>Xem trước</a>
                                                        <a style="color: green; text-decoration: underline;" class='<%# Eval("FileQuyetDinh").ToString() == ""?"hidden":"" %>' href='<%# Eval("FileQuyetDinh").ToString() == ""? "#": "/Handler/DownloadFileQuyetDinh.ashx?filename="+Eval("FileQuyetDinh")  %>'>Tải văn bản</a>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                                <div class="paginations" style="margin-top: 15px; margin-bottom: 15px">
                                    <asp:PlaceHolder ID="plhPaging" runat="server"></asp:PlaceHolder>
                                </div>
                            </div>



                        </div>


                        <div class="box box-primary panelDetail" style="width: 100%;" hidden="hidden">
                            <div class="box-header" style="text-align: center">

                                <span style="font-size: 24px; font-family: inherit; font-weight: 500; line-height: 1.1;">THÔNG TIN VĂN BẢN TRẢ LỜI CÔNG DÂN</span>

                            </div>
                            <div class="box-body">
                                <table class="table table-responsive table-sm">
                                    <tr>
                                        <td class="tdChiTiet col-sm-3" style="height: 10px;">Số đơn thư: </td>
                                        <td class="tdChiTiet col-sm-9" id="dtSoDonThu"></td>
                                    </tr>
                                    <tr>
                                        <td>Tên Quyết định: </td>
                                        <td id="dtTenQuyetDinh"></td>
                                    </tr>
                                    <tr>
                                        <td>Ngày tiếp nhận: </td>
                                        <td id="dtNgayTiepNhan"></td>
                                    </tr>
                                    <tr>
                                        <td>Cơ quan tiếp nhận: </td>
                                        <td id="dtCoQuanTiepNhan"></td>
                                    </tr>
                                    <tr>
                                        <td>Họ tên công dân: </td>
                                        <td id="dtHoTen"></td>
                                    </tr>
                                    <tr>
                                        <td>Địa chỉ: </td>
                                        <td id="dtDiaChi"></td>
                                    </tr>
                                    <tr>
                                        <td>Nội dung: </td>
                                        <td id="dtNoiDung"></td>
                                    </tr>
                                    <tr>
                                        <td>Cơ quan xử lý: </td>
                                        <td id="dtCoQuanXuLy"></td>
                                    </tr>
                                    <tr>
                                        <td>Cơ quan giải quyết: </td>
                                        <td id="dtCoQuanGiaiQuyet"></td>
                                    </tr>
                                    <tr>
                                        <td>Hướng xử lý: </td>
                                        <td id="dtHuongXuLy"></td>
                                    </tr>
                                    <tr>
                                        <td>Văn bản trả lời: </td>
                                        <%--<td id="dtVanBanTraLoi"></td>--%>
                                    </tr>
                                    <tr>
                                        <div class="box-body">
                                            <div class="table-responsive">
                                                <table id="fileTable" class="table table-bordered table-hover table-responsive" style="margin-top: 15px; width: 100%">
                                                    <thead>
                                                        <tr>
                                                            <th style="text-align: center">STT
                                                            </th>
                                                            <th style="text-align: center">Tên file
                                                            </th>
                                                            <th style="text-align: center">Ngày cập nhật
                                                            </th>
                                                            <th style="text-align: center">Thao tác
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </tr>
                                </table>
                            </div>
                            <div class="box-footer">
                                <div style="text-align: center">
                                    <button onclick='hideDetail(); return false;' class="btn btn-sm btn-default">Quay lại</button>
                                </div>
                            </div>
                        </div>

                        <div class="panel panel-primary panelDetail2" style="width: 100%;" hidden="hidden">
                            <div class="panel-heading">
                                THÔNG TIN VĂN BẢN TRẢ LỜI CÔNG DÂN
                            </div>
                            <div class="panel-body">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-lg-3" style="display: none;">
            <uc1:SideBarTinNoiBat runat="server" ID="SideBarTinNoiBat" />
        </div>
    </div>
</asp:Content>
