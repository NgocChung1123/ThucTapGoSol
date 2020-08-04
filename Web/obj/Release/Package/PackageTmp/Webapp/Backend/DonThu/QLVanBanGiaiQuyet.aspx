<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="QLVanBanGiaiQuyet.aspx.cs" Inherits="Com.Gosol.CMS.Web.Webapp.Backend.DonThu.QLVanBanGiaiQuyet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager runat="server" ID="ScriptManager1" EnablePartialRendering="true">
    </asp:ScriptManager>

    <script src="../../../Scripts/dropdownlist/chosen.jquery.js"></script>
    <link href="../../../Styles/dropdownlist/chosen.min.css" rel="stylesheet" />

    <link href="/AdminLte/ValidateForm/css/template.css" rel="stylesheet" type="text/css" />
    <link href="/AdminLte/ValidateForm/css/validationEngine.jquery.css" rel="stylesheet" type="text/css" />
    <script src="/AdminLte/ValidateForm/js/jquery.validationEngine.js" type="text/javascript"></script>
    <script src="/AdminLte/ValidateForm/js/jquery.validationEngine-vi.js" type="text/javascript"></script>
    <script src="../../../Scripts/khiem.js"></script>
    <style type="text/css">
        .tables-noborder {
            width: 100%;
            margin: 0 auto;
        }

            .tables-noborder tr td {
                height: 50px;
            }

        table#example2 tr th {
            text-align: center;
        }

        .divInsert {
            background-color: white;
            border: 1px solid;
            padding-left: 7px;
            padding-right: 7px;
            padding-top: 10px;
            padding-bottom: 10px;
            margin-bottom: 5px;
        }

        .control-label {
            text-align: left !important;
        }

        .dotdot {
            max-height: 50px;
            max-width: 300px;
            white-space: nowrap;
            overflow: hidden;
            text-overflow: ellipsis
        }
    </style>

    <script type="text/javascript">
        $(document).ready(function () {
            var config = {
                '.chosen': {}
            }

            for (var selector in config) {
                $(selector).chosen(config[selector]);
            }

            $(".chosen").trigger("chosen:updated");

            $("input.cbAction").on('change', function () {
                $("input.cbAction").not(this).prop('checked', false);
                if ($("input.cbAction").is(':checked')) {
                    $("#btnAction").show();
                    $("#MainContent_hdfDonThuID").val($(this).attr("id"));
                }
                else {
                    $("#btnAction").hide();
                    $("#MainContent_hdfDonThuID").val('');
                }
            });

            $(".select2").select2();
            $(".js-example-basic-single").select2({
            });

        });
    </script>

    <script type="text/javascript">
        function removeError() { $(".formError").remove() };
        function ShowAddForm() {
            ResetForm();
            removeError();
            $("#MainContent_hdfDonThuID1").val($("#MainContent_hdfDonThuID").val());
            $("#MainContent_hdfDonThuID").val('');
            $("#notAddForm").hide();
            $("#addForm").show();
        }

        function HideAddForm() {
            $("#notAddForm").show();
            $("#addForm").hide();
            $("#MainContent_hdfDonThuID").val($("#MainContent_hdfDonThuID1").val());
        }
        function downloadFileDonThuCT(url) {
            window.location.href = "../../../DowloadFileDonThuSync.aspx?url=" + url;
        } 

        function ShowFileVBGQForm(DonThuID) {
            $("#showFileForm").modal();
            $.ajax({
                type: "POST",
                url: "QLVanBanGiaiQuyet.aspx/GetFile",
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

                            $("#fileTable >tbody").append("<tr class='newFile' id='" + index + "'><td style='text-align:center;'><input type='hidden' value='" + fileUrl + "' /> <input type='hidden' value='' />" + index + "</td><td>" + tenHoSo + "</td><td style='text-align:center;'>" + ngayCapNhat + "</td><td style='display:none;'></td><td style='text-align:center;'><a onclick=downloadFileDonThuCT('" + fileUrl + "')><img style='cursor: pointer;' id='imgDownLoad' title='Download file đính kèm' width='22' height='20' src='../../../images/cloud_download.png'></a></td></tr>");
                        }
                    }
                }
            });
            return false;
        }
        function HideFileVBGQForm() {
            $("#showFileForm").modal("hide");
            ResetForm();
            return false;
        }

        function ShowEditForm() {
            removeError();
            //var donThuId = $("#MainContent_hdfDonThuID").val();
            var donThuId = 0;
            var listRow = document.getElementsByClassName("cbAction");
            for (var i = 0; i < listRow.length; i++) {
                if (listRow[i].checked) {
                    donThuId = listRow[i].value;
                    break;
                }
            }
            $.ajax({
                type: "POST",
                url: "QLVanBanGiaiQuyet.aspx/GetByID",
                data: '{donThuId:"' + donThuId + '"}',
                dataType: "json",
                async: "true",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    var json = eval('(' + data.d + ')');
                    if (json != null) {
                        $('#MainContent_hdfDonThuID').val(donThuId);
                        $('#MainContent_txtSoDon').val(json.SoDonThu);
                        $('#MainContent_txtNgayTiepNhan').datepicker('setDate', json.NgayTiepNhan);
                        $('#MainContent_ddlCoQuan').val(json.CoQuanID).change();
                        //$("#MainContent_ddlCoQuan").attr("disabled", "disabled");
                        $('#MainContent_txtHoTen').val(json.NguoiDaiDien);
                        $('#MainContent_txtDiaChi').val(json.DiaChi);
                        $('#MainContent_txtNoiDungDon').val(json.NoiDungDon);
                        $('#MainContent_txtNgayBanHanh').datepicker('setDate', json.NgayBanHanh);
                        $('#MainContent_txtNgayXuLy').datepicker('setDate', json.NgayXuLy);
                        $('#MainContent_ddlCoQuanBanHanh').val(json.CoQuanBanHanhID).change();
                        $('#MainContent_ddlCoQuanXuLy').val(json.CoQuanXuLyID).change();
                        $('#MainContent_ddlCoQuanGiaiQuyet').val(json.CoQuanGiaiQuyetID).change();
                        $('#MainContent_ddlHuongXuLy').val(json.TrangThaiDonThu);
                        $('#MainContent_txtTienPhaiThu').val(json.SoTienPhaiThu);
                        $('#MainContent_txtDatPhaiThu').val(json.SoDatPhaiThu);
                        $('#MainContent_txtDoiTuongBiXuLy').val(json.SoDoiTuongBiXuLy);
                        $('#MainContent_txtTenQuyetDinh').val(json.TenQuyetDinh);
                        if (json.CongKhai == "1") {
                            $('#MainContent_cbCongKhai1').attr('checked', 'checked');
                        } else {
                            $('#MainContent_cbCongKhai1').removeAttr('checked');
                        }
                        if (json.FileQuyetDinh != "") {
                            $("#lblFile").show();
                        }
                        else {
                            $("#lblFile").hide();
                        }
                        $("#notAddForm").hide();
                        $("#addForm").show();
                    }
                    removeError();
                }
            });
            return false;
        }

        function ResetForm() {
            //$("#MainContent_hdfDonThuID").val('');
            $('#MainContent_txtSoDon').val('');
            $('#MainContent_txtNgayTiepNhan').val('').datepicker('update');
            $('#MainContent_ddlCoQuan').val('');
            $('#MainContent_txtHoTen').val('');
            $('#MainContent_txtDiaChi').val('');
            $('#MainContent_txtNoiDungDon').val('');
            $('#MainContent_txtNgayXuLy').val('').datepicker('update');
            $('#MainContent_txtCoQuanXuLy').val('0');
            $('#MainContent_ddlHuongXuLy').val('0');
            $('#MainContent_txtNgayBanHanh').val('');

            $('#MainContent_ddlCoQuan').val('0').change();
            $('#MainContent_ddlCoQuanBanHanh').val('0').change();
            $('#MainContent_ddlCoQuanXuLy').val('0').change();
            $('#MainContent_ddlCoQuanGiaiQuyet').val('0').change();
            $('#MainContent_txtTienPhaiThu').val('');
            $('#MainContent_txtDatPhaiThu').val('');
            $('#MainContent_txtDoiTuongBiXuLy').val('');
        }

        function CheckValidate() {

        }

        function CapNhatKNTC() {
            //var url = "http://192.168.100.42:10008/api/GetPortalSync";

            var url = '<%= System.Configuration.ConfigurationManager.AppSettings["APIUrl_GetPortalSyncQuyetDinhGiaiQuyet"].ToString() %>';

            $.ajax({
                url: url,
                type: "GET",
                success: function (data) {
                    //console.log(data);
                    if (data != null) {
                        for (var i = 0; i < data.length; i++) {
                            if (data[i].TrangThaiDonID == 2) {
                                $('<tr>').append(
                                    $('<td style="center">').text(i + 1),
                                    $('<td>').text(data[i].SoDonThu),
                                    $('<td>').text(data[i].NguoiDaiDien),
                                    $('<td>').text(data[i].DiaChi),
                                    $('<td>').text(data[i].NoiDungDon),
                                    $('<td>').text(data[i].TenLoaiKhieuTo),
                                    $('<td>').text(data[i].NgayTiepNhan),
                                    $('<td>').text(data[i].TenCoQuanTiepNhan),
                                    $('<td>').text(data[i].NgayBanHanh)
                                ).appendTo("#records_table");
                            }
                        }
                    };
                },
                error: function (data) {
                }
            });
        }

        function DongBoKNTC() {
            $("#syncNotify").modal();
        }
        function sync() {
            $("#btnCapNhatDB").val("Đang lấy dữ liệu");
            $("#btnCapNhatDB").attr("disabled", "disabled");
            //var url = 'http://localhost:49373/api/GetPortalSyncQuyetDinhGiaiQuyet';
            var url = '<%= System.Configuration.ConfigurationManager.AppSettings["APIUrl_GetPortalSyncQuyetDinhGiaiQuyet"].ToString() %>';
            $.ajax({
                url: url,
                type: "GET",
                success: function (data) {
                    $("#btnCapNhatDB").val("Cập nhật");
                    $("#btnCapNhatDB").removeAttr("disabled");
                    if (data != null && data != "") {
                        listDonThu = JSON.stringify({
                            'listDonThu': data
                        });
                        $("#tmplShowDTDongBo > tbody").html("");
                        console.log("aa", data)
                        var stt = 1;
                        for (var i = 0; i < data.length; i++) {
                            if (data[i].TrangThaiDonID == 2) {

                                var tmpl = "<tr class='rowDonThu '>" + $("#tmplCTDTDB tr:first-child").html() + "</tr>";
                                tmpl = tmpl.replace(/_STT_/g, stt);
                                tmpl = tmpl.replace(/_row_/g, stt);
                                tmpl = tmpl.replace(/_pSoDon_/g, data[i].SoDonThu);
                                tmpl = tmpl.replace(/_pXuLyDonID_/g, data[i].XuLyDonID);
                                tmpl = tmpl.replace(/_pNgayTiepNhan_/g, data[i].NgayNhapDonStr);
                                tmpl = tmpl.replace(/_pCoQuanTiepNhan_/g, data[i].TenCoQuanTiepNhan);
                                tmpl = tmpl.replace(/_pCanBoTiepNhan_/g, data[i].TenCanBoTiepNhan);
                                tmpl = tmpl.replace(/_pCoQuanID_/g, data[i].CoQuanID);
                                var fileUrl = "";
                                var tenFile = "";
                                for (var j = 0; j < data[i].lsFileQuyetDinhGD.length;j++) {
                                    fileUrl += data[i].lsFileQuyetDinhGD[j].FileBase64+','+ data[i].lsFileQuyetDinhGD[j].TenFile+','+ data[i].lsFileQuyetDinhGD[j].FileURL+ "*";
                                }
                                tmpl = tmpl.replace(/_FileUrl_/g, fileUrl);
                                tmpl = tmpl.replace(/_FileName_/g, "");
                                var phanLoaiDon = "";

                                if (data[i].TenLoaiKhieuTo1 != "" && data[i].TenLoaiKhieuTo1 != null) {
                                    phanLoaiDon += data[i].TenLoaiKhieuTo1;

                                    if (data[i].TenLoaiKhieuTo2 != "") {

                                        phanLoaiDon += ">" + data[i].TenLoaiKhieuTo2;
                                    }
                                    if (data[i].TenLoaiKhieuTo3 != "") {

                                        phanLoaiDon += ">" + data[i].TenLoaiKhieuTo3;
                                    }

                                    tmpl = tmpl.replace(/_pPhanLoaiDon_/g, phanLoaiDon);
                                }
                                else {
                                    tmpl = tmpl.replace(/_pPhanLoaiDon_/g, " ");
                                }
                                tmpl = tmpl.replace(/_pNoiDungDon_/g, data[i].NoiDungDon);
                                var divNguoiDaiDien = "";
                                if (data[i].lsDoiTuongKN != null && data[i].lsDoiTuongKN.length > 0) {

                                    for (var j = 0; j < data[i].lsDoiTuongKN.length; j++) {


                                        if (data[i].lsDoiTuongKN[j].HoTen != null && data[i].lsDoiTuongKN[j].HoTen != "") {
                                            if (j == 0) {
                                                divNguoiDaiDien += data[i].lsDoiTuongKN[j].HoTen;
                                            }
                                            else {
                                                divNguoiDaiDien += ", " + data[i].lsDoiTuongKN[j].HoTen;
                                            }
                                        }

                                    }

                                }
                                tmpl = tmpl.replace(/_pNguoiDaiDien_/g, divNguoiDaiDien);
                                tmpl = tmpl.replace(/_pCoQuanXuLy_/g, data[i].TenCoQuanXL);
                                tmpl = tmpl.replace(/_pTrangThaiDonThu_/g, data[i].TrangThaiDonThu);
                                tmpl = tmpl.replace(/_pPhongBanXuLy_/g, data[i].TenPhongBanXuLy);

                                if (data[i].NhomKNInfo != null) {
                                    if (data[i].NhomKNInfo.StringLoaiDoiTuongKN == "CaNhan") {
                                        tmpl = tmpl.replace(/_pDoiTuongKhieuNai_/g, "Cá nhân");

                                    }
                                    if (data[i].NhomKNInfo.StringLoaiDoiTuongKN == "CoQuan") {

                                        tmpl = tmpl.replace(/_pDoiTuongKhieuNai_/g, "Cơ quan, tổ chức");
                                    }
                                    if (data[i].NhomKNInfo.StringLoaiDoiTuongKN == "TapThe") {
                                        tmpl = tmpl.replace(/_pDoiTuongKhieuNai_/g, "Tập thể");
                                    }

                                }
                                else {
                                    tmpl = tmpl.replace(/_pDoiTuongKhieuNai_/g, "");
                                }
                                $("#tmplShowDTDongBo > tbody").append(tmpl);
                                stt++;
                            }


                        }

                        $("#tmplDonThuRow").show();
                    }
                    else {
                        console.log('null');
                        alert('Không có dữ liệu mới');
                    }
                },
                error: function (data) {
                    console.log('fail');
                    $("#btnCapNhatDB").val("Cập nhật");
                    $("#btnCapNhatDB").removeAttr("disabled");
                }
            });
        };

        function saveDB() {
            $("#btnDongBoDB").val("Đang đồng bộ");
            $("#btnDongBoDB").attr("disabled", "disabled");
            console.log('saveDB');
            var listRow = document.getElementsByClassName("checkboxDT");
            var dataDonThu = "";
            //var dsFileBase64 = [];
            var dsFileName = [];
            for (var i = 0; i < listRow.length; i++) {
                //console.log(listRow[i].parentElement.parentElement);
                cells = listRow[i].parentElement.parentElement.getElementsByTagName('td');
                var isShow = $("#check" + (i + 1)).is(':checked') ? "1" : "0";
                var fileBase64 = $("#hdfFileUrl" + (i + 1)).val();
                var fileName = $("#hdfFileName" + (i + 1)).val();
                for (var j = 2; j < cells.length; j++) {
                    if (j == 2) {
                        dataDonThu += cells[2].children[0].innerText;
                        dataDonThu += "/;/" + cells[2].children[1].innerText;
                        dataDonThu += "/;/" + cells[2].children[2].innerText;
                        dataDonThu += "/;/" + cells[2].children[3].innerText;
                        dataDonThu += "/;/" + cells[2].children[4].innerText;

                    } else {
                        var noidung = cells[j].innerText.replace(/"/g, "'");
                        dataDonThu += "/;/" + noidung;
                    }
                     
                }
                dataDonThu += "/;/" + isShow;
                dataDonThu += "/;/" + fileName + "/;/";
               dataDonThu += "/;/" + fileBase64 + "/;/";
                
                //if (fileBase64 != null && fileBase64.length > 0) {
                //    dsFileBase64.push(fileBase64);
                //    dsFileName.push(fileName);


                //}

                if (i != cells.length - 1) {
                    dataDonThu += "//@//";
                }

            }
            //var demFile = 0;
            //for (var idxFile = 0; idxFile < dsFileBase64.length; idxFile++) {
            //    $.ajax({
            //        type: "POST",
            //        url: "QLVanBanGiaiQuyet.aspx/SaveFileBase64",
            //        data: '{fileName:"' + dsFileName[idxFile] + '",fileBase64:"' + dsFileBase64[idxFile] + '"}',
            //        async: "true",
            //        contentType: "application/json; charset=utf-8",
            //        success: function (data1) {
            //            demFile++;
            //            console.log('Save file ok', demFile, dsFileBase64.length);
            //            if (demFile >= dsFileBase64.length) {
            //                $("#hdfSaveFileDB").val(1);
            //                CheckKetThucDongBo();
            //            }
            //        }
            //    });


            //}

            //if (dsFileBase64.length == 0) {
            //    $("#hdfSaveFileDB").val(1);
            //    CheckKetThucDongBo();
            //}
            console.log("aaaa", dataDonThu);
            $.ajax({
                type: "POST",
                url: "QLVanBanGiaiQuyet.aspx/SaveDB",
                data: '{dataDonThu:"' + dataDonThu + '"}',
                async: "true",
                contentType: "application/json; charset=utf-8",
                success: function (data1) {
                    console.log('QLVanBanGiaiQuyet save db ssuccess');
                    var XLDIDstr = data1.d;
                    $.ajax({
                        type: "POST",
                       //url: 'http://192.168.100.42:10008/api/UpdateSyncStatus1',
                        url: '<%= System.Configuration.ConfigurationManager.AppSettings["APIUrl_UpdateSyncStatus"].ToString() %>',
                        data: { 'XLDIDstr': XLDIDstr },

                        async: "true",
                        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
                        success: function (data2) {
                            debugger
                            $("#hdfDongBoDB").val(1);
                            $("#hdfSaveFileDB").val(1);
                            CheckKetThucDongBo();
                            console.log('APIUrl_UpdateSyncStatus');

                            if (data2 == 1) {
                                //alert('Đồng bộ thành công!');
                            } else {
                                alert('Đồng bộ không thành công');
                                window.location.reload(false);
                                $("#hdfDongBoDB").val(1);
                            }
                        },
                        error: function (data1) {
                            $("#hdfDongBoDB").val(1);
                            alert('Đồng bộ không thành công');
                            $("#btnDongBoDB").val("Đồng bộ");
                            $("#btnDongBoDB").removeAttr("disabled");
                            window.location.reload(false);
                        }
                    });


                },
                error: function (data1) {
                    $("#hdfDongBoDB").val(1);
                    alert('Đồng bộ không thành công');
                    $("#btnDongBoDB").val("Đồng bộ");
                    $("#btnDongBoDB").removeAttr("disabled");
                    window.location.reload(false);
                }
            });
        }

        function CheckKetThucDongBo() {
            var dongBo = $("#hdfDongBoDB").val();
            var saveFile = $("#hdfSaveFileDB").val();
            if (dongBo == saveFile) {
                $("#hdfDongBoDB").val(2);
                window.location.reload(false);
                alert('Đồng bộ thành công!');
                $("#btnDongBoDB").val("Đồng bộ");
                $("#btnDongBoDB").removeAttr("disabled");
            }
        }

        function CheckboxSelected(rowid) {
            var obj = $("#" + rowid);
            var ischecked = obj.is(':checked');
            if (ischecked) {
                obj.addClass('checkedRow');
            } else {
                obj.removeClass('checkedRow');
            }

        }
        function ChangedIsShow(el) {
            var id = el.value;
            var isShow = el.checked;

            $.ajax({
                type: "POST",
                url: "QLVanBanGiaiQuyet.aspx/ChangedIsShow",
                data: '{IsChecked:"' + isShow + '",ID:"' + id + '"}',
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function () {

                }
            })
        }
        function removeDotDot(el) {


            if ($('#tdNoiDung' + el).hasClass('dotdot'))
                $('#tdNoiDung' + el).removeClass('dotdot');
            else
                $('#tdNoiDung' + el).addClass('dotdot');

        }
    </script>

    <section class="content-header">
        <h1>Quản lý quyết định giải quyết
        <small></small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="/Default.aspx"><i class="fa fa-dashboard"></i>Trang chủ</a></li>
            <li><a href="#">Quản lý tra cứu</a></li>
            <li class="active">Quản lý quyết định giải quyết</li>
        </ol>
    </section>

    <section class="content">
        <div class="row">
            <div class="col-xs-12">
                <div class="box box-primary">
                    <div class="box-body">
                        <input type="hidden" id="hdfDongBoDB" value="0" />
                        <input type="hidden" id="hdfSaveFileDB" value="0" />
                        <ul class="nav nav-tabs">
                            <li class="active"><a data-toggle="tab" href="#home" style="font-size: 14px; font-weight: 700">Quản lý</a></li>
                            <li><a data-toggle="tab" href="#dongbo" style="font-size: 14px; font-weight: 700">Đồng bộ</a></li>
                        </ul>

                        <div class="tab-content">
                            <div id="home" class="tab-pane fade in active" style="padding-top: 10px">
                                <div id="notAddForm">
                                    <div class="box-header" style="padding: 0px; margin-bottom: 10px">
                                        <asp:Panel runat="server" DefaultButton="btnSearch">
                                            <div class="col-lg-3 col-lg-offset-4" style="padding-right: 5px">
                                                <asp:DropDownList ID="ddlCoQuanSearch" runat="server" DataValueField="CoQuanID" DataTextField="TenCoQuan" AutoPostBack="true" OnSelectedIndexChanged="ddlCoQuanSearch_SelectedIndexChanged"
                                                    CssClass="chosen form-control">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-lg-4" style="padding-right: 0px">
                                                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control input-search" placeholder="Nhập tên chủ đơn hoặc ND đơn cần tìm kiếm">
                                                </asp:TextBox>
                                            </div>
                                            <div class="col-lg-1">
                                                <asp:Button ID="btnSearch" runat="server" CausesValidation="false" CssClass="btn btn-default btn-sm" Style="margin-bottom: 10px"
                                                    OnClick="btnSearch_Click" Text="Tìm kiếm" />

                                            </div>
                                        </asp:Panel>
                                        <div class="col-lg-offset-9 col-lg-3 text-right" style="padding-right: 0px;">
                                            <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary" Text="Thêm QĐ" OnClientClick="ShowAddForm(); return false;" />
                                            <%--<input type="button" class="btn btn-primary" value="Thêm QĐ" onclick="ShowAddForm();" />--%>
                                            <span id="btnAction" style="display: none;">
                                                <asp:Button ID="btnEdit" runat="server" CssClass="btn btn-primary" Text="Sửa" OnClientClick="ShowEditForm(); return false;" />
                                                <%--<input type="button" class="btn btn-primary" value="Sửa" onclick="return ShowEditForm();" />--%>
                                                <asp:Button ID="btnDelete1" runat="server" CssClass="btn btn-primary" Text="Xoá" OnClientClick="ConfirmDelete();return false" />
                                                <%--<input type="button" class="btn btn-primary" value="Xóa" onclick="ConfirmDelete();" />--%>
                                            </span>
                                        </div>
                                    </div>
                                    <table id="example2" class="table table-bordered table-hover table-responsive">
                                        <thead>
                                            <tr>
                                                <th>#</th>
                                                <th style="width: 5%">STT</th>
                                                <th style="width: 5%">Số đơn</th>
                                                <th style="width: 10%">Tên chủ đơn</th>
                                                <th style="width: 10%">Địa chỉ</th>
                                                <th style="width: 10%">ND đơn</th>
                                                <th style="width: 10%">Loại khiếu tố</th>
                                                <th style="width: 10%">Ngày tiếp nhận</th>
                                                <th style="width: 10%">CQ tiếp nhận</th>
                                                <th style="width: 10%">Ngày ban hành QĐ</th>
                                                <th style="width: 10%">File QĐ</th>
                                                <th style="width: 10%">Hiển thị</th>
                                            </tr>
                                        </thead>
                                        <tbody>

                                            <asp:Repeater ID="rptQuyetDinh" runat="server" OnItemDataBound="rptQuyetDinh_ItemDataBound" OnItemCommand="rptQuyetDinh_ItemCommand">
                                                <ItemTemplate>
                                                    <tr onclick='<%# "removeDotDot("+ Eval("ID") +");"  %>'>
                                                        <td>
                                                            <input type="checkbox" class="cbAction" value='<%# Eval("ID") %>' id='<%# Eval("ID") %>' />
                                                        </td>
                                                        <td style="text-align: left">
                                                            <asp:Label ID="lblSTT" Text="" runat="server" />
                                                        </td>
                                                        <td style="text-align: left">
                                                            <%# Eval("SoDonThu") %>
                                                        </td>
                                                        <td style="text-align: left">
                                                            <%# Eval("NguoiDaiDien") %>
                                                        </td>
                                                        <td style="text-align: left">
                                                            <%# Eval("DiaChi") %>
                                                        </td>
                                                        <td class="dotdot" id='tdNoiDung<%# Eval("ID") %>' style="text-align: left"><%# Eval("NoiDungDon") %></td>
                                                        <td style="text-align: left"><%# Eval("PhanLoaiDon") %></td>
                                                        <td style="text-align: left">
                                                            <%# Eval("NgayTiepNhan")==null ? "" : Com.Gosol.CMS.Utility.Format.FormatDate(Com.Gosol.CMS.Utility.Utils.ConvertToDateTime(Eval("NgayTiepNhan").ToString(),DateTime.MinValue)) %>
                                                        </td>
                                                        <td style="text-align: left"><%# Eval("CoQuanTiepNhan") %></td>
                                                        <td style="text-align: left">
                                                            <%# Eval("NgayBanHanh")==null ? "" : Com.Gosol.CMS.Utility.Format.FormatDate(Com.Gosol.CMS.Utility.Utils.ConvertToDateTime(Eval("NgayBanHanh").ToString(),DateTime.MinValue)) %>
                                                        </td>
                                                        <td style="text-align: center">
                                                            <asp:ImageButton 
                                                                ID="btnShowFile" 
                                                                runat="server" 
                                                                ImageUrl="~/images/cloud_download.png" 
                                                                CommandName="ShowFile" 
                                                                CommandArgument='<%# Eval("ID") %>' 
                                                                CausesValidation="false" 
                                                                ToolTip="Tải về" width="20px" 
                                                                style="margin-right: 5px;"
                                                                />
                                                        </td>
                                                        <td style="text-align: center">
                                                            <asp:CheckBox Style="display: none" runat="server" ID="cbCongKhai" />
                                                            <input value='<%# Eval("ID") %>' onchange="ChangedIsShow(this)" type="checkbox" <%# Eval("CongKhai").ToString() =="0"?"":"checked" %> />

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

                                <div class="panel panel-primary" id="addForm" style="display: none">
                                    <div class="panel-heading">Thêm / Sửa quyết định giải quyết</div>
                                    <div class="panel-body" style="padding: 7px">
                                        <div class="divInsert">
                                            <asp:HiddenField ID="hdfDonThuID" runat="server" />
                                            <asp:HiddenField ID="hdfDonThuID1" runat="server" />
                                            <div class="form-horizontal">
                                                <div class="form-group">
                                                    <label class="control-label col-md-2">
                                                        Số đơn <span style="color:red">(*)</span>
                                                    </label>
                                                    <div class="col-md-4">
                                                        <asp:TextBox runat="server" ID="txtSoDon" CssClass="form-control validate[required]"></asp:TextBox>
                                                    </div>

                                                </div>
                                                <div class="form-group">
                                                    <label class="control-label col-md-2">
                                                        Ngày tiếp nhận <span style="color:red">(*)</span>
                                                    </label>
                                                    <div class="col-md-4">
                                                        <asp:TextBox runat="server" ID="txtNgayTiepNhan" CssClass="form-control validate[required] validate[custom[date]] datepicker"></asp:TextBox>
                                                    </div>
                                                    <label class="control-label col-md-2">
                                                        Cơ quan tiếp nhận <span style="color:red">(*)</span>
                                                    </label>
                                                    <div class="col-md-4">
                                                        <asp:DropDownList runat="server" ID="ddlCoQuan" CssClass="select2 form-control validate[required]" DataValueField="CoQuanID" DataTextField="TenCoQuan" Style="width: 100%"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="divInsert">
                                            <h4 style="margin-top: 0px;"><b><i>Thông tin công dân</i></b></h4>
                                            <div class="form-horizontal">
                                                <div class="form-group">
                                                    <label class="control-label col-md-2">
                                                        Họ tên
                                                    </label>
                                                    <div class="col-md-4">
                                                        <asp:TextBox runat="server" ID="txtHoTen" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label class="control-label col-md-2">
                                                        Chi tiết địa chỉ
                                                    </label>
                                                    <div class="col-md-10">
                                                        <asp:TextBox runat="server" ID="txtDiaChi" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            
                                                <div class="form-group">
                                                    <label class="control-label col-md-2">
                                                        Nội dung đơn
                                                    </label>
                                                    <div class="col-md-10">
                                                        <asp:TextBox runat="server" ID="txtNoiDungDon" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="divInsert">
                                            <h4 style="margin-top: 0px;"><b><i>Thông tin xử lý</i></b></h4>
                                            <div class="form-horizontal">
                                                <div class="form-group">
                                                     <label class="control-label col-md-2">
                                                        Tên quyết định
                                                    </label>
                                                    <div class="col-md-4">
                                                        <asp:TextBox runat="server" ID="txtTenQuyetDinh" CssClass="form-control"></asp:TextBox>
                                                    </div>

                                                    <label class="control-label col-md-2">
                                                        Ngày ban hành
                                                    </label>
                                                    <div class="col-md-4">
                                                        <asp:TextBox runat="server" ID="txtNgayBanHanh" CssClass="form-control datepicker validate[custom[date]]"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="control-label col-md-2">
                                                        Số tiền phải thu
                                                    </label>
                                                    <div class="col-md-4">
                                                        <asp:TextBox runat="server" ID="txtTienPhaiThu" CssClass="form-control validate[custom[integer]]"></asp:TextBox>
                                                    </div>
                                                             <label class="control-label col-md-2">
                                                        Cơ quan ban hành
                                                    </label>
                                                    <div class="col-md-4">
                                                        <asp:DropDownList runat="server" ID="ddlCoQuanBanHanh" CssClass="select2 form-control" DataValueField="CoQuanID" DataTextField="TenCoQuan" Style="width: 100%"></asp:DropDownList>
                                                    </div>

                                                </div>
                                                <div class="form-group">
                                                    <label class="control-label col-md-2">
                                                        Số đất phải thu
                                                    </label>
                                                    <div class="col-md-4">
                                                        <asp:TextBox runat="server" ID="txtDatPhaiThu" CssClass="form-control validate[custom[integer]]"></asp:TextBox>
                                                    </div>
                                                    
                                                    <label class="control-label col-md-2">
                                                        Cơ quan xử lý
                                                    </label>
                                                    <div class="col-md-4">
                                                        <asp:DropDownList runat="server" ID="ddlCoQuanXuLy" CssClass="select2 form-control" DataValueField="CoQuanID" DataTextField="TenCoQuan" Style="width: 100%"></asp:DropDownList>
                                                    </div>

                                                </div>
                                                <div class="form-group">
                                                    <label class="control-label col-md-2">
                                                        Số đối tượng bị xử lý
                                                    </label>
                                                    <div class="col-md-4">
                                                        <asp:TextBox runat="server" ID="txtDoiTuongBiXuLy" CssClass="form-control validate[custom[integer]]"></asp:TextBox>
                                                    </div>
                                                    
                                                    <label class="control-label col-md-2">
                                                        Cơ quan giải quyết
                                                    </label>
                                                    <div class="col-md-4">
                                                        <asp:DropDownList runat="server" ID="ddlCoQuanGiaiQuyet" CssClass="select2 form-control" DataValueField="CoQuanID" DataTextField="TenCoQuan" Style="width: 100%"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="control-label col-md-2">
                                                        File quyết định
                                                    </label>
                                                    <div class="col-md-4">
                                                        <asp:FileUpload runat="server" ID="fileUpload" CssClass="form-control btn"></asp:FileUpload>
                                                        <br />
                                                        <label id="lblFile" style="display: none">Đã có file quyết định, chọn file khác để thay thế</label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <label style="padding: 2px 7px">
                                            Hiển thị         
                                            <asp:CheckBox runat="server" ID="cbCongKhai1" Width="15" Height="15" />

                                        </label>
                                    </div>
                                    <div class="panel-footer text-center">
                                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Text="Lưu lại" OnClientClick="return CheckValidate();" OnClick="btnSubmit_Click" />
                                        <button type="button" class="btn btn-danger" onclick="HideAddForm();">Hủy bỏ</button>
                                    </div>
                                </div>
                            </div>

                            <div id="dongbo" class="tab-pane fade" style="padding-top: 5px">
                                <div class="col-lg-3 col-lg-offset-9" style="text-align: right; margin-bottom: 5px;">
                                    <input type="button" id="btnCapNhatDB" value="Cập nhật" class="btn btn-primary" onclick="sync();" />
                                    <input type="button" id="btnDongBoDB" value="Đồng bộ" class="btn btn-primary" onclick="this.disabled = true; saveDB();" style="margin-right: -15px;" />
                                </div>
                                <div>
                                    <div id="tmplDonThuRow" style="display: none">
                                        <table id="tmplShowDTDongBo" class="table">
                                            <thead>
                                                <tr>
                                                    <th>Hiển thị</th>
                                                    <th style="width: auto; text-align: center">STT
                                                    </th>
                                                    <th style="width: auto; text-align: center">Số đơn
                                                    </th>
                                                    <th style="width: auto; text-align: center">Ngày tiếp nhận
                                                    </th>
                                                    <th style="width: auto; text-align: center">Cơ quan tiếp nhận
                                                    </th>
                                                    <th style="width: auto; text-align: center">Cán bộ tiếp nhận
                                                    </th>
                                                    <th style="width: auto; text-align: center">Phân loại đơn
                                                    </th>
                                                    <th style="width: auto; text-align: center">Nội dung đơn
                                                    </th>
                                                    <th style="width: auto; text-align: center">Người đại diện
                                                    </th>
                                                    <th style="width: auto; text-align: center">Cơ quan xử lý
                                                    </th>
                                                    <th style="width: auto; text-align: center">Trạng thái đơn thư
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                            </tbody>
                                        </table>
                                        <table id="tmplCTDTDB" style="display: none">
                                            <tr>
                                                <td>
                                                    <input type="checkbox" id='check_row_' name="check_row_" class="checkboxDT" checked="checked" onchange="CheckboxSelected('check_row_');" />
                                                    <input type="hidden" id="hdfFileUrl_row_" value="_FileUrl_" />
                                                    <input type="hidden" id="hdfFileName_row_" value="_FileName_" />

                                                </td>

                                                <td style="text-align: center;">_STT_
                                                </td>
                                                <td style="text-align: center;">
                                                    <label id='sodon_row_' class="check_dt_row_">_pSoDon_</label>
                                                    <label id='xulydonid_row_' class="check_dt_row_" style="display: none">_pXuLyDonID_</label>
                                                    <label style="display: none">_pDoiTuongKhieuNai_</label>
                                                    <label style="display: none">_pPhongBanXuLy_</label>
                                                    <label style="display: none">_pCoQuanID_</label>
                                                </td>
                                                <td style="text-align: center;">_pNgayTiepNhan_
                                                </td>
                                                <td style="text-align: left;">_pCoQuanTiepNhan_
                                                </td>
                                                <td style="text-align: left;">_pCanBoTiepNhan_
                                                </td>
                                                <td style="text-align: left;">_pPhanLoaiDon_
                                                </td>
                                                <td class="dotdot" style="text-align: left;">_pNoiDungDon_
                                                </td>
                                                <td style="text-align: left;">_pNguoiDaiDien_
                                                </td>
                                                <td style="text-align: left;">_pCoQuanXuLy_
                                                </td>
                                                <td style="text-align: left;">_pTrangThaiDonThu_
                                                </td>

                                            </tr>
                                        </table>

                                    </div>
                                </div>
                            </div>

                            <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="syncNotify" class="modal fade">
                                <div class="modal-dialog  modal-md">
                                    <div class="modal-content">
                                        <div class="modal-header bg-primary">
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                <span aria-hidden="true">&times;</span></button>
                                            <h4 class="modal-title">
                                                <i class="glyphicon glyphicon-bullhorn"></i>
                                                Thông báo!
                                            </h4>
                                        </div>
                                        <div class="modal-body">
                                            <asp:Label ID="Label1" runat="server" Style="color: #008d4c">
                    Đồng bộ thành công!</asp:Label>
                                        </div>
                                        <div class="modal-footer" style="text-align: center">
                                            <button type="button" class="btn btn-sm btn-danger" onclick="HideSyncNotify();">
                                                Đóng</button><br />
                                        </div>
                                        <!-- /.modal-content -->
                                    </div>
                                    <!-- /.modal-dialog -->
                                </div>
                            </div>

                            <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="successNotify" class="modal fade">
                                <div class="modal-dialog  modal-md">
                                    <div class="modal-content">
                                        <div class="modal-header bg-primary">
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                <span aria-hidden="true">&times;</span></button>
                                            <h4 class="modal-title">
                                                <i class="glyphicon glyphicon-bullhorn"></i>
                                                Thông báo!
                                            </h4>
                                        </div>
                                        <div class="modal-body">
                                            <asp:Label ID="lblContentSuccess" runat="server" Style="color: #008d4c">
                    Cập nhật thành công!</asp:Label>
                                        </div>
                                        <div class="modal-footer" style="text-align: center">
                                            <button type="button" class="btn btn-sm btn-danger" onclick="HideSuccessNotify();">
                                                Đóng</button><br />
                                        </div>
                                        <!-- /.modal-content -->
                                    </div>
                                    <!-- /.modal-dialog -->
                                </div>
                            </div>

                            <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="errorNotify" class="modal fade">
                                <div class="modal-dialog  modal-md">
                                    <div class="modal-content">
                                        <div class="modal-header bg-primary">
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                <span aria-hidden="true">&times;</span></button>
                                            <h4 class="modal-title">
                                                <i class="glyphicon glyphicon-bullhorn"></i>
                                                Thông báo!
                                            </h4>
                                        </div>
                                        <div class="modal-body">
                                            <asp:Label ID="txtError" runat="server">
                    Cập nhật thất bại!</asp:Label><div class="jquery-msgbox-buttons">
                    </div>
                                            <div class="modal-footer" style="text-align: center">
                                                <button type="button" class="btn btn-sm btn-danger" onclick="HideErrorNotify();">
                                                    Đóng</button>
                                            </div>
                                        </div>
                                        <!-- /.modal-content -->
                                    </div>
                                    <!-- /.modal-dialog -->
                                </div>
                            </div>

                            <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="deleteConfirm" class="modal fade">
                                <div class="modal-dialog  modal-md">
                                    <div class="modal-content">
                                        <div class="modal-header bg-primary">
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                <span aria-hidden="true">&times;</span></button>
                                            <h4 class="modal-title">
                                                <i class="glyphicon glyphicon-bullhorn"></i>
                                                Thông báo!
                                            </h4>
                                        </div>
                                        <div class="modal-body">
                                            <span>Bạn có chắn chắn muốn xóa?</span>
                                        </div>
                                        <div class="modal-footer" style="text-align: center">
                                            <asp:Button ID="btnDelete" runat="server" Text="Đồng ý" CssClass="btn btn-primary btn-sm" OnClick="btnDelete_Click" />
                                            <button type="button" class=" btn btn-danger btn-sm" role="button" aria-disabled="false" onclick="HideDeleteConfirm();">
                                                <span class="ui-button-text">Hủy bỏ</span>
                                            </button>
                                        </div>
                                    </div>
                                    <!-- /.modal-content -->
                                </div>
                                <!-- /.modal-dialog -->
                            </div>

                        </div>
                    </div>
                    <!-- /.box-body -->
                </div>
                <!-- /.box -->

            </div>
            <!-- /.col -->
        </div>
        <!-- /.row -->
    </section>

    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="showFileForm" style="overflow: auto;" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">File quyết định</h4>
                </div>
                <div class="modal-body">
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
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal" onclick="HideFileVBGQForm();">Đóng</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <script type="text/javascript">
        function ShowSuccessNotify() {
            $("#successNotify").modal();
        };

        function ShowErrorNotify() {
            $("#errorNotify").modal();
        };

        function ConfirmDelete() {
            $("#deleteConfirm").modal();
        }
        function HideSuccessNotify() {
            $("#successNotify").modal("hide");
        }

        function HideErrorNotify() {
            $("#errorNotify").modal("hide");
        }

        function HideDeleteConfirm() {
            $("#deleteConfirm").modal("hide");
        }

        function HideSyncNotify() {
            $("#syncNotify").modal("hide");
        }
    </script>
    <link href="/AdminLte/ValidateForm/css/template.css" rel="stylesheet" type="text/css" />
    <link href="/AdminLte/ValidateForm/css/validationEngine.jquery.css" rel="stylesheet" type="text/css" />
    <script src="/AdminLte/ValidateForm/js/jquery.validationEngine.js" type="text/javascript"></script>
    <script src="/AdminLte/ValidateForm/js/jquery.validationEngine-vi.js" type="text/javascript"></script>
    <script type="text/javascript">
        jQuery(document).ready(function () {
            // binds form submission and fields to the validation engine
            jQuery("#Form1").validationEngine({ promptPosition: "topLeft" });
        });

    </script>

</asp:Content>
