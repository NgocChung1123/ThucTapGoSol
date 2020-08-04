<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChucNangManage.aspx.cs" Inherits="Com.Gosol.CMS.Web.ChucNangManage" EnableEventValidation="false" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="IndexContent" runat="server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="script1" runat="server" EnablePartialRendering="true">
    </asp:ScriptManager>
    <link href="../Styles/dropdownlist/chosen.min.css" rel="stylesheet" />
    <script src="../Scripts/dropdownlist/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_initializeRequest(InitializeRequest);
        prm.add_endRequest(EndRequest);
        var postBackElement;
        function InitializeRequest(sender, args) {
            if (prm.get_isInAsyncPostBack()) {
                args.set_cancel(true);
            }
            $("#pleaseWaitDialog").modal("show");
            //$("#ajax_fade").modal("show");
        }
        function EndRequest(sender, args) {
            $("#pleaseWaitDialog").modal("hide");
            //$("#ajax_fade").modal("hide");
        }
    </script>

    <script type="text/javascript">

        <%--function hideMessage() {
            var messageDiv = $("#<%= lblMsg.ClientID %>");
            if (messageDiv.is(":visible")) {
                setTimeout(function () {
                    messageDiv.hide(300);
                }, 2000);
            }
        }

        $(document).ready(function () {
            setInterval(hideMessage, 2000);
        });
        $(function () {
            $(window).load(function () {
                setTimeout(function () {
                    $("#thongbaoSucces_div").modal("hide");
                    $("#thongbaoError_div").modal("hide");
                    $("#thongBaoSuccess").modal("hide");
                }, 1500);
            })
        });--%>

        $(function () {
            $(window).load(function () {
                setTimeout(function () {
                    $("#thongbaoSucces_div").modal("hide");
                    $("#thongbaoError_div").modal("hide");
                    $("#thongBaoSuccess").modal("hide");
                }, 1500);
            })
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            setTimeout(function () {
                $("#thongbaoError_div").modal("hide");
                $("#thongbaoSucces_div").modal("hide");
                $("#thongBaoSuccess").modal("hide");
            }, 1500);
        });

    </script>

    <script type="text/javascript">
        var dialog_title = "<img src='images/edit-add.png' style='vertical-align:middle;' /> Thêm/Sửa Cán bộ";
        var checkAvailableUrl = "checkNhanVienAvailable";
        var saveDbUrl = "saveNhanVien";
        var fetchDetailUrl = "editNhanVien";      

        // must have for all page
        function initValidationRule(field, rules, i, options) {
            return false;
        }
        function showAddGroupForm() {
            //$("#MainContent_fade").show();
            selectedRow = $("tr.selected_hl");
            if (selectedRow.length == 0) {
                $("#thongbaoError_div").modal("show");
                $("#MainContent_lblContentErr").html("Chưa chọn chức năng!");
                $("#MainContent_lblContentSucsses").html("");
            }
            else {
                $("#addGroupForm").modal("show");

                var config = {
                    '.chosen': {}
                }
                for (var selector in config) {
                    $(selector).chosen(config[selector]);
                }
                $('#ctl00_MainContent_ddlCanBo').trigger("chosen:updated");
                $(".chosen").trigger("chosen:updated");

                return false;
                //$("html").animate({ scrollTop: 0 }, 400);
            }
            return false;
        }


        function hideErrorConfirm() {
            $("#thongbaoError_div").modal("hide");
        };

        function showthongBaoSuccess() {
            $("#thongbaoSucces_div").modal();
            return false;
        }

        function hideSuccessConfirm() {
            $("#thongbaoSucces_div").modal("hide");
        };

        function showThongBaoThanhCong() {
            $("#thongbaoSucces_div").modal();
            return false;
        }

        function showthongBaoError() {
            $("#thongbaoError_div").modal();
            return false;
        }

        function hideErrorMessage(div) {
            $("#MainContent_fade").hide();
            HideMessage(div);

        }

        function hideAddGroupForm() {
            $("#addGroupForm").modal("hide");
            $("#MainContent_fade").hide();
            return false;
        }

        function showThongBaoSucces() {
            $("#thongBaoSuccess").modal();
            return false;
        }


        function hideThongBaoSucces() {
            $("#thongBaoSuccess").modal("hide");
            $("#MainContent_fade").hide();
        }

        function hideError() {
            $("#MainContent_error").hide();
            $("#MainContent_fade").hide();
        }

        function showAddForm() {
            $("#MainContent_light").show();
            $("#MainContent_fade").show();

            return false;
        }

        function hideAddEditForm() {
            $("#MainContent_light").hide(300);
            $("#MainContent_fade").hide();
        }

        function ConfirmDeleteGroup(button) {
            //$("#MainContent_fade").show();
            $("#groupDeleteConfirm").modal("show");
            $("#MainContent_hdDeleteGroupID").val($(button).prev().val());
            return false;
        }

        function hideDeleteGroupConfirm() {
            $("#MainContent_fade").hide;
            $("#groupDeleteConfirm").modal("hide");
        }

        function hideSuccessMsg() {
            $("#MainContent_ajax_fade").hide();
            $("#MainContent_success").hide(300);
        }

        function selectChucNang(tr, cnID, event) {
            var sender = event.target;
            if (sender.tagName == "TD") {
                //add selected style and remove selected style of other rows
                $(tr).addClass("selected_hl");
                $(tr).siblings().removeClass("selected_hl");
                //change hidden ddl value
                $("#MainContent_ddlChucNang").val(cnID);
                $("#MainContent_ddlChucNang").change();
            }
        }

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

    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="addGroupForm" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">
                        <img src="../images/users_icon.png" style="vertical-align: middle;">Thêm nhóm</h4>
                </div>
                <div class="modal-body">
                    <div class="form-horizontal" role="form">
                        <div class="form-group">
                            <label class="col-lg-3 col-sm-3 control-label">Chọn nhóm:</label>
                            <div class="col-lg-9">
                                <asp:DropDownList ID="ddlNhom" runat="server" DataTextField="TenNhom" DataValueField="NhomNguoiDungID" CssClass="chosen" Width="200">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-lg-3 col-sm-3 control-label">Quyền:</label>
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
                    <asp:Button ID="btnSaveNhom" runat="server" Text="Lưu lại" CssClass="save" OnClick="btnSaveNhom_Click" OnClientClick="hideAddGroupForm();"
                        CausesValidation="false" />
                    <asp:Button type="button" CssClass="save btn-cancel" Text="Hủy" OnClientClick="return hideAddGroupForm();"
                        align="left" runat="server" ID="Button3" />
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="groupDeleteConfirm" class="modal fade">
        <div class="modal-dialog  modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">
                        <asp:HiddenField ID="hdDeleteGroupID" runat="server" Value="0" />
                        <span>Thao tác xóa dữ liệu.</span>
                    </h4>
                </div>
                <div class="modal-body">
                    <span style="float: left; margin: 0 7px 20px 0;" class="ui-icon ui-icon-alert"></span>
                    <span id="delete-form-message">Bạn có chắc chắn muốn xóa nhóm?</span>
                </div>
                <div class="modal-footer" style="text-align: center">
                    <asp:Button runat="server" CssClass="btn btn-danger btn-sm" Text="CÓ" OnClick="btnDeleteGroup_Click" ID="btnDeleteGroup" OnClientClick="hideDeleteGroupConfirm();"></asp:Button>
                    <button type="button" class="btn btn-primary btn-sm" role="button" aria-disabled="false" onclick="hideDeleteGroupConfirm();">
                        <span class="ui-button-text">KHÔNG</span>
                    </button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="thongBaoSuccess" class="modal fade">
        <div class="modal-dialog  modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">
                        <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
                        <span>Thông báo</span>
                    </h4>
                </div>
                <div class="modal-body">
                    <span style="float: left; margin: 0 7px 20px 0;" class="ui-icon ui-icon-alert"></span>
                    <span>Cập nhật dữ liệu thành công</span>
                </div>
                <div class="modal-footer" style="text-align: center">
                    <button type="button" class="btn btn-danger btn-sm" onclick="hideThongBaoSucces();">Đóng</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <div id="ajax_fade" class="black_overlay" style="z-index: 9000"></div>

    <div id="fade" class="black_overlay" runat="server">
    </div>
    <div style="text-align: center">
        <asp:Label ID="lblMsg" ForeColor="#008d4c" Text="" Visible="false" runat="server" CssClass="" />
    </div>


    <!-- Content Header (Page header) -->
    <div class="content-header">
        <h1>Danh mục chức năng 
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="#">Hệ thống</a></li>
            <li class="active">QL chức năng phân quyền</li>
        </ol>
    </div>

     <!-- Main content -->
    <div class="content">
        <div class="row">
            <div class="col-xs-12">
                <div class="box">
                    <div class="box-header">
                        <%--<button type="button" class="btn btn-primary" id="btnThemNhom" onclick="showAddForm(); return false">
                            <span class="glyphicon glyphicon glyphicon-plus-sign" style="margin-right:5px"></span>Thêm nhóm người dùng
                        </button>--%>
                        <asp:Panel ID="pnlPanel" runat="server" DefaultButton="btnSearch">
                            <asp:Button ID="btnSearch" runat="server" CausesValidation="false" CssClass="btn btn-default btn-sm"
                                OnClick="btnSearch_Click" Text="Tìm kiếm" Style="float: right; margin-bottom: 10px" />
                            <asp:TextBox ID="txtSearch" placeholder="Nhập nội dung cần tìm kiếm" runat="server" CssClass="form-control input-search" Style="float: right; margin-right: 10px; margin-bottom: 10px; width: 30%"></asp:TextBox>

                        </asp:Panel>
                    </div>
                    <div class="box-body ">
                        <div class="table-responsive">
                        <table id="table" class="table table-bordered table-hover" style="margin-top: 15px; width: 100%;">
                            <thead>
                                <tr>
                                    <th style="width: 30%;">Tên chức năng</th>
                                    <th>Thuộc chức năng</th>
                                </tr>
                            </thead>
                            <asp:Repeater ID="rptChucNang" runat="server">
                                <ItemTemplate>
                                    <tr class='<%# Container.ItemIndex % 2 == 0 ? "even" : "odd" %>' onclick="selectChucNang(this, <%# Eval("ChucNangID") %>, event)">
                                        <td style="text-align: left;">
                                            <%# Eval("TenChucNang") %>
                                        </td>
                                        <td style="text-align: left;">
                                            <%# Eval("TenChucNangCha") %>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                        </div>

                        <div class="paginations" style="margin-top: 15px; margin-bottom: 15px">
                            <asp:PlaceHolder ID="plhPaging" runat="server"></asp:PlaceHolder>
                        </div>

                        <asp:DropDownList ID="ddlChucNang" runat="server" CssClass="display-none form-control" Width="150" Style="margin-left: 10px; display: none;" DataTextField="TenChucNang"
                            DataValueField="ChucNangID" AutoPostBack="true" OnSelectedIndexChanged="ddlChucNang_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="udpUserRole">
                            <ContentTemplate>

                                <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" class="modal fade" id="thongbaoSucces_div">
                                    <div class="modal-dialog  modal-sm">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <span id="lblHeaderSuccess">Thông báo
                                                </span>
                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                    <span aria-hidden="true">&times;</span>
                                                </button>
                                            </div>
                                            <div class="modal-body">
                                                <%--<img alt="" src="../images/messagebox_info.png" style="width: 30px; margin-left: 7px; margin-top: 14px;" />--%>
                                                <asp:Label ID="lblContentSuccess" CssClass="content-message"
                                                    runat="server"></asp:Label>
                                            </div>
                                            <div class="modal-footer" style="text-align: center">
                                                <button type="button" class="btn btn-danger btn-sm" onclick="hideSuccessConfirm(); return false">Đóng</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" class="modal fade" id="thongbaoError_div">
                                    <div class="modal-dialog  modal-sm">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                    <span aria-hidden="true">&times;</span></button>
                                                <h4 class="modal-title">Thông báo !</h4>
                                            </div>
                                            <div class="modal-body">
                                                <span>
                                                    <asp:Label ID="lblContentErr" runat="server"></asp:Label>
                                                </span>
                                            </div>
                                            <div class="modal-footer" style="text-align: center">
                                                <button type="button" class="btn btn-danger btn-sm" onclick="hideErrorConfirm(); return false">Đóng</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row" style="margin-top: 15px;">
                                    <div class="col-xs-12">
                                        <%--<div class="col-md-12">--%>
                                        <div class="panel panel-default">
                                            <div class="panel-heading">
                                                Thêm nhóm
                                                    <div style="width: auto; float: right;">
                                                        <asp:LinkButton runat="server" ID="btnThemNhom" OnClientClick="showAddGroupForm(); return false;" Style="font-size: 12px; cursor: pointer;" CausesValidation="false">
                                                            <asp:Image ID="Image3" runat="server" ImageUrl="~/images/add.jpeg" Style="vertical-align: top;" />
                                                            <span>Thêm nhóm</span>
                                                        </asp:LinkButton>
                                                    </div>
                                            </div>
                                            <div class="panel-body">
                                                <ul id="Ul1" style="list-style-type: none;">
                                                    <asp:Repeater ID="rptNhom" runat="server" OnItemCommand="rptNhom_ItemCommand" OnItemDataBound="rptNhom_ItemDataBound">
                                                        <ItemTemplate>
                                                            <li class="phanquyen" style="clear: both;">
                                                                <span class='list'><%# Eval("TenNhom") %></span>
                                                                <asp:HiddenField runat="server" Value='<%# Eval("NhomNguoiDungID") %>' />
                                                                <asp:ImageButton ID="btnDelete" CssClass="addition_area_delete_img" ImageUrl="~/images/delete.png" runat="server" CommandName="DeleteGroup" CommandArgument='<%# Eval("NhomNguoiDungID") %>' CausesValidation="false" ToolTip="Xóa nhóm" OnClientClick="ConfirmDeleteGroup(this); return false;" />
                                                                <asp:ImageButton ID="btnSave" CssClass="addition_area_delete_img" ImageUrl="~/images/save_img.png" runat="server" CommandName="SaveQuyen" CommandArgument='<%# Eval("NhomNguoiDungID") %>' CausesValidation="false" ToolTip="Lưu quyền" />
                                                                <asp:CheckBoxList ID="cblQuyen" runat="server" CssClass="phanquyen-cbl" RepeatDirection="Horizontal" AutoPostBack="false" ClientIDMode="AutoID">
                                                                    <asp:ListItem Text="Đọc" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="Tạo mới" Value="2"></asp:ListItem>
                                                                    <asp:ListItem Text="Sửa" Value="4"></asp:ListItem>
                                                                    <asp:ListItem Text="Xóa" Value="8"></asp:ListItem>
                                                                </asp:CheckBoxList>
                                                            </li>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </ul>
                                            </div>
                                        </div>
                                        <%--</div>--%>
                                    </div>
                                </div>


                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlChucNang" />
                                <asp:AsyncPostBackTrigger ControlID="btnSaveNhom" />
                                <asp:AsyncPostBackTrigger ControlID="btnDeleteGroup" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
        <!-- end #dashboard -->
    </div>

    <div id="sidebar" class="right" aria-hidden="true">
        <asp:Literal ID="ltrSideMenu" runat="server"></asp:Literal>
    </div>
    <!-- end #sidebar -->
    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" class="modal fade jquery-msgbox" style="display: none; position: absolute; top: 114px; left: 50%; margin-left: -200px; width: 400px; height: auto; z-index: 10000; word-wrap: break-word; -webkit-box-shadow: rgba(0, 0, 0, 0.498039) 0px 0px 15px; box-shadow: rgba(0, 0, 0, 0.498039) 0px 0px 15px; border-top-left-radius: 6px; border-top-right-radius: 6px; border-bottom-right-radius: 6px; border-bottom-left-radius: 6px; background-color: rgb(255, 255, 255);"
        id="error"
        runat="server">
        <div class="jquery-msgbox-wrapper jquery-msgbox-error" style="height: auto; min-height: 80px; zoom: 1;">
            <form action="#" method="post">
                Chưa chọn chức năng!<div class="jquery-msgbox-buttons">
                    <button type="button" onclick="hideError();">
                        Đóng</button>
                </div>
            </form>
        </div>
    </div>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div id="ajax_fade" aria-labelledby="myModalLabel" role="dialog" class="jquery-msgbox" runat="server" enableviewstate="false"></div>
            <div id="success" aria-labelledby="myModalLabel" role="dialog" class="jquery-msgbox" style="display: none; position: absolute; top: 114px; left: 50%; margin-left: -200px; width: 400px; height: auto; z-index: 10000; word-wrap: break-word; -webkit-box-shadow: rgba(0, 0, 0, 0.498039) 0px 0px 15px; box-shadow: rgba(0, 0, 0, 0.498039) 0px 0px 15px; border-top-left-radius: 6px; border-top-right-radius: 6px; border-bottom-right-radius: 6px; border-bottom-left-radius: 6px; background-color: rgb(255, 255, 255);"
                runat="server" enableviewstate="false">
                <div class="jquery-msgbox-wrapper jquery-msgbox-info" style="height: auto; min-height: 80px; zoom: 1;">
                    <form action="#" method="post">
                        Quyền đã được lưu!
                <asp:Label ID="lblDefaultPassword" runat="server"></asp:Label>
                        <div class="jquery-msgbox-buttons">
                            <button type="button" onclick="hideSuccessMsg();">
                                Đóng</button>
                        </div>
                    </form>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <style type="text/css">
        #MainContent_cblAccessRight label {
            display: inline-block;
            margin-left: 7px;
            margin-top: 2px;
            vertical-align: top;
            line-height: 17px;
        }
        
    </style>
</asp:Content>
