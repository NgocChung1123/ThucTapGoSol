<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ThamSoHeThong.aspx.cs" Inherits="Com.Gosol.CMS.Web.ThamSoHeThong" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="IndexContent" runat="server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

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
            setInterval(hideMessage, 2000);
        });
        $(function () {
            $(window).load(function () {
                setTimeout(function () {
                    $("#thongBaoSuccess").modal("hide");
                    $("#thongBaoError").modal("hide");
                }, 1500);
            })
        });
    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            $(".datepicker").datepicker();
        });


        function showThongBaoSuccess() {
            $("#thongBaoSuccess").modal();
            return false;
        }

        function hideThongBaoSuccess() {
            $("#thongBaoSuccess").modal("show");
            $("#MainContent_fade").show();
        }

        function showAddForm() {
            $("#MainContent_light").show();
            $("#MainContent_fade").show();

            return false;
        }

        function hideAddEditForm() {
            $("#MainContent_light").hide();
            $("#MainContent_fade").hide();
        }

        function resetForm() {
            $("#MainContent_txtSystemConfigID").val("");
            $("#MainContent_txtConfigKey").val("");
            $("#MainContent_txtConfigValue").val("");
            $("#MainContent_txtDescription").val("");
            hideEditFormThamSoHeThong();
        }

        function showEditFormThamSoHeThong() {
            $("#addUserForm").modal("show");
            $("#fade").modal("show");
        }
        function hideEditFormThamSoHeThong() {
            $("#addUserForm").modal("hide");
            $("#fade").modal("hide");
        }


    </script>



    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="addUserForm" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <img style="float: left;" src="/images/edit-add.png">
                    <h4 class="modal-title" style="margin-left: 30px;">Thêm người dùng vào nhóm</h4>
                </div>
                <div class="modal-body">

                    <div class="form-horizontal" role="form">
                        <div class="form-group">
                            <label class="col-lg-3 col-sm-3 control-label"></label>
                            <div class="col-lg-9">
                                <asp:TextBox ID="txtSystemConfigID" runat="server" Visible="false" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-lg-3 col-sm-3 control-label">Tên tham số: <span style="color: red;">*</span></label>
                            <div class="col-lg-9">
                                <asp:TextBox ID="txtConfigKey" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-3 col-sm-3 control-label">Giá trị: <span style="color: red;">*</span></label>
                            <div class="col-lg-9">
                                <asp:TextBox ID="txtConfigValue" runat="server" Enabled="true" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-3 col-sm-3 control-label">Ghi chú:</label>
                            <div class="col-lg-9">
                                <asp:TextBox ID="txtDescription" runat="server" Enabled="true" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                    </div>
                </div>
                <div class="modal-footer" style="text-align: center">
                    <asp:Button ID="btnSubmit" runat="server" CssClass="save validate_button" Text="Lưu lại" OnClick="btnSubmit_Click" />
                    <asp:Button ID="btnReset" runat="server" CssClass="save btn-cancel" Text="Hủy bỏ" OnClick="btnReset_Click" CausesValidation="false" OnClientClick="resetForm(); return false;" />
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <div id="fade" class="black_overlay" runat="server">
    </div>
    <div id="fade2" class="black_overlay">
    </div>

    <div class="content-header">
        <h1>Danh sách tham số hệ thống 
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="#">Hệ thống</a></li>
            <li class="active">Khai báo tham số hệ thống</li>
        </ol>
    </div>
    <div style="text-align: center">
        <asp:Label ID="lblMsg" ForeColor="#008d4c" Text="" Visible="false" runat="server" CssClass="" />
    </div>
    <div class="content">
        <div class="row">
            <div class="col-xs-12">
                <div class="box">
                    <div class="box-header">
                        <%--<button type="button" class="btn btn-primary" id="btnThemNhom" onclick="showAddForm(); return false">
                            <span class="glyphicon glyphicon glyphicon-plus-sign" style="margin-right:5px"></span>Thêm nhóm người dùng
                        </button>--%>
                    </div>
                    <div class="box-body table-responsive">

                        <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="thongBaoSuccess" class="modal fade">
                            <div class="modal-dialog  modal-sm">
                                <div class="modal-content">
                                    <div class="modal-header" style="background-color: #50679E; color: #fff">
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times;</span></button>
                                        <h4 class="modal-title">Thông báo!</h4>
                                    </div>
                                    <div class="modal-body">
                                        <span>
                                            <asp:Label ID="lblContentSuccess" CssClass="content-message" runat="server"></asp:Label></span>
                                    </div>
                                </div>
                                <!-- /.modal-content -->
                            </div>
                            <!-- /.modal-dialog -->
                        </div>


                        <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="thongBaoError" class="modal fade">
                            <div class="modal-dialog  modal-sm">
                                <div class="modal-content">
                                    <div class="modal-header" style="background-color: #50679E; color: #fff">
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times;</span></button>
                                        <h4 class="modal-title">Thông báo!</h4>
                                    </div>
                                    <div class="modal-body">
                                        <span>
                                            <asp:Label ID="lblContentErr" CssClass="content-message" runat="server"></asp:Label></span>
                                    </div>
                                </div>
                                <!-- /.modal-content -->
                            </div>
                            <!-- /.modal-dialog -->
                        </div>

                        <div class="col-lg-12">

                            <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnSearch">
                                <asp:Button ID="btnSearch" runat="server" CausesValidation="false" Style="float: right; margin-right: 10px; margin-bottom: 10px" CssClass="btn btn-default btn-sm" OnClick="btnSearch_Click" Text="Tìm kiếm" />
                                <asp:TextBox ID="txtSearch" placeholder="Nhập nội dung cần tìm kiếm" runat="server" CssClass="form-control" Style="float: right; margin-right: 10px; margin-bottom: 10px; width: 30%"></asp:TextBox>
                            </asp:Panel>

                        </div>

                        <table id="table" class="table table-bordered table-hover" style="margin-top: 15px">
                            <thead>
                                <tr>
                                    <th style="width: 30%; text-align: center;">Tham số</th>
                                    <th style="width: 20%; text-align: center;">Giá trị</th>
                                    <th style="text-align: center;">Ghi chú</th>
                                    <th style="width: 10%; text-align: center;">Thao tác
                                    </th>
                                </tr>
                            </thead>
                            <asp:Repeater ID="rptSystemConfig" runat="server" OnItemDataBound="rptSystemConfig_ItemDataBound" OnItemCommand="rptSystemConfig_ItemCommand">
                                <ItemTemplate>
                                    <tr class='<%# Container.ItemIndex % 2 == 0 ? "even" : "odd" %>'>
                                        <td style="text-align: left; width: 200px">
                                            <%# Eval("ConfigKey") %>
                                        </td>
                                        <td style="text-align: center;">
                                            <%# Eval("ConfigValue") %>
                                        </td>
                                        <td style="text-align: left;">
                                            <%# Eval("Description") %>
                                        </td>
                                        <td style="width: 50px; text-align: center" class="action-cell">
                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.png" CommandName="Edit"
                                                CommandArgument='<%# Eval("SystemConfigID") %>' CausesValidation="false" ToolTip="Sửa" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                        <div class="paginations" style="margin-top: 15px">
                            <asp:PlaceHolder ID="plhPaging" runat="server"></asp:PlaceHolder>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- end #dashboard -->
    <div id="sidebar" class="right">
        <asp:Literal ID="ltrSideMenu" runat="server">
        </asp:Literal>
    </div>
    <!-- end #sidebar -->
</asp:Content>
