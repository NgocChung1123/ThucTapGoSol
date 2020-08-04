<%@ Page Title="Danh mục Loại Khiếu Tố" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="LoaiKhieuTo.aspx.cs" Inherits="Com.Gosol.CMS.Web.LoaiKhieuTo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Load for DiaDanh -->
    <!-- edit box styles -->
    <!-- styles to make the tree line bigger and wrappable -->
    <!-- styles to make the tree line bigger and wrappable -->
    <style type="text/css">
	    #treeview li { min-height:25px; line-height:25px; }
	    #treeview a { max-width: 96% !important; white-space:normal !important; height: auto; padding:1px 2px; line-height:23px;}
		#treeview a ins { height:23px;}		
		#treeview li > ins { vertical-align:top; }
		#treeview .jstree-hovered, #treeview .jstree-clicked { border:0; }
    </style>

    <!-- jstree included javascript files -->
    <script type="text/javascript" src="scripts/jquery.jstree.js"></script>
    <script type="text/javascript" language="Javascript" src="scripts/ttcp_jstree_loaidon.js"></script>
    <!-- autocomplete -->
    <link rel="stylesheet" type="text/css" media="all" href="styles/jquery.autocomplete.ajax.styles.css" />
    <script type="text/javascript" src="scripts/jquery.autocomplete.ajax.js"></script>
    <%--<link rel="stylesheet" type="text/css" media="all" href="styles/ttcp_extra.css" />--%>

    <script type="text/javascript" language="Javascript">
        $(document).ready(function () {

            // init tree view
            init_tree({
                tree_id: "#treeview",
                level_creatable: 3,
                theme: "loaidon",
                theme_path: "scripts/jstree_themes/themes/",
                show_edit_box: false,
                search_url: "LoaiKhieuToSearch.ashx",
                list_url: "LoaiKhieuToGetData.ashx",
                add_url: "LoaiKhieuToAdd.ashx",
                edit_url: "LoaiKhieuToEdit.ashx",
                move_url: "LoaiKhieuToMove.ashx",
                delete_url: "LoaiKhieuToDelete.ashx",
                delete_deny : <%= GetDeleteDeny() %>,
                create_deny: <%= GetCreateDeny() %>,
                edit_deny: <%= GetEditDeny() %>
            });

            // assign min height to the dashboard on load
            var sidebarHeight = (document.getElementById('sidebar').offsetHeight - 20) + "px";
            $("div.dashboard").css("min-height", sidebarHeight);
        });

        
        
	</script>


    <!-- load delete confirm -->
    <div id="delete-form" title="Thao tác xóa dữ liệu." style="display: none;">
        <p>
            <span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>
            <span id="delete-form-message">
                <fmt:message key='delete_form.msg' />
            </span>
        </p>
    </div>
    <div id="message-form" title="Thông báo." style="display: none;">
        <p>
            <span class="ui-icon ui-icon-circle-check" style="float: left; margin: 0 7px 20px 0;">
            </span><span id="message-form-message">
                <fmt:message key='delete_form.msg' />
            </span>
        </p>
    </div>
    <!-- end load delete confirm  -->

    <div id="main_panel_container">
        <!-- load treeView -->
        <div class="dashboard" style="min-height: 603px;">
            <!--  add mew button -->
            <div class="ico_list">
                <label class="h2">
                    Danh mục loại khiếu tố <a id="themLoaiKT" onclick="addNewRootNode()" href="javascript:void(0)"
                        style="font-size: 12px; float: right" runat="server">
                        <img src="images/add.jpeg" style="vertical-align: middle">
                        <span style="vertical-align: middle">Thêm loại khiếu tố gốc</span> </a>
                </label>
            </div>
            <div class="content-body">

    
                    <div class="confirmDiv message" id="thongbaoSucces_div" >
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
            <!--  search button -->
            <div id="search_div" class="pagination" style="float:right">
                <asp:Panel runat="server" DefaultButton="btnSearch">
                <input id="search_text" name="search_text" type="text" value="" size="40" onfocus="this.select()"
                    autocomplete="off">
                <asp:Button ID="btnSearch" runat="server" OnClientClick="search_tree(); return false;" Text="Tìm kiếm" CssClass="save"/>              
                </asp:Panel>
            </div>
                <br /><br /><br />
            <!-- message area -->
            <%--<div style=" text-align: center; background-color: #fff; padding: 10px;">
            </div>--%>
            <!-- tree area -->
            <div id="treeview" class="jstree jstree-0 jstree-focused jstree-diadanh" style=" background:#F8F8F8 !important">
            </div>
            <!-- clear bottom area -->
            <div style="margin-bottom: 10px;">
                &nbsp;</div>
                </div>
        </div>
    </div>
    <div id="sidebar" class="right">
        <asp:Literal ID="ltrSideMenu" runat="server"></asp:Literal>
    </div>
     <div id="fade2" class="black_overlay"></div>
</asp:Content>
