var chitietTemplate = "";
var TruongDoan = 1;
var PhoDoan = 2;
var ThuKy = 3;
var ThanhVien = 4;
/*State code */
var DuyetBCKetQuaTT_SCode = "DuyetBCKetQuaTT";
var DuyetDuThaoKLThanhTra_SCode = "DuyetDuThaoKLThanhTra";
var DuyetKHTienHanhTT_SCode = "DuyetKHTienHanhTT";
var XDDUTHAOKLTHANHTRA = "XDDuThaoKLThanhTra";
var LapDoanThanhTra_SCode = "LapDoanTT";
var XDKeHoachTienHanh_SCode = "XDKeHoachTienHanhTT";
var KTTienHanhTT_SCode = "KTTienHanhTT";
var DuyetDuThaoKLThanhTra_SCode = "DuyetDuThaoKLThanhTra";
var LapCuocTT_SCode = "LapCuocTT";
var TienHanhTT_SCode = "TienHanhTT";
var KetThucKHTT_SCode = "KetThucKHTT";



function ChiTietCuocThanhTra(cuocThanhTraID, Role, StateID, stateCode) {
    /* chi tiet ke hoach*/
    showChiTietCuocThanhTra();
    $("#hdfChiTiet_StateID").val(StateID);

    $("#hddCuocThanhTraID").val(cuocThanhTraID);

    $.post("/Handler/ChiTietCuocThanhTra/ChiTietCuocThanhTra.ashx", {
        CuocThanhTraID: cuocThanhTraID
    }).done(function (data) {
        var cuocThanhTraInfo = JSON.parse(data);
        fillChiTietCuocThanhTra(cuocThanhTraInfo);
    });

    /*lay danh sach thanh vien doan thanh tra*/
    $.post("/Handler/ChiTietCuocThanhTra/ChiTietCuocThanhTra_GetThanhVienDoan.ashx", {
        CuocThanhTraID: cuocThanhTraID
    }).done(function (data) {
        var cuocThanhTraInfo = JSON.parse(data);
        fillDanhSachThanhVienDoanTT(cuocThanhTraInfo);
    });
    
    /*lay ds file di kem doan thanh tra */
    $.post("/Handler/ChiTietCuocThanhTra/GetDSFileDiKemDoanThanhTra.ashx", {
        CuocThanhTraID: cuocThanhTraID
    }).done(function (data) {
        var thanhVienDoanInfo = JSON.parse(data);
        fillDSFileDiKem(thanhVienDoanInfo);
    });

    GetChiTietKeHoachTienHanhTT(cuocThanhTraID);
    /* luong ke hoach*/
    $.post("/Handler/ChiTietKeHoachTT/LuongDonKeHoachGetByKeHoachID.ashx", {
        KeHoachThanhTraID: cuocThanhTraID
    }).done(function (data) {
        var lsLuongKeHoach = JSON.parse(data);
        if (lsLuongKeHoach.length > 0) {
            $(function () {
                var content = '';
                for (var i = 0; i < lsLuongKeHoach.length; i++) {
                    var fileWFUrl = lsLuongKeHoach[i].FileWFUrl;
                    var tenFile = "";
                    var str = "";
                    if (fileWFUrl != null && fileWFUrl != "") {
                        tenFiles = fileWFUrl.split("/")[2];
                        tenFileDinhKem = tenFiles.split("_")[1];

                        str = "<a href='/" + fileWFUrl + "' download='file_" + tenFileDinhKem + "' target='_blank'><img id='imgDownLoad' title='Download file đính kèm' width='22' height='20' src='/images/download.png'>" + tenFileDinhKem + "</a>";
                    }
                    var stt = i + 1;
                    content += '<tr>';
                    content += '<td style="text-align: center">' + stt + '</td>';
                    content += '<td>' + lsLuongKeHoach[i].BuocThucHien + '</td>';
                    content += "<td style='text-align:center'>" + lsLuongKeHoach[i].ThoiGianThucHienStr + "</td>";
                    content += '<td>' + lsLuongKeHoach[i].CanBoThucHien + '</td>';
                    content += '<td>' + lsLuongKeHoach[i].YKienCanBo + '</td>';
                    content += '<td>' + lsLuongKeHoach[i].ThaoTac + '</td>';
                    content += '<td>' + str + '</td>';
                    content += '</tr>';
                }
                $('#tblLuongKeHoach tbody').html(content);
            });
        }
    });

    /* ket luan bao cao */
    $.post("/Handler/ChiTietLapDuThaoKetQuaTT/ChiTietLapDuThaoKetQuaTT.ashx", {
        CuocThanhTraID: cuocThanhTraID
    }).done(function (data) {
        var lapDuThaoInfo = JSON.parse(data);
        if (lapDuThaoInfo != null) {
            fillChiTietLapDuThaoKetLuanTT(lapDuThaoInfo);
        }
    });

    /*Ket luan thanh tra*/
    $.post("/Handler/ChiTietLapDuThaoKetQuaTT/ChiTietCongBoKetLuanTT.ashx", {
        CuocThanhTraID: cuocThanhTraID
    }).done(function (data) {

        var lsCongBoKetLuans = JSON.parse(data);
        fillChiTIetKetLuanThanhTra(lsCongBoKetLuans);
    });
    /*Thi hanh ket luan thanh tra*/
    $.post("/Handler/ChiTietLapDuThaoKetQuaTT/ChiTietThiHanhKetLuanTT.ashx", {
        CuocThanhTraID: cuocThanhTraID
    }).done(function (data) {

        var ThiHanhKetLuanTTInfo = JSON.parse(data);
        fillChiTietThiHanhThanhTra(ThiHanhKetLuanTTInfo);
    });

    /* show Lap du thao ket qua thanh tra and trinh du thao ket qua thanh tra*/
    var ScreenLapDuThaoKetQuaThanhTra = 10;
    var screen_KetLuanThanhTraID = Role;

    if (Role != null) {

        if (Role[1] != 0)
        {
            $("#hddKetLuanThanhTraID").val(Role[1]);
        }

        if (Role[0] == 'LapDuThaoKetQuaThanhTra') {
            if (stateCode == XDDUTHAOKLTHANHTRA) {
                $("#btnLapDuThaoKetQuaThanhTra").show();
                $("#btnLapDuThaoKetQuaThanhTra_TienHanhTT").show();
                $("#btnLapDuThaoKetQuaThanhTra_LuongTT").show();
                if (Role[1] != 0) {
                    $("#btnTrinhDuThaoKetQuaThanhTra").show();
                    $("#btnTrinhDuThaoKetQuaThanhTra_TienHanhTT").show();
                    $("#btnTrinhDuThaoKetQuaThanhTra_LuongTT").show();
                }
                else {
                    $("#btnTrinhDuThaoKetQuaThanhTra").hide();
                    $("#btnTrinhDuThaoKetQuaThanhTra_TienHanhTT").hide();
                    $("#btnTrinhDuThaoKetQuaThanhTra_LuongTT").hide();
                }
            }
            else {
                $("#btnLapDuThaoKetQuaThanhTra").hide();
                $("#btnTrinhDuThaoKetQuaThanhTra").hide();
                $("#btnLapDuThaoKetQuaThanhTra_TienHanhTT").hide();
                $("#btnLapDuThaoKetQuaThanhTra_LuongTT").hide();
            }
        }
        else {
            $("#btnLapDuThaoKetQuaThanhTra").hide();
            $("#btnTrinhDuThaoKetQuaThanhTra").hide();
            $("#btnLapDuThaoKetQuaThanhTra_TienHanhTT").hide();
            $("#btnLapDuThaoKetQuaThanhTra_LuongTT").hide();
        }
    }

    /*show duyet du thao ket luan thanh tra*/

    if (Role != null)
    {
        if (Role[0] == 'DuyetDuThaoKetLuanTT')
        {
            if (stateCode == DuyetDuThaoKLThanhTra_SCode) {
                $("#btnDuyetDuThaoKetLuanThanhTra").show();
                $("#btnDuyetDuThaoKetLuanThanhTra_TienHanhTT").show();
                $("#btnDuyetDuThaoKetLuanThanhTra_LuongTT").show();
            }
            else {
                $("#btnDuyetDuThaoKetLuanThanhTra").hide();
                $("#btnDuyetDuThaoKetLuanThanhTra_TienHanhTT").hide();
                $("#btnDuyetDuThaoKetLuanThanhTra_LuongTT").hide();
            }
        }
    }



    /*show duyet ke hoach tien hanh thanh tra*/

    if (Role != null) {
        if (Role[0] == 'DuyetKeHoachTienHanhTT') {
            if (stateCode == DuyetKHTienHanhTT_SCode) {
                $("#btnDuyetKeHoachTienHanhThanhTra").show();
                $("#btnDuyetKeHoachTienHanhThanhTra_TienHanhTT").show();
                $("#btnDuyetKeHoachTienHanhThanhTra_LuongTT").show();
            }
            else {
                $("#btnDuyetKeHoachTienHanhThanhTra").hide();
                $("#btnDuyetKeHoachTienHanhThanhTra_TienHanhTT").hide();
                $("#btnDuyetKeHoachTienHanhThanhTra_LuongTT").hide();
            }

        }
    }


    /*show duyet bao cao ket qua thanh tra*/

    if (stateCode == DuyetBCKetQuaTT_SCode) {
        $("#btnDuyetBaoCaoKetQuaTienHanhTT").show();
        $("#btnDuyetBaoCaoKetQuaTienHanhTT_TienHanhTT").show();
        $("#btnDuyetBaoCaoKetQuaTienHanhTT_LuongTT").show();
    }
    else {
        $("#btnDuyetBaoCaoKetQuaTienHanhTT").hide();
        $("#btnDuyetBaoCaoKetQuaTienHanhTT_TienHanhTT").hide();
        $("#btnDuyetBaoCaoKetQuaTienHanhTT_LuongTT").hide();
    }


    /*show xay dung ke hoach tien hanh*/
    if (Role != null) {
        if (Role[0] == 'XDKeHoachTienHanh') {
            if (stateCode == XDKeHoachTienHanh_SCode) {

                $("#btnXayDungKeHoachTienHanhTT").show();
                $("#btnXayDungKeHoachTienHanhTT_TienHanhTT").show();
                $("#btnXayDungKeHoachTienHanhTT_LuongTT").show();
            }
            else {
                $("#btnXayDungKeHoachTienHanhTT").hide();
                $("#btnXayDungKeHoachTienHanhTT_TienHanhTT").hide();
                $("#btnXayDungKeHoachTienHanhTT_LuongTT").hide();
            }
        }
    }
    

    /*show trinh ke hoach tien hanh*/
    if (Role != null) {
        if (Role[0] == 'XDKeHoachTienHanh') {
            if (Role[1] > 0) {
                if (stateCode == XDKeHoachTienHanh_SCode) {
                    $("#btnTrinhKeHoachTienHanhTT").show();
                    $("#btnTrinhKeHoachTienHanhTT_TienHanhTT").show();
                    $("#btnTrinhKeHoachTienHanhTT_LuongTT").show();
                }

                else {
                    $("#btnTrinhKeHoachTienHanhTT").hide();
                    $("#btnTrinhKeHoachTienHanhTT_TienHanhTT").hide();
                    $("#btnTrinhKeHoachTienHanhTT_LuongTT").hide();
                }

            }
            else {
                $("#btnTrinhKeHoachTienHanhTT").hide();
                $("#btnTrinhKeHoachTienHanhTT_TienHanhTT").hide();
                $("#btnTrinhKeHoachTienHanhTT_LuongTT").hide();
            }
        }
    }


    /*show bao cao ket qua thanh tra*/
    if (Role != null) {
        if (Role[0] == 'BaoCaoKetQuaThanhTra')
        {
            if (stateCode == TienHanhTT_SCode) {

                $("#btnBaoCaoKetQuaThanhTra").show();
                $("#btnBaoCaoKetQuaThanhTra_TienHanhTT").show();
                $("#btnBaoCaoKetQuaThanhTra_LuongTT").show();
            }
            else {
                $("#btnBaoCaoKetQuaThanhTra").hide();
                $("#btnBaoCaoKetQuaThanhTra_TienHanhTT").hide();
                $("#btnBaoCaoKetQuaThanhTra_LuongTT").hide();
            }
        }
    }


    /*show cong bo ket luan , thi hanh ket luan thanh tra*/
    if (Role != null)
    {
        if (Role[0] == 'CongBoKetLuanThanhTra')
        {
            if (stateCode == KTTienHanhTT_SCode) {
                if (Role[1] != 0) {

                    $("#hddCongBoKetLuanTTID").val(Role[1]);

                    $("#btnThiHanhKetLuanThanhTra").show();
                    $("#btnCongBoKetLuanThanhTra").show();
                    $("#btnThiHanhKetLuanThanhTra_TienHanhTT").show();
                    $("#btnCongBoKetLuanThanhTra_TienHanhTT").show();
                    $("#btnThiHanhKetLuanThanhTra_LuongTT").show();
                    $("#btnCongBoKetLuanThanhTra_LuongTT").show();
                }
                else {
                    $("#btnThiHanhKetLuanThanhTra").hide();
                    $("#btnCongBoKetLuanThanhTra").show();
                    $("#btnThiHanhKetLuanThanhTra_TienHanhTT").hide();
                    $("#btnCongBoKetLuanThanhTra_TienHanhTT").show();
                    $("#btnThiHanhKetLuanThanhTra_LuongTT").hide();
                    $("#btnCongBoKetLuanThanhTra_LuongTT").show();
                }
            }
            else {
                $("#btnCongBoKetLuanThanhTra").hide();
                $("#btnThiHanhKetLuanThanhTra").hide();
                $("#btnCongBoKetLuanThanhTra_TienHanhTT").hide();
                $("#btnThiHanhKetLuanThanhTra_TienHanhTT").hide();
                $("#btnCongBoKetLuanThanhTra_LuongTT").hide();
                $("#btnThiHanhKetLuanThanhTra_LuongTT").hide();
            }
        }
    }

    /*show lap doan thanh tra*/
    if (Role != null)
    {
        // role 4 doanThanhTraID
        if (Role[3] != null)
        {
            $("#hddDoanThanhTraID").val(Role[3]);        
        }
        // role 1 ThanhTraDotXuat = True
        if (Role[1] == 'LaThanhTraDotXuat')
        {
            $("#hddThanhTraDotXuat").val('True');
        }

        if (Role[0] == 'LapDoanThanhTra')
        {
            if (stateCode == LapDoanThanhTra_SCode) {

                $("#btnLapDoanThanhTra").show();
                $("#btnLapDoanThanhTra_TienHanhTT").show();
                $("#btnLapDoanThanhTra_LuongTT").show();
            }
            else {

                if (Role[1] == "LaThanhTraDotXuat") {
                    if (stateCode == TienHanhTT_SCode) {
                        if (Role[2] == 0) {
                            $("#btnLapDoanThanhTra").show();
                            $("#btnLapDoanThanhTra_TienHanhTT").show();
                            $("#btnLapDoanThanhTra_LuongTT").show();
                        }
                        else {
                            $("#btnLapDoanThanhTra").hide();
                            $("#btnLapDoanThanhTra_TienHanhTT").hide();
                            $("#btnLapDoanThanhTra_LuongTT").hide();

                        }
                    }
                    else
                    {
                        if (Role[2] != 0) {
                            $("#btnLapDoanThanhTra").hide();
                            $("#btnLapDoanThanhTra_TienHanhTT").hide();
                            $("#btnLapDoanThanhTra_LuongTT").hide();
                        }
                        else {
                            $("#btnLapDoanThanhTra").show();
                            $("#btnLapDoanThanhTra_TienHanhTT").show();
                            $("#btnLapDoanThanhTra_LuongTT").show();
                        }
                    }
                }
                else {
                    if (Role[2] != 0) {
                        $("#btnLapDoanThanhTra").hide();
                        $("#btnLapDoanThanhTra_TienHanhTT").hide();
                        $("#btnLapDoanThanhTra_LuongTT").hide();
                    }
                    else {
                        $("#btnLapDoanThanhTra").show();
                        $("#btnLapDoanThanhTra_TienHanhTT").show();
                        $("#btnLapDoanThanhTra_LuongTT").show();
                    }

                }
            }
        }
    }

    
    /*show thong tin ds cuoc thanh tra dot xuat*/

    if (Role != null) {
               
        var cuocThanhTraID = $("#hddCuocThanhTraID").val();
        

        if (Role[0] == 'DSCuocThanhTraDotXuat') {

            showChiTietDivExport(cuocThanhTraID);

            $("#btnInThanhTraDotXuat").show();
            $("#btnXuatThanhTraDotXuat").show();
            $("#btnInThanhTraDotXuat_TienHanhTT").show();
            $("#btnXuatThanhTraDotXuat_TienHanhTT").show();
            $("#btnInThanhTraDotXuat_LuongTT").show();
            $("#btnXuatThanhTraDotXuat_LuongTT").show();

            if (stateCode == LapCuocTT_SCode || stateCode == KetThucKHTT_SCode) {
                $("#btnChuyenLapDoan").show();
                if (Role[1] == "LaThanhTraDotXuat") {
                    $("#btnXoaThanhTraDotXuat").show();
                    $("#btnXoaThanhTraDotXuat_TienHanhTT").show();
                    $("#btnXoaThanhTraDotXuat_LuongTT").show();
                }
                else {
                    $("#btnXoaThanhTraDotXuat").hide();
                    $("#btnXoaThanhTraDotXuat_TienHanhTT").hide();
                    $("#btnXoaThanhTraDotXuat_LuongTT").hide();

                }
            }
            else {
                
                $("#btnChuyenLapDoan").hide();
                $("#btnChuyenLapDoan_TienHanhTT").hide();
                $("#btnChuyenLapDoan_LuongTT").hide();

                $("#btnXoaThanhTraDotXuat").hide();
                $("#btnXoaThanhTraDotXuat_TienHanhTT").hide();
                $("#btnXoaThanhTraDotXuat_LuongTT").hide();
            }
        }
        else
        {
            $("#btnInThanhTraDotXuat").hide();
            $("#btnXuatThanhTraDotXuat").hide();
            $("#btnInThanhTraDotXuat_TienHanhTT").hide();
            $("#btnXuatThanhTraDotXuat_TienHanhTT").hide();
            $("#btnInThanhTraDotXuat_LuongTT").hide();
            $("#btnXuatThanhTraDotXuat_LuongTT").hide();
        }
    }


    /*show thong tin chi tiet cuoc thanh tra*/
    $("#TienHanhTraDetailDiv").show();
    $("#ttKeHoachTienHanhTT").show();
    $("#divThongTinTienHanhThanhTra").show();
    $("#ketLuanBaoCaoThanhTra").show();

    if (stateCode == LapDoanThanhTra_SCode) {
        $("#TienHanhTraDetailDiv").hide();
        $("#ttKeHoachTienHanhTT").hide();
        $("#divThongTinTienHanhThanhTra").hide();
        $("#ketLuanBaoCaoThanhTra").hide();
    }

    if (stateCode == XDKeHoachTienHanh_SCode) {
        $("#ttKeHoachTienHanhTT").hide();
        $("#divThongTinTienHanhThanhTra").hide();
        $("#ketLuanBaoCaoThanhTra").hide();
    }

    if (stateCode == LapCuocTT_SCode) {
        $("#TienHanhTraDetailDiv").hide();
        $("#ttKeHoachTienHanhTT").hide();
        $("#divThongTinTienHanhThanhTra").hide();
        $("#ketLuanBaoCaoThanhTra").hide();
    }
    if (stateCode == DuyetKHTienHanhTT_SCode) {
        $("#ttKeHoachTienHanhTT").show();
        $("#divThongTinTienHanhThanhTra").show();
        $("#ketLuanBaoCaoThanhTra").hide();
    }

    if (stateCode == DuyetBCKetQuaTT_SCode) {
        $("#ttKeHoachTienHanhTT").show();
        $("#divThongTinTienHanhThanhTra").show();
        $("#ketLuanBaoCaoThanhTra").hide();
    }

    if (stateCode == TienHanhTT_SCode) {
        $("#ttKeHoachTienHanhTT").show();
        $("#divThongTinTienHanhThanhTra").show();
        $("#ketLuanBaoCaoThanhTra").hide();
    }

    if (stateCode == DuyetDuThaoKLThanhTra_SCode) {
        $("#ttKeHoachTienHanhTT").show();
        $("#divThongTinTienHanhThanhTra").show();
        $("#ketLuanBaoCaoThanhTra").show();
    }

    if (stateCode == KTTienHanhTT_SCode) {
        $("#ttKeHoachTienHanhTT").show();
        $("#divThongTinTienHanhThanhTra").show();
        $("#ketLuanBaoCaoThanhTra").show();
    }
}

function fillChiTietCuocThanhTra(data) {
    var htmlContent = $("#CuocThanhTraDetailDiv").html();

    htmlContent = htmlContent.replace("$tenCuocThanhTra", data.TenCuocThanhTra);
    htmlContent = htmlContent.replace("$doiTuongThanhTra", data.DoiTuongThanhTraStr);
    if (data.TenLoaiThanhTra1 != "") {
        htmlContent = htmlContent.replace("$loaiThanhTra1", data.TenLoaiThanhTra1);
    }
    else {
        htmlContent = htmlContent.replace("$loaiThanhTra1", "");
    }
    if (data.TenLoaiThanhTra2 != "") {
        htmlContent = htmlContent.replace("$loaiThanhTra2", " / " + data.TenLoaiThanhTra2);
    }
    else {
        htmlContent = htmlContent.replace("$loaiThanhTra2", "");
    }
    htmlContent = htmlContent.replace("$phamViThanhTra", data.PhamViThanhTra);
    htmlContent = htmlContent.replace("$thoiHanThanhTra", data.ThoiHanThanhTra + " Ngày");
    htmlContent = htmlContent.replace("$thoiGianTienHanh", data.ThoiGianTienHanh);
    htmlContent = htmlContent.replace("$noiDung", data.NoiDung);
    htmlContent = htmlContent.replace("$ngayNhap", data.NgayLapKeHoachStr);
    //  htmlContent = htmlContent.replace("$cBTienHanhTT", data.NgayNhapStr);

    $("#CuocThanhTraDetailDiv").html(htmlContent);
}

function duyetDuThaoKetLuanThanhTra() {
    var cuocThanhTraID = $("#hddCuocThanhTraID").val();

    $.post("/Handler/ChiTietKeHoachTT/GetCommandCodeByCuocThanhTraID.ashx", {
        CuocThanhTraID: cuocThanhTraID
    }).done(function (data) {

        $("#MainContent_ddlDuyet").html("");
        var commands = JSON.parse(data);
        if (data.length > 0) {
            for (var i = 0 ; i < commands.length ; i++) {
                $("#MainContent_ddlDuyet").append("<option value='" + commands[i].CommandCode + "'>" + commands[i].CommandName + "</option>");
            }

            var row = $("#MainContent_hdfRow").val();
            var arrDuyet = [];
            for (var i = 0 ; i < row; i++) {
                if ($("#MainContent_rptNguoiDung_CheckBox2_" + i).is(":checked")) {
                    arrDuyet.push(i);
                }
            }

            var commandCode = $("#MainContent_ddlDuyet").val();
            $("#MainContent_hdfCommandCode").val(commandCode);
            $("#MainContent_hdfCuocThanhTraID").val(cuocThanhTraID);

            $("#MainContent_hdfDuThaoKetLuanTT").val(arrDuyet);

            $("#duyetConfirm").modal("show");
            return false;

        }
    });



}

function lapDuThaoKetQuaThanhTra() {

    var cuocThanhTraID = $("#hddCuocThanhTraID").val();
    var ketLuanThanhTraID = $("#hddKetLuanThanhTraID").val();
    ResetFormLapDuThao();

    $("#MainContent_hdfCuocThanhTraID").val(cuocThanhTraID);

    $("#MainContent_hdfKetLuanThanhTraID").val(ketLuanThanhTraID);
    fillThongTinLapDuThaoKetQuaTT(cuocThanhTraID);
    $("#lapDuThaoKeQuaTT").modal("show");
}

function trinhDuThaoKetQuaThanhTra() {
    $("#trinhDuyetConfirm").modal();
    return false;
}

function xayDungKeHoachTienHanhTT() {
    var cuocThanhTraID = $("#hddCuocThanhTraID").val();

    $("#MainContent_hdfCuocThanhTraID").val(cuocThanhTraID);
    $("#LapKHTienHanhTTDiv").modal();

    fillThongTinKeHoachThanhTra(cuocThanhTraID);

    var config = {
        '.chosen': {}
    }
    for (var selector in config) {
        $(selector).chosen(config[selector]);

    }
    $(".chosen").trigger("chosen:updated");

    return false;
}

function congBoKetLuanThanhTra()
{
    ResetCongBoKetLuan();
    ResetThiHanhKetLuan();
    var cuocThanhTraID = $("#hddCuocThanhTraID").val();
    $("#MainContent_hdfCuocThanhTraID").val(cuocThanhTraID);
    fillThongTinCongBoKetLuanTT(cuocThanhTraID);
    var config = {
        '.chosen': {}
    }
    for (var selector in config) {
        $(selector).chosen(config[selector]);

    }
    $(".chosen").trigger("chosen:updated");
    $("#congBoKetLuanThanhTra").modal();
    return false;
}

function chuyenLapDoan()
{
    var cuocThanhTraID = $("#hddCuocThanhTraID").val();
    $("#MainContent_hdfCuocThanhTraDotXuatID").val(cuocThanhTraID);

    $("#trinhDuyetConfirm").modal();
    return false;
}

function xoaThanhTraDotXuat()
{
    var cuocThanhTraID = $("#hddCuocThanhTraID").val();
    $("#MainContent_hdfCuocThanhTraDotXuatID").val(cuocThanhTraID);

    $("#deletetConfirm").modal();
    return false;
}

function thiHanhKetLuanThanhTra()
{
    ResetCongBoKetLuan();
    ResetThiHanhKetLuan();
    var cuocThanhTraID = $("#hddCuocThanhTraID").val();
    var thiHanhKetLuanID = $("#hddCongBoKetLuanTTID").val();


    $("#MainContent_hdfCuocThanhTraID").val(cuocThanhTraID);
    $("#MainContent_hdfCongBoKetLuanID").val(thiHanhKetLuanID);
    fillThongTinThiHanhKetLuanTT(cuocThanhTraID);
    var config = {
        '.chosen': {}
    }
    for (var selector in config) {
        $(selector).chosen(config[selector]);

    }
    $(".chosen").trigger("chosen:updated");
    $("#thiHanhKetLuanThanhTra").modal();
    return false;
}

function fillThongTinThiHanh(cuocThanhTraID)
{
    $.post("/Handler/ChiTietLapDuThaoKetQuaTT/ChiTietThiHanhKetLuanTT.ashx", {
        CuocThanhTraID: cuocThanhTraID
    }).done(function (data) {

        var lsThiHanhKetLuanTT = JSON.parse(data);
        if (lsThiHanhKetLuanTT.length > 0) {
            for (var i = 0 ; i < lsThiHanhKetLuanTT.length ; i++) {
                $("#MainContent_hdfThiHanhKetLuanID").val(lsThiHanhKetLuanTT[i].ThiHanhID);
                $("#MainContent_txtNgayThiHanh").val(lsThiHanhKetLuanTT[i].NgayThiHanh);
                $("#MainContent_txtTienDaThu").val(lsThiHanhKetLuanTT[i].TienDaThu);
                $("#MainContent_txtDatDaThu").val(lsThiHanhKetLuanTT[i].DatDaThu);
                $("#MainContent_ddlCoQuanThiHanh").val(lsThiHanhKetLuanTT[i].CoQuanThiHanh);


                $("#MainContent_hdfFileUpload_thi_hanh").val(lsThiHanhKetLuanTT[i].FileWFUrl);
                var file = $("#MainContent_hdfFileUpload_thi_hanh").val();
                if (file != null && file != "") {
                    $("#tenfileBH_thi_hanh").text(lsThiHanhKetLuanTT[i].TenFile);
                    $("#imgfile_thi_hanh").show();
                    //var fileUrl = window.location.host + "/" + lsThiHanhKetLuanTT[i].FileWFUrl;
                    var fileUrl = lsThiHanhKetLuanTT[i].FileWFUrl;
                    document.getElementById("btnTaiFileQD_thi_hanh").href = fileUrl;
                    document.getElementById("btnTaiFileQD_thi_hanh").download = lsThiHanhKetLuanTT[i].TenFile;
                }
                else {
                    $("#tenfileBH_thi_hanh").text("");
                    $("#imgfile_thi_hanh").hide();
                    document.getElementById("btnTaiFileQD_thi_hanh").href = "";
                }

            }

            $(".chosen").trigger("chosen:updated");
        }

    });
}

function trinhKeHoachTienHanhTT() {
    var cuocThanhTraID = $("#hddCuocThanhTraID").val();

    $("#MainContent_hdfCuocThanhTraID").val(cuocThanhTraID);

    $("#trinhDuyetConfirm").modal();

    return false;
}

function baoCaoKetQuaThanhTra() {
    var cuocThanhTraID = $("#hddCuocThanhTraID").val();

    $("#MainContent_hdfCuocThanhTraID").val(cuocThanhTraID);
    $("#trinhDuyetConfirm").modal();

    return false;
}

function inThanhTraDotXuat(divId)
{
    //var cuocThanhTraID = $("#hddCuocThanhTraID").val();
    //showChiTietDivExport(cuocThanhTraID);

    var printWindow = window.open('', '_newtab', 'height=400,width=630');

    printWindow.document.write('<html>');
    // printWindow.document.write('<head><title>DIV Contents</title></head>');
    printWindow.document.write('<body>');
    printWindow.document.write($("#" + divId).html());
    printWindow.document.write('</body></html>');
    printWindow.document.close();
    printWindow.print();
    printWindow.close();
}
function xuatThanhTraDotXuat(div)
{
    

    var trList = $("#" + div + " tr:not('.no-border')");
    trList.css("border", "0.6pt solid #999");
    trList.css("color", "black");
    var url = 'data:application/vnd.ms-excel,' + encodeURIComponent($('#' + div).html());

    var link = document.createElement("a");
    var ten = "cuoc_thanh_tra_dot_xuat.xls";
    link.download = ten;
    link.href = url;
    link.target = "_blank";
    $("#download_td").html("");
    document.getElementById("download_td").appendChild(link);
    link.click();
}

function duyetKeHoachTienHanhThanhTra() {

    var cuocThanhTraID = $("#hddCuocThanhTraID").val();

    $.post("/Handler/ChiTietKeHoachTT/GetCommandCodeByCuocThanhTraID.ashx", {
        CuocThanhTraID: cuocThanhTraID
    }).done(function (data) {

        $("#MainContent_ddlDuyet").html("");
        var commands = JSON.parse(data);
        if (data.length > 0) {
            for (var i = 0 ; i < commands.length ; i++) {
                $("#MainContent_ddlDuyet").append("<option value='" + commands[i].CommandCode + "'>" + commands[i].CommandName + "</option>");
            }

            var row = $("#MainContent_hdfRow").val();
            var arrDuyet = [];
            for (var i = 0 ; i < row; i++) {
                if ($("#MainContent_rptNguoiDung_CheckBox2_" + i).is(":checked")) {
                    arrDuyet.push(i);
                }
            }

            var commandCode = $("#MainContent_ddlDuyet").val();
            $("#MainContent_hdfCommandCode").val(commandCode);

            $("#MainContent_hdfDuThaoKetLuanTT").val(arrDuyet);
            $("#MainContent_hdfCuocThanhTraID").val(cuocThanhTraID);

            $("#DuyetConfirm").modal();
        }
    });
}

function lapDoanThanhTra()
{
    Reset();
    ResetForm();
    $("#LapDoanDiv").modal();
    $("#MainContent_ddlPhanLoaiDoan").val("doannoibo");
    ddlPhanLoaiDoanChange();

    $("#MainContent_btnSaveNoiBo").show();
    $("#MainContent_btnSaveLienNganh").hide();

    var thanhTraDotXuat = $("#hddThanhTraDotXuat").val();

    if (thanhTraDotXuat == "True") {
        $("#divCheckBoQuaXayDungKHTT").show();
    }


    var coQuanLs = $("#coQuanList").html();
    var truongDoan = $("#canBoTruongDoanLs").html();

    $('#ddlThanhVienNoiBo1').append(truongDoan);
    $("#ddlCoQuanLienNganh1").append(coQuanLs);
    $("#noibo > form-horizontal").html("");

    initDoanTTLienNganh();
    initDoanTTNoiBo();

    var cuocThanhTraID = $("#hddCuocThanhTraID").val();
    var doanThanhTraID = $("#hddDoanThanhTraID").val();


    $("#MainContent_hdfCuocThanhTraID").val(cuocThanhTraID);
    $("#MainContent_hdfDoanThanhTraID").val(doanThanhTraID);


    fillThongTinChiTietLapDoanTT(cuocThanhTraID);

    var config = {
        '.chosen': {}
    }
    for (var selector in config) {
        $(selector).chosen(config[selector]);

    }
    $(".chosen").trigger("chosen:updated");

    return false;
}

function duyetBaoCaoKetQuaTienHanhTT() {
    var cuocThanhTraID = $("#hddCuocThanhTraID").val();

    $.post("/Handler/ChiTietKeHoachTT/GetCommandCodeByCuocThanhTraID.ashx", {
        CuocThanhTraID: cuocThanhTraID
    }).done(function (data) {

        $("#MainContent_ddlDuyet").html("");
        var commands = JSON.parse(data);
        if (data.length > 0) {
            for (var i = 0 ; i < commands.length ; i++) {
                $("#MainContent_ddlDuyet").append("<option value='" + commands[i].CommandCode + "'>" + commands[i].CommandName + "</option>");
            }

            var row = $("#MainContent_hdfRow").val();
            var arrDuyet = [];
            for (var i = 0 ; i < row; i++) {
                if ($("#MainContent_rptNguoiDung_CheckBox2_" + i).is(":checked")) {
                    arrDuyet.push(i);
                }
            }

            var commandCode = $("#MainContent_ddlDuyet").val();
            $("#MainContent_hdfCommandCode").val(commandCode);

            $("#MainContent_hdfCuocThanhTraID").val(cuocThanhTraID);

            $("#MainContent_hdfDuThaoKetLuanTT").val(arrDuyet);

            //Reset();
            $("#DuyetConfirm").modal();

            return false;

        }
    });
}

function fillChiTIetKetLuanThanhTra(data) {
    if (data != null && data.length > 0) {
        $("#chiTietCongBoKLTT").show();
        var htmlContent = $("#chiTietCongBoKLTT").html();

        htmlContent = htmlContent.replace("$CoQuanCongBo", data[0].TenCoQuan);
        htmlContent = htmlContent.replace("$NgayCongBo", data[0].NgayCongBoStr);
        htmlContent = htmlContent.replace("$SoTien", data[0].SoTien);
        htmlContent = htmlContent.replace("$SoDat", data[0].SoDat);
        htmlContent = htmlContent.replace("$SoNguoiDuocTraQuyenLoi", data[0].SoNguoiDuocTraQuyenLoi);
        htmlContent = htmlContent.replace("$SoDoiTuongBiXuLy", data[0].SoDoiTuongBiXuLy);
        htmlContent = htmlContent.replace("$SoDoiTuongDaBiXuLy", data[0].SoDoiTuongDaBiXuLy);

        if (data[0].FileWFUrl != null) {

            var fileWFUrl = data[0].FileWFUrl;
            var tenFile = "";
            var str = "";
            if (fileWFUrl != null && fileWFUrl != "") {
                tenFiles = fileWFUrl.split("/")[2];
                tenFileDinhKem = tenFiles.split("_")[1];

                str = "<a href='/" + fileWFUrl + "' download='file_" + tenFileDinhKem + "' target='_blank'><img id='imgDownLoad' title='Download file đính kèm' width='22' height='20' src='/images/download.png'>" + tenFileDinhKem + "</a>";
            }

            htmlContent = htmlContent.replace("$FileCongBoKetLuanTT", str);
        }
        else {
            htmlContent = htmlContent.replace("$FileCongBoKetLuanTT", "");
        }
        $("#chiTietCongBoKLTT").html(htmlContent);
    }
    else {
        $("#chiTietCongBoKLTT").hide();
    }
}


function fillChiTietThiHanhThanhTra(data) {
    if (data != null && data.length > 0) {
        $("#chiTietThiHanhKLTT").show();
        var htmlContent = $("#chiTietThiHanhKLTT").html();

        htmlContent = htmlContent.replace("$CoQuanThiHanh", data[0].TenCoQuan);
        htmlContent = htmlContent.replace("$NgayThiHanh", data[0].NgayThihanhStr);
        htmlContent = htmlContent.replace("$SoTienThiHanhTT", data[0].TienDaThu);
        htmlContent = htmlContent.replace("$SoDatThiHanhTT", data[0].DatDaThu);

        if (data[0].FileWFUrl != null) {

            var fileWFUrl = data[0].FileWFUrl;
            var tenFile = "";
            var str = "";
            if (fileWFUrl != null && fileWFUrl != "") {
                tenFiles = fileWFUrl.split("/")[2];
                tenFileDinhKem = tenFiles.split("_")[1];

                str = "<a href='/" + fileWFUrl + "' download='file_" + tenFileDinhKem + "' target='_blank'><img id='imgDownLoad' title='Download file đính kèm' width='22' height='20' src='/images/download.png'>" + tenFileDinhKem + "</a>";
            }

            htmlContent = htmlContent.replace("$FileThiHanhKetLuanTT", str);
        }
        else {
            htmlContent = htmlContent.replace("$FileThiHanhKetLuanTT", "");
        }
        $("#chiTietThiHanhKLTT").html(htmlContent);
    }
    else {
        $("#chiTietThiHanhKLTT").hide();
    }
}

function fillChiTietLapDuThaoKetLuanTT(data) {
    if (data != null && data.length > 0) {
        $("#chiTietDuThaoKLTT").show();
        var htmlContent = $("#chiTietDuThaoKLTT").html();

        htmlContent = htmlContent.replace("$TomTatDuThaoKL", data[0].TomTat);
        htmlContent = htmlContent.replace("$KetQuaKiemTra", data[0].KetQuaKT);
        htmlContent = htmlContent.replace("$KetLuan", data[0].KetLuan);
        htmlContent = htmlContent.replace("$BienPhapXuLy", data[0].BienPhapXuLy);
        htmlContent = htmlContent.replace("$KienNghiBienPhapXuLy", data[0].KienNghiBienPhapXL);

        if (data[0].TenFile != null) {

            var fileWFUrl = data[0].FileWFUrl;
            var tenFile = "";
            var str = "";
            if (fileWFUrl != null && fileWFUrl != "") {
                tenFiles = fileWFUrl.split("/")[2];
                tenFileDinhKem = tenFiles.split("_")[1];

                str = "<a href='/" + fileWFUrl + "' download='file_" + tenFileDinhKem + "' target='_blank'><img id='imgDownLoad' title='Download file đính kèm' width='22' height='20' src='/images/download.png'>" + tenFileDinhKem + "</a>";
            }

            htmlContent = htmlContent.replace("$FileUrlDuThaoKetLuanTT", str);
        }
        else {
            htmlContent = htmlContent.replace("$FileUrlDuThaoKetLuanTT", "");
        }
        $("#chiTietDuThaoKLTT").html(htmlContent);
    }
    else {
        $("#chiTietDuThaoKLTT").hide();
    }
}

function fillDanhSachThanhVienDoanTT(data) {
    if (data.length > 0) {
        $(function () {
            var content = '';
            for (var i = 0; i < data.length; i++) {
                if (data[i].TenCanBo != "") {
                    var stt = i + 1;
                    content += '<tr>';
                    content += '<td style="text-align: center">' + stt + '</td>';
                    content += '<td>' + data[i].TenCanBo + '</td>';
                    content += "<td style='text-align:left'>" + data[i].TenCoQuan + "</td>";
                    content += '<td>' + data[i].VaiTroCanBoStr + '</td>';
                    content += '</tr>';
                }
            }
            $('#tblThanhVienDoan tbody').html(content);
        });
    }
    var htmlContent = $("#TienHanhTraDetailDiv").html();
    htmlContent = htmlContent.replace("$ThoiHanTienHanhTT", data[0].ThoiHanTienHanhThanhTra);
    htmlContent = htmlContent.replace("$TenDoanThanhTra", data[0].TenDoanThanhTra);
    htmlContent = htmlContent.replace("$TenDoanThanhTra", data[0].TenDoanThanhTra);
    $("#TienHanhTraDetailDiv").html(htmlContent);
}


function fillDSFileDiKem(data) {
    var htmlContent = $("#fileDiKem").html();
    if (data.length > 0) {
        $(function () {          
            for (var i = 0; i < data.length; i++) {
                if (data[i].FileWFUrl != null) {

                    var fileWFUrl = data[i].FileWFUrl;
                    var tenFile = "";
                    var str = "";
                    if (fileWFUrl != null && fileWFUrl != "") {
                        tenFiles = fileWFUrl.split("/")[2];
                        //tenFileDinhKem = tenFiles.split("_")[1];

                        str = "<a href='/" + fileWFUrl + "' download='file_" + tenFiles + "' target='_blank'><img id='imgDownLoad' title='Download file đính kèm' width='22' height='20' src='/images/download.png'>" + tenFiles + "</a>";
                    }

                    htmlContent = htmlContent.replace("$QuyetDinhThanhTra", str);
                }
                else {
                    htmlContent = htmlContent.replace("$QuyetDinhThanhTra", "");
                }
                $("#fileDiKem").html(htmlContent);
            }
        });
    }
    else
    {
        htmlContent = htmlContent.replace("$QuyetDinhThanhTra", "");
        $("#fileDiKem").html(htmlContent);
    }
    
}

function GetChiTietKeHoachTienHanhTT(cuocThanhTraID) {
    $.post("/Handler/ChiTietCuocThanhTra/ChiTietKeHoachTienHanhThanhTra.ashx", {
        CuocThanhTraID: cuocThanhTraID
    }).done(function (data) {
        var keHoachTienHanhTTInfo = JSON.parse(data);
        if (keHoachTienHanhTTInfo.KeHoachTienHanhTTID > 0) {
            $("#ttKeHoachTienHanhTT").show();
            var htmlContent = $("#ttKeHoachTienHanhTT").html();
            htmlContent = htmlContent.replace("$TenKeHoachTienHanhTT", keHoachTienHanhTTInfo.TenKeHoach);
            htmlContent = htmlContent.replace("$MucDichTienHanhTT", keHoachTienHanhTTInfo.MucDich);
            htmlContent = htmlContent.replace("$YeuCauTienHanhTT", keHoachTienHanhTTInfo.YeuCau);
            htmlContent = htmlContent.replace("$NoiDungTienHanhTT", keHoachTienHanhTTInfo.NoiDung);
            htmlContent = htmlContent.replace("$PhuongPhapTienHanhTT", keHoachTienHanhTTInfo.PhuongPhapTienHanh);
            htmlContent = htmlContent.replace("$GhiChuTienHanhTT", keHoachTienHanhTTInfo.GhiChu);

            if (keHoachTienHanhTTInfo.TenFile != null) {

                var fileWFUrl = keHoachTienHanhTTInfo.FileWFUrl;
                var tenFile = "";
                var str = "";
                if (fileWFUrl != null && fileWFUrl != "") {
                    tenFiles = fileWFUrl.split("/")[2];
                    tenFileDinhKem = tenFiles.split("_")[1];

                    str = "<a href='/" + fileWFUrl + "' download='file_" + tenFileDinhKem + "' target='_blank'><img id='imgDownLoad' title='Download file đính kèm' width='22' height='20' src='/images/download.png'>" + tenFileDinhKem + "</a>";
                }

                htmlContent = htmlContent.replace("$FileUrlKeHoachTT", str);
            }
            else {
                htmlContent = htmlContent.replace("$FileUrlKeHoachTT", "");
            }


            $("#ttKeHoachTienHanhTT").html(htmlContent);
        }
        else {
            $("#ttKeHoachTienHanhTT").hide();
        }
    });
}

function chiTietTT_Click(cuocThanhTraID) {

}

function showChiTietCuocThanhTra() {
    if ($("#tabs").html() != "") {
        chitietTemplate = $("#tabs").html();
    }
    $('.div_ContentCTCuocThanhTra').html(chitietTemplate);

    $(".list-info").hide();
    $('.div_ChiTietCuocThanhTra').show();

    $("#tabs").html("");
}

function hideChiTietCuocThanhTra() {
    $(".list-info").show();
    $('.div_ChiTietCuocThanhTra').hide();
}


function PrintControl_ChiTiet(divId) {
    var printWindow = window.open('', '_newtab', 'height=400,width=630');
    printWindow.document.write('<html>');
    // printWindow.document.write('<head><title>DIV Contents</title></head>');
    printWindow.document.write('<body>');
    printWindow.document.write($("#" + divId).html());
    printWindow.document.write('</body></html>');
    printWindow.document.close();
    printWindow.print();
    printWindow.close();
}

function ExportExcel_ChiTiet(div) {
    var trList = $("#" + div + " tr:not('.no-border')");
    trList.css("border", "0.6pt solid #999");
    trList.css("color", "black");
    var url = 'data:application/vnd.ms-excel,' + encodeURIComponent($('#' + div).html());

    var link = document.createElement("a");
    var ten = "ds_cuoc_thanh_tra.xls";
    link.download = ten;
    link.href = url;
    link.target = "_blank";
    $("#download_td").html("");
    document.getElementById("download_td").appendChild(link);
    link.click();
}