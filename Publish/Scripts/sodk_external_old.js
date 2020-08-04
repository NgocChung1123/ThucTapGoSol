/**
 * @author trungnm
 */


////////////////////////////////////////////////////////////////////////////////////////////	  	
////////////////////////////////////////////////////////////////////////////////////////////
// PHAN MOVE TU sodkList.jsp SANG
////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////

	//// --- 1.UFD
		var ufd_select = "";
		setTimeout(function(){
			if ($("#editForm").length > 0)
			{
				bindTabAndShiftTabForForms();
			}
			else
			{
				setTimeout(function(){
					bindTabAndShiftTabForForms();
				},2000);
			}
		}, 2000);

		// bind tab, shift+tab
		function bindTabAndShiftTabForForms()
		{
			// stimulate tab index for search form quick (#searchForm_quick)
			simulateTabIndexSearchform_quick();

			// simulate shift+tab for search form quick
			simulateShiftTabIndexSearchform_quick();

			// stimulate tab index for search form (#searchForm)
			simulateTabIndexSearchform();

			// simulate shift+tab for search form
			simulateShiftTabIndexSearchform();
			
			// stimulate tab index for edit form
			simulateTabIndex();

			// simulate shift+tab for search form
			simulateShiftTabIndex();
		}

		// init ufd selects
		$(document).ready(function(){

			// bind enter keys for all input in form
			//bindEnterKeyForForm("editForm");
			//bindEnterKeyForForm("searchForm");						
			
			// select searchable
			$("#editForm select, #searchForm select, #searchForm_quick select").live("focus",function(){

				var selected_select = $(this);
				var temp_id = $(this).attr("id");

				// dong tat ca cac select khac truoc khi 
				if (ufd_select != "" && ufd_select != temp_id)
				{
					$("#" + ufd_select).ufd("destroy");
				}
				
				// default options
				var ufdOptions = {
					skin:"sexy",
					addEmphasis: true,
					allowLR: true,
					listWidthFixed: false,
					zIndexPopup: 20000
				};

				// not update on up or down arrow
				if ( temp_id == "tinhThanhPhosId" || temp_id == "sl_quanHuyens" || temp_id == "sl_tinhThanhPhosId_2"
						|| temp_id == "sl_quanHuyens2" || temp_id == "sl_thamQuyenSearch" )
				{
					ufdOptions['updateMasterOnMove'] = false;
				}

				// search form
				if ( temp_id == "sl_loaiDonSearch" || temp_id == "sl_loaiKnTcsSearch" || temp_id == "tinhThanhPhosIdSearch"
						|| temp_id == "sl_quanHuyensSearch" || temp_id == "sl_loaiDon" || temp_id == "sl_loaiKnTcs" 
						|| temp_id == "sl_thamQuyen"	)
				{
					ufdOptions['updateMasterOnMove'] = false;
				}

				// search form quick
				if ( temp_id == "tinhThanhPhosIdSearch_quick"	|| temp_id == "sl_quanHuyensSearch_quick" )
				{
					ufdOptions['updateMasterOnMove'] = false;
				}

				// fix bug chrome, focus vao mot cai khac truoc khi tao ufd select
				var is_chrome = navigator.userAgent.toLowerCase().indexOf('chrome') > -1;
				if (is_chrome)
				{					
					if ($("#id_sodkSo").is(":visible"))
					{
						if ($("#id_sodkSo").is(":enabled"))
							$("#id_sodkSo").focus();
						else
							$("#saveAndCloseBtn").focus();
					}
					else if ($("#txt_from_date").is(":visible"))
						$("#ta_theoDoiVanBanDiSearch").focus();
					else if ($("#txt_hoTenSearch_quick").is(":visible"))
						$("#txt_hoTenSearch_quick").focus();

					$("#" + temp_id).ufd(ufdOptions);
				}
				else
					$(this).ufd(ufdOptions);
				
				ufd_select = temp_id;
				// focus on input field
				if (temp_id)
				{
					// focus vao input field cua select
					var input_field = $("#ufd-" + temp_id);
					bindKeyShortcutForObject(input_field);	// bind default form shortcut for select input
					input_field.focus();	// focus to that input
					
					// tab key pressed -> move to specific input
					input_field.bind('keydown', function(e) {
						// tab
						var keyCode;
						if (e.shiftKey != 1)
						{ 
						  keyCode = e.keyCode || e.which;
						  if (keyCode == 9) { 
						     	e.preventDefault();
						     	selected_select.ufd("destroy");
						     	setTimeout(function(){
						     		focusOnNextTab(temp_id); 	
							    },30);						     	
						  }
						}
						// shift + tab
						else
						{
							  keyCode = e.keyCode || e.which;
							  if (keyCode == 9) { 
							     	e.preventDefault();
							     	selected_select.ufd("destroy");
							     	setTimeout(function(){
							     		focusOnPreviousTab(temp_id); 	
								    },30);						     	
							  }
						}
					});
				}
			});

			// restore original select on blur
			$("#editForm select, #searchForm select, #searchForm_quick select").bind("blur", function(){
				$(this).ufd("destroy");
				ufd_select = "";
			});
		});
	//// +++ end 1 

	//// --- 2.Autocomplete		
		$(document).ready(function(){
			// auto suggestion search name in search form #searchForm
	 		var searchV = $("#txt_hoTenSearch").autocomplete({ 
	 		    serviceUrl:'nameSearchSuggestionSodk',
	 		    minChars:3,
	 		    maxHeight:400,
	 		    width:300,
	 		    zIndex: 9999,
	 		    deferRequestBy: 300 //miliseconds
	 		    // callback function:
	 		    //onSelect: function(value, data){ alert('You selected: ' + value + ', ' + data); },
	 		  });

			// auto suggestion search name in search form quick #searchForm_quick
			var searchV = $("#txt_hoTenSearch_quick").autocomplete({
				serviceUrl:'nameSearchSuggestionSodk',
				minChars:3,
				maxHeight:400,
				width:300,
				zIndex: 9999,
				deferRequestBy: 300 //miliseconds
			});
 		});	
	//// +++ end 2

	//// --- 3.make the page auto reload after specific period of time
		var refreshTimeout = null;
	  	$(document).ready(function(){  	  	
	  	  	setRefreshTimeout();
	   	});

	  	function clearRefreshTimeout()
	  	{
	  	  	if (refreshTimeout)
	  			clearTimeout(refreshTimeout);
	  	}
		
	   	function setRefreshTimeout()
	   	{   		
	  	  refreshTimeout = setTimeout(function(){
											   		var a = window.location.href;
											   		if (a.charAt(a.length-1) == '#')
												   		a = a.substring(0, a.length-1);
											   		window.location.href = a;
											   	},timeOfAutoRefresh
							);
	   	}
	//// +++ end 3
		
	//// --- 4.dong ho cat luu chuyen khi lam gi do
	   	$(document).ready(function(){
			$(document).delegate("#pagingDiv a, #pagingDiv_top a", "click", function() {
				  doBlockUI();
			});
		});
	//// +++ end 4
		
	//// --- 5.Condense
	   	$(document).ready(function(){
	   		$("div.needCondense").condense({
	   			 condensedLength: 350,
	   			 minTrail: 40,
	   			 moreText: '  [<u>H</u>iện tất]',
	             lessText: '  [T<u>h</u>u nhỏ]',
	             ellipsis: "..."
	    	});
   		});
	//// +++ end 5
		
	//// --- 6.select first row on ready, bind key shortcuts
	   	var isTableHasData = false;
		$(document).ready(function(){
			setTimeout(function(){
				var trs = $("div.dashboard #table > tbody > tr");
				isTableHasData = trs.length > 0;
				if (isTableHasData)
					trs.first().trigger("click");

				// left, right arrow for buttons
				bindKeyShortcutLeftRightButtons();
				
				// key shortcut man hinh chinh nghiep vu
				bindKeyShortcutNghiepVu();
				
				// bind up, down arrow for table on focus
				bindKeyShortcutTableUpdownArrow();

				// left, right arrow:
				bindKeyShortCutTableLeftRightArrow();

			},300);
		});
	//// +++ end 6

	//// --- 7.make a row in table selectable
		$(document).ready(function(){
			$("div.dashboard #table tr").each(function(index, e){
				if (index > 0)
				{
					var tr = $(this);
					tr.bind("click", function(){
						var tr = $(this);
						// neu dong nay chua duoc chon
						if ( ! tr.hasClass("selected_hl"))
						{
							// bo chon tat ca cac dong
							$("div.dashboard #table tr").each(function(i){
								$(this).removeClass("selected_hl");
							});
		
							// chon dong nay
							tr.addClass("selected_hl");
		
							// doc id
							//alert(tr.find("input.for_id").val());
							$("#selected_id").val(tr.find("input.for_id").val());
							$("#selected_id").trigger("change");

							// hien thi head icons
							$(".head_icon_hide").removeClass("head_icon_hide");
						}
						// neu duoc chon roi thi bo chon
						//else
						//{
						//	tr.removeClass("selected_hl");
						//	$("#selected_id").val("");
						//	$("#selected_id").trigger("change");
						//}
					});

					// double click to edit
					tr.bind("dblclick", function(){
						var tr = $(this);
						// neu dong nay chua duoc chon
						if ( ! tr.hasClass("selected_hl"))
						{
							// bo chon tat ca cac dong
							$("div.dashboard #table tr").each(function(i){
								$(this).removeClass("selected_hl");
							});
		
							// chon dong nay
							tr.addClass("selected_hl");
		
							// doc id
							//alert(tr.find("input.for_id").val());
							$("#selected_id").val(tr.find("input.for_id").val());
							$("#selected_id").trigger("change");

							// hien thi head icons
							$(".head_icon_hide").removeClass("head_icon_hide");
						}

						var s_id = $("#selected_id").val();
						if (s_id != "" && s_id != "0")
						{
							$("#nv_edit_sodk").trigger("click");
						}
					});
				}
			});
		});
	//// +++ end 7
   	
	//// --- 8.init javascript
		var itv_stopped = false; // is stopped checked for changes
	    var trungDon_focus_object = '';
		$(document).ready(function(){

			// edit form, chuan hoa ten onblur cac field ho ten
			$("#ndHoTen, #ndHoTen1").bind("blur", function(){
				if($.trim($(this).val()) != "")
				{
					var t = chuanHoaTen($(this).val());
					$(this).val(t);
				}
			});

			// changes -> enable btnKT		
			$("#ndHoTen, #ndHoTen1, #co_quan_btc, #tinhThanhPhosId, #sl_quanHuyens, #sl_xaPhuongs").bind("focus",function(){

				trungDon_focus_object = this;	// object de track trung don
				
				// check changes for every 10ms
				var element = $(this);
				var old_value = ($.trim(element.val() + '')).toLowerCase();
				var td = findParentSpecificTag(element, "td");
				var btnKt = $(td).next().find("button");
				
				itv_stopped = false; // start track for changes						
				function checkDifferent()
				{
					var new_value = ($.trim(element.val() + '')).toLowerCase();
					// enable button if it is disable 
					if (new_value != old_value)
					{
						old_value = new_value;
						if ( ! btnKt.hasClass("save"))
						{	
							enableABtnKT(btnKt);	// enable
							itv_stopped = true;
						}
					}

					// 
					if ( ! itv_stopped )
					{
						setTimeout(function(){
							checkDifferent();
						}, 20);
					}
				}

				// khong phai edit mode -> enable btnKT onchange
				if ($("#isEditMode").val() != "1")
					checkDifferent();					
			});

			// blur -> disable interval
			$("#ndHoTen, #ndHoTen1, #co_quan_btc, #tinhThanhPhosId, #sl_quanHuyens, #sl_xaPhuongs").bind("blur",function(){
				//alert(itv);
				//clearInterval(itv);
				//clearTimeout(itv);
				itv_stopped = true;
				trungDon_focus_object = '';
			});
			
			// reset edit form on start up
			reset_edit_form();
			
			// input type = text focus -> select
			$("input[type='text']").focus(function(){
				$(this).select();
			});
			
			//change light area height to the form height when change tab
			$( "div#tabs" ).bind( "tabsshow", function(event, ui) {
				// hide validationEngine error
				if ($("#ndHoTenformError").length > 0)
					if ($("#ndHoTenformError").first().is(":visible"))
						$("#ndHoTenformError").css("display","none");
				
				// hide ufd select
				if (ufd_select != "")
					$("#" + ufd_select).ufd("destroy");
				// hide validation error promt
				$('#editForm').validationEngine('hide');
				//reset form height
				resetEditFormHeight();
	  			scroll(0,0);
			});
			
			// bind escape key to close the form
			$(document).bind('keydown', 'esc', function (evt) {
				if ($("#light").css("display") != "none")
				{
					if ( ! $($(".jquery-msgbox")[0]).is(":visible") )
					{
						if (numberOfOpenedMsgbox < 1)	// print dialog may be opened
							hide_light_box_edit();
					}
					else
					{
						hideMsgBox();
					}
					
				}

				if ($("#light_search").css("display") != "none")
					hide_search_form(); 
			});

			$("#searchForm").validationEngine({
				scroll: false
			});

			$("#searchForm_quick").validationEngine({
				scroll: false
			});
			
			$("#editForm").validationEngine({
				scroll: false
			});
			
			//Ngày nhập đơn
			$( "#txt_ngay" ).datepicker({
				yearRange: '1900:2015',
				changeMonth: true,
				changeYear: true
			});
			$( "#txt_ngay" ).datepicker( "option", $.datepicker.regional[ "vi" ] );
			$( "#txt_ngay" ).datepicker( "option", "dateFormat", "dd/mm/yy");	

			//Ngày quá hạn
			$( "#txt_ngayQuaHan" ).datepicker({
				yearRange: '1900:2015',
				changeMonth: true,
				changeYear: true
			});
			$( "#txt_ngayQuaHan" ).datepicker( "option", $.datepicker.regional[ "vi" ] );
			$( "#txt_ngayQuaHan" ).datepicker( "option", "dateFormat", "dd/mm/yy");	

			//Ngày theo dõi văn bản đến
			$( "#txt_ngayTheoDoi" ).datepicker({
				yearRange: '1900:2015',
				changeMonth: true,
				changeYear: true
			});
			$( "#txt_ngayTheoDoi" ).datepicker( "option", $.datepicker.regional[ "vi" ] );
			$( "#txt_ngayTheoDoi" ).datepicker( "option", "dateFormat", "dd/mm/yy");	


			//TÌM KIẾM ĐƠN THƯ KHIẾU NẠI, TỐ CÁO
			
			//----------- Ngày nhập đơn--------------
			//Từ ngày
			$( "#txt_from_date" ).datepicker({
				yearRange: '2000:2015',
				changeMonth: true,
				changeYear: true
			});
			$( "#txt_from_date" ).datepicker( "option", $.datepicker.regional[ "vi" ] );
			$( "#txt_from_date" ).datepicker( "option", "dateFormat", "dd/mm/yy");	
			//Đến ngày
			$( "#txt_to_date" ).datepicker({
				yearRange: '2000:2015',
				changeMonth: true,
				changeYear: true
			});
			$( "#txt_to_date" ).datepicker( "option", $.datepicker.regional[ "vi" ] );
			$( "#txt_to_date" ).datepicker( "option", "dateFormat", "dd/mm/yy");

			//---------Ngày theo dõi văn bản -----------

			$( "#txt_from_date_vb" ).datepicker({
				yearRange: '2000:2015',
				changeMonth: true,
				changeYear: true
			});
			$( "#txt_from_date_vb" ).datepicker( "option", $.datepicker.regional[ "vi" ] );
			$( "#txt_from_date_vb" ).datepicker( "option", "dateFormat", "dd/mm/yy");	
			//Đến ngày
			$( "#txt_to_date_vb" ).datepicker({
				yearRange: '2000:2015',
				changeMonth: true,
				changeYear: true
			});
			$( "#txt_to_date_vb" ).datepicker( "option", $.datepicker.regional[ "vi" ] );
			$( "#txt_to_date_vb" ).datepicker( "option", "dateFormat", "dd/mm/yy");
		});
	//// +++ end 8
   	
	//// --- 9.filescan stubs
		$(document).ready(function(){
			//Ngày nhập đơn
			$( "#ngayScan_frm" ).datepicker({
				yearRange: '2000:2015',
				changeMonth: true,
				changeYear: true
			});
			$( "#ngayScan_frm" ).datepicker( "option", $.datepicker.regional[ "vi" ] );
			$( "#ngayScan_frm" ).datepicker( "option", "dateFormat", "dd/mm/yy");	

			//	init nhanvien select
//			initNhanVienSelect();
		});
	//// +++ end 9

///////////////////////////////////////// main edit box	///////////////////////////////////	
	//// --- 10a.nguon don
		$(document).ready(function(){
  			//Ngày chuyen don
  			$( "#nguonDon_NgayChuyenDon" ).datepicker({
  				yearRange: '2000:2015',
  				changeMonth: true,
  				changeYear: true
  			});
  			$( "#nguonDon_NgayChuyenDon" ).datepicker( "option", $.datepicker.regional[ "vi" ] );
  			$( "#nguonDon_NgayChuyenDon" ).datepicker( "option", "dateFormat", "dd/mm/yy");	
	  	});

  		function sl_nguonDon_onChange_1()
  		{
  			$("#nguonDon_SoCongVan").val("");
	  		//$("#nguonDon_CoQuan").val("");
	  		$("#nguonDon_NgayChuyenDon").val("");
	  		$("#editForm td.nguonDon").css("display","none");
	  		var cl = "nguonDon_" + 1;
	  		$("#editForm td." + cl).show(10);
	  		return false;
	  	}
	  	
  		function sl_nguonDon_onChange(value)
  		{
  			$("#txt_coQuanCD").val("");
  			$("#txt_maCoQuanCD").val("");
	  		if (value == '1' || value == 1)
		  	{
	  			sl_nguonDon_onChange_1();
			}
	  		else
		  	{
	  			sl_nguonDon_onChange_1();
	  			setTimeout(function(){
	  				$("#nguonDon_SoCongVan").val("");
			  		//$("#nguonDon_CoQuan").val("");
			  		$("#nguonDon_NgayChuyenDon").val("");
			  		$("#editForm td.nguonDon").css("display","none");
			  		var cl = "nguonDon_" + value;
			  		$("#editForm td." + cl).show(10);

			  		if (value == '2' || value == 2)
				  	{
			  			$("#sl_nguonDonCQ").trigger("change");
					}
				  		
			  		if (value == '4' || value == 4)
				  	{
					  	var t1 = $("#fetch_sl_nguonDonChucVu").val();
					  	if (t1 != "")
						{
					  		$('#sl_nguonDonChucVu').val(t1);
					  		$("#fetch_sl_nguonDonChucVu").val("");
						}
			  			$('#sl_nguonDonChucVu').trigger('change');
					}

			  		if (value == '5' || value == 5)
				  	{
			  			$("#sl_nguonDonCucVu").trigger("change");
					}
					
					setTimeout(function(){
						// chrome work around
			  			var is_chrome = navigator.userAgent.toLowerCase().indexOf('chrome') > -1;
					  	if (is_chrome)
						{
					  		$("#rowNguonDonNew1").css("display","block");
					  		setTimeout(function(){
					  			$("#rowNguonDonNew1").css("display","");	
						  	},10);
					  		
					  		$("#rowNguonDonNew2").css("display","block");
					  		setTimeout(function(){
					  			$("#rowNguonDonNew2").css("display","");	
						  	},10);
						}
		  			}, 110);

					var is_chrome = navigator.userAgent.toLowerCase().indexOf('chrome') > -1;
				  	if (is_chrome)
						setTimeout(function(){ resetEditFormHeight(); }, 300);
				  	else
				  		//setTimeout(function(){ resetEditFormHeight(); }, 100);
				  		resetEditFormHeight();
		  		},100);
		  	}

	  		return false;
	  	}
	//// +++ end 10a
   	
	//// --- 10b.Người KN, TC tab
  		function change_type_obj(i){
			$("#txt_soNguoi").val("1");
			$("#txt_caNhanCq").val("");
			//Set value for sodk_canhan
			$("#txt_caNhan").val($('input:radio[name=hs_type]:checked').val());
			//
			$('#so_nguoi').hide();
			$('#ten_co_quan').hide();
			$('#label_cn').css('font-weight','bold');
			$('#label_cq').removeClass('bold');
			$('#label_tt').removeClass('bold');
			if(i==0){
				$('#label_cn').css('font-weight','normal');
				$('#label_cq').removeClass('bold');
				$('#so_nguoi').show(500);
				$('#label_tt').addClass('bold');
			}
			if(i==2){
				$('#label_cn').css('font-weight','normal');
				$('#label_tt').removeClass('bold');
				$('#ten_co_quan').show(500);
				$('#label_cq').addClass('bold');
			}

			//setTimeout(function(){ resetEditFormHeight(); }, 100);
			resetEditFormHeight();
		}
	//// +++ end 10b
  		
	//// --- 10c.Đối tượng KN, TC tab
  		function change_type_obj2(i){
			//Reset
			$("#ndHoTen1").val("");
			$("#co_quan_btc").val("");
			$("#ndDanToc1").val('-1');
			$("#thanhPhan1").val('-1');
			$("#chucVu1").val('-1');
			$("#cap").val('-1');
			$("#quoctich1").val('-1');
			
			$("#sl_tinhThanhPhosId_2").val("-1");
			$("#txt_tinhThanh1").val("");
			$("#sl_quanHuyens2").html("<option value='-1'>Chọn quận, huyện</option>");
			$("sl_quanHuyens2").val("-1");
			$("#txt_quanHuyen1").val("");
			$("#sl_xaPhuongs2").html("<option value='-1'>Chọn xã, phường</option>");
			$("#sl_xaPhuongs2").val("-1");
			$("#txt_xaPhuong1").val("");

			$("select#ndDanToc1 option")
			   .each(function() { this.selected = (this.text == 'Kinh'); });
			$("select#quoctich1 option")
			   .each(function() { this.selected = (this.text == 'Việt Nam'); });
			
			$("#coQuanCt1").val("");
			///
			$("#txt_doiTuong1").val($('input:radio[name=hsdt_type]:checked').val());
			
			$('#ten_dtcq').css("display", "none");
			$('#tr_blank_ten_dtcq').css("display", "none");
			$('.tr_dtcq').css("display", "");
			
			$('#label_dtcn').css('font-weight','bold');
			$('#label_dtcq').removeClass('bold');
			if(i==2){
				$('#label_dtcn').css('font-weight','normal');
				$('#ten_dtcq').css("display", "");
				$('#tr_blank_ten_dtcq').css("display", "");
				$('#label_dtcq').addClass('bold');
				$('.tr_dtcq').css("display", "none");
			}

			resetEditFormHeight();
		}
	//// +++ end 10c
///////////////////////////////////////// END main edit box	///////////////////////////////////  		
  		
	//// --- 11.Search form
  		$(document).ready(function(){
  			//Ngày chuyen don
  			$( "#txt_ngayChuyenDonSearch" ).datepicker({
  				yearRange: '2000:2015',
  				changeMonth: true,
  				changeYear: true
  			});
  			$( "#txt_ngayChuyenDonSearch" ).datepicker( "option", $.datepicker.regional[ "vi" ] );
  			$( "#txt_ngayChuyenDonSearch" ).datepicker( "option", "dateFormat", "dd/mm/yy");	
	  	});
	  	
  		function sl_nguonDonDenSearch_onChange(value)
  		{
  			$("#sl_nguonDonChucVuSearch").val("-1");
  			$("#txt_SoCongVanSearch").val("");
  			$("#sl_nguonDonCQSearch").val("-1");
  			$("#sl_nguonDonDongChiSearch").val("-1");
  			$("#sl_nguonDonCucVuSearch").val("-1");
  			$("#txt_ngayChuyenDonSearch").val("");
  			
  			
	  		$("#searchForm .nguonDon").css("display","none");
	  		if (value != '-1')
		  	{
	  			var cl = "nguonDon_" + value;
	  			//$("#searchForm ." + cl).show(10);
	  			$("#searchForm ." + cl).css("display","table-cell");
	  			if (value == '4')
		  		{
	  				$('#sl_nguonDonDongChiSearch').html("");
	  				$('#sl_nguonDonDongChiSearch').append(
	  						$('<option></option>').val('-1').html('Chọn đồng chí chuyển đơn')
	  				);
			  	}
			  	
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
	//// +++ end 11	
  		
  	//// --- 12.tooltip STT search area
  		$(document).ready(function(){
			$("#table th.for_tooltip").tooltip({
				tip: '#stt_tooltip',
				effect: 'slide',
				offset: [10,20],
				events: {
					  def: "dblclick,mouseout"
				},
				onShow: function(){
					$("#txt_search_stt").select();
					$("#txt_search_stt").focus();
				},
				onHide: function(){
					resetSearchStt();
				}
			});
			resetSearchStt();

			// neu search theo stt thi make text blinking
			if (oldSearchStt != "")
			{
				makeTextBlinking();
			}
		});
  	//// +++ end 12
  		
  	//// --- 13.script end section
  		function delete_record(){

  	    	if ($("#selected_id").val() == "")
  			{
  				alert_error("Vui lòng chọn một đơn thư trước !!!");
  				return false;
  			}
  			var id = $("#selected_id").val();
  	        
  	    	confirm_show(
  	    	    	"Bạn có chắc muốn xóa lựa chọn này?",
  	        	    {
  	        			buttons: {
  	        				"KHÔNG": function() {
  	        					$( this ).dialog( "close" );
  	        					// put focus back to document, must do with an input        					
  	        					setTimeout(function(){ $("#search_mode").focus(); $("#search_mode").blur();},200);
  	        				},					
  	    					"CÓ": function() {
  	    						doBlockUI();
  	    						window.location.href = baseUrl + "/deleteSodk?id="+escape(id);
  	    					}
  	    				}
  	    	    	}
  	        	);

  	    	// focus on the button "không"
  	    	setTimeout(function(){
  	        	var active_dialog = $("body > div.ui-dialog:visible");
  	        	var btns = active_dialog.find("button");
  	        	for (var i = 0; i < btns.length; i++)
  	            {
  	                console.log($(btns.get(i)).find("span").first().text().toLowerCase());
  	                if ($(btns.get(i)).find("span").first().text().toLowerCase() == "không")
  	                {
  	                	$(btns.get(i)).focus();
  	                	break;
  	                }
  	            }
  	        	return false;
  	        },200);
  	        
  			return false;
  	    }


  	    // btnKT onlick => log clicked btn
  	    var btnKT_clicked;
  	    var is_mouse_used = false;        
  	    $(document).ready(function() {
  	        // track xem nut nao duoc an
  			$("button.btnKT").bind("click",function(event){
  				btnKT_clicked = this;
  			});
  			// co phai dung chuot de click hay la ban phim
  			$("button.btnKT").bind("mousedown",function(event){
  				is_mouse_used = true;
  			});
  		});
  	    
  	    // kiem tra trung don
  	    // tab 1: 
  	    // tab 2: type: 1 = ca nhan, 2 = to chuc
  	    function person_create(tab, type)
  	    {        
  	    	var ndHoTen = "", co_quan_btc = "", ndHoTen1 = "";
//  	    	var tinhThanhPhosId = "0", sl_xaPhuongs = "0", sl_quanHuyens = "0";
  	    	var tinhThanhPhosId = "", sl_xaPhuongs = "", sl_quanHuyens = "";
  	    	
  	     	if (tab == 1)
  	        {   
  				ndHoTen = $.trim($("#ndHoTen").val());
  				if ( ndHoTen == "")
  				{
  					alert_error_callback(
  						"Xin vui lòng nhập tên người KNTC trước khi kiểm tra trùng đơn !!!",
  						function(){
  							$("#ndHoTen").focus();
  						}
  					);
  					return false;
  				}
  				
  				var temp = $("#tinhThanhPhosId").val();
  				if (temp != "" && temp != "-1" && temp != "0")
  					tinhThanhPhosId = $("#tinhThanhPhosId option:selected").first().text();
  				
  				temp = $("#sl_quanHuyens").val();
  				if (temp != "" && temp != "-1" && temp != "0")
  					sl_quanHuyens = $("#sl_quanHuyens option:selected").first().text();
  				
  				temp = $("#sl_xaPhuongs").val();
  				if (temp != "" && temp != "-1" && temp != "0")
  					sl_xaPhuongs = $("#sl_xaPhuongs option:selected").first().text();
  					
//  				tinhThanhPhosId = $("#tinhThanhPhosId").val();
//  				sl_quanHuyens = $("#sl_quanHuyens").val();
//  				sl_xaPhuongs = $("#sl_xaPhuongs").val();
  	        }

  	     	if (tab == 2)
  	        {
  				if (type == 1)
  				{
  					ndHoTen1 = $.trim($("#ndHoTen1").val());
  					if ( ndHoTen1 == "")
  					{
  						alert_error_callback(
  							"Xin vui lòng nhập tên đối tượng bị KNTC trước khi kiểm tra trùng đơn !!!",
  							function(){
  								$("#ndHoTen1").focus();
  							}
  						);
  						return false;
  					}
  				}
  				else
  				{
  					co_quan_btc = $.trim($("#co_quan_btc").val());
  					if ( co_quan_btc == "")
  					{
  						alert_error_callback(
  							"Xin vui lòng nhập tên cơ quan, tổ chức bị KNTC trước khi kiểm tra trùng đơn !!!",
  							function(){
  								$("#co_quan_btc").focus();
  							}
  						);
  						return false;
  					}
  				}
  	        }
  	        
  	        var url = 'trungDon';
  	    	var callBack = null;
  	    	callBack = open_windown_td;
  	    	doBlockUI();
  	    	$.ajax({
  	    		type : "POST",
  	    		url : url,
  	    		dataType : "json",
  	    		data : {
  	    			ndHoTen : ndHoTen,
  	    			tinhThanhPhosId : tinhThanhPhosId,
  	    			sl_quanHuyens : sl_quanHuyens,
  	    			sl_xaPhuongs : sl_xaPhuongs,
  	    			co_quan_btc : co_quan_btc,
  	    			ndHoTen1 : ndHoTen1
  	    		},
  	    		success : callBack,
  	    		error: function(){        		
  	        		doUnBlockUI();
  	        		refresh_page();
  	        		alert_error("Có lỗi server khi kiểm tra trùng đơn !!!");
  	        	}
  	    	});
  	    	return false;
  	        
  	    }

  	    function open_windown_td(response){        
  			if ( ! response)
  			{
  				alert_error("Có lỗi trả về từ server");			
  				doUnBlockUI();
  				return false;
  			}
  			
  			if (response.status)
  			{
  				$(btnKT_clicked).focus();
  				
  				// open new window to show trung_dons			
  				var url = "trungDonShow";
  				var fullUrl =location.protocol + '//' + location.host + baseUrl + "/" + url;
  				var windowWidth = Math.round(document.body.offsetWidth);
  				var myWindow = window.open(fullUrl, "_blank", "width=" + windowWidth + ",height=720,scrollbars=yes");
  				if (myWindow==null || typeof(myWindow)=="undefined") 
  				{
  					doUnBlockUI();
  					alert_error("Xin vui lòng tắt chế độ chặn popup !!!");				
  					return false;
  				}
  				else
  				{			
  					myWindow.focus();
  				}			

  				//doUnBlockUI();				
  				return false;
  			}
  			else
  			{			
  				// khong co trung don nao thi disable btnKT
  				if (response.message == "none")
  				{
  					doUnBlockUI();
  					
  					//  va focus vao truong nhap truoc do neu Alt + K				
  					if (is_td_shortcut_key_hit)
  					{
  						is_td_shortcut_key_hit = false;
  						var td = findParentSpecificTag(btnKT_clicked, "td").prev();
  						var inp = td.find("input[type='text'], select");
  						inp.first().focus();
  						inp.first().trigger("focus");
  					}
  					else
  					{
  						// neu dung ban phim thi chuyen sang field tiep theo
  						if ( ! is_mouse_used)
  						{
  							// trigger tab
  							var e = jQuery.Event("keydown");
  							e.which = 9; // # Some key code value
  							$(btnKT_clicked).trigger(e);
  						}
  						is_mouse_used = false;
  					}
  					// disable
  					disalbeABtnKT(btnKT_clicked);
  				}
  				else
  				{
  					if (response.message == "login")
  					{
  						refresh_page();
  						return;
  					}
  					alert_error(response.message);
  				}
  			}
  			return false;	
  	    }

  	    // make excel file
  		function excel(){
  			doBlockUI();
  	        var url = 'excel';
  	    	var callBack = null;
  	    	callBack = open_windown_excel;
  	    	$.ajax({
  	    		type : "POST",
  	    		url : url,
  	    		dataType : "json",
  	    		success : callBack,
  	    		error: function(){
  	        		doUnBlockUI();
  	        	}
  	    	});
  	    	return false;
  		}
  		
  		function open_windown_excel(response){
  			setTimeout(function(){ doUnBlockUI(); }, 1000);
  			if ( ! response)
  			{
  				alert_error("Có lỗi trả về từ server");
  				return false;
  			}
  			
  			if (response.status)
  			{			
  				var url = "excelShow";
  								
  				var fullUrl =location.protocol + '//' + location.host + baseUrl + "/" + url;
  				var windowWidth = Math.round(document.body.offsetWidth);
  				var full_width = $(window).width();
  				var full_height = $(window).height();
  				
  				var myWindow = window.open(fullUrl, "_blank", "width=" + full_width + ",height=" + full_height);
  				if (myWindow==null || typeof(myWindow)=="undefined") 
  				{
  					alert_error("Xin vui lòng tắt chế độ chặn popup !!!");
  					return false;
  				}
  				else
  				{			
  					myWindow.focus();
  				}
  								
  				return false;
  			}
  			else
  			{
  				alert_error(response.message);
  			}
  			return false;	
  		}
  		
  	    function status_change(){
  	    	if($('form #cb_status').is(':checked')){
  	    		$("#txt_status").val('1');
  			}else{
  				$("#txt_status").val('0');
  			}
  	    }

  	    function search_mode_onChange(){
  			if($("#search_mode").val() == '1')
  				show_tat_ca();
  			else if($("#search_mode").val() == '2')
  				show_dang_xu_ly();
  			else if($("#search_mode").val() == '3')
  				show_da_xu_ly();
  	    }

		function search_mode_top_onChange(){
  			if($("#search_mode_top").val() == '1')
  				show_tat_ca();
  			else if($("#search_mode_top").val() == '2')
  				show_dang_xu_ly();
  			else if($("#search_mode_top").val() == '3')
  				show_da_xu_ly();
  	    }

  	//// +++ end 13
  		
  	//// --- 14. fetch paging area when the page is ready
  	// fetch paging area when the page is ready
		$(document).ready(function(){
			$.ajax({
				type : "POST",
				url : "fetchPagingDivSodk",
				dataType : "json",
				success : function(json){
					if (json)
					{
						var paging = json;
						var i;
						$("#pagingDiv").html("");
						var html = "";
						for ( i in paging )
						{
							html += paging[i];
						}
						$("#pagingDiv").html(html);

						// also fill the top pagination div
						$("#pagingDiv_top").html(html);
					}
					else
					{
						// alert_error("Có lỗi server khi lấy thông tin phân trang !!!");	
					}
					return false;
				},
				error: function(){
					//alert_error("Có lỗi server khi lấy thông tin phân trang !!!");
					return false;
				}
			});
		});
  	//// +++ end 14
  	    
  	//// --- 0.		
  	//// +++ end 0
  	    
  	//// --- 0.		
  	//// +++ end 0
