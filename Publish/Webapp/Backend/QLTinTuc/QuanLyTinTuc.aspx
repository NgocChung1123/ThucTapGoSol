<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" ValidateRequest="false" CodeBehind="QuanLyTinTuc.aspx.cs" EnableEventValidation="false" Inherits="Com.Gosol.CMS.Web.Webapp.Backend.QLTinTuc.QuanLyTinTuc" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="script1" runat="server" EnablePartialRendering="true">
    </asp:ScriptManager>
    <link href="/AdminLte/jquery.formvalidation/css/formValidation.min.css" rel="stylesheet" />
    <%--<link href="/AdminLte/plugins/select2/select2.min.css" rel="stylesheet" />--%>
    <link href="/Styles/dropdownlist/chosen.min.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
        .myCssClass {
            font-size: 14px;
            font-family: 'Times New Roman';
            font-weight: normal;
        }

        .control-label {
            text-align: left !important;
        }
    </style>

    <script src="/AdminLte/jquery.formvalidation/js/formValidation.min.js"></script>
    <script src="/AdminLte/jquery.formvalidation/js/framework/bootstrap.min.js"></script>
    <%--<script src="/AdminLte/plugins/select2/select2.min.js" type="text/javascript"></script>--%>

    <script src="/Scripts/dropdownlist/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript" src="/ckeditor/sample.js"></script>
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
            var config = {
                '.chosen': {}
            }
            for (var selector in config) {
                $(selector).chosen(config[selector]);

            }
            $(".chosen").trigger("chosen:updated");

            //CKEDITOR.replace('MainContent_CKEditorTieuDe', {
            //    filebrowserBrowseUrl: '/fileman/index.html',
            //    filebrowserUploadUrl: '/Handler/CkeditorUploader.ashx'
            //});
            CKEDITOR.replace('MainContent_CKEditorNoiDung', {
                filebrowserBrowseUrl: '/fileman/index.html',
                filebrowserUploadUrl: '/Handler/CkeditorUploader.ashx'
            });
            //checkValidation();
            setInterval(hideMessage, 2000);

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
        CKEDITOR.replace('Description', { toolbar: '1', htmlEncodeOutput: true });
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
            $('#Form1').bootstrapValidator('resetForm', true);

            loadForm();
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
                    ctl00$MainContent$txtTieuDe: {
                        validators: {
                            notEmpty: {
                                message: 'Vui lòng nhập tiêu đề'
                            },
                        },
                    },
                    ctl00$MainContent$txtTomTat: {
                        validators: {
                            notEmpty: {
                                message: 'Vui lòng nhập tóm tắt'
                            },
                        },
                    },
                    ctl00$MainContent$CKEditorNoiDung: {
                        validators: {
                            notEmpty: {
                                message: 'Vui lòng nhập nội dung'
                            },
                        },
                    },
                    ctl00$MainContent$ddlLoaiTin: {
                        validators: {
                            notEmpty: {
                                message: 'Vui lòng chọn loại tin'
                            },
                        }
                    },
                },
            })
        };

        function loadForm() {
            $("#MainContent_hdfIDTinTucEdit").val("0");
            $("#MainContent_txtTieuDe").val("");
            $("#MainContent_txtTomTat").val("");
            CKEDITOR.instances['MainContent_CKEditorNoiDung'].setData("");
            $('#MainContent_ddlLoaiTin').val(0).trigger("chosen:updated");
            $('#MainContent_checkLaTinHot').removeAttr('checked');
            $('#MainContent_checkPublic').removeAttr('checked');
            $("#spanTenFile").text("");
            $("#MainContent_imageTieuDe").removeAttr("src");
        }

        function showFormEdit(idTinTuc) {

            $.ajax({
                type: "POST",
                url: "QuanLyTinTuc.aspx/GetByID",
                data: '{idTinTuc:"' + idTinTuc + '"}',
                dataType: "json",
                async: "true",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    var json = eval('(' + data.d + ')');
                    if (json != null) {
                        //loadForm();
                        console.log(json);
                        $("#MainContent_hdfIDTinTucEdit").val(json.IDTinTuc);

                        //$("#MainContent_CKEditorTieuDe").text(json.TieuDe);
                        $("#MainContent_txtTieuDe").val(json.TieuDe);
                        //CKEDITOR.instances['MainContent_CKEditorTieuDe'].setData(json.TieuDe);
                        $("#MainContent_txtTomTat").val(json.TomTat);
                        //$("#MainContent_CKEditorNoiDung").text(json.NoiDung);
                        CKEDITOR.instances['MainContent_CKEditorNoiDung'].setData(json.NoiDung);
                        $("#MainContent_ddlLoaiTin").val(json.IDLoaiTin);
                        if (json.Public == true) {
                            $('#MainContent_checkPublic').prop('checked', true);
                        }
                        if (json.laTinHot == true) {
                            $('#MainContent_checkLaTinHot').prop('checked', true);
                        }

                        if (json.ImageUrl != null && json.ImageUrl != "") {
                            var array = json.ImageUrl.split("/");
                            $("#spanTenFile").text(array[array.length - 1]);
                            $("#MainContent_imageTieuDe").attr("src", "/" + json.ImageUrl);
                            $("#MainContent_hdfFileUpload").val(json.ImageUrl);
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

    <asp:HiddenField runat="server" ID="hdfIDTinTucEdit" />
    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" id="addNguoiDungForm" class="modal fade">
        <div class="modal-dialog" style="width: 60%;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Thêm / Sửa tin tức</h4>
                </div>
                <div class="modal-body">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <label class="col-lg-3 col-sm-3 control-label">Tiêu đề: <span style="color: red;">*</span></label>
                            <div class="col-lg-9">
                                <asp:TextBox runat="server" ID="txtTieuDe" TextMode="multiline" Columns="50" Rows="2" CssClass="form-control" Style="resize: none;"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Vui lòng nhập tiêu đề!" ControlToValidate="txtTieuDe" ForeColor="Red" SetFocusOnError="true"
                                    Display="Dynamic" ValidationGroup="Validation1"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-3 col-sm-3 control-label">Tóm tắt nội dung: <span style="color: red;">*</span></label>

                            <div class="col-lg-9">
                                <asp:TextBox ID="txtTomTat" runat="server" TextMode="multiline" Columns="50" Rows="3" CssClass="form-control" Style="resize: none;"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Vui lòng nhập tóm tắt!" ControlToValidate="txtTomTat" ForeColor="Red" SetFocusOnError="true"
                                    Display="Dynamic" ValidationGroup="Validation1"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-3 col-sm-3 control-label">Nội dung tin: <span style="color: red;">*</span> </label>

                            <div class="col-lg-9">
                                <CKEditor:CKEditorControl ID="CKEditorNoiDung" BasePath="/ckeditor/" runat="server">
                                </CKEditor:CKEditorControl>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Vui lòng nhập nội dung!" ControlToValidate="CKEditorNoiDung" ForeColor="Red" SetFocusOnError="true"
                                    Display="Dynamic" ValidationGroup="Validation1"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-3 col-sm-3 control-label">Loại tin: <span style="color: red;">*</span></label>
                            <div class="col-lg-4">
                                <asp:DropDownList ID="ddlLoaiTin" runat="server" DataTextField="TenLoaiTin" CssClass="chosen" DataValueField="IDLoaiTin" Style="width: 30%"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Vui lòng chọn loại tin!" ControlToValidate="ddlLoaiTin" ValidationGroup="Validation1" ForeColor="Red" SetFocusOnError="true" Display="Dynamic" InitialValue="Tất cả"></asp:RequiredFieldValidator>
                            </div>
                            <label class="col-lg-2 col-sm-2 control-label">Tin nổi bật:</label>
                            <div class="col-lg-2">
                                <asp:CheckBox runat="server" ID="checkLaTinHot" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-3 col-sm-3 control-label">Ảnh đại diện <span style="color: red"></span></label>
                            <div class="col-lg-6">
                                <input type="file" name="fileUploader" id="fileUploader" class="btn btn-default" />
                                <asp:FileUpload ID="file_upload" runat="server" CssClass="file_dinhkem" Style="width: 284px !important; display: none;" />
                                <asp:TextBox ID="txt_fileurl" runat="server" CssClass="txt_fileurl hidden" />
                            </div>
                            <div class="row">
                                <div id="divFiles" class="files">
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-lg-3">
                            </div>
                            <div class="col-lg-9">
                                <asp:Image runat="server" ID="imageTieuDe" Width="300" Height="230" />
                                <span id="spanTenFile" style="display: none;"></span>
                                <asp:HiddenField ID="hdfFileUpload" runat="server" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-3 col-sm-3 control-label">Hiển thị:</label>
                            <div class="col-lg-9">
                                <asp:CheckBox runat="server" ID="checkPublic" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-3 col-sm-3 control-label"></label>
                            <label class="col-lg-9 control-label" style="text-align: left"><strong>Chú ý </strong><span style="color: red">*</span> Hệ thống chỉ hỗ trợ upload file ảnh có đuôi định dạng là <strong>.png, .jpg, .jpeg</strong>.</label>
                        </div>
                    </div>

                </div>
                <div class="modal-footer" style="text-align: center">
                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary btn-sm" Text="Lưu lại" OnClick="btnSave_Click" ValidationGroup="Validation1" />
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
        <h1>Quản lý tin tức
        </h1>
        <ol class="breadcrumb">
            <li><a href="/Default.aspx"><i class="fa fa-dashboard"></i>Trang chủ</a></li>
            <li><a href="#">Quản lý tin tức</a></li>
            <li class="active">Tin tức</li>
        </ol>
    </div>

    <div class="content">
        <div style="display: none;">
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="box">
                    <div class="box-header">
                        <asp:Panel runat="server" DefaultButton="btnSearch">
                            <div class="">
                                <asp:DropDownList ID="ddlLoaiTin_Filter" runat="server" DataTextField="TenLoaiTin" CssClass="chosen form-control" DataValueField="IDLoaiTin" AutoPostBack="true" OnSelectedIndexChanged="ddlLoaiTin_Filter_SelectedIndexChanged" Visible="false"></asp:DropDownList>
                            </div>
                            <div class="col-md-4 col-md-offset-7" style="padding: 0px">
                                <asp:TextBox ID="txtSearch" runat="server" placeholder="Tiêu đề, tóm tắt, nội dung" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-1" style="padding-left: 5px; margin-right: 0px; padding-right: 0px">
                                <asp:Button ID="btnSearch" runat="server" CausesValidation="false" CssClass="btn btn-default btn-sm" OnClick="btnSearch_Click" Text="Tìm kiếm" />
                            </div>
                        </asp:Panel>
                        <div class="col-md-3 col-md-offset-9 text-right" style="padding-right: 0px; margin-top: 10px">
                            <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary" Text="Thêm tin tức" OnClientClick="showAddForm(); return false"  />
                      <%--      <button type="button" class="btn btn-primary" id="" onclick="showAddForm(); return false">
                                <span class="glyphicon glyphicon glyphicon-plus-sign" style="margin-right: 5px"></span>Thêm tin tức
                            </button>--%>
                        </div>
                    </div>
                    <div class="content-body">
                        <div class="box-body">
                            <div class="table-responsive">
                                <table id="table" class="table table-bordered table-hover" style="margin-top: 5px; width: 100%">
                                    <thead>
                                        <tr>
                                            <th style="width: 3%; text-align: center">STT
                                            </th>
                                            <th style="width: 7%; text-align: center">Tiêu đề
                                            </th>
                                            <th style="width: 13%; text-align: center; display: none;">Tóm tắt nội dung
                                            </th>
                                            <th style="width: 5%; text-align: center">Ngày tạo
                                            </th>
                                            <th style="width: 5%; text-align: center; display: none;">Ngày sửa
                                            </th>
                                            <th style="width: 5%; text-align: center">Cán bộ tạo
                                            </th>
                                            <th style="width: 5%; text-align: center; display: none;">Cán bộ sửa
                                            </th>
                                            <th style="width: 8%; text-align: center">Ảnh đại diện
                                            </th>
                                            <th style="text-align: center; width: 3%;">Hiển thị
                                            </th>
                                            <th style="text-align: center; width: 5%;">Loại tin
                                            </th>
                                            <th style="text-align: center; width: 4%;">Thao tác
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="rptTinTuc" runat="server" OnItemDataBound="rptTinTuc_ItemDataBound">
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="text-align: center;" runat="server" id="Td1">
                                                        <asp:Label runat="server" ID="lblSTT"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left;" runat="server" id="tieuDe">
                                                        <%# Eval("TieuDe") %>
                                                    </td>
                                                    <td style="text-align: left; display: none;">
                                                        <asp:Label runat="server" ID="lblNoiDung"></asp:Label>
                                                    </td>
                                                    <td style="text-align: center;">
                                                        <asp:Label ID="lblNgayTao" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="text-align: center; display: none;">
                                                        <asp:Label ID="lblNgaySua" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <%# Eval("NguoiTao") %>
                                                    </td>
                                                    <td style="text-align: left; display: none;">
                                                        <%# Eval("NguoiSua") %>
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="ImageAnh" runat="server" Width="40px" />
                                                        <asp:Label ID="lblAnh" runat="server"></asp:Label>
                                                    </td>
                                                     <td style="text-align: center;">
                                                        <asp:CheckBox ID="cbHienThi" runat="server" />
                                                    </td>
                                                    <td>
                                                        <%# Eval("TenLoaiTin") %>
                                                    </td>
                                                    <td style="text-align: center;">
                                                        <asp:ImageButton ID="btnEdit" runat="server" CommandName="Edit" OnClientClick='<%# "showFormEdit(" + Eval("IDTinTuc") +  "); return false;" %>'
                                                            ImageUrl="~/images/edit.png"
                                                            ToolTip="Sửa" Width="20px" Style="margin-right: 5px;" />
                                                        <asp:ImageButton ID="btnDelete" runat="server" CommandName="Delete" CausesValidation="false"
                                                            OnClientClick="ConfirmDelete(this); return false;"
                                                            ImageUrl="~/images/delete.png" ToolTip="Xóa" Width="20px" />
                                                        <asp:HiddenField ID="hdfTinTucDelete" runat="server" Value='<%# Eval("IDTinTuc") %>' />
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

    <link href="/Styles/uploadfile/uploadify.css" rel="stylesheet" />
    <script src="/scripts/uploadfile/jquery.uploadify.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            var uploaded = 0;

            $(".file_dinhkem").uploadify({
                'swf': '/asset/uploadfile/uploadify.swf',
                'uploader': '/Handler/UploadFile/UploadFileWF.ashx',
                'cancelImg': '/Styles/uploadfile/cancel.png',
                'buttonText': 'Chọn ảnh',
                'fileDesc': 'Image Files',
                'fileSizeLimit': '10MB',
                'uploadLimit': '5',
                'fileExt': '*.jpg;*.jpeg;*.gif;*.png;*.doc;*.docx;*.pdf;*.xls;*.xlsx',
                'multi': true,
                'auto': true,
                'checkExisting': true,
                'removeCompleted': true,
                'onUploadSuccess': function (file, data, response) {
                    $("#MainContent_hdfFileUpload").val(data);
                    $("#spanTenFile").text(file.name);
                    $("#MainContent_imageTieuDe").attr("src", "../../../" + data);
                }
            });

            $('#fileUploader').change(function () {
                for (var i = 0; i < this.files.length; i++) {
                    var fileId = i;
                    $("#divFiles").append('<div class="col-md-12">' +
                        '<div class="progress-bar progress-bar-striped active" id="progressbar_' + fileId + '" role="progressbar" aria-valuemin="0" aria-valuemax="100" style="width:0%"></div>' +
                        '</div>' +
                        '<div class="col-md-12">' +

                        '<div class="col-md-6">' +
                        '<p class="progress-status" style="text-align: right;margin-right:-15px;font-weight:bold;color:saddlebrown" id="status_' + fileId + '"></p>' +
                        '</div>' +
                        '<div class="col-md-6">' +
                        '<input type="button" class="btn btn-danger" style="display:none;line-height:6px;height:25px" id="cancel_' + fileId + '" value="cancel">' +
                        '</div>' +
                        '</div>' +
                        '<div class="col-md-12">' +
                        '</div>');

                    uploadSingleFile(this.files[i], fileId);
                }
            });

        });


        function uploadSingleFile(file, i) {
            var fileId = i;
            var ajax = new XMLHttpRequest();
            //Progress Listener
            ajax.upload.addEventListener("progress", function (e) {
                var percent = (e.loaded / e.total) * 100;
                $("#status_" + fileId).text(Math.round(percent) + "% uploaded, please wait...");
                $('#progressbar_' + fileId).css("width", percent + "%")
                $("#notify_" + fileId).text("Uploaded " + (e.loaded / 1048576).toFixed(2) + " MB of " + (e.total / 1048576).toFixed(2) + " MB ");
            }, false);
            //Load Listener
            ajax.addEventListener("load", function (e) {
                var pathFile = event.target.responseText;
                $("#status_" + fileId).text(event.target.responseText);
                $('#progressbar_' + fileId).css("width", "100%")
                //Hide cancel button
                $('#cancel_' + fileId).hide();
                $("#status_" + fileId).hide();

                $("#MainContent_hdfFileUpload").val(pathFile);
                $("#spanTenFile").text(file.name);
                $("#MainContent_imageTieuDe").attr("src", "../../../" + pathFile);

            }, false);
            //Error Listener
            ajax.addEventListener("error", function (e) {
                $("#status_" + fileId).text("Upload Failed");
            }, false);
            //Abort Listener
            ajax.addEventListener("abort", function (e) {
                $("#status_" + fileId).text("Upload Aborted");
            }, false);

            ajax.open("POST", "/Handler/UploadFile/UploadFileWF.ashx"); // Your API .net, php

            var uploaderForm = new FormData(); // Create new FormData
            uploaderForm.append("Filedata", file); // append the next file for upload
            ajax.send(uploaderForm);

            //Cancel button
            var _cancel = $('#cancel_' + fileId);
            _cancel.show();

            _cancel.on('click', function () {
                ajax.abort();
            })
        }

    </script>
</asp:Content>

