<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DoiMatKhau.aspx.cs" Inherits="Com.Gosol.CMS.Web.DoiMatKhau" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="IndexContent" runat="server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">

        function hideMessage() {
            var messageDiv = $("#<%= lblError.ClientID %>");
            if (messageDiv.is(":visible")) {
                setTimeout(function () {
                    messageDiv.hide(200);
                }, 2000);
            }
        }

        $(document).ready(function () {
            setInterval(hideMessage, 2000);
        });

        $(function () {
            $(window).load(function () {
                setTimeout(function () {
                    //$("#thongbaoSucces_div").modal("hide");
                    $("#thongbaoError_div").modal("hide");
                }, 1500);
            })
        });
    </script>


    <script type="text/javascript">
        function showthongBaoSuccess() {
            $("#thongbaoSucces_div").modal({backdrop: 'static', keyboard: false});
            return false;
        };
        function showthongBaoError() {
            $("#thongbaoError_div").modal();
            return false;
        };
    </script>


    <div class="content-header">
        <h1>Đổi mật khẩu      
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="#">Tables</a></li>
            <li class="active">Data tables</li>
        </ol>
    </div>
    <!-- message area -->
    <div id="fade2" class="black_overlay" runat="server" />

    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="thongbaoSucces_div" class="modal fade">
        <div class="modal-dialog  modal-sm">
            <div class="modal-content">
                <div class="modal-header" style="background-color: #50679E; color: #fff">
                    <%--<button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>--%>
                    <h4 class="modal-title">Thông báo!</h4>
                </div>
                <div class="modal-body">
                    <span>
                        <asp:Label ID="lblContentSuccess" CssClass="content-message" runat="server"></asp:Label></span>
                </div>
                <div class="modal-footer" style="text-align:center">
                    <button type="button" class="btn btn-primary btn-sm" onclick="window.location.href = '../SignOutP.aspx';" >Đăng nhập lại</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="thongbaoError_div" class="modal fade">
        <div class="modal-dialog  modal-sm">
            <div class="modal-content">
                <div class="modal-header" style="background-color: #50679E; color: #fff">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Thông báo!</h4>
                </div>
                <div class="modal-body">
                        <span><asp:Label ID="lblContentErr" CssClass="content-message" runat="server"></asp:Label></span>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <%--<div class="confirmDiv message" id="thongbaoSucces_div">
        <div class="header-message">
            <label id="lblHeaderSuccess" class="header-message">
                Thông báo
            </label>
            <img src="images/close.ico" class="img-close" onclick="HideMessage(this);" />
        </div>
        <div class="content-message">
            <img alt="" src="../images/messagebox_info.png" style="width: 30px; margin-left: 7px; margin-top: 14px;" /><asp:Label ID="lblContentSuccess" CssClass="content-message"
                runat="server"></asp:Label>
        </div>
    </div>--%>

    <%--<div class="confirmDiv message" id="thongbaoError_div">
        <div class="header-message">
            <label id="lblHeaderErr" class="header-message">
                Lỗi
            </label>
            <img src="images/close.ico" class="img-close" onclick="HideMessage(this);" />
        </div>
        <div class="content-message">
            <img alt="" src="../images/Error.png" style="width: 26px; margin-left: 7px; margin-top: 14px;" /><asp:Label ID="lblContentErr" CssClass="content-message" runat="server"></asp:Label>
        </div>
    </div>--%>

    <div class="content">
        <div class="row">
            <div class="col-xs-12">
                <div class="box">
                    <div class="box-header">
                        <div class="box-body">
                            <%--<button type="button" class="btn btn-primary" id="btnThemNhom" onclick="showAddForm(); return false">
                            <span class="glyphicon glyphicon glyphicon-plus-sign" style="margin-right:5px"></span>Thêm nhóm người dùng
                        </button>--%>
                        </div>

                        <div class="content-body">
                            <%--<div style="clear: both; text-align: center; background-color: #fff; padding: 10px;">
            </div>--%>
                            <asp:Panel ID="pn" runat="server" DefaultButton="btnSubmit">
                                <table style="margin-left: 20px;">
                                    <tr class="tab_tr">
                                        <td style="text-align: left">Mật khẩu cũ:<span style="color: red">*</span></td>
                                        <td style="padding-left: 10px;">
                                            <asp:TextBox ID="txtOldPassword" runat="server" TextMode="Password" Width="200" CssClass="validate[required]"></asp:TextBox></td>
                                        <td style="padding-left: 10px;"></td>
                                    </tr>

                                    <tr class="tab_tr">
                                        <td style="text-align: left">Mật khẩu mới:<span style="color: red">*</span></td>
                                        <td style="padding-left: 10px;">
                                            <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" Width="200" CssClass="validate[required,minSize[6]]"></asp:TextBox></td>
                                        <td style="padding-left: 10px;"></td>
                                    </tr>

                                    <tr class="tab_tr">
                                        <td style="text-align: left">Nhập lại mật khẩu:<span style="color: red">*</span></td>
                                        <td style="padding-left: 10px;">
                                            <asp:TextBox ID="txtNewPasswordRepeat" runat="server" TextMode="Password" Width="200" CssClass="validate[required]"></asp:TextBox></td>
                                        <td style="padding-left: 10px;"></td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td style="padding-left: 13px;">
                                            <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="height: 45px;">
                                        <td></td>
                                        <td style="vertical-align: bottom; padding-left: 10px;">
                                            <asp:Button ID="btnSubmit" runat="server" CssClass="save validate_button btn btn-sm btn-default" OnClick="btnSubmit_Click" Text="Lưu" />
                                            <asp:Button ID="btnCancel" runat="server" CssClass="save btn-cancel btn btn-sm btn-default" OnClientClick="resetForm(); return false;" Text="Hủy" CausesValidation="false" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
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
    <script type="text/javascript">
        function hideAdd() {
            $("#MainContent_light").hide();
            $("#MainContent_fade").hide();
        }
        function hideDelete() {
            $("#MainContent_popXoa").hide();
            $("#MainContent_fade").hide();
        }

        function resetForm() {
            $("#MainContent_txtOldPassword").val("");
            $("#MainContent_txtNewPassword").val("");
            $("#MainContent_txtNewPasswordRepeat").val("");
        }

    </script>
    <style type="text/css">
        .tab_tr {
            height: 30px;
        }
    </style>
</asp:Content>
