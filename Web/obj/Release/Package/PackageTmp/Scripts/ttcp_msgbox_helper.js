/**
 * @author trungnm
 */

// avoid dupplicate form submits
	var is_submitted = 0;
  function avoid_dupplicate_submit()
  {
	  is_submitted++;
	  setTimeout(function(){ is_submitted = 0;}, 2000);
	  if (is_submitted > 1)
    	  return false;
	  return true;
  }  

// tranh an phim tat nhieu qua, khi hien message box thi khong cho phep an phim tat
// type = 1: -> editForm
// type = 2: -> searchForm
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
  
// block all keyboards input 
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
  
// blockUILoad
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
  
  
// blockUI
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

// unBlockUI
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

// hide message box created by MsgBox plugin
function hideMsgBox()
{
	$.MsgBoxObject.close();
}

// confirm dialog
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

// alert box
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

// info box
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

// error box
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

// error box with call back
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

// show alert Jquery ui
function confirm_show(message, data)
{
	$( "#dialog:ui-dialog" ).dialog( "destroy" );
	$( "span#delete-form-message" ).html( message );
	
	var default_option = {
			resizable: false,
			width:350,
			height:170,
			modal: true,
			position: 'center',
			buttons: {
				"KHÔNG": function() {
					$( this ).dialog( "close" );
				},
			"CÓ": function() {
					$( this ).dialog( "close" );
				}
			}
	};
	
	jQuery.extend(true, default_option, data);
	$( "#delete-form" ).dialog(default_option);
}

//show alert message Jquery ui
function alert_message(message, data)
{
	$( "#dialog:ui-dialog" ).dialog( "destroy" );
	$( "span#message-form-message" ).html( message );
	
	var default_option = {
			modal: true,
			width:350,
			buttons: {
				"Đóng": function() {
					$( this ).dialog( "close" );
				}
			}
	};
	
	jQuery.extend(true, default_option, data);
	$( "#message-form" ).dialog(default_option);
	
}