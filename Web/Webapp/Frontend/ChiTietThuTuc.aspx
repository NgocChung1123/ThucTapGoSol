<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChiTietThuTuc.aspx.cs" Inherits="Com.Gosol.CMS.Web.Webapp.Frontend.ChiTietThuTuc" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form2" runat="server">
        <div class="box box-primary" style="height: 100%">
            <div class="box-header " style="text-align: center;">
                <span class="box-title" style="text-align: center;">
                    <asp:Label runat="server" ID="lblTenThuTuc1"></asp:Label></span>
            </div>
            <div class="box-body">
                <table class="table table-bordered table-hover">
                    <tr>
                        <td style="text-align: center"><b>STT</b></td>
                        <td style="text-align: center"><b>Trình tự thực hiện</b></td>
                        <td style="text-align: center"><b>Tệp đính kèm</b></td>
                    </tr>
                    <asp:Repeater runat="server" ID="rptThuTuc" OnItemDataBound="rptThuTuc_ItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <td style="text-align: center">
                                    <asp:Label runat="server" ID="lblSTT"></asp:Label></td>
                                <td><%# Eval("NDThuTuc") %></td>
                                <td style="text-align: center" runat="server" id="tdFile">
                                    <%#  Com.Gosol.CMS.Utility.Utils.ConvertToString(Eval("FileDinhKem"),string.Empty)=="" 
                                                        ? ""
                                        :
                                        "<a href = '../../../Handler/DownloadFileQuyetDinh.ashx?filename="+Eval("FileDinhKem") + "'><img src='"+"../../../images/download.png"
                                        +"' style='"+"width: 20px; height: 20px"+"'/>" %>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
            <div class="box-footer">
                <div style="text-align: center">
                    <input type="button" class="btn" value="Quay lại" onclick="BackChiTiet();" />
                </div>
            </div>

        </div>

        <div style="text-align: left; display: none">
            <table class="table table-bordered table-hover" style="margin-top: 15px; width: 100%">
                <thead>
                    <tr>
                        <td style="width: 30%; text-align: center"><b>Tên đề mục</b></td>
                        <td style="width: 70%; text-align: center"><b>Nội dung chi tiết</b></td>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>Tên trình tự, thủ tục hành chính</td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>Trình tự thực hiện + Hồ sơ, giấy tờ liên quan</td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>Cơ sở pháp lý của thủ tục hành chính</td>
                        <td>
                            <asp:Label runat="server" ID="lblCoSoPhapLy"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>Tải thủ tục hành chính</td>
                        <td style="text-align: center" runat="server" id="tdDownload">
                            <%--<asp:LinkButton ID="btnDownload" ToolTip="Bấm vào đây để tải thủ tục" runat="server" OnClick="btnDownload_Click" CausesValidation="False">
                                <i class="fa fa-download"></i>
                            </asp:LinkButton>--%>
                        </td>
                    </tr>
                </tbody>
            </table>
            <div style="text-align: center">
                <input type="button" class="btn" value="Quay lại" onclick="BackChiTiet();" />
            </div>
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
            //function showthongBaoSuccess1() {
            //    //alert('1');
            //    window.location.href = "/Webapp/Frontend/TrinhTuThuTuc.aspx";
            //    //ShowChiTiet(i);
            //    //$("#MainContent_lblContentSuccess").val("File bạn muốn tải không tồn tại !");
            //    $("#successSubmit").modal("show");
            //    return false;
            //}

            function lol() {
                $("#successSubmit1").modal("show");
                return false;
            }

            function hideSuccessSubmit1() {
                $("#successSubmit1").modal("hide");
            }
        </script>
    </form>
</body>
</html>
