<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TraLoiCauHoi.aspx.cs" Inherits="Com.Gosol.CMS.Web.Webapp.Backend.HoiDap.TraLoiCauHoi" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .dotdot {
            max-height: 50px;
            max-width: 300px;
            white-space: nowrap;
            overflow: hidden;
            text-overflow: ellipsis
        }
    </style>
    <asp:ScriptManager ID="script1" runat="server" EnablePartialRendering="true">
    </asp:ScriptManager>
    <link href="/AdminLte/jquery.formvalidation/css/formValidation.min.css" rel="stylesheet" />
    <%--<link href="/AdminLte/plugins/select2/select2.min.css" rel="stylesheet" />--%>

    <script src="/AdminLte/jquery.formvalidation/js/formValidation.min.js"></script>
    <script src="/AdminLte/jquery.formvalidation/js/framework/bootstrap.min.js"></script>
    <%--<script src="/AdminLte/plugins/select2/select2.min.js" type="text/javascript"></script>--%>

    <link href="/Styles/dropdownlist/chosen.min.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/dropdownlist/chosen.jquery.js" type="text/javascript"></script>
    <script src="../../ckeditor/sample.js"></script>
    <script src="../../../ckeditor/adapters/jquery.js"></script>
    <script src="../../../ckeditor/ckeditor.js"></script>

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

            checkValidation();
            setInterval(hideMessage, 2000);

            $('#txtSearch').keypress(function (e) {
                var key = e.which;
                if (key == 13) {
                    $('#btnSearch').click();
                }
            });

            //if ($("#MainContent_hdfChangDiv").val() == 1) {
            //    changeDiv(1);
            //}
            //else changeDiv(1);

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
                //excluded: ':disabled',
                icon: {
                    valid: 'glyphicon glyphicon-ok',
                    invalid: 'glyphicon glyphicon-remove',
                    validating: 'glyphicon glyphicon-refresh'
                },
                fields: {
                    ctl00$MainContent$CKEditorNoiDungTraLoi: {
                        validators: {
                            notEmpty: {
                                message: 'Nội dung câu trả lời không được để trống'
                            },
                        },
                    },
                },
            })
                //.find('[name="ctl00$MainContent$CKEditorNoiDungTraLoi"]')
                //.ckeditor()
                .editor
            // To use the 'change' event, use CKEditor 4.2 or later
            //.on('change', function () {
            // Revalidate the bio field
            $('#profileForm').formValidation('revalidateField', 'ctl00$MainContent$CKEditorNoiDungTraLoi');
            //});
        };

        function loadForm() {
            $("#MainContent_ddlLinhVuc").val("").trigger('change');
            $("#MainContent_txtNoiDungCauHoi").prop("disabled", false);
            $("#MainContent_hdfIDTraLoiEdit").val("0");
            $("#MainContent_txtNoiDungCauHoi").val("");
            $('#MainContent_CKEditorNoiDungTraLoi').val("");
            $('#MainContent_checkPublic').removeAttr('checked');
            $("#spanTenFile").text("");
        }

        function showFormEdit(idTinTuc) {
            loadForm();
            console.log(idTinTuc);
            $("#MainContent_hdfCauHoiID").val(idTinTuc);
            $.ajax({
                type: "POST",
                url: "TraLoiCauHoi.aspx/GetByID",
                data: '{idTraLoiCauHoi:"' + idTinTuc + '"}',
                dataType: "json",
                async: "true",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    var json = eval('(' + data.d + ')');
                    if (json != null) {
                        console.log(json);
                        $("#MainContent_hdfIDTraLoiEdit").val(json.IDTraLoi);
                        $("#MainContent_hdfCauHoiID").val(json.IDCauHoi);
                        $("#MainContent_txtNoiDungCauHoi").val(json.NDCauHoi);
                        $("#MainContent_txtNoiDungCauHoi").prop("disabled", true);
                        $('#MainContent_CKEditorNoiDungTraLoi').val(json.NDTraLoi);
                        if (json.Public == true) {
                            $('#MainContent_checkPublic').prop('checked', true);
                        }
                        if (json.laTinHot == true) {
                            $('#MainContent_checkLaTinHot').prop('checked', true);
                        }

                        $("#MainContent_ddlLinhVuc").val(json.IDLinhVuc).trigger('change');
                        $("#MainContent_ddlLinhVuc").val(json.IDLinhVuc).prop("disabled", true);
                        $(".chosen").trigger("chosen:updated");
                        showAddForm();
                    }
                }
            });
        };

        function showTraLoiCauHoi(idCauHoi) {
            loadForm();
            $("#MainContent_hdfCauHoiID").val(idCauHoi);

            $.ajax({
                type: "POST",
                url: "TraLoiCauHoi.aspx/GetCauHoiByID",
                data: '{idCauHoi:"' + idCauHoi + '"}',
                dataType: "json",
                async: "true",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    var json = eval('(' + data.d + ')');
                    if (json != null) {
                        console.log(json);
                        $("#MainContent_ddlLinhVuc").val(json.IDLinhVuc).trigger('change');
                        $("#MainContent_txtNoiDungCauHoi").val(json.NDCauHoi);
                        $("#MainContent_txtNoiDungCauHoi").prop("disabled", true);
                        $("#MainContent_ddlLinhVuc").val(json.IDLinhVuc).prop("disabled", true);
                        $(".chosen").trigger("chosen:updated");
                        showAddForm();
                    }
                }
            });
        }

        function changeDiv(z) {
            if (z == 1) {
                $("#MainContent_btnCauHoi").addClass("btn-primary");
                $("#MainContent_btnDaTraLoi").removeClass("btn-primary");
                //$("#MainContent_hdfChangDiv").val(1);
                //$("#cauhoi").show();
                //$("#traloi").hide();

            }
            else if (z == 2) {
                $("#MainContent_btnCauHoi").removeClass("btn-primary");
                $("#MainContent_btnDaTraLoi").addClass("btn-primary");
                //$("#MainContent_hdfChangDiv").val(2);
                //$("#cauhoi").hide();
                //$("#traloi").show();
            }
        }
        function removeDotDot(el, type) {
            if (type == 1) {
                if ($('#tdNoiDungCauHoi' + el).hasClass('dotdot'))
                    $('#tdNoiDungCauHoi' + el).removeClass('dotdot');
                else
                    $('#tdNoiDungCauHoi' + el).addClass('dotdot');
            }
            else {
                if ($('#tdNoiDungCauHoi2' + el).hasClass('dotdot'))
                    $('#tdNoiDungCauHoi2' + el).removeClass('dotdot');
                else
                    $('#tdNoiDungCauHoi2' + el).addClass('dotdot');

                if ($('#tdNoiDungTraLoi2' + el).hasClass('dotdot'))
                    $('#tdNoiDungTraLoi2' + el).removeClass('dotdot');
                else
                    $('#tdNoiDungTraLoi2' + el).addClass('dotdot');
            }

        }
    </script>

    <asp:HiddenField ID="hdfChangDiv" runat="server" />
    <asp:HiddenField ID="hdf1" runat="server" />

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

    <asp:HiddenField runat="server" ID="hdfIDTraLoiEdit" />
    <asp:HiddenField runat="server" ID="hdfCauHoiID" />

    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" id="myModal" class="modal fade">
        <div class="modal-dialog" style="width: 60%;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Thêm mới/Sửa thông trả lời câu hỏi</h4>
                </div>
                <div class="modal-body">
                    <div class="form-horizontal">
                        <div class="form-group" style="display: none">
                            <label class="col-lg-3 col-sm-3 control-label">Lĩnh vực: </label>
                            <div class="col-lg-9">
                                <asp:DropDownList ID="ddlLinhVuc" runat="server" DataTextField="TenLinhVuc" CssClass="chosen" DataValueField="IDLinhVuc" Style="width: 30%"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-3 col-sm-3 control-label" style="text-align: left">Nội dung câu hỏi:</label>

                            <div class="col-lg-9">
                                <asp:TextBox ID="txtNoiDungCauHoi" runat="server" TextMode="multiline" Columns="50" Rows="7" Enabled="true" Style="resize: none;" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-3 col-sm-3 control-label" style="text-align: left">Nội dung câu trả lời: <span style="color: red">*</span></label>

                            <div class="col-lg-9">
                                <asp:TextBox ID="CKEditorNoiDungTraLoi" BasePath="/ckeditor/" TextMode="multiline" Columns="50" Rows="5" Width="100%" Height="200px" runat="server" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-lg-3 col-sm-3 control-label">Hiển thị:</label>

                            <div class="col-lg-9">
                                <asp:CheckBox runat="server" ID="checkPublic" />
                            </div>
                        </div>
                        <div class="form-group" style="display: none;">
                            <label class="col-lg-3 col-sm-3 control-label">File <span style="color: red"></span></label>
                            <div class="col-lg-6">
                                <asp:FileUpload ID="file_upload" runat="server" CssClass="file_dinhkem" Style="width: 284px !important" />
                                <asp:TextBox ID="txt_fileurl" runat="server" CssClass="txt_fileurl hidden" />
                            </div>

                        </div>
                        <div class="form-group" style="display: none;">
                            <div class="col-lg-3">
                            </div>
                            <div class="col-lg-9">
                                <span id="spanTenFile"></span></br>
                                    <span style="color: red">(*)</span> Chú ý: chỉ cho upload các định dạng file là .doc, .xlsx, .pdf, .jpeg, .png
                                    <asp:HiddenField ID="hdfFileUpload" runat="server" />
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
        <h1>Trả lời câu hỏi
            <%--        <small>(Tổng ng dùng)</small>--%>
        </h1>
        <ol class="breadcrumb">
            <li><a href="/Default.aspx"><i class="fa fa-dashboard"></i>Trang chủ</a></li>
            <li><a href="#">Hỏi đáp</a></li>
            <li class="active">Trả lời câu hỏi</li>
        </ol>
    </div>

    <div class="content">
        <div style="display: none;">
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
        <div class="row">
            <div class="col-xs-12">
                <div class="box">
                    <div class="content-body">
                        <div class="box-body">
                            <%--OnClientClick="changeDiv(1); return false;" OnClientClick="changeDiv(2); return false;" --%>

                            <asp:Button ID="btnCauHoi" runat="server" CssClass="btn btn-default" Text="Câu hỏi" OnClick="btnCauHoi_Click" />
                            <asp:Button ID="btnDaTraLoi" runat="server" CssClass="btn btn-default" Text="Đã trả lời" OnClick="btnDaTraLoi_Click" />

                            <div class="box-header" style="padding: 0px;">
                                <asp:Panel runat="server" DefaultButton="btnSearch">
                                    <div class="col-lg-3 col-lg-offset-4" style="padding-right: 5px">
                                        <asp:DropDownList ID="ddlLinhVucSearch" runat="server" DataValueField="IDLinhVuc" DataTextField="TenLinhVuc" AutoPostBack="true" OnSelectedIndexChanged="ddlLinhVucSearch_SelectedIndexChanged"
                                            CssClass="chosen form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-lg-4" style="padding: 0px">
                                        <asp:TextBox ID="txtSearch" placeholder="Nhập nội dung cần tìm kiếm" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-1">
                                        <asp:Button ID="btnSearch" runat="server" CausesValidation="false" CssClass="btn btn-default btn-sm" OnClick="btnSearch_Click" Text="Tìm kiếm" />
                                    </div>
                                </asp:Panel>
                                <div class="col-md-12 text-right" style="padding-right: 0px; margin-top: 10px">
                                    <asp:Button ID="btnAdd" CssClass="btn btn-primary" runat="server"  Text="Thêm loại tin" OnClientClick="showAddForm(); return false" />
                                <%--    <button type="button" class="btn btn-primary" id="" onclick="showAddForm(); return false" style="display: none;">
                                        <span class="glyphicon glyphicon glyphicon-plus-sign" style="margin-right: 5px"></span>Thêm loại tin
                                    </button>--%>
                                </div>
                            </div>

                            <asp:Panel ID="pnCauHoi" runat="server">
                                <div id="cauhoi" class="" style="padding-top: 10px">

                                    <div class="table-responsive">
                                        <table id="table" class="table table-bordered table-hover" style="margin-top: 0px; width: 100%">
                                            <thead>
                                                <tr>
                                                    <th style="width: 5%; text-align: center">STT
                                                    </th>
                                                    <th style="width: 30%; text-align: center">Nội dung câu hỏi
                                                    </th>
                                                    <th style="width: 10%; text-align: center">Lĩnh vực
                                                    </th>
                                                    <th style="width: 10%; text-align: center">Ngày gửi
                                                    </th>
                                                    <th style="width: 10%; text-align: center">Người gửi
                                                    </th>
                                                    <th style="width: 10%; text-align: center">Email
                                                    </th>
                                                    <th style="width: 10%; text-align: center">Điện thoại
                                                    </th>
                                                    <th style="width: 7%; text-align: center">Hiển thị
                                                    </th>
                                                    <th style="width: 8%; text-align: center;">Thao tác
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rptCauHoi" runat="server" OnItemDataBound="rptCauHoi_ItemDataBound">
                                                    <ItemTemplate>
                                                        <tr onclick='<%# "removeDotDot("+ Eval("IDCauHoi") +",1);"  %>'>
                                                            <td style="text-align: center;">
                                                                <asp:Label runat="server" ID="lblSTT"></asp:Label>
                                                            </td>
                                                            <td style="text-align: left;">
                                                                <div class="dotdot" id='<%# "tdNoiDungCauHoi" + Eval("IDCauHoi")%>'>
                                                                    <%#Eval("NDCauHoi").ToString() %>
                                                                </div>
                                                                <%--                 <input id="detailCauHoi<%# Container.ItemIndex + 1 %>" style="display: none" value="<%#Eval("NDCauHoi") %>" />--%>
                                                            </td>
                                                            <td style="text-align: left;">
                                                                <%# Eval("TenLinhVuc") %>
                                                            </td>
                                                            <td style="text-align: left;">
                                                                <asp:Label ID="lblNgayHoi" runat="server"></asp:Label>
                                                            </td>
                                                            <td style="text-align: left;">
                                                                <%# Eval("TenCanBo") %>
                                                            </td>
                                                            <td>
                                                                <%# Eval("Email") %>
                                                            </td>
                                                            <td>
                                                                <%# Eval("SDT") %>
                                                            </td>
                                                            <td style="text-align: center">
                                                                <asp:CheckBox runat="server" ID="cbHienThi"></asp:CheckBox>
                                                            </td>
                                                            <td style="text-align: center; vertical-align: middle;" class="action-cell">
                                                             
                                                                <asp:Button ID="btnTraLoi" runat="server" Text="Trả lời" CssClass="btn btn-primary btn-sm" OnClientClick='<%# "showTraLoiCauHoi(" + Eval("IDCauHoi") +  "); return false;" %>'  />
                                                       
                                                                
                                                              <%--  <button type="button" id="btnTraLoiCauHoi" runat="server" onclick='<%# "showTraLoiCauHoi(" + Eval("IDCauHoi") +  "); return false;" %>'
                                                                    class="btn btn-primary btn-sm">
                                                                    Trả lời</button>--%>
                                                                <asp:HiddenField ID="hdfTinTucDelete" runat="server" Value='<%# Eval("IDTraLoi") %>' />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>

                                    <div class="paginations" style="margin-top: 15px; margin-bottom: 15px">
                                        <asp:PlaceHolder ID="plhPaging" runat="server"></asp:PlaceHolder>
                                    </div>

                                </div>
                            </asp:Panel>

                            <asp:Panel ID="pnTraLoi" runat="server">
                                <div id="traloi" style="padding-top: 10px">
                                    <div class="table-responsive">
                                        <table id="" class="table table-bordered table-hover" style="margin-top: 0px; width: 100%">
                                            <thead>
                                                <tr>
                                                    <th style="width: 5%; text-align: center">STT
                                                    </th>
                                                    <th style="width: 25%; text-align: center">Nội dung câu hỏi
                                                    </th>
                                                    <th style="width: 25%; text-align: center">Nội dung câu trả lời
                                                    </th>
                                                    <th style="width: 10%; text-align: center">Lĩnh vực
                                                    </th>
                                                    <th style="width: 10%; text-align: center">Người gửi
                                                    </th>
                                                    <th style="width: 10%; text-align: center">Người trả lời
                                                    </th>
                                                    <th style="width: 7%; text-align: center">Hiển thị
                                                    </th>
                                                    <th style="width: 8%; text-align: center; width: 15%;">Thao tác
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rptTraLoi" runat="server" OnItemDataBound="rptTraLoi_ItemDataBound">
                                                    <ItemTemplate>
                                                        <tr onclick='<%# "removeDotDot("+ Eval("IDTraLoi") +",2);"  %>'>
                                                            <td style="text-align: center;">
                                                                <asp:Label runat="server" ID="lblSTT1"></asp:Label>
                                                            </td>
                                                            <td style="text-align: left;">
                                                                <div class="dotdot" id='<%# "tdNoiDungCauHoi2" + Eval("IDTraLoi")%>'>
                                                                    <%#Eval("NDCauHoi").ToString() %>
                                                                </div>
                                                                <%--<input id="detailCauHoi<%# Container.ItemIndex + 1 %>" style="display: none" value="<%#Eval("NDCauHoi") %>" />--%>
                                                            </td>
                                                            <td style="text-align: left;" class="dotdot" id='<%# "tdNoiDungTraLoi2" + Eval("IDTraLoi")%>'>
                                                                <%# Eval("NDTraLoi") %>
                                                            </td>
                                                            <td style="text-align: left;">
                                                                <%# Eval("TenLinhVuc") %>
                                                            </td>
                                                            <td style="text-align: left;">
                                                                <%# Eval("nguoi_hoi") %>
                                                            </td>
                                                            <td>
                                                                <%# Eval("nguoi_traloi") %>
                                                            </td>
                                                            <td style="text-align: center">
                                                                <asp:CheckBox runat="server" ID="cbHienThi2"></asp:CheckBox>
                                                            </td>
                                                            <td style="text-align: center; vertical-align: middle;" class="action-cell">
                                                                <asp:ImageButton ID="btnEdit" runat="server" CommandName="Edit" OnClientClick='<%# "showFormEdit(" + Eval("IDTraLoi") +  "); return false;" %>'
                                                                    ImageUrl="~/images/edit.png" ToolTip="Sửa" Width="20px" Style="margin-right: 5px;" />
                                                                <asp:ImageButton ID="btnDelete" runat="server" CommandName="Delete" CausesValidation="false"
                                                                    OnClientClick="ConfirmDelete(this); return false;"
                                                                    ImageUrl="~/images/delete.png" ToolTip="Xóa" Width="20px" Style="margin-right: 5px;" />
                                                                <asp:HiddenField ID="hdfTinTucDelete" runat="server" Value='<%# Eval("IDTraLoi") %>' />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>

                                    <div class="paginations" style="margin-top: 15px; margin-bottom: 15px">
                                        <asp:PlaceHolder ID="plhPaging2" runat="server"></asp:PlaceHolder>
                                    </div>
                                </div>
                            </asp:Panel>
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
        function showDetailQues(index) {
            var summary = $('#ndCauHoi' + index).html();
            var detail = $('#detailCauHoi' + index).val();
            $('#ndCauHoi' + index).html(detail);
            $('#detailCauHoi' + index).val(summary);
        };
        function showDetailAnws(index) {
            var summary = $('#ndCauTraLoi' + index).html();
            var detail = $('#detailCauTraLoi' + index).val();
            $('#ndCauTraLoi' + index).html(detail);
            $('#detailCauTraLoi' + index).val(summary);
        };
    </script>
</asp:Content>
