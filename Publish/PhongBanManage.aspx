<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="PhongBanManage.aspx.cs" Inherits="Com.Gosol.CMS.Web.PhongBanManage"
    ClientIDMode="AutoID" EnableEventValidation="false"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="script1" runat="server" EnablePartialRendering="true">
    </asp:ScriptManager>
    <link href="Styles/dropdownlist/chosen.min.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/dropdownlist/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_initializeRequest(InitializeRequest);
        prm.add_endRequest(EndRequest);
        var postBackElement;
        function InitializeRequest(sender, args) {
            if (prm.get_isInAsyncPostBack()) {
                args.set_cancel(true);
            }
            $("#progressDiv").show();
            $("#ajax_fade").show();
        }
        function EndRequest(sender, args) {
            $("#progressDiv").hide();
            $("#ajax_fade").hide();
        }
        var config = {
            '.chosen': {}
        }
        for (var selector in config) {
            $(selector).chosen(config[selector]);
        }

    </script>
    <script type="text/javascript">
        var dialog_title = "<img src='images/edit-add.png' style='vertical-align:middle;' /> Thêm/Sửa Cán bộ";
        var checkAvailableUrl = "checkNhanVienAvailable";
        var saveDbUrl = "saveNhanVien";
        var fetchDetailUrl = "editNhanVien";

        $(document).ready(function () {
            $("#Form1").validationEngine("attach", { scroll: false, binded: false });

            $(".cancel").click(function () {
                $("#Form1").validationEngine("detach");
                $("#Form1").validationEngine("hideAll");
            });            
        });

        // must have for all page
        function initValidationRule(field, rules, i, options) {
            return false;
        }

        function showAddCanBoForm() {
            $("#ctl00_MainContent_fade").show();
            selectedRow = $("tr.selected_hl");
            if (selectedRow.length == 0) {
                //$("#ctl00_MainContent_error").show();
                $("#ctl00_MainContent_lblContentErr").html("Vui lòng chọn một phòng ban trước!");
                showthongBaoError();
                //$("#ctl00_MainContent_lblHeaderSuccess").html("Lỗi");
                //$("#ctl00_MainContent_lblContentSucsses").html("Vui lòng chọn một phòng ban trước!");
                //showthongBaoSuccess();
                //$("#ctl00_MainContent_fade").hide();
            }
            else {
                //$("#addCanBoForm").style.display = "block";
                $("#ctl00_MainContent_ddlCanBo").val(0);
                $("#ctl00_MainContent_addCanBoForm").show();
                $("html").animate({ scrollTop: 0 }, 400);
            }
            return false;
        }

        function hideAddCanBoForm() {
            //$("#addCanBoForm").style.display = "none";
            $("#ctl00_MainContent_addCanBoForm").hide();
            $("#ctl00_MainContent_fade").hide();
        }

        function showAddForm() {
            $("#ctl00_MainContent_light").show();
            $("#ctl00_MainContent_fade").show();

             var config = {
                '.chosen': {}
            }
            for (var selector in config) {
                $(selector).chosen(config[selector]);
            }
            $('#MainContent_ddlCoQuan').trigger("chosen:updated");
            $(".chosen").trigger("chosen:updated");

            return false;
        }

        function hideAddEditForm() {
            $("#ctl00_MainContent_light").hide();
            $("#ctl00_MainContent_fade").hide();

            $("#ctl00_MainContent_txtPhongBanID").val("");
            $("#ctl00_MainContent_txtTenPhongBan").val("");
            $("#ctl00_MainContent_txtGhiChu").val("");
            $("#ctl00_MainContent_txtDienthoai").val("");
            $("#ctl00_MainContent_ddlCoQuan").val(0);
            $("#Form1").validationEngine("hideAll");

        }

        function hideSuccessMsg() {
            $("#ctl00_MainContent_fade").hide();
            $("#ctl00_MainContent_success").hide();
        }

        function ConfirmDelete(button) {
            $("#ctl00_MainContent_fade").show();
            $("#ctl00_MainContent_deleteConfirm").show();
            $("#ctl00_MainContent_hdDeleteID").val($(button).next().val());
            return false;
        }

        function hideDeleteConfirm() {
            $("#ctl00_MainContent_fade").hide();
            $("#ctl00_MainContent_deleteConfirm").hide();
            $("#ctl00_MainContent_hdDeleteID").val(0);
        }

        function ConfirmDeleteCB(button) {
            $("#ctl00_MainContent_hdDeleteCanBoID").val(0);
            $("#ctl00_MainContent_fade").show();
            $("#ctl00_MainContent_canboDeleteConfirm").show();
            $("#ctl00_MainContent_hdDeleteCanBoID").val($(button).next().val());
            return false;
        }

        function hideDeleteCBConfirm() {
            $("#ctl00_MainContent_fade").hide();
            $("#ctl00_MainContent_canboDeleteConfirm").hide();
            //$("#ctl00_MainContent_hdDeleteCanBoID").val(0);
        }

        function selectPhongBan(tr, phongbanID, event) {
            var sender = event.target;
            if (sender.tagName == "TD") {
                //add selected style and remove selected style of other rows
                $(tr).addClass("selected_hl");
                $(tr).siblings().removeClass("selected_hl");
                //change hidden ddl value
                $("#ctl00_MainContent_ddlPhongBan").val(phongbanID);
                $("#ctl00_MainContent_ddlPhongBan").change();
                //$("#ctl00_MainContent_hdfPhongBanID").val(phongbanID);
            }
        }

        function hideErrorMessage(div) {
            $("#ctl00_MainContent_fade").hide();
            HideMessage(div);
        }

        function StopParentEvent(event, control) {
            //if (!$(control).parent().parent().hasClass("selected_hl")) {
            //    event.stopPropagation();
            //}            
        }

     

    </script>
    <div class="ajax-progress" id="progressDiv">
        Đang xử lý ...
    </div>
    <div id="ajax_fade" class="black_overlay" style="z-index: 9000">
    </div>
    <div id="main_panel_container">
        <div class="ui-dialog ui-widget ui-widget-content ui-corner-all ui-draggable popup" style="z-index: 1002;
            outline: 0px; height: auto; width: 400px; top: 114px; left: 50%; margin-left:-200px; display: none;"
            id="addCanBoForm" runat="server">
            <div class="ui-dialog-titlebar ui-widget-header ui-corner-all ui-helper-clearfix">
                <span class="ui-dialog-title" id="ui-dialog-title-editFormGroup">
                    <img src="images/users_icon.png" style="vertical-align: middle;">Thêm cán bộ cho phòng ban
                </span><a
                        href="#" class="ui-dialog-titlebar-close ui-corner-all" onclick="hideAddCanBoForm();"><span class="ui-icon ui-icon-closethick">close</span></a></div>
            <fieldset>
                <table>
                    <!--  edit fields -->
                    <!-- group -->
                    <tbody>
                        <tr>
                            <th class="field_label lblSelect right-align" style="width:100px">
                                Chọn cán bộ:
                            </th>
                            <td colspan="2" style="text-align: left; padding-left: 3px;">
                                <asp:DropDownList ID="ddlCanBo" runat="server" DataTextField="TenCanBo" DataValueField="CanBoID"
                                    CssClass="selectFrm" Width="200">
                                </asp:DropDownList>
                                
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div style="height:10px"></div>
                            </td>
                        </tr>
                        <!-- blank -->
                        
                        <!--  button fields -->
                        <tr>
                            <td>
                            </td>
                            <td colspan="2" style="text-align: left;">
                                <div class="div-button-footer">
                                    <div>
                                <asp:Button ID="btnSaveCB" runat="server" Text="Lưu lại" CssClass="save " OnClick="btnSaveCB_Click" OnClientClick="hideAddCanBoForm();"
                                     CausesValidation="false" />
                                <asp:Button type="button" CssClass="save btn-cancel" Text="Hủy" OnClientClick="hideAddCanBoForm(); return false;"
                                    align="left" runat="server" ID="btnCancel" />
                                        </div>
                                    </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </fieldset>
        </div>
        <!--  form edit, add new -->
        <div tabindex="-1" class="ui-dialog ui-widget ui-widget-content ui-corner-all ui-draggable popup"
            id="light" runat="server" style="display: none; z-index: 2009; width: 500px; left: 50%; margin-left:-250px; overflow: visible">
            <div class="ui-dialog-titlebar ui-widget-header ui-corner-all ui-helper-clearfix">
                <span id="ui-dialog-title-macapForm" class="ui-dialog-title">
                    <img style="float: left;" src="images/edit-add.png">
                    <font face="arial">&nbsp; Thêm mới/ Sửa thông tin phòng ban</font> </span><a onclick="hideAddEditForm();"
                        role="button" class="ui-dialog-titlebar-close ui-corner-all cancel" href="#"><span
                            class="ui-icon ui-icon-closethick">close</span> </a>
            </div>
            <div scrollleft="0" scrolltop="0" class="ui-dialog-content ui-widget-content" id="MainContent_macapForm" style="overflow: visible">
                <ul>
                    <fieldset>
                        <table>
                            <!--  edit fields -->
                            <!-- id -->
                            <tr>
                                <th class="field_label lblText right-align" style="width:120px;">
                                    ID:
                                </th>
                                <td colspan="2">
                                    <li id="wwgrp_id_show" class="wwgrp">
                                        <div id="wwctrl_id_show" class="wwctrl">
                                            <asp:TextBox ID="txtPhongBanID" runat="server" Enabled="false" Width="265"></asp:TextBox>
                                        </div>
                                    </li>
                                </td>
                            </tr>
                            <!-- name -->
                            <tr>
                                <th class="field_label lblText right-align right-align">
                                    <span style="color: red;">*</span> Tên phòng ban:
                                </th>
                                <td colspan="2">
                                    <li id="wwgrp_name_frm" class="wwgrp">
                                        <div id="wwctrl_name_frm" class="wwctrl">
                                            <asp:TextBox ID="txtTenPhongBan" Width="265" runat="server" Enabled="true" placeholder="Không quá 200 ký tự."></asp:TextBox>
                                        </div>
                                    </li>
                                </td>
                            </tr>
                            <td colspan="2">
                            <asp:RequiredFieldValidator ID="txtTenPhongBan_Required" runat="server" ErrorMessage="Tên phòng ban không được để trống!"
                                ControlToValidate="txtTenPhongBan" ForeColor="Red" Font-Bold="true" SetFocusOnError="true"
                                Display="Dynamic" ValidationGroup="QuocTich"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="txtTenPhongBan_Regular" runat="server" ErrorMessage="Tên phòng ban không chứa ký tự đặc biệt và không quá 200 ký tự!" 
                                ControlToValidate="txtTenPhongBan" ValidationExpression="^([^!@#$%\^\&\*]*){0,200}$"
                                ForeColor="Red" Font-Bold="true" SetFocusOnError="true" ValidationGroup="PhongBan"
                                Display="Dynamic"> </asp:RegularExpressionValidator>
                        </td>
                            <tr>
                                <th class="field_label lblText right-align right-align">
                                    <span style="color: red;">*</span> Số điện thoại:
                                </th>
                                <td colspan="2">
                                    <li id="wwgrp_sdt_frm" class="wwgrp">
                                        <div id="wwctrl_sdt_frm" class="wwctrl">
                                            <asp:TextBox ID="txtDienthoai" Width="265" runat="server" Enabled="true" ></asp:TextBox>
                                        </div>
                                    </li>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:RegularExpressionValidator ID="txtDienthoai_Regular" runat="server" ErrorMessage="Số điện thoại chỉ chứa ký tự số và không quá 20 ký tự!"
                                ControlToValidate="txtDienthoai" ValidationExpression="^[0-9]{0,20}$"
                                ForeColor="Red" Font-Bold="true" SetFocusOnError="true" ValidationGroup="PhongBan"
                                Display="Dynamic"> </asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <th class="field_label lblText right-align">
                                    Ghi chú:
                                </th>
                                <td colspan="2">
                                    <li id="wwgrp_diaChi_frm" class="wwgrp">
                                        <div id="wwctrl_diaChi_frm" class="wwctrl">
                                            <asp:TextBox ID="txtGhiChu" runat="server" Width="265"></asp:TextBox>
                                        </div>
                                    </li>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:RegularExpressionValidator ID="txtGhiChu_Regular" runat="server" ErrorMessage="Ghi chú không quá 200 ký tự!"
                                ControlToValidate="txtGhiChu" ValidationExpression="^[\s\S\w\W\d\D]{0,200}$"
                                ForeColor="Red" Font-Bold="true" SetFocusOnError="true" ValidationGroup="PhongBan"
                                Display="Dynamic"> </asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <!-- bo phan -->
                            <tr>
                                <th class="field_label lblText right-align">
                                    <span style="color: red;">*</span> Cơ quan:
                                </th>
                                <td colspan="2">
                                    <asp:DropDownList ID="ddlCoQuan" runat="server" Width="273" DataTextField="TenCoQuan"
                                        DataValueField="CoQuanID" CssClass="selectFrm chosen" >
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                            <td></td>
                            <td colspan="2"> 
                                <asp:CompareValidator runat="server" ID="ddlCoQuan_Compare" ControlToValidate="ddlCoQuan" Type="Integer" ForeColor="Red" Font-Bold="true" 
                            Display="Dynamic"
                                Operator="NotEqual" ErrorMessage="Hãy chọn cơ quan!" ValueToCompare="0" ValidationGroup="PhongBan"></asp:CompareValidator>
                            </td>
                        </tr>
                            <!--  button fields -->
                            <tr>
                                <td>
                                </td>
                                <td colspan="2" align="left">
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="save" Text="Lưu lại"  OnClick="btnSubmit_Click" ValidationGroup="PhongBan" CausesValidation="true"/>
                                    <asp:Button ID="btnReset" runat="server" CssClass="save btn-cancel" Text="Hủy bỏ"  OnClientClick="hideAddEditForm(); return false;" CausesValidation="false"  />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </ul>
            </div>
        </div>
        <div id="fade" class="black_overlay" runat="server" enableviewstate="false">
        </div>
        <div id="fade2" class="black_overlay">
        </div>
        <div class="dashboard">
            <div class="ico_list">
                <label class="h2">
                    Danh mục phòng ban
                    <asp:LinkButton ID="btnThem"  Style="font-size: 12px; float: right;
                        cursor: pointer;" runat="server" CausesValidation="false" OnClientClick="showAddForm(); return false;">
                        <asp:Image ID="Image2" ImageUrl="~/images/add.jpeg" runat="server" CssClass="vertical-align" />
                        <span>Thêm phòng ban</span>
                    </asp:LinkButton>
                </label>
            </div>
            <div class="content-body">
                <!-- message area -->
          
            <div class="pagination" style="float:right; margin-bottom:10px">
                <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnSearch">
                    <asp:TextBox ID="txtSearch" runat="server" Width="265"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" CausesValidation="false" CssClass="save"
                        OnClick="btnSearch_Click" Text="Tìm kiếm" />
                </asp:Panel>
            </div>
            
            <table id="table" class="table" style="margin-top:15px">
                <thead>
                    <tr>
                        <th style="width:200px">
                            Tên cơ quan
                        </th>
                        <th >
                            Tên phòng ban
                        </th>
                        <th>
                            Ghi chú
                        </th>
                        <th style="width:150px">
                            Số điện thoại
                        </th>
                        <th style="width: 70px">
                            Thao tác
                        </th>
                    </tr>
                </thead>
                <asp:Repeater ID="rptPhongBan" runat="server" 
                    OnItemCommand="rptPhongBan_ItemCommand" OnItemDataBound="rptPhongBan_ItemDataBound">
                    <ItemTemplate>
                        <tr class='<%# Container.ItemIndex % 2 == 0 ? "even" : "odd" %>' 
                            <%--<%# SetSelected(Convert.ToInt32(Eval("PhongBanID"))) %>--%>
                            onclick="selectPhongBan(this, <%# Eval("PhongBanID") %>, event)">
                            <td>
                                
                                <asp:Label ID="lblTenCoQuan" runat="server"></asp:Label>
                            </td>
                            <td style="text-align: left;">
                                <%--<asp:Label ID="lblTenCoQuan" runat="server"></asp:Label> --%>
                                <asp:Label ID="lblTenPhongBan" runat="server"></asp:Label>
                            </td>
                            
                            <td style="text-align: left;">
                                <%# Eval("GhiChu") %>
                            </td>
                            <td style="text-align: left;">
                                <%# Eval("SoDienThoai") %>
                                
                            </td>
                            <td style="width: 70px; text-align:center" class="action-cell">
                                
                                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.png" CommandName="Edit" 
                                    CommandArgument='<%# Eval("PhongBanID") %>' CausesValidation="false" OnClientClick=""
                                    ToolTip="Sửa" />
                                <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.png"  CommandName="Delete"
                                    CommandArgument='<%# Eval("PhongBanID") %>' CausesValidation="false" OnClientClick="ConfirmDelete(this); return false; "
                                    ToolTip="Xóa" />
                                <asp:HiddenField ID="hdPhongbanID" runat="server" Value='<%# Eval("PhongBanID") %>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <div class="pagination" style="margin-top:15px; margin-bottom:15px">
                <asp:PlaceHolder ID="plhPaging" runat="server"></asp:PlaceHolder>
            </div>
            <asp:DropDownList ID="ddlPhongBan" runat="server" CssClass="display-none" DataTextField="TenPhongBan"
                DataValueField="PhongBanID" AutoPostBack="true" OnSelectedIndexChanged="ddlPhongBan_SelectedIndexChanged">
            </asp:DropDownList>
                
                <%--<asp:HiddenField ID="hdfPhongBanID" runat="server"/>--%>
            <asp:UpdatePanel ID="udpCanBo" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                 <div class="confirmDiv message" id="thongbaoSucces_div" >
                        <div class="header-message">
                            <label id="lblHeaderSuccess" class="header-message" runat="server">
                                Thông báo
                            </label>
                            <img src="images/close.ico" class="img-close" onclick="hideErrorMessage(this); " />
                        </div>
                        <div class="content-message">
                            <img alt="" src="../images/messagebox_info.png" style="width: 30px; margin-left: 7px; margin-top: 14px;" /><asp:Label ID="lblContentSuccess" CssClass="content-message"
                                runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="confirmDiv message" id="thongbaoError_div" >
                        <div class="header-message">
                             <label id="lblHeaderErr" class="header-message">
                            Lỗi
                        </label>
                            <img src="images/close.ico" class="img-close" onclick="hideErrorMessage(this); " />
                        </div>
                        <div class="content-message">
                            <img alt="" src="../images/Error.png" style="width: 26px; margin-left: 7px; margin-top: 14px;" /><asp:Label ID="lblContentErr" CssClass="content-message" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div id="addition_area" style="width: 100%; margin-top: 15px;">
                        <fieldset class="chiTiet" style="min-height: 200px;">
                            <legend>Thêm cán bộ</legend>
                            <div>
                                <div style="width: auto; float: right; margin-right: 10px; background-color: #fff;">
                                    <asp:LinkButton ID="btnThemCB" OnClientClick="showAddCanBoForm(); return false;" Style="font-size: 12px;
                                        cursor: pointer;" runat="server" CausesValidation="false">
                                        <asp:Image runat="server" ImageUrl="~/images/add.jpeg" Style="vertical-align: top;" />
                                        <span>Thêm cán bộ cho phòng ban</span>
                                    </asp:LinkButton>
                                </div>
                                <div class="addition_function">
                                    <ul id="list_function" style="list-style-type: circle; padding-left: 10px;">
                                        <asp:Repeater ID="rptCanBo" runat="server" >
                                            <ItemTemplate>
                                                <li><span class="list">
                                                    <%# Eval("TenCanBo") %></span>
                                                    <asp:ImageButton ID="btnDelete" CssClass="addition_area_delete_img" ImageUrl="~/images/delete.png"
                                                        runat="server" CommandName="DeleteGroup" CommandArgument='<%# Eval("CanBoID") %>'
                                                        CausesValidation="false" ToolTip="Xóa" OnClientClick="ConfirmDeleteCB(this); return false;" />
                                                    <asp:HiddenField runat="server" Value='<%# Eval("CanBoID") %>' />
                                                </li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ul>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlPhongBan" />
                    <asp:AsyncPostBackTrigger ControlID="btnSaveCB" />
                    <asp:AsyncPostBackTrigger ControlID="btnDeleteCB" />
                </Triggers>
            </asp:UpdatePanel>
                </div>
        </div>
        <!-- end #dashboard -->
    </div>
    <div id="sidebar" class="right">
        <asp:Literal ID="ltrSideMenu" runat="server"></asp:Literal>
    </div>
    <!-- end #sidebar -->
   
    <div style="display: none; z-index: 1002; outline: 0px none; height: auto; width: 350px;
        top: 142px; left: 50%; margin-left:-175px;" class="ui-dialog ui-widget ui-widget-content ui-corner-all ui-draggable popup"
        tabindex="-1" role="dialog" aria-labelledby="ui-dialog-title-delete-form" id="deleteConfirm"
        runat="server">
        <asp:HiddenField ID="hdDeleteID" runat="server" Value="0" />
        <div class="ui-dialog-titlebar ui-widget-header ui-corner-all ui-helper-clearfix">
            <span class="ui-dialog-title" id="ui-dialog-title-delete-form">Thao tác xóa dữ liệu.</span><a
                href="#" class="ui-dialog-titlebar-close ui-corner-all" role="button" onclick="hideDeleteConfirm(); return false;"><span
                    class="ui-icon ui-icon-closethick">close</span></a></div>
        <div style="width: auto; min-height: 0px; " id="delete-form" class="ui-dialog-content ui-widget-content"
            scrolltop="0" scrollleft="0">
            <p>
                <span style="float: left; margin: 0 7px 20px 0;" class="ui-icon ui-icon-alert"></span>
                <span id="delete-form-message">Bạn có chắn chắn muốn xóa phòng ban?</span>
            </p>
        </div>
        <div class="ui-dialog-buttonpane ui-widget-content ui-helper-clearfix">
            <div class="ui-dialog-buttonset">
                <button type="button" class="deldete-button btn-cancel save"
                    role="button" aria-disabled="false" onclick="hideDeleteConfirm();">
                    <span class="ui-button-text">KHÔNG</span>
                </button>
                <%--<asp:Button runat="server" CssClass="button-delete" Text="KHÔNG" OnClick="hideDeleteConfirm()"
                    ID="btnNo"></asp:Button>--%>
                <asp:Button runat="server" CssClass="deleteBtn" Text="CÓ" OnClick="btnDelete_Click" 
                    ID="btnDelete"></asp:Button>
            </div>
        </div>
    </div>
    
    <div style="display: none; z-index: 1002; outline: 0px none; height: auto; width: 350px;
        top: 142px; left: 50%; margin-left:-175px;" class="ui-dialog ui-widget ui-widget-content ui-corner-all ui-draggable"
        tabindex="-1" role="dialog" aria-labelledby="ui-dialog-title-delete-form" id="canboDeleteConfirm"
        runat="server">
        <asp:HiddenField ID="hdDeleteCanBoID" runat="server" Value="0" />
        <div class="ui-dialog-titlebar ui-widget-header ui-corner-all ui-helper-clearfix">
            <span class="ui-dialog-title" id="Span3">Thao tác xóa cán bộ</span><a href="#"
                class="ui-dialog-titlebar-close ui-corner-all" role="button" onclick="hideDeleteCBConfirm(); return false;"><span
                    class="ui-icon ui-icon-closethick">close</span></a></div>
        <div style="width: auto; min-height: 0px;" id="Div3" class="ui-dialog-content ui-widget-content"
            scrolltop="0" scrollleft="0">
            <p>
                <span style="float: left; margin: 0 7px 20px 0;" class="ui-icon ui-icon-alert"></span>
                <span id="Span4">Bạn có chắn chắn muốn xóa cán bộ này khỏi phòng?</span>
            </p>
        </div>
        <div class="ui-dialog-buttonpane ui-widget-content ui-helper-clearfix">
            <div class="ui-dialog-buttonset">
                <button type="button" class="btn-cancel save"
                    role="button" aria-disabled="false" onclick="hideDeleteCBConfirm();">
                    <span class="ui-button-text">KHÔNG</span>
                </button>
                <asp:Button runat="server" CssClass="deleteBtn" Text="CÓ" OnClick="btnDeleteCB_Click" OnClientClick="hideDeleteCBConfirm();"
                    ID="btnDeleteCB" ></asp:Button>
            </div>
        </div>
    </div>
</asp:Content>
