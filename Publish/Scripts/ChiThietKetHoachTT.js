var chitietTemplate = "";
var CV_LAPKHTT_SCode = "CVLapKHTT";
var LD_DuyetKHTT_SCode = "LDDuyetKHTT";
var KTLapKHTT_SCode = "KetThucKHTT";
var LapDoanTT_SCode = "LapDoanTT";
var XDKeHoachTienHanhTT_SCode = "XDKeHoachTienHanhTT";
var DuyetKHTienHanhTT_SCode = "DuyetKHTienHanhTT";
var TienHanhTT_SCode = "TienHanhTT";
var DuyetBCKetQuaTT_SCode = "DuyetBCKetQuaTT";

var RoleLanhDaoDonVi = 1;
var RoleLanhDaoPhongBan = 2;
var RoleChuyenVien= 3;
function ChiTietKeHoachTT(keHoachThanhTraID, Role, StateID, stateCode) {
    /* chi tiet ke hoach*/
    $("#suaCTKeHoach").hide();
    $("#xoaCTKeHoach").hide(); 

    $("#hdfChiTiet_StateID").val(StateID);
    $("#hdfChiTiet_KeHoachTTID").val(keHoachThanhTraID);
    
    $("#hddKeHoachThanhTraID").val(keHoachThanhTraID);

    $.post("/Handler/ChiTietKeHoachTT/ChiTietKeHoachThanhTra.ashx", {
        KeHoachThanhTraID: keHoachThanhTraID
    }).done(function (data) {
        var lsChiTietKeHoach = JSON.parse(data);
        if (lsChiTietKeHoach.length > 0) {
            $(function () {
                var content = '';
                for (var i = 0; i < lsChiTietKeHoach.length; i++) {
                    var stt = i + 1;   
                    content += '<tr onclick="chiTietTT_Click(' + lsChiTietKeHoach[i].CuocThanhTraID + ');return false" id="CuocThanhTra' + lsChiTietKeHoach[i].CuocThanhTraID + '">';
                    content += '<td style="text-align: center">' + stt + '</td>';
                    content += '<td>' + lsChiTietKeHoach[i].DoiTuongThanhTraStr + '</td>';
                    content += '<td>' + lsChiTietKeHoach[i].NoiDung + '</td>';
                    content += '<td>' + lsChiTietKeHoach[i].PhamViThanhTra + '</td>';
                    content += '<td>' + lsChiTietKeHoach[i].ThoiHanThanhTra + '</td>';
                    content += '<td>' + lsChiTietKeHoach[i].ThoiGianTienHanh + '</td>';
                    content += '<td>' + lsChiTietKeHoach[i].DonViChuTriStr + '</td>';
                    content += '<td>' + lsChiTietKeHoach[i].DonViPhoiHopStr + '</td>';
                    content += '</tr>';
                }               
                $('#tblChiTietKeHoachTT tbody').html(content);
            });
        }
    });

    /* luong ke hoach*/
    $.post("/Handler/ChiTietKeHoachTT/LuongDonKeHoachGetByKeHoachID.ashx", {
        KeHoachThanhTraID: keHoachThanhTraID
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
                    content += '<td>' + lsLuongKeHoach[i].ThaoTac + '</td>';
                    content += '<td>' + lsLuongKeHoach[i].YKienCanBo + '</td>';
                    content += '<td>' + str + '</td>';
                    content += '</tr>';
                }
                $('#tblLuongKeHoach tbody').html(content);
            });
        }
    });
    showChiTietKeHoachThanhTra();

    if (Role == RoleChuyenVien || Role == RoleLanhDaoPhongBan) {
        $("#pheDuyetKeHoach").hide();
        if (stateCode == CV_LAPKHTT_SCode) {
            $("#addCuocTTs").show();
        }
        else {
            $("#addCuocTTs").hide();
        }
    }
    if (Role == RoleLanhDaoDonVi) {
        $("#addCuocTTs").hide();
        if (stateCode == LD_DuyetKHTT_SCode) {
            $("#pheDuyetKeHoach").show();
        }
        else {
            $("#pheDuyetKeHoach").hide();
        }
    }
}

function chiTietTT_Click(cuocThanhTraID) {
    $("#hddCuocThanhTraID").val(cuocThanhTraID);
    $('tr').removeClass('selected_hl');
    $("#CuocThanhTra" + cuocThanhTraID).addClass('selected_hl');

    var stateID = $("#hdfChiTiet_StateID").val();

    if (stateID == 1) {
        $("#xoaCTKeHoach").show();
        $("#suaCTKeHoach").hide();
    } else {
        $("#xoaCTKeHoach").hide();
        $("#suaCTKeHoach").hide();
    }
}

function showChiTietKeHoachThanhTra() {

    if ($("#tabs").html() != "") {
        chitietTemplate = $("#tabs").html();
    }
    $('.div_ContentCTKeHoachThanhTra').html(chitietTemplate);

    $(".list-info").hide();
    $('.div_ChiTietKeHoachThanhTra').show();

    $("#tabs").html("");
}

function hideChiTietKeHoachThanhTra() {
    $(".list-info").show();
    $('.div_ChiTietKeHoachThanhTra').hide();
}

function AddNewCuocTT() {
    var keHoachTTID = $("#hdfChiTiet_KeHoachTTID").val();
    
    if (keHoachTTID != 0) {
        window.location.href = "/SoftWare/Bus/LapKeHoachThanhTra.aspx?keHoachTTID=" + keHoachTTID;
    }
}

function suaCTKeHoach_Click() {
    var cuocThanhTraID = $("#hddCuocThanhTraID").val();
    $.post("/Handler/ChiTietKeHoachTT/CuocThanhTraGetByID.ashx", {
        CuocThanhTraID: cuocThanhTraID
    }).done(function (data) {
        var cuocThanhTraInfo = JSON.parse(data);
        console.log(cuocThanhTraInfo);
        if (cuocThanhTraInfo != null) {        
           // $("#MainContent_ddlDoiTuongThanhTra").select2('data', { id: 515, text: "Văn phòng Chính phủ" });
            // $("#MainContent_ddlDoiTuongThanhTra").val(cuocThanhTraInfo.CoQuanBiThanhTraID);
            var data1 = [{ id: 312, text: 'Ban Tiếp dân huyện Tân Thành' }];
            $(".select2").select2({
                data: data1
            })
            $("#MainContent_txtPhamViThanhTra").val(cuocThanhTraInfo.PhamViThanhTra);
            $("#MainContent_txtThoiHanTT").val(cuocThanhTraInfo.ThoiHanThanhTra);
            $("#MainContent_txtThoiGianTienHanh").val(cuocThanhTraInfo.ThoiGianTienHanh);
            $("#MainContent_txtNoiDungTT").val(cuocThanhTraInfo.NoiDung);
            $("#editKeHoachThanhTra").modal();
            return false;
        }
    });
}

function delConfirmCuocTT() {
    var cuocThanhTraID = $("#hddCuocThanhTraID").val();
    $("#hdfCuocThanhTraID").val(cuocThanhTraID);
    $("#delConfirmCuocTT").modal();
    //alert(cuocThanhTraID);
}

function XoaCTKeHoach_Click() {
    var cuocThanhTraID = $("#hdfCuocThanhTraID").val();
    $.ajax({
        async: false,
        type: 'POST',
        url: "/Handler/ChiTietKeHoachTT/DeleteCuocThanhTra.ashx",
        dataType: "json",
        data: {
            "id": cuocThanhTraID,
        },
        success: function (json) {
            
            if (json > 0) {
                $("#MainContent_lblThongBao").html("Xóa cuộc thanh tra thành công.");
                showThongBao()();
            }
            else {

                $("#MainContent_lblThongBao").html("Xảy ra lỗi trong quá trình xóa dữ liệu. Vui lòng thử lại sau.");
                showThongBao();
            }

        },
        error: function (json) {

        }
    });

    window.location.href = "/SoftWare/Bus/DSKeHoachThanhTra.aspx";
}

function pheDuyetKeHoach_Click()
{
    //alert($("#hddCuocThanhTraID").val());
    $("#MainContent_hdfKeHoachDuocDuyet").val($("#hddKeHoachThanhTraID").val());
    var keHoachDuocDuyetID =$("#hddKeHoachThanhTraID").val();
    getDDLPheDuyetKeHoach(keHoachDuocDuyetID);
}

function getDDLPheDuyetKeHoach(keHoachDuocDuyetID) {
    $.ajax({
        type: "POST",
        url: "DSKeHoachCanPheDuyet.aspx/GetPheDuyetKeHoachThanhTra",
        data: '{keHoachDuocDuyetID:"' + keHoachDuocDuyetID + '"}',
        dataType: "json",
        async: "true",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var json = eval('(' + data.d + ')');
            if (json != null) {
                console.log(json);
                var listItems = "";
                for (var i = 0; i < json.length; i++) {
                    listItems += "<option value='" + json[i].CommandCode + "'>" + json[i].CommandName + "</option>";
                }
                $("#MainContent_ddlDuyet").html(listItems);
                $("#pheDuyetKeHoachThanhTra").modal("show");
                var commandCode = $("#MainContent_ddlDuyet").val();
                $("#MainContent_hdfCommandCode").val(commandCode);
            }
        }
    });
};

    function hideAddEditCTKeHoach() {
        $("#editKeHoachThanhTra").modal("hide");
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