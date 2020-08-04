function InitDiv() {
    
    var coQuanList = $("#coQuanList").html();
    var phanLoaiTTList = $("#phanLoaiTTList").html();
    var row = $("#MainContent_hdfCount").val();

    //var str = "";
    //str += "<div class='div-wrap'>";
    //str += "<div class='box-header'>";
    //str += "<h3 class='box-title'>Thông tin cuộc thanh tra " + row + "</h3>";
    //str += "</div><!-- /.box-header -->";
    //str += "<div class='box-body' style='border-top: 1px solid #cfcfcf;'>";
    //str += "<div class='form-horizontal'>";
    //str += "<div class='form-group'>";
    //str += "<label class='col-lg-3 col-sm-3 control-label'>Đối tượng thanh tra <span style='color:red'>*</span></label>";
    //str += "<div class='col-lg-6'>";
    //str += "<select class='form-control select2 validate[required] text-input' multiple='multiple' data-placeholder='Chọn đối tượng thanh tra.' style='width: 100%;' id='ddlDoiTuongThanhTra" + row + "' name='ddlDoiTuongThanhTra" + row + "' onchange='validate_engine(" + row + ")'>" + coQuanList + "</select>";
    //str += "</div>";
    //str += "</div>";
    //str += "<div class='form-group'>";
    //str += "<label class='col-lg-3 col-sm-3 control-label'>Loại thanh tra <span style='color:red'>*</span></label>";
    //str += "<div class='col-lg-3'>";
    //str += "<select class='form-control select2 validate[required] text-input' id='ddlLoaiThanhTra" + row + "' name='ddlLoaiThanhTra" + row + "' onchange='loaiThanhTraChange(" + row + ")'><option value=''>Chọn loại thanh tra.</option>" + phanLoaiTTList + "</select>";
    //str += "</div>";
    //str += "<div class='col-lg-3'>";
    //str += "<select class='form-control select2 validate[required] text-input' id='ddlLoaiThanhTraDetail" + row + "' name='ddlLoaiThanhTraDetail" + row + "' ></select>";
    //str += "</div>";
    //str += "</div>";


    $("#div_dynamic ").append("<div class='div-wrap'><div class='box-header'><h3 class='box-title'>Thông tin cuộc thanh tra " + row + "</h3></div><!-- /.box-header --><div class='box-body' style='border-top: 1px solid #cfcfcf;'><div class='form-horizontal'>"
        + "<div class='form-group'><label class='col-lg-3 col-sm-3 control-label'>Tên cuộc thanh tra <span style='color:red'>*</span></label><div class='col-lg-6'><input ID='txtTenCuocThanhTra" + row + "' name='txtTenCuocThanhTra" + row + "' class='form-control validate[required] text-input'/></div></div>"
        + "<div class='form-group'><label class='col-lg-3 col-sm-3 control-label'>Đối tượng thanh tra <span style='color:red'>*</span></label><div class='col-lg-6'><select class='form-control select2 validate[required] text-input' multiple='multiple' data-placeholder='Chọn đối tượng thanh tra.' style='width: 100%;' id='ddlDoiTuongThanhTra" + row + "' name='ddlDoiTuongThanhTra" + row + "' onchange='validate_engine(" + row + ")'>" + coQuanList + "</select></div></div>"
        + " <div class='form-group'><label class='col-lg-3 col-sm-3 control-label' style='padding-top: 0px;'>Thanh tra lại</label><div class='col-lg-3'><label><input type='checkbox' class='minimal'  name='thanhtralai" + row + "' onchange='ThanhTraLaiChange(" + row + ")'/><input type='hidden' name='hdfThanhTraLai" + row + "' value='0'/></label></div></div> "
        + "<div class='form-group'><label class='col-lg-3 col-sm-3 control-label'>Loại thanh tra <span style='color:red'>*</span></label><div class='col-lg-3'><select class='form-control select2 validate[required] text-input' id='ddlLoaiThanhTra" + row + "' name='ddlLoaiThanhTra" + row + "' onchange='loaiThanhTraChange(" + row + ")'><option value=''>Chọn loại thanh tra.</option>" + phanLoaiTTList + "</select></div><div class='col-lg-3'><select class='form-control select2 chitietloaitt validate[required] text-input' id='ddlLoaiThanhTraDetail" + row + "' name='ddlLoaiThanhTraDetail" + row + "' style='display:none'><option value=''>Chọn loại thanh tra.</option></select></div></div><div class='form-group'><label class='col-lg-3 col-sm-3 control-label'>Phạm vi thanh tra <span style='color:red'>*</span></label><div class='col-lg-3'><input ID='txtPhamViThanhTra" + row + "' name='txtPhamViThanhTra" + row + "'  class='form-control validate[required]  text-input'/></div></div>"
        + "<div class='form-group'><label class='col-lg-3 col-sm-3 control-label'>Thời hạn thanh tra <span style='color:red'>*</span></label><div class='col-lg-3'><input name='txtThoiGianThanhTra" + row + "'  ID='txtThoiGianThanhTra" + row + "' class='form-control validate[required,custom[integer],maxSize[2],,max[70]] text-input'/></div></div>"
        + "<div class='form-group'><label class='col-lg-3 col-sm-3 control-label'>Thời gian tiến hành <span style='color:red'>*</span></label><div class='col-lg-3'><input ID='txtThoiGianTienHanh" + row + "' name='txtThoiGianTienHanh" + row + "' class='form-control validate[required] text-input'/></div></div>"
        + "<div class='form-group'><label class='col-lg-3 col-sm-3 control-label'>Nội dung thanh tra <span style='color:red'>*</span></label><div class='col-lg-9'><textarea ID='txtNoiDung" + row + "' name='txtNoiDung" + row + "' class='form-control validate[required] text-input' rows='5' style='resize:none;width:95%'></textarea></div></div>"
        + "<div class='form-group'><label class='col-lg-3 col-sm-3 control-label'>Đơn vị phối hợp <span style='color:red'>*</span></label><div class='col-lg-6'><select class='form-control select2 validate[required]' multiple='multiple' data-placeholder='Chọn đơn vị phối hợp.' style='width: 100%;' id='ddlDVPhoiHop" + row + "' name='ddlDVPhoiHop" + row + "'>" + coQuanList + "</select></div></div></div></div><!-- /.box-body --></div>");

    //$("#ddlLoaiThanhTraDetail" + row).hide();

}

function addNewDivRow() {
    var coQuanList = $("#coQuanList").html();
    var phanLoaiTTList = $("#phanLoaiTTList").html();
    var row = $("#MainContent_hdfCount").val();
    row++;
    $("#MainContent_hdfCount").val(row);
    $("#div_dynamic ").append("<div class='div-wrap'><div class='box-header'><h3 class='box-title'>Thông tin cuộc thanh tra " + row + "</h3></div><!-- /.box-header --><div class='box-body' style='border-top: 1px solid #cfcfcf;'><div class='form-horizontal'>"
         + "<div class='form-group'><label class='col-lg-3 col-sm-3 control-label'>Tên cuộc thanh tra <span style='color:red'>*</span></label><div class='col-lg-6'><input ID='txtTenCuocThanhTra" + row + "' name='txtTenCuocThanhTra" + row + "' class='form-control validate[required] text-input'/></div></div>"
        + "<div class='form-group'><label class='col-lg-3 col-sm-3 control-label'>Đối tượng thanh tra <span style='color:red'>*</span></label><div class='col-lg-6'><select class='form-control select2 validate[required] text-input' multiple='multiple' data-placeholder='Chọn đối tượng thanh tra.' style='width: 100%;' id='ddlDoiTuongThanhTra" + row + "' name='ddlDoiTuongThanhTra" + row + "' onchange='validate_engine(" + row + ")'>" + coQuanList + "</select></div></div>"
        + "<div class='form-group'><label class='col-lg-3 col-sm-3 control-label' style='padding-top: 0px;'>Thanh tra lại</label><div class='col-lg-3'><label><input type='checkbox' class='minimal'  name='thanhtralai" + row + "'/></label></div></div>  "
        + "<div class='form-group'><label class='col-lg-3 col-sm-3 control-label'>Loại thanh tra <span style='color:red'>*</span></label><div class='col-lg-3'><select class='form-control select2 validate[required] text-input' id='ddlLoaiThanhTra" + row + "' name='ddlLoaiThanhTra" + row + "' onchange='loaiThanhTraChange(" + row + ")'><option value=''>Chọn loại thanh tra.</option>" + phanLoaiTTList + "</select></div><div class='col-lg-3'><select class='form-control select2 chitietloaitt validate[required] text-input' id='ddlLoaiThanhTraDetail" + row + "' name='ddlLoaiThanhTraDetail" + row + "'><option value=''>Chọn loại thanh tra.</option></select></div></div>"
        + "<div class='form-group'><label class='col-lg-3 col-sm-3 control-label'>Phạm vi thanh tra <span style='color:red'>*</span></label><div class='col-lg-3'><input ID='txtPhamViThanhTra" + row + "' name='txtPhamViThanhTra" + row + "'  class='form-control validate[required]  text-input'/></div></div>"
        + "<div class='form-group'><label class='col-lg-3 col-sm-3 control-label'>Thời hạn thanh tra <span style='color:red'>*</span></label><div class='col-lg-3'><input name='txtThoiGianThanhTra" + row + "'  ID='txtThoiGianThanhTra" + row + "' class='form-control validate[required,custom[integer],maxSize[2],,max[70]] text-input'/></div></div>"
        + " <div class='form-group'><label class='col-lg-3 col-sm-3 control-label'>Thời gian tiến hành <span style='color:red'>*</span></label><div class='col-lg-3'><input ID='txtThoiGianTienHanh" + row + "' name='txtThoiGianTienHanh" + row + "' class='form-control validate[required] text-input'/></div></div>"
        + "<div class='form-group'><label class='col-lg-3 col-sm-3 control-label'>Nội dung thanh tra <span style='color:red'>*</span></label><div class='col-lg-9'><textarea ID='txtNoiDung" + row + "' name='txtNoiDung" + row + "' class='form-control validate[required] text-input' rows='5' style='resize:none;width:95%'></textarea></div></div>"
        + "<div class='form-group'><label class='col-lg-3 col-sm-3 control-label'>Đơn vị phối hợp <span style='color:red'>*</span></label><div class='col-lg-6'><select class='form-control select2 validate[required]' multiple='multiple' data-placeholder='Chọn đơn vị phối hợp.' style='width: 100%;' id='ddlDVPhoiHop" + row + "' name='ddlDVPhoiHop" + row + "'>" + coQuanList + "</select></div></div></div></div><!-- /.box-body --></div>");

    $(".select2").select2();

    //iCheck for checkbox and radio inputs
    $('input[type="checkbox"].minimal, input[type="radio"].minimal').iCheck({
        checkboxClass: 'icheckbox_minimal-blue',
        radioClass: 'iradio_minimal-blue'
    });

}

function removeLastDivRow() {
    var row = $("#MainContent_hdfCount").val();
    if (row > 0) {
        $("#div_dynamic > div:last").remove();
        row--;
    }

    $("#MainContent_hdfCount").val(row);
}

function validate_engine(row) {
    
    jQuery("#Form1").validationEngine();
    //alert(1);
    CheckChongCheo_KeHoachTT(row);
}

function CheckChongCheo_KeHoachTT(row) {
    var id = $("#ddlDoiTuongThanhTra" + row).val();
    var ids = id + "";
    var namThanhTra = $("#MainContent_ddlNamThanhTra").val();

    $.ajax({
        type: "GET",
        url: "/Handler/KeHoachThanhTra/GetChongCheoDoiTuongThanhTra.ashx",
        data: {
            doiTuongTTID: ids,
            namThanhTra: namThanhTra
        },
        success: function (data) {
            if (data != null && data != "") {
                FillDoiTuong_ChongCheo(data);
            }
        }
    });

}

function FillDoiTuong_ChongCheo(data) {
    $("#table_DoiTuong_ChongCheo tbody").empty();
    $("#table_DoiTuong_ChongCheo tbody").append(data);

    $('#DoiTuongThanhTra_ChongCheo').modal();
}

function ThanhTraLaiChange(row) {
    //alert(row);
    //var selected = $("#thanhtralai" + row).val();
    //alert(selected);
    //if (selected) {
    //    alert("true");
    //}
    //else
    //    alert("false");
}

function loaiThanhTraChange(row) {
    
    var id = $("#ddlLoaiThanhTra" + row).val();
    $.ajax({
        type: "GET",
        url: "/Handler/PhanLoaiTT/GetPhanLoaiTT2ByParent.ashx",
        data:{parentID: id},
        success: function (data) {
            if (data != null && data != "") {
                $("#ddlLoaiThanhTraDetail" + row).empty();
                $("#ddlLoaiThanhTraDetail" + row).append(data);
            } else {
                $("#ddlLoaiThanhTraDetail" + row).hide();
                $("#ddlLoaiThanhTraDetail" + row).next().hide();
                $("#ddlLoaiThanhTraDetail" + row).next().css({"display": "none" });
            }
        }
    });
}

function HuyBoClick() {
    window.location.href = "/SoftWare/Bus/LapKeHoachThanhTra.aspx";
}

