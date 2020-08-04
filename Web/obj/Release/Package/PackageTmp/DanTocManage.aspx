<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DanTocManage.aspx.cs" Inherits="Com.Gosol.CMS.Web.DanTocManage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%-- <link id="Link1" rel="stylesheet" type="text/css" media="all" href="styles/trinm.css"
        runat="server" />--%>

    <script type="text/javascript">

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
        function showThemDanToc() {
            $("#light").modal("show");
        }

        function showthongBaoSuccess() {
            $("#thongBaoSuccess").modal();
            return false;
        }

        function showthongBaoError() {
            $("#thongBaoError").modal();
            return false;
        }


        function btnXoa_Click(button) {
            $("#deleteConfirm").modal("show");
            $("#MainContent_hdfDanTocIDXoa").val($(button).next().val());
            return false;
        }
        function hideDeleteGroupConfirm() {
            $("#deleteConfirm").modal("hide");
        }

        function hidePop() {
            $("#light").modal("hide");
        }

    </script>

    <%--<div id="main_panel_container"  class="left">--%>

    <%--<div id="light" class="ui-dialog ui-widget ui-widget-content ui-corner-all ui-draggable popup"
        tabindex="-1" role="dialog" aria-labelledby="ui-dialog-title-macapForm" runat="server" style="z-index: 1002; top: 200px; left: 50%; margin-left: -225px; width: 450px">
        <div class="ui-dialog-titlebar ui-widget-header ui-corner-all ui-helper-clearfix">
            <span class="ui-dialog-title" id="ui-dialog-title-macapForm">
                <img src="images/edit-add.png" style="float: left;">
                <font face="arial">&nbsp; Thêm mới/ Sửa thông tin dân tộc</font> </span><a href="#"
                    class="ui-dialog-titlebar-close ui-corner-all close_link" role="button" onclick="hidePop(); return false;">
                    <span class="ui-icon ui-icon-closethick">close</span> </a>
        </div>
        <div id="macapForm" class="ui-dialog-content ui-widget-content" scrolltop="0" scrollleft="0"
            runat="server">
            <ul>
                <fieldset>
                    <table>
                        <tbody>
                            <tr>
                                <th class="field_label"></th>
                                <td colspan="2">
                                    <li id="wwgrp_DanTocID" class="wwgrp">
                                        <div id="wwctrl_DanTocID" class="wwctrl">
                                            <asp:TextBox ID="DanTocID" runat="server" Width="248" Enabled="false" Visible="false" />
                                        </div>
                                    </li>
                                </td>
                            </tr>
                            <tr>
                                <th class="field_label" style="width: 100px">Tên dân tộc <span style="color: Red">*</span>
                                </th>
                                <td colspan="2">
                                    <li id="wwgrp_name" class="wwgrp">
                                        <div id="wwctrl_name" class="wwctrl">
                                            <asp:TextBox ID="txtTenDanToc" placeholder="Không quá 20 ký tự" MaxLength="255" name="maCap.tenCap" Width="248" runat="server" />
                                        </div>
                                    </li>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td colspan="2">
                                    <asp:RequiredFieldValidator ID="txtTenDanToc_Required" runat="server" ErrorMessage="Tên dân tộc không được để trống!"
                                        ControlToValidate="txtTenDanToc" ForeColor="Red" Font-Bold="true" SetFocusOnError="true"
                                        Display="Dynamic" ValidationGroup="DanToc"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="txtTenDanToc_Regular" runat="server" ErrorMessage="Tên dân tộc không chứa số và ký tự đặc biệt và không quá 20 ký tự!"
                                        ControlToValidate="txtTenDanToc" ValidationExpression="^([^0-9!@#$%\^\&\*]*){0,20}$"
                                        ForeColor="Red" Font-Bold="true" SetFocusOnError="true" ValidationGroup="DanToc"
                                        Display="Dynamic"> </asp:RegularExpressionValidator>
                                </td>
                            </tr>

                            <tr>
                                <td colspan="2">&nbsp;
                                </td>
                            </tr>
                            <!--  button fields -->
                            <tr>
                                <td></td>
                                <td colspan="2" align="left">
                                    <asp:Button ID="btnLuu" class="save validate_button" Text="Lưu lại" runat="server" OnClick="btnLuu_Click" CausesValidation="true" ValidationGroup="DanToc" />
                                    <asp:Button ID="btnHuy" class=" save btn-cancel" Text="Hủy bỏ" runat="server" OnClientClick="hidePop(); return false;" CausesValidation="false" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </fieldset>
            </ul>
        </div>
    </div>--%>

    <%--<div id="popXoa" class="ui-dialog ui-widget ui-widget-content ui-corner-all ui-draggable popup"
        tabindex="-1" role="dialog" aria-labelledby="ui-dialog-title-delete-form" runat="server" style="z-index: 1002; top: 200px; left: 50%; margin-left: -175px; width: 350px">
        <div class="ui-dialog-titlebar ui-widget-header ui-corner-all ui-helper-clearfix">
            <span class="ui-dialog-title" id="ui-dialog-title-delete-form">Thao tác xóa dữ liệu.</span>
            <a href="#" class="ui-dialog-titlebar-close ui-corner-all close_link" role="button" onclick="hidePop(); return false;">
                <span class="ui-icon ui-icon-closethick">close</span> </a>
        </div>
        <div id="delete-form" class="ui-dialog-content ui-widget-content" scrolltop="0" scrollleft="0">
            <p>
                <span id="ui-icon ui-icon-alert" class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span><span id="delete-form-message">Bạn có chắc muốn xóa dân tộc:
                            <asp:Label ID="ten_xoa" Text="" runat="server" />
                    ?
                            <asp:Label ID="id_xoa" Text="text" runat="server" Style="display: none;" />
                </span>
            </p>
        </div>
        <div class="ui-dialog-buttonpane ui-widget-content ui-helper-clearfix">
            <div class="ui-dialog-buttonset">
                <asp:Button ID="Button1" class="save validate_button deleteBtn" Text="CÓ" runat="server" OnClick="btnXacNhan_Click"
                    CausesValidation="false" />
                <asp:Button ID="Button2" class="save" Text="KHÔNG" runat="server" OnClientClick="hidePop(); return false;" CssClass="save btn-cancel deldete-button" CausesValidation="false" />
            </div>
        </div>
    </div>--%>
    <asp:HiddenField runat="server" ID="hdfDanTocIDXoa" />
    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="deleteConfirm" class="modal fade">
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
                    <span id="delete-form-message">Bạn có chắc chắn muốn xóa dân tộc?</span>
                </div>
                <div class="modal-footer" style="text-align: center">
                    <asp:Button runat="server" CssClass="btn btn-danger btn-sm" Text="CÓ" OnClick="btnXacNhan_Click" ID="btnDeleteGroup" OnClientClick="hideDeleteGroupConfirm();"></asp:Button>
                    <button type="button" class="btn btn-primary btn-sm" role="button" aria-disabled="false" onclick="hideDeleteGroupConfirm();">
                        <span class="ui-button-text">KHÔNG</span>
                    </button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <div id="fade" class="black_overlay" runat="server">
        <div class="ui-widget-overlay" style="width: 1280px; height: 899px; z-index: 1001;">
        </div>
    </div>

    <div id="fade2" class="black_overlay">
        <%--<div class="ui-widget-overlay" style="width: 1280px; height: 899px; z-index: 1001;">
            </div>--%>
    </div>


    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="light" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <img style="float: left;" src="/images/edit-add.png">
                    <h4 class="modal-title" style="margin-left: 30px;">Thêm mới/ Sửa thông tin dân tộc</h4>
                </div>
                <div class="modal-body">
                    <asp:TextBox ID="DanTocID" runat="server" Width="248" Enabled="false" Visible="false" />
                    <div class="form-horizontal" role="form">
                        <div class="form-group">
                            <div class="col-lg-3" style="text-align: right; padding-right: 10px; line-height: 35px;">
                                Tên dân tộc :
                            </div>
                            <div class="col-lg-9">
                                <asp:TextBox ID="txtTenDanToc" CssClass="form-control" placeholder="Không quá 20 ký tự" MaxLength="255" name="maCap.tenCap" Width="248" runat="server" />
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-lg-3">
                            </div>
                            <div class="col-lg-9">
                                <asp:RequiredFieldValidator ID="txtTenDanToc_Required" runat="server" ErrorMessage="Tên dân tộc không được để trống!"
                                    ControlToValidate="txtTenDanToc" ForeColor="Red" Font-Bold="true" SetFocusOnError="true"
                                    Display="Dynamic" ValidationGroup="DanToc"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="txtTenDanToc_Regular" runat="server" ErrorMessage="Tên dân tộc không chứa số và ký tự đặc biệt và không quá 20 ký tự!"
                                    ControlToValidate="txtTenDanToc" ValidationExpression="^([^0-9!@#$%\^\&\*]*){0,20}$"
                                    ForeColor="Red" Font-Bold="true" SetFocusOnError="true" ValidationGroup="DanToc"
                                    Display="Dynamic"> </asp:RegularExpressionValidator>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer" style="text-align: center">
                    <asp:Button ID="btnLuu" CssClass="btn btn-primary" Text="Lưu lại" runat="server" OnClick="btnLuu_Click" CausesValidation="true" ValidationGroup="DanToc" />
                    <asp:Button ID="btnHuy" CssClass="btn btn-danger" Text="Hủy bỏ" runat="server" OnClientClick="hidePop(); return false;" CausesValidation="false" />
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <div class="content-header">
        <h1>Danh mục dân tộc
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="#">Danh mục</a></li>
            <li class="active">Danh mục dân tộc</li>
        </ol>
    </div>

    <div class="content">
        <div class="row">
            <div class="col-xs-12">
                <div class="box">
                    <div class="box-header">
                        <asp:LinkButton ID="btnThem" Text="Thêm mới" runat="server" OnClick="btnThem_Click"
                            CssClass="btn btn-primary" CausesValidation="false">
                            <span class="glyphicon glyphicon glyphicon-plus-sign">Thêm dân tộc</span>                           

                        </asp:LinkButton>
                    </div>
                    <div class="box-body table-responsive">
                        <div class="col-lg-12" style="margin-bottom:15px;">
                            <asp:Panel ID="Panel1" runat="server" DefaultButton="btnTimKiem">
                                <asp:Button ID="btnTimKiem" class="btn btn-default sm" Text="Tìm kiếm" runat="server" CausesValidation="false"
                                    OnClick="btnTimKiem_Click" Style="float: right;" />
                                <asp:TextBox name="txtSearch" ID="txtSearch" runat="server" CssClass="form-control" Style="float: right; width: 30%; margin-right: 10px;" />
                            </asp:Panel>
                        </div>
                        <%--<div class="confirmDiv message" id="thongbaoSucces_div">
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
                        <div class="confirmDiv message" id="thongbaoError_div">
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

                        <%--<div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="thongBaoSuccess" class="modal fade">
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
                        </div>--%>

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

                        <%--<div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="thongBaoError" class="modal fade">
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
                        </div>--%>
                        <asp:Label ID="lblMsg" ForeColor="#008d4c" Text="" Visible="false" runat="server" CssClass="" />
                        <table id="table" class="table table-bordered table-hover" style="margin-top: 15px">
                            <thead>
                                <tr>
                                    <th style="width: 50px;">STT
                                    </th>
                                    <th style="">Tên dân tộc
                                    </th>

                                    <th style="width: 70px">Thao tác
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="rptDanToc" runat="server" OnItemDataBound="rptDanToc_ItemDataBound">
                                    <ItemTemplate>
                                        <tr class="<%# Container.ItemIndex % 2 == 0 ? "even" : "odd" %>">
                                            <td>
                                                <%=stt ++ %>
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:Label ID="lblDanToc" runat="server" Text='<%# Eval("TenDanToc") %>'></asp:Label>
                                            </td>

                                            <td style="text-align: center">
                                                <asp:LinkButton ID="btnEdit" Text="text" runat="server" Style="color: White;"
                                                    OnClick="btnSua_Click" CommandArgument='<%# Eval("DanTocID") %>' CausesValidation="false">
                    <img src="images/edit.png" style="cursor:pointer;border:0;" alt=""/>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="btnDelete" Text="text" runat="server" Style="color: White;"
                                                    OnClientClick="btnXoa_Click(this); return false;" CommandArgument='<%# Eval("DanTocID") %>'
                                                    CausesValidation="false">
                    <img src="images/delete.png" style="cursor:pointer;border:0;" alt=""/>
                                                </asp:LinkButton>
                                                <asp:HiddenField runat="server" ID="hdfDanTocID" Value='<%# Eval("DanTocID") %>' />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                        <div class="paginations">
                            <asp:PlaceHolder ID="plhPaging" runat="server"></asp:PlaceHolder>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <%--<div class="dashboard">
        <div class="ico_list">
            <label class="h2">
                Danh mục dân tộc
                    <asp:LinkButton ID="btnThem" Text="Thêm mới" runat="server" OnClick="btnThem_Click"
                        CssClass="button-add" CausesValidation="false">
                    <img src="images/add.jpeg" alt=""/>
                    <span style="font-size: 13px;cursor: pointer;">Thêm dân tộc</span>
                    </asp:LinkButton>
            </label>
        </div>
        <div class="content-body">
            <!-- message area -->
            <table style="width: 100%">
                <tr>
                    <td>
                        <asp:Panel ID="Panel1" runat="server" DefaultButton="btnTimKiem">
                            <div style="float: right">
                                <asp:TextBox name="txtSearch" ID="txtSearch" Width="248" runat="server" />
                                <asp:Button ID="btnTimKiem" class="save" Text="Tìm kiếm" runat="server" CausesValidation="false"
                                    OnClick="btnTimKiem_Click" />
                            </div>
                        </asp:Panel>
                    </td>
                </tr>
            </table>

            <div class="confirmDiv message" id="thongbaoSucces_div">
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
            <div class="confirmDiv message" id="thongbaoError_div">
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
            <table id="table" class="table" style="margin-top: 15px">
                <thead>
                    <tr>
                        <th style="width: 50px;">STT
                        </th>
                        <th style="">Tên dân tộc
                        </th>

                        <th style="width: 70px">Thao tác
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptDanToc" runat="server" OnItemDataBound="rptDanToc_ItemDataBound">
                        <ItemTemplate>
                            <tr class="<%# Container.ItemIndex % 2 == 0 ? "even" : "odd" %>">
                                <td>
                                    <%=stt ++ %>
                                </td>
                                <td style="text-align: left;">
                                    <asp:Label ID="lblDanToc" runat="server" Text='<%# Eval("TenDanToc") %>'></asp:Label>
                                </td>

                                <td style="text-align: center">
                                    <asp:LinkButton ID="btnEdit" Text="text" runat="server" Style="color: White;"
                                        OnClick="btnSua_Click" CommandArgument='<%# Eval("DanTocID") %>' CausesValidation="false">
                    <img src="images/edit.png" style="cursor:pointer;border:0;" alt=""/>
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnDelete" Text="text" runat="server" Style="color: White;"
                                        OnClick="btnXoa_Click" CommandArgument='<%# Eval("DanTocID") %>'
                                        CausesValidation="false">
                    <img src="images/delete.png" style="cursor:pointer;border:0;" alt=""/>
                                    </asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            <div class="pagination">
                <asp:PlaceHolder ID="plhPaging" runat="server"></asp:PlaceHolder>
            </div>
        </div>
    </div>--%>
    <!-- end #dashboard -->

    <%--<div id="sidebar" class="right" >
        <asp:Literal ID="ltrSideMenu" runat="server"></asp:Literal>
    </div>--%>
    <script type="text/javascript">
        function hidePop() {
            $("#light").modal("hide");
            $("#MainContent_fade").hide();
            $("#fade2").modal("hide");
            //$("#MainContent_messageDiv").hide();
            $("#Form1").validationEngine("hideAll");
            //$("#MainContent_sidebar").hide();
        }


        //function hideMessage() {

        //    if (document.getElementById("MainContent_lblmessageError").innerHTML != "") {
        //        setTimeout(function () {
        //            document.getElementById("MainContent_lblmessageError").innerHTML = "";
        //        }, 1500);
        //        if (document.getElementById("MainContent_lblmessageSucsses").innerHTML != "") {
        //            setTimeout(function () {
        //                document.getElementById("MainContent_lblmessageSucsses").innerHTML = "";
        //            }, 1500);
        //            //}
        //        }
        //        $(document).ready(function () {
        //            setInterval(hideMessage, 1500);
        //        });
        //    }
        //}
    </script>
    <%--<style>
        .glyphicon {
            position: relative;
            top: 1px;
            display: inline-block;
            font-family: 'Glyphicons Halflings';
            font-style: normal;
            font-weight: 400;
             line-height: 2; 
            -webkit-font-smoothing: antialiased;
        }
    </style>--%>
</asp:Content>
