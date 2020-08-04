<%@ Page Title="" Language="C#" MasterPageFile="~/Frontend.Master" AutoEventWireup="true" EnableEventValidation="true" CodeBehind="TraCuuTrangThaiHoSo.aspx.cs" Inherits="Com.Gosol.CMS.Web.Webapp.Frontend.TraCuuTrangThaiHoSo" %>

<%@ Register Src="~/Webapp/Frontend/SideBarTinNoiBat.ascx" TagPrefix="uc1" TagName="SideBarTinNoiBat" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link href="/AdminLte/plugins/select2/select2.min.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/AdminLte/plugins/select2/select2.full.min.js"></script>
    <style type="text/css">
        .table-sm td:first-child {
            font-weight: bold;
        }

        .table-sm > tbody > tr > td {
            padding: 5px !important;
        }

        .div-content {
            padding-left: 30px;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {

            $(".select2").select2({
                placeholder: "",
                width: '100%'
            });

            //getCoQuanParent();
            getCoQuan_C1();
            var keyword = getUrlParameter("keyword");
            if (keyword != "") {
                $("#MainContent_txtSoDonThu").val(keyword);
                getData();
            }




            $("#cbCMND").change(function () {
                if ($("#cbCMND").is(":checked") == true) {
                    $("#MainContent_txtSoDonThu").attr("disabled", "disabled");
                    $("#MainContent_txtCMND").removeAttr("disabled");
                }
                else {
                    $("#MainContent_txtSoDonThu").removeAttr("disabled");
                    $("#MainContent_txtCMND").attr("disabled", "disabled");
                }
            });

            $(".liTraCuu").addClass("active");
        });


        function changeSearchType(isCMND) {
            if (isCMND == "CMND") {
                $("#MainContent_txtSoDonThu").val("");
                $("#MainContent_txtSoDonThu").attr("readonly", "readonly");
                $("#MainContent_txtCMND").removeAttr("readonly");
            }
            else {
                $("#MainContent_txtCMND").val("");
                $("#MainContent_txtCMND").attr("readonly", "readonly");
                $("#MainContent_txtSoDonThu").removeAttr("readonly");
            }
        }
        var getUrlParameter = function getUrlParameter(sParam) {
            var sPageURL = window.location.search.substring(1),
                sURLVariables = sPageURL.split('&'),
                sParameterName,
                i;

            for (i = 0; i < sURLVariables.length; i++) {
                sParameterName = sURLVariables[i].split('=');

                if (sParameterName[0] === sParam) {
                    return sParameterName[1] === undefined ? true : decodeURIComponent(sParameterName[1]);
                }
            }
        };
        function getCoQuanParent() {
            var url = '<%= System.Configuration.ConfigurationManager.AppSettings["APIUrl_GetCoQuanParent"].ToString() %>';
            $("#MainContent_ddlCoQuanParent").html("");
            var arrLS = [];
            var listChil_Defalut = {};
            listChil_Defalut.id = 0;
            listChil_Defalut.text = "Tất cả";
            arrLS.push(listChil_Defalut);
            $.ajax({
                url: url,
                type: "GET",
                success: function (data) {
                    console.log(data);
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
            var url = '<%= System.Configuration.ConfigurationManager.AppSettings["APIUrl_GetCoQuan_By_ParentID"].ToString() %>';
            //var coQuanChaID = $("#MainContent_ddlCoQuanParent").val();
            $("#MainContent_ddlCoQuan_C1").html("");
            var arrLS = [];
            var listChil_Defalut = {};
            listChil_Defalut.id = 0;
            listChil_Defalut.text = "Tất cả";
            arrLS.push(listChil_Defalut);
            //if (coQuanChaID > 0 && coQuanChaID != null && coQuanChaID != "") {
            $.ajax({
                url: url + 1,
                type: "GET",
                success: function (data) {
                    if (data != null && data != "") {
                        for (var i = 0; i < data.length; i++) {
                            var id = data[i].CoQuanID;
                            var text = data[i].TenCoQuan;
                            var listChil = {};
                            listChil.id = id;
                            listChil.text = text;
                            arrLS.push(listChil);
                        }
                        $("#MainContent_ddlCoQuan_C1").select2({ data: arrLS });
                        $(".ddlCoQuan_C1").show();
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
            var url = '<%= System.Configuration.ConfigurationManager.AppSettings["APIUrl_GetCoQuan_By_ParentID"].ToString() %>';
            var coQuanChaID = $("#MainContent_ddlCoQuan_C1").val();
            $("#MainContent_ddlCoQuan_C2").html("");
            var arrLS = [];
            var listChil_Defalut = {};
            listChil_Defalut.id = 0;
            listChil_Defalut.text = "Tất cả";
            arrLS.push(listChil_Defalut);
            if (coQuanChaID > 0 && coQuanChaID != null && coQuanChaID != "") {
                $.ajax({
                    url: url + coQuanChaID,
                    type: "GET",
                    success: function (data) {
                        console.log(data);
                        if (data != null && data != "") {
                            for (var i = 0; i < data.length; i++) {
                                var id = data[i].CoQuanID;
                                var text = data[i].TenCoQuan;
                                var listChil = {};
                                listChil.id = id;
                                listChil.text = text;
                                arrLS.push(listChil);
                            }
                            $("#MainContent_ddlCoQuan_C2").select2({ data: arrLS });
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

        function getData() {
            if ($("#MainContent_txtCMND").val() != "") {
                getDataCMND();
            }
            else {
                $("#sdtRequired").hide();
                var url = '<%= System.Configuration.ConfigurationManager.AppSettings["APIUrl_TraCuuTrangThai"].ToString() %>';
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

                $("#lblCoQuanID").html(coQuanID_Param);

                if (soDonThu != null && soDonThu != '') {
                    $(".loadding_qrcode").show();
                    $("#divMsgError").html("");
                    $.ajax({
                        url: url + soDonThu + "/" + coQuanID_Param,
                        type: "GET",
                        //data: '{"TCD124"}',
                        //contentType: "application/json",
                        success: function (data) {
                            $(".loadding_qrcode").hide();
                            fillData(data);
                            $(".div-content").show();
                            if (data != null) {
                                SaveHistory();
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
        }

        function getDataCMND() {
            var url = '<%= System.Configuration.ConfigurationManager.AppSettings["APIUrl_TraCuuTrangThaiByCMND"].ToString() %>';
            var cmnd = $("#MainContent_txtCMND").val();
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

            $("#lblCoQuanID").html(coQuanID_Param);

            if (cmnd != null && cmnd != '') {
                $(".loadding_qrcode").show();
                $("#divMsgError").html("");
                $.ajax({
                    url: url + cmnd + "/" + coQuanID_Param,
                    type: "GET",
                    //data: '{"TCD124"}',
                    //contentType: "application/json",
                    success: function (data) {
                        $(".loadding_qrcode").hide();
                        fillData(data);
                        $(".div-content").show();
                        if (data != null) {
                            SaveHistory();
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
            $("#lblXuLyDonThu").html(data.XuLyDonID);
            $("#lblCoQuanID").html(data.CoQuanID);
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
            $("#ltrNguoiDaiDien").html("");
            if (data.lsDoiTuongKN != null && data.lsDoiTuongKN.length > 0) {
                $("#lblNHoten").html(data.lsDoiTuongKN[0].HoTen);
                $("#lblNCMND").html(data.lsDoiTuongKN[0].CMND);
                $("#lblNGioiTinh").html(data.lsDoiTuongKN[0].GioiTinh == 0 ? "Nam" : "Nữ");
                $("#lblNDanToc").html(data.lsDoiTuongKN[0].TenDanToc);
                $("#lblNDiaChi").html(data.lsDoiTuongKN[0].DiaChiCT);
                //for (var i = 0; i < data.lsDoiTuongKN.length; i++) {
                //    var gioitinh = "Nữ";
                //    if (data.lsDoiTuongKN[i].GioiTinh == 0) {
                //        gioitinh = "Nam";
                //    }
                //    var divNguoiDaiDien = "";
                //    if (data.lsDoiTuongKN[i].HoTen != null && data.lsDoiTuongKN[i].HoTen != "") {
                //        divNguoiDaiDien += "<div class='col-md-2'></div><div class='col-md-10'><b>Người đại diện " + (i + 1) + "</b><br/><br/><label>Họ tên: </label><span class='info spanHoTen'>" + data.lsDoiTuongKN[i].HoTen + "</span><br/><br/>";
                //    }
                //    if (data.lsDoiTuongKN[i].CMND != null && data.lsDoiTuongKN[i].CMND != "") {
                //        divNguoiDaiDien += " <label>CMND: </label><span class='info spanCMND'>" + data.lsDoiTuongKN[i].CMND + "</span><br/><br/> <label>Giới tính: </label><span class='info'>" + gioitinh + "</span><br/><br/>";
                //    }
                //    if (data.lsDoiTuongKN[i].NgheNghiep != null && data.lsDoiTuongKN[i].NgheNghiep != "") {
                //        divNguoiDaiDien += " <label>Nghề nghiệp: </label><span class='info'>" + data.lsDoiTuongKN[i].NgheNghiep + "</span><br/><br/>";
                //    }
                //    if (data.lsDoiTuongKN[i].TenQuocTich != null && data.lsDoiTuongKN[i].TenQuocTich != "") {
                //        divNguoiDaiDien += " <label>Quốc tịch: </label><span class='info'>" + data.lsDoiTuongKN[i].TenQuocTich + "</span><br/><br/>";
                //    }
                //    if (data.lsDoiTuongKN[i].TenDanToc != null && data.lsDoiTuongKN[i].TenDanToc != "") {
                //        divNguoiDaiDien += "  <label>Dân tộc: </label> <span class='info'>" + data.lsDoiTuongKN[i].TenDanToc + "</span> <br /> <br />";
                //    }
                //    if (data.lsDoiTuongKN[i].DiaChiCT != null && data.lsDoiTuongKN[i].DiaChiCT != "") {
                //        divNguoiDaiDien += " <label>Địa chỉ: </label> <span class='info spanDiaChi'>" + data.lsDoiTuongKN[i].DiaChiCT + "</span> <br /></div > ";
                //    }
                //    $("#ltrNguoiDaiDien").html($("#ltrNguoiDaiDien").html() + divNguoiDaiDien);
                //}
                //$(".ltrNguoiDaiDien").show();
            }
            else {
                $(".ltrNguoiDaiDien").hide();
            }

            $("#lblDoiTuongKhieuNai").html("");
            if (data.NhomKNInfo != null) {
                if (data.NhomKNInfo.StringLoaiDoiTuongKN == "CaNhan") {
                    $("#lblDoiTuongKhieuNai").html("Cá nhân");
                }
                if (data.NhomKNInfo.StringLoaiDoiTuongKN == "CoQuan") {
                    $("#lblDoiTuongKhieuNai").html("Cơ quan, tổ chức");
                    $("#lblTenCoQuan").html(data.NhomKNInfo.TenCQ);
                    $("#lblDiaChiCoQuan").html(data.NhomKNInfo.DiaChiCQ);
                    $(".lblTenCoQuan").show();
                    $(".lblDiaChiCoQuan").show();
                }
                else {
                    $(".lblTenCoQuan").hide();
                    $(".lblDiaChiCoQuan").hide();
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
        }

        function SaveHistory() {
            $.ajax({
                type: "POST",
                url: "TraCuuTrangThaiHoSo.aspx/SaveHistory",
                data:
                    '{ XuLyDonID:"' + $("#lblXuLyDonThu").text() + '",' +
                    'SoDonThu:"' + $("#lblSoDonThu").text() + '",' +
                    'NgayTiepNhan:"' + $("#lblNgayTiepNhan").text() + '",' +
                    'PhanLoaiDon:"' + $("#lblPhanLoaiDon").text() + '",' +
                    'NoiDungDon:"' + $("#lblNoiDungDon").text() + '",' +
                    'DoiTuongKhieuNai:"' + $("#lblDoiTuongKhieuNai").text() + '",' +
                    'HuongXuLy:"' + $("#lblHuongXuLy").text() + '",' +
                    'CoQuanXuLy:"' + $("#lblCoQuanXuLy").text() + '",' +
                    'CanBoXuLy:"' + $("#lblCanBoXuLy").text() + '",' +
                    'CoQuanTiepNhan:"' + $("#lblCoQuanTiepNhan").text() + '",' +
                    'CanBoTiepNhan:"' + $("#lblCanBoTiepNhan").text() + '",' +
                    'CMND:"' + $(".spanCMND").text() + '",' +
                    'NguoiDaiDien:"' + $(".spanHoTen").text() + '",' +
                    'DiaChi:"' + $(".spanDiaChi").text() + '",' +
                    'TrangThaiDonThu:"' + $("#lblTrangThaiXuLy").text() + '",' +
                    'CoQuanID:"' + $("#lblCoQuanID").text() +
                    '"}',
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {

                },
                error: function (data) {

                }
            });
        }
    </script>
        <script type="text/javascript" src="/Scripts/cameraOption.js"></script>
    <div class="col-md-9">
        <div class="row">
            <div class="col-md-12 content-left body-backgroud">
                <div class="clear-both"></div>
                <div class="box-body">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <%--<label class="control-label col-md-2">Chọn cơ quan: </label>--%>
                            <%--<div class="col-md-3">
                        <asp:DropDownList ID="ddlCoQuanParent" CssClass="select2 form-control" runat="server" DataTextField="TenCoQuan" DataValueField="CoQuanID" Width="100%" onchange="getCoQuan_C1();return false"></asp:DropDownList>
                    </div>--%>
                            <%--<div class="col-md-3 ddlCoQuan_C1" style="display: block">
                        <asp:DropDownList ID="ddlCoQuan_C1" CssClass="select2 form-control" runat="server" DataTextField="TenCoQuan" DataValueField="CoQuanID" Width="100%" onchange="getCoQuan_C2();return false"></asp:DropDownList>
                    </div>
                    <div class="col-md-3 ddlCoQuan_C2" style="display: none">
                        <asp:DropDownList ID="ddlCoQuan_C2" CssClass="select2 form-control" runat="server" DataTextField="TenCoQuan" DataValueField="CoQuanID" Width="100%"></asp:DropDownList>
                    </div>--%>
                        </div>
                        <div class="form-group">
                            <label class="control-label col-md-2">Số đơn thư</label>
                            <div class="col-md-8">
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtSoDonThu" onfocus='changeSearchType("SDT");'></asp:TextBox>
                                <label style="color: red; display: none" id="sdtRequired">Vui lòng nhập số đơn thư!</label>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Vui lòng nhập số đơn thư!" ControlToValidate="txtCMND" ForeColor="Red" SetFocusOnError="true"
                                Display="Dynamic" ValidationGroup="vldSoDonThu"></asp:RequiredFieldValidator>--%>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="control-label col-md-2">Số CMND</label>
                            <div class="col-md-8">
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtCMND" onfocus='changeSearchType("CMND");'></asp:TextBox>
                                <label style="color: red; display: none" id="cmndRequired">Vui lòng nhập số CMND!</label>
                                <label style="color: red; display: none" id="cmndInvalid">Số CMND không hợp lệ!</label>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Vui lòng nhập số CMND!" ControlToValidate="txtCMND" ForeColor="Red" SetFocusOnError="true"
                                Display="Dynamic" ValidationGroup="vldCMND"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="txtCMND" runat="server"
                                ErrorMessage="Số CMND không hợp lệ!" ValidationExpression="^[0-9]+$" ForeColor="Red" SetFocusOnError="true" ValidationGroup="vldCMND">
                            </asp:RegularExpressionValidator>--%>
                            </div>
                            <div class="col-md-2" style="display:none">
                                <input type="checkbox" id="cbCMND" />
                            </div>
               <%--               <input type="file" name="image" accept="image/*" capture="user" />
                              <input type="file" name="video" accept="video/*" capture="capture" />
                              <input type="file" name="image2" accept="image/*" capture="capture" />--%>
                        </div>
                         <div id="videoContain" class="col-md-12 text-center form-group" hidden="hidden">
                        <video id="video" width="900" height="600" style="border: 1px solid red"></video>
                        <div id="sourceSelectPanel" style="display: none">
                            <label for="sourceSelect">Đổi camera:</label>
                            <select id="sourceSelect" style="max-width: 400px"></select>
                            <a class="btn btn-sm btn-danger" onclick="CancelQR()">Huỷ quét</a>
                        </div>
                    </div>
                        <div class="form-group">
                            <div class="col-md-11 text-center">
                                <asp:Button ID="btnSearch" runat="server" CausesValidation="false" CssClass="btn btn-sm btn-primary" Text="Tìm kiếm" OnClientClick="getData(); return false" ValidationGroup="vldSoDonThu" />
                                <%--<asp:Button ID="btnCMND" runat="server" CausesValidation="false" CssClass="btn btn-sm" Text="Tìm kiếm theo số CMND" OnClientClick="getDataCMND(); return false" ValidationGroup="vldCMND" />--%>
                                   <button class="btn btn-sm btn-warning" onclick="QRCode(); return false;">QRCode</button>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
            <div class="loadding_qrcode" style="display: none"></div>
            <div class="col-lg-12" id="divMsgError" style="text-align: center; color: red"></div>

            <div class="div-content" style="display: none">
                <div class="clear-both"></div>
                <div class="box box-primary">
                    <div class="box-header" style="text-align: center">
                        <span style="font-size: 24px; font-family: inherit; font-weight: 500; line-height: 1.1;">Thông tin trạng thái đơn thư</span>
                    </div>
                    <div class="clear-both"></div>
                    <div style="display: none">
                        <label id="lblXuLyDonThu"></label>
                    </div>
                    <div style="display: none">
                        <label id="lblCoQuanID"></label>
                    </div>
                    <div class="box-body">
                        <table class="table table-responsive table-sm">
                            <tr>
                                <td class="col-lg-3 col-md-3 col-xs-3 col-sm-3">Số đơn:</td>
                                <td class="col-sm-9 col-lg-9 col-md-9 col-xs-9" id="lblSoDonThu"></td>
                            </tr>
                            <tr>
                                <td>Ngày tiếp nhận:</td>
                                <td id="lblNgayTiepNhan"></td>
                            </tr>
                            <tr>
                                <td>Cơ quan tiếp nhận:</td>
                                <td id="lblCoQuanTiepNhan"></td>
                            </tr>
                            <tr>
                                <td class="col-sm-3">Cơ quan xử lý:</td>
                                <td class="col-sm-9" id="lblCoQuanXuLy"></td>
                            </tr>

                            <tr>
                                <td>Ngày xử lý :</td>
                                <td id="lblNgayXuLy"></td>
                            </tr>

                            <tr>
                                <td>Trạng thái đơn thư:</td>
                                <td id="lblTrangThaiXuLy"></td>
                            </tr>

                            <tr style="display: none;">
                                <td>Cán bộ tiếp nhận</td>
                                <td id="lblCanBoTiepNhan"></td>
                            </tr>
                            <tr style="display: none;">
                                <td>Phân loại đơn:</td>
                                <td id="lblPhanLoaiDon"></td>
                            </tr>
                            <tr style="display: none;">
                                <td>Nội dung đơn:</td>
                                <td id="lblNoiDungDon"></td>
                            </tr>
                            <tr style="display: none;">
                                <td>Đối tượng khiếu nại:</td>
                                <td id="lblDoiTuongKhieuNai"></td>
                            </tr>
                            <tr style="display: none;">
                                <td>Người đại diện:</td>
                                <td id="ltrNguoiDaiDien"></td>
                            </tr>
                        </table>
                    </div>

                </div>
                <div class="box box-primary" style="display: none;">
                    <div class="box-header" style="text-align: center">
                        <span style="font-size: 24px; font-family: inherit; font-weight: 500; line-height: 1.1;">Thông người đại diện</span>
                    </div>
                    <div class="box-body">
                        <table class="table table-responsive table-sm">
                            <tr>
                                <td class="col-sm-3">Họ tên:</td>
                                <td class="col-sm-9" id="lblNHoten"></td>
                            </tr>
                            <tr>
                                <td>CMND :</td>
                                <td id="lblNCMND"></td>
                            </tr>
                            <tr>
                                <td>Giới tính :</td>
                                <td id="lblNGioiTinh"></td>
                            </tr>
                            <tr>
                                <td>Dân tộc:</td>
                                <td id="lblNDanToc"></td>
                            </tr>
                            <tr>
                                <td>Địa chỉ :</td>
                                <td id="lblNDiaChi"></td>
                            </tr>
                        </table>
                    </div>
                </div>

                <div class="box box-primary" style="display: none;">
                    <div class="box-header" style="text-align: center">
                        <span style="font-size: 24px; font-family: inherit; font-weight: 500; line-height: 1.1;">Thông tin xử lý</span>
                    </div>
                    <div class="box-body">
                        <table class="table table-responsive table-sm">

                            <tr>
                                <td>Phòng ban xử lý :</td>
                                <td id="lblPhongBanXuLy"></td>
                            </tr>
                            <tr>
                                <td>Cán bộ xử lý :</td>
                                <td id="lblCanBoXuLy"></td>
                            </tr>


                            <tr>
                                <td>Hướng xử lý :</td>
                                <td id="lblHuongXuLy"></td>
                            </tr>
                        </table>
                    </div>
                </div>




            </div>

        </div>
    </div>

    <div class="col-md-3">
        <uc1:SideBarTinNoiBat runat="server" ID="SideBarTinNoiBat" />
    </div>

        <script type="text/javascript">
        var codeReader;
        var selectedDeviceId;
        function CancelQR() {
               $("#videoContain").attr('hidden', 'hidden');
                    codeReader.reset();
        }
        function QRCode() {
            try {
           
                 codeReader = new ZXing.BrowserQRCodeReader();
                codeReader.getVideoInputDevices()
                    .then((videoInputDevices) => {
                        if (videoInputDevices.length < 2) {
                            alert('Không hỗ trợ thiết bị này!');
                            return;
                        }
                      

                        const sourceSelect = document.getElementById('sourceSelect');
                        if (videoInputDevices.length >= 2)
                            selectedDeviceId = videoInputDevices[1].deviceId;
                        else if (videoInputDevices.length >= 1)
                            selectedDeviceId = videoInputDevices[0].deviceId;

                        if (videoInputDevices.length >= 1) {
                            videoInputDevices.forEach((element) => {
                                const sourceOption = document.createElement('option');
                                sourceOption.text = element.label;
                                sourceOption.value = element.deviceId;
                                sourceSelect.appendChild(sourceOption);
                            });
                            sourceSelect.onchange = () => {
                                selectedDeviceId = sourceSelect.value;
                            };
                            const sourceSelectPanel = document.getElementById('sourceSelectPanel');
                            sourceSelectPanel.style.display = 'block';
                        }
                        $("#videoContain").removeAttr('hidden');
                    })
                    .catch((err) => {
                        $("#videoContain").attr('hidden', 'hidden');
                        console.log(err);
                    });
                codeReader.decodeFromInputVideoDevice(selectedDeviceId, 'video').then((result) => {
                    console.log(result);
                    //window.open('/Webapp/Frontend/TraCuuDonThu.aspx?keyword=' + result.text);
                    $("#videoContain").attr('hidden', 'hidden');
                    codeReader.reset();
                    console.log('Reset.');
                    window.location.href = '/Webapp/Frontend/TraCuuTrangThaiHoSo.aspx?keyword=' + result.text;

                }).catch((e) => {
                    $("#videoContain").attr('hidden', 'hidden');
                    console.log(e);
                });
            }
            catch (e) {
                $("#videoContain").attr('hidden', 'hidden');
                console.error(e);
            }

        }

    </script>
</asp:Content>
