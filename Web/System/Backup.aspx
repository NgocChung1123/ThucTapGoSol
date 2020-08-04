<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Backup.aspx.cs" Inherits="Com.Gosol.CMS.Web.Backup" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="IndexContent" runat="server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content-header">
        <h1>Sao lưu và phục hồi dữ liệu </h1>
        <ol class="breadcrumb">
            <li><a href="/Default.aspx"><i class="fa fa-dashboard"></i>Trang chủ</a></li>
            <li><a href="#">Hệ thống</a></li>
            <li class="active">Sao lưu và phục hồi dữ liệu</li>
        </ol>
    </div>



    <div class="content">
        <div class="row">
            <div class="col-xs-12">
                <div class="box">
                    <div class="content-body">
                        <div class="box-body">
                            <!-- message area -->
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    Sao lưu
                                </div>
                                <div class="panel-body">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <label class="col-lg-1 col-sm-2  control-label" style="padding:5px;padding-top:10px">Tên file :</label>
                                            <div class="col-lg-4" style="padding:5px">
                                                <asp:TextBox ID="txtBackupFile" runat="server" CssClass="validate[required] form-control" placeholder="Nhập tên file"></asp:TextBox>
                                            </div>
                                            <div class="col-lg-3" style="padding:5px">
                                                <asp:Button ID="btnBackup" runat="server" OnClick="btnBackup_Click" Text="Sao lưu"
                                                    CssClass="btn btn-default btn-sm validate_button" OnClientClick="validateForm();" />
                                            </div>
                                        </div>
                                            <asp:Label ID="lblError" ForeColor="Red" Text="" Visible="false" runat="server" CssClass="" />
                                    </div>
                                </div>
                            </div>
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    Phục hồi
                                </div>
                                <div class="panel-body">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <label class="col-lg-1 col-sm-2  control-label" style="padding: 5px; padding-top: 10px">Chọn file :</label>
                                            <div class="col-lg-4" style="padding: 5px">
                                                <asp:DropDownList ID="ddlBackupFile" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-lg-3" style="padding: 5px">
                                                <asp:Button runat="server" Text="Phục hồi" CssClass="btn btn-default btn-sm" OnClientClick="showConfirm(); return false;"
                                                    ID="btnRestore" />
                                            </div>
                                        </div>
                                        <b>Chú ý: </b>Quá trình khôi phục dữ liệu có thể kéo dài. Trong quá trình này, những
                                người dùng khác không thể truy cập hệ thống.<br />
                                          <asp:Label ID="lblMessage" ForeColor="Red" Text="" Visible="false" runat="server" CssClass="" />
                                    </div>
                                </div>
                            </div>
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    Lịch sử sao lưu
                                </div>
                                <div class="panel-body">
                                    <div class="table-responsive">
                                        <table id="table" class="table table-bordered">
                                            <tr>
                                                <th>STT
                                                </th>
                                                <th>Thời gian
                                                </th>
                                                <th>Hành động
                                                </th>
                                                <th>Người thực hiện
                                                </th>
                                            </tr>
                                            <tr>
                                                <td>1</td>
                                                <td>15/11/2015 17:45:12</td>
                                                <td>Sao lưu dữ liệu</td>
                                                <td>Administrator</td>
                                            </tr>
                                            <tr>
                                                <td>1</td>
                                                <td>18/11/2015 19:23:02</td>
                                                <td>Phục hồi dữ liệu</td>
                                                <td>Administrator</td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                </div>
            </div>
            <!-- end #dashboard -->
        </div>
    </div>
    </div>
    <div id="sidebar" class="right">
        <asp:Literal ID="ltrSideMenu" runat="server"></asp:Literal>
    </div>
    <!-- end #sidebar -->

    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="thongbaoSucces_div" class="modal fade">
        <div class="modal-dialog  modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Thông báo!</h4>
                </div>
                <div class="modal-body">
                    <img alt="" src="../images/messagebox_info.png" style="width: 30px; margin-left: 7px; margin-top: 14px;" /><asp:Label ID="lblContentSuccess" CssClass="content-message"
                        runat="server"></asp:Label>
                </div>
                <div class="modal-footer" style="text-align: center">
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="thongbaoSucces_div" class="modal fade">
        <div class="modal-dialog  modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Thông báo!</h4>
                </div>
                <div class="modal-body">
                    <img alt="" src="../images/messagebox_info.png" style="width: 30px; margin-left: 7px; margin-top: 14px;" />Lỗi     
                    <img alt="" src="../images/Error.png" style="width: 26px; margin-left: 7px; margin-top: 14px;" /><asp:Label
                        ID="lblContentErr" CssClass="content-message" runat="server"></asp:Label>
                </div>
                <div class="modal-footer" style="text-align: center">
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>


    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="success" class="modal fade">
        <div class="modal-dialog  modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Thông báo!</h4>
                </div>
                <div class="modal-body">
                    <span>Dữ liệu đã được lưu trữ thành công?</span>
                </div>
                <div class="modal-footer" style="text-align: center">
                    <button type="button" class=" btn btn-danger btn-sm" role="button" aria-disabled="false" onclick="hideSuccessMsg();">
                        <span class="ui-button-text">Hủy bỏ</span>
                    </button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="restoreConfirm" class="modal fade">
        <div class="modal-dialog  modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Thông báo!</h4>
                </div>
                <div class="modal-body">
                    <span>Dữ liệu hệ thống sẽ được đưa về thời điểm sao lưu. Các dữ liệu khác
                    sẽ bị mất. Bạn có chắc chắn không?</span>
                </div>
                <div class="modal-footer" style="text-align: center">
                    <asp:Button runat="server" CssClass="deleteBtn" Text="CÓ" OnClick="btnRestoreConfirm_Click"
                        ID="btnRestoreConfirm" OnClientClick="hideConfirm(); showProgress();" CausesValidation="false"></asp:Button>
                    <button type="button" class="deldete-button btn-cancel save" role="button" aria-disabled="false"
                        onclick="hideConfirm(); return false;">
                        <span class="ui-button-text">Hủy bỏ</span>
                    </button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>


    <script type="text/javascript">
        function hideMessage() {
            var messageDiv = $("#<%= lblError.ClientID %>");
            if (messageDiv.is(":visible")) {
                setTimeout(function () {
                    messageDiv.hide(300);
                }, 2000);
            }
        }
        function hideMessagePhucHoi() {
            var messageDiv = $("#<%= lblMessage.ClientID %>");
                    if (messageDiv.is(":visible")) {
                        setTimeout(function () {
                            messageDiv.hide(300);
                        }, 2000);
                    }
                }

        $(document).ready(function () {
            setInterval(hideMessage, 2000);
            setInterval(hideMessagePhucHoi, 2000);
            
        });

        function hideSuccessMsg() {
            $("#MainContent_success").hide();
            $("#MainContent_fade").hide();
        }

        function validateForm() {
            if ($("#Form1").validationEngine("validate")) {
                $("#progressDiv").show();
                $("#ajax_fade").show();
                return $("#Form1").validationEngine("validate");
            }
            else {
                return false;
            }
        }

        function showProgress() {
            $("#progressDiv").show();
            $("#ajax_fade").show();
        }

        function showConfirm() {
            $("#MainContent_restoreConfirm").show();
            $("#MainContent_fade").show();
        }

        function hideConfirm() {
            $("#MainContent_restoreConfirm").hide();
            $("#MainContent_fade").hide();
        }
    </script>
</asp:Content>
