<%@ Page Title="Danh mục Địa chỉ" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="DiaChi.aspx.cs" Inherits="Com.Gosol.CMS.Web.DiaChi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <title>Danh mục Địa giới hành chính</title>

    <!-- Load for DiaDanh -->
    <!-- edit box styles -->
    <style type="text/css">
        #editForm th.lblText
        {
            text-align: right;
            padding-right: 10px;
            vertical-align: top;
            padding-top: 10px;
        }
        #editForm th.lblSelect
        {
            text-align: right;
            padding-right: 10px;
            vertical-align: top;
            padding-top: 10px;
        }
        #editForm select.selectFrm
        {
            width: 300px;
            margin-top: 5px;
            margin-left: 3px;
            margin-bottom: 5px;
        }
        #editForm td.required_span
        {
            padding-left: 5px;
            vertical-align: top;
            padding-top: 10px;
        }
        #editForm .hide_field
        {
            display: none;
        }
    </style>
    <!-- styles to make the tree line bigger and wrappable -->
    <style type="text/css">
        #treeview li
        {
            min-height: 25px;
            line-height: 25px;
        }
        #treeview a
        {
            max-width: 96% !important;
            white-space: normal !important;
            height: auto;
            padding: 1px 2px;
            line-height: 23px;
        }
        #treeview a ins
        {
            height: 23px;
        }
        #treeview li > ins
        {
            vertical-align: top;
        }
        #treeview .jstree-hovered, #treeview .jstree-clicked
        {
            border: 0;
        }
    </style>
    <!-- jstree included javascript files -->
    <script type="text/javascript" src="scripts/jquery.jstree.js"></script>
    <script type="text/javascript" language="Javascript" src="scripts/ttcp_jstree_diadanh.js"></script>
    <!-- autocomplete -->
    <!--<link rel="stylesheet" type="text/css" media="all" href="styles/jquery.autocomplete.ajax.styles.css" />
    <script type="text/javascript" src="scripts/jquery.autocomplete.ajax.js"></script>
    <script type="text/javascript" language="Javascript">
        $(document).ready(function () {
            var searchV = $("#search_text").autocomplete({
                serviceUrl: 'DiaChiSuggestion.ashx',
                minChars: 2,
                maxHeight: 400,
                //width:300,
                zIndex: 9999,
                deferRequestBy: 300 //miliseconds
                // callback function:
                //onSelect: function(value, data){ alert('You selected: ' + value + ', ' + data); },
            });
        });
    </script>-->
    <!-- do stuff on startup -->
    <script type="text/javascript" language="Javascript">
        $(document).ready(function () {

            // assign min height to the dashboard on load
            var sidebarHeight = 0 + "px";
            $("div.dashboard").css("min-height", sidebarHeight);


            // init tree view
            var jstree_data = {
                tree_id: "#treeview",
                theme: "diadanh",
                theme_path: "scripts/jstree_themes/themes/",
                show_edit_box: true,
                search_url: "DiaChiSearch.ashx",
                list_url: "DiaChiGetData.ashx",
                add_url: "Handler1.ashx",
                edit_url: "DiaChiEdit.ashx",
                move_url: "moveNodeDiaDanh",
                delete_url: "DiaChiDelete.ashx",
                level_creatable: 2

            };

            init_tree(jstree_data);

            // bind escape key to the form
            if (jstree_data.show_edit_box) {
                bindEscapeKey();
            }

            // populate select fields
            populateSelectFields();

            // assign default behavior
            $("#cap_frm").bind("change", function () {
                assign_fullName_value();
            });

            $("#name_frm").bind("keyup", function () {
                assign_fullName_value();
            });
            $(".ui-icon ui-icon-closethick").click(function () {

                hideAddEditForm();
            });
        });
    </script>
    <!-- some init action -->
    <script type="text/javascript" language="Javascript">

        function populateSelectFields() {
            $("#level_frm").val("1");
            populate_cap_select_box(1);
        }

        function assign_fullName_value() {
            var cap_value = $("#cap_frm").val();
            var parentName = $("#parentName_frm").val();
            if (cap_value == "khác") {
                var fullName = "";
                if (parentName == "")
                    fullName = $.trim($("#name_frm").val());
                else
                    fullName = $.trim($("#name_frm").val()) + " - " + $("#parentName_frm").val();
                $("#fullName_frm").val(fullName);
            }
            else {
                var fullName = "";
                if (parentName == "")
                    fullName = $("#cap_frm").val() + " " + $.trim($("#name_frm").val());
                else
                    fullName = $("#cap_frm").val() + " " + $.trim($("#name_frm").val()) + " - " + $("#parentName_frm").val();
                $("#fullName_frm").val(fullName);
            }
        }

        function populate_cap_select_box(level) {
            $("#cap_frm").html("");
            var html;
            if (level == 1 || level == "1") {
                html = "<option value='khác'>Khác</option>"
					+ "<option value='tỉnh'>Tỉnh</option>"
					+ "<option value='thành phố'>Thành phố</option>";
                $("#cap_frm").html(html);
            }

            if (level == 2 || level == "2") {
                html = "<option value='khác'>Khác</option>"
					+ "<option value='quận'>Quận</option>"
					+ "<option value='huyện'>Huyện</option>"
					+ "<option value='thị xã'>Thị xã</option>"
					+ "<option value='thành phố'>Thành phố</option>";
                $("#cap_frm").html(html);
            }

            if (level == 3 || level == "3") {
                html = "<option value='khác'>Khác</option>"
					+ "<option value='xã'>Xã</option>"
					+ "<option value='phường'>Phường</option>"
					+ "<option value='thị trấn'>Thị trấn</option>";
                $("#cap_frm").html(html);
            }

            $("#cap_frm")[0].selectedIndex = 1;
        }
    </script>
    <!-- form add/edit rule -->
    <script type="text/javascript" language="Javascript">
        var checkAvailableUrl = "DiaChiCheckAvailable.ashx"; 	// url check available
        var fetchEditDetailUrl = "DiaChiEdit.ashx"; 	// url fetch edit details
        var saveDbUrl = "DiaChiSaveData.ashx"; 	// url fetch edit details

        // bind escape key to form;
        function bindEscapeKey() {
            $(document).bind('keydown', 'esc', function (evt) { hideAddEditForm(); });
            $("#name_frm").bind('keydown', 'esc', function (evt) { hideAddEditForm(); });
            $("#code_frm").bind('keydown', 'esc', function (evt) { hideAddEditForm(); });
        }

        // must have for all page
        function initValidationRule(field, rules, i, options) {
            // config ajax rule for a field
            if (field.attr("id") == "name_frm" || field.attr("id") == "code_frm") {
                var rule = options.allrules[rules[i + 1]];
                rule.url = checkAvailableUrl;
                //rule.extraData = "";
                //rule.level = 1;
                rule.extraDataDynamic = "#id_frm,#parentId_frm,#level_frm";
                //rule.alertTextOk = "Có thể dùng";
                //rule.alertText = "* Đã tổn tại thành phần xã hội này";
                //rule.alertTextLoad = "* đang kiểm tra dữ liệu, xin chờ giây lát....";
            }
            return false;
        }

        // final validate step (for ajax field)
        var submitted = 0;
        function final_validate() {
//            if ($("#editForm").validationEngine('validate')) {
//                if (!($("#name_frm").attr("isValid") == "true")) {
//                    if ($.trim($("#name_frm").val()) == "")
//                    //alert_error("Tên không được bỏ trống");
//                        alert_message("Tên không được bỏ trống");
//                    else
//                    //alert_error("Tên này đã tồn tại!!! xin vui lòng nhập lại");
//                        alert_message("Tên này đã tồn tại!!! xin vui lòng nhập lại");
//                    focus_editable_field("name_frm");
//                    return false;
//                }

//                submitted++;
//                if (submitted > 1)
//                    return false;
//                setTimeout(function () { submitted = 0; }, 3000);

//                return true;
//            }
//            else
            //                return false;
          return  true;
        }
        function chuanHoaChuoi(s) {
            s = $.trim(s);

            // loai bo khoang trong thua o giua cac tu
            var sArr = s.split(" ");
            s = "";
            for (var i = 0; i < sArr.length; i++) {
                if ($.trim(sArr[i]) == "")
                    continue;
                s = s + sArr[i] + " ";
            }
            s = $.trim(s);
            return s;
        }
        function confirm_show(message, data) {
            $("#dialog:ui-dialog").dialog("destroy");
            $("span#delete-form-message").html(message);

            var default_option = {
                resizable: false,
                width: 300,
                height: 170,
                left: '50%',
                top: 200,
                modal: true,
                position: 'center',
                buttons: {
                    "KHÔNG": function () {
                        $(this).dialog("close");
                        $('.ui-dialog').hide();
                        $("#fade").hide();
                    },
                    "CÓ": function () {
                        $(this).dialog("close");
                        $('.ui-dialog').hide();
                        $("#fade").hide();
                    }
                }
            };

            jQuery.extend(true, default_option, data);
            $("#delete-form").dialog(default_option);
        }
        // sau khi final validate thi se gui ajax de cap nhap du lieu trong db
        function handleSubmitAction() {
//            var ok = final_validate();
//            // if form is invalid then not submit
//            if (!ok)
//                return false;
            // if form is valid then do ajax save to database and close the form
            $.ajax({
                type: "POST",
                url: saveDbUrl,
                dataType: "json",
                data:
					   {
					       id: $("#id_frm").val(),
					       name: chuanHoaChuoi($("#name_frm").val()),
					       parentId: $("#parentId_frm").val(),
					       level: $("#level_frm").val(),
					       fullName: $("#fullName_frm").val(),
					       typeValue: $("#cap_frm").val()
					   },
                success:
					   function (json) {
					       hideAddEditForm();
					       if (json.status) {
					           //setTimeout(function(){ alert_info("Thành công"); }, 300);
					           //					           setTimeout(function () { alert_message("Thành công"); }, 300);

					           $("#MainContent_lblContentErr").html("");
					           $("#MainContent_lblContentSuccess").html("Cập nhật dữ liệu thành công!");
					           showthongBaoSuccess();
					           var parentId = json.parentId;
					           if (parentId) {
					               $(tree_id).jstree("refresh", $("#node_" + parentId));
					               setTimeout(function () {
					                   if (!$("#node_" + parentId).hasClass("jstree-open"))
					                       $("#node_" + parentId).trigger("dblclick");
					               }, 500);
					           }
					           else
					               $(tree_id).jstree("refresh", -1);
					       }
					       else {
					           //					           alert_error(json.message);
					           $("#MainContent_lblContentErr").html("Cập nhật dữ liệu thất bại.");
					           $("#MainContent_lblContentSuccess").html("");
					           showthongBaoError();
					       }
					       return false;
					   },
                error:
					   	function () {
					   	    hideAddEditForm();
					   	    alert_error("Có lỗi Server !!!");
					   	    $(tree_id).jstree("refresh", -1);
					   	    return false;
					   	}
            });
            return false;
        }

        function doHandleSubmitAction() {
            $("#cap_frm").focus();
            setTimeout(function () { handleSubmitAction(); }, 1400);
            return false;
        }

        // bind validation Engine to the form
        function bindValidationEngine() {
            $("#editForm").validationEngine({
                validationEventTrigger: "blur",
                scroll: false
            });
            return false;
        }

        // detach validation engine
        function detachValidationEngine() {
            $("#editForm").validationEngine('hideAll');
            $("#editForm").validationEngine('detach');
            return false;
        }

        // set fade layer to fullscreen
        function setFullScreenFade() {
            var fullHeight = document.body.offsetHeight;
            $('#fade').css('height', fullHeight + 'px');
        }

        // show edit box
        function show_light_box_edit() {            
            setFullScreenFade();
            //showEditDialog();
            showDiaChiForm();
            bindValidationEngine();            
            return false;
        }

        // hide edit box
        function hide_light_box_edit() {
            detachValidationEngine();
            reset_edit_form();
            $("#editForm").dialog({
                autoOpen: false,
                position: 'center'
            });
            $("#editForm").dialog("close");
            return false;
        }

        // set focus to first editable field in the form
        function focus_first_editable_field() {
            focus_editable_field("name_frm");
            return false;
        }

        // set focus to editable field in the form by given field id
        function focus_editable_field(field_id) {
            $("#" + field_id).focus();
            $("#" + field_id).select();
            return false;
        }

        // reset edit form
        function reset_edit_form() {
            $("#id_show").val("");
            $("#id_frm").val("");

            $("#name_frm").val("");
            $("#name_frm").attr("isValid", "");

            $("#parentId_frm").val("");
            $("#parentName_frm").val("");

            $("#fullName_frm").val("");

            $("#level_frm").val("1");
            populate_cap_select_box(1);

            return false;
        }


        // fetch parent to the form
        function fetchParentAddForm(parentId, level) {
            $.ajax({
                type: "POST",
                url: fetchEditDetailUrl,
                dataType: "json",
                data:
					   {
					       id: parentId, level:level
					   },
                success: fetchParentSuccess,
                error: fetchParentError
            });
            return false;
        }

        function fetchParentSuccess(json) {
            if (json) {
                var parent = json;
                $("#parentId_frm").val(parent.id);
                $("#parentName_frm").val(parent.fullName);
                var level = parent.level + 1;
                $("#level_frm").val(level);
                populate_cap_select_box(level);

                showAddForm();
            }
            else {
                alert_error("Node cha đã bị xóa khỏi db, refresh lại trang...");
                refreshTree();
                focusTree();
            }
            return false;
        }

        function fetchParentError() {
            alert_error("Có lỗi server !!!");
            hideAddEditForm();
            focusTree();
            return false;
        }


        // show add form
        function showAddForm() {
            show_light_box_edit();

            focus_first_editable_field();
            $('.ui-dialog-titlebar-close').click(function () {
                hideAddEditForm();
            });
            return false;
        }

        // fetch details of selected item to edit box
        function showEditForm(id, level) {
            $.ajax({
                type: "POST",
                url: fetchEditDetailUrl,
                dataType: "json",
                data:
					   {
					       id: id, level: level
					   },
                success: fetchSuccess,
                error: fetchError
            });
            return false;
        }

        function fetchSuccess(json) {
            //this is the json return data
            if (json) {
                var diaDanh = json;
                $("#id_show").val(diaDanh.id);
                $("#id_frm").val(diaDanh.id);
                $("#name_frm").val(diaDanh.name);
                $("#parentId_frm").val(diaDanh.parentId);
                $("#parentName_frm").val(diaDanh.parentName);
                $("#level_frm").val(diaDanh.level);
                $("#fullName_frm").val(diaDanh.fullName);

                populate_cap_select_box(diaDanh.level);
                $("#cap_frm").val(diaDanh.cap);

                show_light_box_edit();
                focus_editable_field("name_frm");
                focus_editable_field("cap_frm");
                focus_editable_field("fullName_frm");
            }
            else {
                alert_error("Không tìm thấy địa danh đó!!!");
                hideAddEditForm();
                refreshTree();
                focusTree();
            }
            return false;
        }

        function fetchError() {
            alert_error("Có lỗi server !!!");
            hideAddEditForm();
            focusTree();
            return false;
        }

        function showEditDialog() {            
            var startX, startY, stopX, stopY, moveX, moveY;
            $("#editForm").dialog({
                title: "<img src='images/edit-add.png' style='float:left;' /><font face='arial'> &nbsp; Thêm mới/ Sửa thông tin địa giới hành chính",
                autoOpen: true,
                modal: true,
                resizable: false,
                height: auto,
                width: 500,
                dragStart: function (event, ui) {
                    var parentDiv = $("#editForm").parent();
                    startX = parentDiv[0].offsetLeft;
                    startY = parentDiv[0].offsetTop;
                },

                dragStop: function (event, ui) {
                    var parentDiv = $("#editForm").parent();
                    stopX = parentDiv[0].offsetLeft;
                    stopY = parentDiv[0].offsetTop;
                    moveX = stopX - startX;
                    moveY = stopY - startY;
                    $(".formErrorContent").each(function (index) {
                        var parentError = this.offsetParent;
                        var parentTopS = $(parentError).css("top");
                        var parentTop = parseInt(parentTopS.replace("px", ""));
                        var parentLeftS = $(parentError).css("left");
                        var parentLeft = parseInt(parentLeftS.replace("px", ""));
                        parentTop = parentTop + moveY;
                        parentLeft = parentLeft + moveX;
                        parentTopS = parentTop + "px";
                        parentLeftS = parentLeft + "px";
                        $(parentError).css("top", parentTopS);
                        $(parentError).css("left", parentLeftS);
                    });
                }
            });
            
        }

        // hide edit form
        function hideAddEditForm() {
            hide_light_box_edit();
            return false;
        }

        // submit form
        function submitForm() {
            $("form").submit();
            return false;
        }


        function hidethongBaoError() {
            if ($("#thongbaoError_div").is(":visible")) {
                setTimeout(function () {
                    $("#thongbaoError_div").hide();
                    $("#fade2").hide();
                }, 3000);
            }
        }
        function showthongBaoError() {
            $("#thongbaoError_div").show();
            $("#fade2").show();
        }
        function hidethongBaoSuccess() {
            if ($("#thongbaoSucces_div").is(":visible")) {
                setTimeout(function () {
                    $("#thongbaoSucces_div").hide();
                    $("#fade2").hide();
                    $("#fade").hide();
                }, 3000);
            }
        }
        function showthongBaoSuccess() {
            $("#thongbaoSucces_div").show();
            $("#fade2").show();
        }
        $(document).ready(function () {
            setInterval(hidethongBaoError, 500);
            setInterval(hidethongBaoSuccess, 500);

        });

        function showDiaChiForm() {
            $("#MainContent_light").show();
            $("#fade").show();
        }

        function hideDiaChiForm() {
            reset_edit_form();
            $("#Form1").validationEngine("hideAll");
            $("#MainContent_light").hide();
            $("#fade").hide();
        }
        function hide_light_box_edit() {
            detachValidationEngine();
            reset_edit_form();
            $("#MainContent_light").hide();
            $("#MainContent_fade").hide();
            return false;
        }
        function xoaclick() {
            var idd = $("#MainContent_hdfCoQuanID").val();
            var level = $("#MainContent_hdflevel").val();
            hide_light_box_edit();
            $("#MainContent_light").hide();
            $("#MainContent_fade").hide();
            $("#MainContent_popXoa").hide();

            $.ajax({
                async: false,
                type: 'POST',
                //url: tree_data.delete_url,
                url: "DiaChiDelete.ashx",
                dataType: "json",
                data: {
                    "operation": "remove_node",
                    "id": idd,
                    "level": level,
                },
                success: function (json) {

                    if (json > 0) {
                        $("#MainContent_lblContentSuccess").html("Đã xóa thành công.");
                        showthongBaoSuccess();
                    }
                    else {

                        $("#MainContent_lblContentErr").html("Cơ quan đang được sử dụng, không thể xóa!");
                        showthongBaoError();
                    }
                    data.inst.refresh();

                },
                error: function (json) {

                }
            });

        }

        function showDelelePopup(idd,level) {
            $("#MainContent_hdflevel").val(level);
            $("#MainContent_hdfCoQuanID").val(idd);
            $("#MainContent_popXoa").show();
            $("#fade").show();
        }
    </script>
    <%--<link rel="stylesheet" type="text/css" media="all" href="styles/ttcp_extra.css" />--%>

    <!-- load delete confirm -->
    <div id="popXoa" class="ui-dialog ui-widget ui-widget-content ui-corner-all ui-draggable popup"
            tabindex="-1" role="dialog" aria-labelledby="ui-dialog-title-delete-form" runat="server" style="z-index:1002;  top:200px; left: 50%; margin-left:-175px; width:350px; display:none">
            <div class="ui-dialog-titlebar ui-widget-header ui-corner-all ui-helper-clearfix">
                <span class="ui-dialog-title" id="ui-dialog-title-delete-form">Thao tác xóa dữ liệu.</span>
                <a href="#" class="ui-dialog-titlebar-close ui-corner-all close_link" role="button" onclick="hidePop(); return false;">
                    <span class="ui-icon ui-icon-closethick">close</span> </a>
            </div>
        <asp:HiddenField ID="hdfCoQuanID" runat="server" />
        <asp:HiddenField ID="hdflevel" runat="server" />
            <div id="delete-form" class="ui-dialog-content ui-widget-content" scrolltop="0" scrollleft="0">
                <p>
                    <span id="ui-icon ui-icon-alert" class="ui-icon ui-icon-alert" style="float: left;
                        margin: 0 7px 20px 0;"></span><span id="delete-form-message">Bạn có chắc muốn xóa cơ quan này?
                            <asp:Label ID="ten_xoa" Text="" runat="server" /> ?
                            <asp:Label ID="id_xoa" Text="text" runat="server" Style="display: none;" />
                        </span>
                </p>
            </div>
            <div class="ui-dialog-buttonpane ui-widget-content ui-helper-clearfix">
                <div class="ui-dialog-buttonset">
                    <asp:Button ID="Button1" class="save validate_button deleteBtn" Text="CÓ" runat="server" OnClientClick="xoaclick();"
                        CausesValidation="false" />
                    <asp:Button ID="Button2" class="save" Text="KHÔNG" runat="server" OnClientClick="hidePop(); return false;"  CssClass="save btn-cancel deldete-button" CausesValidation="false" />
                </div>
            </div>
   </div>
    <div id="delete-form" title="Thao tác xóa dữ liệu." style="display: none;">
        <p>
            <span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>
            <span id="delete-form-message">
                <fmt:message key='delete_form.msg' />
            </span>
        </p>
    </div>
    <div id="message-form" title="Thông báo." style="display: none;">
        <p>
            <span class="ui-icon ui-icon-circle-check" style="float: left; margin: 0 7px 20px 0;">
            </span><span id="message-form-message">
                <fmt:message key='delete_form.msg' />
            </span>
        </p>
    </div>
    <!-- end load delete confirm  -->

    <div id="main_panel_container">
        <!-- load treeView -->
        <div class="dashboard" style="min-height: 603px;">
            <!--  add mew button -->
            <div class="ico_list">
                <label class="h2">
                    Danh mục địa giới hành chính 
                    <asp:LinkButton ID="btnThem"  runat="server" OnClientClick="showDiaChiForm(); return false;"
                        CssClass="button-add" CausesValidation="false">
                    <img src="images/add.jpeg" alt=""/>
                    <span style="font-size: 13px;cursor: pointer;">Thêm địa giới hành chính</span>
                    </asp:LinkButton>
                    <%--<a id="them_diaDanh" onclick="addNewRootNode()" href="javascript:void(0)"
                        style="font-size: 12px; float: right">
                        <img src="images/add.jpeg" style="vertical-align: middle">
                        <span style="vertical-align: middle">Thêm địa giới hành chính</span> </a>--%>
                </label>
            </div>
            <!-- load form update diachi -->
            <%--<div id="light" class="white_content" style="height: auto; width:500px; display:none;
                top: 10%; left: 50%; margin-left:-250px">
                <h2 class="ico_list">
                    Thêm mới/ Sửa thông tin địa chỉ</h2>--%>
            <div id="light" class="ui-dialog ui-widget ui-widget-content ui-corner-all ui-draggable popup"
            tabindex="-1" role="dialog" aria-labelledby="ui-dialog-title-macapForm" runat="server" style="z-index:1002;  top:200px; left: 50%; margin-left:-250px; width:500px; display:none">
            <div class="ui-dialog-titlebar ui-widget-header ui-corner-all ui-helper-clearfix">
                <span class="ui-dialog-title" id="ui-dialog-title-macapForm">
                    <img src="images/edit-add.png" style="float: left;">
                    <font face="arial">&nbsp; Thêm mới/ Sửa thông tin địa giới hành chính</font> </span><a href="#"
                        class="ui-dialog-titlebar-close ui-corner-all close_link" role="button" onclick="hideDiaChiForm(); return false;">
                        <span class="ui-icon ui-icon-closethick">close</span> </a>
            </div>
                <div id="editForm" name="editForm">
                <ul>
                    <fieldset>
                        <table>
                            <!--  edit fields -->
                            <!-- id -->
                            <tbody>
                                <tr>
                                    <th class="field_label lblText">
                                        Mã:
                                    </th>
                                    <td colspan="2">
                                        <li id="wwgrp_id_show" class="wwgrp">
                                            <div id="wwctrl_id_show" class="wwctrl">
                                                <input type="text" name="id_show" size="40" value="" disabled="disabled" id="id_show"></div>
                                        </li>
                                        <input type="hidden" name="id_frm" value="" id="id_frm">
                                    </td>
                                </tr>
                                <!-- name -->
                                <tr>
                                    <th class="field_label lblText">
                                        Tên địa danh:
                                    </th>
                                    <td colspan="2">
                                        <li id="wwgrp_name_frm" class="wwgrp">
                                            <div id="wwctrl_name_frm" class="wwctrl">
                                                <input type="text" name="name_frm" size="40" maxlength="255" value="" tabindex="1"
                                                    id="name_frm" class="validate[required,custom[onlyLetterNumberSpace],maxSize[255],ajax[ajaxAvailable]] text-input"></div>
                                        </li>
                                    </td>
                                    <td class="required_span">
                                    </td>
                                </tr>
                                <!-- level -->
                                <tr>
                                    <th class="field_label lblText">
                                        Cấp bậc:
                                    </th>
                                    <td colspan="2">
                                        <li id="wwgrp_level_frm" class="wwgrp">
                                            <div id="wwctrl_level_frm" class="wwctrl">
                                                <input type="text" name="level_frm" size="40" value="" disabled="disabled" id="level_frm"></div>
                                        </li>
                                    </td>
                                </tr>
                                <!-- parent -->
                                <tr>
                                    <th class="field_label lblText">
                                        Địa danh cha:
                                    </th>
                                    <td colspan="2">
                                        <li id="wwgrp_parentName_frm" class="wwgrp">
                                            <div id="wwctrl_parentName_frm" class="wwctrl">
                                                <input type="text" name="parentName_frm" size="40" value="" disabled="disabled" id="parentName_frm"></div>
                                        </li>
                                        <input type="hidden" name="parentId_frm" value="" id="parentId_frm">
                                    </td>
                                </tr>
                                <!-- cap -->
                                <tr>
                                    <th class="field_label lblSelect">
                                        Loại địa danh:
                                    </th>
                                    <td colspan="2" style="align: left;">
                                        <select tabindex="2" class="selectFrm" name="cap_frm" id="cap_frm" style="width: 98%; padding:0px 0px !important">
                                            <option value="khác">Khác</option>
                                            <option value="tỉnh">Tỉnh</option>
                                            <option value="thành phố">Thành phố</option>
                                        </select>
                                    </td>
                                </tr>
                                <!-- fullName -->
                                <tr>
                                    <th class="field_label lblText">
                                        Tên đầy đủ:
                                    </th>
                                    <td colspan="2">
                                        <li id="wwgrp_fullName_frm" class="wwgrp">
                                            <div id="wwctrl_fullName_frm" class="wwctrl">
                                                <input type="text" name="fullName_frm" size="40" value="" disabled="disabled" id="fullName_frm"></div>
                                        </li>
                                    </td>
                                </tr>
                                <!-- junk_row -->
                                <tr>
                                    <td colspan="2">
                                        &nbsp;
                                    </td>
                                </tr>
                                <!--  button fields -->
                                <tr>
                                    <td></td>
                                    <td colspan="2" align="left">
                                        <div class="" style="padding-bottom:10px">
                                        <asp:Button ID="btnLuu" class="save validate_button" Text="Lưu lại" runat="server" CausesValidation="true" OnClientClick="handleSubmitAction(); return false;" OnClick="btnSave_Click" />
                                        <asp:Button ID="btnHuy" class=" save btn-cancel" Text="Hủy bỏ" runat="server" OnClientClick="hideDiaChiForm(); return false;" CausesValidation="false" />
                                            </div>
                                        
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </fieldset>
                </ul>
                </div>
            </div>
            <!-- end load form -->
            <!--  search button -->
            <div class="content-body">
                       <div class="confirmDiv" id="thongbaoSucces_div" style="padding: 1px; background: #50679E">
        <label id="lblHeaderSuccess" style="color: White; background: #50679E; height: 10px">
            Thông báo
        </label>
        <div style="border: 1px solid #cecece; height: 80px; background: white; margin-left: 4px;
            margin-bottom: 4px; margin-right: 4px;">
            <img alt="" src="../images/messagebox_info.png" style="width: 30px; margin-left: 7px;
                margin-top: 14px;" /><asp:Label id="lblContentSuccess" Style="margin-top: -33px;
                    margin-left: 35px" runat="server"></asp:Label>
        </div>
    </div>
    <div class="confirmDiv" id="thongbaoError_div" style="padding: 1px; background: #50679E">
        <label id="lblHeaderErr" style="color: Black; background: #50679E; height: 10px">
            Lỗi
        </label>
        <div style="border: 1px solid #cecece; height: 80px; background: white; margin-left: 4px;
            margin-bottom: 4px; margin-right: 4px;">
            <img alt="" src="../images/Error.png" style="width: 26px; margin-left: 7px; margin-top: 14px;" /><asp:Label id="lblContentErr"  Style="margin-top: -33px; margin-left: 35px" runat="server"></asp:Label>
        </div>
    </div>
            <asp:Panel runat="server" DefaultButton="btnSearch">
                <div id="search_div" class="pagination" style="float:right">                
                <input id="search_text" name="search_text" type="text" value="" size="40" onfocus="this.select()"
                    autocomplete="off" />
                <asp:Button ID="btnSearch" CssClass="save" OnClientClick="search_tree(); return false;" runat="server" Text="Tìm kiếm"/>
            </div>
            </asp:Panel>
            <br /><br /><br />
            <!-- message area -->
           <%-- <div style=" text-align: center; background-color: #fff; padding: 10px;">
            </div>--%>
            <!-- tree area -->
            <div id="treeview" class="jstree jstree-0 jstree-focused jstree-diadanh" style=" background:#F8F8F8 !important" >
            </div>
            <!-- clear bottom area -->
            <div style="margin-bottom: 10px;">
                &nbsp;</div>
                </div>
        </div>
    </div>
    <div id="sidebar" class="right">
        <asp:Literal ID="ltrSideMenu" runat="server"></asp:Literal>
    </div>
    <div id="fade" class="black_overlay"></div>
    <div id="fade2" class="black_overlay"></div>
</asp:Content>
