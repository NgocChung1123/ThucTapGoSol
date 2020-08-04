<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DMLoaiTin.aspx.cs" Inherits="Com.Gosol.CMS.Web.Webapp.Backend.QLDanhMuc.DMLoaiTin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="script1" runat="server" EnablePartialRendering="true">
    </asp:ScriptManager>
    <link href="/AdminLte/jquery.formvalidation/css/formValidation.min.css" rel="stylesheet" />
    <%--<link href="/AdminLte/plugins/select2/select2.min.css" rel="stylesheet" />--%>

    <script src="/AdminLte/jquery.formvalidation/js/formValidation.min.js"></script>
    <script src="/AdminLte/jquery.formvalidation/js/framework/bootstrap.min.js"></script>
    <%--<script src="/AdminLte/plugins/select2/select2.min.js" type="text/javascript"></script>--%>

    <link href="/Styles/dropdownlist/chosen.min.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/dropdownlist/chosen.jquery.js" type="text/javascript"></script>

    <script type="text/javascript">
        function hideMessage() {
            var messageDiv = $("#<%= lblMsg.ClientID %>");
            if (messageDiv.is(":visible")) {
                setTimeout(function () {
                    messageDiv.hide(300);
                }, 2000);
            }
        }

        $(document).ready(function () {
            $(".js-example-basic-single").select2({
            });
            //$(".selectCoQuan").select2({
            //    placeholder: "Chọn cơ quan",
            //});

            //$(".selectCanBo").select2({
            //    placeholder: "Chọn cán bộ",
            //});

            var config = {
                '.chosen': {}
            }
            for (var selector in config) {
                $(selector).chosen(config[selector]);

            }
            $(".chosen").trigger("chosen:updated");

            checkValidation();
            setInterval(hideMessage, 2000);

        });
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            checkValidation();

            setTimeout(function () {
                $("#successSubmit").modal("hide");
                $("#error").modal("hide");
                $("#mesgAddGroup").modal("hide");
                $("#thongBaoAddSuccess").modal("hide");
                $("#thongBaoEditSuccess").modal("hide");
                $("#thongBaoDeleteError").modal("hide");
            }, 2000);
        });

        $(function () {
            $(window).load(function () {
                setTimeout(function () {
                    $("#successSubmit").modal("hide");
                    $("#error").modal("hide");
                    $("#mesgAddGroup").modal("hide");
                    $("#thongBaoAddSuccess").modal("hide");
                    $("#thongBaoEditSuccess").modal("hide");
                    $("#thongBaoDeleteError").modal("hide");
                }, 2000);
            })
        });
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
        function initValidationRule(field, rules, i, options) {
            return false;
        }

        function hideError() {
            $("#error").modal("hide");
        }

        function hideDeleteError() {
            $("#deleteError").modal("hide");
        }


        function hideSubmitError() {
            $("#submitError").modal("hide");
        }

        function hideAddGroupForm() {
            $("#addGroupForm").modal("hide");
        }

        function showAddForm() {

            $("#addNguoiDungForm").modal("show");
            return false;
        }

        function hideAddEditForm() {

            loadForm();
            $('#Form1').bootstrapValidator('resetForm', true);
        }

        function showthongBaoSuccess() {
            $("#successSubmit").modal();
            return false;
        }
        function showThongBaoAddSuccess() {
            $("#thongBaoAddSuccess").modal();
            return false;
        }
        function showthongBaoEditSuccess() {
            $("#thongBaoEditSuccess").modal();
            return false;
        }

        function hideThongBaoAddSuccess() {
            $("#thongBaoAddSuccess").modal();
            return false;
        }
        function hidethongBaoEditSuccess() {
            $("#thongBaoEditSuccess").modal();
            return false;
        }


        function hideSuccessSubmit() {
            $("#successSubmit").modal("hide");

        }

        function showthongBaoError() {
            $("#thongBaoError").modal();
            return false;
        }
        function hidethongBaoError() {
            $("#thongBaoError").modal("hide");
        }

        function showthongBaoDeleteError() {
            $("#thongBaoDeleteError").modal();
            return false;
        }
        function hidethongBaoDeleteError() {
            $("#thongBaoDeleteError").modal("hide");
        }



        function hideSuccessMsg() {
            $("#success").modal("hide");
        }

        function ConfirmDelete(button) {
            $("#deleteConfirm").modal();
            $("#MainContent_hdDeleteID").val($(button).next().val());
            return false;
        }

        function hideDeleteConfirm() {
            $("#deleteConfirm").modal("hide");
            $("#MainContent_hdDeleteID").val(0);
        }

        function ConfirmRefresh(button) {
            $("#refreshConfirm").modal("show");
            $("#MainContent_hdRefreshID").val($(button).next().next().next().val());
            return false;
        }

        function hideRefreshConfirm() {
            $("#refreshConfirm").modal("hide");
            $("#MainContent_hdRefreshID").val(0);
        }

        function ConfirmDeleteGroup(button) {
            $("#groupDeleteConfirm").modal("show");
            $("#MainContent_hdDeleteGroupID").val($(button).next().val());
            return false;
        }

        function hideDeleteGroupConfirm() {
            $("#groupDeleteConfirm").modal("hide");
        }

        function selectUser(tr, userID, event) {
            var sender = event.target;
            if (sender.tagName == "TD") {
                //add selected style and remove selected style of other rows
                $(tr).addClass("selected_hl");
                $(tr).siblings().removeClass("selected_hl");
                //change hidden ddl value
                $("#MainContent_ddlNguoiDung").val(userID);
                $("#MainContent_ddlNguoiDung").change();
            }
        }

        function StopParentEvent(event, control) {
            if (!$(control).parent().parent().hasClass("selected_hl")) {
                event.stopPropagation();
            }
        }


        function showLogIn() {
            $("#loginModal").modal();
        };

        function showMesGroup() {
            $("#mesgAddGroup").modal();
        };
        function hideMesgAddGroup() {
            $("#mesgAddGroup").modal("hide");
        };

        function checkValidation() {

            $('#Form1').formValidation({
                framework: 'bootstrap',
                button: {
                    selector: '#MainContent_btnSave',
                    disabled: 'disabled'
                },
                icon: {
                    valid: 'glyphicon glyphicon-ok',
                    invalid: 'glyphicon glyphicon-remove',
                    validating: 'glyphicon glyphicon-refresh'
                },
                fields: {
                    ctl00$MainContent$txtLoaiTin: {
                        validators: {
                            notEmpty: {
                                message: 'Vui lòng nhập tên loại tin'
                            },
                            stringLength: {
                                max: 500,
                                message: 'Vui lòng không nhập quá 500 ký tự'
                            }
                        },
                    },
                    ctl00$MainContent$txtOrder: {
                        validators: {
                            notEmpty: {
                                message: 'Sắp xếp không được bỏ trống'
                            },
                            regexp: {
                                regexp: /^[0-9]+$/,
                                message: 'Sắp xếp chỉ có thể bao gồm các số'
                            },
                        },
                    },
                    ctl00$MainContent$ddlLoaiTin: {
                        validators: {
                            notEmpty: {
                                message: 'Vui lòng chọn loại tin cha'
                            },
                        }
                    },
                },
            })
        };

        function loadForm() {
            $("#MainContent_txtLoaiTin").val("");
            $("#MainContent_txtGhiChu").val("");
            $("#MainContent_txtOrder").val("");
            $('#MainContent_ddlLoaiTin').val(0).trigger("chosen:updated");
            $("#MainContent_checkPublic").removeAttr('checked');
        }

        function showFormEdit(idLoaiTin) {

            $.ajax({
                type: "POST",
                url: "DMLoaiTin.aspx/GetByID",
                data: '{idLoaiTin:"' + idLoaiTin + '"}',
                dataType: "json",
                async: "true",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    debugger;
                    loadForm();
                    var json = eval('(' + data.d + ')');
                    if (json != null) {
                        console.log(json);
                        $("#MainContent_hdfIDLoaiTinEdit").val(json.IDLoaiTin);

                        $("#MainContent_txtLoaiTin").val(json.TenLoaiTin);
                        $("#MainContent_txtGhiChu").val(json.GhiChu);
                        $("#MainContent_txtOrder").val(json.Order);
                        $("#MainContent_ddlLoaiTin").val(json.ParentID);
                        if (json.Public == true) {
                            $('#MainContent_checkPublic').prop('checked', true);
                        }

                        $(".chosen").trigger("chosen:updated");
                        showAddForm();
                    }
                }
            });
        };

    </script>

    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="deleteConfirm" class="modal fade">
        <div class="modal-dialog  modal-md">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Thông báo!</h4>
                    <asp:HiddenField ID="hdDeleteID" runat="server" Value="0" />
                </div>
                <div class="modal-body">
                    <span>Bạn có chắn chắn muốn xóa?</span>
                </div>
                <div class="modal-footer" style="text-align: center">
                    <%--<asp:Button runat="server" CssClass="btn btn-primary btn-sm"  OnClick="btnDelete_Click" ID="btnDelete"></asp:Button>--%>
                    <asp:Button ID="btnDeleteUser" runat="server" Text="Đồng ý" CssClass="btn btn-primary btn-sm" OnClick="btnDeleteUser_Click" />
                    <button type="button" class=" btn btn-danger btn-sm" role="button" aria-disabled="false" onclick="hideDeleteConfirm();">
                        <span class="ui-button-text">Hủy bỏ</span>
                    </button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
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
                    <img alt="" src="../images/Error.png" style="width: 26px; margin-left: 7px;" />
                    <asp:Label ID="lblThongBaoError" runat="server" Style="color: red"></asp:Label>
                </div>
                <div class="modal-footer" style="text-align: center">
                    <button type="button" onclick="hidethongBaoError();" class="btn btn-danger btn-sm">
                        Đóng</button><br />
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
    </div>
    <asp:HiddenField runat="server" ID="hdfIDLoaiTinEdit" />
    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" id="addNguoiDungForm" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Thêm mới/Sửa thông tin loại tin</h4>
                </div>
                <div class="modal-body">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <label class="col-lg-3 col-sm-3 control-label">Tên loại tin: <span style="color: red;">*</span></label>
                            <div class="col-lg-9">
                                <asp:TextBox ID="txtLoaiTin" runat="server" Enabled="true" CssClass="form-control" Rows="3" TextMode="MultiLine" MaxLength="490"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-3 col-sm-3 control-label">Ghi chú:</label>

                            <div class="col-lg-9">
                                <asp:TextBox ID="txtGhiChu" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-3 col-sm-3 control-label">Sắp xếp: <span style="color: red;">*</span></label>

                            <div class="col-lg-9">
                                <asp:TextBox ID="txtOrder" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group" style="display: none">
                            <label class="col-lg-3 col-sm-3 control-label">Loại tin cha:</label>
                            <div class="col-lg-9">
                                <asp:DropDownList ID="ddlLoaiTin" runat="server" DataTextField="TenLoaiTin" CssClass="chosen" DataValueField="IDLoaiTin" Style="width: 100%"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-3 col-sm-3 control-label">Hiển thị:</label>

                            <div class="col-lg-9">
                                <asp:CheckBox runat="server" ID="checkPublic" Checked="true" />
                            </div>
                        </div>
                    </div>

                </div>
                <div class="modal-footer" style="text-align: center">
                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary btn-sm" Text="Lưu lại" OnClick="btnSave_Click" />
                    <button type="button" class="btn btn-default btn-sm" data-dismiss="modal" onclick="hideAddEditForm(); return false">Hủy bỏ</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="successRSPass" class="modal fade">
        <div class="modal-dialog  modal-md">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Thông báo!</h4>
                    <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
                </div>
                <div class="modal-body">
                </div>

                <div class="modal-footer" style="text-align: center">
                    <button type="button" onclick="hideSuccessMsg();" class="btn btn-default">
                        Đóng</button><br />
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="successSubmit" class="modal fade">
        <div class="modal-dialog  modal-md">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Thông báo!</h4>
                </div>
                <div class="modal-body">
                    <asp:Label ID="lblContentSuccess" runat="server" Style="color: #008d4c"></asp:Label>
                </div>
                <div class="modal-footer" style="text-align: center">
                    <button type="button" onclick="hideSuccessSubmit();" class="btn btn-default">
                        Đóng</button><br />
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
    </div>

    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="submitError" class="modal fade">
        <div class="modal-dialog  modal-md">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Thông báo!</h4>
                </div>
                <div class="modal-body">
                    <asp:Label ID="txtError" runat="server"></asp:Label><div class="jquery-msgbox-buttons">
                    </div>
                    <div class="modal-footer" style="text-align: center">
                        <button type="button" onclick="hideSubmitError();" class="btn btn-default">
                            Đóng</button>
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
    </div>

    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="thongBaoDeleteError" class="modal fade">
        <div class="modal-dialog  modal-md">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Thông báo!</h4>
                </div>
                <div class="modal-body">
                    <img alt="" src="../images/Error.png" style="width: 26px; margin-left: 7px;" />
                    <asp:Label ID="lblthongBaoDeleteError" runat="server" Style="color: red"></asp:Label>
                </div>
                <div class="modal-footer" style="text-align: center">
                    <button type="button" onclick="hideSubmitError();" class="btn btn-default">
                        Đóng</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <!-- Content Header (Page header) -->
    <div class="content-header">
        <h1>Danh mục loại tin tức
            <%--        <small>(Tổng ng dùng)</small>--%>
        </h1>
        <ol class="breadcrumb">
            <li><a href="/Default.aspx"><i class="fa fa-dashboard"></i>Trang chủ</a></li>
            <li><a href="#">Quản lý tin tức</a></li>
            <li class="active">QL loại tin tức</li>
        </ol>
    </div>

    <div class="content">
        <div style="display: none;">
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="box">
                    <div class="box-header" style="margin-top: 10px">
                        <asp:Panel runat="server" DefaultButton="btnSearch" style="padding-right: 0px;">
                            <div class="col-lg-4 col-md-offset-7">
                                <asp:TextBox ID="txtSearch" placeholder="Nhập tên loại tin tức cần tìm kiếm" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-lg-1" style="padding-left: 5px; margin-right: 0px; padding-right: 0px">
                                <asp:Button ID="btnSearch" runat="server" CausesValidation="false" CssClass="btn btn-default btn-sm" OnClick="btnSearch_Click" Text="Tìm kiếm" Style="width:100%" />
                            </div>
                        </asp:Panel>
                        <div class="col-md-3 col-md-offset-9 text-right" style="padding-right: 0px; margin-top: 10px">
                            <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary" Text="Thêm loại tin tức" OnClientClick="showAddForm(); return false" />
                     <%--       <button type="button" class="btn btn-primary" id="" onclick="showAddForm(); return false">
                                <span class="glyphicon glyphicon glyphicon-plus-sign" style="margin-right: 5px"></span>Thêm loại tin tức
                            </button>--%>
                        </div>
                    </div>
                    <div class="content-body">
                        <div class="box-body">
                            <!-- message area -->
                            <div class="table-responsive">
                                <table id="table" class="table table-bordered table-hover" style="margin-top: 15px; width: 100%">
                                    <thead>
                                        <tr>
                                            <th style="width: 5%; text-align: center">STT
                                            </th>
                                            <th style="width: 35%; text-align: center">Tên loại tin
                                            </th>
                                            <th style="width: 35%; text-align: center">Ghi chú
                                            </th>
                                            <th style="width: 15%; text-align: center">Trạng thái
                                            </th>
                                            <th style="width: 10%; text-align: center;">Thao tác
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="rptLoaiTin" runat="server" OnItemDataBound="rptLoaiTin_ItemDataBound">
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="text-align: center">
                                                        <asp:Label runat="server" ID="lblSTT"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <%# Eval("TenLoaiTin") %>
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <%# Eval("GhiChu") %>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblTrangThai" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="text-align: center" class="action-cell">
                                                        <asp:ImageButton ID="btnEdit" runat="server" CommandName="Edit" ImageUrl="~/images/edit.png"
                                                            OnClientClick='<%# "showFormEdit(" + Eval("IDLoaiTin") +  "); return false;" %>'
                                                            ToolTip="Sửa" Width="20px" Style="margin-right: 5px;" />
                                                        <asp:ImageButton ID="btnDelete" runat="server" CommandName="Delete" CausesValidation="false"
                                                            OnClientClick="ConfirmDelete(this); return false;" ImageUrl="~/images/delete.png"
                                                            ToolTip="Xóa" Width="20px" />
                                                        <asp:HiddenField ID="hdfIDLoaiTin" runat="server" Value='<%# Eval("IDLoaiTin") %>' />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                                <div class="paginations" style="margin-top: 15px; margin-bottom: 15px">
                                    <asp:PlaceHolder ID="plhPaging" runat="server"></asp:PlaceHolder>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- end #dashboard -->
    </div>
</asp:Content>
