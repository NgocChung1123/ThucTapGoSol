<%@ Page Title="Danh mục Cơ Quan" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CoQuan.aspx.cs" Inherits="Com.Gosol.CMS.Web.CoQuan" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager runat="server" EnablePartialRendering="true">
    </asp:ScriptManager>
    <%--<link href="/Styles/dropdownlist/chosen.min.css" rel="stylesheet" type="text/css" />--%>
   <%-- <script  type="text/javascript" src="Scripts/coquan_popup_delete.js"></script>--%>
    <script src="/Scripts/dropdownlist/chosen.jquery.js" type="text/javascript"></script>
    <%--<script src="/Scripts/ttcp_all.js"></script>--%>

    <!-- Select2 -->
    <link href="/AdminLte/plugins/select2/select2.min.css" rel="stylesheet" type="text/css" />
    
    <title>Danh mục Cơ quan đơn vị</title>
    <!-- Load for CoQuan -->
    <!-- edit box styles -->
    <style type="text/css">
        #editForm th.lblText
        {
            text-align: left;
            padding-right: 10px;
            vertical-align: top;
            padding-top: 10px;
        }
        #editForm th.lblSelect
        {
            text-align: left;
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
        #editForm td
        {
            vertical-align: middle;
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
    <script type="text/javascript" src="/Scripts/jquery.jstree.js"></script>
    <script type="text/javascript" language="Javascript" src="/Scripts/ttcp_jstree_coquan_new.js"></script>
    <script type="text/javascript" language="Javascript" src="/Scripts/jquery.hotkeys.js"></script>
    
    <!-- autocomplete -->
    <!--<link rel="stylesheet" type="text/css" media="all" href="styles/jquery.autocomplete.ajax.styles.css" />
    <script type="text/javascript" src="scripts/jquery.autocomplete.ajax.js"></script>
    <script type="text/javascript" language="Javascript">
        $(document).ready(function () {
            var searchV = $("#search_text").autocomplete({
                serviceUrl: 'CoQuanSuggestion.ashx',
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

            $("#light")
            .css({                
                visibility: 'hidden',
                display:    'block'
            });

            var config = {
                '.search_select': {}
            }
            for (var selector in config) {
                $(selector).chosen(config[selector]);
            }

            $("#light")
                .css({                    
                    visibility: 'visible',
                    display:    'none'
            });            

            $("#MainContent_ddlTinh").change(function () {
                var id = $(this).val();
                $.ajax({
                    type: "GET",
                    url: "GetHuyenByTinh.ashx",
                    data:
					   {
					       tinhID: id
					   },
                    success: function (data) {
                        $("#MainContent_ddlHuyen").empty();
                        $("#MainContent_ddlHuyen").append(data);
                       
                        $("#MainContent_ddlHuyen").trigger("chosen:updated");
                        
                    }
                });
            });

            $("#MainContent_ddlHuyen").change(function () {
                var id = $(this).val();
                $("#MainContent_hdHuyenID").val(id);
                $.ajax({
                    type: "GET",
                    url: "GetXaByHuyen.ashx",
                    data:
					   {
					       huyenID: id
					   },
                    success: function (data) {
                        $("#MainContent_ddlXa").empty();
                        $("#MainContent_ddlXa").append(data);
                        $("#MainContent_ddlXa").trigger("chosen:updated");
                    }
                });
            });

            $("#MainContent_ddlXa").change(function () {
                var id = $(this).val();
                $("#MainContent_hdXaID").val(id);
            });
          
            // assign min height to the dashboard on load
            var sidebarHeight = 0 + "px";
            $("div.dashboard").css("min-height", sidebarHeight);

            
            // init tree view
            var jstree_data = {
                //capID: $("#MainContent_hdfCapID").val(),

                tree_id: "#treeview",
                theme: "coquan",
                theme_path: "scripts/jstree_themes/themes/",
                show_edit_box: true,
                search_url: "CoQuanSearch.ashx",
                list_url: "CoQuanGetData.ashx?coQuanID=" + $("#MainContent_hdfCoQuanChaID").val() + "&&capID=" + $("#MainContent_hdfCapID").val(),
                add_url: "CoQuanAdd.ashx.ashx",
                edit_url: "CoQuanEdit.ashx",
                move_url: "moveNodeCoQuan",
                delete_url: "CoQuanDelete.ashx"
                //delete_deny: <%= GetDeleteDeny() %>,
                //create_deny: <%= GetCreateDeny() %>,
                //edit_deny: <%= GetEditDeny() %>,
            };
            init_tree(jstree_data);

            // bind escape key to the form
            if (jstree_data.show_edit_box) {
                bindEscapeKey();
            }
        });

    </script>
    <!-- some init action -->
    <script type="text/javascript" language="Javascript">
        //function hidePop() {
        //    hide_light_box_edit();
        //}
        function hidePop() {
            var data = $("#treeview"); 
            hide_light_box_edit();
            $("#popXoa").modal("hide");

            refreshTree();
        }

        function xoaclick() {
            
            var idd = $("#MainContent_hdfCoQuanID").val();
            //hide_light_box_edit();

            $("#popXoa").modal("hide");
                $.ajax({
                    async: false,
                    type: 'POST',
                    url: "CoQuanDelete.ashx",
                    dataType: "json",
                    data: {
                        "operation": "remove_node",
                        "id": idd
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

        function showthongBaoSuccess() {
            $("#thongbaoSucces_div").modal();
        }

        function showthongBaoError() {
            $("#thongbaoError_div").modal();
        }

        function showDelelePopup(idd) {
            $("#MainContent_hdfCoQuanID").val(idd);
            $("#popXoa").modal();
        }

        function populateSelectFields() {
            $("#level_frm").val("1");
        }

        function populate_cap_select_box(level) {
            //            $("#cap_frm").html("");
            //            var html;
            //            if (level == 1 || level == "1") {
            //                html = "<option value='khác'>Khác</option>"
            //					+ "<option value='tỉnh'>Tỉnh</option>"
            //					+ "<option value='thành phố'>Thành phố</option>";
            //                $("#cap_frm").html(html);
            //            }

            //            if (level == 2 || level == "2") {
            //                html = "<option value='khác'>Khác</option>"
            //					+ "<option value='quận'>Quận</option>"
            //					+ "<option value='huyện'>Huyện</option>"
            //					+ "<option value='thị xã'>Thị xã</option>"
            //					+ "<option value='thành phố'>Thành phố</option>";
            //                $("#cap_frm").html(html);
            //            }

            //            if (level == 3 || level == "3") {
            //                html = "<option value='khác'>Khác</option>"
            //					+ "<option value='xã'>Xã</option>"
            //					+ "<option value='phường'>Phường</option>"
            //					+ "<option value='thị trấn'>Thị trấn</option>";
            //                $("#cap_frm").html(html);
            //            }

            //            $("#cap_frm")[0].selectedIndex = 1;
        }
    </script>
    <!-- form add/edit rule -->
    <script type="text/javascript" language="Javascript">
        var checkAvailableUrl = "CoQuanCheckAvailable.ashx"; 	// url check available
        var fetchEditDetailUrl = "CoQuanEdit.ashx"; 	// url fetch edit details
        var saveDbUrl = "CoQuanSaveData.ashx"; 	// url fetch edit details
        var deleteUrl = "CoQuanDelete.ashx";

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
            //            if (!($("#name_frm").attr("isValid") == "true")) {
            //            if ($.trim($("#name_frm").val()) == "")
            //            //alert_error("Tên không được bỏ trống");
            //            alert_message("Tên không được bỏ trống");
            //            else
            //            //alert_error("Tên này đã tồn tại!!! xin vui lòng nhập lại");
            //            alert_message("Tên này đã tồn tại!!! xin vui lòng nhập lại");
            //            focus_editable_field("name_frm");
            //            return false;
            //            }

            //            submitted++;
            //            if (submitted > 1)
            //            return false;
            //            setTimeout(function () { submitted = 0; }, 3000);

            //            return true;
            //            }
            //            else
            //            return false;
            return true;
        }


        // bind validation Engine to the form
        function bindValidationEngine() {
            /*$("#editForm").validationEngine({
            validationEventTrigger: "blur",
            scroll: false
            });*/
            return false;
        }

        // detach validation engine
        function detachValidationEngine() {
            $("#editForm").validationEngine('hideAll');
            $("#editForm").validationEngine('detach');

            $("#Form1").validationEngine('hideAll');
            $("#Form1").validationEngine('detach');
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
            showCoQuanForm();
            bindValidationEngine();
            return false;
        }

        // hide edit box
        function hide_light_box_edit() {
            detachValidationEngine();
            reset_edit_form();
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
            $("#MainContent_id_show").val(0);
            $("#MainContent_id_frm").val(0);
            $("#MainContent_name_frm").val("");
            $("#MainContent_ddlCoQuanCha").val("0");
            $("#MainContent_hdfCoQuanChaEditID").val("0");
            $("#level_frm").val("0");
            $("#MainContent_ddlCap").val("0");
            //$("#MainContent_ddlThamQuyen").val("0");
            $("#MainContent_ddlWorkFlow").val("");
            $("#MainContent_ddlWorkFlowTienHanhTT").val("");

            $("#MainContent_ddlTinh").val("").trigger("chosen:updated");
            $("#MainContent_ddlHuyen").val("0").trigger("chosen:updated");
            $("#MainContent_ddlXa").val("0").trigger("chosen:updated");

            $("#MainContent_cbxSuDungPM").attr('checked', false);
            $("#level_frm").val("1");

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
					       id: parentId, level: level
					   },
                success: fetchParentSuccess,
                error: fetchParentError
            });
            return false;
        }

        function fetchParentSuccess(json) {
            
            if (json) {
                var parent = json;
                $("#MainContent_ddlCoQuanCha").val(parent.id);
                $("#MainContent_hdfCoQuanChaEditID").val(parent.id);
                $("#parentName_frm").val(parent.fullName);
                var level = parent.level + 1;
                $("#level_frm").val(level);
                //                populate_cap_select_box(level);

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

        function bindHuyenSelect(data) {
            MainContent_name_frm
            $("#MainContent_ddlHuyen").append(data);
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
                var coQuan = json;
                $("#MainContent_id_show").val(coQuan.id);
                $("#MainContent_id_frm").val(coQuan.id);
                $("#MainContent_name_frm").val(coQuan.name);               
                $("#MainContent_ddlCoQuanCha").val(coQuan.coquanchaID);
                $("#MainContent_hdfCoQuanChaEditID").val(coQuan.coquanchaID);
                
                $("#level_frm").val(coQuan.level);
                $("#MainContent_ddlCap").val(coQuan.capID);
                $("#MainContent_ddlThamQuyen").val(coQuan.thamquyenID);

                $("#MainContent_ddlTinh").val(coQuan.tinhID);
                $("#MainContent_code_frm").val(coQuan.maCQ);
                $("#MainContent_ddlTinh").trigger("chosen:updated");
                if (coQuan.sudungPM) $("#MainContent_cbxSuDungPM").attr('checked', true);
                if (coQuan.cQCoHieuLuc) $("#MainContent_cbxCQCoHieuLuc").attr('checked', true);

                $("#MainContent_ddlWorkFlow").val(coQuan.workFlowID);
                $("#MainContent_ddlWorkFlowTienHanhTT").val(coQuan.wFTienHanhTTID);
                $.ajax({
                    type: "GET",
                    url: "GetHuyenByTinh.ashx",
                    data:
					{
					    tinhID: coQuan.tinhID
					},
                    success: function (data) {
                        $("#MainContent_ddlHuyen").empty();
                        $("#MainContent_ddlHuyen").append(data);
                        $("#MainContent_ddlHuyen").val(coQuan.huyenID);
                        $("#MainContent_hdHuyenID").val(coQuan.huyenID);
                        $("#MainContent_ddlHuyen").trigger("chosen:updated");

                        $.ajax({
                            type: "GET",
                            url: "GetXaByHuyen.ashx",
                            data:
					        {
					            huyenID: coQuan.huyenID
					        },
                            success: function (html) {
                                $("#MainContent_ddlXa").empty();
                                $("#MainContent_ddlXa").append(html);
                                $("#MainContent_ddlXa").val(coQuan.xaID);
                                $("#MainContent_ddlXa").trigger("chosen:updated");
                                $(".select2").select2();
                            }
                        });
                    }
                });


                show_light_box_edit();
                focus_editable_field("name_frm");
                focus_editable_field("cap_frm");
            }
            else {
                alert_error("Không tìm thấy cơ quan đó!!!");
                hideAddEditForm();
                refreshTree();
                focusTree();
            }
            return false;
        }

        function fetchError() {
            //alert_error("Có lỗi server !!!");
            $("#MainContent_lblContentErr").html("Có lỗi server !!!");
            showthongBaoError();
            hideAddEditForm();
            focusTree();
            return false;
        }

        function showEditDialog() {
            var startX, startY, stopX, stopY, moveX, moveY;
            $("#editForm").dialog({
                title: "<img src='images/edit-add.png' style='float:left;' /><font face='arial'> &nbsp; Thêm mới/Sửa thông tin Cơ quan",
                autoOpen: true,
                modal: true,
                resizable: false,
                height: 500,
                width: 455,
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
                },
                open: function (event, ui) {
                    $(this).css('overflow', 'visible');
                }
            }).parent().appendTo("form");

        }

        // hide edit form
        function hideAddEditForm() {
            //hide_light_box_edit();
            $("#MainContent_id_show").val(0);
            $("#MainContent_id_frm").val(0);
            $("#MainContent_code_frm").val("");
            $("#MainContent_name_frm").val("");
            $("#MainContent_ddlCoQuanCha").val("0");
            $("#MainContent_hdfCoQuanChaEditID").val("0");
            $("#level_frm").val("0");
            $("#MainContent_ddlCap").val("0");
            //$("#MainContent_ddlThamQuyen").val("0");
            $("#MainContent_ddlWorkFlow").val("");
            $("#MainContent_ddlWorkFlowTienHanhTT").val("");

            $("#MainContent_ddlTinh").val("0").trigger("chosen:updated");
            $("#MainContent_ddlHuyen").val("0").trigger("chosen:updated");
            $("#MainContent_ddlXa").val("0").trigger("chosen:updated");

            //$("#MainContent_cbxCapUBND").attr('checked', false);
            //$("#MainContent_cbxCapThanhTra").attr('checked', false);
            $("#MainContent_cbxSuDungPM").attr('checked', false);

            //$("#MainContent_cbxSuDungQuyTrinh").attr('checked', false);
            //$("#MainContent_cbxSuDungQuyTrinhGQ").attr('checked', false);
            //$("#MainContent_cbxQuyTrinhVanThuTiepNhan").attr('checked', false);
            //$("#MainContent_cbxQuyTinhCBTiepDan").attr('checked', false);
            $("#MainContent_cbxCQCoHieuLuc").attr('checked', false);
            return false;
        }

        // submit form
        function submitForm() {
            $("form").submit();
            return false;
        }

        function showCoQuanForm() {
            $("#light").modal();
        }

        function hideCoQuanForm() {
            hideAddEditForm();
            $("#light").modal("hide");
            
            var jstree_data = {
                tree_id: "#treeview",
                theme: "coquan",
                theme_path: "scripts/jstree_themes/themes/",
                show_edit_box: true,
                search_url: "CoQuanSearch.ashx",
                list_url: "CoQuanGetData.ashx?coQuanID=" + $("#MainContent_hdfCoQuanChaID").val() + "&&capID=" + $("#MainContent_hdfCapID").val(),
                add_url: "CoQuanAdd.ashx.ashx",
                edit_url: "CoQuanEdit.ashx",
                move_url: "moveNodeCoQuan",
                delete_url: "CoQuanDelete.ashx"
                //delete_deny: <%= GetDeleteDeny() %>,
                //create_deny: <%= GetCreateDeny() %>,
                //edit_deny: <%= GetEditDeny() %>,
            };
            init_tree(jstree_data);
        }

        function ReloadPage() {
            window.location.href = "/CoQuan.aspx";
        }

        function SuccessAddCoQuan() {
            //hideCoQuanForm();
            $("#light").modal("hide");
            $("#MainContent_lblContentSuccess").html("Cập nhật dữ liệu thành công.");
            showthongBaoSuccess();
        }

        function ErrorAddCoQuan() {
            //hideCoQuanForm();
            $("#light").modal("hide");
            $("#MainContent_lblContentErr").html("Xảy ra lỗi trong quá trình cập nhật.");
            showthongBaoError();
        }
      
    </script>
    <!-- end load delete confirm  -->
    
    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="light" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Thêm mới/Sửa cơ quan, đơn vị</h4>
                </div>
                <div class="modal-body">
                    <table style="margin: auto;">
                        <tbody>
                            <tr style="display: none;">
                                <th class="field_label lblText">
                                    Mã:
                                </th>
                                <td colspan="2">
                                    <input type="text" name="id_show" size="40" value="" disabled="disabled" id="id_show" class="form-control " runat="server" />
                                    <input type="hidden" name="id_frm" value="" id="id_frm" runat="server">
                                </td>
                            </tr>
                            <tr style="height: 35px;">
                                <th class="field_label lblText">
                                    Tên cơ quan: <span style="color: red">*</span>
                                </th>
                                <td colspan="2">
                                    <input type="text" style="" name="name_frm" size="40" maxlength="255" value="" tabindex="1" id="name_frm" class="validate[required] text-input form-control" runat="server" />
                                </td>
                            </tr>
                            <tr style="height: 35px;">
                                <th class="field_label lblText">
                                    Mã cơ quan: <span style="color: red">*</span>
                                </th>
                                <td colspan="2">
                                    <input type="text" style="" name="code_frm" size="40" maxlength="255" value="" tabindex="1" id="code_frm" class="validate[required] text-input form-control" runat="server" />
                                </td>
                                <td class="required_span">
                                </td>
                            </tr>
                            <!-- level -->
                            <tr style="display: none;">
                                <th class="field_label lblText">
                                    Cấp bậc:
                                </th>
                                <td colspan="2">
                                    <input type="text" name="level_frm" size="40" value="" disabled="disabled" id="level_frm">
                                </td>
                            </tr>
                            <!-- parent -->
                            <tr style="height: 35px;">
                                <th class="field_label lblText">
                                    Cơ quan cha:
                                </th>
                                <td colspan="2">
                                    <%--<asp:UpdatePanel runat="server">
                                        <ContentTemplate>--%>
                                            <asp:DropDownList ID="ddlCoQuanCha" DataTextField="TenCoQuan" DataValueField="CoQuanID"
                                                runat="server" TabIndex="2" Enabled="false" CssClass="form-control">
                                            </asp:DropDownList>
                                    <asp:HiddenField ID="hdfCoQuanChaEditID" runat="server"/>
                                        <%--</ContentTemplate>
                                        <Triggers>--%>
                                            <%--<asp:AsyncPostBackTrigger ControlID="btnLuu" EventName="Click" />--%>
                                        <%--</Triggers>
                                    </asp:UpdatePanel>--%>
                                </td>
                            </tr>
                            <!-- cap -->
                            <tr style="height: 35px;">
                                <th class="field_label lblSelect">
                                    Cấp: <span style="color: red">*</span>
                                </th>
                                <td colspan="2" style="align: left;">
                                    <asp:DropDownList ID="ddlCap" DataTextField="TenCap" DataValueField="CapID" runat="server"
                                        TabIndex="3" CssClass="validate[required] form-control" style="width: 100%;">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <%--<tr>
                                <td>
                                </td>
                                <td>
                                    <asp:CompareValidator runat="server" ID="ddlCap_Compare" ControlToValidate="ddlCap"
                                        Type="Integer" ForeColor="Red" Font-Bold="true" Display="Dynamic" Operator="NotEqual"
                                        ErrorMessage="Hãy chọn cấp!" ValueToCompare="0" ValidationGroup="CoQuan"></asp:CompareValidator>
                                </td>
                            </tr>--%>
                            <!-- thamquyen -->
                            <%--<tr style="height: 35px;">
                                <th class="field_label lblSelect">
                                    Thẩm quyền: <span style="color: red">*</span>
                                </th>
                                <td colspan="2" style="align: left;">
                                    <asp:DropDownList ID="ddlThamQuyen" runat="server" DataTextField="TenThamQuyen" DataValueField="ThamQuyenID"
                                        TabIndex="4" CssClass="validate[required] form-control" style="width: 100%;">
                                    </asp:DropDownList>
                                </td>
                            </tr>--%>
                            <!-- tinh -->
                            <tr style="height: 35px;">
                                <th class="field_label lblSelect">
                                    Tỉnh:
                                </th>
                                <td colspan="2" style="align: left;">
                                    <asp:DropDownList ID="ddlTinh" runat="server" DataTextField="TenTinh" DataValueField="TinhID"
                                        TabIndex="5" CssClass="form-control validate[required] text-input" style="width: 100%;">
                                    </asp:DropDownList>
                                    <asp:HiddenField ID="hdTinhID" runat="server" />
                                </td>
                            </tr>
                            <!-- huyen -->
                            <tr style="height: 35px;">
                                <th class="field_label lblSelect">
                                    Huyện:
                                </th>
                                <td colspan="2" style="align: left;">
                                    <asp:DropDownList ID="ddlHuyen" DataTextField="TenHuyen" DataValueField="HuyenID" runat="server" TabIndex="6" CssClass="form-control " style="width: 100%;">
                                    </asp:DropDownList>
                                    <asp:HiddenField ID="hdHuyenID" runat="server" />
                                </td>
                            </tr>
                            <!-- xa -->
                            <tr style="height: 35px;">
                                <th class="field_label lblSelect">
                                    Xã:
                                </th>
                                <td colspan="2" style="align: left;">
                                    <asp:DropDownList ID="ddlXa" DataTextField="TenXa" DataValueField="XaID" runat="server" TabIndex="7" CssClass="form-control " style="width: 100%;">
                                    </asp:DropDownList>
                                    <asp:HiddenField ID="hdXaID" runat="server" />
                                </td>
                            </tr>

                            <!-- xa -->
                            <tr style="height: 35px; display:none">
                                <th class="field_label lblSelect">
                                    Luồng nghiệp vụ:
                                </th>
                                <td colspan="2" style="align: left;">
                                    <asp:DropDownList ID="ddlWorkFlow" DataTextField="WorkFlowName" DataValueField="WorkFlowID" runat="server" TabIndex="7" CssClass=" form-control " style="width: 100%;">
                                    </asp:DropDownList>
                                    <asp:HiddenField ID="HiddenField1" runat="server" />
                                </td>
                            </tr>
                             <tr style="height: 35px; display:none">
                                <th class="field_label lblSelect">
                                    Luồng tiến hành TT:
                                </th>
                                <td colspan="2" style="align: left;">
                                    <asp:DropDownList ID="ddlWorkFlowTienHanhTT" DataTextField="WorkFlowName" DataValueField="WorkFlowID" runat="server" TabIndex="7" CssClass=" form-control " style="width: 100%;">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr style="height: 25px;">
                                <th class="field_label lblSelect">
                                    Cơ quan thuộc hệ thống PM
                                </th>
                                <td colspan="2" style="text-align: left;padding-left: 10px;">
                                    <asp:CheckBox ID="cbxSuDungPM" runat="server" />
                                </td>
                            </tr>
                            <tr style="height: 25px;">
                                <th class="field_label lblSelect">Cơ quan có hiệu lực
                                </th>
                                <td colspan="2" style="text-align: left;padding-left: 10px;">
                                    <asp:CheckBox ID="cbxCQCoHieuLuc" runat="server" />
                                </td>
                            </tr>
                            <!-- junk_row -->
                            <tr>
                                <td colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnLuu" class="save btn btn-primary submit" Text="Lưu lại" runat="server" OnClick="btnLuu_Click" />
                    <%--<asp:Button CssClass="btn btn-primary submit"  OnClick="Save_Click" Text="Lưu lại" runat="server"></asp:Button>--%>
                    <%--<asp:Button ID="btnHuy" class="btn btn-default" Text="Đóng" runat="server" OnClientClick="hideCoQuanForm(); return false;" CausesValidation="false" />--%>
                    <button class="btn btn-default" onclick="ReloadPage();return false;">Hủy bỏ</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="popXoa" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Thông báo</h4>
                </div>
                <div class="modal-body">
                    <p>Bạn có muốn xóa cơ quan này không?</p>
                  <asp:HiddenField ID="hdfCoQuanID" runat="server" />
                </div>
                <div class="modal-footer">
                    <asp:Button runat="server" CssClass="btn btn-primary" Text="Đồng ý" OnClientClick="xoaclick(); return false;" ID="btnDelete"></asp:Button>
                    <button type="button" class="btn btn-default" data-dismiss="modal" onclick="ReloadPage(); return false;">Không</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="thongbaoSucces_div" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Thông báo</h4>
                </div>
                <div class="modal-body">
                    <p>
                        <asp:Label ID="lblContentSuccess" CssClass="content-message"
                            runat="server"></asp:Label>
                    </p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal" onclick="hideCoQuanForm(); return false;">Đóng</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="thongbaoError_div" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Thông báo</h4>
                </div>
                <div class="modal-body">
                    <p>
                        <asp:Label ID="lblContentErr" CssClass="content-message" runat="server"></asp:Label>
                    </p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal" onclick="hideCoQuanForm(); return false;">Đóng</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <!-- Content Header (Page header) -->
    <section class="content-header">
      <h1>
        Danh mục cơ quan, đơn vị
        <%--<small>(500 anh em)</small>--%>
      </h1>
      <ol class="breadcrumb">
        <li><a href="Default.aspx"><i class="fa fa-dashboard"></i> Trang chủ</a></li>
        <li><a href="#">Danh mục</a></li>
        <li class="active">Danh mục cơ quan, đơn vị</li>
      </ol>
    </section>

    <!-- Main content -->
    <section class="content">
        <asp:HiddenField ID="hdfCapID" runat="server" Value="0"/>
        <asp:HiddenField ID="hdfCoQuanChaID" runat="server" Value="0"/>
      <div class="row">
        <div class="col-xs-12">
          <div class="box">
            <div class="box-header">
                <asp:Button ID="btnThemMoi" runat="server" class="btn btn-primary" OnClientClick="showCoQuanForm(); return false;" Text="Thêm mới"></asp:Button>
                <button type="button" class="btn btn-primary" onclick="ReloadPage(); return false;">Refresh (F5)</button>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
                <div style="width:100%;min-height:40px">
                    <asp:Panel runat="server" DefaultButton="btnSearch">
                        <asp:Button runat="server" OnClientClick="search_tree(); return false;" Text="Tìm kiếm" ID="btnSearch" CssClass="btn btn-default btn-sm" Style="float: right; margin-right: 5px; margin-bottom: 10px" />
                        <input id="search_text" placeholder="Nhập nội dung cần tìm kiếm" name="search_text" type="text" value="" size="40" onfocus="this.select()" autocomplete="off" Class="form-control input-search" Style="float: right; margin-right: 10px; margin-bottom: 10px; width: 30%"/>
                    </asp:Panel>
                </div>
                
                <!-- tree area -->
                <div id="treeview" class="jstree jstree-0 jstree-focused jstree-diadanh" style="background: #F8F8F8 !important">
                </div>
                <!-- clear bottom area -->
                <div style="margin-bottom: 10px;">
                    &nbsp;</div>
            </div>
            <!-- /.box-body -->
          </div>
          <!-- /.box -->
        </div>
        <!-- /.col -->
      </div>
      <!-- /.row -->
    </section>
    <!-- /.content -->

    <script type="text/javascript" src="/AdminLte/plugins/select2/select2.full.min.js"></script>
    <script type="text/javascript">
        $(function () {
            //Initialize Select2 Elements
            $(".select2").select2();
        });

    </script>

    <!-- validation -->
    <link href="/AdminLte/ValidateForm/css/template.css" rel="stylesheet" type="text/css"/>
    <link href="/AdminLte/ValidateForm/css/validationEngine.jquery.css" rel="stylesheet" type="text/css"/>
    <%--<script src="/AdminLte/ValidateForm/js/jquery-1.8.2.min.js"></script>--%>
    <script src="/AdminLte/ValidateForm/js/jquery.validationEngine.js" type="text/javascript"></script>
    <script src="/AdminLte/ValidateForm/js/jquery.validationEngine-vi.js" type="text/javascript"></script>
    <script type="text/javascript">
        jQuery(document).ready(function () {
            // binds form submission and fields to the validation engine
            jQuery("#Form1").validationEngine();
        });
        function checkHELLO(field, rules, i, options) {
            if (field.val() != "HELLO") {
                // this allows to use i18 for the error msgs
                return options.allrules.validate2fields.alertText;
            }
        }
	</script>

</asp:Content>
