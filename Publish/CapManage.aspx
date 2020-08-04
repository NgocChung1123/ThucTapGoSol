<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CapManage.aspx.cs" Inherits="Com.Gosol.CMS.Web.CapManage" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <form action="" method="post">
        <link id="Link1" rel="stylesheet" type="text/css" media="all" href="styles/cap.css" runat="server" />
        <div id="main_panel_container">
            <div id="light" class="ui-dialog ui-widget ui-widget-content ui-corner-all ui-draggable popup" tabindex="-1" role="dialog" aria-labelledby="ui-dialog-title-macapForm" runat="server" style="z-index: 1001; width: 450px; left: 50%; margin-left: -225px">
                <div class="ui-dialog-titlebar ui-widget-header ui-corner-all ui-helper-clearfix">
                    <span class="ui-dialog-title" id="ui-dialog-title-macapForm">
                        <img src="images/edit-add.png" style="float: left;">
                        <font face="arial">&nbsp; Thêm mới/ Sửa thông tin cấp</font>
                    </span>
                    <a href="#" id="close_link" class="ui-dialog-titlebar-close ui-corner-all" role="button" onclick="hideAdd();">
                        <span class="ui-icon ui-icon-closethick">close</span>
                    </a>
                </div>
                <div id="macapForm" class="ui-dialog-content ui-widget-content" scrolltop="0" scrollleft="0" runat="server">
                    <ul>
                        <fieldset>

                            <table>
                                <tbody>
                                    <tr>
                                        <th class="field_label">Mã
                                        </th>
                                        <td colspan="2">
                                            <li id="wwgrp_CapID" class="wwgrp">
                                                <div id="wwctrl_CapID" class="wwctrl">
                                                    <asp:TextBox ID="CapID" runat="server" Width="248" Enabled="false" />
                                                </div>
                                            </li>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th class="field_label">Tên cấp (*)
                                        </th>
                                        <td colspan="2">
                                            <li id="wwgrp_name" class="wwgrp">
                                                <div id="wwctrl_name" class="wwctrl">
                                                    <asp:TextBox ID="txtTenCap" MaxLength="255" name="maCap.tenCap" Width="248" runat="server" />
                                                    <asp:RequiredFieldValidator ID="Validate_TenCap" runat="server" ErrorMessage="Tên cấp không được để trống" ControlToValidate="txtTenCap" ForeColor="Red" Font-Bold="true"
                                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                                </div>
                                            </li>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th class="field_label">Cấp quản lý (*)
                                        </th>
                                        <td colspan="2">
                                            <li id="wwgrp_capQLy" class="wwgrp">
                                                <div id="wwctrl_capQLy" class="wwctrl">
                                                    <asp:TextBox ID="capQLy" name="maCap.capQLy" Width="248" MaxLength="255" runat="server" />
                                                    <asp:RequiredFieldValidator ID="Validate_ThuTu" runat="server" ErrorMessage="Cấp quản lý không được để trống" ControlToValidate="capQLy" ForeColor="Red" Font-Bold="true"
                                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                                    <asp:CompareValidator ID="cv" runat="server" ControlToValidate="capQLy" Type="Integer" ForeColor="Red" Font-Bold="true"
                                                        Operator="DataTypeCheck" ErrorMessage="Cấp quản lý chỉ được nhập số!" />
                                                </div>
                                            </li>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">&nbsp;
                                        </td>
                                    </tr>
                                    <!--  button fields -->
                                    <tr>
                                        <td colspan="3" align="center">
                                            <asp:Button ID="btnLuu" class="save" Text="Lưu lại" runat="server" OnClick="btnLuu_Click" CausesValidation="true" />
                                            <asp:Button ID="btnHuy" class="button-delete" Text="Hủy bỏ" OnClick="btnHuy_Click" runat="server" CausesValidation="false" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </fieldset>
                    </ul>
                </div>
            </div>

            <div id="popXoa" class="ui-dialog ui-widget ui-widget-content ui-corner-all ui-draggable popup" tabindex="-1" role="dialog" aria-labelledby="ui-dialog-title-delete-form" runat="server" style="z-index: 1001; width: 350px; left: 50%; margin-left: -175px">
                <div class="ui-dialog-titlebar ui-widget-header ui-corner-all ui-helper-clearfix">
                    <span class="ui-dialog-title" id="ui-dialog-title-delete-form">Thao tác xóa dữ liệu.</span>
                    <a href="#" id="close_link" class="ui-dialog-titlebar-close ui-corner-all" role="button" onclick="hideDelete();">
                        <span class="ui-icon ui-icon-closethick">close</span>
                    </a>
                </div>
                <div id="delete-form" class="ui-dialog-content ui-widget-content" scrolltop="0" scrollleft="0">
                    <p>
                        <span id="ui-icon ui-icon-alert" class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>
                        <span id="delete-form-message">Bạn có chắc muốn xóa cấp: 
            <asp:Label ID="ten_xoa" Text="" runat="server" />
                            <asp:Label ID="id_xoa" Text="text" runat="server" Style="display: none;" />
                        </span>
                    </p>
                </div>
                <div class="ui-dialog-buttonpane ui-widget-content ui-helper-clearfix">
                    <div class="ui-dialog-buttonset">
                        <asp:Button ID="Button1" class="save" Text="CÓ" runat="server" OnClick="btnXacNhan_Click" CausesValidation="false" />
                        <asp:Button ID="Button2" class="button-delete" Text="KHÔNG" runat="server" OnClick="btnHuyXoa_Click" CausesValidation="false" />
                    </div>
                </div>
            </div>
            <div id="fade" class="black_overlay" runat="server" style="z-index: 1000">
                <div id="fade2" class="black_overlay">
                    <div class="ui-widget-overlay" style="width: 1280px; height: 899px; z-index: 1001;"></div>
                </div>
                <div class="dashboard">
                    <div class="ico_list">
                        <label class="h2">
                            Danh mục các cấp
                    <asp:LinkButton ID="LinkButton3" Text="Thêm mới" runat="server" OnClick="btnThem_Click" Style="float: right;" CausesValidation="false">
                    <img src="images/add.jpeg" alt=""/>
                    <span style="font-size: 13px;cursor: pointer;">Thêm cấp</span>
                    </asp:LinkButton>
                        </label>

                    </div>
                    <div class="content-body">
                        <div class="pagination" style="float: right; margin-bottom: 10px">
                            <asp:TextBox name="txtSearch" ID="txtSearch" Width="248" runat="server" />
                            <asp:Button ID="btnTimKiem" class="save" Text="Tìm kiếm" runat="server" CausesValidation="false" OnClick="btnTimKiem_Click" />
                        </div>
                        <!-- message area -->
                        <div class="confirmDiv" id="thongbaoSucces_div" style="padding: 1px; background: #50679E">
                            <label id="lblHeaderSuccess" style="color: White; background: #50679E; height: 10px">
                                Thông báo
                            </label>
                            <div style="border: 1px solid #cecece; height: 80px; background: white; margin-left: 4px; margin-bottom: 4px; margin-right: 4px;">
                                <img alt="" src="../images/messagebox_info.png" style="width: 30px; margin-left: 7px; margin-top: 14px;" /><asp:Label ID="lblContentSuccess" Style="margin-top: -33px; margin-left: 35px"
                                    runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="confirmDiv" id="thongbaoError_div" style="padding: 1px; background: #50679E">
                            <label id="lblHeaderErr" style="color: Black; background: #50679E; height: 10px">
                                Lỗi
                            </label>
                            <div style="border: 1px solid #cecece; height: 80px; background: white; margin-left: 4px; margin-bottom: 4px; margin-right: 4px;">
                                <img alt="" src="../images/Error.png" style="width: 26px; margin-left: 7px; margin-top: 14px;" /><asp:Label ID="lblContentErr" Style="margin-top: -33px; margin-left: 35px" runat="server"></asp:Label>
                            </div>
                        </div>
                        <table id="table" class="table" style="margin-top: 20px">
                            <thead>
                                <te>
                        <th style="width:50px">
                            Mã
                        </th>
                        <th>
                            Tên cấp
                        </th>
                        <th style="width:100px">
                            Cấp quản lý
                        </th>
                        <th style="width:100px">
                            Thao tác
                        </th>
                    </te>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="rptCap" runat="server">
                                    <ItemTemplate>
                                        <tr class="<%# Container.ItemIndex % 2 == 0 ? "even" : "odd" %>">
                                            <td>
                                                <asp:Label ID="lblCapID" runat="server" Text='<%# Eval("CapID") %>'></asp:Label>
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:Label ID="lblTenCap" runat="server" Text='<%# Eval("TenCap") %>'></asp:Label>
                                            </td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("CapQuanLy") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="LinkButton1" Text="text" runat="server" Style="color: White;" OnClick="btnSua_Click" CommandArgument='<%# Eval("CapID") %>' CausesValidation="false">
                    <img src="images/edit.png" style="cursor:pointer;border:0;" alt=""/>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="LinkButton2" Text="text" runat="server" Style="color: White;" OnClick="btnXoa_Click" OnClientClick="showConfirm();" CommandArgument='<%# Eval("CapID") %>' CausesValidation="false">
                    <img src="images/cancel.png" style="cursor:pointer;border:0;" alt=""/>
                                                </asp:LinkButton>
                                            </td>
                                        </tr>
                                    </ItemTemplate>

                                </asp:Repeater>
                            </tbody>
                        </table>
                        <div class="pagination" style="margin-top: 15px">
                            <asp:PlaceHolder ID="plhPaging" runat="server"></asp:PlaceHolder>
                        </div>
                    </div>
                </div>
                <!-- end #dashboard -->
            </div>
            <%--<div id="sidebar" class="right">
        <h2 class="ico_sys">
            Danh mục</h2>
        <ul id="menu">
            <li><a href="./macaps" class="icon_posts">Danh mục các Cấp</a> </li>
        </ul>
    </div>--%>
            <!-- end #sidebar -->
    </form>
    <script type="text/javascript">
        function hideAdd() {
            $("#MainContent_light").hide();
            $("#MainContent_fade").hide();
        }
        function hideDelete() {
            $("#MainContent_popXoa").hide();
            $("#MainContent_fade").hide();
        }

        function hidethongBaoError() {
            if ($("#thongbaoError_div").is(":visible")) {
                setTimeout(function () {
                    $("#thongbaoError_div").hide();
                    $("#fade2").hide();
                }, 3000);
            }
        }
        function showthongBaoError() {
            $("#thongbaoError_div").show();
            $("#fade2").show();
        }
        function hidethongBaoSuccess() {
            if ($("#thongbaoSucces_div").is(":visible")) {
                setTimeout(function () {
                    $("#thongbaoSucces_div").hide();
                    $("#fade2").hide();
                }, 3000);
            }
        }
        function showthongBaoSuccess() {
            $("#thongbaoSucces_div").show();
            $("#fade2").show();
        }
        $(document).ready(function () {
            setInterval(hidethongBaoError, 500);
            setInterval(hidethongBaoSuccess, 500);

        });
    </script>
</asp:Content>
