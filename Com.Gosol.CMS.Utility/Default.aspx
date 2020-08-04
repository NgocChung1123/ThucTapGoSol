<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="Com.Gosol.CMS.Web.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainGridContent" runat="server">
    <div class="itemCategory" style="height: auto !important">
        <fieldset class="items">
            <legend>&nbsp; Kế toán chi tiết &nbsp;</legend>
            <ul>
                <li onmouseover="this.className = 'liHover'" onmouseout="this.className = ''" style="width: 120px;
                    margin-left: 2px"><a href="/CapitalAccounting/ReceiptVoucher.aspx">
                        <img src="/image/phieuthu.png" /><br />
                        Phiếu thu</a></li>
                <li onmouseover="this.className = 'liHover'" onmouseout="this.className = ''" style="width: 120px;">
                    <a href="/CapitalAccounting/PaymentVoucher.aspx">
                        <img src="/image/phieuchi.png" /><br />
                        Phiếu chi</a></li>
                <li onmouseover="this.className = 'liHover'" onmouseout="this.className = ''" style="width: 120px;">
                    <a href="/CapitalAccounting/DebitNote.aspx">
                        <img src="/image/baono.png" /><br />
                        Báo nợ</a></li>
                <li onmouseover="this.className = 'liHover'" onmouseout="this.className = ''" style="width: 120px">
                    <a href="/CapitalAccounting/CreditNote.aspx">
                        <img src="/image/baoco.png" /><br />
                        Báo có</a></li>
            </ul>
            <div class="clr">
            </div>
        </fieldset>
        <div class="clr">
        </div>
        <fieldset class="items">
            <legend>&nbsp; Sổ kế toán &nbsp;</legend>
            <div>
                <ul>
                    <li onmouseover="this.className = 'liHover'" onmouseout="this.className = ''" style="width: 120px;
                        margin-left: 2px">
                        <asp:LinkButton ID="btnCBook" runat="server" CausesValidation="false" OnClick="btnCBook_Click">
                            <img src="/image/soquytienmat.png" alt="IMAGE" /><br />
                            Sổ quỹ tiền mặt</asp:LinkButton></li>
                    <li onmouseover="this.className = 'liHover'" onmouseout="this.className = ''" style="width: 120px">
                        <asp:LinkButton ID="btnBBook" runat="server" CausesValidation="false" OnClick="btnBBook_Click">
                            <img src="/image/sophunganhang.png" alt="IMAGE" /><br />
                            Sổ phụ ngân hàng</asp:LinkButton></li>
                    <li onmouseover="this.className = 'liHover'" onmouseout="this.className = ''" style="width: 120px">
                        <asp:LinkButton ID="btnADBook" runat="server" CausesValidation="false" OnClick="btnADBook_Click">
                            <img src="/image/icon_adbook.png" alt="IMAGE" /><br />
                            Sổ chi tiết tài khoản</asp:LinkButton></li>
                    <li onmouseover="this.className = 'liHover'" onmouseout="this.className = ''" style="width: 120px">
                        <asp:LinkButton ID="btnBCBook" runat="server" CausesValidation="false" OnClick="btnBCBook_Click">
                            <img src="/image/icon_bcbook.png" alt="IMAGE" /><br />
                            Sổ chi phí đầu tư xây dựng</asp:LinkButton></li>
                    <li onmouseover="this.className = 'liHover'" onmouseout="this.className = ''" style="width: 120px">
                        <asp:LinkButton ID="btnIBook" runat="server" CausesValidation="false" OnClick="btnIBook_Click">
                            <img src="/image/icon_ibook.png" alt="IMAGE" /><br />
                            Sổ chi tiết nguồn vốn đầu tư</asp:LinkButton></li>
                    <li onmouseover="this.className = 'liHover'" onmouseout="this.className = ''" style="width: 120px">
                        <asp:LinkButton ID="btnPMCBook" runat="server" CausesValidation="false" OnClick="btnPMCBook_Click">
                            <img src="/image/icon_pmcbook.png" alt="IMAGE" /><br />
                            Sổ chi phí BQLDA</asp:LinkButton></li>
                    <li onmouseover="this.className = 'liHover'" onmouseout="this.className = ''" style="width: 120px">
                        <asp:LinkButton ID="btnOPBook" runat="server" CausesValidation="false" OnClick="btnOPBook_Click">
                            <img src="/image/icon_opbook.png" alt="IMAGE" /><br />
                            Sổ chi phí khác</asp:LinkButton></li>
                    <li onmouseover="this.className = 'liHover'" onmouseout="this.className = ''" style="width: 120px">
                        <asp:LinkButton ID="btnBDBook" runat="server" CausesValidation="false" OnClick="btnBDBook_Click">
                            <img src="/image/icon_bdbook.png" alt="IMAGE" /><br />
                            Sổ chi phí thu, chi bán hồ sơ mời thầu</asp:LinkButton></li>
                    <li onmouseover="this.className = 'liHover'" onmouseout="this.className = ''" style="width: 120px">
                        <asp:LinkButton ID="btnPDBook" runat="server" CausesValidation="false" OnClick="btnPDBook_Click">
                            <img src="/image/icon_pdbook.png" alt="IMAGE" /><br />
                            Sổ chi tiết thanh toán</asp:LinkButton></li>
                </ul>
            </div>
            <div class="clr">
            </div>
            <div>
                <ul>
                    <li onmouseover="this.className = 'liHover'" onmouseout="this.className = ''" style="width: 120px;
                        margin-left: 2px"><a href="/GeneralBook/MainBook.aspx">
                            <img src="/image/icon_mainbook.png" /><br />
                            Sổ cái</a></li>
                    <li onmouseover="this.className = 'liHover'" onmouseout="this.className = ''" style="width: 120px">
                        <a href="/GeneralBook/DDBook.aspx">
                            <img src="/image/icon_ddbook.png" /><br />
                            Chứng từ ghi sổ</a></li>
                    <li onmouseover="this.className = 'liHover'" onmouseout="this.className = ''" style="width: 120px">
                        <a>
                            <img src="/image/icon_diary_disable.png" /><br />
                            Nhật ký chung</a></li>
                    <li onmouseover="this.className = 'liHover'" onmouseout="this.className = ''" style="width: 120px">
                        <a>
                            <img src="/image/icon_drbook_disable.png" /><br />
                            Sổ đăng ký chứng từ ghi sổ</a></li>
                </ul>
            </div>
            <div class="clr">
            </div>
        </fieldset>
        <div class="clr">
        </div>
        <fieldset class="items">
            <legend>&nbsp; Báo cáo &nbsp;</legend>
            <ul>
                <li onmouseover="this.className = 'liHover'" onmouseout="this.className = ''" style="width: 120px;
                    margin-left: 2px">
                    <asp:LinkButton ID="btnBS" runat="server" CausesValidation="false" OnClick="btnBS_Click">
                <img src="/image/icon_bsreport.png" alt="Bảng cân đối kế toán"/><br />
                Bảng cân đối kế toán
                    </asp:LinkButton>
                </li>
                <li onmouseover="this.className = 'liHover'" onmouseout="this.className = ''" style="width: 120px">
                    <asp:LinkButton ID="btnFSN" runat="server" CausesValidation="false" OnClick="btnFSN_Click">
                <img src="/image/icon_fsnreport.png" /><br />
                Thuyết minh báo cáo tài chính</asp:LinkButton></li>
                <li onmouseover="this.className = 'liHover'" onmouseout="this.className = ''" style="width: 120px">
                    <asp:LinkButton ID="btnIR" runat="server" CausesValidation="false" OnClick="btnIR_Click">
                <img src="/image/icon_ireport.png" /><br />
                Nguồn kinh phí đầu tư</asp:LinkButton></li>
                <li onmouseover="this.className = 'liHover'" onmouseout="this.className = ''" style="width: 120px">
                    <asp:LinkButton ID="btnIDR" runat="server" CausesValidation="false" OnClick="btnIDR_Click">
                <img src="/image/icon_idreport.png" /><br />
                Chi tiết nguồn kinh phí đầu tư</asp:LinkButton></li>
                <li onmouseover="this.className = 'liHover'" onmouseout="this.className = ''" style="width: 120px">
                    <asp:LinkButton ID="btnIPR" runat="server" CausesValidation="false" OnClick="btnIPR_Click">
                <img src="/image/icon_ipreport.png" /><br />
                Thực hiện đầu tư</asp:LinkButton></li>
                <li onmouseover="this.className = 'liHover'" onmouseout="this.className = ''" style="width: 120px">
                    <asp:LinkButton ID="btnIPDR" runat="server" CausesValidation="false" OnClick="btnIPDR_Click">
                <img src="/image/icon_ipdreport.png" /><br />
                Chi tiết thực hiện đầu tư</asp:LinkButton></li>
                <li onmouseover="this.className = 'liHover'" onmouseout="this.className = ''" style="width: 120px">
                    <asp:LinkButton ID="btnOP" runat="server" CausesValidation="false" OnClick="btnOP_Click">
                <img src="/image/icon_opreport.png" /><br />
                Chi phí khác</asp:LinkButton></li>
                <li onmouseover="this.className = 'liHover'" onmouseout="this.className = ''" style="width: 120px">
                    <asp:LinkButton ID="btnPMCR" runat="server" CausesValidation="false" OnClick="btnPMCR_Click">
                <img src="/image/icon_pmcreport.png" /><br />
                Chi phí Ban quản lý dự án</asp:LinkButton></li>
            </ul>
            <div class="clr">
            </div>
        </fieldset>
        <div class="clr">
        </div>
        <fieldset class="items" style="margin-bottom: 15px">
            <legend>&nbsp; Hệ thống &nbsp;</legend>
            <ul>
                <li onmouseover="this.className = 'liHover'" onmouseout="this.className = ''" style="width: 120px;
                    margin-left: 2px"><a href="/AccountManage.aspx">
                        <img src="/image/danhmuc/tkketoan_to.png" /><br />
                        Danh mục TK Kế toán</a></li>
                <li onmouseover="this.className = 'liHover'" onmouseout="this.className = ''" style="width: 120px">
                    <a href="/ProjectManage.aspx">
                        <img src="/image/danhmuc/duan_to.png" /><br />
                        Danh mục dự án</a></li>
                <li onmouseover="this.className = 'liHover'" onmouseout="this.className = ''" style="width: 120px">
                    <a href="/AgencyManage.aspx">
                        <img src="/image/danhmuc/congty_to.png" alt="Bảng cân đối kế toán" /><br />
                        Danh mục Công ty </a></li>
                <li onmouseover="this.className = 'liHover'" onmouseout="this.className = ''" style="width: 120px">
                    <a href="/StaffManage.aspx">
                        <img src="/image/danhmuc/nhanvien_to.png" /><br />
                        Danh mục Nhân viên</a></li>
                <li onmouseover="this.className = 'liHover'" onmouseout="this.className = ''" style="width: 120px">
                    <a href="/ResourcesManage.aspx">
                        <img src="/image/danhmuc/nguonvon_to.png" /><br />
                        Danh mục Nguồn vốn</a></li>
                <li onmouseover="this.className = 'liHover'" onmouseout="this.className = ''" style="width: 120px">
                    <a href="/PackageManage.aspx">
                        <img src="/image/danhmuc/goithau_to.png" /><br />
                        Danh mục gói thầu</a></li>
            </ul>
            <div class="clr">
            </div>
            <ul>
                <li onmouseover="this.className = 'liHover'" onmouseout="this.className = ''" style="width: 120px;
                    margin-left: 2px"><a href="/RoleManager/RolesManager.aspx">
                        <img src="/image/danhmuc/icon_access_control.png" /><br />
                        Thiết lập quyền truy cập</a></li>
                <li onmouseover="this.className = 'liHover'" onmouseout="this.className = ''" style="width: 120px">
                    <a href="/RoleManager/UserManage.aspx">
                        <img src="/image/danhmuc/user_to.png" /><br />
                        Quản lý người dùng</a></li>
                <li onmouseover="this.className = 'liHover'" onmouseout="this.className = ''" style="width: 120px">
                    <a href="/RoleManager/GroupManage.aspx">
                        <img src="/image/danhmuc/group_to.png" /><br />
                        Quản lý nhóm người dùng</a></li>
            </ul>
            <div class="clr">
            </div>
        </fieldset>
    </div>
    <div class="clr">
    </div>
    <div style="height: 25px">
    </div>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="Script" runat="server">
    <script type="text/javascript">        
    </script>
</asp:Content>
