<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ChucVuManage.aspx.cs" Inherits="Com.Gosol.CMS.Web.ChucVuManage" %>

    <asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link id="Link1" rel="stylesheet" type="text/css" media="all" href="styles/trinm.css"
        runat="server" />   

    <div id="main_panel_container">
        <div id="light" class="ui-dialog ui-widget ui-widget-content ui-corner-all ui-draggable popup"
            tabindex="-1" role="dialog" aria-labelledby="ui-dialog-title-macapForm" runat="server" style="z-index:1002; top:150px; left: 50%; margin-left:-200px; width:400px">
            <div class="ui-dialog-titlebar ui-widget-header ui-corner-all ui-helper-clearfix">
                <span class="ui-dialog-title" id="ui-dialog-title-macapForm">
                    <img src="images/edit-add.png" style="float: left;">
                    <font face="arial">&nbsp; Thêm mới/ Sửa thông tin chức Vụ</font> </span><a href="#"
                        class="ui-dialog-titlebar-close ui-corner-all close_link" role="button" onclick="hidePop(); return false;">
                        <span class="ui-icon ui-icon-closethick">close</span> </a>
            </div>
            <div id="macapForm" class="ui-dialog-content ui-widget-content" scrolltop="0" scrollleft="0"
                runat="server">
                <ul>
                    <fieldset>
                        <table>
                            <tbody>
                                <tr id="ma" runat="server">
                                    <th class="field_label">                                        
                                    </th>
                                    <td colspan="2">
                                        <li id="wwgrp_ChucVuID" class="wwgrp">
                                            <div id="wwctrl_ChucVuID" class="wwctrl">
                                                <asp:TextBox ID="ChucVuID" runat="server" Width="248" Enabled="false" Visible="false" />
                                            </div>
                                        </li>
                                    </td>
                                </tr>

                                <tr>
                                    <th class="field_label right-align" style="width:100px">
                                        Tên chức vụ <span style="color:Red">*</span>
                                    </th>
                                    <td colspan="2">
                                        <li id="wwgrp_name" class="wwgrp">
                                            <div id="wwctrl_name" class="wwctrl">
                                                <asp:TextBox ID="txtTenChucVu" placeholder="Không quá 50 ký tự"  MaxLength="255" name="maCap.tenCap" Width="248" runat="server" />      
                                            </div>
                                        </li>
                                    </td>
                                </tr>
                                <tr>
                        <td>
                        </td>
                        <td colspan="2">
                            <asp:RequiredFieldValidator ID="txtTenChucVu_Required" runat="server" ErrorMessage="Tên chức vụ không được để trống!"
                                ControlToValidate="txtTenChucVu" ForeColor="Red" Font-Bold="true" SetFocusOnError="true"
                                Display="Dynamic" ValidationGroup="ChucVu"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="txtTenChucVu_Regular" runat="server" ErrorMessage="Tên chức vụ không chứa số và ký tự đặc biệt và không quá 50 ký tự!"
                                ControlToValidate="txtTenChucVu" ValidationExpression="^([^0-9!@#$%\^\&\*]*){0,50}$"
                                ForeColor="Red" Font-Bold="true" SetFocusOnError="true" ValidationGroup="ChucVu"
                                Display="Dynamic"> </asp:RegularExpressionValidator>
                        </td>
                    </tr>
                                <tr>
                                    <td colspan="2">
                                        &nbsp;
                                    </td>
                                </tr>
                                <!--  button fields -->
                                <tr>
                                    <td></td>
                                    <td colspan="2" align="left">
                                        <asp:Button ID="btnLuu" class="save validate_button" Text="Lưu lại" runat="server" OnClick="btnLuu_Click" ValidationGroup="ChucVu"
                                            CausesValidation="true" />
                                        <asp:Button ID="btnHuy" class="save btn-cancel" Text="Hủy bỏ" OnClick="CloseAddFormClick" runat="server" 
                                            CausesValidation="false" /><%--OnClientClick="hidePop(); return false;"--%>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </fieldset>
                </ul>
            </div>
        </div>
        <div id="popXoa" class="ui-dialog ui-widget ui-widget-content ui-corner-all ui-draggable popup"
            tabindex="-1" role="dialog" aria-labelledby="ui-dialog-title-delete-form" runat="server" style="z-index:1002;  top:150px; left: 50%; margin-left:-175px; width:350px ">
            <div class="ui-dialog-titlebar ui-widget-header ui-corner-all ui-helper-clearfix">
                <span class="ui-dialog-title" id="ui-dialog-title-delete-form">Thao tác xóa dữ liệu.</span>
                <a href="#" class="ui-dialog-titlebar-close ui-corner-all close_link" role="button" onclick="hidePop(); return false;">
                    <span class="ui-icon ui-icon-closethick">close</span> </a>
            </div>
            <div id="delete-form" class="ui-dialog-content ui-widget-content" scrolltop="0" scrollleft="0">
                <p>
                    <span id="ui-icon ui-icon-alert" class="ui-icon ui-icon-alert" style="float: left;
                        margin: 0 7px 20px 0;"></span><span id="delete-form-message">Bạn có chắc muốn xóa chức
                            vụ: 
                            <asp:Label ID="ten_xoa" Text="" runat="server" /> ?
                            <asp:Label ID="id_xoa" Text="text" runat="server" Style="display: none;" />
                        </span>
                </p>
            </div>
            <div class="ui-dialog-buttonpane ui-widget-content ui-helper-clearfix">
                <div class="ui-dialog-buttonset">
                    <asp:Button ID="Button1" class="deleteBtn" Text="CÓ" runat="server" OnClick="btnXacNhan_Click"
                        CausesValidation="false" />
                    <asp:Button ID="Button2" class="save deldete-button btn-cancel" Text="KHÔNG" runat="server" OnClick="btnKhongXoa_Click"  
                        CausesValidation="false" />
                </div>
            </div>
        </div>
        <div id="fade" class="black_overlay" runat="server">
            <div class="ui-widget-overlay" style="width: 1280px; height: 899px; z-index: 1001;">
            </div>
        </div>
        <div id="fade2" class="black_overlay">
         
        </div>
        <div class="dashboard">
            <div class="ico_list">
                <label class="h2">
                    Danh mục chức vụ
                    <asp:LinkButton ID="btnThem" Text="Thêm mới" runat="server" OnClick="btnThem_Click"
                        CssClass="button-add" CausesValidation="false">
                    <img src="images/add.jpeg" alt=""/>
                    <span style="font-size: 13px;cursor: pointer;">Thêm chức vụ</span>
                    </asp:LinkButton>
                </label>
            </div>
            <div class="content-body">
                <!-- message area -->
            <table style="width:100%">
                 <tr >
                     <td >
                         <asp:Panel ID="Panel1" runat="server" DefaultButton="btnTimKiem">
                         <div style="float:right">
                         <asp:TextBox name="txtSearch" ID="txtSearch" Width="248" runat="server" />
                         <asp:Button ID="btnTimKiem" class="save" Text="Tìm kiếm" runat="server" CausesValidation="false"
                            OnClick="btnTimKiem_Click"/>
                             </div>
                             </asp:Panel>
                     </td>
                 </tr>
            </table>
            
                  
                    <div class="confirmDiv message" id="thongbaoSucces_div" >
                        <div class="header-message">
                            <label id="lblHeaderSuccess" class="header-message" runat="server">
                                Thông báo
                            </label>
                            <img src="images/close.ico" class="img-close" onclick="HideMessage(this);" />
                        </div>
                        <div class="content-message">
                            <img alt="" src="../images/messagebox_info.png" style="width: 30px; margin-left: 7px; margin-top: 14px;" /><asp:Label ID="lblContentSuccess" CssClass="content-message"
                                runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="confirmDiv message" id="thongbaoError_div" >
                        <div class="header-message">
                             <label id="lblHeaderErr" class="header-message">
                            Lỗi
                        </label>
                            <img src="images/close.ico" class="img-close" onclick="HideMessage(this);" />
                        </div>
                        <div class="content-message">
                            <img alt="" src="../images/Error.png" style="width: 26px; margin-left: 7px; margin-top: 14px;" /><asp:Label ID="lblContentErr" CssClass="content-message" runat="server"></asp:Label>
                        </div>
                    </div>

            <table id="table" class="table" style="margin-top:15px">
                <thead>
                    <tr>
                        <th style="width:50px">
                            STT
                        </th>
                        <th>
                            Tên chức vụ
                        </th>
                        <%--<th>
                            Cấp quản lý
                        </th>--%>
                        <th style="width:70px">
                            Thao tác
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptChucVu" runat="server" OnItemDataBound="rptChucVu_ItemDataBound">
                        <ItemTemplate>
                            <tr class="<%# Container.ItemIndex % 2 == 0 ? "even" : "odd" %>">
                                <td >
                                    <%--<asp:Label ID="lblChucVuID" runat="server" Text='<%# Eval("ChucVuID") %>'></asp:Label>--%>
                                    <%=stt ++ %>
                                </td>
                                <td style="text-align: left;">
                                    <asp:Label ID="lblTenChucVu" runat="server" Text='<%# Eval("TenChucVu") %>'></asp:Label>
                                </td>
                                <%--<td style="text-align: left; width: 200px">
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("CapQuanLy") %>'></asp:Label>
                                </td>--%>
                                <td style="text-align:center">
                                    <asp:LinkButton ID="btnEdit" Text="text" runat="server" Style="color: White;"
                                        OnClick="btnSua_Click" CommandArgument='<%# Eval("ChucVuID") %>' CausesValidation="false">
                    <img src="images/edit.png" style="cursor:pointer;border:0;" alt="" title="Sửa"/>
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnDelete" Text="text" runat="server" Style="color: White;"
                                        OnClick="btnXoa_Click" CommandArgument='<%# Eval("ChucVuID") %>'
                                        CausesValidation="false">
                    <img src="images/delete.png" style="cursor:pointer;border:0;" alt="" title="Xóa"/>
                                    </asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            <div class="pagination" style="margin-top:15px">
                <asp:PlaceHolder ID="plhPaging" runat="server"></asp:PlaceHolder>
            </div>
                </div>
        </div>
        <!-- end #dashboard -->
    </div> 
    <div id="sidebar" class="right">
        <asp:Literal ID="ltrSideMenu" runat="server"></asp:Literal>
    </div>
    <script type="text/javascript">
        function hidePop() {
            $("#MainContent_light").hide();
            $("#MainContent_fade").hide();
            $("#MainContent_popXoa").hide();
            $("#MainContent_fade").hide();
            $("#Form1").validationEngine("hideAll");
        }

       

    </script>
</asp:Content>
