<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LichTiepDan.aspx.cs" Inherits="Com.Gosol.CMS.Web.Webapp.Backend.LichTiepDan.LichTiepDan" EnableEventValidation="false" %>

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

    <link href="/AdminLte/plugins/select2/select2.min.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/AdminLte/plugins/select2/select2.full.min.js"></script>

    <link href="/AdminLte/plugins/datepicker/datepicker3.css" rel="stylesheet" />
    <link href="/AdminLte/plugins/daterangepicker/daterangepicker.css" rel="stylesheet" />
    <link href="/AdminLte/bootstrap/css/bootstrap-datepicker3.standalone.css" rel="stylesheet" />
    <script type="text/javascript" src="/AdminLte/bootstrap/js/bootstrap-datepicker.min.js"></script>
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

            $(".select2").select2({
                placeholder: "",
                width: '100%'
            });

            $(".datepicker3").datepicker({
                autoclose: true,
                format: 'dd/mm/yyyy',
                todayHighlight: true,
                clearBtn: true,
                orientation: 'bottom',
                language: 'vi',
            });
            //$(".datepicker3").mask("99/99/9999");

            //$('#txtSearch').keypress(function (e) {
            //    var key = e.which;
            //    if (key == 13) {
            //        $('#btnSearch').click();
            //    }
            //});

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
        //var dialog_title = "<img src='images/edit-add.png' style='vertical-align:middle;' /> Thêm/Sửa Cán bộ";
        //var checkAvailableUrl = "checkNhanVienAvailable";
        //var saveDbUrl = "saveNhanVien";
        //var fetchDetailUrl = "editNhanVien";


        // must have for all page
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
            $("#addNguoiDungForm").modal("show");
            return false;
        }

        function hideAddEditForm() {

            //loadForm();
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

            $('#MainContent_txtTuNgayFilter')
                .datepicker({
                    format: 'dd/mm/yyyy'
                })
                .on('changeDate', function (e) {
                    $('#Form1').formValidation('revalidateField', 'ctl00$MainContent$txtTuNgayFilter');
                });
            $('#datepicker')
                .datepicker({
                    format: 'dd/mm/yyyy'
                })
                .on('changeDate', function (e) {
                    $('#Form1').formValidation('revalidateField', 'ctl00$MainContent$txtTuNgayFilter');
                });


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
                    ctl00$MainContent$txtLoaiTin: {
                        validators: {
                            notEmpty: {
                                message: 'Vui lòng nhập nội dung tiếp'
                            },
                        },
                    },
                    ctl00$MainContent$ddlCoQuanTiep: {
                        validators: {
                            notEmpty: {
                                message: 'Vui lòng chọn cơ quan tiếp'
                            },
                        }
                    },
                    ctl00$MainContent$ddlCanBoTiep: {
                        validators: {
                            notEmpty: {
                                message: 'Vui lòng chọn cán bộ tiếp'
                            },
                        },
                    },
                    ctl00$MainContent$txtTuNgayFilter: {
                        validators: {
                            notEmpty: {
                                message: 'Vui lòng nhập ngày tiếp'
                            },
                        }
                    },
                },
            })
        };

        function loadForm() {
            $("#MainContent_hdfIDLichTiepEdit").val("");
            $("#MainContent_hdfCheckEdit").val("0");
            getCoQuanTiep();
            //$("#MainContent_ddlCoQuanTiep").prop('selectedIndex', 0).trigger('change');
            $("#MainContent_txtLoaiTin").val("");
            $("#MainContent_txtTuNgayFilter").val("");
            $('#MainContent_ddlCanBoTiep').val("").trigger('change');
            $("#MainContent_checkPublic").removeAttr('checked');
            
        }


        function getCoQuanTiep() {
            var coQuanID = $("#MainContent_hdfCoQuanTiep").val(); 
            $("#MainContent_ddlCoQuanTiep").val(coQuanID).trigger('change');
        }

        function changeCoQuan() {
            var coQuanID = $("#MainContent_ddlCoQuanTiep").val();
            var chekcEdit = $("#MainContent_hdfCheckEdit").val();
            //alert(chekcEdit);
            $("#MainContent_ddlCanBoTiep").html("");
            $.ajax({
                type: "POST",
                url: "LichTiepDan.aspx/GetCanBoByCoQuanID",
                data: '{idCoQuan:"' + coQuanID + '"}',
                dataType: "json",
                async: "true",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    var json = eval('(' + data.d + ')');
                    console.log(json);

                    if ($("#MainContent_ddlCanBoTiep").val() == null || $("#MainContent_ddlCanBoTiep").val() == "") {
                        $("#MainContent_ddlCanBoTiep").append(json);
                    }
                }
            });
        }

        function changeCanBo() {
            var canBoID = $("#MainContent_ddlCanBoTiep").val();
            $("#MainContent_hdfCanBoTiep").val(canBoID);
        }

        function showFormEdit(idLoaiTin) {
            $("#MainContent_hdfCheckEdit").val("1");
            $.ajax({
                type: "POST",
                url: "LichTiepDan.aspx/GetByID",
                data: '{idLoaiTin:"' + idLoaiTin + '"}',
                dataType: "json",
                async: "true",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    //loadForm();
                    var json = eval('(' + data.d + ')');
                    if (json != null) {
                        console.log(json);
                        $("#MainContent_hdfIDLichTiepEdit").val(json.IDLichTiep);

                        $("#MainContent_ddlCoQuanTiep").val(json.IDCoQuanTiep).trigger('change');
                        $("#MainContent_txtLoaiTin").val(json.NDTiep);
                        $("#MainContent_txtTuNgayFilter").val(json.NgayTiep_Str);

                        if (json.IDCoQuanTiep != null && json.IDCoQuanTiep != 0) {
                            $("#MainContent_ddlCanBoTiep").html("");
                            $.ajax({
                                type: "POST",
                                url: "LichTiepDan.aspx/GetCanBoByCoQuanID",
                                data: '{idCoQuan:"' + json.IDCoQuanTiep + '"}',
                                dataType: "json",
                                async: "true",
                                contentType: "application/json; charset=utf-8",
                                success: function (data) {
                                    var json1 = eval('(' + data.d + ')');
                                    console.log(json);
                                    if ($("#MainContent_ddlCanBoTiep").val() == null || $("#MainContent_ddlCanBoTiep").val() == "") {
                                        $("#MainContent_ddlCanBoTiep").append(json1);
                                        $("#MainContent_ddlCanBoTiep").val(json.IDCanBoTiep).trigger('change');
                                    }
                                    else {
                                        $("#MainContent_ddlCanBoTiep").val(json.IDCanBoTiep).trigger('change');
                                    }

                                }
                            });
                        }

                        if (json.Public == true) {
                            $('#MainContent_checkPublic').prop('checked', true);
                        }

                        $(".chosen").trigger("chosen:updated");
                        //showAddForm();
                        $("#addNguoiDungForm").modal("show");
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
    <asp:HiddenField runat="server" ID="hdfIDLichTiepEdit" />
    <asp:HiddenField runat="server" ID="hdfCanBoTiep" />
    <asp:HiddenField runat="server" Value="0" ID="hdfCheckEdit" />
    <asp:HiddenField runat="server" ID="hdfCoQuanTiep" />
    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" id="addNguoiDungForm" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Thêm mới/Sửa thông tin lịch tiếp dân</h4>
                </div>
                <div class="modal-body">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <label class="col-lg-3 col-sm-3 control-label">Cơ quan tiếp: <span style="color: red;">*</span></label>
                            <div class="col-lg-9">
                                <asp:DropDownList ID="ddlCoQuanTiep" runat="server" DataTextField="TenCoQuan" CssClass="form-control select2" DataValueField="CoQuanID" Style="width: 100%" onchange="changeCoQuan();"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-3 col-sm-3 control-label">Lãnh đạo tiếp:</label>
                            <div class="col-lg-9">
                                <asp:DropDownList ID="ddlCanBoTiep" runat="server" CssClass="form-control select2" onchange="changeCanBo();" Style="width: 100%"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-lg-3 col-sm-3 control-label">Nội dung tiếp: <span style="color: red;">*</span></label>
                            <div class="col-lg-9">
                                <asp:TextBox ID="txtLoaiTin" runat="server" Enabled="true" TextMode="multiline" Columns="50" Rows="5" Style="resize: none;" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-lg-3 col-sm-3 control-label">Ngày tiếp: <span style="color: red;">*</span></label>
                            <div class="col-lg-9">
                                <div class='input-group date datepicker' id="datepicker">
                                    <asp:TextBox ID="txtTuNgayFilter" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                                    <span class="input-group-addon">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </span>
                                </div>
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
                    <button type="button" onclick="hideSuccessMsg();" class="btn">
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
                    <button type="button" onclick="hideSuccessSubmit();" class="btn">
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
                        <button type="button" onclick="hideSubmitError();" class="btn">
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
                    <button type="button" onclick="hideSubmitError();" class="btn">
                        Đóng</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <!-- Content Header (Page header) -->
    <div class="content-header">
        <h1>Lịch tiếp dân
            <%--        <small>(Tổng ng dùng)</small>--%>
        </h1>
        <ol class="breadcrumb">
            <li><a href="/Default.aspx"><i class="fa fa-dashboard"></i>Trang chủ</a></li>
            <li><a href="#">Danh mục</a></li>
            <li class="active">Lịch tiếp dân</li>
        </ol>
    </div>

    <div class="content">
        <div style="display: none;">
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
        <div class="row">
            <div class="col-xs-12">
                <div class="box">
                    <div class="box-body">
                        <div class="box-header" style="padding: 0px;">
                            <asp:Panel runat="server" DefaultButton="btnSearch">
                                <div class="col-lg-3 col-lg-offset-4" style="padding-right: 5px">
                                    <asp:DropDownList ID="ddlCoQuan" runat="server" DataValueField="CoQuanID" DataTextField="TenCoQuan" AutoPostBack="true" OnSelectedIndexChanged="ddlCoQuan_SelectedIndexChanged"
                                        CssClass="chosen form-control">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg-4" style="padding: 0px;">
                                    <asp:TextBox ID="txtSearch" placeholder="Nhập tên cán bộ hoặc ND tiếp cần tìm kiếm" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-lg-1">
                                    <asp:Button ID="btnSearch" runat="server" CausesValidation="false" CssClass="btn btn-default btn-sm" OnClick="btnSearch_Click" Text="Tìm kiếm" />
                                </div>
                            </asp:Panel>
                            <div class="col-md-12 text-right" style="padding-right:0px; margin-top:10px">
                                <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary" Text="Thêm lịch tiếp dân" OnClientClick="showAddForm(); return false"/>
                    <%--        <button type="button" class="btn btn-primary" id="" onclick="showAddForm(); return false">
                                <span class="glyphicon glyphicon glyphicon-plus-sign" style="margin-right: 5px"></span>Thêm lịch tiếp dân
                            </button>--%>
                                </div>
                        </div>
                        <div class="table-responsive">
                            <table id="table" class="table table-bordered table-hover" style="margin-top: 15px; width: 100%">
                                <thead>
                                    <tr>
                                        <th style="text-align: center; width: 5%">STT</th>
                                        <th style="width: 10%; text-align: center">Ngày tiếp dân
                                        </th>
                                        <th style="text-align: center; width: 20%">Cơ quan
                                        </th>
                                        <th style="width: 15%; text-align: center">Lãnh đạo tiếp
                                        </th>
                                        <th style="width: 30%; text-align: center">Nội dung tiếp dân
                                        </th>
                                        <th style="width: 10%; text-align: center">Hiển thị
                                        </th>
                                        <th style="text-align: center; width: 10%;">Thao tác
                                        </th>
                                    </tr>
                                </thead>
                                <tbody id="mySearch">
                                    <asp:Repeater ID="rptLichTiepDan" runat="server" OnItemDataBound="rptLichTiepDan_ItemDataBound">
                                        <ItemTemplate>
                                            <tr>
                                                <td style="text-align: center;">
                                                    <asp:Label runat="server" ID="lblSTT"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblNgayTiep" runat="server"></asp:Label>
                                                </td>
                                                <td style="text-align: left;">
                                                    <%# Eval("CoQuanTiep") %>
                                                </td>
                                                <td style="text-align: left;">
                                                    <%# Eval("CanBoTiep") %>
                                                </td>
                                                <td style="text-align: left;">
                                                    <div id="summary<%# Container.ItemIndex + 1 %>" style="cursor: pointer" onclick="showDetail(<%# Container.ItemIndex + 1 %>)">
                                                        <%#Eval("NDTiep").ToString().Length>=100?Eval("NDTiep").ToString().Substring(0,99) + " ... .":Eval("NDTiep").ToString() %>
                                                    </div>
                                                    <input id="detail<%# Container.ItemIndex + 1 %>" style="display: none" value="<%#Eval("NDTiep") %>" />
                                                </td>
                                                <td style="text-align: center;">
                                                    <asp:CheckBox ID="cbHienThi" runat="server" />
                                                    <%--<asp:Label ID="lblTrangThai" runat="server"></asp:Label>--%>
                                                </td>
                                                <td style="text-align: center" class="action-cell">

                                                    <asp:ImageButton ID="btnEdit" runat="server" CommandName="Edit" OnClientClick='<%# "showFormEdit(" + Eval("IDLichTiep") +  "); return false;" %>'
                                                        ImageUrl="~/images/edit.png" ToolTip="Sửa" Width="20px" Style="margin-right: 5px;" />
                                                    <asp:ImageButton ID="btnDelete" runat="server" CommandName="Delete"
                                                        CausesValidation="false" OnClientClick="ConfirmDelete(this); return false;"
                                                        ImageUrl="~/images/delete.png" ToolTip="Xóa" Width="20px" Style="margin-right: 5px;" />
                                                    <asp:HiddenField ID="hdfIDLoaiTin" runat="server" Value='<%# Eval("IDLichTiep") %>' />
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
        <!-- end #dashboard -->
    </div>
    <script>
        function showDetail(index) {
            var summary = $('#summary' + index).html();
            var detail = $('#detail' + index).val();
            $('#summary' + index).html(detail);
            $('#detail' + index).val(summary);
        };
    </script>
</asp:Content>
