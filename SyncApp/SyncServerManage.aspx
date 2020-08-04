<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SyncServerManage.aspx.cs" Inherits="SyncApp.SyncServerManage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function showDeleteConfirmDiv() {
            $("#deleteConfirmDiv").show();
            $("#fade").show();
        }

        function hideDeleteConfirmDiv() {
            $("#deleteConfirmDiv").hide();
            $("#fade").hide();
        }

        function showDAddDiv() {
            $("#addDiv").show();
            $("#fade").show();
        }

        function hideAddDiv() {
            $("#addDiv").hide();
            $("#fade").hide();
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="deleteConfirmDiv" class="popup confirm">   
        <label class="popup-header">
            Cảnh báo
        </label>
        <div class="popup-content">
            Bạn có chắc chắn muốn xóa máy chủ đồng bộ này?
        </div>
        <div class="popup-footer">
            <asp:Button ID="btnYes" runat="server" OnClick="btnYes_Click" Text="Có" />
            <asp:Button ID="btnNo" runat="server" OnClientClick="hideDeleteConfirmDiv(); return false;" Text="Không" />
        </div>
    </div>
    <div id="addDiv" class="popup">
        <label class="popup-header">
            Thêm/Sửa máy chủ đồng bộ
        </label>
        <div class="popup-content">
            <asp:HiddenField ID="hdfSyncServerID" runat="server" />
            <table>
                <tr>
                    <td>Tên máy chủ</td>
                    <td>
                        <asp:TextBox ID="txtSyncServerName" runat="server"></asp:TextBox>
                    </td>                    
                </tr>
                <tr>
                    <td>IP</td>
                    <td>
                        <asp:TextBox ID="txtIP" runat="server"></asp:TextBox>
                    </td>                    
                </tr>
                <tr>
                    <td>Hoạt động</td>
                    <td>
                        <asp:CheckBox ID="cbxIsActive" runat="server" />
                    </td>                    
                </tr>
                <tr>
                    <td>Ghi chú</td>
                    <td>
                        <asp:TextBox ID="txtDesc" runat="server" TextMode="MultiLine"></asp:TextBox>
                    </td>                    
                </tr>                
            </table>
        </div>
        <div class="popup-footer">
            <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Có" />
            <asp:Button ID="btnCancel" runat="server" OnClientClick="hideAddDiv(); return false;" Text="Hủy" />
        </div>
    </div>
    <div>
        <asp:LinkButton runat="server" Text="Thêm mới" OnClientClick="showAddDiv(); return false;"></asp:LinkButton>
        <table>
            <tr>
                <td>STT</td>
                <td>Tên máy chủ</td>
                <td>IP</td>
                <td>Hoạt động</td>
                <td>Ghi chú</td>
                <td>Thao tác</td>
            </tr>
            <asp:Repeater ID="rptSyncServer" runat="server">
                <ItemTemplate>
                    <tr>
                        <td><%# Container.ItemIndex %></td>
                        <td><%# Eval("SyncServerName") %></td>
                        <td><%# Eval("SyncServerIP") %></td>
                        <td><%# Eval("IsActive") %></td>
                        <td><%# Eval("Description") %></td>
                        <td>
                            <asp:LinkButton runat="server" CommandName="Edit">Sửa</asp:LinkButton>
                            <asp:LinkButton runat="server" OnClientClick="showDeleteConfirmDiv(); return false;">Xóa</asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </div>
</asp:Content>
