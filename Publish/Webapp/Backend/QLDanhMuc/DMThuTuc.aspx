<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DMThuTuc.aspx.cs" Inherits="Com.Gosol.CMS.Web.Webapp.Backend.QLDanhMuc.DMThuTuc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager runat="server" EnablePartialRendering="true">
    </asp:ScriptManager>

    <link href="/AdminLte/jquery.formvalidation/css/formValidation.min.css" rel="stylesheet" />
    <link href="/Styles/dropdownlist/chosen.min.css" rel="stylesheet" type="text/css" />

    <script src="/AdminLte/jquery.formvalidation/js/formValidation.min.js"></script>
    <script src="/AdminLte/jquery.formvalidation/js/framework/bootstrap.min.js"></script>

    <script src="/Scripts/dropdownlist/chosen.jquery.js" type="text/javascript"></script>
    <script src="../../../Scripts/khiem.js"></script>
    <script>
        $(document).ready(function () {
            //debugger;
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

            changeLoaiThuTuc();
        });

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
                    ctl00$MainContent$txtNDThuTuc: {
                        validators: {
                            notEmpty: {
                                message: 'Vui lòng nhập tên nội dung thủ tục'
                            },
                        },
                    },
                    ctl00$MainContent$txtOrder: {
                        validators: {
                            notEmpty: {
                                message: 'Vui lòng nhập trình tự thủ tục '
                            },

                        },
                    },

                    ctl00$MainContent$ddlLoaiThuTuc: {
                        validators: {
                            notEmpty: {
                                message: 'Vui lòng chọn loại thủ tục'
                            },
                        }
                    },
                },
            })
        };

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

        function StopParentEvent(event, control) {
            if (!$(control).parent().parent().hasClass("selected_hl")) {
                event.stopPropagation();
            }
        }

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

        function reset(id) {
            $('#trinhtuthutuc' + id).html("");
        };

        function loadThuTuc(id) {
            //alert("Ahihi");
            reset(id);
            $.ajax({
                type: "POST",
                url: "DMThuTuc.aspx/LoadThuTuc",
                data: "{ 'id':'" + id + "' }",
                dataType: "json",
                async: "true",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    //debugger;
                    var json = eval('(' + data.d + ')');
                    if (json != null) {
                        if (json.length > 0) {
                            var html = "";
                            for (var i = 0; i < json.length; i++) {
                                html += "<h5>Bước ";
                                html += i + 1 + ": " + json[i].NDThuTuc;
                                html += "</h5>";
                            }
                            $("#trinhtuthutuc" + id).append(html);
                            $("#hidden_trinhtuthutuc" + id).append(html);

                        } else {
                            $("#trinhtuthutuc" + id).append("<span style='color:red;padding-left:30px;font-size:14px;'>Loại thủ tục này chưa có thủ tục con</span>");
                        }

                    }
                }
            });
        }

        function showEditForm() {
            $("#myModal").modal("show");
            return false;
        }

        function showthongBaoError() {
            $("#thongBaoError").modal();
            return false;
        }

        function showthongBaoDeleteError() {
            $("#thongBaoDeleteError").modal();
            return false;
        }

        function hidethongBaoDeleteError() {
            $("#thongBaoDeleteError").modal("hide");
        }


        function showAddForm() {
            //debugger;
            loadForm();
            $("#div_dynamic").html();
            $("#myModal").modal("show");
            $("#MainContent_hdfIDEdit").val('0');
            return false;
        }

        function showthongBaoSuccess() {
            $("#successSubmit").modal();
            return false;
        }

        function hideSubmitError() {
            $("#submitError").modal("hide");
        }

        function hideSuccessSubmit() {
            $("#successSubmit").modal("hide");
        }

        function hidethongBaoError() {
            $("#thongBaoError").modal("hide");
        }

        function hideEditForm() {
            loadForm();
            $('#Form1').bootstrapValidator('resetForm', true);
        }

        function hideMessage() {
            var messageDiv = $("#<%= lblMsg.ClientID %>");
            if (messageDiv.is(":visible")) {
                setTimeout(function () {
                    messageDiv.hide(300);
                }, 2000);
            }
        }

        function ConfirmDelete(id) {
            //debugger;
            $("#deleteConfirm").modal("show");
            $("#MainContent_hdDeleteID").val(id);

            return false;
        }

        function hideDeleteConfirm() {
            $("#deleteConfirm").modal("hide");
            $("#MainContent_hdDeleteID").val(0);
        }

        function loadForm() {
            $("#MainContent_txtNDThuTuc").val("");
            $("#MainContent_txtOrder").val("1");
            $('#MainContent_ddlLoaiThuTuc').prop("disabled", false);
            $('#MainContent_ddlLoaiThuTuc').prop('selectedIndex', 0).trigger("chosen:updated");
            //$('#MainContent_ddlLoaiThuTuc').val(0).trigger("chosen:updated");
            console.log($('#MainContent_ddlLoaiThuTuc').val());
        }

        function showFormEdit(id) {
            $.ajax({
                type: "POST",
                url: "DMThuTuc.aspx/GetByID",
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
                        $("#MainContent_hdfIDEdit").val(json.ThuTucID);

                        $("#MainContent_txtNDThuTuc").val(json.NDThuTuc);
                        $("#MainContent_txtOrder").val(json.Order);
                        $("#MainContent_ddlLoaiThuTuc").val(json.LoaiThuTucID);
                        $('#MainContent_ddlLoaiThuTuc').prop("disabled", true);
                        $("#MainContent_hdfLoaiThuTucID").val(json.LoaiThuTucID);

                        console.log('json.LoaiThuTucID', json.LoaiThuTucID);
                        $(".chosen").trigger("chosen:updated");
                        showEditForm();

                    }
                }
            });
        };

        function changeLoaiThuTuc() {
            var loaiID = $("#MainContent_ddlLoaiThuTuc").val();
            $("#MainContent_hdfLoaiThuTucID").val(loaiID);

        }

        function get_order(id) {
            $.ajax({
                type: "POST",
                url: "DMThuTuc.aspx/GetByOrder",
                data: '{id:"' + id + '"}',
                dataType: "json",
                async: "true",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    loadForm();
                    var json = eval('(' + data.d + ')');
                    if (json != null) {
                        $("#MainContent_txtOrder").min(json.Order + 1);
                    }
                }
            });
        };

    </script>

    <div class="content-header">
        <h1>Quản lý trình tự thủ tục</h1>
        <ol class="breadcrumb">
            <li><a href="/Default.aspx"><i class="fa fa-dashboard"></i>Trang chủ</a></li>
            <li><a href="#">Danh mục</a></li>
            <li class="active">Danh mục trình tự thủ tục</li>
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
                            <div class="box-body">
                        <div class="box-header" style="padding: 0px;">
                                <asp:Panel runat="server" ID="pnlSearch" DefaultButton="btnSearch">
                                    <div class="col-lg-4 col-lg-offset-7" style="padding: 0px">
                                        <asp:TextBox ID="txtSearch" placeholder="Nhập nội dung cần tìm kiếm" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-1 text-right" style="padding-left: 5px; padding-right:0px;">
                                        <asp:Button type="button" ID="btnSearch" runat="server" CausesValidation="false" CssClass="btn btn-default btn-sm" OnClick="btnSearch_Click" Text="Tìm kiếm"  style="margin-right:0px;"/>
                                    </div>
                                    </asp:Panel>
                            <div class="col-md-12 text-right" style="padding-right:0px; margin-top:10px">
                                <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary" Text="Thêm thủ tục" OnClientClick="showAddForm(); return false" />
                           <%--     <button type="button" class="btn btn-primary" id="" onclick="showAddForm(); return false">
                                    <span class="glyphicon glyphicon glyphicon-plus-sign" style="margin-right: 5px;"></span>Thêm thủ tục
                                </button>--%>
                            </div>
                        </div>
                                <div class="table-responsive">
                                    <table id="table" class="table table-bordered table-hover" style="margin-top: 15px; width: 100%">
                                        <thead>
                                            <tr>
                                                <th style="text-align: center; width: 5%">STT
                                                </th>
                                                <th style="text-align: center; width: 15%">Tên trình tự thủ tục
                                                </th>
                                                <th style="text-align: center; width: 15%">Cơ sở pháp lý
                                                </th>
                                                <th style="text-align: center; width: 35%">Bước thực hiện
                                                </th>
                                                <th style="text-align: center; width: 15%">File đính kèm
                                                </th>
                                                <th style="text-align: center; width: 15%">Thao tác
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="rptLoaiThuTuc" runat="server" OnItemDataBound="rptLoaiThuTuc_ItemDataBound">
                                                <ItemTemplate>
                                                    <tr style="cursor: pointer;">
                                                        <td style="text-align: center;" runat="server" id="tdSTT">
                                                            <asp:Label runat="server" ID="lblSTT"></asp:Label>
                                                        </td>
                                                        <td runat="server" id="tdLoaiThuTuc" style="text-align: justify">
                                                            <%# Eval("loaithutuc_name") %>
                                                        </td>
                                                        <td runat="server" id="td3">
                                                            <%# Eval("CoSoPhapLy") %>
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <%# Eval("NDThuTuc") %>
                                                            <%--<asp:Table runat="server" ID="tbStep" BorderWidth="1" Width="100%" CssClass="table table-bordered" Style="margin-bottom: 0px;"></asp:Table>--%>
                                                            <%--<div id="summary<%# Container.ItemIndex + 1 %>" style="cursor: pointer" onclick="showDetail(<%# Container.ItemIndex + 1 %>)">
                                                                <%#Eval("NDThuTuc").ToString().Length>=50?Eval("NDThuTuc").ToString().Substring(0,50) + " ... .":Eval("NDThuTuc").ToString() %>
                                                            </div>
                                                            <input id="detail<%# Container.ItemIndex + 1 %>" style="display: none" value="<%#Eval("NDThuTuc") %>" />--%>
                                                        </td>

                                                        <td style="text-align: center;">
                                                            <%# Com.Gosol.CMS.Utility.Utils.ConvertToString(Eval("FileDinhKem"),string.Empty)=="" 
                                                        ? ""
                                                        :
                                                        "<a href = '../../../Handler/DownloadFileQuyetDinh.ashx?filename="+Eval("FileDinhKem") + "'><img src='"+"../../../images/download.png"
                                                        +"' style='"+"width: 20px; height: 20px"+"'/>" %>
                                                        </td>
                                                        <td style="text-align: center">
                                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.png" CommandName="Edit"
                                                                OnClientClick='<%# "showFormEdit(" + Eval("ThuTucID") +  "); return false;" %>'
                                                                ToolTip="Sửa" Width="20px" Style="margin-right: 5px;" />
                                                            <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.png" CommandName="Delete" CausesValidation="false"
                                                                CommandArgument='<%# Eval("ThuTucID") %>' OnClientClick='<%# "ConfirmDelete(" + Eval("ThuTucID") +  "); return false;" %>'
                                                                ToolTip="Xóa" Width="20px" />
                                                            <asp:HiddenField ID="hdfID" runat="server" Value='<%# Eval("ThuTucID") %>' />
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

    <asp:HiddenField runat="server" ID="hdfIDEdit" />

    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" id="myModal" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Thêm mới/Sửa thông tin thủ tục</h4>
                </div>
                <div class="modal-body">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <label class="col-lg-3 col-sm-3 control-label">Loại thủ tục:<span style="color: red;">*</span></label>
                            <div class="col-lg-9">
                                <asp:HiddenField runat="server" ID="hdfLoaiThuTucID"/>
                                <asp:DropDownList ID="ddlLoaiThuTuc" onchange="changeLoaiThuTuc();" runat="server" DataTextField="TenLoaiThuTuc" CssClass="chosen" DataValueField="LoaiThuTucID" Style="width: 100%"></asp:DropDownList>
                                <asp:RequiredFieldValidator Style="font-size: 12px;" ID="RequiredFieldValidator7" runat="server" ErrorMessage="Vui lòng chọn loại loại thủ tục!" ControlToValidate="ddlLoaiThuTuc" ValidationGroup="Validation1" ForeColor="Red" SetFocusOnError="true" Display="Dynamic" InitialValue="Tất cả"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-3 col-sm-3 control-label">Nội dung thủ tục: <span style="color: red;">*</span></label>
                            <div class="col-lg-9">
                                <asp:TextBox ID="txtNDThuTuc" runat="server" Enabled="true" CssClass="form-control"></asp:TextBox>
                                <span style="color: blue;">Ví dụ: Bước 1: Xin giấy xác nhận</span>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-lg-3 col-sm-3 control-label">File đính kèm: </label>

                            <div class="col-lg-9">
                                <asp:FileUpload ID="fileDinhKem" runat="server" Style="width: 284px !important"></asp:FileUpload>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-lg-3 col-sm-3 control-label">Trình tự thứ: <span style="color: red;">*</span></label>

                            <div class="col-lg-9">
                                <asp:TextBox Type="number" Min="1" ID="txtOrder" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                        <%--<asp:HiddenField--%>
                        <div id="div_dynamic">
                        </div>
                    </div>

                    <asp:HiddenField ID="hdfCount" runat="server" Value="1" />
                    <asp:HiddenField ID="hdfIDKeHoachNew" runat="server" Value="0" />
                    <asp:HiddenField ID="hdfKeHoachTTID" runat="server" Value="0" />

                    <%--<div class="text-right">
                        <button type="button" class="btn btn-primary" onclick="addNewDivRow(); return false;"><span class="glyphicon glyphicon glyphicon-plus-sign" style="margin-right: 5px"></span>Thêm</button>
                        <button type="button" class="btn btn-danger" onclick="removeLastDivRow(); return false;"><span class="glyphicon glyphicon-minus-sign" style="margin-right: 5px"></span>Bớt</button>
                    </div>--%>
                </div>
                <div class="modal-footer" style="text-align: center">
                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary btn-sm" Text="Lưu lại" OnClick="btnSave_Click" />
                    <button type="button" class="btn btn-default btn-sm" data-dismiss="modal" onclick="hideEditForm(); return false">Hủy bỏ</button>
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

    <script>
        function showDetail(index) {
            var summary = $('#summary' + index).html();
            var detail = $('#detail' + index).val();
            $('#summary' + index).html(detail);
            $('#detail' + index).val(summary);
        };

        $(document).ready(function () {
            $('#txtSearch').keypress(function (e) {
                var key = e.which;
                if (key == 13) {
                    $('#btnSearch').click();
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
