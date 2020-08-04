<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GroupManage.aspx.cs" Inherits="Com.Gosol.CMS.Web.GroupManage" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="script1" runat="server" EnablePartialRendering="true"></asp:ScriptManager>
    <link href="../Styles/dropdownlist/chosen.min.css" rel="stylesheet" />
    <script src="../Scripts/dropdownlist/chosen.jquery.js" type="text/javascript"></script>

    <%--<script src="../AdminLte/jquery.formvalidation/js/formValidation.min.js"></script>
    <script src="../AdminLte/jquery.formvalidation/js/framework/bootstrap.min.js"></script>--%>
    <style type="text/css">
        #MainContent_cblAccessRight label {
            display: inline-block;
            margin-left: 7px;
            margin-top: 2px;
            vertical-align: top;
            line-height: 17px;
        }
    </style>


    <script type="text/javascript">
        //$(function () {
        //    $(window).load(function () {
        //        setTimeout(function () {
        //            $("#thongBaoSuccess").modal("hide");
        //            $("#thongBaoError").modal("hide");
        //        }, 1500);
        //    })
        //});

        //var prm = Sys.WebForms.PageRequestManager.getInstance();
        //prm.add_endRequest(function () {
        //    setTimeout(function () {
        //        $("#thongBaoSuccess").modal("hide");
        //        $("#thongBaoError").modal("hide");
        //    }, 1500);
        //});

        //$(document).ready(function () {
        //    checkValidationAddNhom();
        //});
    </script>

    <script type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_initializeRequest(InitializeRequest);
        prm.add_endRequest(EndRequest);
        var postBackElement;
        function InitializeRequest(sender, args) {
            if (prm.get_isInAsyncPostBack()) {
                args.set_cancel(true);
            }
            $("#pleaseWaitDialog").modal();
        }
        function EndRequest(sender, args) {
            $("#pleaseWaitDialog").modal("hide");
        }

    </script>


    <script type="text/javascript">
        //var dialog_title = "<img src='images/edit-add.png' style='vertical-align:middle;' /> Thêm/Sửa Cán bộ";
        //var checkAvailableUrl = "checkNhanVienAvailable";
        //var saveDbUrl = "saveNhanVien";
        //var fetchDetailUrl = "editNhanVien";

        //function checkValidationAddNhom() {
        //    $('#Form1').formValidation({
        //        framework: 'bootstrap',
        //        //  excluded: ':disabled',
        //        button: {
        //            selector: '#MainContent_btnSubmit',
        //            disabled: 'disabled'
        //        },
        //        icon: {
        //            valid: 'glyphicon glyphicon-ok',
        //            invalid: 'glyphicon glyphicon-remove',
        //            validating: 'glyphicon glyphicon-refresh'
        //        },
        //        fields: {
        //            ctl00$MainContent$txtTenNhom: {
        //                validators: {
        //                    notEmpty: {
        //                        message: 'Tên đăng nhập không được để trống'
        //                    },
        //                }
        //            },
        //        }
        //    });
        //}

        function showAddUserForm() {
            selectedRow = $("tr.selected_hl");
            if (selectedRow.length == 0) {
                //$("#MainContent_error").show();
                //$("#MainContent_lblContentErr").html("Chưa chọn nhóm người dùng!");
                $("#error").modal();
            }
            else {
                $("#addUserForm").modal("show");
                //$("html").animate({ scrollTop: 0 }, 400);

                var config = {
                    '.chosen': {}
                }
                for (var selector in config) {
                    $(selector).chosen(config[selector]);
                }
                $('#MainContent_ddlNguoiDung').trigger("chosen:updated");
                $(".chosen").trigger("chosen:updated");
            }
            return false;
        }

        function showAddChucNangForm() {
            selectedRow = $("tr.selected_hl");
            if (selectedRow.length == 0) {
                $("#error").modal();
            }
            else {
                $("#addChucNangForm").modal();
                $("html").animate({ scrollTop: 0 }, 400);
            }
            return false;
        }

        function hideAddChucNangForm() {
            $("#addChucNangForm").modal("hide");
            $("#MainContent_ddlChucNang").val(0);
            $("#MainContent_cblAccessRight_0").prop('checked', false);
            $("#MainContent_cblAccessRight_1").prop('checked', false);
            $("#MainContent_cblAccessRight_2").prop('checked', false);
            $("#MainContent_cblAccessRight_3").prop('checked', false);

            return false;
        }

        function hideError() {
            $("#error").modal("hide");
        }

        function hideDeleteError() {
            $("#deleteError").modal("hide");
        }

        function hideAddUserForm() {
            $("#addUserForm").modal("hide");
            $("#MainContent_ddlNguoiDung").val(0);
            return false;
        }

        function showAddForm() {
            $("#addNhomNguoiDung").modal("show");

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
            $('#Form1').bootstrapValidator('resetForm', true);
            $("#addNhomNguoiDung").modal("hide");
        }

        function ConfirmDelete(button) {
            $("#deleteConfirm").modal("show");
            $("#MainContent_hdDeleteID").val($(button).next().val());
            return false;
        }

        function hideDeleteConfirm() {
            $("#deleteConfirm").modal("hide");
            $("#MainContent_hdDeleteID").val(0);
        }

        function ConfirmDeleteRole(button) {
            $("#roleDeleteConfirm").modal();
            $("#MainContent_hdDeleteRoleID").val($(button).prev().val());
            return false;
        }

        function hideDeleteRoleConfirm() {
            $("#roleDeleteConfirm").modal("hide");
        }

        function ConfirmDeleteUser(button) {
            $("#userDeleteConfirm").modal("show");
            $("#MainContent_hdDeleteUserID").val($(button).next().val());
            return false;
        }

        function hideDeleteUserConfirm() {
            $("#userDeleteConfirm").modal("hide");
        }


        function showthongBaoSuccess() {
            $("#MainContent_lblContentSuccess").html("Cập nhật dữ liệu thành công.");
            $("#thongBaoSuccess").modal("show");
            return false;
        }

        function hidethongBaoSuccess() {
            $("#thongBaoSuccess").modal("hide");
            return false;
        }

        function showthongBaoError() {
            $("#MainContent_lblContentErr").html("Xảy ra lỗi trong quá trình cập nhật. Vui lòng thử lại sau.");
            $("#thongBaoError").modal();
            return false;
        }

        function hidethongBaoError() {
            $("#thongBaoError").modal("hide");
        }


        function selectGroup(tr, groupID, event) {
            var sender = event.target;
            //add selected style and remove selected style of other rows
            if (sender.tagName == "TD") {
                if (!$(tr).hasClass("selected_hl")) {
                    $(tr).addClass("selected_hl");
                    $(tr).siblings().removeClass("selected_hl");
                    //change hidden ddl value
                    $("#MainContent_ddlNhom").val(groupID);
                    $("#MainContent_ddlNhom").change();
                    $("#MainContent_hdfNhomNguoiDungID").val(groupID);
                }
            }
        }

        function resetAddGroupForm() {
            $("#MainContent_hdNhomNguoiDungID").val("");
            $("#MainContent_txtTenNhom").val("");
            $("#MainContent_txtGhiChu").val("");
            $("#MainContent_ddlCoQuan").val("0");
            hideAddEditForm();
        }

        function StopParentEvent(event) {
            event.stopPropagation();
        }

        function showFormEdit(nhomNguoiDungID) {
            $("#MainContent_hdNhomNguoiDungID").val(nhomNguoiDungID);
            $.ajax({
                type: "POST",
                url: "GroupManage.aspx/GetByNhomNguoiDungID",
                data: '{nhomNguoiDungID:"' + nhomNguoiDungID + '"}',
                dataType: "json",
                async: "true",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    var json = eval('(' + data.d + ')');
                    if (json != null) {
                        console.log(json);
                        $("#MainContent_txtTenNhom").val(json.TenNhom);
                        $("#MainContent_txtGhiChu").val(json.GhiChu);

                        if (json.CoQuanID != null) {
                            $("#MainContent_ddlCoQuan").val(json.CoQuanID);
                        }
                        showAddForm();
                    }
                }
            });
        };
    </script>


    <div class="modal fade" data-backdrop="static" data-keyboard="false" id="pleaseWaitDialog" tabindex="-1" role="dialog" aria-hidden="true" style="padding-top: 15%; overflow-y: visible;">
        <div class="modal-dialog" style="width: 350px">
            <div class="modal-content" style="background-color: rgb(21, 32, 36)">
                <div class="modal-header">
                    <h3 style="margin: 0; color: #fff">Đang xử lý...</h3>
                    <div class="loader"></div>
                </div>
            </div>
        </div>
    </div>

    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="addChucNangForm" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Thêm chức năng cho nhóm</h4>
                    <asp:HiddenField ID="HiddenField3" runat="server"></asp:HiddenField>
                </div>
                <div class="modal-body">
                    <div class="form-horizontal" role="form">
                        <div class="form-group">
                            <label class="col-lg-3 col-sm-3 control-label">Chọn chức năng: <span style="color: red;">*</span></label>
                            <div class="col-lg-9">
                                <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlChucNang" runat="server" DataTextField="TenChucNang" DataValueField="ChucNangID"
                                            CssClass="form-control" Style="width: 100%">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddlNhom" />
                                        <asp:AsyncPostBackTrigger ControlID="btnSaveChucNang" />
                                        <asp:AsyncPostBackTrigger ControlID="rptChucNang" EventName="ItemCommand" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-3 col-sm-3 control-label">Quyền: <span style="color: red;">*</span></label>
                            <div class="col-lg-9">
                                <asp:CheckBoxList ID="cblAccessRight" runat="server">
                                    <asp:ListItem Text="Xem" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Tạo mới" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Sửa" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="Xóa" Value="8"></asp:ListItem>
                                </asp:CheckBoxList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer" style="text-align: center">
                    <asp:Button ID="btnSaveChucNang" runat="server" Text="Lưu lại" CssClass="btn btn-primary btn-sm" OnClick="btnSaveChucNang_Click" CausesValidation="false" />
                    <asp:Button type="button" CssClass="btn btn-danger btn-sm" Text="Hủy" OnClientClick="return hideAddChucNangForm();" align="left" runat="server" ID="Button2" />
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <!--  form edit, add new -->
    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="addNhomNguoiDung" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="form-horizontal" role="form" id="formThemNhom">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title">Thêm, sửa thông tin nhóm</h4>
                        <asp:HiddenField ID="hdNhomNguoiDungID" runat="server"></asp:HiddenField>
                    </div>
                    <div class="modal-body">
                        <div class="form-horizontal" role="form">
                            <div class="form-group">
                                <label class="col-lg-3 col-sm-3 control-label">Tên nhóm: <span style="color: red;">*</span></label>
                                <div class="col-lg-9">
                                    <asp:TextBox ID="txtTenNhom" Style="width: 100%" runat="server" CssClass="validate[required] text-input form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-lg-3 col-sm-3 control-label">Ghi chú:</label>
                                <div class="col-lg-9">
                                    <asp:TextBox ID="txtGhiChu" runat="server" Style="width: 100%" CssClass=" form-control" Rows="3" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-lg-3 col-sm-3 control-label">Cơ quan:</label>
                                <div class="col-lg-9">
                                    <asp:DropDownList ID="ddlCoQuan" runat="server" DataValueField="CoQuanID" DataTextField="TenCoQuan" CssClass="chosen form-control"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer" style="text-align: center">
                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary btn-sm submit" Text="Lưu lại" OnClick="btnSubmit_Click" />
                        <asp:Button ID="btnReset" runat="server" CssClass="btn btn-danger btn-sm" Text="Hủy bỏ" OnClick="btnReset_Click" OnClientClick="resetAddGroupForm(); return false;"
                            CausesValidation="false" />
                    </div>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="addUserForm" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Thêm người dùng vào nhóm</h4>
                    <asp:HiddenField ID="HiddenField2" runat="server"></asp:HiddenField>
                </div>
                <div class="modal-body">
                    <div class="form-horizontal" role="form">
                        <div class="form-group">
                            <label class="col-lg-3 col-sm-3 control-label">Chọn người dùng: <span style="color: red;">*</span></label>
                            <div class="col-lg-9">
                                <asp:UpdatePanel runat="server" UpdateMode="Always" ID="UpdatePanelNguoiDung">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlNguoiDung" runat="server" DataTextField="TenNguoiDung" DataValueField="NguoiDungID"
                                            CssClass="chosen" Style="width: 100%">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer" style="text-align: center">
                    <asp:Button ID="btnSaveUser" runat="server" Text="Lưu lại" CssClass="btn btn-primary btn-sm" OnClick="btnSaveUser_Click" CausesValidation="false" />
                    <asp:Button type="button" CssClass="btn btn-danger btn-sm" Text="Hủy" OnClientClick="return hideAddUserForm();"
                        align="left" runat="server" ID="btnCancel" />
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <div class="content-header">
        <h1>Danh mục nhóm người dùng
        </h1>
        <ol class="breadcrumb">
            <li><a href="/Default.aspx"><i class="fa fa-dashboard"></i>Trang chủ</a></li>
            <li><a href="#">Hệ thống</a></li>
            <li class="active">Danh mục nhóm người dùng</li>
        </ol>
    </div>

    <div class="content">
        <div class="row">
            <div class="col-xs-12">
                <div class="box">
                    <div class="box-header">
                        <button type="button" class="btn btn-primary" id="btnThemNhom" onclick="showAddForm(); return false">
                            <span class="glyphicon glyphicon glyphicon-plus-sign" style="margin-right: 5px"></span>Thêm nhóm người dùng
                        </button>
                    </div>
                    <div class="content-body">
                        <div class="col-xs-12">
                            <!-- message area -->
                            <div class="box-header">
                                <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnSearch">
                                    <asp:Button ID="btnSearch" runat="server" CausesValidation="false" CssClass="btn btn-default btn-sm" Style="float: right; margin-right: 10px; margin-bottom: 10px"
                                        OnClick="btnSearch_Click" Text="Tìm kiếm" />
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control " placeholder="Tìm theo tên nhóm" Style="float: right; margin-right: 10px; margin-bottom: 10px; width: 30%"></asp:TextBox>
                                </asp:Panel>
                            </div>
                            <div class="table-responsive">
                                <asp:HiddenField ID="hdfNhomNguoiDungID" runat="server" Value="0" />
                                <table id="table" class="table table-bordered table-hover" style="margin-top: 15px">
                                    <thead>
                                        <tr>
                                            <th style="width: 35%; text-align:center">Tên nhóm
                                            </th>
                                            <th style="text-align:center">Ghi chú
                                            </th>
                                            <th style="width: 20%; text-align:center">Thao tác
                                            </th>
                                        </tr>
                                    </thead>
                                    <asp:Repeater ID="rptNhom" runat="server" OnItemCommand="rptNhom_ItemCommand" OnItemDataBound="rptNhom_ItemDataBound">
                                        <ItemTemplate>
                                            <%--<asp:LinkButton ID="tr" runat="server" ImageUrl="~/images/edit.png" CommandName="abc" CommandArgument='<%# Eval("NhomNguoiDungID") %>'>--%>
                                            <tr class='<%# Container.ItemIndex % 2 == 0 ? "even" : "odd" %> <%# SetSelected(Convert.ToInt32(Eval("NhomNguoiDungID"))) %>'
                                                onclick="selectGroup(this, <%# Eval("NhomNguoiDungID") %>, event)">
                                                <%--onclick="selectGroup(this, <%# Eval("NhomNguoiDungID") %>, event)"--%>
                                                <td style="text-align: left;">
                                                    <%# Eval("TenNhom") %>
                                                </td>
                                                <td style="text-align: left;">
                                                    <%# Eval("GhiChu") %>
                                                </td>
                                                <td style="text-align: center" class="action-cell">
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.png" CommandName="Edit"
                                                        OnClientClick='<%# "showFormEdit(" + Eval("NhomNguoiDungID") +  "); return false;" %>' ToolTip="Sửa" />
                                                    <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.png" CommandName="Delete"
                                                        CommandArgument='<%# Eval("NhomNguoiDungID") %>' CausesValidation="false" OnClientClick="return ConfirmDelete(this);" ToolTip="Xóa" />
                                                    <asp:HiddenField ID="hdNhomID" runat="server" Value='<%# Eval("NhomNguoiDungID") %>' />

                                                </td>
                                            </tr>
                                            <%--</asp:LinkButton>--%>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                                <div class="paginations" style="margin-top: 15px; margin-bottom: 15px">
                                    <asp:PlaceHolder ID="plhPaging" runat="server"></asp:PlaceHolder>
                                </div>
                            </div>
                            <asp:DropDownList ID="ddlNhom" runat="server" CssClass="form-control" DataTextField="TenNhom" Style="margin-bottom: 5px; display: none"
                                DataValueField="NhomNguoiDungID" AutoPostBack="true" OnSelectedIndexChanged="ddlNhom_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <asp:UpdatePanel runat="server" UpdateMode="Always" ID="udpUserRole">
                            <ContentTemplate>


                                <div class="row" style="margin-top: 15px;">
                                    <div class="col-xs-12">
                                        <div class="col-md-4">
                                            <div class="panel panel-default">
                                                <div class="panel-heading">
                                                    Thêm người dùng
                                        <asp:LinkButton ID="btnThemNguoiDung" Style="font-size: 12px; cursor: pointer; float: right" CausesValidation="false" runat="server" OnClick="showAddUserForm"> <%--OnClientClick="showAddUserForm(); return false;"--%>
                                        <asp:Image runat="server" ImageUrl="~/images/add.jpeg" Style="vertical-align: top;" />
                                        <span>Thêm mới</span> </asp:LinkButton>
                                                </div>
                                                <div class="panel-body">
                                                    <ul id="list_function" style="list-style-type: circle; padding-left: 10px;">
                                                        <asp:Repeater ID="rptNguoiDung" runat="server" OnItemCommand="rptNguoiDung_ItemCommand"
                                                            OnItemDataBound="rptNguoiDung_ItemDataBound">
                                                            <ItemTemplate>
                                                                <li><span class="list">
                                                                    <%# Eval("TenNguoiDung") %></span>
                                                                    <asp:ImageButton ID="btnDelete" CssClass="addition_area_delete_img" ImageUrl="~/images/delete.png"
                                                                        runat="server" CommandName="DeleteUser" CommandArgument='<%# Eval("NguoiDungID") %>'
                                                                        CausesValidation="false" ToolTip="Xóa" OnClientClick="ConfirmDeleteUser(this); return false;" />
                                                                    <asp:HiddenField runat="server" Value='<%# Eval("NguoiDungID") %>' />
                                                                </li>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </ul>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-8">
                                            <div class="panel panel-default">
                                                <div class="panel-heading">
                                                    Thêm chức năng
                                     <asp:LinkButton ID="btnThemChucNang" OnClientClick="showAddChucNangForm(); return false;" Style="font-size: 12px; cursor: pointer; float: right"
                                         runat="server" CausesValidation="false">
                                         <asp:Image ID="Image3" runat="server" ImageUrl="~/images/add.jpeg" Style="vertical-align: top;" />
                                         <span>Thêm mới</span>
                                     </asp:LinkButton>
                                                </div>
                                                <div class="panel-body">
                                                    <ul id="Ul1" style="list-style-type: none;">
                                                        <asp:Repeater ID="rptChucNang" runat="server" OnItemCommand="rptChucNang_ItemCommand"
                                                            OnItemDataBound="rptChucNang_ItemDataBound">
                                                            <ItemTemplate>
                                                                <li class="phanquyen" style="clear: both; margin-left: -50px">
                                                                    <span class='list<%# Eval("Level") + " has-child-" + Eval("HasChild")%>'><%# Eval("TenChucNang") %></span>
                                                                    <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("ChucNangID") %>' />
                                                                    <asp:ImageButton ID="btnDelte" CssClass="addition_area_delete_img" ImageUrl="~/images/delete.png"
                                                                        runat="server" CommandName="DeleteChucNang" CommandArgument='<%# Eval("ChucNangID") %>'
                                                                        CausesValidation="false" ToolTip="Xóa chức năng" OnClientClick="ConfirmDeleteRole(this); return false;" />
                                                                    <asp:ImageButton ID="btnLuu" CssClass="addition_area_delete_img" ImageUrl="~/images/save_img.png"
                                                                        runat="server" CommandName="SaveQuyen" CommandArgument='<%# Eval("ChucNangID") %>'
                                                                        CausesValidation="false" ToolTip="Lưu quyền" />
                                                                    <asp:CheckBoxList ID="cblQuyen" runat="server" CssClass="phanquyen-cbl" RepeatDirection="Horizontal"
                                                                        AutoPostBack="false" ClientIDMode="AutoID">
                                                                        <asp:ListItem Text="Đọc" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="Thêm" Value="2"></asp:ListItem>
                                                                        <asp:ListItem Text="Sửa" Value="4"></asp:ListItem>
                                                                        <asp:ListItem Text="Xóa" Value="8"></asp:ListItem>
                                                                    </asp:CheckBoxList>
                                                                </li>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </ul>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlNhom" />
                                <asp:AsyncPostBackTrigger ControlID="btnSaveUser" />
                                <asp:AsyncPostBackTrigger ControlID="btnSaveChucNang" />
                                <asp:AsyncPostBackTrigger ControlID="btnDeleteUser" />
                                <asp:AsyncPostBackTrigger ControlID="btnDeleteRole" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- end #dashboard -->
    <div id="sidebar" class="right">
        <asp:Literal ID="ltrSideMenu" runat="server"></asp:Literal>
    </div>
    <!-- end #sidebar -->

    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="thongBaoSuccess" class="modal fade">
        <div class="modal-dialog  modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Thông báo!</h4>
                </div>
                <div class="modal-body">
                    <img alt="" src="../images/messagebox_info.png" style="width: 30px; margin-left: 7px;" /><asp:Label ID="lblContentSuccess" CssClass="content-message"
                        runat="server"></asp:Label>
                </div>
                <div class="modal-footer" style="text-align: center">
                    <button type="button" onclick="hidethongBaoSuccess();" class="btn btn-danger btn-sm">
                        Đóng</button>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
    </div>
    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="thongBaoError" class="modal fade">
        <div class="modal-dialog  modal-md">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Thông báo!</h4>
                </div>
                <div class="modal-body">
                    <img alt="" src="../images/Error.png" style="width: 26px; margin-left: 7px;" /><asp:Label ID="lblContentErr" CssClass="content-message" runat="server"></asp:Label>
                </div>
                <div class="modal-footer" style="text-align: center">
                    <button type="button" onclick="hidethongBaoError();" class="btn btn-danger btn-sm">
                        Đóng</button>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
    </div>

    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="deleteError" class="modal fade">
        <div class="modal-dialog  modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Thông báo!</h4>
                </div>
                <div class="modal-body">
                    Dữ liệu đang được sử dụng, không thể xóa
                </div>
                <div class="modal-footer" style="text-align: center">
                    <button type="button" onclick="hideDeleteError();" class="btn btn-danger btn-sm">
                        Đóng</button>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
    </div>

    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="error" class="modal fade">
        <div class="modal-dialog  modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Thông báo!</h4>
                </div>
                <div class="modal-body">
                    <img alt="" src="../images/Error.png" style="width: 26px; margin-left: 7px;" />
                    Chưa chọn nhóm người dùng!
                </div>
                <div class="modal-footer" style="text-align: center">
                    <button type="button" onclick="hideError();" class="btn btn-danger btn-sm">
                        Đóng</button>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
    </div>

    <asp:UpdatePanel runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="success" class="modal fade">
                <div class="modal-dialog  modal-sm">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title">Thông báo!</h4>
                        </div>
                        <div class="modal-body">
                            Quyền đã được lưu!
                        </div>
                        <div class="modal-footer" style="text-align: center">
                            <button type="button" onclick="hideSuccessMsg();" class="btn btn-danger btn-sm">
                                Đóng</button>
                        </div>
                        <!-- /.modal-content -->
                    </div>
                    <!-- /.modal-dialog -->
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="deleteConfirm" class="modal fade">
        <div class="modal-dialog  modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Thông báo!</h4>
                    <asp:HiddenField ID="hdDeleteID" runat="server" Value="0" />
                </div>
                <div class="modal-body">
                    Bạn có chắc chắn muốn xóa dữ liệu?
                </div>
                <div class="modal-footer" style="text-align: center">
                    <asp:Button runat="server" CssClass="btn btn-primary btn-sm" Text="Đồng ý" OnClick="btnDelete_Click" ID="btnDelete"></asp:Button>
                    <button type="button" class="btn btn-danger btn-sm"
                        role="button" aria-disabled="false" onclick="hideDeleteConfirm();">
                        <span class="ui-button-text">Hủy bỏ</span>
                    </button>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
    </div>


    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="roleDeleteConfirm" class="modal fade">
        <div class="modal-dialog  modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Thông báo!</h4>
                    <asp:HiddenField ID="hdDeleteRoleID" runat="server" Value="0" />
                </div>
                <div class="modal-body">
                    Bạn có chắc chắn muốn xóa quyền?
                </div>
                <div class="modal-footer" style="text-align: center">
                    <asp:Button runat="server" CssClass="btn btn-primary btn-sm" Text="Đồng ý" OnClick="btnDeleteRole_Click" ID="btnDeleteRole" OnClientClick="hideDeleteRoleConfirm();"></asp:Button>
                    <button type="button" class="btn btn-danger btn-sm"
                        role="button" aria-disabled="false" onclick="hideDeleteRoleConfirm();">
                        <span class="ui-button-text">Hủy bỏ</span>
                    </button>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
    </div>


    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="userDeleteConfirm" class="modal fade">
        <div class="modal-dialog  modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Thông báo!</h4>
                    <asp:HiddenField ID="hdDeleteUserID" runat="server" Value="0" />
                </div>
                <div class="modal-body">
                    Bạn có chắc chắn muốn xóa người dùng?
                </div>
                <div class="modal-footer" style="text-align: center">
                    <asp:Button runat="server" CssClass="btn btn-primary btn-sm" Text="Đồng ý" OnClick="btnDeleteUser_Click" ID="btnDeleteUser" OnClientClick="hideDeleteUserConfirm();"></asp:Button>
                    <button type="button" class="btn btn-danger btn-sm"
                        role="button" aria-disabled="false" onclick="hideDeleteUserConfirm();">
                        <span class="ui-button-text">Hủy bỏ</span>
                    </button>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
    </div>

    <%--validate--%>
    <link href="/AdminLte/ValidateForm/css/template.css" rel="stylesheet" type="text/css" />
    <link href="/AdminLte/ValidateForm/css/validationEngine.jquery.css" rel="stylesheet" type="text/css" />
    <script src="/AdminLte/ValidateForm/js/jquery.validationEngine.js" type="text/javascript"></script>
    <script src="/AdminLte/ValidateForm/js/jquery.validationEngine-vi.js" type="text/javascript"></script>
    <script type="text/javascript">
        jQuery(document).ready(function () {
            // binds form submission and fields to the validation engine
            jQuery("#Form1").validationEngine();
        });
    </script>
</asp:Content>
