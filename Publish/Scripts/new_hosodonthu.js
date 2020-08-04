function soluongOnchange() {
    var soluong = $('#txt_songuoidaidien').val();
}

//Ngày chuyen don
//$(".ngaychuyendon").datepicker({
//    yearRange: '2000:2015',
//    changeMonth: true,
//    changeYear: true
//});
//$(".ngaychuyendon").datepicker("option", $.datepicker.regional["vi"]);
//$(".ngaychuyendon").datepicker("option", "dateFormat", "dd/mm/yy");

//ngay chuyen don
$(document).ready(function () {
    //Ngày chuyen don
    $(".ngaynhapdon").datepicker({
        yearRange: '2000:2015',
        changeMonth: true,
        changeYear: true
    });
    $(".ngaynhapdon").datepicker("option", $.datepicker.regional["vi"]);
    $(".ngaynhapdon").datepicker("option", "dateFormat", "dd/mm/yy");
});

//ngay qua han
$(document).ready(function () {
    //Ngày chuyen don
    $(".ngayquahan").datepicker({
        yearRange: '2000:2015',
        changeMonth: true,
        changeYear: true
    });
    $(".ngayquahan").datepicker("option", $.datepicker.regional["vi"]);
    $(".ngayquahan").datepicker("option", "dateFormat", "dd/mm/yy");
});

//ngay chuyen don
$(document).ready(function () {
    //Ngày chuyen don
    $(".ngaychuyendon").datepicker({
        yearRange: '2000:2015',
        changeMonth: true,
        changeYear: true
    });
    $(".ngaychuyendon").datepicker("option", $.datepicker.regional["vi"]);
    $(".ngaychuyendon").datepicker("option", "dateFormat", "dd/mm/yy");
});


// hide nguon don class
$("#searchForm .nguonDon").css("display", "none");

// fetch details of selected item to edit box
function show_add_light_box() {
    doBlockUI();

    //	document.editForm.btnKT.disabled=false;
    //	document.editForm.btnKT.className="save";
//    enableBtnKTs();
    //	showSaveAndCreateNewButton();

    var url = 'HoSoDonCurrentStatus.ashx';
    $.ajax({
        type: "POST",
        url: url,
        dataType: "json",
        success: fetchSuccess,
        error: function () {
            //doUnBlockUI();
            alert_error("Có lỗi server !!!");
        }
    });
    return false;
}

function fetchSuccess(json) {
    doUnBlockUI();

    // error
    if (json.status) {
        alert_error("Có lỗi server !!!");
        return false;
    }

    $("#txt_quanHuyen").val("");
    $("#txt_caNhan").val("1");

    // this is the json return data	
    var stt = json;
    //	old_soDk = parseInt(stt.soDk) + 1;
    old_soDk = parseInt(stt.soDk);
    $("#id_sodkSo").val(old_soDk);
    // set ngay mac dinh
    var ngayNhapS = formatDate(new Date(), "dd/MM/yyyy");
    $("#txt_ngay").val(ngayNhapS);

    var ngayQuaHan = new Date();
    var days = ngay_qua_han;
    var res = ngayQuaHan.setTime(ngayQuaHan.getTime() + (days * 24 * 60 * 60 * 1000));
    ngayQuaHan = new Date(res);
    var ngayQuaHanS = formatDate(ngayQuaHan, "dd/MM/yyyy");
    $("#txt_ngayQuaHan").val(ngayQuaHanS);

    show_light_box_edit();
    return false;
}
function show_light_box_edit() {

    // remove time out on open dialog
    //	clearRefreshTimeout();

    // bind shortcut keys
    
    //bindKeyShortcutEditForm();

    $('#light').show();
    $('#fade').show();
    $('#light').css({
        'width': '95.5%',
        'top': '1%',
        'left': '0'
    });
    var fullHeight = document.body.offsetHeight;
    $('#fade').css("height", fullHeight + "px");
    //Fix height for editForm

    //var new_height = $("#editForm")[0].offsetHeight;
    //new_height = fullHeight;
    new_height = $(window).height() - 100;//660;
    $("#light").css('height', new_height + "px");
    scroll(0, 0);
    //Fill data for SODK_SO (Default)
    // focus on open
    if ($("#id_sodkSo").is(":enabled"))
        $('#id_sodkSo').focus();
    else
        $('#txt_ngayQuaHan').focus();
    return false;
}

function change_type_obj(i) {
    $("#txt_soNguoi").val("1");
    $("#txt_caNhanCq").val("");
    //Set value for sodk_canhan
    $("#txt_caNhan").val($('input:radio[name=hs_type]:checked').val());
    //
    $('#so_nguoi').hide();
    $('#ten_co_quan').hide();
    $('#label_cn').css('font-weight', 'bold');
    $('#label_cq').removeClass('bold');
    $('#label_tt').removeClass('bold');
    if (i == 0) {
        $('#label_cn').css('font-weight', 'normal');
        $('#label_cq').removeClass('bold');
        $('#so_nguoi').show(500);
        $('#label_tt').addClass('bold');
    }
    if (i == 2) {
        $('#label_cn').css('font-weight', 'normal');
        $('#label_tt').removeClass('bold');
        $('#ten_co_quan').show(500);
        $('#label_cq').addClass('bold');
    }

    //setTimeout(function(){ resetEditFormHeight(); }, 100);
    resetEditFormHeight();
}