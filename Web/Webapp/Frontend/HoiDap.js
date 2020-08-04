$(document).ready(function () {
    $(".select2").select2({
        placeholder: "",
        width: '100%'
    });

    $(".datepicker3").datepicker({
        autoclose: true,
        format: 'dd/mm/yyyy',
        todayHighlight: true,
        clearBtn: true,
        orientation: 'bottom',
        language: 'vi'
    });

    $("#ctl00_MainContent_btnGuiCauHoi").click(function () {

        $("#ctl00_MainContent_RequiredFieldValidator4").show();



    });
    var tabID = $("#ctl00_MainContent_hdfTab").val();
    
    if (tabID == "1") {
        showTabDS();
    }
    else {
        $("#ctli").addClass("active");
        $("#dsli").removeClass("active");
        $("#tab_2").removeClass("active");
        $("#tab_1").addClass("active");

    }
    //setInterval(hideMessage, 2000);

    //$(".datepicker3").mask("99/99/9999");

});

function hideMessage() {
    var messageDiv = $("#<%= messageError.ClientID %>");
    if (messageDiv.is(":visible")) {
        setTimeout(function () {
            messageDiv.hide(300);
        }, 2000);
    }
}

function showTabDS() {
    $("#ctli").removeClass("active");
    $("#dsli").addClass("active");
    $("#tab_1").removeClass("active");
    $("#tab_2").addClass("active");

}

function showCauHoi(linhVuc) {
    var linhVucID = linhVuc;
    var noiDungCauHoi = $("#ctl00_MainContent_txtNoiDungCauHoi").val();

    $("#tableNoiDungTimKiem > tbody").html("");

    $.ajax({
        type: "POST",
        url: "HoiDap.aspx/BindCauHoi",
        data: '{linhVucID:"' + linhVucID + '",noiDungCauHoi:"' + noiDungCauHoi + '"}',
        dataType: "json",
        async: "true",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var json = eval('(' + data.d + ')');
            if (json != null) {
                if (json.length > 0) {
                    var html = "";
                    for (var i = 0 ; i < json.length; i++) {
                        var row = i + 1;

                        html += "<tr>";
                        html += "<td>";
                        html += "<div class='panel panel-default'>";
                        html += "<div class='panel-body'>";
                        html += "<header style='border-bottom: none;'>";
                        html += "       <div class='col-md-9' style='padding-left: 0px; padding-right: 0px;font-weight: bold;font-size: 16px; color: #460c69'>";
                        html += "           <div class='";
                        html += "linhVuc" + row + "'>";
                        html += "               <i class='fa fa-adjust'></i>&nbsp;";
                        html += json[i].TenLinhVuc;
                        html += "           </div>";
                        html += "       </div>";
                        html += "       <div class='col-md-9' style='padding-left: 0px; padding-right: 0px;'>";
                        html += "           <div class='";
                        html += "nguoiGui" + row + "'>";
                        html += "               <i class='glyphicon glyphicon-user'></i>&nbsp;";
                        html += json[i].HoTen;
                        html += "           </div>";
                        html += "           <div class='ngayGui";
                        html += row + "' style='font-size: 13px;'>";
                        html += "               <i class='glyphicon glyphicon-time' style='font-size: 13px; margin: 5px 0px 10px 0px'></i>&nbsp;";
                        html += json[i].NgayHoi_Str;
                        html += "           </div>";
                        html += "       </div>";
                        html += "       <div class='col-md-3'>";
                        html += "           <div id='trangThai' style='text-align:right'";
                        html += row + "'> ";
                        if (json[i].IDTraLoi != 0) {
                            html += "Đã trả lời";
                        }
                        else {
                            html += "Chưa trả lời";
                        }
                        html += "           </div>";
                        html += "       </div>";
                        html += "   </header>";

                        html += "   <div>";
                        html += "       <div id='ngayTiep' runat='server' style='font-size: 15px; font-weight: bold; font-family: 'Times New Roman';'>";
                        html += "           <label id=\"noiDungCauHoi" + row + "\" onclick='showDetailSearchQA(\"CauHoi\", " + row + ")' style=\"width: 100%;font-weight: bold !important;\">";
                        if (json[i].NDCauHoi.length != null && json[i].NDCauHoi.length != 0) {
                            if (json[i].NDCauHoi.length > 100) {
                                html += json[i].NDCauHoi.substring(0, 100) + "... .";
                            }
                            else {
                                html += json[i].NDCauHoi;
                            }
                        }
                        html += "           </label>";
                        html += "           <div id='noiDungChiTietCauHoi" + row + "' style='display: none'>";
                        html += json[i].NDCauHoi;
                        html += "           </div>";
                        html += "       </div>";
                        html += "       <div>";
                        if (json[i].NDTraLoi.length != null && json[i].NDTraLoi.length != 0) {
                            html += "           <label id=\"noiDungCauTraLoi" + row + "\" onclick='showDetailSearchQA(\"CauTL\", " + row + ")' class=\"control-label\" style=\"width: auto; padding: 10px 10px 0px 10px; border-radius: 5px\">";
                            if (json[i].NDTraLoi.length > 100) {
                                html += "-- " + json[i].NDTraLoi.substring(0, 100) + "... .";
                            }
                            else {
                                html += "-- " + json[i].NDTraLoi;
                            }
                        }
                        else {
                            html += "";
                        }
                        html += "           </label>";
                        html += "       </div>";
                        html += "       <div id='noiDungChiTietCauTL" + row + "' style='display: none;'>";
                        html += json[i].NDTraLoi;
                        html += "       </div>";

                        if (json[i].NDCauHoi.length >= 100 || json[i].NDTraLoi.length >= 100) {
                            html += "       <div id=\"idXemChiTiet" + row + "\" onclick='showDetailSearchQA(\"XemChiTiet\", " + row + ")' style=\"display: block; cursor: pointer; width: auto;\">";
                            html += "           <span class='btn btn-warning btn-sm' style='color:black;'>";
                            html += "Xem chi tiết";
                            html += "           </span>";
                            html += "       </div>";
                        }
                        if (json[i].NDCauHoi.length < 100 && json[i].NDTraLoi.length < 100) {
                            html += "";
                        }

                        html += "   </div>";
                        html += "</div>";
                        html += "</div>";
                        html += "</td>";
                        html += "</tr>";

                    }
                    $("#tableNoiDungTimKiem > tbody").append(html);

                } else {
                    $("#tableNoiDungTimKiem > tbody").append("<span style='color:red;'>Không tìm thấy câu hỏi</span>");
                }
            }
        }
    });

    return false;
}

function timKiemCauHoi() {
    var linhVucID = $("#ctl00_MainContent_ddlLinhVuc").val();
    var noiDungCauHoi = $("#ctl00_MainContent_txtNoiDungCauHoi").val();

    $("#tableNoiDungTimKiem > tbody").html("");

    $.ajax({
        type: "POST",
        url: "HoiDap.aspx/BindCauHoi",
        data: '{linhVucID:"' + linhVucID + '",noiDungCauHoi:"' + noiDungCauHoi + '"}',
        dataType: "json",
        async: "true",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var json = eval('(' + data.d + ')');
            if (json != null) {
                if (json.length > 0) {
                    var html = "";
                    for (var i = 0 ; i < json.length; i++) {
                        var row = i + 1;
                        
                        html += "<tr>";
                        html += "<td>";
                        html += "<div class='panel panel-default'>";
                        html += "<div class='panel-body'>";
                        html += "<header style='border-bottom: none;'>";
                        html += "       <div class='col-md-9' style='padding-left: 0px; padding-right: 0px;font-weight: bold;font-size: 16px; color: #460c69'>";
                        html += "           <div class='";
                        html += "linhVuc" + row + "'>";
                        html += "               <i class='fa fa-adjust'></i>&nbsp;";
                        html += json[i].TenLinhVuc;
                        html += "           </div>";
                        html += "       </div>";
                        html += "       <div class='col-md-9' style='padding-left: 0px; padding-right: 0px;'>";
                        html += "           <div class='";
                        html += "nguoiGui" + row + "'>";
                        html += "               <i class='glyphicon glyphicon-user'></i>&nbsp;";
                        html += json[i].HoTen;
                        html += "           </div>";
                        html += "           <div class='ngayGui";
                        html += row + "' style='font-size: 13px;'>";
                        html += "               <i class='glyphicon glyphicon-time' style='font-size: 13px; margin: 5px 0px 10px 0px'></i>&nbsp;";
                        html += json[i].NgayHoi_Str;
                        html += "           </div>";
                        html += "       </div>";
                        html += "       <div class='col-md-3'>";
                        html += "           <div id='trangThai";
                        html += row + "'> ";
                        if (json[i].IDTraLoi != 0) {
                            html += "Đã trả lời";
                        }
                        else {
                            html += "Chưa trả lời";
                        }
                        html += "           </div>";
                        html += "       </div>";
                        html += "   </header>";

                        html += "   <div>";
                        html += "       <div id='ngayTiep' runat='server' style='font-size: 18px; font-weight: bold; font-family: 'Times New Roman';'>";
                        html += "           <label id=\"noiDungCauHoi" + row + "\" onclick='showDetailSearchQA(\"CauHoi\", " + row + ")' style=\"width: 100%;font-size: 16px;font-weight: bold;\">";
                        if (json[i].NDCauHoi.length != null && json[i].NDCauHoi.length != 0) {
                            if (json[i].NDCauHoi.length > 100) {
                                html += json[i].NDCauHoi.substring(0, 100) + "... .";
                            }
                            else {
                                html += json[i].NDCauHoi;
                            }
                        }
                        html += "           </label>";
                        html += "           <div id='noiDungChiTietCauHoi" + row + "' style='display: none'>";
                        html += json[i].NDCauHoi;
                        html += "           </div>";
                        html += "       </div>";
                        html += "       <div>";
                        if (json[i].NDTraLoi.length != null && json[i].NDTraLoi.length != 0) {
                            html += "           <label id=\"noiDungCauTraLoi" + row + "\" onclick='showDetailSearchQA(\"CauTL\", " + row + ")' class=\"control-label\" style=\"width: auto; padding: 10px 10px 0px 10px; border-radius: 5px\">";
                            if (json[i].NDTraLoi.length > 100) {
                                html += "-- " + json[i].NDTraLoi.substring(0, 100) + "... .";
                            }
                            else {
                                html += "-- " + json[i].NDTraLoi;
                            }
                        }
                        else {
                            html += "";
                        }
                        html += "           </label>";
                        html += "       </div>";
                        html += "       <div id='noiDungChiTietCauTL" + row + "' style='display: none;'>";
                        html += json[i].NDTraLoi;
                        html += "       </div>";

                        if (json[i].NDCauHoi.length >= 100 || json[i].NDTraLoi.length >= 100) {
                            html += "       <div id=\"idXemChiTiet" + row + "\" onclick='showDetailSearchQA(\"XemChiTiet\", " + row + ")' style=\"display: block; cursor: pointer; width: auto;\">";
                            html += "           <span class='btn btn-warning btn-sm' style='color:black;'>";
                            html += "Xem chi tiết";
                            html += "           </span>";
                            html += "       </div>";
                        }
                        if (json[i].NDCauHoi.length < 100 && json[i].NDTraLoi.length < 100) {
                            html += "";
                        }

                        html += "   </div>";
                        html += "</div>";
                        html += "</div>";
                        html += "</td>";
                        html += "</tr>";

                    }
                    $("#tableNoiDungTimKiem > tbody").append(html);

                } else {
                    $("#tableNoiDungTimKiem > tbody").append("<span style='color:red;'>Không tìm thấy câu hỏi</span>");
                }
            }
        }
    });



    return false;
}

function showDetailSearchQA(type, row) {
    var detailQuesion = $('#noiDungChiTietCauHoi' + row).text();
    console.log(detailQuesion);
    var summaryQuesion = $('#noiDungCauHoi' + row).text();

    var detailAnswer = $('#noiDungChiTietCauTL' + row).text();
    console.log("1: " + detailAnswer);
    var summaryAnswer = $('#noiDungCauTraLoi' + row).text();

    if (type == "CauHoi") {
        $('#noiDungCauHoi' + row).text(detailQuesion);
        $('#noiDungChiTietCauHoi' + row).text(summaryQuesion);
    }

    if (type == "CauTL") {
        $('#noiDungCauTraLoi' + row).text(detailAnswer);
        $('#noiDungChiTietCauTL' + row).text(summaryAnswer);
    }

    if (type == "XemChiTiet") {
        $('#noiDungCauHoi' + row).text(detailQuesion);
        $('#noiDungChiTietCauHoi' + row).text(summaryQuesion);

        $('#noiDungCauTraLoi' + row).text(detailAnswer);
        $('#noiDungChiTietCauTL' + row).text(summaryAnswer);
    }
}

function showthongBaoSuccess() {
    $("#successSubmit").modal();
    return false;
}

function hideSuccessSubmit() {
    resetForm();
    $("#successSubmit").modal("hide");

}

function changeLinhVucCauHoi() {
    var linhVucID = $("#ctl00_MainContent_ddlLinhVuc_GuiCauHoi").val();
    
    $("#ctl00_MainContent_hdfLinhVucCauHoi").val(linhVucID);
}

function resetForm() {
    $("#ctl00_MainContent_ddlLinhVuc_GuiCauHoi").val('-- Chọn lĩnh vực --').trigger('change');
    $("#ctl00_MainContent_txtGuiCauHoi").val("");
}

function checkThongTin() {
    var linhVucID = $("#ctl00_MainContent_ddlLinhVuc_GuiCauHoi").val();

    console.log('linhVucID', linhVucID);
    if (linhVucID != null && linhVucID != 0 && linhVucID != "-- Chọn lĩnh vực --") {
        return false;
        $("#messageError").hide();
    }
    else {
        $("#messageError").show();
        return false;
    }
}

function checkValidation() {
    
};