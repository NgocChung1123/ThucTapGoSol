<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChiTietCauHoi.aspx.cs" Inherits="Com.Gosol.CMS.Web.Webapp.Frontend.ChiTietCauHoi" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="row">
            <div class="col-md-12">
                <h4 style="font-size:16px; font-weight:700">
                <img src="../../images/icon_tit_help_kntc.png" />
                    <asp:Label runat="server" ID="lblTieuDe"></asp:Label></h4>
                <p style="font-size:13px; font-weight:400">
                    <i>Người gửi:
                        <asp:Label runat="server" ID="lblNguoiHoi"></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                   Ngày hỏi:
                        <asp:Label runat="server" ID="lblNgayHoi"></asp:Label></i>
                </p>
                <asp:Label runat="server" ID="lblNDCauHoi"></asp:Label>
                <hr />
                <h4 style="font-size:16px; font-weight:700">
                <img src="../../images/icon_tit_help_kntc.png" />Câu trả lời

                </h4>
                <asp:Panel runat="server" ID="pnTraLoi">
                <p style="font-size:13px; font-weight:400"><i>Người trả lời: Admin</i></p>
                <p style="font-size:13px; font-weight:700">
                    <asp:Label runat="server" ID="lblNguoiTraLoi"></asp:Label>
                    xin trả lời như sau</p>
                <asp:Label runat="server" ID="lblNDTraLoi"></asp:Label>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="pnChuaTraLoi">
                        <p style="font-size:13px; font-weight:400"><i>Hiện tại chưa có câu trả lời</i></p>
                    </asp:Panel>
                <br />
                <br />
                <div style="width: 100%" class="text-center">
                    <input type="button" class="btn" onclick="BackChiTiet();" value="Quay lại" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
