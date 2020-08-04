//function CheckRole() {
//	var roleID = $('#hdfRoleUser').val();
//	if (roleID == 1) {
//		$('#lanh-dao').show();
//		$('#truong-phong').hide();
//		$('#chuyen-vien').hide();
//	} else if (roleID == 2) {
//		$('#lanh-dao').hide();
//		$('#truong-phong').show();
//		$('#chuyen-vien').hide();
//	} else {
//		$('#lanh-dao').hide();
//		$('#truong-phong').hide();
//		$('#chuyen-vien').show();
//	}
//}

//function CheckByCap() {
//    var capUBND = $('#hdfCapUBND').val();
//    var capThanhTra = $('#hdfCapThanhTra').val();
//	if (capUBND == "True") {
//	    $('.li-phangq-tp').hide();
//	    $('.li-duyetgq-tp').hide();
//		$('.li-gq-tp').hide();
//		$('.li-gq-cv').hide();
//	}
//	if (capUBND == "False") {
//	    $('.li-phangq-tp').show();
//	    $('.li-duyetgq-tp').show();
//		$('.li-gq-tp').show();
//		$('.li-gq-cv').show();
//	}
//	if (capUBND == "False" && capThanhTra == "False") {
//	    $('.li-phanxl-ld').hide();
//	    $('.li-duyetxl-ld').hide();
//	    $('.li-phanxl-tp').hide();
//	    $('.li-duyetxl-tp').hide();
//	    $('.li-xl-tp').hide();
//	}
//}

function autoloadTask() {
	var coQuanID = $('#hdfCoQuanID').val();
	var roleID = $('#hdfRoleUser').val();
	var phongBanID = $('#hdfPhongBanID').val();
	$.get(
		"/Handler/NotifyHandler/CountTask.ashx",
		{
			coQuanID: coQuanID,
			roleID: roleID,
			phongBanID: phongBanID
		}
	).done(function (data) {
	    var number = JSON.parse(data);
	    $("span.notify_sum").text(number);
	    //$('span.notify ').show();
	});
}

function CountKeHoachTTCanDuyet() {

	var coQuanID = $('#hdfCoQuanID').val();
	var roleID = $('#hdfRoleUser').val();
	var phongBanID = $('#hdfPhongBanID').val();
	$.get(
		"/Handler/NotifyHandler/CountKeHoachTTCanDuyet.ashx",
		{
			coQuanID: coQuanID,
			roleID: roleID,
			phongBanID: phongBanID
		}
	).done(function (data) {
		var number = JSON.parse(data);
		$("span.duyetkh_lanhdao").text(number);
	});
}

function ShowNotify() {
    var lanhDao = 1;
    var lanhDaoPhong = 2;
    var chuyenVien = 3;

	var roleID = $('#hdfRoleUser').val();
	if (roleID == 1) {
	    var loadDTPhanXL = setTimeout(CountKeHoachTTCanDuyet, 200);
	}
	else if (roleID == 2) {

	} else if (roleID == 3) {

	}

}
