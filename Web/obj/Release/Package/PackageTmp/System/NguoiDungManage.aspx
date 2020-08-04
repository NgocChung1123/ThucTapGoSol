<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NguoiDungManage.aspx.cs" Inherits="Com.Gosol.CMS.Web.NguoiDungManage" EnableEventValidation="false" %>

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
            $(".select2").select2();
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

            //checkValidation();
            setInterval(hideMessage, 2000);
            $("#MainContent_ddlLoai").change(function () {
                if ($("#MainContent_ddlLoai").val() == 1) {
                    $("#MainContent_ddlCoQuanSearch").hide();
                } else {
                    $("#MainContent_ddlCoQuanSearch").show();
                }
                
            });

        });
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            //checkValidation();
  
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
        //var dialog_title = "<img src='images/edit-add.png' style='vertical-align:middle;' /> Thêm/Sửa Cán bộ";
        //var checkAvailableUrl = "checkNhanVienAvailable";
        //var saveDbUrl = "saveNhanVien";
        //var fetchDetailUrl = "editNhanVien";


        // must have for all page
        function initValidationRule(field, rules, i, options) {
            return false;
        }

        function showAddGroupForm() {
            selectedRow = $("tr.selected_hl");
            if (selectedRow.length == 0) {
                $("#error").modal("show");
            }
            else {
                $("#addGroupForm").modal("show");
                $("html").animate({ scrollTop: 0 }, 400);
            }
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
            $('#Form1').bootstrapValidator('resetForm', true);

            $("#addNguoiDungForm").modal("hide");
            $("#MainContent_txtTenNguoiDung").removeAttr('disabled');
            $("#MainContent_txtNguoiDungID").val("");
            $("#MainContent_txtTenNguoiDung").val("");
            $("#MainContent_txtGhiChu").val("");
            $('#MainContent_ddlCoQuan').val(0).trigger('change');
            $('#MainContent_ddlCanBo').val(0).trigger('change');
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
            console.log("confirm");
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
                    selector: '#MainContent_btnSubmit1',
                    disabled: 'disabled'
                },
                //excluded: ':disabled',
                icon: {
                    valid: 'glyphicon glyphicon-ok',
                    invalid: 'glyphicon glyphicon-remove',
                    validating: 'glyphicon glyphicon-refresh'
                },
                fields: {
                    ctl00$MainContent$txtTenNguoiDung: {
                        validators: {
                            notEmpty: {
                                message: 'Tên đăng nhập không được để trống'
                            },
                            regexp: {
                                regexp: /^[a-zA-Z0-9_\.]+$/,
                                message: 'Tên người dùng chỉ có thể bao gồm các chữ cái, số, dấu chấm và gạch dưới'
                            },
                            //remote: {
                            //    url: 'NguoiDung_CheckExists.ashx',
                            //    data: function (validator, $field, value) {
                            //        return {
                            //            fieldValue: validator.getFieldElements('ctl00$MainContent$txtTenNguoiDung').val()
                            //        };            
                            //    },
                            //    valid: false,
                            //    message: 'Tên người dùng đã tồn tại',
                            //    type: 'POST'
                            //}
                        }
                    },
                    ctl00$MainContent$ddlCanBo: {
                        validators: {
                            notEmpty: {
                                message: 'Chọn cán bộ'
                            },
                        }
                    },
                },
            })
        };

        function showFormEdit(nguoiDungID) {
            //alert(nguoiDungID);
            $.ajax({
                type: "POST",
                url: "NguoiDungManage.aspx/GetByNguoiDungID",
                data: '{nguoiDungID:"' + nguoiDungID + '"}',
                dataType: "json",
                async: "true",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    var json = eval('(' + data.d + ')');
                    if (json != null) {
                        //console.log(json);
                        $("#MainContent_hdfNguoiDungID").val(json.NguoiDungID);
                        $("#MainContent_txtTenNguoiDung").val(json.TenNguoiDung);
                        $("#MainContent_txtTenNguoiDung").attr("disabled", "disabled");
                        $("#MainContent_txtGhiChu").val(json.GhiChu);
                        $("#MainContent_ddlTrangThai").val(json.TrangThai);
                        $('#MainContent_ddlCanBo').val(json.CanBoID).change();
                        $('#MainContent_ddlCoQuan').val(json.CoQuanID);

                        $(".chosen").trigger("chosen:updated");
                        showAddForm();
                        //return false;
                    }
                }
            });
        };

        function changeCanBo() {
            var canBoID = $("#MainContent_ddlCanBo").val();
            //console.log('changeCanBo',canBoID);
            //$("#MainContent_ddlCanBo").val(canBoID).trigger("chosen:updated");

            //$("#MainContent_ddlCanBo option[selected]").val(canBoID).text('a');
         
        }

        function getCanBoByCoQuanID() {
            //$('#Form1').bootstrapValidator('resetForm', true);
            var coQuanID = $("#MainContent_ddlCoQuan").val();
            if (coQuanID > 0) {
                //alert(coQuanID);
                $.ajax({
                    type: "POST",
                    url: "NguoiDungManage.aspx/getCanBoByCoQuanID",
                    data: '{coQuanID:"' + coQuanID + '"}',
                    dataType: "json",
                    async: "true",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        var json = eval('(' + data.d + ')');
                        if (json != null) {
                            $('#MainContent_ddlCanBo').empty(); //remove all child nodes
                            var newOption = $('<option value = "">Chọn cán bộ</option>');
                            $('#MainContent_ddlCanBo').append(newOption);
                            for (var i = 0; i < json.length; i++) {
                                var newOption1 = $('<option value="' + json[i].CanBoID + '">' + json[i].TenCanBo + '</option>');
                                $('#MainContent_ddlCanBo').append(newOption1);
                            }
                            $(".chosen").trigger("chosen:updated");
                        }
                    }
                });
            }
            else {
                $.ajax({
                    type: "POST",
                    url: "NguoiDungManage.aspx/getALLCanBo",
                    dataType: "json",
                    async: "true",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        var json = eval('(' + data.d + ')');
                        if (json != null) {
                            $('#MainContent_ddlCanBo').empty(); //remove all child nodes
                            var newOption = $('<option value = "">Chọn cán bộ</option>');
                            $('#MainContent_ddlCanBo').append(newOption);
                            for (var i = 0; i < json.length; i++) {
                                var newOption2 = $('<option value="' + json[i].CanBoID + '">' + json[i].TenCanBo + '</option>');
                                $('#MainContent_ddlCanBo').append(newOption2);
                            }
                            $(".chosen").trigger("chosen:updated");
                        }
                    }
                });
            }
            //checkValidation();
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

    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="refreshConfirm" class="modal fade">
        <div class="modal-dialog  modal-md">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Thông báo!</h4>
                    <asp:HiddenField ID="hdRefreshID" runat="server" />
                </div>
                <div class="modal-body">
                    <span>Bạn có chắn chắn muốn đặt lại mật khẩu cho người dùng này?</span>
                </div>
                <div class="modal-footer" style="text-align: center">
                    <asp:Button runat="server" Text="Đồng ý" ID="btnResetMatKhau" OnClick="btnResetMatKhau_Click" CssClass="btn btn-primary btn-sm" CausesValidation="false" />
                    <button type="button" class="btn btn-danger btn-sm" onclick="hideRefreshConfirm(); return false">Hủy bỏ</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="groupDeleteConfirm" class="modal fade">
        <div class="modal-dialog  modal-md">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Thông báo!</h4>
                    <asp:HiddenField ID="hdDeleteGroupID" runat="server" />
                </div>
                <div class="modal-body">
                    <span>Bạn có chắn chắn muốn xóa nhóm người dùng này?</span>
                </div>
                <div class="modal-footer" style="text-align: center">
                    <asp:Button runat="server" CssClass="btn btn-primary btn-sm" Text="Đồng ý" OnClick="btnDeleteGroup_Click"
                        ID="btnDeleteGroup" OnClientClick="hideDeleteGroupConfirm();"></asp:Button>
                    <button type="button" class="btn btn-danger btn-sm" data-dismiss="modal">Hủy bỏ</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="addGroupForm" class="modal fade">
        <div class="modal-dialog  modal-md">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Thêm nhóm cho User</h4>
                </div>
                <div class="modal-body">
                    <div class="form-horizontal" role="form">
                        <div class="form-group">
                            <label class="col-lg-3 col-sm-3 control-label">Chọn nhóm:</label>
                            <div class="col-lg-9">
                                <asp:DropDownList ID="ddlNhom" runat="server" DataTextField="TenNhom" DataValueField="NhomNguoiDungID"
                                    CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer" style="text-align: center">
                    <asp:Button ID="btnSaveGroup" runat="server" Text="Lưu lại" CssClass="btn btn-primary btn-sm" OnClick="btnSaveGroup_Click"
                        CausesValidation="false" />
                    <%-- OnClientClick="hideAddGroupForm();" --%>
                    <button type="button" class="btn btn-default btn-sm" data-dismiss="modal">Hủy bỏ</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="thongBaoAddSuccess" class="modal fade">
        <div class="modal-dialog  modal-md">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Thông báo!</h4>
                </div>
                <div class="modal-body">
                    <asp:Label ID="lblthongBaoAddSussess" runat="server" Style="color: #008d4c">Thêm mới dữ liệu thành công</asp:Label>
                </div>
                <div class="modal-footer" style="text-align: center">
                    <button type="button" onclick="hideThongBaoAddSuccess();" class="btn btn-default">
                        Đóng</button><br />
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

    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="thongBaoEditSuccess" class="modal fade">
        <div class="modal-dialog  modal-md">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Thông báo!</h4>
                </div>
                <div class="modal-body">
                    <asp:Label ID="Label1" runat="server" Style="color: #008d4c">Cập nhật dữ liệu thành công</asp:Label>
                </div>
                <div class="modal-footer" style="text-align: center">
                    <button type="button" onclick="hideThongBaoEditSuccess();" class="btn btn-default">
                        Đóng</button><br />
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
    </div>
  
    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog"  id="addNguoiDungForm" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Thêm mới/Sửa thông tin người dùng</h4>
                </div>
                <div class="modal-body">
                    <div class="form-horizontal">
                        <%--<div class="form-group">
                            <label class="col-lg-3 col-sm-3 control-label">ID:</label>
                            <div class="col-lg-9">
                                <asp:TextBox ID="txtNguoiDungID" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>--%>
                        <asp:HiddenField ID="hdfNguoiDungID" runat="server" Value="0" />
                        <div class="form-group">
                            <label class="col-lg-3 col-sm-3 control-label">Tên đăng nhập: <span style="color: red;">*</span></label>
                            <div class="col-lg-9">
                                <asp:TextBox ID="txtTenNguoiDung" runat="server" Enabled="true" CssClass="form-control validate[required]"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-3 col-sm-3 control-label">Ghi chú:</label>

                            <div class="col-lg-9">
                                <asp:TextBox ID="txtGhiChu" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-3 col-sm-3 control-label">Chọn cơ quan:</label>
                            <div class="col-lg-9">
                                <asp:DropDownList ID="ddlCoQuan" runat="server" DataTextField="TenCoQuan" CssClass="chosen" DataValueField="CoQuanID"  Style="width: 100%" onchange="getCanBoByCoQuanID(); return false;"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-3 col-sm-3 control-label">Chọn cán bộ: <span style="color: red;">*</span></label>
                            <div class="col-lg-9">
                                <%--<asp:UpdatePanel ID="updatePN1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>--%>
                                        <asp:DropDownList ID="ddlCanBo" onchange="changeCanBo();" runat="server" DataTextField="TenCanBo" CssClass="select2 validate[required]" Style="width: 100%" DataValueField="CanBoID"></asp:DropDownList>
                                    <%--</ContentTemplate>
                                    <Triggers>
                                    </Triggers>
                                </asp:UpdatePanel>--%>
                                
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-3 col-sm-3 control-label">Trạng thái:</span></label>

                            <div class="col-lg-9">
                                <asp:DropDownList ID="ddlTrangThai" runat="server" CssClass="selectFrm form-control">
                                    <asp:ListItem Value="1" Text="Đang hoạt động"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="Bị khóa"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-lg-3">
                            </div>
                            <div class="col-lg-9">
                                <span style="color: red">(*)</span> Tài khoản mới có mật khẩu mặc định là
                                    <%= defaultPassword  %>
                            </div>
                        </div>
                    </div>

                </div>
                <div class="modal-footer" style="text-align: center">
                    <asp:Button ID="btnSubmit1" runat="server" CssClass="btn btn-primary btn-sm" Text="Lưu lại" OnClick="btnSubmit_Click" />
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
                    Mật khẩu của người dùng đã được reset về
            <%= defaultPassword %>
                </div>

                <div class="modal-footer" style="text-align: center">
                    <button type="button" onclick="hideSuccessMsg();"  class="btn btn-default">
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

    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="deleteError" class="modal fade">
        <div class="modal-dialog  modal-md">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Thông báo!</h4>
                    <asp:HiddenField ID="HiddenField3" runat="server" Value="0" />
                </div>
                <div class="modal-body">
                    Dữ liệu đang được sử dụng, không thể xóa
                </div>
                <div class="modal-footer" style="text-align: center">
                    <button type="button" onclick="hideDeleteError();" class="btn btn-default">
                        Đóng</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="error" class="modal fade">
        <div class="modal-dialog  modal-md">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Thông báo!</h4>
                </div>
                <div class="modal-body">
                    Vui lòng chọn một user trước
                </div>
                <div class="modal-footer" style="text-align: center">
                    <button type="button" onclick="hideError();" class="btn btn-sm btn-danger">Đóng</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <!-- Content Header (Page header) -->
    <div class="content-header">
        <h1>Danh mục người dùng
            <%--        <small>(Tổng ng dùng)</small>--%>
        </h1>
        <ol class="breadcrumb">
            <li><a href="/Default.aspx"><i class="fa fa-dashboard"></i>Trang chủ</a></li>
            <li><a href="#">Hệ thống</a></li>
            <li class="active">Danh mục người dùng</li>
        </ol>
    </div>

    <div class="content">
        <div class="row">
            <div class="col-xs-12">
                <div class="box">
                    <div class="box-header text-right">
                        <%--<h3 class="box-title">Hover Data Table</h3>--%>
                            <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnSearch">
                                <div class="col-md-5" style="padding: 5px;">
                                    <asp:DropDownList ID="ddlLoai" runat="server" CssClass="form-control"  AutoPostBack="true" OnSelectedIndexChanged="ddlLoai_SelectedIndexChanged" 
                                        Visible="false">
                                        <asp:ListItem Text="Backend" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Frontend" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2" style="padding: 5px;">
                                    <asp:DropDownList ID="ddlCoQuanSearch" runat="server" DataTextField="TenCoQuan" CssClass="form-control" DataValueField="CoQuanID" AutoPostBack="true" OnSelectedIndexChanged="ddlCoQuanSearch_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-4" style="padding: 5px;">
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placehoder="Nhập tên cán bộ hoặc tên đăng nhập cần tìm kiếm"></asp:TextBox>
                                </div>
                                <div class="col-md-1 text-right" style="padding: 5px; padding-right:0px;">
                                    <asp:Button style="width:100%" ID="btnSearch" runat="server" CausesValidation="false" CssClass="btn btn-default btn-sm" OnClick="btnSearch_Click" Text="Tìm kiếm" />
                                </div>
                            </asp:Panel>
                        <button type="button" class="btn btn-primary" id="" onclick="showAddForm(); return false">
                            <span class="glyphicon glyphicon glyphicon-plus-sign" style="margin-right: 5px"></span>Thêm người dùng
                        </button>
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
                                            <th style="width: 15%; text-align: center">Tên đăng nhập
                                            </th>
                                            <th style="width: 20%; text-align: center">Tên cán bộ
                                            </th>
                                            <th style="width: 20%; text-align: center">Cơ quan
                                            </th>
                                            <th style="width: 15%; text-align: center">Ghi chú
                                            </th>
                                            <th style="width: 15%; text-align: center">Trạng thái
                                            </th>
                                            <th style="width:15%; text-align: center">Thao tác
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="rptNguoiDung" runat="server" OnItemDataBound="rptNguoiDung_ItemDataBound" >
                                            <ItemTemplate>
                                                <tr class='<%# Container.ItemIndex % 2 == 0 ? "even" : "odd" %> <%# SetSelected(Convert.ToInt32(Eval("NguoiDungID"))) %>'
                                                    onclick="selectUser(this, <%# Eval("NguoiDungID") %>, event)">
                                                    
                                                <td style="text-align: center">
                                                    <asp:Label runat="server" ID="lblSTT"></asp:Label>
                                                </td>
                                                    <td style="text-align: left;">
                                                        <%# Eval("TenNguoiDung") %>
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <%# Eval("TenCanBo") %>
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <%# Eval("TenCoQuan") %>
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <%# Eval("GhiChu") %>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblTrangThai" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="text-align: center" class="action-cell">
                                                        <asp:ImageButton ID="btnRefresh" runat="server" ImageUrl="~/images/refresh.png" CommandName="Refresh"
                                                            CommandArgument='<%# Eval("NguoiDungID") %>' CausesValidation="false" OnClientClick="ConfirmRefresh(this); return false;"
                                                            ToolTip="Reset mật khẩu" />
                                                        <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.png" CommandName="Edit"
                                                           OnClientClick='<%# "showFormEdit(" + Eval("NguoiDungID") +  "); return false;" %>'
                                                            ToolTip="Sửa" />
                                                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.png" CommandName="Delete"
                                                            CommandArgument='<%# Eval("NguoiDungID") %>' CausesValidation="false" OnClientClick="ConfirmDelete(this); return false; "
                                                            ToolTip="Xóa" />
                                                        <asp:HiddenField ID="hdNguoiDungID" runat="server" Value='<%# Eval("NguoiDungID") %>' />
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
                            <asp:DropDownList ID="ddlNguoiDung" runat="server" CssClass="display-none form-control" Width="150" Style="margin-left: 10px; display: none" DataTextField="TenNguoiDung"
                                DataValueField="NguoiDungID" AutoPostBack="true" OnSelectedIndexChanged="ddlNguoiDung_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:UpdatePanel ID="udpChucNang" runat="server" UpdateMode="Conditional" style="display:none">
                                <ContentTemplate>
                                    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="mesgAddGroup" class="modal fade">
                                        <div class="modal-dialog  modal-sm">
                                            <div class="modal-content">
                                                <div class="modal-header">
                                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                        <span aria-hidden="true">&times;</span></button>
                                                    <h4 class="modal-title">Thông báo!</h4>
                                                </div>
                                                <div class="modal-body">
                                                    <asp:Label ID="lblMesGroup" runat="server" Style="color: #008d4c"></asp:Label>
                                                </div>
                                                <div class="modal-footer" style="text-align: center">
                                                    <button type="button" onclick="hideMesgAddGroup();" class="btn btn-sm btn-danger">
                                                        Đóng</button><br />
                                                </div>
                                                <!-- /.modal-content -->
                                            </div>
                                            <!-- /.modal-dialog -->
                                        </div>
                                    </div>
                                    <div style="text-align: center">
                                        <asp:Label ID="lblMsg" ForeColor="#008d4c" Text="" Visible="false" runat="server" CssClass="" />
                                    </div>
                                    <div class="panel panel-default">
                                        <div class="panel-heading">
                                            Phân nhóm
                                        <asp:LinkButton ID="btnThemNhom" OnClientClick="showAddGroupForm(); return false;" Style="font-size: 12px; float: right; cursor: pointer;"
                                            runat="server" CausesValidation="false">
                                        <asp:Image runat="server" ImageUrl="~/images/add.jpeg" Style="vertical-align: top;" />
                                        <span>Thêm nhóm cho User</span>
                                        </asp:LinkButton>
                                        </div>
                                        <div class="panel-body">
                                            <ul id="list_function" style="list-style-type: circle; padding-left: 20px;">
                                                <asp:Repeater ID="rptNhom" runat="server" OnItemCommand="rptNhom_ItemCommand" OnItemDataBound="rptNhom_ItemDataBound">
                                                    <ItemTemplate>
                                                        <li><span class="list">
                                                            <%# Eval("TenNhom") %></span>
                                                            <asp:ImageButton ID="btnDelete" CssClass="addition_area_delete_img" ImageUrl="~/images/delete.png"
                                                                runat="server" CommandName="DeleteGroup" CommandArgument='<%# Eval("NhomNguoiDungID") %>'
                                                                CausesValidation="false" ToolTip="Xóa" OnClientClick="ConfirmDeleteGroup(this); return false;" />
                                                            <asp:HiddenField runat="server" Value='<%# Eval("NhomNguoiDungID") %>' />
                                                        </li>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </ul>
                                        </div>
                                        </fieldset>
                                    </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlNguoiDung" />
                                    <asp:AsyncPostBackTrigger ControlID="btnSaveGroup" />
                                    <asp:PostBackTrigger ControlID="btnDeleteGroup" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- end #dashboard -->
    </div>

    <div id="sidebar" class="right">
        <asp:Literal ID="ltrSideMenu" runat="server"></asp:Literal>
    </div>
    <!-- end #sidebar -->
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
