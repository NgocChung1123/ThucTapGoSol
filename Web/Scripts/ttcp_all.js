/**
 * @author trungnm
 */

////////////////////////////////////////////////////////////////////
// ttcp.js
////////////////////////////////////////////////////////////////////
function refresh_page()
{
	// loai bo nhung dau # o cuoi href
	var href = $.trim(window.location.href);
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
	
	// reload
	window.location.href = href;
}

function hide_light_box(){
	$('#fade').hide();
	$('#light').hide();
}

function show_light_box(label){
 	$('#light').show();
 	$('#fade').show();
 	$('#user').val('');
 	$('#fullname').val('');
 	$('#label_input').val('Thêm '+ label);
}

function edit_light_box(user, fullname, descript, label){
	$('#light').show();
	$('#fade').show();
	$('#user').val(user);
	$('#fullname').val(fullname);
	$('#password').hide();
	$('#re_password').hide();
	$('#button_save').val('Cập nhật');
	$('#label_input').val('Cập nhật thông tin '+ label);
}

function show_msg(url){
	if (confirm("Bạn chắc chắn sẽ xóa?")) {
		/*document.location = url;*/
	}
}

////////////////////////////////////////////////////////////////////
//ttcp_text_helper.js
////////////////////////////////////////////////////////////////////

if (! window.chuanHoaChuoi)
{
	// chuan hoa chuoi
	// src: "   ĐI  họC RẤT    LÀ VuI   NhỂ     "
	// result: "ĐI học RẤT LÀ VuI NhỂ"
	function chuanHoaChuoi(s)
	{
		s = $.trim(s);
		
		// loai bo khoang trong thua o giua cac tu
		var sArr = s.split(" ");
		s = "";
		for (i = 0; i < sArr.length; i++)
		{
			if ($.trim(sArr[i]) == "")
				continue;
			s = s + sArr[i] + " ";
		}
		s = $.trim(s);
		return s;
	}
}

if (! window.chuanHoaChuoiLowercase)
{
	// chuan hoa chuoi
	// src: "   ĐI  họC RẤT    LÀ VuI   NhỂ     "
	// result: "Đi học rất là vui nhể"
	function chuanHoaChuoiLowercase(s)
	{
		s = $.trim(s);
		
		// loai bo khoang trong thua o giua cac tu
		var sArr = s.split(" ");
		s = "";
		for (i = 0; i < sArr.length; i++)
		{
			if ($.trim(sArr[i]) == "")
				continue;
			s = s + sArr[i] + " ";
		}
		s = $.trim(s);
		// chuan hoa cac tu
		sArr = s.split(" ");
		var i = 0;
		s = "";
		for (i = 0; i < sArr.length; i++)
		{
			if (i==0)
			{
				var s1 = sArr[i].substr(0,1).toUpperCase();
				var s2 = sArr[i].substr(1,sArr[i].length-1).toLowerCase();
				sArr[i] = s1 + s2 + " ";
			}
			else
				sArr[i] = sArr[i].toLowerCase() + " ";			
			s = s + sArr[i];
		}
		return $.trim(s);
	}
}

if (! window.chuanHoaTen)
{
	// chuan hoa chuoi
	// src: "   ĐI  họC RẤT    LÀ VuI   NhỂ     "
	// result: "Đi Học Rất Là Vui Nhể"
	function chuanHoaTen(s)
	{
		s = $.trim(s);
		
		// loai bo khoang trong thua o giua cac tu
		var sArr = s.split(" ");
		s = "";
		for (i = 0; i < sArr.length; i++)
		{
			if ($.trim(sArr[i]) == "")
				continue;
			s = s + sArr[i] + " ";
		}
		s = $.trim(s);
		// chuan hoa cac tu
		sArr = s.split(" ");
		var i = 0;
		s = "";
		for (i = 0; i < sArr.length; i++)
		{
			var s1 = sArr[i].substr(0,1).toUpperCase();
			var s2 = sArr[i].substr(1,sArr[i].length-1).toLowerCase();
			sArr[i] = s1 + s2 + " ";
			s = s + sArr[i];
		}
		return $.trim(s);
	}
}

if (! window.loai_bo_dau_tieng_viet_cho_sort)
{
	//loại bỏ dấu tiếng việt cho việc sắp xếp theo quy tắc
	// a < à < ả < ã < á < ạ < ă < ... < â
	// e < ê
	// i		
	// o < ô < ơ		
	// u < ư
	// y
	// d < đ
	function loai_bo_dau_tieng_viet_cho_sort(s)
	{	
		//alert(s.indexOf("Ấ"));
		//console.log(s.indexOf("Ấ"));
		s = chuanHoaChuoi(s);
		s = s.toLowerCase();
		var src = new Array();
		var replace = new Array();
	
		src.push("đ"); replace.push("dw");
		
		src.push("à"); replace.push("awa2");
		src.push("ả"); replace.push("awa3");
		src.push("ã"); replace.push("awa4");
		src.push("á"); replace.push("awa5");
		src.push("ạ"); replace.push("awa6");
	
		src.push("ă"); replace.push("awc1");
		src.push("ằ"); replace.push("awc2");
		src.push("ẳ"); replace.push("awc3");
		src.push("ẵ"); replace.push("awc4");
		src.push("ắ"); replace.push("awc5");
		src.push("ặ"); replace.push("awc6");
		
		src.push("â"); replace.push("awd1");
		src.push("ầ"); replace.push("awd2");
		src.push("ẩ"); replace.push("awd3");
		src.push("ẫ"); replace.push("awd4");
		src.push("ấ"); replace.push("awd5");
		src.push("ậ"); replace.push("awd6");		
	
		src.push("è"); replace.push("ewa2");
		src.push("ẻ"); replace.push("ewa3");
		src.push("ẽ"); replace.push("ewa4");
		src.push("é"); replace.push("ewa5");
		src.push("ẹ"); replace.push("ewa6");
	
		src.push("ê"); replace.push("ewb1");
		src.push("ề"); replace.push("ewb2");
		src.push("ể"); replace.push("ewb3");
		src.push("ễ"); replace.push("ewb4");
		src.push("ế"); replace.push("ewb5");
		src.push("ệ"); replace.push("ewb6");
	
		src.push("ì"); replace.push("iwa2");
		src.push("ỉ"); replace.push("iwa3");
		src.push("ĩ"); replace.push("iwa4");
		src.push("í"); replace.push("iwa5");
		src.push("ị"); replace.push("iwa6");
	
		src.push("ò"); replace.push("owa2");
		src.push("ỏ"); replace.push("owa3");
		src.push("õ"); replace.push("owa4");
		src.push("ó"); replace.push("owa5");
		src.push("ọ"); replace.push("owa6");
	
		src.push("ô"); replace.push("owb1");
		src.push("ồ"); replace.push("owb2");
		src.push("ổ"); replace.push("owb3");
		src.push("ỗ"); replace.push("owb4");
		src.push("ố"); replace.push("owb5");
		src.push("ộ"); replace.push("owb6");
	
		src.push("ơ"); replace.push("owc1");
		src.push("ờ"); replace.push("owc2");
		src.push("ở"); replace.push("owc3");
		src.push("ỡ"); replace.push("owc4");
		src.push("ớ"); replace.push("owc5");
		src.push("ợ"); replace.push("owc6");
	
		src.push("ù"); replace.push("uwa2");
		src.push("ủ"); replace.push("uwa3");
		src.push("ũ"); replace.push("uwa4");
		src.push("ú"); replace.push("uwa5");
		src.push("ụ"); replace.push("uwa6");
	
		src.push("ư"); replace.push("uwb1");
		src.push("ừ"); replace.push("uwb2");
		src.push("ử"); replace.push("uwb3");
		src.push("ữ"); replace.push("uwb4");
		src.push("ứ"); replace.push("uwb5");
		src.push("ự"); replace.push("uwb6");
		
		src.push("ỳ"); replace.push("ywa2");
		src.push("ỷ"); replace.push("ywa3");
		src.push("ỹ"); replace.push("ywa4");
		src.push("ý"); replace.push("ywa5");
		src.push("ỵ"); replace.push("ywa6");
	
		var i = 0;
		for (i=0; i < src.length; i++)
		{
			s = s.replace(src[i],replace[i]);
		}
		return s;
	}
}

if (! window.so_sanh_chuoi)
{
	// so sánh 2 chuỗi tiếng việt với nhau 
	// tra ve 1 neu chuoi s1 > s2
	// tra ve -1 neu chuoi s1 <= s2
	function so_sanh_chuoi(s1, s2)
	{
		var s1_t = loai_bo_dau_tieng_viet_cho_sort(s1);
		var s2_t = loai_bo_dau_tieng_viet_cho_sort(s2);
		return s1_t > s2_t ? 1 : -1;
	}
}

if (! window.so_sanh_ten)
{
	// so sánh 2 ten tiếng việt với nhau
	// so sanh ten roi so sanh ho + ten dem
	// 	tra ve 1 neu ten s1 > s2
	// 	tra ve -1 neu ten s1 <= s2
	function so_sanh_ten(s1a, s2a)
	{
		var s1 = chuanHoaChuoi(s1a);
		s1 = s1.toLowerCase();
		var s2 = chuanHoaChuoi(s2a);
		s2 = s2.toLowerCase();
		
		if (s1 == "")
			return -1;
		if (s2 == "")
			return 1;
		
		var a1 = s1.split(" ");
		var a2 = s2.split(" ");
		
		var ten1 = a1[a1.length - 1];
		var ten2 = a2[a2.length - 1];
		
		if (ten1 != ten2)
		{
			return so_sanh_chuoi(ten1, ten2);
		}
		
		var i;
		var ho1 = "", ho2 = "";
		for (i = 0; i < a1.length-1; i++)
		{
			ho1 += a1[i] + " ";
		}
		for (i = 0; i < a2.length-1; i++)
		{
			ho2 += a2[i] + " ";
		}
		return so_sanh_chuoi(ho1, ho2);
		
	}
}

////////////////////////////////////////////////////////////////////
//ttcp_msgbox_helper.js
////////////////////////////////////////////////////////////////////
var is_submitted = 0;
function avoid_dupplicate_submit()
{
	  is_submitted++;
	  setTimeout(function(){ is_submitted = 0;}, 2000);
	  if (is_submitted > 1)
  	  return false;
	  return true;
}  

//tranh an phim tat nhieu qua, khi hien message box thi khong cho phep an phim tat
//type = 1: -> editForm
//type = 2: -> searchForm
var shortcutHit = 0;
function checkShortcutHit(type)
{
	shortcutHit ++;
	setTimeout(function(){ shortcutHit = 0;}, 1000);  	
	if (shortcutHit > 1)
		return false;
	if ( ! $("#fade").is(":visible"))
		return false;
	
	if (type == 1)
		if ( ! $("#light").is(":visible") )
			return false;
	if (type == 2)
		if ( ! $("#light_search").is(":visible") )
			return false;
	
	if (numberOfOpenedMsgbox > 0)
		return false;
	
	// print box
	// ui-widget-overlay
	
	return true;
}  


//check if an element is on top of current state, (z-index display), cannot applied for scroll div
function isDisplayOnTop(elem)
{
	var x1,y1;
	var offset = $(elem).offset();
	x1 = offset.left;
	y1 = offset.top;
	var v = false;
	var extra = 10;	// extra point to search for element
	
	// chrome fix
	var is_chrome = navigator.userAgent.toLowerCase().indexOf('chrome') > -1;
	if (is_chrome)
	{
		extra = 100;
	}
	
 for (var x = x1; x <= x1 + extra; x++) {
   for(var y = y1; y <= y1 + extra; y++) {
     if (document.elementFromPoint(x,y) == elem) {
       // item is visible
       v = true;
       break;
     }
   }
   if (v == true) {
     break;
   }
 }
 return v;
}  

//block all keyboards input 
function doBlockKeyboard()
{
	$("body").bind("keydown",blockKeyboard);	
}

function doUnBlockKeyboard()
{
	$("body").unbind("keydown",blockKeyboard);	
}

function blockKeyboard(e)
{
	e.preventDefault();
	return false;
}

var isBlockUI = false;
//blockUISolid
function doBlockUISolid()
{
	$.blockUI({
		overlayCSS:  {
			backgroundColor: 'black',
			opacity:	  	 1,
			cursor:		  	 'wait'
		}
	});
	doBlockKeyboard();
	isBlockUI = true;
	return false;
}  

//blockUILoad
function doBlockUILoad()
{
	$.blockUI({
		message: "",
		css : {
			backgroundColor : '#fff',
			opacity : 0.01,
			color : '#fff'
		},
		overlayCSS:  {
			backgroundColor: '#fff',
			opacity:	  	 0.01,
			cursor:		  	 'wait'
		}
	});
	doBlockKeyboard();
	isBlockUI = true;
	return false;
}


//blockUI
function doBlockUI()
{
	$.blockUI({
		css : {
			border : 'none',
			padding : '15px',
			backgroundColor : '#000',
			'-webkit-border-radius' : '10px',
			'-moz-border-radius' : '10px',
			opacity : .5,
			color : '#fff'
		}
	});
	doBlockKeyboard();
	isBlockUI = true;
	return false;
}

//unBlockUI
function doUnBlockUI()
{
	$.unblockUI();
	doUnBlockKeyboard();
	isBlockUI = false;
	return false;
}

//check if popup is blocked
function IsPopupBlocker() {
	var oWin = window.open("","testpopupblocker","width=10,height=10,top=-20,left=-20");
	if (oWin==null || typeof(oWin)=="undefined") {
		return true;
	} else {
		setTimeout(function(){
			oWin.close();
		}, 5000);
		return false;
	}
}

//hide message box created by MsgBox plugin
function hideMsgBox()
{
	$.MsgBoxObject.close();
}

//confirm dialog
function confirm_gosol(message, callback_function)
{
	// assign options
	$.msgbox({
		showDuration : 0,
		 closeDuration   : 0,
       moveDuration    : 0
	});
	// show box
	$.msgbox(
 		   	message, 
 		   	{
  		  type : 'confirm',
  		  buttons : [
  		             {type: 'submit', value:'Có'},
  		             {type: 'cancel', value:'Không'}
  		           	] 
      	},
      	callback_function
  );
}

//alert box
function alert_alert(message)
{
	// assign options
	$.msgbox({
		showDuration : 0,
		 closeDuration   : 0,
       moveDuration    : 0
	});
	// show box
	$.msgbox(
 		   	message, 
 		   	{
  		  type : 'alert',
  		  buttons : [
  		             {type: 'cancel', value:'Đóng'}
  		           	] 
      	}
  );
}

//info box
function alert_info(message)
{
	// assign options
	$.msgbox({
		showDuration : 0,
		 closeDuration   : 0,
       moveDuration    : 0
	});
	// show box
	$.msgbox(
 		   	message, 
 		   	{
  		  type : 'info',
  		  buttons : [
  		             {type: 'cancel', value:'Đóng'}
  		           	] 
      	}
  );
}

//info box with call back
function alert_info_callback(message, callback)
{
	// assign options
	$.msgbox({
		showDuration : 0,
		 closeDuration   : 0,
       moveDuration    : 0
	});
	// show box
	$.msgbox(
			message, 
 		   	{
  		  type : 'info',
  		  buttons : [
  		             {type: 'cancel', value:'Đóng'}
  		           	] 
      	},
      	callback
  );
}

//error box
function alert_error(message)
{
	// assign options
	$.msgbox({
		showDuration : 0,
		 closeDuration   : 0,
       moveDuration    : 0
	});
	// show box
	$.msgbox(
 		   	message, 
 		   	{
  		  type : 'error',
  		  buttons : [
  		             {type: 'cancel', value:'Đóng'}
  		           	] 
      	}
  );
}

//error box with call back
function alert_error_callback(message, callback)
{
	// assign options
	$.msgbox({
		showDuration : 0,
		 closeDuration   : 0,
       moveDuration    : 0
	});
	// show box
	$.msgbox(
 		   	message, 
 		   	{
  		  type : 'error',
  		  buttons : [
  		             {type: 'submit', value:'Đóng'}
  		           	] 
      	},
      	callback
  );
}

//show alert Jquery ui
function confirm_show(message, data)
{
	$( "#dialog:ui-dialog" ).dialog( "destroy" );
	$( "span#delete-form-message" ).html( message );
	
	var default_option = {
			resizable: false,
			width:300,
			height: 170,
			left: '50%',
            top:200,
			modal: true,
			position: 'center',
			buttons: {
				"KHÔNG": function() {
				    $(this).dialog("close");
				    $('.ui-dialog').hide();
				    $("#fade").hide();
				},
			"CÓ": function() {
			    $(this).dialog("close");
			    $('.ui-dialog').hide();
			    $("#fade").hide();
				}
			}
	};

	 jQuery.extend(true, default_option, data);
	$( "#delete-form" ).dialog(default_option);
}

//show alert message Jquery ui
function alert_message(message, data) {
	$( "#dialog:ui-dialog" ).dialog( "destroy" );
	$( "span#message-form-message" ).html( message );

	var default_option = {
	    modal: true,
	    width: 350,
	    buttons: {
	        "Đóng": function () {
	            $(this).dialog("close");
	        }
	    },
	    open: function (event, ui) {
	        setTimeout(function () { $("#message-form").dialog('close'); }, 1500);
	    }
	};
	
	jQuery.extend(true, default_option, data);
	$( "#message-form" ).dialog(default_option);
	
}

////////////////////////////////////////////////////////////////////
//ttcp_date_helper.js
////////////////////////////////////////////////////////////////////

var MONTH_NAMES=new Array('January','February','March','April','May','June','July','August','September','October','November','December','Jan','Feb','Mar','Apr','May','Jun','Jul','Aug','Sep','Oct','Nov','Dec');
var DAY_NAMES=new Array('Sunday','Monday','Tuesday','Wednesday','Thursday','Friday','Saturday','Sun','Mon','Tue','Wed','Thu','Fri','Sat');
function LZ(x) {
	return ( x<0 || x>9 ? "" : "0" ) + x;
}

// ------------------------------------------------------------------
// isDate ( date_string, format_string )
// Returns true if date string matches format of format string and
// is a valid date. Else returns false.
// It is recommended that you trim whitespace around the value before
// passing it to this function, as whitespace is NOT ignored!
// ------------------------------------------------------------------
function isDate(val,format) {
	var date=getDateFromFormat(val,format);
	if (date==0) { return false; }
	return true;
	}

// -------------------------------------------------------------------
// compareDates(date1,date1format,date2,date2format)
//   Compare two date strings to see which is greater.
//   Returns:
//   1 if date1 is greater than date2
//   0 if date2 is greater than date1 of if they are the same
//  -1 if either of the dates is in an invalid format
// -------------------------------------------------------------------
function compareDates(date1,dateformat1,date2,dateformat2) {
	var d1=getDateFromFormat(date1,dateformat1);
	var d2=getDateFromFormat(date2,dateformat2);
	if (d1==0 || d2==0) {
		return -1;
		}
	else if (d1 > d2) {
		return 1;
		}
	return 0;
	}

// ------------------------------------------------------------------
// formatDate (date_object, format)
// Returns a date in the output format specified.
// The format string uses the same abbreviations as in getDateFromFormat()
// ------------------------------------------------------------------
function formatDate(date,format) {
	format=format+"";
	var result="";
	var i_format=0;
	var c="";
	var token="";
	var y=date.getYear()+"";
	var M=date.getMonth()+1;
	var d=date.getDate();
	var E=date.getDay();
	var H=date.getHours();
	var m=date.getMinutes();
	var s=date.getSeconds();
	var yyyy,yy,MMM,MM,dd,hh,h,mm,ss,ampm,HH,H,KK,K,kk,k;
	// Convert real date parts into formatted versions
	var value=new Object();
	if (y.length < 4) {y=""+(y-0+1900);}
	value["y"]=""+y;
	value["yyyy"]=y;
	value["yy"]=y.substring(2,4);
	value["M"]=M;
	value["MM"]=LZ(M);
	value["MMM"]=MONTH_NAMES[M-1];
	value["NNN"]=MONTH_NAMES[M+11];
	value["d"]=d;
	value["dd"]=LZ(d);
	value["E"]=DAY_NAMES[E+7];
	value["EE"]=DAY_NAMES[E];
	value["H"]=H;
	value["HH"]=LZ(H);
	if (H==0){value["h"]=12;}
	else if (H>12){value["h"]=H-12;}
	else {value["h"]=H;}
	value["hh"]=LZ(value["h"]);
	if (H>11){value["K"]=H-12;} else {value["K"]=H;}
	value["k"]=H+1;
	value["KK"]=LZ(value["K"]);
	value["kk"]=LZ(value["k"]);
	if (H > 11) { value["a"]="PM"; }
	else { value["a"]="AM"; }
	value["m"]=m;
	value["mm"]=LZ(m);
	value["s"]=s;
	value["ss"]=LZ(s);
	while (i_format < format.length) {
		c=format.charAt(i_format);
		token="";
		while ((format.charAt(i_format)==c) && (i_format < format.length)) {
			token += format.charAt(i_format++);
			}
		if (value[token] != null) { result=result + value[token]; }
		else { result=result + token; }
		}
	return result;
	}
	
// ------------------------------------------------------------------
// Utility functions for parsing in getDateFromFormat()
// ------------------------------------------------------------------
function _isInteger(val) {
	var digits="1234567890";
	for (var i=0; i < val.length; i++) {
		if (digits.indexOf(val.charAt(i))==-1) { return false; }
		}
	return true;
	}
function _getInt(str,i,minlength,maxlength) {
	for (var x=maxlength; x>=minlength; x--) {
		var token=str.substring(i,i+x);
		if (token.length < minlength) { return null; }
		if (_isInteger(token)) { return token; }
		}
	return null;
	}
	
// ------------------------------------------------------------------
// getDateFromFormat( date_string , format_string )
//
// This function takes a date string and a format string. It matches
// If the date string matches the format string, it returns the 
// getTime() of the date. If it does not match, it returns 0.
// ------------------------------------------------------------------
function getDateFromFormat(val,format) {
	val=val+"";
	format=format+"";
	var i_val=0;
	var i_format=0;
	var c="";
	var token="";
	var token2="";
	var x,y;
	var now=new Date();
	var year=now.getYear();
	var month=now.getMonth()+1;
	var date=1;
	var hh=now.getHours();
	var mm=now.getMinutes();
	var ss=now.getSeconds();
	var ampm="";
	
	while (i_format < format.length) {
		// Get next token from format string
		c=format.charAt(i_format);
		token="";
		while ((format.charAt(i_format)==c) && (i_format < format.length)) {
			token += format.charAt(i_format++);
			}
		// Extract contents of value based on format token
		if (token=="yyyy" || token=="yy" || token=="y") {
			if (token=="yyyy") { x=4;y=4; }
			if (token=="yy")   { x=2;y=2; }
			if (token=="y")    { x=2;y=4; }
			year=_getInt(val,i_val,x,y);
			if (year==null) { return 0; }
			i_val += year.length;
			if (year.length==2) {
				if (year > 70) { year=1900+(year-0); }
				else { year=2000+(year-0); }
				}
			}
		else if (token=="MMM"||token=="NNN"){
			month=0;
			for (var i=0; i<MONTH_NAMES.length; i++) {
				var month_name=MONTH_NAMES[i];
				if (val.substring(i_val,i_val+month_name.length).toLowerCase()==month_name.toLowerCase()) {
					if (token=="MMM"||(token=="NNN"&&i>11)) {
						month=i+1;
						if (month>12) { month -= 12; }
						i_val += month_name.length;
						break;
						}
					}
				}
			if ((month < 1)||(month>12)){return 0;}
			}
		else if (token=="EE"||token=="E"){
			for (var i=0; i<DAY_NAMES.length; i++) {
				var day_name=DAY_NAMES[i];
				if (val.substring(i_val,i_val+day_name.length).toLowerCase()==day_name.toLowerCase()) {
					i_val += day_name.length;
					break;
					}
				}
			}
		else if (token=="MM"||token=="M") {
			month=_getInt(val,i_val,token.length,2);
			if(month==null||(month<1)||(month>12)){return 0;}
			i_val+=month.length;}
		else if (token=="dd"||token=="d") {
			date=_getInt(val,i_val,token.length,2);
			if(date==null||(date<1)||(date>31)){return 0;}
			i_val+=date.length;}
		else if (token=="hh"||token=="h") {
			hh=_getInt(val,i_val,token.length,2);
			if(hh==null||(hh<1)||(hh>12)){return 0;}
			i_val+=hh.length;}
		else if (token=="HH"||token=="H") {
			hh=_getInt(val,i_val,token.length,2);
			if(hh==null||(hh<0)||(hh>23)){return 0;}
			i_val+=hh.length;}
		else if (token=="KK"||token=="K") {
			hh=_getInt(val,i_val,token.length,2);
			if(hh==null||(hh<0)||(hh>11)){return 0;}
			i_val+=hh.length;}
		else if (token=="kk"||token=="k") {
			hh=_getInt(val,i_val,token.length,2);
			if(hh==null||(hh<1)||(hh>24)){return 0;}
			i_val+=hh.length;hh--;}
		else if (token=="mm"||token=="m") {
			mm=_getInt(val,i_val,token.length,2);
			if(mm==null||(mm<0)||(mm>59)){return 0;}
			i_val+=mm.length;}
		else if (token=="ss"||token=="s") {
			ss=_getInt(val,i_val,token.length,2);
			if(ss==null||(ss<0)||(ss>59)){return 0;}
			i_val+=ss.length;}
		else if (token=="a") {
			if (val.substring(i_val,i_val+2).toLowerCase()=="am") {ampm="AM";}
			else if (val.substring(i_val,i_val+2).toLowerCase()=="pm") {ampm="PM";}
			else {return 0;}
			i_val+=2;}
		else {
			if (val.substring(i_val,i_val+token.length)!=token) {return 0;}
			else {i_val+=token.length;}
			}
		}
	// If there are any trailing characters left in the value, it doesn't match
	if (i_val != val.length) { return 0; }
	// Is date valid for month?
	if (month==2) {
		// Check for leap year
		if ( ( (year%4==0)&&(year%100 != 0) ) || (year%400==0) ) { // leap year
			if (date > 29){ return 0; }
			}
		else { if (date > 28) { return 0; } }
		}
	if ((month==4)||(month==6)||(month==9)||(month==11)) {
		if (date > 30) { return 0; }
		}
	// Correct hours value
	if (hh<12 && ampm=="PM") { hh=hh-0+12; }
	else if (hh>11 && ampm=="AM") { hh-=12; }
	var newdate=new Date(year,month-1,date,hh,mm,ss);
	return newdate.getTime();
	}

// ------------------------------------------------------------------
// parseDate( date_string [, prefer_euro_format] )
//
// This function takes a date string and tries to match it to a
// number of possible date formats to get the value. It will try to
// match against the following international formats, in this order:
// y-M-d   MMM d, y   MMM d,y   y-MMM-d   d-MMM-y  MMM d
// M/d/y   M-d-y      M.d.y     MMM-d     M/d      M-d
// d/M/y   d-M-y      d.M.y     d-MMM     d/M      d-M
// A second argument may be passed to instruct the method to search
// for formats like d/M/y (european format) before M/d/y (American).
// Returns a Date object or null if no patterns match.
// ------------------------------------------------------------------
function parseDate(val) {
	var preferEuro=(arguments.length==2)?arguments[1]:false;
	generalFormats=new Array('y-M-d','MMM d, y','MMM d,y','y-MMM-d','d-MMM-y','MMM d');
	monthFirst=new Array('M/d/y','M-d-y','M.d.y','MMM-d','M/d','M-d');
	dateFirst =new Array('d/M/y','d-M-y','d.M.y','d-MMM','d/M','d-M');
	var checkList=new Array('generalFormats',preferEuro?'dateFirst':'monthFirst',preferEuro?'monthFirst':'dateFirst');
	var d=null;
	for (var i=0; i<checkList.length; i++) {
		var l=window[checkList[i]];
		for (var j=0; j<l.length; j++) {
			d=getDateFromFormat(val,l[j]);
			if (d!=0) { return new Date(d); }
			}
		}
	return null;
	}
