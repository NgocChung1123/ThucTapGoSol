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