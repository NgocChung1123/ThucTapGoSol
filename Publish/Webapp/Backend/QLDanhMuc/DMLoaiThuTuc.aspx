<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="DMLoaiThuTuc.aspx.cs" Inherits="Com.Gosol.CMS.Web.Webapp.Backend.QLDanhMuc.DMLoaiThuTuc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager runat="server" EnablePartialRendering="true">
    </asp:ScriptManager>

    <link href="/AdminLte/jquery.formvalidation/css/formValidation.min.css" rel="stylesheet" />
    <%--<link href="/AdminLte/plugins/select2/select2.min.css" rel="stylesheet" />--%>

    <script src="/AdminLte/jquery.formvalidation/js/formValidation.min.js"></script>
    <script src="/AdminLte/jquery.formvalidation/js/framework/bootstrap.min.js"></script>
    <%--<script src="/AdminLte/plugins/select2/select2.min.js" type="text/javascript"></script>--%>

    <link href="/Styles/dropdownlist/chosen.min.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/dropdownlist/chosen.jquery.js" type="text/javascript"></script>
    <script>
        $(document).ready(function () {
            var config = {
                '.chosen': {}
            }
            for (var selector in config) {
                $(selector).chosen(config[selector]);

            }
            $(".chosen").trigger("chosen:updated");

            checkValidation();
            setInterval(hideMessage, 2000);

            $("#MainContent_file_upload").on('change', function () {
                var file = $('#MainContent_file_upload')[0].files[0]
                if (file) {
                    console.log(file);
                    $("#MainContent_file_name").val(file.name);
                }
            });

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

        function hideMessage() {
            var messageDiv = $("#<%= lblMsg.ClientID %>");
            if (messageDiv.is(":visible")) {
                setTimeout(function () {
                    messageDiv.hide(300);
                }, 2000);
            }
        }

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
                    ctl00$MainContent$txtLoaiThuTuc: {
                        validators: {
                            notEmpty: {
                                message: 'Vui lòng nhập tên loại loại thủ tục'
                            },
                        },
                    },

                    ctl00$MainContent$ddlLoaiThuTuc: {
                        validators: {
                            notEmpty: {
                                message: 'Vui lòng chọn loại thủ tục cha'
                            },
                        }
                    },
                    ctl00$MainContent$file_name: {
                        validators: {
                            notEmpty: {
                                message: 'Vui lòng chọn tệp đính kèm'
                            },
                        }
                    },
                    ctl00$MainContent$txtNameFile: {
                        validators: {
                            notEmpty: {
                                message: 'Vui lòng nhập tên file hướng dẫn'
                            },
                        }
                    },
                },
            })
        };

        function showthongBaoSuccess() {
            $("#successSubmit").modal();
            return false;
        }

        function resetForm() {
            $("#tableCon > tbody").remove();
        }

        function showFormEdit(id) {
            $.ajax({
                type: "POST",
                url: "DMLoaiThuTuc.aspx/GetByID",
                data: '{id:"' + id + '"}',
                dataType: "json",
                async: "true",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    //debugger;
                    loadForm();
                    var json = eval('(' + data.d + ')');
                    if (json != null) {
                        console.log(json);

                        $("#MainContent_hdfIDEdit").val(json.LoaiThuTucID);

                        $("#MainContent_txtLoaiThuTuc").val(json.TenLoaiThuTuc);
                        $("#MainContent_txtGhiChu").val(json.GhiChu);
                        $("#MainContent_ddlLoaiThuTuc").val(json.ParentID);
                        if (json.Public == true) {
                            $('#MainContent_checkPublic').prop('checked', true);
                        }
                        $("#MainContent_file_name").attr("ReadOnly", "true");

                        if (json.FileUrl != null && json.FileUrl != "") {
                            $("#MainContent_file_name").val(json.FileUrl);
                        }
                        $("#MainContent_txtNameFile").val(json.FileName);
                        $(".chosen").trigger("chosen:updated");
                        showEditForm();

                    }
                }
            });
        };
    </script>

    <script>
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

    <script>
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
            loadForm();
            $("#myModal").modal("show");
            return false;
        }

        function showEditForm() {
            $("#myModal").modal("show");
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

        function StopParentEvent(event, control) {
            if (!$(control).parent().parent().hasClass("selected_hl")) {
                event.stopPropagation();
            }
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

        function ConfirmDelete(button, type) {
            //debugger;
            $("#deleteConfirm").modal("show");
            var value;
            if (type == 'html') {
                value = $('#btnDeleteCon').attr('myId');
            }
            if (type == 'asp') {
                value = $(button).next().val();
            }
            $("#MainContent_hdDeleteID").val(value);

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

        function showMesGroup() {
            $("#mesgAddGroup").modal();
        };

        function hideMesgAddGroup() {
            $("#mesgAddGroup").modal("hide");
        };

        function loadForm() {
            $("#MainContent_txtLoaiThuTuc").val("");
            $("#MainContent_txtGhiChu").val("");
            $('#MainContent_ddlLoaiThuTuc').val(0).trigger("chosen:updated");
            $("#MainContent_checkPublic").removeAttr('checked');
            $("#MainContent_file_name").val("");
            $("#MainContent_file_name").attr("ReadOnly", "true");
            $("#MainContent_txtNameFile").val("");

        }
    </script>

    <div class="content-header">
        <h1>Danh mục loại thủ tục</h1>
        <ol class="breadcrumb">
            <li><a href="/Default.aspx"><i class="fa fa-dashboard"></i>Trang chủ</a></li>
            <li><a href="#">Danh mục</a></li>
            <li class="active">Danh mục loại thủ tục</li>
        </ol>
    </div>

    <div class="content">
        <div style="display: none;">
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
        <div class="row">
            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" style="padding-right: 5px;">
                <div class="col-xs-12">
                    <div class="box">
                        <div class="box-header">
                            <div class="col-md-3" style="padding-left: 0px;">
                                <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary" Text="Thêm loại thủ tục" OnClientClick="showAddForm(); return false"  />
                             <%--   <button type="button" class="btn btn-primary" id="" onclick="showAddForm(); return false">
                                    <span class="glyphicon glyphicon glyphicon-plus-sign" style="margin-right: 5px;"></span>Thêm loại thủ tục
                                </button>--%>
                            </div>
                        </div>
                        <div class="content-body">
                            <div class="box-body">
                                <!-- message area -->
                                <div class="box-header" style="padding: 0px;">
                                    <div class="col-lg-4" style="padding: 0px">
                                        <asp:TextBox ID="txtSearchCha" placeholder="Nhập tên loại thủ tục, loại thủ tục cha" runat="server" AutoPostBack="True" CssClass="form-control" OnTextChanged="txtSearchCha_TextChanged"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-4" style="padding-left: 5px">
                                        <asp:Button ID="btnSearchCha" runat="server" CausesValidation="false" CssClass="btn btn-default btn-sm" OnClick="btnSearchCha_Click" Text="Tìm kiếm" />
                                    </div>
                                </div>
                                <div class="table-responsive">
                                    <table id="tableCha" class="table table-bordered table-hover" style="margin-top: 15px; width: 100%">
                                        <thead>
                                            <tr>
                                                <th style="text-align: center; width: 5%;">STT</th>
                                                <th style="text-align: center; width: 20%;">Tên loại thủ tục
                                                </th>
                                                <th style="text-align: center; width: 20%">Loại thủ tục cha
                                                </th>
                                                <th style="text-align: center; width: 15%">Ghi chú
                                                </th>
                                                <th style="text-align: center; width: 10%">Trạng thái
                                                </th>
                                                <th style="text-align: center; width: 20%">Tệp hưỡng dẫn
                                                </th>
                                                <th style="text-align: center; width: 15%">Thao tác
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="rptLoaiThuTucCha" runat="server" OnItemDataBound="rptLoaiThuTucCha_ItemDataBound">
                                                <ItemTemplate>
                                                    <tr style="cursor: pointer;">
                                                        <td><%# Container.ItemIndex + 1 %></td>
                                                        <td style="text-align: left;">
                                                            <%# Eval("TenLoaiThuTuc") %>
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <%# Eval("parent_name") %>
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <%# Eval("GhiChu") %>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblTrangThaiCha" runat="server"></asp:Label>
                                                        </td>
                                                        <td style="text-align: center;">
                                                            <%# Com.Gosol.CMS.Utility.Utils.ConvertToString(Eval("FileUrl"),string.Empty)=="" 
                                                        ? "<a onclick='lol();'; return false;';><img src='"+"../../../images/download.png"
                                        +"' style='"+"width: 20px; height: 20px"+"'/>"
                                        :
                                        "<a href = '../../../Handler/DownloadFileQuyetDinh.ashx?filename="+Eval("FileUrl") + "&zz=FileWF'><img src='"+"../../../images/download.png"
                                        +"' style='"+"width: 20px; height: 20px"+"'/>" %>
                                                        </td>
                                                        <td style="text-align: center" class="action-cell">
                                                            <asp:ImageButton ID="btnEditParent" runat="server" ImageUrl="~/images/edit.png" CommandName="Edit"
                                                                OnClientClick='<%# "showFormEdit(" + Eval("LoaiThuTucID") +  "); return false;" %>'
                                                                ToolTip="Sửa" Width="20px" Style="margin-right: 5px;" />
                                                            <asp:ImageButton ID="btnDeleteParent" runat="server" ImageUrl="~/images/delete.png" CommandName="Delete" CausesValidation="false"
                                                                CommandArgument='<%# Eval("LoaiThuTucID") %>' OnClientClick="ConfirmDelete(this, 'asp') ; return false;"
                                                                ToolTip="Xóa" Width="20px" />
                                                            <%--<asp:LinkButton ID="btnEditParent" runat="server" CommandName="Edit" CssClass="btn btn-primary btn-sm" OnClientClick='<%# "showFormEdit(" + Eval("LoaiThuTucID") +  "); return false;" %>'>Sửa</asp:LinkButton>--%>
                                                            <%--<asp:LinkButton ID="btnDeleteParent" runat="server" CommandName="Delete" CausesValidation="false" OnClientClick="ConfirmDelete(this, 'asp'); return false;" CssClass="btn btn-primary btn-sm">Xóa</asp:LinkButton>--%>
                                                            <asp:HiddenField ID="hdfIDLoaiThuTucCha" runat="server" Value='<%# Eval("LoaiThuTucID") %>' />
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
        </div>
        <!-- end #dashboard -->
    </div>
    <asp:HiddenField runat="server" ID="hdfIDEdit" />

    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" id="myModal" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Thêm mới/Sửa thông tin loại thủ tục</h4>
                </div>
                <div class="modal-body">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <label class="col-lg-3 col-sm-3 control-label">Tên loại thủ tục: <span style="color: red;">*</span></label>
                            <div class="col-lg-9">
                                <asp:TextBox ID="txtLoaiThuTuc" runat="server" Enabled="true" CssClass="form-control"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator runat="server" id="reqName" controltovalidate="txtLoaiThuTuc" ForeColor="Red" errormessage="Vui lòng nhập tên loại thủ tục!" />--%>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-lg-3 col-sm-3 control-label">Chọn file <span style="color: red">*</span></label>
                            <div class="col-lg-9">
                                <asp:FileUpload ID="file_upload" runat="server" CssClass="file_dinhkem" Style="width: 284px !important; margin-top: 5px;" />
                                <asp:TextBox ID="txt_fileurl" runat="server" CssClass="txt_fileurl hidden" />
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-lg-3 col-sm-3 control-label">
                                Tên file
                            </div>
                            <div class="col-lg-9">
                                <asp:TextBox runat="server" ID="file_name" CssClass="form-control" ReadOnly="false" />
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-lg-3 col-sm-3 control-label">
                                Tên tệp hưỡng dẫn <span style="color: red">*</span>
                            </div>
                            <div class="col-lg-9">
                                <asp:TextBox runat="server" ID="txtNameFile" MaxLength="50" CssClass="form-control" />
                                <span style="color: blue;">Giới hạn 50 ký tự</span>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-3 col-sm-3 control-label">Ghi chú:</label>

                            <div class="col-lg-9">
                                <asp:TextBox ID="txtGhiChu" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-3 col-sm-3 control-label">Loại thủ tục cha:</label>
                            <div class="col-lg-9">
                                <asp:DropDownList ID="ddlLoaiThuTuc" runat="server" DataTextField="TenLoaiThuTuc" CssClass="chosen form-control" DataValueField="LoaiThuTucID" Style="width: 100%"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-lg-3 col-sm-3 control-label">Hiển thị:</label>

                            <div class="col-lg-9">
                                <asp:CheckBox runat="server" ID="checkPublic" />
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
                    <asp:Button ID="btnDelete" runat="server" Text="Đồng ý" CssClass="btn btn-primary btn-sm" OnClick="btnDelete_Click" />
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
                    <button type="button" onclick="hideSuccessMsg();">
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
                    <button type="button" onclick="hideSuccessSubmit();">
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
                        <button type="button" onclick="hideSubmitError();">
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
                    <button type="button" onclick="hideSubmitError();">
                        Đóng</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="successSubmit1" class="modal fade">
        <div class="modal-dialog  modal-md">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Thông báo!</h4>
                </div>
                <div class="modal-body">
                    <label style="color: #008d4c">Chưa có file hướng dẫn</label>
                </div>
                <div class="modal-footer" style="text-align: center">
                    <button type="button" onclick="hideSuccessSubmit1();" class="btn btn-default">
                        Đóng</button><br />
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
    </div>

    <link href="/Styles/uploadfile/uploadify.css" rel="stylesheet" />
    <script src="/scripts/uploadfile/jquery.uploadify.js" type="text/javascript"></script>
    <script>
        $(document).ready(function () {
            $('#txtSearchCha').keypress(function (e) {
                var key = e.which;
                if (key == 13) {
                    $('#btnSearchCha').click();
                }
            });
        });

        function lol() {
            $("#successSubmit1").modal("show");
            return false;
        }

        function hideSuccessSubmit1() {
            $("#successSubmit1").modal("hide");
        }
    </script>
</asp:Content>
