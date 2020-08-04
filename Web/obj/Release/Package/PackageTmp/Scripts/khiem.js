function Download() {
    var filepath = "D:\Projects\CongThongTin\Web\UploadFiles\FileWF\122(553).jpg";
    $.post(
        "/Handler/DownloadFileQuyetDinh.ashx",
        {
            filepath: filepath
        }
    ).done(function (data) {

    }).fail(function (data) {
        alert("Chưa có file tải về!");
    });
}

function InitDiv() {
    $("#div_dynamic").append(
        "<div class='form-group'>"
        + "<label class='col-lg-3 col-sm-3 control-label'>Nội dung thủ tục: <span style='color: red;'>*</span></label>"
        + "<div class='col-lg-9'>"
        + "<input ID='txtNDThuTuc' class='form-control'/>"
        + "<span style='color: blue;'>Ví dụ: Bước 1: Xin giấy xác nhận</span>"
        + "</div>"
        + "      </div >"
        + "<div class='form-group'>"
        + "<label class='col-lg-3 col-sm-3 control-label'>Trình tự thứ: <span style='color: red;'>*</span></label>"

        + "<div class='col-lg-9'>"
        + "<input ID='txtOrder' Class='form-control'/>"
        + "</div>"
        + "      </div >"
    );
}

function addNewDivRow() {
    var row = $("#MainContent_hdfCount").val();
    row++;

    $("#div_dynamic").append(
        "<div class='form-group'>"
            + "<label class='col-lg-3 col-sm-3 control-label'>Nội dung thủ tục: <span style='color: red;'>*</span></label>"
            +"<div class='col-lg-9'>"
                + "<input ID='txtNDThuTuc' class='form-control'/>"
                                +"<span style='color: blue;'>Ví dụ: Bước 1: Xin giấy xác nhận</span>"
        + "</div>"
                  + "      </div >"
        + "<div class='form-group'>"
            +"<label class='col-lg-3 col-sm-3 control-label'>Trình tự thứ: <span style='color: red;'>*</span></label>"

            + "<div class='col-lg-9'>"
                + "<input ID='txtOrder' Class='form-control'/>"
        + "</div>"
        + "      </div >"
    );
}

function removeLastDivRow() {
    var row = $("#MainContent_hdfCount").val();
    if (row > 0) {
        $("#div_dynamic > div:last").remove();
        row--;
    }

    $("#MainContent_hdfCount").val(row);
}