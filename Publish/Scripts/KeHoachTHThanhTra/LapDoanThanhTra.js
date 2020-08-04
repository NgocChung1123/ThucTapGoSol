function ddlPhanLoaiDoanChange() {
    var ddlPhanLoaiDoan = $("#MainContent_ddlPhanLoaiDoan").val();
    if (ddlPhanLoaiDoan == "doannoibo") {
        $("#noibo").show();
        $("#liennganh").hide();
        $("#MainContent_btnSaveNoiBo").show();
        $("#MainContent_btnSaveLienNganh").hide();
    } else if (ddlPhanLoaiDoan == "doanliennganh") {
        $("#noibo").hide();
        $("#liennganh").show();
        $("#MainContent_btnSaveNoiBo").hide();
        $("#MainContent_btnSaveLienNganh").show();
    }
}

/*
* noi bo
*/
function addNewRowNoiBo() {
    //var coQuanList = $("#coQuanList").html();
    var canBoLs = $("#canBoLs").html();
    var vaiTro = $("#lsVaiTro").html();
    var row = $("#MainContent_hdfCountNoiBo").val();
    row++;
    $("#MainContent_hdfCountNoiBo").val(row);
    $("#additem_noibo" + (row - 1)).after("<div class='form-group' id='additem_noibo" + row + "'><label class='col-lg-3 col-sm-3 control-label'>Giao cho <span style='color: red'>*</span></label><div class='col-lg-6'><select name='ddlThanhVienNoiBo" + row + "' class='form-control chosen'>" + canBoLs + "</select> <input type='hidden' id='hddThanhVienDoanThanhTraID" + (row) + "' name='hddThanhVienDoanThanhTraID" + (row) + "' /> </div><div class='col-lg-3'><select name='ddlVaiTroNoiBo" + row + "' class='form-control chosen'>" + vaiTro + "</select></div></div>");

    var config = {
        '.chosen': {}
    }
    for (var selector in config) {
        $(selector).chosen(config[selector]);

    }
    $(".chosen").trigger("chosen:updated");
}
var thanhVienDoan = [];
function removeLastRowNoiBo() {
   
    var row = $("#MainContent_hdfCountNoiBo").val();
    row = parseInt(row);
    if (row > 1) {
        var thanhVienDoanID = $("#hddThanhVienDoanThanhTraID" + row).val();
        thanhVienDoan.push(thanhVienDoanID);
        $("#additem_noibo" + row).remove();       
        row--;
    }

    $("#MainContent_hdfCountNoiBo").val(row);
    $("#MainContent_listThanhVienDoanDeleteID").val(thanhVienDoan);
}
/*
* end noi bo
*/

/*
* lien nganh
*/
function addNewRowLienNganh() {
    var coQuanList = $("#coQuanList").html();
    var vaiTro = $("#lsVaiTro").html();
    var row = $("#MainContent_hdfCountLienNganh").val();
    row++;
    $("#MainContent_hdfCountLienNganh").val(row);
    $("#additem_liennganh" + (row - 1)).after("<div class='form-group' id='additem_liennganh" + row + "'><label class='col-lg-3 col-sm-3 control-label'>Giao cho <span style='color: red'>*</span></label><div class='col-lg-3'><select name='ddlCoQuanLienNganh" + row + "' id='ddlCoQuanLienNganh" + row + "' class='form-control chosen' onchange='CoQuanLienNganhChanged(" + row + "); return false;'>" + coQuanList + "</select></div><div class='col-lg-3'><select name='ddlThanhVienLienNganh" + row + "' id='ddlThanhVienLienNganh" + row + "' class='form-control chosen'><option value=''>Chọn cán bộ</option></select></div><div class='col-lg-3'><select name='ddlVaiTroLienNganh" + row + "' class='form-control chosen'>" + vaiTro + "</select></div></div>");

    var config = {
        '.chosen': {}
    }
    for (var selector in config) {
        $(selector).chosen(config[selector]);

    }
    $(".chosen").trigger("chosen:updated");
}

var thanhVienDoanLienNganh = [];
function removeLastRowLienNganh() {
    var row = $("#MainContent_hdfCountLienNganh").val();
    row = parseInt(row);
    if (row > 1) {
        var thanhVienDoanID = $("#hddThanhVienDoanThanhTraLienNganhID" + row).val();
        thanhVienDoanLienNganh.push(thanhVienDoanID);
        $("#additem_liennganh" + row).remove();
        row--;
    }

    $("#MainContent_hdfCountLienNganh").val(row);
    $("#MainContent_listThanhVienDoanLienNganhDeleteID").val(thanhVienDoanLienNganh);
}

function CoQuanLienNganhChanged(row) {
    var coQuanID = $("#ddlCoQuanLienNganh" + row).val();
    
    $.ajax({
        type: "GET",
        url: "/Handler/KeHoachTHThanhTra/GetCanBoByCoQuanID.ashx",
        data: { coQuanID: coQuanID },
        success: function (data) {
            if (data != null && data != "") {
                
                $("#ddlThanhVienLienNganh" + row).empty();
                $("#ddlThanhVienLienNganh" + row).append(data);
            } 
            else {
                data = "<option value=''>Chọn cán bộ</option>";
                $("#ddlThanhVienLienNganh" + row).empty();
                $("#ddlThanhVienLienNganh" + row).append(data);
            }

            var config = {
                '.chosen': {}
            }
            for (var selector in config) {
                $(selector).chosen(config[selector]);

            }
            $(".chosen").trigger("chosen:updated");
        }

    });
}

function BindCanBoByCoQuanID(coQuanID,canBoID,thanhVienDoanID,row) {
    var rows = row + 1;
    $.ajax({
        type: "GET",
        url: "/Handler/KeHoachTHThanhTra/GetCanBoByCoQuanID.ashx",
        data: { coQuanID: coQuanID },
        success: function (data) {
            if (data != null && data != "") {

                $("#ddlThanhVienLienNganh" + rows).empty();
                $("#ddlThanhVienLienNganh" + rows).append(data);


                $("#ddlCoQuanLienNganh" + (row + 1)).val(coQuanID);
                $("#ddlThanhVienLienNganh" + (row + 1)).val(canBoID);
                $("#hddThanhVienDoanThanhTraLienNganhID" + (row + 1)).val(thanhVienDoanID);
                
            }
            var config = {
                '.chosen': {}
            }
            for (var selector in config) {
                $(selector).chosen(config[selector]);

            }
            $(".chosen").trigger("chosen:updated");
        }

    });
}

/*
* end lien nganh
*/
