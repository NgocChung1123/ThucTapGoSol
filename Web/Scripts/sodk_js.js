/**
 * @author ChuongNT
 */

// searchSTT
				function makeTextBlinking()
				{
					var randomColor = '#'+Math.floor(Math.random()*16777215).toString(16);
					$("#table th.for_tooltip").css("color", randomColor);
					setTimeout(function(){
						makeTextBlinking();
					}, 100);
				}
				
				function resetSearchStt()
				{
					$("#txt_search_stt").val(oldSearchStt);
					return false;
				}

				function searchStt()
				{
					var stt = $.trim($("#txt_search_stt").val());
					// neu truoc do chua search stt thi khong cho phep nhap trang
					if (oldSearchStt == "")
					{
						if ( stt == "")
						{
							$("#txt_search_stt").focus();
							return false;
						}
					}

					// chi chap nhan nhap so
					if (stt != "")
					{
						if (isNaN(stt)) 
						{
							$("#txt_search_stt").select();
							$("#txt_search_stt").focus();
							return false;
						}
					}

					if ( ! avoid_dupplicate_submit() )
						return false;

					doBlockUI();
					$.ajax({
						type : "POST",
						url : "searchStt",
						data : {
							stt : stt
						},
						success: function(response){
							var href = $.trim(window.location.href);
							
							// xoa bo nhung ky tu # o cuoi href
							var i = href.length;
							var j = 0;
							for (i = href.length - 1; i > 0; i-- )
							{
								if ( href.charAt(i) != '#' )
								{
									j = i;
									break;
								}
							}
							href = href.substring(0, j+1);
							
							// change page to 1
							i = href.indexOf("page=");
							if (i > 0)
							{
								href = href.substring(0, i) + "page=1";
							}
							
							// reload
							window.location.href = href;
						},
						error: function(){
							doUnBlockUI();
							alert_error("Có lỗi server !!!");
						}
					});
					return false;	
				}
// END searchSTT

// bind enter key to all the form
function bindEnterKeyForForm(formId)
{
	$("#" + formId).find('input[type="text"],input[type="password"],textarea,select,input:radio,input:checkbox')
	.bind('keydown', 'return', function(evt){
		// printForm
		if (formId == "printForm")
		{
			submit_print_form();
			return false;
		}
		
		$("#" + formId).submit();
		return false;
	});
}
				
// bind shortcut key to object depend on which form it is in				
function bindKeyShortcutForObject(jquery_obj)
{
	if ( ! jquery_obj[0].form)
		return false;
	
	var form_id = $(jquery_obj[0].form).attr("id");
	if (form_id == "editForm")
	{
		return bindKeyShortcutEditFormForObject(jquery_obj);
	}
	if (form_id == "searchForm")
	{
		return bindKeyShortcutSearchFormForObject(jquery_obj);
	}
	return false;
}

// bind key shortcut
function bindKeyShortcutEditForm()
{
	bindKeyShortcutEditFormForObject($(document));
	
	var editFormInputElements = $("#editForm").find('button,input[type="text"],textarea,select,input:radio,input:checkbox');
	bindKeyShortcutEditFormForObject(editFormInputElements);
	
	return false;
}

function bindKeyShortcutEditFormForObject(jquery_obj)
{
	jquery_obj.bind('keydown', 'alt+1', fncLuuVaThoat);
	jquery_obj.bind('keydown', 'alt+2', fncLuuVaTaoMoi);
	jquery_obj.bind('keydown', 'alt+3', fncIn);
	jquery_obj.bind('keydown', 'alt+4', fncHuyBo);
	jquery_obj.bind('keydown', 'alt+k', fncKiemTraTrungDon);
	return false;
}

function unbindKeyShortcutEditForm()
{
	$(document).unbind('keydown', fncLuuVaThoat);
	$(document).unbind('keydown', fncLuuVaTaoMoi);
	$(document).unbind('keydown', fncIn);
	$(document).unbind('keydown', fncHuyBo);
	$(document).unbind('keydown', fncKiemTraTrungDon);
	
	var editFormInputElements = $("#editForm").find('button,input[type="text"],textarea,select,input:radio,input:checkbox');
	editFormInputElements.unbind('keydown', fncLuuVaThoat);
	editFormInputElements.unbind('keydown', fncLuuVaTaoMoi);
	editFormInputElements.unbind('keydown', fncIn);
	editFormInputElements.unbind('keydown', fncHuyBo);
	editFormInputElements.unbind('keydown', fncKiemTraTrungDon);
	return false;
}

function fncRong(evt)
{
	return false;
}

var is_td_shortcut_key_hit = false;
function fncKiemTraTrungDon(evt)
{
	if (checkShortcutHit(1))
	{
		// phim tat chi tac dung khi cac truong duoc focus day du
		if (trungDon_focus_object)
		{
			// gia lap nhu la an chuot vao nut KT, giu focus tai vi tri hien tai 
			is_td_shortcut_key_hit = true;
			
			var td = findParentSpecificTag(trungDon_focus_object, "td");
			var btnKt = $(td).next().find("button").first();
			
			// neu nut do duoc enable thi check trung don
			if (btnKt.hasClass("save"))
			{
				btnKt.trigger("click");
			}
			
			// focus on the first visible button
//			var foundVisibleButton = false;
//			var theBtn = null;
//			$("#editForm button.btnKT").each(function(index, element){
//				if (! foundVisibleButton)
//				{
//					if ($(this).is(":visible"))
//					{
//						foundVisibleButton = true;
//						theBtn = $(this);
//					}
//				}
//			});
//			
//			if (theBtn != null)
//			{
//				theBtn.focus();
//				theBtn.trigger("click");
//			}
			return false;
		}
	}
}

function fncLuuVaThoat(evt)
{
	if (checkShortcutHit(1))
	{
		$("#saveAndCloseBtn").focus();
		$("#saveAndCloseBtn").trigger("click");
		return false;
	}
}
function fncLuuVaTaoMoi(evt)
{
	if (checkShortcutHit(1))
	{
		$("#saveAndCreateNewButton").focus();
		$("#saveAndCreateNewButton").trigger("click");
		return false;
	}
}
function fncIn(evt)
{
	if (checkShortcutHit(1))
		if ( ! isPrintDialogOpened )
		{
			$("#printEditFormBtn").focus();
			$("#printEditFormBtn").trigger("click");
			return false;
		}
}
function fncHuyBo(evt)
{
	if (checkShortcutHit(1))
	{
		$("#closeEditFormBtn").focus();
		$("#closeEditFormBtn").trigger("click");
		return false;
	}
}


// bind left, right arrow key for buttons
function bindKeyShortcutLeftRightButtons()
{
	$("body").delegate("button", "keydown", function(evt){
		switch (evt.keyCode) 
		{
	        case 37: // left
	        	fncButtonTruocDo(evt);
	        	return false;
	            break;
	            
	        case 39: // right
	        	fncButtonTiepTheo(evt);
	        	return false;
	            break;
		}
	});
	
//	$("button").bind('keydown', 'right', fncButtonTiepTheo);
//	$("button").bind('keydown', 'left', fncButtonTruocDo);
	return false;
}

function fncButtonTiepTheo(evt)
{
	var tg = $(evt.target);
	var btns = tg.parent().find("button");
	if (btns.length > 1)
	{
		var index = btns.index(tg[0]);
		var nextBtn = null;
		if (index == btns.length - 1)
			nextBtn = $(btns.get(0));
		else
			nextBtn = $(btns.get(index + 1));
		
		nextBtn.focus();	
		return false;
	}
}

function fncButtonTruocDo(evt)
{
	var tg = $(evt.target);
	var btns = tg.parent().find("button");
	if (btns.length > 1)
	{
		var index = btns.index(tg[0]);
		var nextBtn = null;
		if (index == 0)
			nextBtn = $(btns.get(btns.length - 1));
		else
			nextBtn = $(btns.get(index - 1));
		
		nextBtn.focus();	
		return false;
	}
}

/////////////////////////////////////////////////////////////////////////////////////////////////////////
// bind shortcut key trung don screen
function bindKeyShortcutTrungDonWindow()
{
	// neu co data thi moi cho sua, xoa, in
	$(document).bind('keydown', 'Alt+1', fncBoQuaTrungDon);
	$(document).bind('keydown', 'Alt+2', fncDanhDauTrungDon);
	return false;
}

function fncBoQuaTrungDon(event)
{
	if (!isCommentFormOpened)
		$("#boQuaBtn").trigger("click");
}

function fncDanhDauTrungDon(event)
{
	if (!isCommentFormOpened)
		$("#OkBtn").trigger("click");
}

var isCommentFormOpened = false;
// display comment form in trung don screen
function display_comment_form()
{
	isCommentFormOpened = true;
	bindKeyShortcutCommentFormForObject($(document));
	bindKeyShortcutCommentFormForObject($("#commentTA"));
	// show dialog
	$("#commentForm").dialog({
		title: "Ghi Chú",
		autoOpen: true,
		modal: true,
		resizable : false,
		minHeight: 250,
		minWidth: 380,
		beforeClose: function(event, ui) {
			isCommentFormOpened = false;
			// unbind key short cut
			unbindKeyShortcutCommentForm($(document));
			unbindKeyShortcutCommentForm($("#commentTA"));
			// reset form
			reset_comment_form();
		}
	});
}

function reset_comment_form() {
	$("#commentTA").val("");
}

// bind key short cut for comment form in trung don screen
function bindKeyShortcutCommentFormForObject(jquery_obj)
{
	jquery_obj.bind('keydown', 'Alt+3', fncCommitCommentForm);
	jquery_obj.bind('keydown', 'Alt+4', fncHuyBoCommentForm);
	return false;
}
// unbind key short cut for print dialog
function unbindKeyShortcutCommentForm(jquery_obj)
{
	jquery_obj.unbind('keydown', fncCommitCommentForm);
	jquery_obj.unbind('keydown', fncHuyBoCommentForm);
	return false;
}
function fncCommitCommentForm() {
	$("#commentForm_btnLuuTrungDon").trigger("click");
}
function fncHuyBoCommentForm() {
	$("#commentForm_btnHuy").trigger("click");
}
///////////////////////////////////////////////////////////////////////////////////////


// bind short cut key main screen
function bindKeyShortcutNghiepVu()
{
	// neu co data thi moi cho sua, xoa, in
	if (isTableHasData)
	{
		$(document).bind('keydown', 'Alt+c', fncXemLichSuNopDon);
		$(document).bind('keydown', 'Alt+s', fncSuaDonThu);
		$(document).bind('keydown', 'Alt+x', fncXoaDonThu);
		$(document).bind('keydown', 'Alt+i', fncInDonThu);
	}
	
	$(document).bind('keydown', 'Alt+n', fncDangKyMoi);
	$(document).bind('keydown', 'Alt+t', fncTimKiemDonThu);		// show search form (#searchForm)
	$(document).bind('keydown', 'Alt+e', fncExcel);
	$(document).bind('keydown', 'Alt+d', fncThoatNghiepVu);
	
	// chuyen trang
	$(document).bind('keydown', 'Alt+l', fncTrangTiepTheo);
	$(document).bind('keydown', 'Alt+p', fncTrangTruocDo);

	// hien tat, thu nho cot noi dung trong bang du lieu short cut
	$(document).bind('keydown', 'Alt+h', fncHienNoiDung);

	// show search form quick (#searchForm_quick)
	$(document).bind('keydown', 'Alt+k', fncTimKiemDonThu_quick);

	return false;
}

function unbindKeyShortcutNghiepVu()
{
	return false;
}

// hien tat / thu nho
function fncHienNoiDung() {
	if (isAtMainScreenNghiepVu())
	{
		var rows = $("div.dashboard #table > tbody > tr");
		if (rows.length > 0)
		{
			// focus up row
			var selected_row = $("div.dashboard #table > tbody > tr.selected_hl")[0];
			var span_condense = $(selected_row).find("div.needCondense:visible").find("span.condense_control");
			span_condense.click();
			return false;
		}
	}
	return false;
}

function fncTrangTiepTheo(evt)
{
	if (isAtMainScreenNghiepVu())
	{
		var children = $("#pagingDiv").children();
		if (children.length > 1)
		{
			// find current span
			var current_span_index = children.index($("#pagingDiv span.current")[0]);
			var a_target = null;
			
			if (current_span_index == children.length - 1)
				a_target = $(children.get(0));
			else
				a_target = $(children.get(current_span_index + 1));
			
			if (a_target != null)
			{
				doBlockUI();
				window.location.href = a_target.attr("href");
			}
		}
		return false;
	}
}

function fncTrangTruocDo(evt)
{
	if (isAtMainScreenNghiepVu())
	{
		var children = $("#pagingDiv").children();
		if (children.length > 1)
		{
			// find current span
			var current_span_index = children.index($("#pagingDiv span.current")[0]);
			var a_target = null;
			
			if (current_span_index == 0)
				a_target = $(children.get(children.length - 1));
			else
				a_target = $(children.get(current_span_index - 1));
			
			if (a_target != null)
			{
				doBlockUI();
				window.location.href = a_target.attr("href");
			}
		}
		return false;
	}
}

function fncThoatNghiepVu(evt)
{
	if (isAtMainScreenNghiepVu())
	{
		doBlockUI();
		window.location.href = "mainMenu?show=1";
		return false;
	}
}

function fncExcel(evt)
{
	if (isAtMainScreenNghiepVu())
	{
		$("#nv_excel").trigger("click");
		return false;
	}
}

function fncTimKiemDonThu(evt)
{
	if (isAtMainScreenNghiepVu())
	{
		$("#nv_search").trigger("click");
		return false;
	}
}

function fncTimKiemDonThu_quick(evt)
{
	if (isAtMainScreenNghiepVu())
	{
		$("#quick_search_btn").trigger("click");
		return false;
	}
}

function fncDangKyMoi(evt)
{
	if (isAtMainScreenNghiepVu())
	{
		$("#add_dm").trigger("click");
		return false;
	}
}

function fncSuaDonThu(evt)
{
	if (isAtMainScreenNghiepVu())
	{
		$("#nv_edit_sodk").trigger("click");
		return false;
	}
}

function fncXoaDonThu(evt)
{
	if (isAtMainScreenNghiepVu())
	{
		$("#nv_xoa_sodk").trigger("click");
		return false;
	}
}

function fncInDonThu(evt)
{
	if (isAtMainScreenNghiepVu())
	{
		$("#nv_print").trigger("click");
		return false;
	}
}


// is user at main screen nghiep vu
function isAtMainScreenNghiepVu()
{
//	if (isDisplayOnTop($("div.dashboard #table")[0]))
//		return true;
	var fadeDivs = $("#fade.black_overlay, .ui-widget-overlay, .jquery-lightbox-overlay");
	for (var i = 0; i < fadeDivs.length; i++)
	{
		if ($(fadeDivs.get(i)).is(":visible"))
		{
			return false;
		}
	}
	
	return true;
}

// bind left, right arrow key
function bindKeyShortCutTableLeftRightArrow()
{
	$(document).bind('keydown', 'left', fncScrollLeft);
	$(document).bind('keydown', 'right', fncScrollRight);
	return false;
}

// bind up, down arrow key
function bindKeyShortcutTableUpdownArrow()
{
	$(document).bind('keydown', 'up', fncUpOneRow);
	$(document).bind('keydown', 'down', fncDownOneRow);
	return false;
}

function unbindKeyShortcutTableUpdownArrow()
{
	//$("div.dashboard #table").unbind('keydown', 'up', fncUpOneRow);
	//$("div.dashboard #table").unbind('keydown', 'down', fncDownOneRow);
	return false;
}

// on start up refresh the navigation button
$(document).ready(function(){
	var pr = $("div.dashboard #table").parent();
	if (pr.scrollLeft() > horizontalScrollStep) {
		$("#nav_left_arrow").css("display", "");
	}

	var parent = pr[0];
	if (parent) {
		var scroll_bar_width = parent.scrollWidth - pr.width();
		if (pr.scrollLeft() >= (scroll_bar_width)) {
			$("#nav_right_arrow").css("display", "none");
		}
	}
});

var horizontalScrollStep = 80;
function fncScrollLeft(event)
{
	if (isAtMainScreenNghiepVu())
	{
		var pr = $("div.dashboard #table").parent();
		var new_scroll_left = pr.scrollLeft() > horizontalScrollStep ? (pr.scrollLeft() - horizontalScrollStep) : 0;
		pr.scrollLeft(new_scroll_left);

		// display right arrow navigation
		$("#nav_right_arrow").css("display","");

		// at most left -> hide scroll left button,
		if (new_scroll_left < 10) {
			$("#nav_left_arrow").css("display", "none");
		}
	}
}

function fncScrollRight(event)
{
	if (isAtMainScreenNghiepVu())
	{
		var pr = $("div.dashboard #table").parent();
		var parent_width = pr.width();
		var new_scroll_left = pr.scrollLeft() < (parent_width - horizontalScrollStep)  ? (pr.scrollLeft() + horizontalScrollStep) : parent_width;
		if (new_scroll_left == parent_width)
			new_scroll_left += 120;
		pr.scrollLeft(new_scroll_left);

		// detect scroll at the right most of the scroll bar
		var parent = pr[0];
		if (parent) {
			var scroll_bar_width = parent.scrollWidth - parent_width;
			// display left arrow navigation
			$("#nav_left_arrow").css("display","");

			// at most left -> hide scroll right arrow
			if (new_scroll_left >= (scroll_bar_width + 10)) {
				$("#nav_right_arrow").css("display", "none");
			}
		}
	}
	
}

var isAtCheckTrungDonWindow = false;
function fixScrollHeight(selected_row_index)
{
	var rows, row, offset, scrollTop;
 // man hinh trung don
	if (isAtCheckTrungDonWindow)
	{
		// tbody rows
		rows = $("div.dashboard #table > tbody > tr");
		if (rows.length <= 1)
			return false;
		
		row = rows.eq(selected_row_index);
		offset = row.offset();
		scrollTop = offset.top > 400 ? offset.top - 400 : 0;
		if (selected_row_index == 0)
			scrollTop = 0;
		
		$("html:not(:animated),body:not(:animated)").animate({ scrollTop: scrollTop}, 200 );
		
		// thead rows		
		return false;
	}
	
 // man hinh nghiep vu

  // table without max-height style:
	// tbody rows
	rows = $("div.dashboard #table > tbody > tr");
	if (rows.length <= 1)
		return false;

	row = rows.eq(selected_row_index);
	offset = row.offset();
	scrollTop = offset.top > 400 ? offset.top - 400 : 0;
	if (selected_row_index == 0)
		scrollTop = 0;

	$("html:not(:animated),body:not(:animated)").animate({ scrollTop: scrollTop}, 200 );

	// thead rows
	return false;

  // scroll table mode ?
	// tbody rows
//	var rows = $("div.dashboard #table > tbody > tr");
//	if (rows.length <= 1)
//		return false;
//
//	// parent div of table, which contains scroll bars
//	var pr = $("div.dashboard #table").parent();
//	var pr_height = pr.height();
//
//	// thead rows
//	var thead_rows = $("div.dashboard #table > thead > tr");
//	var total_previous_rows_height = 0;					// heigh of regions before selected row
//	for (var i = 0; i < thead_rows.length; i++)
//	{
//		total_previous_rows_height += thead_rows.get(i).offsetHeight;
//	}
//
//	for (var i = 0; i <= selected_row_index; i++)
//	{
//		total_previous_rows_height += rows.get(i).offsetHeight;
//	}
//
//	var scrollTop = total_previous_rows_height - pr_height + 30;
//	scrollTop = scrollTop > 0 ? scrollTop : 0;
//	pr.animate({
//		scrollTop: scrollTop
//	}, 200);

//	return false;
}

function fncUpOneRow(evt)
{
	if (isAtMainScreenNghiepVu())
	{
		var rows = $("div.dashboard #table > tbody > tr");
		if (rows.length > 0)
		{
			// focus up row
			var selected_row = $("div.dashboard #table > tbody > tr.selected_hl")[0];
			var r_index = rows.index(selected_row);
			var focus_target = r_index == 0 ? $("div.dashboard #table > tbody > tr").last() : $($("div.dashboard #table > tbody > tr").get(r_index - 1));
			focus_target.trigger("click");
				//focus_target[0].scrollIntoView();
			// auto scroll to that row
			var new_r_index = r_index == 0 ? rows.length - 1 : r_index - 1;
			fixScrollHeight(new_r_index);
			return false;
		}
	}
}

function fncDownOneRow(evt)
{
	if (isAtMainScreenNghiepVu())
	{
		var rows = $("div.dashboard #table > tbody > tr");
		if (rows.length > 0)
		{
			var selected_row = $("div.dashboard #table > tbody > tr.selected_hl")[0];
			var r_index = rows.index(selected_row);
			var focus_target = r_index == rows.length - 1 ? $("div.dashboard #table > tbody > tr").first() : $($("div.dashboard #table > tbody > tr").get(r_index + 1));
			focus_target.trigger("click");
			//focus_target[0].scrollIntoView();
			var new_r_index = r_index == rows.length -1 ? 0 : r_index + 1;
			fixScrollHeight(new_r_index);
			return false;
		}
	}
}



				
// wait for end of loading and focus
function waitAndFocus(targetId)
{
	if (isBlockUI)
		setTimeout(function(){waitAndFocus(targetId);},50);
	else
		$("#" + targetId).focus();
	return false;
}
				
// handle tab pressed when a select is choosen
function focusOnNextTab(source_id)
{
	// tabs-1
	if (source_id == "sex")
	{
		if ($("#id_sodkSo").is(":enabled"))
		{
			$("#thanhPhan").focus();
		}
		else
		{
			$("div#tabs").tabs( "select" , 3 );
			$("#sl_tenNguoiKy").focus();
		}
		return false;
	}
	if (source_id == "thanhPhan")
	{
		$("#quoctich").focus();
		return false;
	}
	if (source_id == "quoctich")
	{
		$("#ndDanToc").focus();
		return false;
	}
	if (source_id == "ndDanToc")
	{
		$("#tinhThanhPhosId").focus();
		return false;
	}
	if (source_id == "tinhThanhPhosId")
	{
		if ($("#btnKT_tinhThanh").is(":disabled") == false)
			$("#btnKT_tinhThanh").focus();
		else
			setTimeout(function(){waitAndFocus("sl_quanHuyens");},50);
		return false;
	}
	if (source_id == "sl_quanHuyens")
	{
		if ($("#btnKT_quanHuyen").is(":disabled") == false)
			$("#btnKT_quanHuyen").focus();
		else
			setTimeout(function(){waitAndFocus("sl_xaPhuongs");},50);
		return false;
	}
	if (source_id == "sl_xaPhuongs")
	{
		if ($("#btnKT_xaPhuong").is(":disabled") == false)
			$("#btnKT_xaPhuong").focus();
		else
			setTimeout(function(){waitAndFocus("chiTietDiaChi");},50);
		return false;
	}
	
	// tabs-2
	if (source_id == "sl_tinhThanhPhosId_2")
	{
		setTimeout(function(){waitAndFocus("sl_quanHuyens2");},50);
		return false;
	}
	if (source_id == "sl_quanHuyens2")
	{
		setTimeout(function(){waitAndFocus("sl_xaPhuongs2");},50);
		return false;
	}
	if (source_id == "sl_xaPhuongs2")
	{
//		doChangeSection(2);
		$("#chiTietDiaChi1").focus();
		return false;
	}
	
	// tabs-3
	if (source_id == "sl_loaiDon")
	{
		setTimeout(function(){waitAndFocus("sl_loaiKnTcs");},50);
		return false;
	}
	if (source_id == "sl_loaiKnTcs")
	{
		setTimeout(function(){waitAndFocus("sl_loaiKnTcCts");},50);
		return false;
	}
	
	// tabs-4
	if (source_id == "sl_tenNguoiKy")
	{
		if ($("#id_sodkSo").is(":enabled"))
		{
			$("#sl_thamQuyen").focus();
		}
		else
		{
			$("#txt_giaiQuyet").focus();
		}
		return false;
	}
	if (source_id == "sl_thamQuyen")
	{
		setTimeout(function(){waitAndFocus("sl_nhomThamQuyen");},50);
		return false;
	}
	
	// search form (#searchForm)
	if (source_id == "sl_loaiDonSearch")
	{
		setTimeout(function(){waitAndFocus("sl_loaiKnTcsSearch");},50);
		return false;
	}
	if (source_id == "sl_loaiKnTcsSearch")
	{
		setTimeout(function(){waitAndFocus("sl_loaiKnTcCtsSearch");},50);
		return false;
	}
	if (source_id == "tinhThanhPhosIdSearch")
	{
		setTimeout(function(){waitAndFocus("sl_quanHuyensSearch");},50);
		return false;
	}
	if (source_id == "sl_quanHuyensSearch")
	{
		setTimeout(function(){waitAndFocus("sl_xaPhuongsSearch");},50);
		return false;
	}
	if (source_id == "sl_thamQuyenSearch")
	{
		setTimeout(function(){waitAndFocus("sl_nhomThamQuyenSearch");},50);
		return false;
	}
	if (source_id == "sl_nhomThamQuyenSearch")
	{
		$("#txt_coQuanGiaiQuyetTiepSearch").focus();
		return false;
	}

	// search form quick (#searchForm_quick)
	if (source_id == "tinhThanhPhosIdSearch_quick")
	{
		setTimeout(function(){waitAndFocus("sl_quanHuyensSearch_quick");},50);
		return false;
	}
	if (source_id == "sl_quanHuyensSearch_quick")
	{
		setTimeout(function(){waitAndFocus("sl_xaPhuongsSearch_quick");},50);
		return false;
	}

	// default action, move to the next visible input element
	var editForm = $("#" + source_id)[0].form;
	var editFormFields = $(editForm).find('button,input[type="text"],textarea,select,input:radio:checked,input:checkbox');
	var index = editFormFields.index($("#" + source_id));
	var nextElement = null;
	if ( index > -1 && ( index + 1 ) < editFormFields.length ) 
	{ 
		var i;
		for (i = index + 1; i < editFormFields.length; i++)
		{
			nextElement = editFormFields.eq(i);
			if (nextElement.is(":visible"))
				break;
			nextElement = null;
		}
    }
	if (nextElement != null)
		nextElement.focus();
	
	return false;
}


//handle shift+tab pressed when a select is choosen
function focusOnPreviousTab(source_id)
{
	// tabs-1
	if (source_id == "sl_xaPhuongs")
	{
		if ($("#btnKT_quanHuyen").attr("disabled") == "disabled")
			$("#sl_quanHuyens").focus();
		else
			$("#btnKT_quanHuyen").focus();
		return false;
	}
	if (source_id == "sl_quanHuyens")
	{
		if ($("#btnKT_tinhThanh").attr("disabled") == "disabled")
			$("#tinhThanhPhosId").focus();
		else
			$("#btnKT_tinhThanh").focus();
		return false;
	}
	if (source_id == "tinhThanhPhosId")
	{
		$("#ndDanToc").focus();
		return false;
	}
	if (source_id == "ndDanToc")
	{
		$("#quoctich").focus();
		return false;
	}
	if (source_id == "quoctich")
	{
		$("#thanhPhan").focus();
		return false;
	}
	if (source_id == "thanhPhan")
	{
		$("#sex").focus();
		return false;
	}
	if (source_id == "sex")
	{
		if ($("#id_sodkSo").is(":enabled"))
		{
			if ($("#btnKT_ndHoTen").hasClass("save"))
				$("#btnKT_ndHoTen").focus();
			else
				$("#ndHoTen").focus();
		}
		else
		{
			$("#txt_ngayQuaHan").focus();
		}
		return false;
	}
	
	
	// tabs-2
	if (source_id == "sl_tinhThanhPhosId_2")
	{
		if ($("#ndDanToc1").is(":visible"))
		{
			$("#ndDanToc1").focus();
			return false;
		}
		else
		{
			if ($("#btnKT_co_quan_btc").hasClass("save"))
				$("#btnKT_co_quan_btc").focus();
			else
				$("#co_quan_btc").focus();
			return false;
		}
	}
	if (source_id == "sl_quanHuyens2")
	{
		$("#sl_tinhThanhPhosId_2").focus();
		return false;
	}
	if (source_id == "sl_xaPhuongs2")
	{
		$("#sl_quanHuyens2").focus();
		return false;
	}
	if (source_id == "thanhPhan1")
	{
		if ($("#btnKT_ndHoTen1").hasClass("save"))
			$("#btnKT_ndHoTen1").focus();
		else
			$("#ndHoTen1").focus();
		return false;
	}
	
	
	// tabs-3
	if (source_id == "sl_loaiDon")
	{
		doChangePreviousSection(1);
		return false;
	}
	if (source_id == "sl_loaiKnTcs")
	{
		$("#sl_loaiDon").focus();
		return false;
	}
	
	// tabs-4
	if (source_id == "sl_tenNguoiKy")
	{
		if ($("#id_sodkSo").is(":enabled"))
		{
			$("#sl_tenNguoiXL").focus();
		}
		else
		{
			$("div#tabs").tabs( "select" , 0 );
			$("#sex").focus();
		}
		return false;
	}
	if (source_id == "huongGQ")
	{
		doChangePreviousSection(2);
		return false;
	}	
	if (source_id == "sl_thamQuyen")
	{
		$("#sl_tenNguoiKy").focus();
		return false;
	}
	
	// search form
	if (source_id == "sl_thamQuyenSearch")
	{
		$("#txt_coQuanDaGiaiQuyetSearch").focus();
		return false;
	}
	
	
	// default action, move to the previous visible input element
	var editForm = $("#" + source_id)[0].form;
	var editFormFields = $(editForm).find('button,input[type="text"],textarea,select,input:radio:checked,input:checkbox');
	var index = editFormFields.index($("#" + source_id));
	var previousElement = null;
	if ( index > 0 ) 
	{ 
		var i;
		for (i = index - 1; i >= 0; i--)
		{
			previousElement = editFormFields.eq(i);
			if (previousElement)
			{
				if (previousElement.is(":visible"))
				{
					if (previousElement.attr("id") != '')
					{
						if (previousElement.attr("id").indexOf("ufd") == -1)
							break;
					}
					else
						break;
				}
			}
			previousElement = null;
		}
	}
	if (previousElement != null)
		previousElement.focus();
	
	return false;
}

/////////////////////////////   SHIFT + TAB   ////////////////////////////

//bind shift+tab keys to the form to simulate tabindex
function simulateShiftTabIndex()
{
	// in case some fields are disabled
	doStimulateShiftTabActionSpecial("cb_status","sl_kemTheoVB","txt_phucDap",-1);
	doStimulateShiftTabActionSpecial("txt_giaiQuyet","txt_coQuanTq","sl_tenNguoiKy",-1);
	doStimulateShiftTabActionSpecial("txt_ngayQuaHan","txt_ngay","closeEditFormBtn",-1);
	
	
	// tab header
	doStimulateShiftTabActionChangeSection($("ul.ui-tabs-nav a[href='#tabs-2']"),0);
	doStimulateShiftTabActionChangeSection($("ul.ui-tabs-nav a[href='#tabs-3']"),1);
	doStimulateShiftTabActionChangeSection($("ul.ui-tabs-nav a[href='#tabs-4']"),2);
	doStimulateShiftTabActionChangeSection($("ul.ui-tabs-nav a[href='#tabs-5']"),3);
	
	// tabs-1
	doStimulateShiftTabAction("sl_xaPhuongs","chiTietDiaChi");
	doStimulateShiftTabAction("closeEditFormBtn","id_sodkSo");
		
	// tabs-2
	doStimulateShiftTabActionChangeSection($("input[name=hsdt_type]"), 0);

	// tabs-3
	
	// tabs-4
	
	// tabs-5
	doStimulateShiftTabActionChangeSection($("#saveAndCloseBtn"), 3);
	
	return false;
}

function doStimulateShiftTabActionSpecial(sourceId, targetId, alternateTargetId, section)
{
	$('#' + sourceId).bind('keydown', function(e) { 
		  if (e.shiftKey == 1)
		  {
			  var keyCode = e.keyCode || e.which; 
			  if (keyCode == 9) { 
			    e.preventDefault(); 
			    // call custom function here
			    //$("#" + targetId).focus();
			    if (isBlockUI)
			    	return false;
			    
			    if ($("#id_sodkSo").is(":enabled"))
			    	$("#" + targetId).focus();
			    else
			    {
			    	if (section >= 0)
			    		$("div#tabs").tabs( "select" , section );
			    	$("#" + alternateTargetId).focus();
			    }
			  }
		  }
	});
	return false;
}

function doStimulateShiftTabAction(targetId, sourceId)
{
	$('#' + sourceId).bind('keydown', function(e) { 
		  if (e.shiftKey == 1)
		  {
			  var keyCode = e.keyCode || e.which; 
			  if (keyCode == 9) { 
			    e.preventDefault(); 
			    // call custom function here
			    //$("#" + targetId).focus();
			    if (isBlockUI)
			    	return false;
			    
			    var targetE = $("#" + targetId);
			    targetE.focus();
			  }
		  }
	});
	return false;
}

function doStimulateShiftTabActionO(targetObj, srcObj)
{
	// if null return
	if (srcObj == null)
		return false;
	
	$(srcObj).bind('keydown', function(e) {
		if (e.shiftKey == 1)
		{
		  var keyCode = e.keyCode || e.which; 
		  if (keyCode == 9) { 
		    e.preventDefault(); 
		    // call custom function here
		    if (isBlockUI)
		    	return false;
		    targetObj.focus();
		  }
		}
	});
	return false;
}

function doStimulateShiftTabActionS(targetSelector, srcObj)
{
	// if null return
	if (srcObj == null)
		return false;
	
	$(srcObj).bind('keydown', function(e) { 
		if (e.shiftKey == 1)
		{
		  var keyCode = e.keyCode || e.which; 
		  if (keyCode == 9) { 
		    e.preventDefault(); 
		    // call custom function here
		    if (isBlockUI)
		    	return false;
		    $(targetSelector).first().focus();
		  }
		}
	});
	return false;
}

function doStimulateShiftTabActionChangeSection(source, section)
{
	$(source).bind('keydown', function(e) { 
		if (e.shiftKey == 1)
		{
		  var keyCode = e.keyCode || e.which; 
		  if (keyCode == 9) { 
		    e.preventDefault();
		    
		    if (isBlockUI)
		    	return false;
		    // call custom function here
		    doChangePreviousSection(section);
		    return false;
		  }
		}
	});
	return false;
}


function doChangePreviousSection(section)
{
	if (section == 0)
	{
    	$("div#tabs").tabs( "select" , 0 );    	
    	$("#chiTietDiaChi").focus();    	
		return false;
	}
	
	if (section == 1)
	{
		$("div#tabs").tabs( "select" , 1 );
//    	$("#sl_xaPhuongs2").focus();
    	$("#chiTietDiaChi1").focus();
		return false;
	}
	
	if (section == 2)
	{
		$("div#tabs").tabs( "select" , 2 );
    	$("#coquangq").focus();
		return false;
	}
	
	if (section == 3)
	{
		if ($("#tabs-5").is(":visible"))
		{
			$("div#tabs").tabs( "select" , 3 );
	    	$("#cb_status").focus();
		}
		else
		{
			$("div#tabs").tabs( "select" , 4 );
	    	$("#saveAndCloseBtn").focus();
		}
		return false;
	}
}


//////////////////////////////////////////////// TAB
				
// bind tab keys to the form to simulate tabindex
function simulateTabIndex()
{	
	doStimulateTabActionChangeSection("nguonDon_NgayChuyenDon", 0);
	
	// in case some fields are disabled
	doStimulateTabActionSpecial("txt_ngayQuaHan", "select_nguonDon", "sex", 0);
	doStimulateTabActionSpecial("txt_phucDap", "sl_kemTheoVB", "cb_status", -1);
	doStimulateTabActionSpecial("closeEditFormBtn", "id_sodkSo", "txt_ngayQuaHan", -1);
	
	// tabs-1	
	doStimulateTabAction("sex","thanhPhan");
	doStimulateTabAction("thanhPhan","quoctich");
	doStimulateTabAction("quoctich","ndDanToc");
	doStimulateTabAction("ndDanToc","tinhThanhPhosId");
	
	doStimulateTabAction("btnKT_ndHoTen","sex");
	doStimulateTabAction("btnKT_tinhThanh","sl_quanHuyens");
	doStimulateTabAction("btnKT_quanHuyen","sl_xaPhuongs");
	doStimulateTabAction("btnKT_xaPhuong","chiTietDiaChi");
	doStimulateTabActionChangeSection("chiTietDiaChi", 1);
	
	// tabs-2
	doStimulateTabAction("btnKT_ndHoTen1","thanhPhan1");
	doStimulateTabAction("btnKT_co_quan_btc","sl_tinhThanhPhosId_2");
	doStimulateTabActionChangeSection("chiTietDiaChi1", 2);
//	doStimulateTabActionChangeSection("sl_xaPhuongs2", 2);

	// tabs-3
	doStimulateTabActionChangeSection("coquangq", 3);
	
	// tabs-4
	doStimulateTabActionChangeSection("cb_status", 4);
	
	// tabs-5
	doStimulateTabAction("btnAddFilescan","saveAndCloseBtn");
//	doStimulateTabAction("closeEditFormBtn","id_sodkSo");
	
	return false;
}

function doStimulateTabActionSpecial(sourceId, targetId, alternateTargetId, section)
{
	$('#' + sourceId).bind('keydown', function(e) {
		if (e.shiftKey != 1)
		{
		  var keyCode = e.keyCode || e.which; 
		  if (keyCode == 9) { 
		    e.preventDefault(); 
		    // call custom function here
		    //$("#" + targetId).focus();
		    if (isBlockUI)
		    	return false;
		    
		    if ($("#id_sodkSo").is(":enabled"))
		    	$("#" + targetId).focus();
		    else
		    {
		    	if (section >= 0)
		    		$("div#tabs").tabs( "select" , section );
		    	$("#" + alternateTargetId).focus();
		    }
		  }
		}
	});
	return false;
}

function doStimulateTabAction(sourceId, targetId)
{
	$('#' + sourceId).bind('keydown', function(e) {
		if (e.shiftKey != 1)
		{
		  var keyCode = e.keyCode || e.which; 
		  if (keyCode == 9) { 
		    e.preventDefault(); 
		    // call custom function here
		    //$("#" + targetId).focus();
		    if (isBlockUI)
		    	return false;
		    
		    var targetE = $("#" + targetId);
		    if (targetE.is("select"))
		    {
		    	//targetE.attr('size',10);
		    }
		    targetE.focus();
		  }
		}
	});
	return false;
}

function doStimulateTabActionO(srcObj, targetObj)
{
	// if null return
	if (srcObj == null)
		return false;
	
	$(srcObj).bind('keydown', function(e) {
		if (e.shiftKey != 1)
		{
		  var keyCode = e.keyCode || e.which; 
		  if (keyCode == 9) { 
		    e.preventDefault(); 
		    // call custom function here
		    if (isBlockUI)
		    	return false;
		    targetObj.focus();
		  }
		}
	});
	return false;
}

function doStimulateTabActionS(srcObj, targetSelector)
{
	// if null return
	if (srcObj == null)
		return false;
	
	$(srcObj).bind('keydown', function(e) { 
		if (e.shiftKey != 1)
		{
		  var keyCode = e.keyCode || e.which; 
		  if (keyCode == 9) { 
		    e.preventDefault(); 
		    // call custom function here
		    if (isBlockUI)
		    	return false;
		    $(targetSelector).first().focus();
		  }
		}
	});
	return false;
}

function doStimulateTabActionChangeSection(sourceId, section)
{
	$('#' + sourceId).bind('keydown', function(e) { 
		if (e.shiftKey != 1)
		{
		  var keyCode = e.keyCode || e.which; 
		  if (keyCode == 9) { 
		    e.preventDefault();
		    
		    if (isBlockUI)
		    	return false;
		    // call custom function here
		    doChangeSection(section);
		    return false;
		  }
		}
	});
	return false;
}

function doChangeSection(section)
{
	if (section == 0)
	{
		if ( ! $("#tabs-1").is(":visible"))
		{
	    	$("div#tabs").tabs( "select" , 0 );
	    	$("input[name=hs_type]:checked").focus();
	    	return false;
		}
		else
		{
			$("input[name=hs_type]:checked").focus();
			return false;
		}
	}
	
	if (section == 1)
	{
    	$("div#tabs").tabs( "select" , 1 );
    	$("input[name=hsdt_type]:checked").focus();
		return false;
	}
	
	if (section == 2)
	{
		$("div#tabs").tabs( "select" , 2 );
    	$("#sl_loaiDon").focus();
		return false;
	}
	
	if (section == 3)
	{
		$("div#tabs").tabs( "select" , 3 );
    	$("#huongGQ").focus();
		return false;
	}
	
	if (section == 4)
	{
		$("div#tabs").tabs( "select" , 4 );
    	$("#saveAndCloseBtn").focus();
		return false;
	}
}

				
				
// is enough fields for submit				
function isEnoughFields()
{
	if ($("#ndHoTen").val() == "")
	{
		alert_error_callback(
				"Vui lòng nhập Họ và tên người KNTC !!!",
				function(){
					$("div#tabs").tabs( "select" , 0 );
					setTimeout(function(){ $("#ndHoTen").focus(); }, 300);
				}
		);
		return false;
	}
	
	if ( $("#sl_loaiDon").val() == "" || $("#sl_loaiDon").val() == "-1" )
	{
		alert_error_callback(
				"Vui lòng chọn Loại đơn !!!",
				function(){
					$("div#tabs").tabs( "select" , 2 );
					setTimeout(function(){ $("#sl_loaiDon").focus(); }, 300);
				}
		);
		return false;
	}
	
	if ( $("#sl_loaiKnTcs").val() == "" || $("#sl_loaiKnTcs").val() == "-1" )
	{
		alert_error_callback(
				"Vui lòng chọn Loại KN, TC !!!",
				function(){
					$("div#tabs").tabs( "select" , 2 );
					setTimeout(function(){ $("#sl_loaiKnTcs").focus(); }, 300);
				}
		);
		return false;
	}
	
	if ( $("#sl_loaiKnTcCts").val() == "" || $("#sl_loaiKnTcCts").val() == "-1" )
	{
		alert_error_callback(
				"Vui lòng chọn Chi Tiết Loại KN, TC !!!",
				function(){
					$("div#tabs").tabs( "select" , 2 );
					setTimeout(function(){ $("#sl_loaiKnTcCts").focus(); }, 300);
				}
		);
		return false;
	}
	
	if ( $.trim($("#ghiChu").val()) == "")
	{
		alert_error_callback(
				"Vui lòng nhập nội dung đơn !!!",
				function(){
					$("div#tabs").tabs( "select" , 2 );
					setTimeout(function(){ $("#ghiChu").focus(); }, 300);
				}
		);
		return false;
	}
	
	if ( $("#sl_thamQuyen").val() == "" || $("#sl_thamQuyen").val() == "-1" )
	{
		alert_error_callback(
				"Vui lòng chọn Nhóm thẩm quyền giải quyết !!!",
				function(){
					$("div#tabs").tabs( "select" , 3 );
					setTimeout(function(){ $("#sl_thamQuyen").focus(); }, 300);
				}
		);
		return false;
	}
	
	if ( $("#sl_nhomThamQuyen").val() == "" || $("#sl_nhomThamQuyen").val() == "-1" )
	{
		alert_error_callback(
				"Vui lòng chọn Nhóm cơ quan giải quyết tiếp !!!",
				function(){
					$("div#tabs").tabs( "select" , 3 );
					setTimeout(function(){ $("#sl_nhomThamQuyen").focus(); }, 300);
				}
		);
		return false;
	}
	
	if ( $.trim($("#txt_coQuanTq").val()) == "")
	{
		alert_error_callback(
				"Vui lòng chọn Tên cơ quan giải quyết tiếp !!!",
				function(){
					$("div#tabs").tabs( "select" , 3 );
					setTimeout(function(){ $("#txt_coQuanTq").focus(); }, 300);
				}
		);
		return false;
	}
	
	return true;
}	

function saveAndCreateNew()
{
	$("#isSaveAndCreateNewMode").val('1');
	submit_edit_form();
	return false;
}

function resetModeEditForm()
{
	$("#isPrintMode").val('0');
	$("#isFileMode").val('0');
	return false;
}

function showSaveAndCreateNewButton()
{
	$("#saveAndCreateNewButton").css("display","");
	return false;
}

function hideSaveAndCreateNewButton()
{
	$("#saveAndCreateNewButton").css("display","none");
	return false;
}

var need_to_reload_when_close_edit_form = false;
var submitted = 0;
function startCallbackEditForm() 
{
	// disable all ufd_select before submit form
	if (ufd_select != "")
	{
		$("#" + ufd_select).ufd("destroy");
	}
	
	// fill enough required fields?
	if ( ! isEnoughFields() )
		return false;
	
	if($("#editForm").validationEngine('validate'))
	{
		if (submitted > 0)
   		{
   			return false;
   		}
		submitted++;
		setTimeout(function(){submitted = 0;},2000);
		doBlockUI();
		return true;
	}
	
	return false;
}
	
function completeCallbackEditForm(response) 
{
	doUnBlockUI();
	
	if ($.type(response)=='string' && response.indexOf("</pre>") != -1)
		response = $(response).text();

	var arr = response.split("+_+");
	if (arr.length < 2 || arr.length > 3)
	{
		resetModeEditForm();
		alert_error("Có lỗi trả về từ server");
		refresh_page();
		return false;
	}

	if (arr[0] == 'success')
	{
		need_to_reload_when_close_edit_form = true;		// reload after close form
		
		if (arr[1] == 'print')
		{
			$("#txt_id").val(arr[2]);
			resetModeEditForm();
			display_print_dialog();
			return false;
		}
		
		if (arr[1] == 'file')
		{
			$("#txt_id").val(arr[2]);
			resetModeEditForm();
			showAddFilescanForm();
			return false;
		}
		
		// save & create new button clicked
		if ($("#isSaveAndCreateNewMode").val() == '1')
		{
			var is_edit_success = $("#isEditMode").val() == "1";
//			need_to_reload_when_close_edit_form = true;
			reset_edit_form();
			
			// neu vua them moi thanh cong thi them moi sodk
			if ( ! is_edit_success )
				old_soDk = old_soDk + 1;
				
			$("#id_sodkSo").val(old_soDk);
			alert_info_callback(
				"Đã lưu lại thành công !!!",
				function(){
					$("#id_sodkSo").focus();
				}
			);
		}
		else
		{
			doBlockUI();
			refresh_page();
		}
	}
	else
	{
		alert_error(arr[2]);
	}
	resetModeEditForm();
	return false;		
} 

function submit_edit_form()
{
	$("#editForm").submit();
}

// fetch details of selected item to edit box
function show_add_light_box() {
	doBlockUI();
	
//	document.editForm.btnKT.disabled=false;
//	document.editForm.btnKT.className="save";
	enableBtnKTs();
//	showSaveAndCreateNewButton();
	
	var url = 'fethCurrentStt';
	$.ajax({
		type : "POST",
		url : url,
		dataType : "json",
		success : fetchSuccess,
		error: function(){
			doUnBlockUI();
			alert_error("Có lỗi server !!!");
		}
	});
	return false;
}

function fetchCurrentSodk()
{
	var url = 'fethCurrentStt';
	$.ajax({
		type : "POST",
		url : url,
		dataType : "json",
		success : function(json){
			var stt = json;
//			old_soDk = parseInt(stt.soDk) + 1;
			old_soDk = parseInt(stt.soDk);
		}
	});
}

var old_soDk = 0;
function fetchSuccess(json) {
	doUnBlockUI();
	
	// error
	if (json.status)
	{
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
	clearRefreshTimeout();
	
	// bind shortcut keys
	bindKeyShortcutEditForm();
	
	$('#light').show();
	$('#fade').show();
	$('#light').css({
		'width' : '95.5%',
		'top' : '1%',
		'left' : '0'
	});
	var fullHeight = document.body.offsetHeight;
	$('#fade').css("height", fullHeight + "px");
	//Fix height for editForm
	
	var new_height = $("#editForm")[0].offsetHeight;
	new_height = fullHeight;
	new_height = 620;
	$("#light").css('height', new_height +"px");
	scroll(0,0);
	//Fill data for SODK_SO (Default)
	// focus on open
	if ($("#id_sodkSo").is(":enabled"))
		$('#id_sodkSo').focus();
	else
		$('#txt_ngayQuaHan').focus();
	return false;
}

// region: search form

//// search form needed functions
$(document).ready(function(){
	//Ngày chuyen don
	$( "#txt_ngayChuyenDonSearch" ).datepicker({
		yearRange: '2000:2015',
		changeMonth: true,
		changeYear: true
	});
	$( "#txt_ngayChuyenDonSearch" ).datepicker( "option", $.datepicker.regional[ "vi" ] );
	$( "#txt_ngayChuyenDonSearch" ).datepicker( "option", "dateFormat", "dd/mm/yy");

	// hide nguon don class
	$("#searchForm .nguonDon").css("display", "none");

});

function sl_nguonDonDenSearch_onChange(value)
{
	$("#sl_nguonDonChucVuSearch").html("");
	$("#txt_SoCongVanSearch").val("");
	$("#sl_nguonDonCQSearch").html("");
	$("#txt_ngayChuyenDonSearch").val("");
	$("#searchForm .nguonDon").css("display","none");

	if (value == -1) {
		return false;
	}

	var i;
	var nguon_don;
	for (i = 0; i < nguon_don_list.length; i++) {
		nguon_don = nguon_don_list[i];
		if (nguon_don.value == value) {
			break;
		}
	}

	var select_html, select_data;
	// p1 visible
	if (nguon_don.p1.visible) {
		$("#p1_label_search").css("display", "");
		$("#p1_content_search").css("display", "");
		$("#p1_label_search").html(nguon_don.p1.title + ":");
		// get list data from prefetched data
		select_html = "<option value='-1'>Chọn " + nguon_don.p1.title.toLowerCase() + "</option>";
		select_data = nguon_don_selects_data["list_" + nguon_don.value + "_p1"];
		for (i=0; i < select_data.length; i++) {
			select_html += "<option value='" + select_data[i].id +"'>" + select_data[i].name + "</option>";
		}
		$("#sl_nguonDonChucVuSearch").html(select_html);
		$("#sl_nguonDonChucVuSearch").trigger("change");
	}
	else {
		$("#p1_1_label_search").css("display", "");
		$("#p1_1_content_search").css("display", "");
	}

	// p2 visible
	if (nguon_don.p2.visible) {
		$("#p2_label_search").css("display", "");
		$("#p2_content_search").css("display", "");
		$("#p2_label_search").html(nguon_don.p2.title + ":");
	}

	// p3 visible
	if (nguon_don.p3.visible) {
		$("#p3_label_search").css("display", "");
		$("#p3_content_search").css("display", "");
		$("#p3_label_search").html(nguon_don.p3.title + ":");
		if ( ! nguon_don.p1.visible) {
			// get list data from prefetched data
			select_html = "<option value='-1'>Chọn " + nguon_don.p3.title.toLowerCase() + "</option>";
			select_data = nguon_don_selects_data["list_" + nguon_don.value + "_p3"];
			for (i=0; i < select_data.length; i++) {
				select_html += "<option value='" + select_data[i].id +"'>" + select_data[i].name + "</option>";
			}
			$("#sl_nguonDonCQSearch").html(select_html);
		}
		$("#sl_nguonDonCQSearch").trigger("change");
	}
	else {
		$("#p3_1_label_search").css("display", "");
		$("#p3_1_content_search").css("display", "");
	}

	// p4 visible
	if (nguon_don.p4.visible) {
		$("#p4_label_search").css("display", "");
		$("#p4_content_search").css("display", "");
		$("#p4_label_search").html(nguon_don.p4.title + ":");
	}

	setTimeout(function(){
		var searchFormHeight = $("#searchForm")[0].offsetHeight + 50;
		$('#light_search').css("height", searchFormHeight + "px");
	},300);

	return false;
}

function sl_daiDienSearch_onChange(id)
{
	$("#txt_soNguoiFromSearch").val("");
	$("#txt_soNguoiToSearch").val("");
	$("#txt_tenCQTCSearch").val("");
	$(".daidien").hide();
	$(".daidien_" + id).show();

	var searchFormHeight = $("#searchForm")[0].offsetHeight + 30;
	$("#light_search").css("height", searchFormHeight + "px" );
	return false;
}

function sl_doiTuongKnTcSearch_onChange(id)
{
	$("#txt_coQuanSearch").val("");
	$("#txt_hoTenDoiTuongSearch").val("");
	$(".doiTuongKnTc").hide();
	$(".doiTuongKnTc_" + id).show();
}

function sl_doiTuongKnTcSearch_onChange_quick(id)
{
	$("#txt_coQuanSearch_quick").val("");
	$("#txt_hoTenDoiTuongSearch_quick").val("");
	$(".doiTuongKnTc_quick").hide();
	$(".doiTuongKnTc_quick_" + id).show();
}
// needed functions

// action if btn search clicked
function show_search_form(){
	fetch_search_detail();
	return false;
}

function do_show_search_form()
{
	$('#fade').show();
	$('#light_search').show();
//	$('#light_search').css({
//		'width' : '96%',
//		'min-width' : '943px',
//		'top' : '1%',
//		'left' : '0'
//	});
	
	var searchFormHeight = $("#searchForm")[0].offsetHeight + 40;
	$('#light_search').css("height", searchFormHeight + "px");
	
	var fullHeight = document.body.offsetHeight;
	$('#fade').css("height", fullHeight + "px");
	
	searchFormHeight = $("#searchForm")[0].offsetHeight;
	$('#light_search').css("height", searchFormHeight + "px");
	
	$('#txt_from_date').focus();
	$("#ui-datepicker-div").addClass("datePickerCustomColor");
	$('#searchForm > #table td').css("border", "0");
	
	// bind short cut
	bindKeyShortcutSearchForm();
}

function fetch_search_detail()
{
	doBlockUI();
	$.ajax({
		type : "POST",
		url : "fetchSearchDetailSodk",
		dataType : "json",
		success : function(json){
			doUnBlockUI();
			var sd = json;
			if (sd)
			{
				if (sd.txt_from_date)
					$("#txt_from_date").val(sd.txt_from_date);
				if (sd.txt_to_date)
					$("#txt_to_date").val(sd.txt_to_date);
				if (sd.sl_nguoiNhapSearch)
					$("#sl_nguoiNhapSearch").val(sd.sl_nguoiNhapSearch);
				
				if (sd.sl_nguonDonDenSearch)
				{
					$("#sl_nguonDonDenSearch").val(sd.sl_nguonDonDenSearch);
					$("#sl_nguonDonDenSearch").trigger("change");
					setTimeout(
							function(){
								if (sd.txt_SoCongVanSearch)
									$("#txt_SoCongVanSearch").val(sd.txt_SoCongVanSearch);
								if (sd.txt_ngayChuyenDonSearch)
									$("#txt_ngayChuyenDonSearch").val(sd.txt_ngayChuyenDonSearch);

								if (sd.sl_nguonDonChucVuSearch) {
									if (sd.sl_nguonDonCQSearch)
										$("#fetch_sl_nguonDonCQSearch").val(sd.sl_nguonDonCQSearch);
									$("#sl_nguonDonChucVuSearch").val(sd.sl_nguonDonChucVuSearch);
									$("#sl_nguonDonChucVuSearch").trigger("change");
								}
								else {
									if (sd.sl_nguonDonCQSearch)
										$("#sl_nguonDonCQSearch").val(sd.sl_nguonDonCQSearch);
								}
							}, 400
					);
				}
				
				if (sd.sl_tinhTrangSearch)
					$("#sl_tinhTrangSearch").val(sd.sl_tinhTrangSearch);
				
				if (sd.txt_hoTenSearch)
					$("#txt_hoTenSearch").val(sd.txt_hoTenSearch);
				if (sd.nameSearchType)
					setCheckedValue(document.forms['searchForm'].elements['nameSearchType'], sd.nameSearchType);
				
				if (sd.sl_daiDienSearch)
				{
					$("#sl_daiDienSearch").val(sd.sl_daiDienSearch);
					$("#sl_daiDienSearch").trigger("change");
					setTimeout(
							function(){
								if (sd.txt_soNguoiFromSearch)
									$("#txt_soNguoiFromSearch").val(sd.txt_soNguoiFromSearch);
								if (sd.txt_soNguoiToSearch)
									$("#txt_soNguoiToSearch").val(sd.txt_soNguoiToSearch);								
								if (sd.txt_tenCQTCSearch)
									$("#txt_tenCQTCSearch").val(sd.txt_tenCQTCSearch);
							}, 500 
					);
				}
				
				if (sd.txt_soLanSearch)
					$("#txt_soLanSearch").val(sd.txt_soLanSearch);
				
				if (sd.sl_loaiDonSearch)
				{
					if (sd.sl_loaiKnTcsSearch)
					{
						$("#fetch_sl_loaiKnTcsSearch").val(sd.sl_loaiKnTcsSearch);
						if (sd.sl_loaiKnTcCtsSearch)
							$("#fetch_sl_loaiKnTcCtsSearch").val(sd.sl_loaiKnTcCtsSearch);
					}
					$("#sl_loaiDonSearch").val(sd.sl_loaiDonSearch);
					$("#sl_loaiDonSearch").trigger("change");
				}
				
				if (sd.tinhThanhPhosIdSearch)
				{
					if (sd.sl_quanHuyensSearch)
					{
						$("#fetch_sl_quanHuyensSearch").val(sd.sl_quanHuyensSearch);
						if (sd.sl_xaPhuongsSearch)
							$("#fetch_sl_xaPhuongsSearch").val(sd.sl_xaPhuongsSearch);
					}
					$("#tinhThanhPhosIdSearch").val(sd.tinhThanhPhosIdSearch);
					$("#tinhThanhPhosIdSearch").trigger("change");
				}
				if (sd.txt_chiTietDiaDanhSearch)
					$("#txt_chiTietDiaDanhSearch").val(sd.txt_chiTietDiaDanhSearch);
				
				if (sd.sl_doiTuongKnTcSearch)
				{
					$("#sl_doiTuongKnTcSearch").val(sd.sl_doiTuongKnTcSearch);
					$("#sl_doiTuongKnTcSearch").trigger("change");
					setTimeout(
							function(){
								if (sd.txt_coQuanSearch)
									$("#txt_coQuanSearch").val(sd.txt_coQuanSearch);
								if (sd.txt_hoTenDoiTuongSearch)
									$("#txt_hoTenDoiTuongSearch").val(sd.txt_hoTenDoiTuongSearch);
							}, 500 
					);
				}
				
				if (sd.txt_coQuanDaGiaiQuyetSearch)
					$("#txt_coQuanDaGiaiQuyetSearch").val(sd.txt_coQuanDaGiaiQuyetSearch);
				if (sd.txt_coQuanGiaiQuyetTiepSearch)
					$("#txt_coQuanGiaiQuyetTiepSearch").val(sd.txt_coQuanGiaiQuyetTiepSearch);
				if (sd.sl_thamQuyenSearch)
				{
					if (sd.sl_nhomThamQuyenSearch)
						$("#fetch_sl_nhomThamQuyenSearch").val(sd.sl_nhomThamQuyenSearch);
					$("#sl_thamQuyenSearch").val(sd.sl_thamQuyenSearch);
					$("#sl_thamQuyenSearch").trigger("change");
				}
				if (sd.ta_noiDungKnTcSearch)
					$("#ta_noiDungKnTcSearch").val(sd.ta_noiDungKnTcSearch);
				if (sd.sl_huongGiaiQuyetSearch)
					$("#sl_huongGiaiQuyetSearch").val(sd.sl_huongGiaiQuyetSearch);
				if (sd.sl_nguoiXLSearch)
					$("#sl_nguoiXLSearch").val(sd.sl_nguoiXLSearch);
				if (sd.phucDapType)
					setCheckedValue(document.forms['searchForm'].elements['phucDapType'], sd.phucDapType);
				
				if (sd.ta_theoDoiVanBanDenSearch)
					$("#ta_theoDoiVanBanDenSearch").val(sd.ta_theoDoiVanBanDenSearch);
				if (sd.txt_from_date_vb)
					$("#txt_from_date_vb").val(sd.txt_from_date_vb);
				if (sd.txt_to_date_vb)
					$("#txt_to_date_vb").val(sd.txt_to_date_vb);
				if (sd.ta_theoDoiVanBanDiSearch)
					$("#ta_theoDoiVanBanDiSearch").val(sd.ta_theoDoiVanBanDiSearch);
				
				do_show_search_form();
			}
		},
		error: function(){
			alert_error("Có lỗi server khi lấy dữ liệu search!!!");
			refresh_page();
		}
	});
	return false;
}

function hide_search_form()
{
	$("#txt_hoTenSearch").focus();	
	setTimeout(function(){
		unbindKeyShortcutSearchForm();
		$("#ui-datepicker-div").hide();		
		$("#ui-datepicker-div").removeClass("datePickerCustomColor");
		reset_search_form_2();
		$("#searchForm").validationEngine('hideAll');
		$('#light_search').hide();
		$('#fade').hide();
		
		// hide all datepicker div if still displayed
		if ( $("#ui-datepicker-div").is(":visible"))
			$.datepicker._hideDatepicker();
			
	},300);	
	return false;
}

function submit_search_form()
{
	$('#searchForm').submit();
}

function reset_search_form_2()
{
	reset_search_form();
	search_form_date_default();
	return false;
}

function reset_search_form()
{
	$("#searchForm").validationEngine('hideAll');

	$("#txt_from_date").val("");
	$("#txt_to_date").val("");
	$("#sl_nguoiNhapSearch").val("-1");
	$("#sl_coDonSearch").val("-1");

	$("#sl_nguonDonDenSearch").val("-1");
	$("#sl_nguonDonDenSearch").trigger("change");

	$("#sl_tinhTrangSearch").val("-1");

	$("#txt_hoTenSearch").val("");
	setCheckedValue(document.forms['searchForm'].elements['nameSearchType'], '1');

	$("#sl_daiDienSearch").val("-1");
	$("#sl_daiDienSearch").trigger("change");
	$("#txt_soLanSearch").val("");

	$("#sl_loaiDonSearch").val("-1");
//	$("#sl_loaiDonSearch").trigger("change");
	$("#sl_loaiKnTcsSearch").html("<option value='-1'>Chọn loại KN, TC</option>");
	$("#sl_loaiKnTcsSearch").val("-1");
	$("#sl_loaiKnTcCtsSearch").html("<option value='-1'>Chọn chi tiết loại KN, TC</option>");
	$("#sl_loaiKnTcCtsSearch").val("-1");
	$("#sl_loaiKnTcCtsSearch").trigger("change");
//	setTimeout(function(){
//		$("#sl_loaiKnTcsSearch").html("<option value='-1'>Chọn loại đơn trước</option>");
//		$("#sl_loaiKnTcsSearch").val("-1");
//		$("#sl_loaiKnTcsSearch").trigger("change");
//		setTimeout(function(){
//			$("#sl_loaiKnTcCtsSearch").html("<option value='-1'>Chọn loại KN, TC trước</option>");
//			$("#sl_loaiKnTcCtsSearch").val("-1");
//		}, 700);
//	}, 700);

	$("#tinhThanhPhosIdSearch").val("-1");
//	$("#tinhThanhPhosIdSearch").trigger("change");
	$("#sl_quanHuyensSearch").html("<option value='-1'>Chọn Quận, huyện</option>");
	$("#sl_quanHuyensSearch").val("-1");
	$("#sl_xaPhuongsSearch").html("<option value='-1'>Chọn Phường, xã</option>");
	$("#sl_xaPhuongsSearch").val("-1");
	$("#sl_xaPhuongsSearch").trigger("change");
//	setTimeout(function(){
//		$("#sl_quanHuyensSearch").html("<option value='-1'>Chọn tỉnh thành phố trước</option>");
//		$("#sl_quanHuyensSearch").val("-1");
//		$("#sl_quanHuyensSearch").trigger("change");
//		setTimeout(function(){
//			$("#sl_xaPhuongsSearch").html("<option value='-1'>Chọn quận huyện trước</option>");
//			$("#sl_xaPhuongsSearch").val("-1");
//		}, 700);
//	}, 700);
	$("#txt_chiTietDiaDanhSearch").val("");

	$("#sl_doiTuongKnTcSearch").val("-1");
	$("#sl_doiTuongKnTcSearch").trigger("change");

	$("#ta_noiDungKnTcSearch").val("");
	$("#sl_thamQuyenSearch").val("-1");
//	$("#sl_thamQuyenSearch").trigger("change");
	$("#sl_nhomThamQuyenSearch").html("<option value='-1'>Chọn nhóm thẩm quyền giải quyết tiếp</option>");
	$("#sl_nhomThamQuyenSearch").val("-1");
//	setTimeout(function(){
//		$("#sl_nhomThamQuyenSearch").html("<option value='-1'>Chọn nhóm thẩm quyền giải quyết trước</option>");
//		$("#sl_nhomThamQuyenSearch").val("-1");
//	}, 700);

	$("#txt_coQuanDaGiaiQuyetSearch").val("");
	$("#txt_coQuanGiaiQuyetTiepSearch").val("");
	$("#sl_huongGiaiQuyetSearch").val("-1");
	$("#sl_nguoiXLSearch").val("-1");
	setCheckedValue(document.forms['searchForm'].elements['phucDapType'], '-1');
	$("#ta_theoDoiVanBanDenSearch").val("");
	$("#txt_from_date_vb").val("");
	$("#txt_to_date_vb").val("");
	$("#ta_theoDoiVanBanDiSearch").val("");
}

function search_form_date_default()
{
	// to date => today
//	var todayS = formatDate(new Date(), "dd/MM/yyyy");
//	$("#txt_to_date").val(todayS);

	$("#txt_to_date").val("");

	// chi lay 15 ngay gan nhat
//	var tuNgay = new Date();
//	var days = -15;
//	var res = tuNgay.setTime(tuNgay.getTime() + (days * 24 * 60 * 60 * 1000));
//	tuNgay = new Date(res);
//	var tuNgayS = formatDate(tuNgay, "dd/MM/yyyy");
//	$("#txt_from_date").val(tuNgayS);
	$("#txt_from_date").val("");
}


// bind shortcut key search form
function bindKeyShortcutSearchForm()
{
	$(document).bind('keydown', 'Alt+1', fncThucHienSearch);
	$(document).bind('keydown', 'Alt+2', fncNhapLaiSearch);
	$(document).bind('keydown', 'Alt+3', fncHuyBoSearch);

	var searchFormInputElements = $("#searchForm").find('button,input[type="text"],textarea,select,input:radio,input:checkbox');
	searchFormInputElements.bind('keydown', 'Alt+1', fncThucHienSearch);
	searchFormInputElements.bind('keydown', 'Alt+2', fncNhapLaiSearch);
	searchFormInputElements.bind('keydown', 'Alt+3', fncHuyBoSearch);
	return false;
}

function bindKeyShortcutSearchFormForObject(jquery_object)
{
	jquery_object.bind('keydown', 'Alt+1', fncThucHienSearch);
	jquery_object.bind('keydown', 'Alt+2', fncNhapLaiSearch);
	jquery_object.bind('keydown', 'Alt+3', fncHuyBoSearch);
	return false;
}

function unbindKeyShortcutSearchForm()
{
	$(document).unbind('keydown', 'Alt+1', fncThucHienSearch);
	$(document).unbind('keydown', 'Alt+2', fncNhapLaiSearch);
	$(document).unbind('keydown', 'Alt+3', fncHuyBoSearch);

	var searchFormInputElements = $("#searchForm").find('button,input[type="text"],textarea,select,input:radio,input:checkbox');
	searchFormInputElements.unbind('keydown', 'Alt+1', fncThucHienSearch);
	searchFormInputElements.unbind('keydown', 'Alt+2', fncNhapLaiSearch);
	searchFormInputElements.unbind('keydown', 'Alt+3', fncHuyBoSearch);
	return false;
}

function fncThucHienSearch(evt)
{
	if (checkShortcutHit(2))
		$("#thucHienSearchBtn").trigger("click");
}
function fncNhapLaiSearch(evt)
{
	if (checkShortcutHit(2))
	{
		$("#nhapLaiSearchBtn").trigger("click");
		$("#txt_from_date").focus();
	}
}
function fncHuyBoSearch(evt)
{
	if (checkShortcutHit(2))
		$("#huyBoSearchBtn").trigger("click");
}

// Quan Huyen Select
function fetchQuanHuyehSuccessSearch(json) {
	$('#sl_quanHuyensSearch').find('option').remove().end();
	$('#sl_quanHuyensSearch').append(
		$('<option></option>').val('-1').html('Chọn Quận, huyện'));
	$.each(json, function() {
		$('#sl_quanHuyensSearch').append(
			$('<option></option>').val(this.id).html(this.name));

	});

	var temp1 = $("#fetch_sl_quanHuyensSearch").val();
	if (temp1 != "")
	{
		$('#sl_quanHuyensSearch').val(temp1);
		$("#fetch_sl_quanHuyensSearch").val("");
		$("#sl_quanHuyensSearch").trigger("change");
		return false;
	}

	$("#sl_quanHuyensSearch").val("-1");
	$("#sl_xaPhuongsSearch").html("<option value='-1'>Chọn Phường, xã</option>");
	$("#sl_xaPhuongsSearch").val("-1");
	$("#sl_xaPhuongsSearch").trigger("change");

//	sl_diaDanh_onChange('sl_quanHuyensSearch',$('#sl_quanHuyensSearch option:selected').val());
	return false;
}

// Phuong xa select
function fetchPhuongXaSuccessSearch(json) {
	$('#sl_xaPhuongsSearch').find('option').remove().end();
	$('#sl_xaPhuongsSearch').append(
		$('<option></option>').val('-1').html('Chọn Phường, xã'));
	$.each(json, function() {
		$('#sl_xaPhuongsSearch').append(
			$('<option></option>').val(this.id).html(this.name));

	});

	var temp1 = $("#fetch_sl_xaPhuongsSearch").val();
	if (temp1 != "")
	{
		$('#sl_xaPhuongsSearch').val(temp1);
		$("#fetch_sl_xaPhuongsSearch").val("");
	}

	return false;
}

// bind tab keys to the form to simulate tabindex for searchForm
function simulateTabIndexSearchform()
{
	doStimulateTabAction("ta_noiDungKnTcSearch","txt_coQuanDaGiaiQuyetSearch");
	doStimulateTabAction("txt_coQuanDaGiaiQuyetSearch","sl_thamQuyenSearch");
	doStimulateTabAction("txt_from_date_vb","txt_to_date_vb");
	doStimulateTabAction("txt_to_date_vb","ta_theoDoiVanBanDiSearch");
	doStimulateTabActionO($("#ta_theoDoiVanBanDiSearch"),$("#searchForm button").first());
	doStimulateTabActionO($("#searchForm button").last(),$("#txt_from_date"));
	return false;
}

//bind shift+tab keys to the form to simulate shift+tab for searchForm
function simulateShiftTabIndexSearchform()
{
	doStimulateShiftTabAction("ta_noiDungKnTcSearch","txt_coQuanDaGiaiQuyetSearch");
	doStimulateShiftTabAction("sl_nhomThamQuyenSearch","txt_coQuanGiaiQuyetTiepSearch");
	doStimulateShiftTabAction("ta_theoDoiVanBanDenSearch","txt_from_date_vb");
	doStimulateShiftTabAction("txt_from_date_vb","txt_to_date_vb");
	doStimulateShiftTabAction("txt_to_date_vb","ta_theoDoiVanBanDiSearch");
	doStimulateShiftTabActionO($("#ta_theoDoiVanBanDiSearch"),$("#searchForm button").first());
	doStimulateShiftTabActionO($("#searchForm button").last(),$("#txt_from_date"));
	doStimulateShiftTabActionO($("#searchForm button").last().prev(),$("#searchForm button").last());
	return false;
}

// endregion search form

// region: quick search form
function show_search_form_quick(){
	fetch_search_detail_quick();
	return false;
}

function do_show_search_form_quick()
{
	$('#fade').show();
	$('#light_search_quick').show();

	var searchFormHeight = $("#searchForm_quick")[0].offsetHeight + 40;
	$('#light_search_quick').css("height", searchFormHeight + "px");

	var fullHeight = document.body.offsetHeight;
	$('#fade').css("height", fullHeight + "px");

	searchFormHeight = $("#searchForm_quick")[0].offsetHeight;
	$('#light_search_quick').css("height", searchFormHeight + "px");

	$('#txt_hoTenSearch_quick').focus();
	$('#searchForm_quick > #table td').css("border", "0");

	// bind short cut
	bindKeyShortcutSearchForm_quick();
}

function fetch_search_detail_quick()
{
	doBlockUI();
	$.ajax({
		type : "POST",
		url : "fetchSearchDetailSodk",
		dataType : "json",
		success : function(json){
			doUnBlockUI();
			var sd = json;
			if (sd)
			{
				if (sd.txt_hoTenSearch)
					$("#txt_hoTenSearch_quick").val(sd.txt_hoTenSearch);
				if (sd.nameSearchType)
					setCheckedValue(document.forms['searchForm_quick'].elements['nameSearchType'], sd.nameSearchType);

				if (sd.tinhThanhPhosIdSearch)
				{
					if (sd.sl_quanHuyensSearch)
					{
						$("#fetch_sl_quanHuyensSearch_quick").val(sd.sl_quanHuyensSearch);
						if (sd.sl_xaPhuongsSearch)
							$("#fetch_sl_xaPhuongsSearch_quick").val(sd.sl_xaPhuongsSearch);
					}
					$("#tinhThanhPhosIdSearch_quick").val(sd.tinhThanhPhosIdSearch);
					$("#tinhThanhPhosIdSearch_quick").trigger("change");
				}
				if (sd.txt_chiTietDiaDanhSearch)
					$("#txt_chiTietDiaDanhSearch_quick").val(sd.txt_chiTietDiaDanhSearch);

				if (sd.sl_doiTuongKnTcSearch)
				{
					$("#sl_doiTuongKnTcSearch_quick").val(sd.sl_doiTuongKnTcSearch);
					$("#sl_doiTuongKnTcSearch_quick").trigger("change");
					setTimeout(
						function(){
							if (sd.txt_coQuanSearch)
								$("#txt_coQuanSearch_quick").val(sd.txt_coQuanSearch);
							if (sd.txt_hoTenDoiTuongSearch)
								$("#txt_hoTenDoiTuongSearch_quick").val(sd.txt_hoTenDoiTuongSearch);
						}, 500
					);
				}

				if (sd.ta_noiDungKnTcSearch)
					$("#ta_noiDungKnTcSearch_quick").val(sd.ta_noiDungKnTcSearch);

				do_show_search_form_quick();
			}
		},
		error: function(){
			alert_error("Có lỗi server khi lấy dữ liệu search!!!");
			refresh_page();
		}
	});
	return false;
}

function hide_search_form_quick()
{
	$("#txt_hoTenSearch_quick").focus();
	setTimeout(function(){
		unbindKeyShortcutSearchForm_quick();
		$("#ui-datepicker-div").hide();
		$("#ui-datepicker-div").removeClass("datePickerCustomColor");
		reset_search_form_2_quick();
		$("#searchForm_quick").validationEngine('hideAll');
		$('#light_search_quick').hide();
		$('#fade').hide();

		// hide all datepicker div if still displayed
		if ( $("#ui-datepicker-div").is(":visible"))
			$.datepicker._hideDatepicker();

	},300);
	return false;
}

function submit_search_form_quick()
{
	$('#searchForm_quick').submit();
}

function reset_search_form_2_quick()
{
	reset_search_form_quick();
	return false;
}

function reset_search_form_quick()
{
	$("#searchForm_quick").validationEngine('hideAll');

	$("#txt_hoTenSearch_quick").val("");
	setCheckedValue(document.forms['searchForm_quick'].elements['nameSearchType'], '1');

	$("#tinhThanhPhosIdSearch_quick").val("-1");
	$("#sl_quanHuyensSearch_quick").html("<option value='-1'>Chọn Quận, huyện</option>");
	$("#sl_quanHuyensSearch_quick").val("-1");
	$("#sl_xaPhuongsSearch_quick").html("<option value='-1'>Chọn Phường, xã</option>");
	$("#sl_xaPhuongsSearch_quick").val("-1");
	$("#sl_xaPhuongsSearch_quick").trigger("change");
	$("#txt_chiTietDiaDanhSearch_quick").val("");

	$("#sl_doiTuongKnTcSearch_quick").val("-1");
	$("#sl_doiTuongKnTcSearch_quick").trigger("change");

	$("#ta_noiDungKnTcSearch_quick").val("");
}

function bindKeyShortcutSearchForm_quick()
{
	$(document).bind('keydown', 'Alt+1', fncThucHienSearch_quick);
	$(document).bind('keydown', 'Alt+2', fncNhapLaiSearch_quick);
	$(document).bind('keydown', 'Alt+3', fncHuyBoSearch_quick);

	var searchFormInputElements = $("#searchForm_quick").find('button,input[type="text"],textarea,select,input:radio,input:checkbox');
	searchFormInputElements.bind('keydown', 'Alt+1', fncThucHienSearch_quick);
	searchFormInputElements.bind('keydown', 'Alt+2', fncNhapLaiSearch_quick);
	searchFormInputElements.bind('keydown', 'Alt+3', fncHuyBoSearch_quick);
	return false;
}

function bindKeyShortcutSearchFormForObject_quick(jquery_object)
{
	jquery_object.bind('keydown', 'Alt+1', fncThucHienSearch_quick);
	jquery_object.bind('keydown', 'Alt+2', fncNhapLaiSearch_quick);
	jquery_object.bind('keydown', 'Alt+3', fncHuyBoSearch_quick);
	return false;
}

function unbindKeyShortcutSearchForm_quick()
{
	$(document).unbind('keydown', 'Alt+1', fncThucHienSearch_quick);
	$(document).unbind('keydown', 'Alt+2', fncNhapLaiSearch_quick);
	$(document).unbind('keydown', 'Alt+3', fncHuyBoSearch_quick);

	var searchFormInputElements = $("#searchForm_quick").find('button,input[type="text"],textarea,select,input:radio,input:checkbox');
	searchFormInputElements.unbind('keydown', 'Alt+1', fncThucHienSearch_quick);
	searchFormInputElements.unbind('keydown', 'Alt+2', fncNhapLaiSearch_quick);
	searchFormInputElements.unbind('keydown', 'Alt+3', fncHuyBoSearch_quick);
	return false;
}

function fncThucHienSearch_quick(evt)
{
	if (checkShortcutHit_quick())
		$("#thucHienSearchBtn_quick").trigger("click");
}
function fncNhapLaiSearch_quick(evt)
{
	if (checkShortcutHit_quick())
	{
		$("#nhapLaiSearchBtn_quick").trigger("click");
		$("#txt_hoTenSearch_quick").focus();
	}
}
function fncHuyBoSearch_quick(evt)
{
	if (checkShortcutHit_quick())
		$("#huyBoSearchBtn_quick").trigger("click");
}

//tranh an phim tat nhieu qua, khi hien message box thi khong cho phep an phim tat
var shortcutHit_quick = 0;
function checkShortcutHit_quick()
{
	shortcutHit_quick ++;
	setTimeout(function(){ shortcutHit_quick = 0;}, 1000);
	if (shortcutHit_quick > 1)
		return false;
	if ( ! $("#fade").is(":visible"))
		return false;

	if ( ! $("#light_search_quick").is(":visible") )
		return false;

	if (numberOfOpenedMsgbox > 0)
		return false;

	return true;
}


// Quan Huyen Select
function fetchQuanHuyehSuccessSearch_quick(json) {
	$('#sl_quanHuyensSearch_quick').find('option').remove().end();
	$('#sl_quanHuyensSearch_quick').append(
		$('<option></option>').val('-1').html('Chọn Quận, huyện'));
	$.each(json, function() {
		$('#sl_quanHuyensSearch_quick').append(
			$('<option></option>').val(this.id).html(this.name));

	});

	var temp1 = $("#fetch_sl_quanHuyensSearch_quick").val();
	if (temp1 != "")
	{
		$('#sl_quanHuyensSearch_quick').val(temp1);
		$("#fetch_sl_quanHuyensSearch_quick").val("");
		$("#sl_quanHuyensSearch_quick").trigger("change");
		return false;
	}

	$("#sl_quanHuyensSearch_quick").val("-1");
	$("#sl_xaPhuongsSearch_quick").html("<option value='-1'>Chọn Phường, xã</option>");
	$("#sl_xaPhuongsSearch_quick").val("-1");
	$("#sl_xaPhuongsSearch_quick").trigger("change");

	return false;
}

// Phuong xa select
function fetchPhuongXaSuccessSearch_quick(json) {
	$('#sl_xaPhuongsSearch_quick').find('option').remove().end();
	$('#sl_xaPhuongsSearch_quick').append(
		$('<option></option>').val('-1').html('Chọn Phường, xã'));
	$.each(json, function() {
		$('#sl_xaPhuongsSearch_quick').append(
			$('<option></option>').val(this.id).html(this.name));

	});

	var temp1 = $("#fetch_sl_xaPhuongsSearch_quick").val();
	if (temp1 != "")
	{
		$('#sl_xaPhuongsSearch_quick').val(temp1);
		$("#fetch_sl_xaPhuongsSearch_quick").val("");
	}

	return false;
}


// bind tab keys to the form to simulate tabindex for searchForm_quick
function simulateTabIndexSearchform_quick()
{
	doStimulateTabActionO($("#ta_noiDungKnTcSearch_quick"),$("#searchForm_quick button").first());
	doStimulateTabActionO($("#searchForm_quick button").last(),$("#txt_hoTenSearch_quick"));
	return false;
}

//bind shift+tab keys to the form to simulate shift+tab for searchForm_quick
function simulateShiftTabIndexSearchform_quick()
{
	doStimulateShiftTabActionO($("#ta_noiDungKnTcSearch_quick"),$("#searchForm_quick button").first());
	doStimulateShiftTabActionO($("#searchForm_quick button").last(),$("#txt_hoTenSearch_quick"));
	doStimulateShiftTabActionO($("#searchForm_quick button").last().prev(),$("#searchForm_quick button").last());
	return false;
}

// endregion quick search form

// region: chuyen trang thai search select
function show_dang_xu_ly()
{
	reset_search_form_2();
//	search_form_date_default();
	$("#sl_tinhTrangSearch").val("0");
	$('#searchForm').submit();
}
function show_da_xu_ly()
{
	reset_search_form_2();
//	search_form_date_default();
	$("#sl_tinhTrangSearch").val("1");
	$('#searchForm').submit();
}
function show_tat_ca()
{
	reset_search_form_2();
//	search_form_date_default();
	$('#searchForm').submit();
}
// endregion chuyen trang thai search select


function hide_light_box_edit() {
	confirm_gosol(
		"Bạn chắc chắn muốn hủy bỏ?",
		function(result){
			if (result)
			{
				// neu ma user da nhan in hoac them file roi an huy bo 
				// -> delete don da ghi va reload lai trang
				if ($("#isPrintClickedInNewMode").val() == '1' || $("#isFileInNewMode").val() == '1')
				{
//					alert_info("Đơn thư vừa in đã được lưu vào cơ sở dữ liệu. Đang tải lại trang.......");
//					refresh_page();
					doBlockUI();
					var saved_id = $("#txt_id").val();
					if (saved_id != "")
					{
						window.location.href = "deleteSodk?message=0&id="+escape(saved_id);
						return false;
					}
					else
					{
						refresh_page();
						return false;
					}
				}
				
				// neu ma dong form khi vua them moi mot don thu thi refresh lai trang
				if (need_to_reload_when_close_edit_form)
				{
					doBlockUI();
					refresh_page();
					return false;
				}
				
				// da danh dau trung don -> refresh trang
				if (daDanhDauTrungDon)
				{
					doBlockUI();
					refresh_page();
					return false;
				}
				
				
				unbindKeyShortcutEditForm();
				reset_edit_form();
				$("#editForm").validationEngine('hideAll');
				hide_light_box();
				
				// hide all datepicker div if still displayed
				if ( $("#ui-datepicker-div").is(":visible"))
					$.datepicker._hideDatepicker();
				
				// add auto refresh timeout on close of dialog
				setRefreshTimeout();
			}
			else
			{
				$("#id_sodkSo").focus();
			}
			
			return false;
		}
	);	
	return false;
}

// disable fields that are not edittable by other staff other than the one who input the sodk
function disableForbiddenFields()
{
	$("#editForm input[type='text']").attr("disabled", "disabled");
	$("#editForm input[type='radio']").attr("disabled", "disabled");
	$("#editForm input[type='checkbox']").attr("disabled", "disabled");
	$("#editForm select").attr("disabled", "disabled");
	$("#editForm textarea").attr("disabled", "disabled");
	
	// editable fields
	$("#txt_ngayTheoDoi").removeAttr("disabled");
	$("#txt_phucDap").removeAttr("disabled");
	
	$("#txt_ngayQuaHan").removeAttr("disabled");
	$("#sex").removeAttr("disabled");
	$("#sl_tenNguoiKy").removeAttr("disabled");
	$("#txt_nguoiKy").removeAttr("disabled");
	$("#txt_giaiQuyet").removeAttr("disabled");
	$("#cb_status").removeAttr("disabled");
	
	return false;
}

// enable fields that are not edittable by other staff other than the one who input the sodk
function enableForbiddenFields()
{
	$("#editForm input[type='text']").removeAttr("disabled");
	$("#editForm input[type='radio']").removeAttr("disabled");
	$("#editForm input[type='checkbox']").removeAttr("disabled");
	$("#editForm select").removeAttr("disabled");
	$("#editForm textarea").removeAttr("disabled");
	return false;
}

function reset_edit_form(){
	
	enableForbiddenFields();
	enableBtnKTs();
	$("#userId").val("");
	
	//$("#print_button_editForm").css("display", "none");
	$("#isEditMode").val("0");
	$("#isPrintClickedInNewMode").val('0');
	$("#isPrintMode").val('0');
	$("#isFileMode").val('0');
	$("#isFileInNewMode").val('0');
	$("#isSaveAndCreateNewMode").val('0');
	
	//main
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

	$("#select_nguonDon")[0].selectedIndex = 0;
//	$("#select_nguonDon").val("1");
	$("#select_nguonDon").trigger("change");
	//Cơ quan chuyển đơn
//	$("#sl_nguonDonCQ").val("-1");
	
	
	// tab Người KN, TC
	setCheckedValue(document.forms['editForm'].elements['hs_type'], '1');
	$("#txt_soNguoi").val("1");
	$("#txt_caNhanCq").val("");
	$("#ndHoTen").val("");
	
	$("#txt_id").val("");
	$("#txt_table").val(table_id);
	$("#txt_nguoiNhap").val("");
	$("#txt_userID").val("");
	$("#txt_diaChi").val("");
	$("#txt_diaChi1").val("");
	
	$("#sex")[0].selectedIndex = 0;
	$("select#ndDanToc option")
	   .each(function() { this.selected = (this.text == 'Kinh'); });
	$("#txt_ndDanToc").val("");
	$("#ndDanToc").trigger("change");
	
	$("#thanhPhan").val("-1");
	$("#txt_thanhPhan").val("");
	
	$("select#quoctich option")
	   .each(function() { this.selected = (this.text == 'Việt Nam'); });
	$("#txt_quocTich").val("");
	$("#quoctich").trigger("change");
	
	$("#tinhThanhPhosId").val("-1");
	$("#txt_tinhThanh").val("");
	$("#sl_quanHuyens").html("<option value='-1'>Chọn Quận, huyện</option>");
	$("#sl_quanHuyens").val("-1");
	$("#txt_quanHuyen").val("");
	$("#sl_xaPhuongs").html("<option value='-1'>Chọn Phường, xã</option>");
	$("#sl_xaPhuongs").val("-1");
	$("#txt_xaPhuong").val("");
	$("#sl_xaPhuongs").trigger("change");

	// set to default values based on mabophan
	if (default_nguoiKNTC_tinh != "") {
		$("#tinhThanhPhosId option").filter(function() {
			return ($(this).text().toLowerCase().indexOf(default_nguoiKNTC_tinh) != -1);
		}).attr('selected', true);
		$("#tinhThanhPhosId").trigger("change");
		// set default huyen va xa
		setTimeout(function(){
			if (default_nguoiKNTC_huyen != "") {
				$("#sl_quanHuyens option").filter(function() {
					return ($(this).text().toLowerCase().indexOf(default_nguoiKNTC_huyen) != -1);
				}).attr('selected', true);
				$("#sl_quanHuyens").trigger("change");
				// set default xa
				setTimeout(function(){
					if (default_nguoiKNTC_xa != "") {
						$("#sl_xaPhuongs option").filter(function() {
							return ($(this).text().toLowerCase().indexOf(default_nguoiKNTC_xa) != -1);
						}).attr('selected', true);
						$("#sl_xaPhuongs").trigger("change");
					}
				},700);
			}
		},700);
	}


//	$("#tinhThanhPhosId").trigger("change");

	
//	setTimeout(
//		function(){
//			$("#sl_quanHuyens").html("<option value='-1'>Chọn quận, huyện</option>");
//			$("#sl_quanHuyens").val("-1");
//			$("#txt_quanHuyen").val("");
//			$("#sl_quanHuyens").trigger("change");
//			setTimeout(
//				function(){
//					$("#sl_xaPhuongs").html("<option value='-1'>Chọn xã, phường</option>");
//					$("#sl_xaPhuongs").val("-1");
//					$("#txt_xaPhuong").val("");
//				}, 
//				700
//			);
//		}, 
//		700
//	);
	
	$("#chiTietDiaChi").val("");
	$("#txt_nguoiNhap").val("");
	$("#txt_userID").val("");
	
	// tab doi tuong KN, TC
	setCheckedValue(document.forms['editForm'].elements['hsdt_type'], '1');
	$("#co_quan_btc").val("");
	$("#ndHoTen1").val("");
	
	$("select#ndDanToc1 option")
	   .each(function() { this.selected = (this.text == 'Kinh'); });
	$("#txt_ndDanToc1").val("");
	$("#ndDanToc1").trigger("change");
	
	$("#thanhPhan1").val("-1");
	$("#txt_thanhPhan1").val("");
	$("#chucVu1").val("-1");
	$("#txt_chucVu").val("");
	$("#cap").val("-1");
	$("#txt_cap").val("");
	$("select#quoctich1 option")
	   .each(function() { this.selected = (this.text == 'Việt Nam'); });
	$("#txt_quocTich1").val("");
	$("#quoctich1").trigger("change");
	
	$("#sl_tinhThanhPhosId_2").val("-1");
	$("#txt_tinhThanh1").val("");
	$("#sl_quanHuyens2").html("<option value='-1'>Chọn Quận, huyện</option>");
	$("#sl_quanHuyens2").val("-1");
	$("#txt_quanHuyen1").val("");
	$("#sl_xaPhuongs2").html("<option value='-1'>Chọn Phường, xã</option>");
	$("#sl_xaPhuongs2").val("-1");
	$("#txt_xaPhuong1").val("");
	$("#sl_xaPhuongs2").trigger("change");
	$("#coQuanCt1").val("");
	$("#chiTietDiaChi1").val("");

	// set to default values based on mabophan
	if (default_doituongKNTC_tinh != "") {
		$("#sl_tinhThanhPhosId_2 option").filter(function() {
			return ($(this).text().toLowerCase().indexOf(default_doituongKNTC_tinh) != -1);
		}).attr('selected', true);
		$("#sl_tinhThanhPhosId_2").trigger("change");
		// set default huyen va xa
		setTimeout(function(){
			if (default_doituongKNTC_huyen != "") {
				$("#sl_quanHuyens2 option").filter(function() {
					return ($(this).text().toLowerCase().indexOf(default_doituongKNTC_huyen) != -1);
				}).attr('selected', true);
				$("#sl_quanHuyens2").trigger("change");
				// set default xa
				setTimeout(function(){
					if (default_doituongKNTC_xa != "") {
						$("#sl_xaPhuongs2 option").filter(function() {
							return ($(this).text().toLowerCase().indexOf(default_doituongKNTC_xa) != -1);
						}).attr('selected', true);
						$("#sl_xaPhuongs2").trigger("change");
					}
				},700);
			}
		},700);
	}

	// tab don khieu nai to cao
	$("#sl_loaiDon").val("-1");
	$("#txt_loaiDon").val("");
	
	$("#sl_loaiKnTcs").html("<option value='-1'>Chọn loại KN, TC</option>");
	$("#sl_loaiKnTcs").val("-1");
	$("#txt_loaiKnTcs").val("");
	$("#sl_loaiKnTcCts").html("<option value='-1'>Chọn chi tiết loại KN, TC</option>");
	$("#sl_loaiKnTcCts").val("-1");
	$("#txt_loaiKnTcCts").val("");
	$("#sl_loaiKnTcCts").trigger("change");
	$("#ghiChu").val("");
	$("#coquangq").val("");
	
	
	// tab huong giai quyet
	$("#huongGQ")[0].selectedIndex = 0;
	
	// mac dinh nguoi xu ly la nguoi dang nhap
	if (currentUserNhanVienId > 0)
		$("#sl_tenNguoiXL").val(currentUserNhanVienId);
	else
		$("#sl_tenNguoiXL")[0].selectedIndex = 0;
	$("#txt_nguoiXL").val($("#sl_tenNguoiXL option:selected").text());
	//$("#sl_tenNguoiXL")[0].selectedIndex = 0;
	//$("#txt_nguoiXL").val("");
	
	$("#sl_tenNguoiKy")[0].selectedIndex = 0;
	$("#txt_nguoiKy").val("");
	$("#sl_thamQuyen")[0].selectedIndex = 0;
	$("#txt_thamQuyen").val("");
	$("#sl_nhomThamQuyen")[0].selectedIndex = 0;
	$("#txt_nhomThamQuyen").val("");
	$("#sl_nhomThamQuyen").trigger("change");
	//$("#sl_tenNguoiXL")[0].selectedIndex = 0;
	$("#txt_coQuanTq").val("");
	$("#txt_giaiQuyet").val("");
	
	$("#txt_ngayTheoDoi").val("");
	
	$("#txt_phucDap").val("");
	
	$("#sl_kemTheoVB").val("-1");
	$("#txt_kemTheoVB").val("");
	
	$('input[name=cb_status]').attr('checked', false);
	$("#txt_status").val('0');
	
	// tab file dinh kem
	$("div#tabs-5 > table#table > tbody").html("");
	
	// selected tab = 1
	$("div#tabs").tabs( "select" , 0 );
	
	return false;
}

//return an empty string if none are checked, or
//there are no radio buttons
function getCheckedValue(radioObj) {
	if(!radioObj)
		return "";
	var radioLength = radioObj.length;
	if(radioLength == undefined)
		if(radioObj.checked)
			return radioObj.value;
		else
			return "";
	for(var i = 0; i < radioLength; i++) {
		if(radioObj[i].checked) {
			return radioObj[i].value;
		}
	}
	return "";
}

//set the radio button with the given value as being checked
//do nothing if there are no radio buttons
//if the given value does not exist, all the radio buttons
//are reset to unchecked
function setCheckedValue(radioObj, newValue) {
	if(!radioObj)
		return;
	var radioLength = radioObj.length;
	if(radioLength == undefined) {
		radioObj.checked = (radioObj.value == newValue.toString());
		$(radioObj).trigger("change");
		return;
	}
	for(var i = 0; i < radioLength; i++) {
		radioObj[i].checked = false;
		if(radioObj[i].value == newValue.toString()) {
			radioObj[i].checked = true;
			$(radioObj[i]).trigger("change");
		}
	}
}

function enableBtnKTs()
{
	$("#editForm .btnKT").removeAttr("disabled");
	$("#editForm .btnKT").addClass("save");
	return false;
}

function enableABtnKT(btnKt)
{
	$(btnKt).removeAttr("disabled");
	$(btnKt).addClass("save");
	return false;
}

function disalbeBtnKTs()
{
	$("#editForm .btnKT").attr("disabled","disabled");
	$("#editForm .btnKT").removeClass("save");
	return false;
}

function disalbeABtnKT(btnKT)
{
	$(btnKT).removeClass("save");
	$(btnKT).attr("disabled","disabled");	
	return false;
}


// FOR EDIT
function fetchJSONData() {
	
	if ($("#selected_id").val() == "")
	{
		alert_error("Vui lòng chọn một đơn thư trước !!!");
		return false;
	}

	doBlockUI();
	
	// fetch file dinh kem list
	var id = $("#selected_id").val();
	initFilescanTable(id);
	
	var url = 'editSodk';
	$.ajax({
		type : "POST",
		url : url,
		dataType : "json",
		data : {
			id : id
		},
		success : fetchJSONSuccess,
		error : function() {			
			$.unblockUI();
			refresh_page();
		}
	});
	
	return false;
}

// fetch edit data
function fetchJSONSuccess(json) {	
	doUnBlockUI();
	scroll(0,0);
	
	// disable kt button
	setTimeout(function(){disalbeBtnKTs();},200);
//	hideSaveAndCreateNewButton();
	
	//$("#print_button_editForm").css("display", "block");
	$("#isEditMode").val("1");
	
	// this is the json return data
	var sodk = json;
	
	// neu nguoi dung hien tai khong phai la nguoi nhap don thi disable cac truong khong cho phep chinh sua
	if (sodk.userId)
	{
		$("#userId").val(sodk.userId);
		if (sodk.userId != currentUserNhanVienId)
		{
			disableForbiddenFields();
		}
	}
	
	// fill data
	// ------------------TAB 1 (Người KNTC)-----------------
	// ID của hồ sơ
	$("#txt_id").val(sodk.id);
	// Số thứ tự hồ sơ
	$("#id_sodkSo").val(sodk.so);


	//Nguồn đơn
	$("#select_nguonDon").val(sodk.nguonDon);
	$("#select_nguonDon").trigger("change");

	var i, nguon_don;
	for (i = 0; i < nguon_don_list.length; i++) {
		nguon_don = nguon_don_list[i];
		if (nguon_don.value == parseInt(sodk.nguonDon)) {
			break;
		}
	}

	//Ngày chuyển đơn
	if (sodk.ngayChuyenDon)
		$("#nguonDon_NgayChuyenDon").val(sodk.ngayChuyenDon);
	//Số công văn
	if (sodk.soCV)
		$("#nguonDon_SoCongVan").val(sodk.soCV);

	if (nguon_don.p1.visible) {
		//Chức vụ
		if (sodk.maChucVuCD && sodk.maChucVuCD != "")
		{
			setTimeout(function(){
				if (sodk.maCoQuanCD)
					$("#fetch_sl_nguonDonCQ").val(sodk.maCoQuanCD);
				$("#sl_nguonDonChucVu").val(sodk.maChucVuCD);
				$("#sl_nguonDonChucVu").trigger("change");
			}, 300);
		}
	}

	if (nguon_don.p3.visible) {
		if (!nguon_don.p1.visible) {
			setTimeout(function(){
				if (sodk.maCoQuanCD) {
					$("#sl_nguonDonCQ").val(sodk.maCoQuanCD);
					$("#sl_nguonDonCQ").trigger("change");
				}
			}, 300);
		}
	}


	// Ngày nhập đơn
	$("#txt_ngay").val(sodk.ngay);
	// Ngày quá hạn
	$("#txt_ngayQuaHan").val(sodk.ngayQuaHan);
	// Ngày theo dõi văn bản đến
	$("#txt_ngayTheoDoi").val(sodk.ngayTheoDoi);
	//Hồ sơ cá nhân(Cá nhân, tập thể, cơ quan tổ chức)
	change_type_obj(sodk.caNhan);
	if(sodk.caNhan == '1')
		$('input:radio[name=hs_type]')[0].checked = true;
	else if(sodk.caNhan == '0')
		$('input:radio[name=hs_type]')[1].checked = true;
	else if(sodk.caNhan == '2') 
		$('input:radio[name=hs_type]')[2].checked = true;
	// Họ tên
	$("#ndHoTen").val(sodk.ndHoTen);
	// Giới tính
	$("#sex").val(sodk.ndNamNu);
	// Số người của Tập thể
	$("#txt_soNguoi").val(sodk.soNguoi);
	// Cơ quan
	$("#txt_caNhanCq").val(sodk.caNhanCq);
	// Quốc tịch
	$("#quoctich").val(sodk.quocTichId);
	// Dân tộc
	$("#ndDanToc").val(sodk.maDanToc);
	// Tỉnh/Thành Phố
	if (sodk.maHuyen)
		$("#fetch_sl_quanHuyens").val(sodk.maHuyen);
	if (sodk.maXa)
		$("#fetch_sl_xaPhuongs").val(sodk.maXa);
	if (sodk.maTinh)
	{
		$("#tinhThanhPhosId").val(sodk.maTinh);
		$("#tinhThanhPhosId").trigger("change");
	}
	
	// Địa chỉ
	$("#chiTietDiaChi").val(sodk.chiTietDiaChi);
	// Nghề nghiệp
	$("#thanhPhan").val(sodk.maNgheNghiep);

	// ------------------TAB 2 (Đối tượng bị KNTC)-----------------
//	setCheckedValue(document.forms['editForm'].elements['hsdt_type'], sodk.doiTuong1);
	
	if(sodk.doiTuong1 == '1'){
		$('input:radio[name=hsdt_type]')[0].checked = true;
		$("#dt_cn").trigger("change");
	}	
	else if(sodk.doiTuong1 == '2')
	{
		$('input:radio[name=hsdt_type]')[1].checked = true;
		$("#dt_cq").trigger("change");
	}
	
	// Họ và tên
	$("#ndHoTen1").val(sodk.ndHoTen1);
	// Cơ quan
	$("#co_quan_btc").val(sodk.coQuanBtc);
	// Tỉnh/TP
//	$("#sl_tinhThanhPhosId_2").val(sodk.maTinh1);
	if (sodk.maHuyen1)
		$("#fetch_sl_quanHuyens2").val(sodk.maHuyen1);
	if (sodk.maXa1)
		$("#fetch_sl_xaPhuongs2").val(sodk.maXa1);
	setTimeout(
			function(){
				if (sodk.maTinh1)
				{
					$("#sl_tinhThanhPhosId_2").val(sodk.maTinh1);
					$("#sl_tinhThanhPhosId_2").trigger("change");
				}
			}, 200
	);
	$("#chiTietDiaChi1").val(sodk.chiTietDiaChi1);
	
	// Dân tộc
	$("#ndDanToc1").val(sodk.maDtoc1);
	// Quốc tịch
	$("#quoctich1").val(sodk.quocTichId1);
	// Nghề nghiệp
	$("#thanhPhan1").val(sodk.maNgheNghiep1);
	// Chức vụ
	$("#chucVu1").val(sodk.maChucVuId);
	// Cấp
	$("#cap").val(sodk.maCapId);
	// Nơi công tác
	$("#coQuanCt1").val(sodk.coQuanCt1);
	// ------------------TAB 3-----------------
	// Nội dung đơn
	$("#ghiChu").val(sodk.ghiChu);
	// Cơ quan giải quyết
	$("#coquangq").val(sodk.coquangq);
	// Loại đơn
	if (sodk.loaiKTID)
		$("#fetch_sl_loaiKnTcs").val(sodk.loaiKTID);
	if (sodk.chiTietKTID)
		$("#fetch_sl_loaiKnTcCts").val(sodk.chiTietKTID);
	if (sodk.loaiDonID)
	{
		$("#sl_loaiDon").val(sodk.loaiDonID);
		$("#sl_loaiDon").trigger("change");
	}

	// ------------------TAB 4 (Hướng giải quyết)-----------------

	// Hướng giải quyết
	$("#huongGQ").val(sodk.huongGq);
	// Theo dõi văn bản đi
	$("#txt_giaiQuyet").val(sodk.giaiQuyet);
	// Theo dõi văn bản đến
	$("#txt_phucDap").val(sodk.phucDap);
	// Văn bản kèm theo
	$("#txt_kemTheoVB").val(sodk.kemTheoVB);
	
	if (sodk.kemTheoVB)
		$("#sl_kemTheoVB").find("option:contains(" + sodk.kemTheoVB + ")").attr("selected", "selected");
	// Cơ quan giải quyết tiếp
	$("#txt_coQuanTq").val(sodk.coQuanTq);
	// Tên của người xử lý
	$("#sl_tenNguoiXL").val(sodk.maNguoiXL);
	// Tên của người ký
	$("#sl_tenNguoiKy").val(sodk.maNguoiKy);
	// Thẩm Quyền
	if (sodk.maNhomTQ)
		$("#fetch_sl_nhomThamQuyen").val(sodk.maNhomTQ);
	if (sodk.maThamQuyen)
	{
		$("#sl_thamQuyen").val(sodk.maThamQuyen);
		$("#sl_thamQuyen").trigger("change");
	}
	
	//Trang thái đơn	
	if(sodk.status == 1)
	{
		$("#cb_status").attr('checked','checked');
		$("#txt_status").val('1');
	}
	else
	{
		$('input[name=cb_status]').attr('checked', false);
		$("#txt_status").val('0');
	}
	

	// ------------------TAB 5-----------------
	$("#txt_table").val(sodk.table);

	// =======================Fill data for hiddenfield===================
	//Cá nhân, Tập Thể, Cơ quan tổ chức
	$("#txt_caNhan").val(sodk.caNhan);
	// ID của hồ sơ
	$("#txt_id").val(sodk.id);
	// --------------Người KNTC
	//Cơ quan chuyển đơn  (Từ nguồn cơ quan khác chuyển tới)
	$("#txt_coQuanCD").val(sodk.coQuanCD);
	$("#txt_maCoQuanCD").val(sodk.maCoQuanCD);
	//Chức vụ (Từ nguồn Lãnh đạo TTCP)
	$("#txt_chucVuCD").val(sodk.chucVuCD);
	sl_thamQuyen('sl_nguonDonChucVu', sodk.maChucVuCD);
	// Tên dân tộc
	$("#txt_ndDanToc").val(sodk.ndDanToc);
	// Nghề nghiệp
	$("#txt_thanhPhan").val(sodk.thanhPhan);
	// Quốc tịch
	$("#txt_quocTich").val(sodk.tenQuocTich);
	// Tỉnh Thành
	$("#txt_tinhThanh").val(sodk.tenTinh);
	// Quận Huyện
	$("#txt_quanHuyen").val(sodk.tenHuyen);
	// Xã/Phường
	$("#txt_xaPhuong").val(sodk.tenXa);
	if(sodk.maXa == null){
		if(sodk.maHuyen == null)
			$("#txt_diaChi").val(sodk.maTinh);
		else
			$("#txt_diaChi").val(sodk.maHuyen);
	}else
		$("#txt_diaChi").val(sodk.maXa);
	
	if(sodk.maXa1 == null){
		if(sodk.maHuyen1 == null)
			$("#txt_diaChi1").val(sodk.maTinh1);
		else
			$("#txt_diaChi1").val(sodk.maHuyen1);
	}else
		$("#txt_diaChi1").val(sodk.maXa1);

	// -------------Đối tượng bị KNTC
	$("#txt_doiTuong1").val(sodk.doiTuong1);
	// Tên dân tộc
	$("#txt_ndDanToc1").val(sodk.danToc1);
	// Nghề nghiệp
	$("#txt_thanhPhan1").val(sodk.thanhPhan1);
	// Quốc tịch
	$("#txt_quocTich1").val(sodk.quocTich1);
	// Tỉnh Thành
	$("#txt_tinhThanh1").val(sodk.tinh1);
	// Quận Huyện
	$("#txt_quanHuyen1").val(sodk.tenHuyen1);
//	sl_diaDanh('sl_tinhThanhPhosId_2', sodk.maTinh1);
	// Xã/Phường
	$("#txt_xaPhuong1").val(sodk.tenXa1);
//	sl_diaDanh('sl_quanHuyens2', sodk.maHuyen1);
	// Chức vụ
	$("#txt_chucVu").val(sodk.chucVu1);
	// Cấp
	$("#txt_cap").val(sodk.cap1);

	// ------------ĐƠN KN, TC

	// Loại đơn
	$("#txt_loaiDon").val(sodk.tenLoaiDon);
	// Loại KN,TC
	$("#txt_loaiKnTcs").val(sodk.tenLoaiKT);
	// Chi tiết KN,TC
	$("#txt_loaiKnTcCts").val(sodk.tenChiTietKT);
	// ------------------Hướng giải quyết
	// Người xử lý
	$("#txt_nguoiXL").val(sodk.tenNguoiXL);
	// Người ký
	$("#txt_nguoiKy").val(sodk.tenNguoiKy);
	// Tên Thẩm Quyền
	$("#txt_thamQuyen").val(sodk.tenThamQuyen);
	// Têm Nhóm Thẩm Quyền
	$("#txt_nhomThamQuyen").val(sodk.tenNhomTQ);
	//sl_thamQuyen('sl_thamQuyen', sodk.maThamQuyen);
	//Trạng thái đơn
//	$("#txt_status").val(sodk.status);

	// ======================================================
	// HIỂN THỊ ^^
	show_light_box_edit();
	return false;
}
//CƠ QUAN CHUYỂN ĐƠN
function sl_coQuanCD_onChange(id, val){
	if (id == 'sl_nguonDonCQ') {
		$("#txt_coQuanCD").val($('#sl_nguonDonCQ option:selected').text());
		$("#txt_maCoQuanCD").val(val);
	}
}

// DÂN TỘC
function sl_danToc_onChange(id) {
	if (id == 'ndDanToc') 
	{
		if ($('#ndDanToc').val() == '-1' || $('#ndDanToc').val() == '')
			$("#txt_ndDanToc").val('');
		else
			$("#txt_ndDanToc").val($('#ndDanToc option:selected').text());
	} 
	else if (id == 'ndDanToc1') 
	{
		if ($('#ndDanToc1').val() == '-1' || $('#ndDanToc1').val() == '')
			$("#txt_ndDanToc1").val('');
		else
			$("#txt_ndDanToc1").val($('#ndDanToc1 option:selected').text());
	}
	return false;
}
// NGHỀ NGHIỆP
function sl_thanhPhan_onChange(id) {
	if (id == 'thanhPhan') 
	{
		if ($('#thanhPhan').val() == '-1' || $('#thanhPhan').val() == '')
			$("#txt_thanhPhan").val('');
		else
			$("#txt_thanhPhan").val($('#thanhPhan option:selected').text());
	} 
	else if (id == 'thanhPhan1') 
	{
		if ($('#thanhPhan1').val() == '-1' || $('#thanhPhan1').val() == '')
			$("#txt_thanhPhan1").val('');
		else
			$("#txt_thanhPhan1").val($('#thanhPhan1 option:selected').text());
	}
	return false;
}
// QUỐC TỊCH
function sl_quocTich_onChange(id) {
	if (id == 'quoctich') 
	{
		if ($('#quoctich').val() == '-1' || $('#quoctich').val() == '')
			$("#txt_quocTich").val('');
		else
			$("#txt_quocTich").val($('#quoctich option:selected').text());
	} 
	else if (id == 'quoctich1') 
	{
		if ($('#quoctich1').val() == '-1' || $('#quoctich1').val() == '')
			$("#txt_quocTich1").val('');
		else
			$("#txt_quocTich1").val($('#quoctich1 option:selected').text());
	}
	return false;
}
// CHỨC VỤ
function sl_chucVu_onChange(id) {
	if (id == 'chucVu1') 
	{
		if ($('#chucVu1').val() == '-1' || $('#chucVu1').val() == '')
			$("#txt_chucVu").val('');
		else
			$("#txt_chucVu").val($('#chucVu1 option:selected').text());
	}
	return false;
}
// CẤP
function sl_cap_onChange(id) {
	if (id == 'cap') 
	{
		if ($('#cap').val() == '-1' || $('#cap').val() == '')
			$("#txt_cap").val('');
		else
			$("#txt_cap").val($('#cap option:selected').text());
	}
	return false;
}
// NGƯỜI XỬ LÝ
function sl_nguoiXL_onChange(id) {
	if (id == 'sl_tenNguoiXL') {
		$("#txt_nguoiXL").val($('#sl_tenNguoiXL option:selected').text());
	}
	return false;
}
// NGƯỜI KÝ
function sl_nguoiKy_onChange(id) {
	if (id == 'sl_tenNguoiKy') {
		if ($('#sl_tenNguoiKy').val() != null && $('#sl_tenNguoiKy').val() !='' && $('#sl_tenNguoiKy').val() != '-1')
			$("#txt_nguoiKy").val($('#sl_tenNguoiKy option:selected').text());
		else
			$("#txt_nguoiKy").val("");
	}
	return false;
}
// NHÓM THẨM QUYỀN
function sl_nhomThamQuyen_onChange(id) {
	if (id == 'sl_nhomThamQuyen') {
		if ($("#sl_nhomThamQuyen").val() == "-1")
		{
			$("#txt_nhomThamQuyen").val("");
		}
		else
		{
			$("#txt_nhomThamQuyen").val($('#sl_nhomThamQuyen option:selected').text());
		}
	}
	return false;
}
//VĂN BẢN KÈM THEO
function sl_kemTheoVB_onChange(id) {
	if (id == 'sl_kemTheoVB') {
		if ($("#sl_kemTheoVB").val() == "-1")
		{
			$("#txt_kemTheoVB").val("");
		}
		else
		{
			$("#txt_kemTheoVB").val($('#sl_kemTheoVB option:selected').text());
		}
	}
	return false;
}
// CHI TIẾT LOẠI KN, TC
function sl_loaiKnTcCts_onChange(id) {
	if (id == 'sl_loaiKnTcCts') {
		if ($("#sl_loaiKnTcCts").val() == "-1")
		{
			$("#txt_loaiKnTcCts").val("");
		}
		else
		{
			$("#txt_loaiKnTcCts").val($('#sl_loaiKnTcCts option:selected').text());
		}
		
	}
	return false;
}
// TỈNH THÀNH

function sl_diaDanh_onChange(id, val) {
	var url = 'fethDiaDanhTrucThuoc';
	var callBack = null;
	if (id == 'tinhThanhPhosId') {
		if($('#tinhThanhPhosId').val() == '-1'){
			$("#txt_tinhThanh").val("");
		}
		else
			$("#txt_tinhThanh").val($('#tinhThanhPhosId option:selected').text());
		
		callBack = fetchQuanHuyehSuccess;
	} 
	else if (id == 'sl_quanHuyens') {
		if($('#sl_quanHuyens').val() == '-1'){
			$("#txt_quanHuyen").val("");
		}
		else
			$("#txt_quanHuyen").val($('#sl_quanHuyens option:selected').text());
		
		callBack = fetchPhuongXaSuccess;
	} 
	else if (id == 'sl_tinhThanhPhosId_2') {
		if($('#sl_tinhThanhPhosId_2').val() == '-1'){
			$("#txt_tinhThanh1").val("");
		}
		else
			$("#txt_tinhThanh1").val($('#sl_tinhThanhPhosId_2 option:selected').text());
		
		callBack = fetchQuanHuyen2Success;
	} 
	else if (id == 'sl_quanHuyens2') {
		if($('#sl_quanHuyens2').val() == '-1'){
			$("#txt_quanHuyen1").val("");
		}
		else
			$("#txt_quanHuyen1").val($('#sl_quanHuyens2 option:selected').text());
		
		callBack = fetchPhuongXa2Success;
		
	}
	else if (id == 'tinhThanhPhosIdSearch') {
		callBack = fetchQuanHuyehSuccessSearch;
	}
	else if (id == 'sl_quanHuyensSearch') {
		callBack = fetchPhuongXaSuccessSearch;
	}
	else if (id == 'tinhThanhPhosIdSearch_quick') {
		callBack = fetchQuanHuyehSuccessSearch_quick;
	}
	else if (id == 'sl_quanHuyensSearch_quick') {
		callBack = fetchPhuongXaSuccessSearch_quick;
	}
	
	doBlockUILoad();
	$.ajax({
		type : "POST",
		url : url,
		dataType : "json",
		data : {
			diaDanhChaId : val
		},
		success : function(json){
			doUnBlockUI();
			callBack(json);
		},
		error: function(){
			doUnBlockUI();
		}
	});
	return false;
}

// QUẬN/HUYỆN (NGƯỜI KNTC)
function fetchQuanHuyehSuccess(json) {
	$('#sl_quanHuyens').find('option').remove().end();
	$('#sl_quanHuyens').append(
			$('<option></option>').val("-1").html("Chọn Quận, huyện"));
	$.each(json, function() {
		$('#sl_quanHuyens').append(
				$('<option></option>').val(this.id).html(this.name));
	});
	
	var temp1 = $("#fetch_sl_quanHuyens").val();
	if (temp1 != "")
	{
		$('#sl_quanHuyens').val(temp1);
		$("#fetch_sl_quanHuyens").val("");
		$("#sl_quanHuyens").trigger("change");
		return false;
	}
	
//	if ($("#txt_quanHuyen").val() != "")
//		$("select").find("option:contains(" + $("#txt_quanHuyen").val() + ")").attr("selected", "selected");
	//sl_diaDanh_onChange('sl_quanHuyensSearch',$('#sl_quanHuyensSearch option:selected').val());
	$("#sl_quanHuyens").val("-1");
	$("#txt_quanHuyen").val("");
	$("#sl_xaPhuongs").html("<option value='-1'>Chọn Phường, xã</option>");
	$("#sl_xaPhuongs").val("-1");
	$("#txt_xaPhuong").val("");
	$("#sl_xaPhuongs").trigger("change");
	
	return false;
}
// QUẬN/HUYỆN (ĐỐI TƯỢNG BỊ KNTC)
function fetchQuanHuyen2Success(json) {
	$('#sl_quanHuyens2').find('option').remove().end();
	$('#sl_quanHuyens2').append(
			$('<option></option>').val("-1").html("Chọn Quận, huyện"));
	$.each(json, function() {
		$('#sl_quanHuyens2').append(
				$('<option></option>').val(this.id).html(this.name));

	});
	
	var temp1 = $("#fetch_sl_quanHuyens2").val();
	if (temp1 != "")
	{
		$('#sl_quanHuyens2').val(temp1);
		$("#fetch_sl_quanHuyens2").val("");
		$("#sl_quanHuyens2").trigger("change");
		return false;
	}
	
	$("sl_quanHuyens2").val("-1");
	$("#txt_quanHuyen1").val("");
	$("#sl_xaPhuongs2").html("<option value='-1'>Chọn Phường, xã</option>");
	$("#sl_xaPhuongs2").val("-1");
	$("#txt_xaPhuong1").val("");
	$("#sl_xaPhuongs2").trigger("change");
	
	return false;
}

// XÃ/PHƯỜNG (NGƯỜI KNTC)
function fetchPhuongXaSuccess(json) {
	$('#sl_xaPhuongs').find('option').remove().end();
	$('#sl_xaPhuongs').append(
			$('<option></option>').val('-1').html('Chọn Phường, xã'));
	$.each(json, function() {
		$('#sl_xaPhuongs').append(
				$('<option></option>').val(this.id).html(this.name));
	});
	
	var temp1 = $("#fetch_sl_xaPhuongs").val();
	if (temp1 != "")
	{
		$('#sl_xaPhuongs').val(temp1);
		$("#fetch_sl_xaPhuongs").val("");
	}
	
//	if ($("#txt_xaPhuong").val() != "")
//		$("select").find("option:contains(" + $("#txt_xaPhuong").val() + ")").attr("selected", "selected");
	
	$('#sl_xaPhuongs').trigger("change");
	return false;
}

// XÃ/PHƯỜNG (ĐỐI TƯỢNG BỊ KNTC)
function fetchPhuongXa2Success(json) {
	$('#sl_xaPhuongs2').find('option').remove().end();
	$('#sl_xaPhuongs2').append(
			$('<option></option>').val('-1').html('Chọn Phường, xã'));
	$.each(json, function() {
		$('#sl_xaPhuongs2').append(
				$('<option></option>').val(this.id).html(this.name));

	});
	
	var temp1 = $("#fetch_sl_xaPhuongs2").val();
	if (temp1 != "")
	{
		$('#sl_xaPhuongs2').val(temp1);
		$("#fetch_sl_xaPhuongs2").val("");
	}
	
	$('#sl_xaPhuongs2').trigger("change");
	return false;
}
// XÃ/PHƯỜNG
function sl_xaPhuong_onChange(id) {
	if (id == 'sl_xaPhuongs') {
		if ($("#sl_xaPhuongs").val() == "-1")
		{
			$("#txt_xaPhuong").val("");
			if($("#sl_quanHuyens").val() == "-1")
				$("#txt_diaChi").val($("#tinhThanhPhosId").val());
			else 
				$("#txt_diaChi").val($("#sl_quanHuyens").val());
		}
		else
		{
			$("#txt_xaPhuong").val($('#sl_xaPhuongs option:selected').text());
			$("#txt_diaChi").val($('#sl_xaPhuongs').val());
		}
	} else if (id == 'sl_xaPhuongs2') {
		if ($("#sl_xaPhuongs2").val() == "-1")
		{
			$("#txt_xaPhuong1").val("");
			if($("#sl_quanHuyens2").val() == "-1")
				$("#txt_diaChi1").val($("#sl_tinhThanhPhosId_2").val());
			else 
				$("#txt_diaChi1").val($("#sl_quanHuyens2").val());
		}
		else
		{
			$("#txt_xaPhuong1").val($('#sl_xaPhuongs2 option:selected').text());
			$("#txt_diaChi1").val($('#sl_xaPhuongs2').val());
		}
	}
	return false;
}

// THẨM QUYỀN
function sl_thamQuyen_onChange(id, value) {
	var url = 'fethThamQuyenThuoc';
	var callBack = null;
	if (id == 'sl_thamQuyen') {
		if($('#sl_thamQuyen').val() == '-1'){
			$("#txt_thamQuyen").val("");
		}
		else
			$("#txt_thamQuyen").val($('#sl_thamQuyen option:selected').text());
		callBack = fetchThamQuyenThuocSuccess;
		
	}else if (id == 'sl_thamQuyenSearch') {
		callBack = fetchThamQuyenThuocSuccessSearch;
	}else if (id == 'sl_nguonDonChucVu') {
		callBack = fetchThamQuyenThuocSuccessCD;
		$("#txt_chucVuCD").val($('#sl_nguonDonChucVu option:selected').text());
	}else if (id == 'sl_nguonDonChucVuSearch') {
		callBack = fetchThamQuyenThuocSearchSuccess;
	}
	
	doBlockUILoad();
	$.ajax({
		type : "POST",
		url : url,
		dataType : "json",
		data : {
			thamQuyenChaId : value
		},
		success : function(json) {
			doUnBlockUI();
			callBack(json); 
		},
		error: function(){
			doUnBlockUI();
		}
	});
	return false;
}

function fetchThamQuyenThuocSuccess(json) {
	$('#sl_nhomThamQuyen').find('option').remove().end();
	$('#sl_nhomThamQuyen').append(
			$('<option></option>').val('-1').html('Chọn nhóm Cơ quan giải quyết tiếp'));
	$.each(json, function() {
		$('#sl_nhomThamQuyen').append(
				$('<option></option>').val(this.id).html(this.name));

	});
	
	var temp1 = $("#fetch_sl_nhomThamQuyen").val();
	if (temp1 != "")
	{
		$('#sl_nhomThamQuyen').val(temp1);
		$("#fetch_sl_nhomThamQuyen").val("");
	}
	
	return false;
}
//Thẩm quyền cho Search
function fetchThamQuyenThuocSuccessSearch(json) {
	$('#sl_nhomThamQuyenSearch').find('option').remove().end();
	$('#sl_nhomThamQuyenSearch').append(
			$('<option></option>').val('-1').html('Chọn nhóm Cơ quan giải quyết tiếp'));
	$.each(json, function() {
		$('#sl_nhomThamQuyenSearch').append(
				$('<option></option>').val(this.id).html(this.name));

	});
	
	var temp1 = $("#fetch_sl_nhomThamQuyenSearch").val();
	if (temp1 != "")
	{
		$('#sl_nhomThamQuyenSearch').val(temp1);
		$("#fetch_sl_nhomThamQuyenSearch").val("");
	}
	
	return false;
}
//Chức vụ (Nguồn đơn từ TTCP)
function fetchThamQuyenThuocSuccessCD(json) {
	$('#sl_nguonDonCQ').find('option').remove().end();
//	$('#sl_nguonDonDongChi').append(
//			$('<option></option>').val(this.id).html(this.name));
	$.each(json, function() {
		$('#sl_nguonDonCQ').append(
				$('<option></option>').val(this.id).html(this.name));

	});
	
	var temp1 = $("#fetch_sl_nguonDonCQ").val();
	if (temp1 != "")
	{
		$('#sl_nguonDonCQ').val(temp1);
		$("#fetch_sl_nguonDonCQ").val("");
	}
	
	$('#sl_nguonDonCQ').trigger("change");
	return false;
}

//Chức vụ search (Nguồn đơn từ TTCP)
function fetchThamQuyenThuocSearchSuccess(json) {
	$('#sl_nguonDonCQSearch').find('option').remove().end();
	
	$('#sl_nguonDonCQSearch').append(
			$('<option></option>').val('-1').html('Chọn ' + $("#p3_label_search").text().toLowerCase().replace(":","")));
	
	$.each(json, function() {
		$('#sl_nguonDonCQSearch').append(
				$('<option></option>').val(this.id).html(this.name));

	});
	
	var temp1 = $("#fetch_sl_nguonDonCQSearch").val();
	if (temp1 != "")
	{
		$('#sl_nguonDonCQSearch').val(temp1);
		$("#fetch_sl_nguonDonCQSearch").val("");
	}
	
	return false;
}


// LOẠI KHIẾU TỐ (LOẠI ĐƠN)
function sl_loaiDon_onChange(id, value) {
	var url = 'fethLoaiDonThuoc';
	var callBack = null;
	if (id == 'sl_loaiDon') {
		if($('#sl_loaiDon').val() == '-1'){
			$("#txt_loaiDon").val("");
		}
		else
			$("#txt_loaiDon").val($('#sl_loaiDon option:selected').text());
		callBack = fetchLoaiDonSuccess;
		
	} else if (id == 'sl_loaiKnTcs') {
		if($('#sl_loaiKnTcs').val() == '-1'){
			$("#txt_loaiKnTcs").val("");
		}
		else
			$("#txt_loaiKnTcs").val($('#sl_loaiKnTcs option:selected').text());
		callBack = fetchLoaiKNCTSuccess;
		
	}else if (id == 'sl_loaiDonSearch') {
		callBack = fetchLoaiDonSuccessSearch;
	}else if (id == 'sl_loaiKnTcsSearch') {
		callBack = fetchLoaiKNCTSuccessSearch;
	} 
	
	doBlockUILoad();
	$.ajax({
		type : "POST",
		url : url,
		dataType : "json",
		data : {
			loaiDonChaId : value
		},
		success : function(json){
			doUnBlockUI();
			callBack(json); 
		},
		error: function(){
			doUnBlockUI();
		}
	});
	return false;
}

//=========== LOẠI KN CT=======================
function fetchLoaiDonSuccess(json) {
	$('#sl_loaiKnTcs').find('option').remove().end();
	$('#sl_loaiKnTcs').append(
			$('<option></option>').val("-1").html("Chọn loại KN, TC"));
	$.each(json, function() {
		$('#sl_loaiKnTcs').append(
				$('<option></option>').val(this.id).html(this.name));

	});
	
	var temp1 = $("#fetch_sl_loaiKnTcs").val();
	if (temp1 != "")
	{
		$('#sl_loaiKnTcs').val(temp1);
		$("#fetch_sl_loaiKnTcs").val("");
		$("#sl_loaiKnTcs").trigger("change");
		return false;
	}
	
	$("#sl_loaiKnTcs").val("-1");
	$("#txt_loaiKnTcs").val("");
	$("#sl_loaiKnTcCts").html("<option value='-1'>Chọn chi tiết loại KN, TC</option>");
	$("#sl_loaiKnTcCts").val("-1");
	$("#txt_loaiKnTcCts").val("");
	$("#sl_loaiKnTcCts").trigger("change");
	
	return false;
}
//For Search
function fetchLoaiDonSuccessSearch(json) {
	$('#sl_loaiKnTcsSearch').find('option').remove().end();
	$('#sl_loaiKnTcsSearch').append(
			$('<option></option>').val("-1").html("Chọn loại KN, TC"));
	$.each(json, function() {
		$('#sl_loaiKnTcsSearch').append(
				$('<option></option>').val(this.id).html(this.name));

	});
	
	var temp1 = $("#fetch_sl_loaiKnTcsSearch").val();
	if (temp1 != "")
	{
		$('#sl_loaiKnTcsSearch').val(temp1);
		$("#fetch_sl_loaiKnTcsSearch").val("");
		$("#sl_loaiKnTcsSearch").trigger("change");
		return false;
	}	
	
	$("#sl_loaiKnTcsSearch").val("-1");
	$("#sl_loaiKnTcCtsSearch").html("<option value='-1'>Chọn chi tiết loại KN, TC</option>");
	$("#sl_loaiKnTcCtsSearch").val("-1");
	$("#sl_loaiKnTcCtsSearch").trigger("change");
//	sl_loaiDon_onChange('sl_loaiKnTcsSearch',$('#sl_loaiKnTcsSearch option:selected').val());
	return false;
}
//========= CHI TIẾT=========================
function fetchLoaiKNCTSuccess(json) {
	$('#sl_loaiKnTcCts').find('option').remove().end();
	$('#sl_loaiKnTcCts').append(
			$('<option></option>').val("-1").html("Chọn chi tiết loại KN, TC"));
	$.each(json, function() {
		$('#sl_loaiKnTcCts').append(
				$('<option></option>').val(this.id).html(this.name));

	});
	
	var temp1 = $("#fetch_sl_loaiKnTcCts").val();
	if (temp1 != "")
	{
		$('#sl_loaiKnTcCts').val(temp1);
		$("#fetch_sl_loaiKnTcCts").val("");
	}
	
	return false;
}
//For Search
function fetchLoaiKNCTSuccessSearch(json) {
	$('#sl_loaiKnTcCtsSearch').find('option').remove().end();
	$('#sl_loaiKnTcCtsSearch').append(
			$('<option></option>').val('-1').html('Chọn chi tiết loại KN, TC'));
	$.each(json, function() {
		$('#sl_loaiKnTcCtsSearch').append(
				$('<option></option>').val(this.id).html(this.name));

	});
	
	var temp1 = $("#fetch_sl_loaiKnTcCtsSearch").val();
	if (temp1 != "")
	{
		$('#sl_loaiKnTcCtsSearch').val(temp1);
		$("#fetch_sl_loaiKnTcCtsSearch").val("");
	}
		
	return false;
}
// -------------------------FILL DATA DROPDOWNLIST TỈNH/HUYỆN/XÃ
// EDIT---------------------

// THẨM QUYỀN
function sl_thamQuyen(id, value) {
	var url = 'fethThamQuyenThuoc';
	var callBack = null;
	if (id == 'sl_thamQuyen') {
		callBack = fetchThamQuyenThuocSuccessEdit;
	}else if(id == 'sl_nguonDonChucVu'){
		callBack = fetchThamQuyenThuocSuccessCDs;
	}
	
	doBlockUILoad();
	$.ajax({
		type : "POST",
		url : url,
		dataType : "json",
		data : {
			thamQuyenChaId : value
		},
		success : function(json){
			doUnBlockUI();
			callBack(json);
		},
		error: function(){
			doUnBlockUI();
		}
	});
	return false;
}

function fetchThamQuyenThuocSuccessEdit(json) {
	$('#sl_nhomThamQuyen').find('option').remove().end();
	$('#sl_nhomThamQuyen').append(
			$('<option></option>').val(this.id).html(this.name));
	$.each(json, function() {
		$('#sl_nhomThamQuyen').append(
				$('<option></option>').val(this.id).html(this.name));

	});
	$("select").find("option:contains(" + $("#txt_nhomThamQuyen").val() + ")")
			.attr("selected", "selected");
	return false;
}
//Chuyển đơn (Từ nguồn Lãnh đạo TTCP)
function fetchThamQuyenThuocSuccessCDs(json) {
	$('#sl_nguonDonDongChi').find('option').remove().end();
	$('#sl_nguonDonDongChi').append(
			$('<option></option>').val(this.id).html(this.name));
	$.each(json, function() {
		$('#sl_nguonDonDongChi').append(
				$('<option></option>').val(this.id).html(this.name));

	});
	$("select").find("option:contains(" + $("#txt_coQuanCD").val() + ")")
			.attr("selected", "selected");
	return false;
}
// TRUNGNM filescan

	//
	function getKieuFileFromTenFile(fileName) {
		if ( ! fileName || fileName.length < 5) {
			return "unknown";
		}
		var extension = fileName.substring(fileName.length - 4, fileName.length);
		if (extension == ".jpg") return "image";
		if (extension == ".png") return "image";
		if (extension == ".gif") return "image";
		if (extension == ".doc") return "doc";
		if (extension == "docx") return "doc";
		if (extension == ".xls") return "excel";
		if (extension == "xlsx") return "excel";
		if (extension == ".ppt") return "powerpoint";
		if (extension == "pptx") return "powerpoint";
		if (extension == ".zip") return "archive";
		if (extension == ".rar") return "archive";
		if (extension == ".pdf") return "pdf";
		if (extension == ".txt") return "text";
		return "unknown";
	}

	//
	function getKieuFileTiengViet(kieuFile) {
		if (kieuFile == "image") return "Ảnh scan";
		if (kieuFile == "doc") return "Tài liệu doc";
		if (kieuFile == "excel") return "Excel";
		if (kieuFile == "powerpoint") return "Powerpoint";
		if (kieuFile == "archive") return "File lưu trữ";
		if (kieuFile == "pdf") return "Tài liệu PDF";
		if (kieuFile == "txt") return "Text";
		return "Không biết";
	}

	//
	function getExtensionPartOutOfFileName(fileName) {
		if ( ! fileName || fileName.length < 5) {
			return "not";
		}

		var extension = fileName.substring(fileName.length - 4, fileName.length);
		if (extension == ".jpg") return "jpg";
		if (extension == ".png") return "png";
		if (extension == ".gif") return "gif";
		if (extension == ".doc") return "doc";
		if (extension == ".xls") return "xls";
		if (extension == ".ppt") return "ppt";
		if (extension == ".zip") return "zip";
		if (extension == ".rar") return "rar";
		if (extension == ".pdf") return "pdf";

		if (fileName.length < 6) return "not";

		extension = fileName.substring(fileName.length - 5, fileName.length);
		if (extension == ".docx") return "docx";
		if (extension == ".xlsx") return "xlsx";
		if (extension == ".pptx") return "pptx";

		return "not";
	}

	//
	function getNamePartOutOfFileName(fileName) {
		if (fileName.length < 5) {
			return fileName;
		}
		var extension = fileName.substring(fileName.length - 4, fileName.length);
		var namePart = fileName.substring(0, fileName.length - 4);
		if (extension == ".jpg") return namePart;
		if (extension == ".png") return namePart;
		if (extension == ".gif") return namePart;
		if (extension == ".doc") return namePart;
		if (extension == ".xls") return namePart;
		if (extension == ".ppt") return namePart;
		if (extension == ".zip") return namePart;
		if (extension == ".rar") return namePart;
		if (extension == ".pdf") return namePart;
		if (extension == ".txt") return namePart;
		if (extension == ".not") return namePart;

		if (fileName.length < 6) {
			return fileName;
		}
		extension = fileName.substring(fileName.length - 5, fileName.length);
		namePart = fileName.substring(0, fileName.length - 5);
		if (extension == ".docx") return namePart;
		if (extension == ".xlsx") return namePart;
		if (extension == ".pptx") return namePart;

		return fileName;
	}

	// show galleria for data got from specific div
	function showGalleria(data_div_id)
	{
		var galleria_div_id = "#galleria_all_filescans";
		$(galleria_div_id).html($("#" + data_div_id).html());
		if ($.trim($(galleria_div_id).html()) == "") {
			alert_info("Không có file đính kèm nào !!!");
			return;
		}

		// display the div
		$(galleria_div_id).css("display", "");

		// Initialize Galleria
		Galleria.run(galleria_div_id);
		Galleria.ready(function () {
			$(galleria_div_id).data('galleria').enterFullscreen(function(){
				isAtGalleriaFullscreen = true;
				var c = this;
				c.detachKeyboard();
				c.attachKeyboard({
					escape:function (e) {
						var original_div = c._target;
						c.destroy();
						jQuery(original_div).css("display", "none");
						$(galleria_div_id).data('galleria',"");
						isAtGalleriaFullscreen = false;
					},
					right:c.next,
					left:c.prev
				});
			});
		});
	}

	// html for galleria item
	function getGalleriaItemHtmlForFile(namePart, extension, filescan) {
		var html = "";
		var mainImageUrl, thumbImageUrl, data_title, data_description, pdf_file;
		var base_url_dash = (baseUrl == "/" ? baseUrl : baseUrl + "/");
		// jpg -> may be a scan image
		if (extension == "jpg") {
			pdf_file = base_url_dash + filescan_folder + "/" + namePart + "_page.pdf";
			var noOfFiles = parseInt(filescan.noOfFiles);
			var i, namePartJpg;
			for (i = 0; i < noOfFiles; i++) {
				namePartJpg = namePart + (i < 1 ? "" : "_page" + i);
				data_title = "(" + filescan.ngayViet + ") " + filescan.name + "  (Trang " + (i+1) + ")";
				data_description = filescan.tomTat;
				mainImageUrl = base_url_dash + filescan_folder + "/" + namePartJpg + ".jpg";
				thumbImageUrl = base_url_dash + filescan_folder + "/" + namePartJpg + "_thumb.jpg";
				html += "<a href='" + mainImageUrl + "'>"
							+"<img data-title='" + data_title + "' "
								+"data-description='" + data_description + "' "
								+"data-pdf='" + pdf_file + "' "
								+"src='" + thumbImageUrl + "'>"
						+"</a>";
			}
			return html;
		}

		// other types
		data_title = "(" + filescan.ngayViet + ") " + filescan.name;
		data_description = filescan.tomTat;
		pdf_file = "";

		// png
		if (extension == "png" || extension == "gif") {
			mainImageUrl = base_url_dash + filescan_folder + "/" + namePart + "." + extension;
			thumbImageUrl = base_url_dash + filescan_folder + "/" + namePart + "_thumb.jpg";
			pdf_file = base_url_dash + filescan_folder + "/" + namePart + "_page.pdf";
		}
		if (extension == "pdf") {
			mainImageUrl = base_url_dash + "images/pdf_logo.png";
			thumbImageUrl = base_url_dash + "images/pdf_logo_100px.png";
			pdf_file = base_url_dash + filescan_folder + "/" + namePart + ".pdf";
		}
		if (extension == "doc" || extension == "docx") {
			mainImageUrl = base_url_dash + "images/word_logo.png";
			thumbImageUrl = base_url_dash + "images/word_logo_100px.png";
//			pdf_file = base_url_dash + filescan_folder + "/" + namePart + "_page.pdf";
			pdf_file = base_url_dash + filescan_folder + "/" + filescan.name;
		}
		if (extension == "xls" || extension == "xlsx") {
			mainImageUrl = base_url_dash + "images/excel_logo.png";
			thumbImageUrl = base_url_dash + "images/excel_logo_100px.png";
			pdf_file = base_url_dash + filescan_folder + "/" + filescan.name;
		}
		if (extension == "ppt" || extension == "pptx") {
			mainImageUrl = base_url_dash + "images/ppt_logo.png";
			thumbImageUrl = base_url_dash + "images/ppt_logo_100px.png";
			pdf_file = base_url_dash + filescan_folder + "/" + filescan.name;
		}
		if (extension == "zip") {
			mainImageUrl = base_url_dash + "images/zip_logo.png";
			thumbImageUrl = base_url_dash + "images/zip_logo_100px.png";
			pdf_file = base_url_dash + filescan_folder + "/" + filescan.name;
		}
		if (extension == "rar") {
			mainImageUrl = base_url_dash + "images/rar_logo.png";
			thumbImageUrl = base_url_dash + "images/rar_logo_100px.png";
			pdf_file = base_url_dash + filescan_folder + "/" + filescan.name;
		}
		if (extension == "not") {
			mainImageUrl = base_url_dash + "images/unknown_file_logo.png";
			thumbImageUrl = base_url_dash + "images/unknown_file_logo_100px.png";
			pdf_file = base_url_dash + filescan_folder + "/" + filescan.name;
		}
		html += "<a href='" + mainImageUrl + "'>"
			+"<img data-title='" + data_title + "' "
			+"data-description='" + data_description + "' "
			+"data-pdf='" + pdf_file + "' "
			+"src='" + thumbImageUrl + "'>"
			+"</a>";
		return html;
	}

	//init file dinh kem table		
	function initFilescanTable(id)
	{
		$("div#tabs-5 > table#table > tbody").html("");
		$.ajax({
			   type: "POST",
			   url: "fetchFilescanTable",
			   dataType: "json",
			   data: { id: id },
			   success:
				   function(json){
				   		var filescanList = json;
				   		var userId = $("#userId").val();	// id of the user who input this sodk
					    var html = "";
					    var html_galleria_all_filescans = "";
					   	var html_galleria_specific_filescans = "";
				   		if (filescanList)
					   	{
//				   			filescanList.sort(function(a,b){ return so_sanh_chuoi(a.name, b.name); });
							var i, count = 0;
							var filescan;
						   	for (i in filescanList)
					   		{
								count++;
						   		filescan = filescanList[i];
						   		var ngayScan = filescan.ngayViet == null ? "" : filescan.ngayViet;
						   		var tomTat = filescan.tomTat == null ? "" : filescan.tomTat;
								var kieuFile = getKieuFileFromTenFile(filescan.name);

								var isPrintable = kieuFile == "image" || kieuFile == "doc" || kieuFile == "pdf";
								var isViewable = kieuFile == "image";
								var isDownloadable = kieuFile != "image" && kieuFile != "pdf";

								var extension = getExtensionPartOutOfFileName(filescan.name);
								var namePart = getNamePartOutOfFileName(filescan.name);

						   		html += "<tr id='filescan_tr_" + filescan.id + "' title='" + filescan.name + "'>";
						   			html += '<td style="width:30px;">' + (count < 10 ? "0" + count : count) + '</td>';
						   			html += '<td style="text-align:center; width:85px; vertical-align: top;">' + ngayScan + '</td>';
						   			html += '<td style="text-align:left; vertical-align: top;"><b>' + tomTat + '</b></td>';
								    html += '<td style="text-align:left; vertical-align: top; width:120px;">' + getKieuFileTiengViet(kieuFile) + '</td>';
								   	html += '<td style="text-align:center; vertical-align: top; width:70px;">' + (kieuFile == "image" ? filescan.noOfFiles : "") + '</td>';
						   			html += '<td style="width:160px; vertical-align: top; text-align: left; padding-left:10px;">';

								    // chi nhan vien nhap don moi co the sua hay xoa file dinh kem
//						   			if (userId == "" || userId == currentUserNhanVienId)
//						   			{
						   				html += '<img title="Sửa" style="cursor:pointer; vertical-align: middle;" onclick="showEditFilescanForm(' + "'"  + filescan.id + "'" + ')" src="images/edit.png">';
						   				html += '&nbsp;&nbsp;|&nbsp;&nbsp;';
						   				html += '<img title="Xóa" style="cursor: pointer; vertical-align: middle;" onclick="deleteFilescan(' + "'" + filescan.id + "'" + ')" src="images/cancel.png">';

//						   			}
//						   				html += '<a href="' + baseUrl + '/upload/filescan/'+ filescan.name + '" class="lightbox"><img title="Xem" style="cursor:pointer; vertical-align: middle;" src="images/download.png"></a>';
//						   				html += '<a href="' + baseUrl + '/upload/filescan/'+ filescan.name + '" target="_blank"><img title="Xem" style="cursor:pointer; vertical-align: middle;" src="images/download.png"></a>';
								   	if (isDownloadable) {
										html += '&nbsp;&nbsp;|&nbsp;&nbsp;';
										html += '<a href="javascript:void(0);" onclick="downloadFilescan(\'' + filescan.name + '\')"><img title="Tải về" style="cursor:pointer; vertical-align: middle;" src="images/download.png"></a>';
								   	}

								    if (isViewable) {
										html += '&nbsp;&nbsp;|&nbsp;&nbsp;';
									   	html += '<a href="javascript:void(0);" onclick="showGalleria(\'galleria_data_' + filescan.id + '\')"><img title="Xem" style="cursor:pointer; vertical-align: middle;" src="images/gallery_16px.png"></a>';
									}

								   	if (isPrintable) {
										var base_url = window.location.protocol + "//" + window.location.hostname +	(window.location.port && ":" + window.location.port);
										var printPDFFilename = extension == "pdf" ? namePart + ".pdf" : namePart + "_page.pdf";
										var printLink = base_url + (baseUrl == "/" ? baseUrl : baseUrl + "/") + filescan_folder + "/" + printPDFFilename;
									   	html += '&nbsp;&nbsp;|&nbsp;&nbsp;';
									   	html += '<a href="javascript:void(0);" onclick="printFilescan(\'' + printLink + '\')"><img title="In" style="cursor:pointer; vertical-align: middle;" src="images/print.png"></a>';
								   	}

						   			html += '</td>';
						   		html += '</tr>';


								var fileHtml = getGalleriaItemHtmlForFile(namePart, extension, filescan);
								html_galleria_all_filescans += fileHtml;

							    var specificDataHtml = "<div id='galleria_data_" + filescan.id + "'>" + fileHtml + "</div>";
								html_galleria_specific_filescans += specificDataHtml;
						   	}
						}
	
				   		$("div#tabs-5 > table#table > tbody").html(html);
					    $("#galleria_all_filescan_data").html(html_galleria_all_filescans);
					   	$("#galleria_specific_filescan_data").html(html_galleria_specific_filescans);
						return false;
					},
			   error: 
				   	function(){
				   		alert_error("Có lỗi Server khi fetch danh sách các file đính kèm !!!");
				   		return false;
					}
		});
	}
	
	// init select nhanVien
	function initNhanVienSelect()
	{
		$("#nhanVien_frm").html("");
		var html = "";
		$.ajax({
			   type: "POST",
			   url: "fetchNhanVienSelectFilescan",
			   dataType: "json",
			   success:
				   function(json){
				   		var nhanVienList = json;
				   		if (nhanVienList)
					   	{
						   	nhanVienList.sort(function(a,b){ return so_sanh_ten(a.name, b.name); });
						   	for (i in nhanVienList)
					   		{
						   		var nhanVien = nhanVienList[i];
						   		var j;
						   		html += "<option value='" +nhanVien.value + "'>";
						   		html += nhanVien.display;
						   		html += "</option>";
						   	}
						}
						
				   		$("#nhanVien_frm").html(html);
				   		if (html != "")
				   			$("#nhanVien_frm").attr("selectedIndex",0);
	
						return false;
					},
			   error: 
				   	function(){
//				   		alert_error("Có lỗi Server khi fetch danh sách các nhân viên !!!");
				   		refresh_page();
				   		return false;
					}
		});
	}
	
	function submitFilescanForm()
	{
		$("#filescanForm").submit();
	}
	
	// when btn add_file clicked
	function showAddFilescanForm2()
	{
		var isEditSodk = parseInt($("#isEditMode").val()) > 0;
		// dang trong form them moi -> save form sodk de lay id truoc khi cho phep luu file dinh kem
		if (isEditSodk) {
			showAddFilescanForm();
		}
		else {
			if (! isEnoughFields())
				return false;

			doBlockUI();

			$("#isPrintClickedInNewMode").val('0');
			$("#isPrintMode").val('0');

			// log addFileScan button clicked in new dialog
			if ($("#isEditMode").val() == '0')
				$("#isFileInNewMode").val('1');

			// save the form data before allow to add filescan
			$("#isFileMode").val('1');
			submit_edit_form();
		}
		return false;
	}
	
	// callback after submit sodk details
	function showAddFilescanForm()
	{
		doUnBlockUI();
		var sodk_id = $("#txt_id").val();
		if ( ! sodk_id )
		{
		   	alert_error("Có lỗi xảy ra, vui lòng ấn F5 để refresh lại trang !!!");
		   	return false;
		}
		$('#sodk_id_frm').val(sodk_id);
		var ngayScanS = formatDate(new Date(), "dd/MM/yyyy");
		$('#ngayScan_frm').val(ngayScanS);
		
		showEditFilescanDialog();
		// Hide message validate
		$('.ui-dialog-titlebar-close').click( function(){
			hideFilescanForm();
		});

	}
		
	function showEditFilescanForm(val){
		var url = 'editFilescan';
		doBlockUI();
		$.ajax({
			   type: "POST",
			   url: url,
			   dataType: "json", 
			   data: 
				   {
				   		id: val
				   },
			   success: fetchEditFilescanSuccess,
			   error: function(){
					doUnBlockUI();
			   }
		});
	}
	
	function fetchEditFilescanSuccess(json){
		doUnBlockUI();
		//this is the json return data
		if (json)
		{
			if (json.status === true)
			{
				alert_error(json.message);
				return false;
			}
			
			$('#file_frm').css("display","none");
			$('#fileName_frm').css("display","inline");
			$('#showChooseFileA').css("display","inline");
			
			var filescan = json;
			$("#filescan_id_frm").val(filescan.id);
			$("#fileName_frm").val(filescan.name);
			$("#ngayScan_frm").val(filescan.ngayViet);
			$("#tomTat_frm").val(filescan.tomTat);
			$("#sodk_id_frm").val(filescan.sodkId);
			if (filescan.nhanVien)
			{
				$("#nhanVien_frm").val(filescan.nhanVien.nhanVienId);
			}
			showEditFilescanDialog();
		}
	}	
	
	function showEditFilescanDialog()
	{
		$("#filescanForm").dialog({
			title: "<img src='images/edit-add.png' style='vertical-align:middle;' />&nbsp;&nbsp;Thêm mới / Sửa File đính kèm ",
			autoOpen: true,
			modal: true,
			resizable : false,
			height: 200,
			width: 480
		});
		$("#tomTat_frm").focus();
		setTimeout(function(){ $("#tomTat_frm").trigger("click"); }, 200);
	}
	
	function hideFilescanForm()
	{
		resetFilescanForm();
		
		$("#filescanForm").dialog({
			autoOpen: false,
			position: 'center' 
		});
		$("#filescanForm").dialog("close"); 
	}
	
	function resetFilescanForm()
	{
		$('#filescan_id_frm').val("");
		$('#file_frm').css("display","block");
		$('#file_frm').val("");
		$('#fileName_frm').css("display","none");
		$('#showChooseFileA').css("display","none");
		$('#fileName_frm').val("");
		var ngayScanS = formatDate(new Date(), "dd/MM/yyyy");
		$('#ngayScan_frm').val(ngayScanS);
		$('#tomTat_frm').val("");
		$('#sodk_id_frm').val("");
		return false;
	}
	
	function showChooseFile()
	{
		$('#file_frm').css("display","block");
		$('#file_frm').val("");
		$('#fileName_frm').css("display","none");
		$('#showChooseFileA').css("display","none");
	}
	
	function startCallback() 
	{
		var isNew = true;
		if ($('#filescan_id_frm').val() != '')
			isNew = false;
		
		if (isNew)
		{
			if ($('#sodk_id_frm').val() == '')
			{
				alert_error("Có lỗi form! Vui lòng refresh lại trang !!!");
				return false;
			}
				
			if($('#file_frm').val() == '')
			{
				alert_error("Vui lòng chọn một file để upload !!!");
				return false;
			}
		}
		
		doBlockUI();
		return true;
	}
		
	function completeCallback(response) 
	{
		doUnBlockUI();
//		response = response.replace("<pre>","");
//		response = response.replace("</pre>","");
		if ($.type(response)=='string' && response.indexOf("</pre>") != -1)
			response = $(response).text();
		try
		{
			var result;
			if ($.type(response) == 'object')
				result = response;
			else
				result = $.parseJSON(response);

			if (result.status === true)
			{
				hideFilescanForm();
				var arr = result.message.split("+_+");
				var sodkId = arr[0];
				if ( ! sodkId )
				{
					alert_error("Có lỗi trả về từ server !!!");
					return false;
				}
				
				initFilescanTable(sodkId);
				alert_info(arr[1]);
			}
			else
			{
				alert_error(result.message);
			}
		}
		catch (error)
		{
			refresh_page();
		}
		return false;		
	}
	
	function deleteFilescan(id){
		confirm_show(
		    	"Bạn có chắc muốn xóa file này?",
	    	    {
	    			buttons: {					
						"CÓ": function() {
							$( this ).dialog( "close" );
							doBlockUI();
							var url = (baseUrl == "/" ? baseUrl : baseUrl + "/") + "deleteFilescan?id="+escape(id);
							$.ajax({
								   type: "POST",
								   url: url,
								   dataType: "json", 
								   data: 
									   {
									   		id: id
									   },
								   success: function(json){
									    doUnBlockUI();
		    							if (json)
			    						{
				    						if (json.status)
					    					{
						    					alert_info("Xóa thành công !!!");
						    					$("#filescan_tr_" + id).remove();
												var sodk_id = $("#txt_id").val();
												initFilescanTable(sodk_id);
						    				}
				    						else
					    					{
						    					alert_error(json.message);							    					
						    				}
				    					}
		    							else
			    						{
				    						alert_error("Có lỗi server !!!");
				    					}
				    					return false;
	    							},
	    							error: function(){
	    								doUnBlockUI();
	    							}
							});
							
						}
					}
		    	}
	    	);
		return false;
	}

// PRINT FORM
	
	// reset print form
	function reset_print_form()
	{
		$("#print_radio_list").html("");
		
		$("#printFrm_hoVaTen").val("");
		$("#printFrm_loaiDon").val("");
		$("#printFrm_loaiKNTC").val("");
		$("#printFrm_loaiKNTCChiTiet").val("");
		$("#printFrm_nhomThamQuyenGiaiQuyet").val("");
		$("#printFrm_nhomCoQuanGiaiQuyetTiep").val("");
		$("#printFrm_tenCoQuanGiaiQuyetTiep").val("");
		$("#printFrm_ngayNhap").val("");
		$("#printFrm_noiDungDon").val("");
		
		$("#printFrm_huongGiaiQuyet").val("");
		$("#printFrm_nguoiXuLy").val("");
		$("#printFrm_nguoiKy").val("");
		$("#printFrm_doiTuongGuiDon").val("");
		$("#printFrm_loaiKhieuTo").val("");
		$("#printFrm_doiTuongKNTC").val("");
		$("#printFrm_tenDoiTuongKNTC").val("");
		$("#printFrm_capQuanLyCQKNTC").val("");
		$("#printFrm_soHoSo").val("");
		$("#printFrm_nguoiNopDon").val("");
		$("#printFrm_homNay").val("");
		$("#printFrm_ngayVietDon").val("");
		$("#printFrm_diaChi").val("");
		
		$("#printFrm_gioiTinh").val("");
		$("#printFrm_coQuanChuyenDon").val("");
		$("#printFrm_nguonDon").val("");
		$("#printFrm_vanBanChuyenKemTheo").val("");
		$("#printFrm_ngayChuyenDon").val("");
		$("#printFrm_soCongVan").val("");
		
		return false;
	}
	
	// hide dialog
	function hide_print_form()
	{
		$("#printForm").dialog({
			autoOpen: false,
			position: 'center' 
		});
		$("#printForm").dialog("close"); 
	}

	// make print form ajax submit
	function makePrintFormAjaxSubmit() {
		$('#printForm').ajaxForm({
			url: ( baseUrl == "/" ? "/" : baseUrl + "/") + 'printSodk',
			async: false,	// need this to make sure the success callback code executed in "user action" context
			type: 'post',
			dataType: 'text',
			beforeSubmit: function(arr, $form, options) {

			},
			success:function (response, statusText, xhr, $form) {
				doUnBlockUI();
				$("#isForPrint").val("0");
				if ($.type(response)=='string' && response.indexOf("</pre>") != -1)
					response = $(response).text();

				// error
				if (!response || response.indexOf("error") == 0) {
					alert_error("Có lỗi xảy ra: " + response);
					return;
				}

				// success
				var ext = response.substring(response.length - 4, response.length);
				// print
				if (ext == ".pdf") {
					var base_url = window.location.protocol + "//" + window.location.hostname +	(window.location.port && ":" + window.location.port);
					var full_url = base_url + (baseUrl == "/" ? baseUrl : baseUrl + "/") + response;
					var newTab = window.open();
					newTab.location = full_url;
					if (window.focus) { newTab.focus(); }
				}
				else if (ext == ".doc") {
					window.location.href = (baseUrl == "/" ? baseUrl : baseUrl + "/") + response;
				}
				// download doc file
				else {
//					console.log(response);
//					alert_info(response);
					alert_error("Đã có lỗi xảy ra !!!");
					refresh_page();
//					window.location.href = (baseUrl == "/" ? baseUrl : baseUrl + "/") + response;
				}
			},
			error:function (xhr) {
				doUnBlockUI();
				$("#isForPrint").val("0");
				alert_error("Có lỗi Server dữ liệu trên server !!! \n" + xhr.responseText);
			}
		});
	}

	// for webtoolkit.aim.js
	// not used now
	function startCallbackPrintForm() {
		return true;
	}

	// for webtoolkit.aim.js
	// not used now
	function completeCallbackPrintForm(response) {
		doUnBlockUI();
		$("#isForPrint").val("0");
		if ($.type(response)=='string' && response.indexOf("</pre>") != -1)
			response = $(response).text();

		// error
		if (!response || response.indexOf("error") == 0) {
			alert_error("Có lỗi xảy ra: " + response);
			return;
		}

		// success
		var ext = response.substring(response.length - 4, response.length);
		// print
		if (ext == ".pdf") {
			var base_url = window.location.protocol + "//" + window.location.hostname +	(window.location.port && ":" + window.location.port);
			var full_url = base_url + (baseUrl == "/" ? baseUrl : baseUrl + "/") + response;
			var params  = 'width='+screen.width;
			params += ', height='+screen.height;
			params += ', top=0, left=0'
			params += ', fullscreen=yes';
			var newTab = window.open(full_url, "print", params);
			if (window.focus) { newTab.focus(); }
		}
		// download doc file
		else {
			window.location.href = (baseUrl == "/" ? baseUrl : baseUrl + "/") + response;
		}
	}

	// not used
	function requestFullScreen(element) {
		// Supports most browsers and their versions.
		var requestMethod = element.requestFullScreen || element.webkitRequestFullScreen || element.mozRequestFullScreen || element.msRequestFullScreen;
		console.log(requestMethod);

		if (requestMethod) { // Native full screen.
			requestMethod.call(element);
		} else if (typeof window.ActiveXObject !== "undefined") { // Older IE.
			var wscript = new ActiveXObject("WScript.Shell");
			if (wscript !== null) {
				wscript.SendKeys("{F11}");
			}
		}
	}

	function submit_print_form_for_print() {
		$("#isForPrint").val("1");
		submit_print_form();
	}

	// submit
	function submit_print_form()
	{
		$("input[name=mauIn]:checked").first().focus();
		doBlockUI();
//		checkFinishPrint();
		
		$("#printForm").submit(); 
	}
	
	// check finish print, unblock UI if print file downloaded
	function checkFinishPrint()
	{
		var printDone = $.cookie("printDone");
	    if (printDone == "yes")
	    {
	    	$.cookie("printDone", "no"); //clears this cookie value
	    	doUnBlockUI();
	    }
	    else
	    {
	    	setTimeout(function(){ checkFinishPrint(); }, 200);
	    }
	}
	
	// show dialog
	function show_print_dialog(nguonPrint)
	{
		//console.log("show_print_dialog");
		// fetch radio list
		var loaiDon = $.trim($("#printFrm_loaiDon").val());
		var temp = loaiDon.toLowerCase();
		var ok = false;
		
		var html = "";
		if (temp == 'khiếu nại')
		{
			ok = true;
			html += "<input type='radio' name='mauIn' value='1' id='mau1' /><label for='mau1'>1. Phiếu hướng dẫn</label>";
			html += "<br />";
			html += "<input type='radio' name='mauIn' value='2' id='mau2' /><label for='mau2'>2. Phiếu chuyển đơn có báo cáo</label>";
			html += "<br />";
			html += "<input type='radio' name='mauIn' value='3' id='mau3' /><label for='mau3'>3. Phiếu chuyển đơn không có báo cáo</label>";
			html += "<br />";
			html += "<input type='radio' name='mauIn' value='4' id='mau4' /><label for='mau4'>4. Giấy báo tin 1</label>";
			html += "<br />";
			html += "<input type='radio' name='mauIn' value='5' id='mau5' /><label for='mau5'>5. Giấy báo tin 2</label>";
			html += "<br />";
			html += "<input type='radio' name='mauIn' value='6' id='mau6' /><label for='mau6'>6. Hồ sơ khiếu tố</label>";
			html += "<br />";
			html += "<input type='radio' name='mauIn' value='7' id='mau7' /><label for='mau7'>7. Phiếu chuyển đơn cho cá nhân CBC</label>";
			html += "<br />";
			html += "<input type='radio' name='mauIn' value='8' id='mau8' /><label for='mau8'>8. Phiếu chuyển đơn cho cá nhân KBC</label>";
			html += "<br />";
			html += "<input type='radio' name='mauIn' value='9' id='mau9' /><label for='mau9'>9. Mẫu chuyển đơn thừa lệnh</label>";
			html += "<br />";
			html += "<input type='radio' name='mauIn' value='10' id='mau10' /><label for='mau10'>10. Phiếu đề xuất chuyển Cục, Vụ chức năng</label>";
			html += "<br />";
			html += "<input type='radio' name='mauIn' value='11' id='mau11' /><label for='mau11'>11. Phiếu đề xuất xử lý đơn khiếu nại</label>";
			html += "<br />";
			html += "<input type='radio' name='mauIn' value='12' id='mau12' /><label for='mau12'>12. Phiếu trả đơn khiếu nại</label>";
			html += "<br />";
			html += "<input type='radio' name='mauIn' value='13' id='mau13' /><label for='mau13'>13. Thông báo không thụ lý giải quyết khiếu nại</label>";
			html += "<br />";
    	}

		if (temp == 'kiến nghị')
		{
			ok = true;
			html += "<input type='radio' name='mauIn' value='1' id='mau1' /><label for='mau1'>1. Phiếu hướng dẫn</label>";
			html += "<br />";
			html += "<input type='radio' name='mauIn' value='2' id='mau2' /><label for='mau2'>2. Phiếu chuyển đơn có báo cáo</label>";
			html += "<br />";
			html += "<input type='radio' name='mauIn' value='3' id='mau3' /><label for='mau3'>3. Phiếu chuyển đơn không có báo cáo</label>";
			html += "<br />";
			html += "<input type='radio' name='mauIn' value='4' id='mau4' /><label for='mau4'>4. Giấy báo tin 1</label>";
			html += "<br />";
			html += "<input type='radio' name='mauIn' value='5' id='mau5' /><label for='mau5'>5. Giấy báo tin 2</label>";
			html += "<br />";
			html += "<input type='radio' name='mauIn' value='6' id='mau6' /><label for='mau6'>6. Hồ sơ khiếu tố</label>";
			html += "<br />";
			html += "<input type='radio' name='mauIn' value='7' id='mau7' /><label for='mau7'>7. Phiếu chuyển đơn cho cá nhân CBC</label>";
			html += "<br />";
			html += "<input type='radio' name='mauIn' value='8' id='mau8' /><label for='mau8'>8. Phiếu chuyển đơn cho cá nhân KBC</label>";
			html += "<br />";
			html += "<input type='radio' name='mauIn' value='9' id='mau9' /><label for='mau9'>9. Mẫu chuyển đơn thừa lệnh</label>";
			html += "<br />";
			html += "<input type='radio' name='mauIn' value='10' id='mau10' /><label for='mau10'>10. Phiếu đề xuất chuyển Cục, Vụ chức năng</label>";
			html += "<br />";
    	}

		if (temp == 'phản ánh')
		{
			ok = true;
			html += "<input type='radio' name='mauIn' value='1' id='mau1' /><label for='mau1'>1. Phiếu hướng dẫn</label>";
			html += "<br />";
			html += "<input type='radio' name='mauIn' value='2' id='mau2' /><label for='mau2'>2. Phiếu chuyển đơn có báo cáo</label>";
			html += "<br />";
			html += "<input type='radio' name='mauIn' value='3' id='mau3' /><label for='mau3'>3. Phiếu chuyển đơn không có báo cáo</label>";
			html += "<br />";
			html += "<input type='radio' name='mauIn' value='4' id='mau4' /><label for='mau4'>4. Giấy báo tin 1</label>";
			html += "<br />";
			html += "<input type='radio' name='mauIn' value='5' id='mau5' /><label for='mau5'>5. Giấy báo tin 2</label>";
			html += "<br />";
			html += "<input type='radio' name='mauIn' value='6' id='mau6' /><label for='mau6'>6. Hồ sơ khiếu tố</label>";
			html += "<br />";
			html += "<input type='radio' name='mauIn' value='7' id='mau7' /><label for='mau7'>7. Phiếu chuyển đơn cho cá nhân CBC</label>";
			html += "<br />";
			html += "<input type='radio' name='mauIn' value='8' id='mau8' /><label for='mau8'>8. Phiếu chuyển đơn cho cá nhân KBC</label>";
			html += "<br />";
			html += "<input type='radio' name='mauIn' value='9' id='mau9' /><label for='mau9'>9. Mẫu chuyển đơn thừa lệnh</label>";
			html += "<br />";
			html += "<input type='radio' name='mauIn' value='10' id='mau10' /><label for='mau10'>10. Phiếu đề xuất chuyển Cục, Vụ chức năng</label>";
			html += "<br />";
    	}

		if (temp == 'tố cáo')
		{
			ok = true;
			html += "<input type='radio' name='mauIn' value='1' id='mau1' /><label for='mau1'>1. Phiếu hướng dẫn</label>";
			html += "<br />";
			html += "<input type='radio' name='mauIn' value='2' id='mau2' /><label for='mau2'>2. Phiếu chuyển đơn có báo cáo</label>";
			html += "<br />";
			html += "<input type='radio' name='mauIn' value='3' id='mau3' /><label for='mau3'>3. Phiếu chuyển đơn không có báo cáo</label>";
			html += "<br />";
			html += "<input type='radio' name='mauIn' value='4' id='mau4' /><label for='mau4'>4. Giấy báo tin 1</label>";
			html += "<br />";
			html += "<input type='radio' name='mauIn' value='5' id='mau5' /><label for='mau5'>5. Giấy báo tin 2</label>";
			html += "<br />";
			html += "<input type='radio' name='mauIn' value='6' id='mau6' /><label for='mau6'>6. Hồ sơ khiếu tố</label>";
			html += "<br />";
			html += "<input type='radio' name='mauIn' value='7' id='mau7' /><label for='mau7'>7. Phiếu chuyển đơn cho cá nhân CBC</label>";
			html += "<br />";
			html += "<input type='radio' name='mauIn' value='8' id='mau8' /><label for='mau8'>8. Phiếu chuyển đơn cho cá nhân KBC</label>";
			html += "<br />";
			html += "<input type='radio' name='mauIn' value='9' id='mau9' /><label for='mau9'>9. Mẫu chuyển đơn thừa lệnh</label>";
			html += "<br />";
			html += "<input type='radio' name='mauIn' value='10' id='mau10' /><label for='mau10'>10. Phiếu đề xuất chuyển Cục, Vụ chức năng</label>";
			html += "<br />";
			html += "<input type='radio' name='mauIn' value='11' id='mau11' /><label for='mau11'>11. Phiếu chuyển đơn tố cáo</label>";
			html += "<br />";
			html += "<input type='radio' name='mauIn' value='12' id='mau12' /><label for='mau12'>12. Thông báo chuyển đơn tố cáo</label>";
			html += "<br />";
    	}
		
		// not ok => show error
		if ( ! ok )
		{
			doUnBlockUI();
			alert_error("Có lỗi khi hiển thị form In !!!");
			reset_print_form();
			return false;
		}
		$("#print_radio_list").html(html);
		setCheckedValue(document.forms['printForm'].elements['mauIn'], '1');
		var first_btn = $("#printForm").find("button.save").first();
		var last_btn = $("#printForm").find("button.save").last();
		doStimulateTabActionO($("#printForm input:radio"),first_btn);
		doStimulateTabActionS(last_btn, "#printForm input:radio:checked");
		
		// bind enter key short cut to printForm input
		bindEnterKeyForForm("printForm");
		
		// print mode in editForm
		if (nguonPrint == '2' || nguonPrint == 2)
		{
			$("#isFileMode").val('0');
			$("#isFileInNewMode").val('0');
			
			// log print button clicked in new dialog 
			if ($("#isEditMode").val() == '0')
				$("#isPrintClickedInNewMode").val('1');
				
			$("#isPrintMode").val('1');
			submit_edit_form();
		}
		else
		{
			// ok => show dialog
			display_print_dialog();
		}
		
    	return false;
	}
	
	var isPrintDialogOpened = false;
	// display print dialog
	function display_print_dialog()
	{
		doUnBlockUI();
		isPrintDialogOpened = true;
		numberOfOpenedMsgbox ++;
		
		bindKeyShortcutPrintDialogForObject($(document));
		// show dialog
		$("#printForm").dialog({
			title: "<img src='images/print.png' style='vertical-align:middle;' />&nbsp;&nbsp;Mẫu In ",
			autoOpen: true,
			modal: true,
			resizable : false,			
			minHeight: 370,
			minWidth: 480,
			beforeClose: function(event, ui) {
				$("#isPrintMode").val('0');
				isPrintDialogOpened = false;
				
				// 50ms sau moi reset lai bien de tranh truong hop event bubble up
				setTimeout(function(){
					if (numberOfOpenedMsgbox > 0)
						numberOfOpenedMsgbox --;
				},50);
				
				// unbind key short cut
				unbindKeyShortcutPrintDialog($(document));
				
				// neu ma editForm hien thi thi focus vao truong dau tien
				if ($("#light").css("display") != "none")
				{
					setTimeout(function(){ $("#id_sodkSo").focus();}, 100);
				}
				
				reset_print_form();
			}
		});
	}
	
	// bind key short cut for print dialog
	function bindKeyShortcutPrintDialogForObject(jquery_obj)
	{
		jquery_obj.bind('keydown', 'alt+1', fncPrintPhieuIn);
		jquery_obj.bind('keydown', 'alt+2', fncPrintPhieuIn_for_print);
		jquery_obj.bind('keydown', 'alt+3', fncHuyBoPhieuIn);
		return false;
	}
	
	// unbind key short cut for print dialog
	function unbindKeyShortcutPrintDialog(jquery_obj)
	{
		jquery_obj.unbind('keydown', fncPrintPhieuIn);
		jquery_obj.unbind('keydown', fncPrintPhieuIn_for_print);
		jquery_obj.unbind('keydown', fncHuyBoPhieuIn);
		return false;
	}
	
	// action print Phieu In
	function fncPrintPhieuIn(event)
	{
		if ($("div.ui-widget-overlay").length > 0 && $("div.ui-widget-overlay").first().is(":visible"))
		{
			$("#printForm").find("button.save").first().trigger("click");
			$("input[name=mauIn]:checked").first().focus();
			return false;
		}
	}

	// action print Phieu In
	function fncPrintPhieuIn_for_print(event)
	{
		if ($("div.ui-widget-overlay").length > 0 && $("div.ui-widget-overlay").first().is(":visible"))
		{
			$("#printForm").find("#printForm_print_btn").first().trigger("click");
			$("input[name=mauIn]:checked").first().focus();
			return false;
		}
	}
	
	// action close Print Form
	function fncHuyBoPhieuIn(event)
	{
		if ($("div.ui-widget-overlay").length > 0 && $("div.ui-widget-overlay").first().is(":visible"))
		{
			$("#printForm").find("button.save").last().trigger("click");
			return false;
		}
	}
	
	// 
	function show_print_form(nguonPrint)
	{
		doBlockUI();
		// in tu table
		if (nguonPrint == "1" || nguonPrint == 1)
		{
			if ($("#selected_id").val() == "")
			{
				doUnBlockUI();
				alert_error("Vui lòng chọn một đơn thư trước !!!");
				return false;
			}

			var id = $("#selected_id").val();
			$.ajax({
				   type: "POST",
				   url: "fetchPrintFormSodk",
				   dataType: "json",
				   data: { id : id },
				   success:
					   function(json){
					   		doUnBlockUI();
					   		if (json)
						   	{
							   	if (json.status)
								{
							   		if (json.hoVaTen)
										$("#printFrm_hoVaTen").val(json.hoVaTen);
									if (json.loaiDon)
										$("#printFrm_loaiDon").val(json.loaiDon);
									if (json.loaiKNTC)
										$("#printFrm_loaiKNTC").val(json.loaiKNTC);
									if (json.loaiKNTCChiTiet)
										$("#printFrm_loaiKNTCChiTiet").val(json.loaiKNTCChiTiet);
									if (json.nhomThamQuyenGiaiQuyet)
										$("#printFrm_nhomThamQuyenGiaiQuyet").val(json.nhomThamQuyenGiaiQuyet);
									if (json.nhomCoQuanGiaiQuyetTiep)
										$("#printFrm_nhomCoQuanGiaiQuyetTiep").val(json.nhomCoQuanGiaiQuyetTiep);
									if (json.tenCoQuanGiaiQuyetTiep)
										$("#printFrm_tenCoQuanGiaiQuyetTiep").val(json.tenCoQuanGiaiQuyetTiep);
									if (json.ngayNhap)
										$("#printFrm_ngayNhap").val(json.ngayNhap);
									if (json.noiDungDon)
										$("#printFrm_noiDungDon").val(json.noiDungDon);
									
									if (json.huongGiaiQuyet)
										$("#printFrm_huongGiaiQuyet").val(json.huongGiaiQuyet);
									if (json.nguoiXuLy)
										$("#printFrm_nguoiXuLy").val(json.nguoiXuLy);
									if (json.nguoiKy)
										$("#printFrm_nguoiKy").val(json.nguoiKy);
									if (json.doiTuongGuiDon)
										$("#printFrm_doiTuongGuiDon").val(json.doiTuongGuiDon);
									if (json.loaiKhieuTo)
										$("#printFrm_loaiKhieuTo").val(json.loaiKhieuTo);
									if (json.doiTuongKNTC)
										$("#printFrm_doiTuongKNTC").val(json.doiTuongKNTC);
									if (json.tenDoiTuongKNTC)
										$("#printFrm_tenDoiTuongKNTC").val(json.tenDoiTuongKNTC);
									if (json.capQuanLyCQKNTC)
										$("#printFrm_capQuanLyCQKNTC").val(json.capQuanLyCQKNTC);
									if (json.soHoSo)
										$("#printFrm_soHoSo").val(json.soHoSo);
									if (json.nguoiNopDon)
										$("#printFrm_nguoiNopDon").val(json.nguoiNopDon);
									if (json.homNay)
										$("#printFrm_homNay").val(json.homNay);
									if (json.ngayVietDon)
										$("#printFrm_ngayVietDon").val(json.ngayVietDon);
									if (json.diaChi)
										$("#printFrm_diaChi").val(json.diaChi);
									
									if (json.gioiTinh)
										$("#printFrm_gioiTinh").val(json.gioiTinh);
									if (json.coQuanChuyenDen)
										$("#printFrm_coQuanChuyenDon").val(json.coQuanChuyenDen);
									if (json.nguonDon)
										$("#printFrm_nguonDon").val(json.nguonDon);
									if (json.vanBanChuyenKemTheo)
										$("#printFrm_vanBanChuyenKemTheo").val(json.vanBanChuyenKemTheo);
									if (json.ngayChuyenDon)
										$("#printFrm_ngayChuyenDon").val(json.ngayChuyenDon);																		
									if (json.soCongVan)
										$("#printFrm_soCongVan").val(json.soCongVan);
									
									show_print_dialog(nguonPrint);
								}
							   	else
								{
							   		alert_error(json.message);							   		
//							   		alert_error("Đơn thư này không đủ thông tin để In !!! Xin vui lòng thêm thông tin cho hồ sơ đó !!!");	
								}
							}
					   		else
					   			alert_error("Có lỗi Server khi lấy thông tin để In !!!");

				   			return false;
						},	
				   error: 
					   	function(){
					   		doUnBlockUI();
					   		alert_error("Có lỗi Server khi lấy thông tin để In !!!");
					   		refresh_page();
					   		return false;
						}
			});
			
			return false;	
		}

		// in tu form edit
		if (nguonPrint == "2" || nguonPrint == 2)
		{
			
			if ( ! isEnoughFields() )
			{
				doUnBlockUI();
				return false;
			}
			
			$("#printFrm_hoVaTen").val( chuanHoaTen($("#ndHoTen").val()) );
			var loaiDon = $("#sl_loaiDon option:selected").text();
			$("#printFrm_loaiDon").val(loaiDon);
			$("#printFrm_loaiKNTC").val($("#sl_loaiKnTcs option:selected").text());
			$("#printFrm_loaiKNTCChiTiet").val($("#sl_loaiKnTcCts option:selected").text());
			
			if ( $("#sl_thamQuyen").val() == "" || $("#sl_thamQuyen").val() == "-1" )
				$("#printFrm_nhomThamQuyenGiaiQuyet").val("");
			else
				$("#printFrm_nhomThamQuyenGiaiQuyet").val($("#sl_thamQuyen option:selected").text());
			
			if ( $("#sl_nhomThamQuyen").val() == "" || $("#sl_nhomThamQuyen").val() == "-1" )
				$("#printFrm_nhomCoQuanGiaiQuyetTiep").val("");
			else
				$("#printFrm_nhomCoQuanGiaiQuyetTiep").val($("#sl_nhomThamQuyen option:selected").text());
			
			$("#printFrm_tenCoQuanGiaiQuyetTiep").val($("#txt_coQuanTq").val());
			
//			var ngayNhap = formatDate(new Date(getDateFromFormat($("#txt_ngay").val(),"yyyy-MM-dd")),"dd/MM/yyyy");
			$("#printFrm_ngayNhap").val($("#txt_ngay").val());
			$("#printFrm_noiDungDon").val($("#ghiChu").val());
			
			// 
			$("#printFrm_huongGiaiQuyet").val($("#huongGQ option:selected").text());
			$("#printFrm_nguoiXuLy").val($("#sl_tenNguoiXL option:selected").text());
			
			if ($("#sl_tenNguoiKy").val() != "-1" && $("#sl_tenNguoiKy").val() != -1)
				$("#printFrm_nguoiKy").val($("#sl_tenNguoiKy option:selected").text());
			else
				$("#printFrm_nguoiKy").val("");
			
			var dtgd1 = getCheckedValue(document.forms['editForm'].elements['hs_type']);
			if (dtgd1 == "1" || dtgd1 == 1)
				$("#printFrm_doiTuongGuiDon").val("Cá nhân");
			if (dtgd1 == "0" || dtgd1 == 0)
				$("#printFrm_doiTuongGuiDon").val("Tập thể");
			if (dtgd1 == "2" || dtgd1 == 2)
				$("#printFrm_doiTuongGuiDon").val("Cơ quan, tổ chức");
			
			$("#printFrm_loaiKhieuTo").val($("#sl_loaiKnTcCts option:selected").text());
			
			$("#printFrm_capQuanLyCQKNTC").val("");
			var dtkntc1 = getCheckedValue(document.forms['editForm'].elements['hsdt_type']);
			if (dtkntc1 == "1" || dtkntc1 == 1)
			{
				$("#printFrm_doiTuongKNTC").val("Cá nhân");
				$("#printFrm_tenDoiTuongKNTC").val( chuanHoaTen($("#ndHoTen1").val()) );
				$("#printFrm_capQuanLyCQKNTC").val( $.trim($("#coQuanCt1").val()) );
			}
			
			if (dtkntc1 == "2" || dtkntc1 == 2)
			{
				$("#printFrm_doiTuongKNTC").val("Cơ quan");
				$("#printFrm_tenDoiTuongKNTC").val( $.trim($("#co_quan_btc").val()) );
			}			
			
			$("#printFrm_soHoSo").val( $("#id_sodkSo").val() );
			$("#printFrm_nguoiNopDon").val( chuanHoaTen($("#ndHoTen").val()) );
			
			var homNay = formatDate(new Date(),"dd/MM/yyyy");
			$("#printFrm_homNay").val(homNay);
			
			$("#printFrm_ngayVietDon").val("");
			
			
			var diaChiChiTiet = $.trim($("#chiTietDiaChi").val());
			diaChiChiTiet = diaChiChiTiet.replace(/ \-/g,",");
			diaChiChiTiet = diaChiChiTiet.replace(/\-/g,",");
			
			var diaChiId = "";
			if ($("#sl_xaPhuongs").val() == "-1" || $("#sl_xaPhuongs").val() == "")
			{
				if ($("#sl_quanHuyens").val() == "-1" || $("#sl_quanHuyens").val() == "")
				{
					if ($("#tinhThanhPhosId").val() == "-1" || $("#tinhThanhPhosId").val() == "")
						diaChiId = "";
					else
						diaChiId = $("#tinhThanhPhosId").val();
				}
				else
				{
					diaChiId = $("#sl_quanHuyens").val();
				}
			}
			else
			{
				diaChiId = $("#sl_xaPhuongs").val();
			}
			
			if (diaChiId == "")
				$("#printFrm_diaChi").val(diaChiChiTiet);
			else
			{
				$.ajax({
					   type: "POST",
					   url: "getDiaChiChiTietSodk",
					   dataType: "json",
					   data: { 
						   diaChiId : diaChiId,
						   diaChiChiTiet : diaChiChiTiet
					   },
					   success:
						   function(json){
						   		if (json)
							   	{
						   			$("#printFrm_diaChi").val(json.message);
								}
						   		else
						   			$("#printFrm_diaChi").val("");

					   			return false;
							},	
					   error: 
						   	function(){
						   		$("#printFrm_diaChi").val("");
						   		return false;
							}
				});
			}
			
			var gioiTinh = "ông (bà)";
			if ($("#sex").val() == '0' || $("#sex").val() == 0)
				gioiTinh = "bà";
			if ($("#sex").val() == '1' || $("#sex").val() == 1)
				gioiTinh = "ông";
			$("#printFrm_gioiTinh").val(gioiTinh);
			
			// nguon don chuyen den
			var coQuanChuyenDen = "";
			var nguonDon = "";
			var sl_nd = parseInt($("#select_nguonDon").val());
			var i, nd;
			for (i=0; i < nguon_don_list.length; i++ ) {
				nd = nguon_don_list[i];
				if (nd.value == sl_nd) {
					nguonDon = nd.name;
					if (nd.p3.visible) {
						coQuanChuyenDen = $("#sl_nguonDonCQ option:selected").text();
					}
					break;
				}
			}
			$("#printFrm_coQuanChuyenDon").val(coQuanChuyenDen);
			$("#printFrm_nguonDon").val(nguonDon);

//			if (sl_nd == '2')
//				coQuanChuyenDen = $("#sl_nguonDonCQ option:selected").text();
//			if (sl_nd == '4')
//				coQuanChuyenDen = $("#sl_nguonDonDongChi option:selected").text();
//			if (sl_nd == '5')
//				coQuanChuyenDen = $("#sl_nguonDonCucVu option:selected").text();
//			$("#printFrm_coQuanChuyenDon").val(coQuanChuyenDen);
			
			// nguon don
//			var nguonDon = "";
//			if (sl_nd == '1')
//				nguonDon = "Do dân chuyển tới";
//			if (sl_nd == '2')
//				nguonDon = "Cơ quan khác chuyển tới";
//			if (sl_nd == '3')
//				nguonDon = "Trực tiếp trình bày";
//			if (sl_nd == '4')
//				nguonDon = "Lãnh đạo TTCP";
//			if (sl_nd == '5')
//				nguonDon = "Đơn vị thuộc TTCP";
//			$("#printFrm_nguonDon").val(nguonDon);
			
			// van ban chuyen kem theo
			$("#printFrm_vanBanChuyenKemTheo").val($("#txt_kemTheoVB").val());
			
			// ngay chuyen don, so cong van
			$("#printFrm_ngayChuyenDon").val($("#nguonDon_NgayChuyenDon").val());
			$("#printFrm_soCongVan").val($("#nguonDon_SoCongVan").val());
			
			show_print_dialog(nguonPrint);
			return false;	
		}

		// loi
		doUnBlockUI();
		alert_error("Có lỗi trong quá trình khởi tạo in !!!");
		return false;	
	}

// END TRUNGNM

	function resetEditFormHeight()
	{
		//var new_height = $("#editForm")[0].offsetHeight + 1;
		//$("#light").css('height', new_height +"px");
	}
	
// them
	
	// find parent with specific tag name
	function findParentSpecificTag(element, tagName)
	{
		if ($(element).is("body"))
			return false;
		if ($(element).parent().is("body"))
			return false;
		if ($(element).parent().is(tagName))
			return $(element).parent();
		else
			return findParentSpecificTag($(element).parent(), tagName);
	}
	
	// focus on the first text field in editForm
	function focusOnFirstFieldEditForm()
	{
		if ($("#editForm").is(":visible"))
		{
			$("#id_sodkSo").focus();
		}
	}
	
	// mark danh dau trung don
	var daDanhDauTrungDon = false;
	function markDaDanhDauTrungDon()
	{
		daDanhDauTrungDon = true;
	}
	
// filescan

	// print filescan
	function printFilescan(link) {
		var newtab = window.open();
		newtab.location = link;
		if (window.focus) { newtab.focus(); }
	}

	// download filescan
	function downloadFilescan(fileName) {
		window.location.href = (baseUrl == "/" ? baseUrl : baseUrl + "/") + filescan_folder + "/" + fileName;
	}

//	// download filescan
//	function downloadFilescan(filescanId)
//	{
//		doBlockUI();
//		$.ajax({
//			type : "POST",
//			url : "downloadFilescan",
//			data : { id: filescanId },
//			dataType : "json",
//			success : function(json){
//				doUnBlockUI();
//				if (json.status)
//				{
//				if (json.fileReady)
//				{
//					filescanFileReady(json.fileUrl);
//				}
//				else
//				{
//					filescanBinary(false);
//				}
//				}
//				else
//				{
//					alert_error(json.message);
//				}
//				return false;
//			},
//			error: function(){
//				doUnBlockUI();
//			}
//		});
//		return false;
//	}
		
		// is file is image?
		function isImageFile(file_url)
		{
	  		var ext = file_url.substr(-4);
	  		ext = ext.toLowerCase();
			if( ext === ".jpg" || ext === "jpeg")
	  			return true;
			if( ext === ".png")
	  			return true;
			if( ext === ".gif")
	  			return true;
			if( ext === ".bmp")
	  			return true;
			if( ext === ".tif")
	  			return true;
  		return false;
	  	}
		
		// file ready in webserver
	  	function filescanFileReady(file_url)
	  	{	  		
	  		if ( isImageFile(file_url) )
	  		{
	  			$.lightbox(file_url);	  			
	  			setTimeout(function(){
	  				var zindex = $(".jquery-lightbox-buttons").css("z-index");
	  				if ( ! zindex)
	  					zindex = "7001";
	  				$(".jquery-lightbox-background").css("z-index","" + zindex);
	  			},300);
	  		}
	  		else  	  	  		
	  			filescanBinary(true);
  		return false;
	  	}

	  	// binary file not image
	  	function filescanBinary(isIframe)
	  	{  	  	  	
	  		$.lightbox("doDownloadFilescan", {
		        'width'       : 310,
		        'height'      : 310,
		        'modal'		  : true,
		        'iframe'	  : isIframe        
	  		});
	  		return false;
	  	}		

/////////////////////////////////////////////////////////////////////
var isSoLanTDList_add_or_delete_action_happened = false;	// nếu mà có add hoac delete action xảy ra thì refresh lại page.
var currentSelectedSodkId = 0;
var isFirstTimeSoLanTD = true;

// show so lan trung don in table of don
function showSoLanTDList(sodkId) {
	if (isFirstTimeSoLanTD) {
		isFirstTimeSoLanTD = false;
		// make input box datepicker
		$( "#td_ngay_frm" ).datepicker({
			yearRange: '1900:2015',
			changeMonth: true,
			changeYear: true
		});
		$( "#td_ngay_frm" ).datepicker( "option", $.datepicker.regional[ "vi" ] );
		$( "#td_ngay_frm" ).datepicker( "option", "dateFormat", "dd/mm/yy");
		$( "#td_ngay_frm" ).datepicker( "option", "showOn", "button");
	}

	currentSelectedSodkId = sodkId;
	fetchSoLanTDtable(sodkId, doShowSoLanTDListAfterFetchData);
}

// do show dialog after fetching the data
function doShowSoLanTDListAfterFetchData() {
	// remove time out on open dialog
	clearRefreshTimeout();

	// bind shortcut keys
	bindKeyShortcutTDList();

	$('#light_td').show();
	$('#fade').show();
	var fullHeight = document.body.offsetHeight;
	$('#fade').css("height", fullHeight + "px");

	isSoLanTDList_add_or_delete_action_happened = false;
	$("#closeTDFormBtn").focus();
}

// fetch so lan trung don and do the callback action
function fetchSoLanTDtable(id, callbackAction)
{
	var tableTbody = $("div#td_content table#table > tbody");
	tableTbody.html("");
	doBlockUI();
	$.ajax({
		type: "GET",
		url: "fetchSoLanTDtable",
		dataType: "json",
		data: { id: id },
		success:
			function(json){
				var tdList = json;
				var html = "";
				if (tdList)
				{
					var i, count = 0;
					var td;
					for (i in tdList)
					{
						count++;
						td = tdList[i];
						var ngay = td.ngay == null ? "" : td.ngay;
						var ghiChu = td.ghiChu == null ? "" : td.ghiChu;
						var canbo = td.canBo == null ? "" : td.canBo;

						html += "<tr id='td_tr_" + td.id + "' title='" + td.ngay + "'>";
						html += '<td style="text-align:center;">' + (count < 10 ? "0" + count : count) + '</td>';
						html += '<td style="text-align:center;">' + ngay + '</td>';
						html += '<td style="text-align:left;"><b>' + ghiChu + '</b></td>';
						html += '<td style="text-align:left;">' + canbo + '</td>';
						html += '<td style="text-align:center;">';
							if (td.id > 0) {
								html += '<img title="Sửa" class="td_thao_tac_image_cursor" onclick="showEditTrungDonForm(' + "'"  + td.id + "'" + ')" src="images/edit.png" />';
								html += '&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;';
								html += '<img title="Xóa" class="td_thao_tac_image_cursor" onclick="deleteTrungDon(' + "'" + td.id + "'" + ')" src="images/cancel.png" />';
							}
							html += '<input type="hidden" id="sodk_id_' + td.id + '" value="' + td.sodk_id + '" />';
							html += '<input type="hidden" id="user_id_' + td.id + '" value="' + td.user_id + '" />';
							html += '<input type="hidden" id="table_id_' + td.id + '" value="' + td.table_id + '" />';
							html += '<input type="hidden" id="ngay_' + td.id + '" value="' + td.ngay + '" />';
							html += '<input type="hidden" id="ghiChu_' + td.id + '" value="' + td.ghiChu + '" />';
							html += '<input type="hidden" id="canBo_' + td.id + '" value="' + td.canBo + '" />';
						html += '</td>';
						html += '</tr>';
					}
				}

				tableTbody.html(html);
				doUnBlockUI();
//				doShowSoLanTDListAfterFetchData();
				if ($.isFunction(callbackAction)) callbackAction();
				return false;
			},
		error:
			function(){
				doUnBlockUI();
				alert_error("Có lỗi Server khi fetch danh sách các đơn trùng !!!");
				refresh_page();
				return false;
			}
	});
}



function bindKeyShortcutTDList() {
	bindKeyShortcutTDListForObject($(document));
}

function bindKeyShortcutTDListForObject(jquery_obj)
{
	jquery_obj.bind('keydown', 'Alt+1', fncThoatTDList);
	return false;
}

function unbindKeyShortcutTDList(jquery_obj) {
	jquery_obj.unbind('keydown', fncThoatTDList);
	return false;
}

function fncThoatTDList() {
	$("#closeTDFormBtn").trigger("click");
}

function hideSoLanTDList() {
	unbindKeyShortcutTDList($(document));
	$('#fade').hide();
	$('#light_td').hide();

	// add auto refresh timeout on close of dialog
	setRefreshTimeout();

	// refresh lại page nếu có delete action xảy ra
	if (isSoLanTDList_add_or_delete_action_happened)
		refresh_page();
}

// handle button lich su nop don action, man hinh sodk
function xem_lich_su_nop_don() {
	if ($("#selected_id").val() == "") {
		alert_error("Vui lòng chọn một đơn thư trước !!!");
		return false;
	}
	var id = $("#selected_id").val();
	showSoLanTDList(id);
}

// function handle shortcut keyboard
function fncXemLichSuNopDon(evt)
{
	if (isAtMainScreenNghiepVu()) {
		$("#nv_lich_su_nop_don_sodk").trigger("click");
		return false;
	}
}

function showAddTrungDonForm()
{
	var ngayNhap = formatDate(new Date(), "dd/MM/yyyy");
	$('#td_ngay_frm').val(ngayNhap);
	$("#td_can_bo_frm").val(currentUserFullName);

	showEditTrungDonDialog();
	// Hide message validate
	$('.ui-dialog-titlebar-close').click( function(){
		hideTrungDonForm();
	});

}

// format saved text data for displaying in text area: <br /> to new line \n\r
function formatSavedTextDataForDisplayInTextArea(text) {
	return text.replace(/<br \/>/g, '\n');
}

// format value in text area to text that will be saved into db, reserve new line \n\r
function formatValueInTextAreaToTextSavedToDB(value) {
	return value.replace(/\n\r?/g, '<br />');
}

// show edit trung don form
function showEditTrungDonForm(td_id) {
	var td_list_div = $('#light_td');
	$("#td_can_bo_frm").val(td_list_div.find('#canBo_' + td_id).eq(0).val());
	$("#td_id_frm").val(td_id);
	$("#td_ngay_frm").val(td_list_div.find('#ngay_' + td_id).eq(0).val());
	$("#td_ghi_chu_frm").val(formatSavedTextDataForDisplayInTextArea(td_list_div.find('#ghiChu_' + td_id).eq(0).val()));
	showEditTrungDonDialog();
}

function showEditTrungDonDialog()
{
	$("#trungDonFormDiv").dialog({
		title: "<img src='images/edit-add.png' style='vertical-align:middle;' />&nbsp;&nbsp;Thêm mới / Sửa trùng đơn ",
		autoOpen: true,
		modal: true,
		resizable : false,
		width: 480
	});

//	if ( $("#ui-datepicker-div").is(":visible"))
//		$.datepicker._hideDatepicker();

	setTimeout(function(){
			$("#td_ghi_chu_frm").focus();
		}
		, 10
	);
}

function hideTrungDonForm()
{
	resetTrungDonForm();

	$("#trungDonFormDiv").dialog({
		autoOpen: false,
		position: 'center'
	});
	$("#trungDonFormDiv").dialog("close");
}

function resetTrungDonForm()
{
	$('#td_can_bo_frm').val("");
	$('#td_id_frm').val("0");
	$('#td_ngay_frm').val("");
	$('#td_ghi_chu_frm').val("");
	return false;
}

function submitTrungDonForm() {
	var td_id_frm = $('#td_id_frm').val();
	var td_ngay_frm = $.trim($('#td_ngay_frm').val());
	if (td_ngay_frm == '') {
		alert_error_callback("Ngày nộp đơn không được bỏ trống", function() {
			$('#td_ngay_frm').focus();
		});
		return false;
	}
	var td_ghi_chu_frm = formatValueInTextAreaToTextSavedToDB($('#td_ghi_chu_frm').val());

	doBlockUI();
	$.ajax({
		type: "POST",
		url: "saveTrungDonForm",
		dataType: "json",
		data: { id: td_id_frm, sodkId: currentSelectedSodkId, ngay: td_ngay_frm, ghiChu: td_ghi_chu_frm },
		success:
			function(response) {
				doUnBlockUI();
				if ($.type(response)=='string' && response.indexOf("</pre>") != -1) {
					response = $(response).text();
				}
				try	{
					var result;
					if ($.type(response) == 'object')
						result = response;
					else
						result = $.parseJSON(response);
					// thành công
					if (result.status == true) {
						// nếu là thêm mới thì đánh dấu lại tí nữa tắt hộp thoại đi sẽ refresh page
						if (td_id_frm == 0) {
							isSoLanTDList_add_or_delete_action_happened = true;
						}

						// hiện thông báo thành công và ẩn form
						hideTrungDonForm();
						fetchSoLanTDtable(currentSelectedSodkId, function() {
							alert_info(result.message);
						});
					}
					// thất bại
					else {
						alert_error(result.message);
					}
				}
				catch (error) {
					refresh_page();
				}
				return false;
			},
		error:
			function(){
				doUnBlockUI();
				alert_error("Có lỗi Server khi lưu thông tin đơn trùng !!!");
				return false;
			}
	});

	return false;
}


function deleteTrungDon(td_id) {
	confirm_gosol(
		"Bạn có chắc muốn xóa trùng đơn này?",
		function(result) {
			if (result)	{
				$.ajax({
					type: "POST",
					url: "deleteTrungDonForm",
					dataType: "json",
					data: { id: td_id },
					success:
						function(response) {
							doUnBlockUI();
							if ($.type(response)=='string' && response.indexOf("</pre>") != -1) {
								response = $(response).text();
							}
							try	{
								var result;
								if ($.type(response) == 'object')
									result = response;
								else
									result = $.parseJSON(response);
								// thành công
								if (result.status == true) {
									// đánh dấu lại tí nữa tắt hộp thoại đi sẽ refresh page
									isSoLanTDList_add_or_delete_action_happened = true;
									// hiện thông báo thành công
									fetchSoLanTDtable(currentSelectedSodkId, function() {
										alert_info(result.message);
									});
								}
								// thất bại
								else {
									alert_error(result.message);
								}
							}
							catch (error) {
								refresh_page();
							}
							return false;
						},
					error:
						function(){
							doUnBlockUI();
							alert_error("Có lỗi kết nối Server khi xóa đơn trùng !!!");
							return false;
						}
				});
			}
			return false;
	});
	return false;
} // end deleteTrungDon




