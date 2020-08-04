function CheckRole() {
	var roleID = $('#hdfRoleUser').val();
	if (roleID == 1) {
		$('#lanh-dao').show();
		$('#truong-phong').hide();
		$('#chuyen-vien').hide();
	} else if (roleID == 2) {
		$('#lanh-dao').hide();
		$('#truong-phong').show();
		$('#chuyen-vien').hide();
	} else {
		$('#lanh-dao').hide();
		$('#truong-phong').hide();
		$('#chuyen-vien').show();
	}
}

function CheckByCap() {
    var capUBND = $('#hdfCapUBND').val();
    var capThanhTra = $('#hdfCapThanhTra').val();
	if (capUBND == "True") {
	    $('.li-phangq-tp').hide();
	    $('.li-duyetgq-tp').hide();
		$('.li-gq-tp').hide();
		$('.li-gq-cv').hide();
	}
	if (capUBND == "False") {
	    $('.li-phangq-tp').show();
	    $('.li-duyetgq-tp').show();
		$('.li-gq-tp').show();
		$('.li-gq-cv').show();
	}
	if (capUBND == "False" && capThanhTra == "False") {
	    $('.li-phanxl-ld').hide();
	    $('.li-duyetxl-ld').hide();
	    $('.li-phanxl-tp').hide();
	    $('.li-duyetxl-tp').hide();
	    $('.li-xl-tp').hide();
	}
}

function autoloadTask() {
	var userID = $('#hdfUserID').val();
	var coQuanID = $('#hdfCoQuanID').val();
	var roleID = $('#hdfRoleUser').val();
	var phongBanID = $('#hdfPhongBanID').val();
	$.get(
		"/Handler/CountTask.ashx",
		{
			userID: userID,
			coQuanID: coQuanID,
			roleID: roleID,
			phongBanID: phongBanID
		}
	).done(function (data) {
	    var number = JSON.parse(data);
	    $("span.notify").text(number);
	    $('span.notify ').show();
	});
}

function autoCountDTPhanXL() {

	var userID = $('#hdfUserID').val();
	var coQuanID = $('#hdfCoQuanID').val();
	var roleID = $('#hdfRoleUser').val();
	var phongBanID = $('#hdfPhongBanID').val();
	$.get(
		"/Handler/NotifyHandler/CountPhanXL.ashx",
		{
			userID: userID,
			coQuanID: coQuanID,
			roleID: roleID,
			phongBanID: phongBanID
		}
	).done(function (data) {

		var number = JSON.parse(data);
		$("span.phanxl-ld").text(number);
		$("span.phanxl-tp").text(number);
	});
}

function autoCountDTDuyetXL() {

	var userID = $('#hdfUserID').val();
	var coQuanID = $('#hdfCoQuanID').val();
	var roleID = $('#hdfRoleUser').val();
	var phongBanID = $('#hdfPhongBanID').val();
	$.get(
		"/Handler/NotifyHandler/CountDuyetXL.ashx",
		{
			userID: userID,
			coQuanID: coQuanID,
			roleID: roleID,
			phongBanID: phongBanID
		}
	).done(function (data) {

		var number = JSON.parse(data);
		$("span.duyetxl-ld").text(number);
		$("span.duyetxl-tp").text(number);
	});
}

function autoCountDTPhanGQ() {

    var coQuanID = $('#hdfCoQuanID').val();
    var roleID = $('#hdfRoleUser').val();
    var userID = $('#hdfUserID').val();
	$.get(
		"/Handler/NotifyHandler/CountPhanGQ.ashx",
		{
		    coQuanID: coQuanID,
		    roleID: roleID,
		    userID: userID
		}
	).done(function (data) {

	    var number = JSON.parse(data);
	    if (roleID == 1)
	        $("span.phangq-ld").text(number);
	    if(roleID == 2)
	        $("span.phangq-tp").text(number);
	});
}

function autoCountDTDuyetGQ() {

    var coQuanID = $('#hdfCoQuanID').val();
    var roleID = $('#hdfRoleUser').val();
    var userID = $('#hdfUserID').val();
	$.get(
		"/Handler/NotifyHandler/CountDuyetGQ.ashx",
		{
		    coQuanID: coQuanID,
		    roleID: roleID,
		    userID: userID
		}
	).done(function (data) {

	    var number = JSON.parse(data);
	    if (roleID == 1)
	        $("span.duyetgq-ld").text(number);
	    if (roleID == 2)
	        $("span.duyetgq-tp").text(number);
	});
}

function autoCountDTCanXL() {

	var userID = $('#hdfUserID').val();
	var coQuanID = $('#hdfCoQuanID').val();
	var roleID = $('#hdfRoleUser').val();
	var phongBanID = $('#hdfPhongBanID').val();
	$.get(
		"/Handler/NotifyHandler/CountDTCanXL.ashx",
		{
			userID: userID,
			coQuanID: coQuanID,
			phongBanID: phongBanID
		}
	).done(function (data) {

		var number = JSON.parse(data);
		$("span.xl-cv").text(number);
		$("span.xl-tp").text(number);
	});
}

function autoCountDTCanGQ() {

	var userID = $('#hdfUserID').val();
	var coQuanID = $('#hdfCoQuanID').val();
	var roleID = $('#hdfRoleUser').val();
	var phongBanID = $('#hdfPhongBanID').val();
	$.get(
		"/Handler/NotifyHandler/CountDTCanGQ.ashx",
		{
			userID: userID,
			coQuanID: coQuanID
		}
	).done(function (data) {

		var number = JSON.parse(data);
		$("span.gq-cv").text(number);
		$("span.gq-tp").text(number);
	});
}

function ShowNotify() {
	var roleID = $('#hdfRoleUser').val();
	if (roleID == 1) {
		var loadDTPhanXL = setTimeout(autoCountDTPhanXL, 200);
		var loadDTDuyetXL = setTimeout(autoCountDTDuyetXL, 200);
		var loadDTPhanGQ = setTimeout(autoCountDTPhanGQ, 200);
		var loadDTDuyetGQ = setTimeout(autoCountDTDuyetGQ, 200);
	}
	else if (roleID == 2) {
	    var loadDTPhanXL = setTimeout(autoCountDTPhanXL, 200);
	    var loadDTDuyetXL = setTimeout(autoCountDTDuyetXL, 200);
	    var loadDTPhanGQ = setTimeout(autoCountDTPhanGQ, 200);
	    var loadDTDuyetGQ = setTimeout(autoCountDTDuyetGQ, 200);
		var loadDTCanXL = setTimeout(autoCountDTCanXL, 200);
		var loadDTCanGQ = setTimeout(autoCountDTCanGQ, 200);
	} else if (roleID == 3) {
	    var loadDTCanXL = setTimeout(autoCountDTCanXL, 200);
	    var loadDTCanGQ = setTimeout(autoCountDTCanGQ, 200);
	}


	$(".swap_notify").show()
}

function HideNotify() {
	$(".swap_notify").hide();
}