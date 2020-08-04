<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TroGiupManage.aspx.cs" Inherits="Com.Gosol.CMS.Web.TroGiupManage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
    </asp:ScriptManager>
    <script type="text/javascript" language="Javascript" src="/scripts/new_hosodonthu.js"></script>
    <%--<script src="Scripts/jquery.hotkeys.js" type="text/javascript"></script>--%>
    <!-- validation -->
    <%--<script src="Scripts/validation/validation.js" type="text/javascript"></script>--%>
    <%--<script src="Scripts/jquery.validationEngine-vi.js" type="text/javascript"></script>--%>
    <%--<script src="Scripts/wow/wow.js" type="text/javascript"></script>--%>
    <%--<script src="Scripts/wow/wow.min.js" type="text/javascript"></script>--%>
    <%--<script src='http://forumr.googlecode.com/files/jquery.nicescroll.js' type='text/javascript'></script>--%>
    <%--<link href="Styles/custom_new.css" rel="stylesheet" type="text/css" />--%>
    <%--<link href="/styles/uploadfile/uploadify.css" rel="stylesheet" type="text/css" />--%>
    <%--<script src="/scripts/uploadfile/jquery.uploadify.js" type="text/javascript"></script>--%>
    <%--<script type="text/javascript" language="Javascript" src="/scripts/new_hosodonthu.js"></script>--%>
    <script type="text/javascript">

        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {
            var config = {
                '.chon-nguon-don-den': { "disable_search": true },
                '.chon-co-quan': {},
                '.chon-so-nguoi-dai-dien': { "disable_search": true },
                '.chon-gioi-tinh': { "disable_search": true },
                '.chon-dan-toc': {},
                '.chon-quoc-tich': {},
                '.chon-nghe-nghiep': {},
                '.chon-tinh-thanh': {},
                '.chon-quan-huyen': {},
                '.chon-xa-phuong': {},
                '.chon-nguoi-ky': {},
                '.chon-nguoi-xu-ly': {},
                '.chon-huong-giai-quyet': {},
                '.chon-loai-don': {},
                '.chon-loai-kntc': {},
                '.chon-chi-tiet': {},
                '.chon-chuc-vu': {},
                '.loai-ho-so': { "disable_search": true },
                '.loai-vu-viec': { "disable_search": true },
                '.chon-don-vi': { "disable_search": true },
                '.chon-don-vi-tiep-nhan-ho-so': {}

            }
            //for (var selector in config) {
            //    $(selector).chosen(config[selector]);
            //}

            //an form uploadFileScan
            var hideFileScanDiv = $(".hideFileScanDiv").val();
            //alert(hideFileScanDiv);
            if (hideFileScanDiv) {
                $('#fileScanDiv').removeClass("display");
                $('#fileScanDiv').addClass("hidden");
            }

            var confirmDelete = $(".txt_hideFileDeleteConfirm").val();
            if (confirmDelete)
                $('#dialogMsg').hide();
        });
        function hideFileScan() {
            if ($(".txt_fileurl").val() != "" && ($('.txt_fileid').val() == "" || $('.txt_fileid').val() != "" && $('.txt_foruploadfile').val() == 1)) {

                //$("#ajax_fade").modal("show");
                $("#hide_huyfileid").modal("show");
                $("#fileScanDiv").removeClass("display");
                $('#fileScanDiv').addClass("hidden");
            }

            $('#fileScanDiv').removeClass("display");
            //$('#fileScanDiv').hide();
            $('#fileScanDiv').addClass("hidden");
            $("#MainContent_fade").modal("hide");
        }
        function ShowAddSuccess(str) {
            $("#MainContent_lblContentSuccess").html(str);
        }
        function trueHuyFileConfirm() {
            $.ajax({
                type: "POST",
                url: "/DeleteFileUploaded.ashx"
            }).done(function (msg) {
                $("#ajax_fade").hide();
                $("#hide_huyfileid").hide();
                $('#fileScanDiv').removeClass("display");
                $('#fileScanDiv').addClass("hidden");
                //$(".uploadify-queue-item").remove();
            });
        }

        function falseHuyFileConfirm() {
            $("#hide_huyfileid").hide();
            $('#fileScanDiv').removeClass("hidden");
            $('#fileScanDiv').addClass("display");
        }

        function showDialogDelete(id, upId) {
            $("#dialogMsg").show();
            $("#MainContent_deleteFileHoSoID").val(id);
            $("#MainContent_nguoiUpFileID").val(upId);
            $('#fade2').show();
        }

        function hideDiaLogDelete(id) {
            if (id == 0) {
                $("#MainContent_deleteFileHoSoID").val("");
                $("#MainContent_nguoiUpFileID").val("");
            }
            $("#dialogMsg").hide();
            $('#MainContent_fade').hide();
        }

        function showDeleteFileScanConfirm(id) {
            if (confirm("Dữ liệu bị xóa sẽ không thể phục hồi, bạn có chắc không?"))
                deleteFileHoSo(id);
        }

        function downloadFileScanNow(id) {
            window.location.href = "/DownloadFileTaiLieu.aspx?ma_file=" + id;
        }
    </script>
    <script type="text/javascript">
        // JQUERY ".Class" SELECTOR.
        var remove = 0;
        function removeUnload() {
            remove = 1;
            return remove;
        }
        function checkData() {
            var check = 0;

            $("input[type='text']").each(function () {
                if ($(this).val() != "") check = 1;
            });
            if (remove == 1)
                check = 0;
            return check;
        }

        function scrollTop() {
            $(window).scrollTop(0);
        }

        $(document).ready(function () {
            $('.datepicker-field').datepicker();

            //$("html").niceScroll({
            //    zindex: 1000000,

            //    cursorborderradius: "4px", // Làm cong các góc của scroll bar

            //    cursorcolor: "rgb(148, 144, 138)", // Màu của scroll bar

            //    cursorwidth: "8px", // Kích thước bề ngang của scroll bar

            //    autohidemode: true   //bật chế độ tự ẩn của scroll bar         

            //});

            $(window).scroll(function () {
                if ($(window).scrollTop() == 0) {
                    $('#go_top').stop(false, true).fadeOut(600);
                } else {
                    $('#go_top').stop(false, true).fadeIn(600);
                }
            });

            var show_file_err = $(".show_err_msg").val();

            if (show_file_err) {
                $("#div_err").removeClass();
                $('#fileScanDiv').removeClass();
                $('#fileScanDiv').addClass("display");
            }

            // mask the date input
            //$("input.datepicker").mask("99/99/9999");


            //Ngày scan
            $(".txt_ngayscan").datepicker({
                yearRange: '2000:2015',
                changeMonth: true,
                changeYear: true
            });
            $(".txt_ngayscan").datepicker("option", $.datepicker.regional["vi"]);
            $(".txt_ngayscan").datepicker("option", "dateFormat", "dd/mm/yy");

        });
        function showTiepDanError() {
            $(window).scrollTop(0);
            $("#maTiepDanError").show();
            $("#fade").show();
        }

        function showEditFileScan(id) {

            //an error
            $("#fileScanDiv").show();
            $("#MainContent_fade").show();
            //$(".err_txt_fileurl").removeClass("display");
            //$(".err_txt_fileurl").addClass("hidden");
            //$(".err_tenfile").removeClass("display");
            //$(".err_tenfile").addClass("hidden");

            //$("#div_err").addClass("hidden");
            //$('#fileScanDiv').removeClass();
            //$('#fileScanDiv').addClass("display");
            //$('#chooseFileDiv').removeClass();
            //$('#chooseFileDiv').addClass("hidden");
            //$('#editFileDiv').removeClass();
            //$('#editFileDiv').addClass("display");
            //show for edit
            //$(".NgayU")
            $('.txt_fileurl').val($('#hd_fileurl_' + id).val());
            $('.txt_fileid').val(id);
            $('#hd_file').val($('#hd_fileurl_' + id).val());
            $('.txt_ngayscan').val($('#hd_ngayscan_' + id).val());
            $('.txt_tenfile').val($('#hd_tenfile_' + id).val());
            $('.txt_tomtat').val($('#hd_tomtat_' + id).val());
            $('.txt_foruploadfile').val("");
        }
        function showChooseFile() {
            $('#chooseFileDiv').removeClass();
            $('#chooseFileDiv').addClass("display");
            $('#editFileDiv').removeClass();
            $('#editFileDiv').addClass("hidden");

            $('.txt_foruploadfile').val("1");
        }
        function showAddFileScan() {
            $("#fileScanDiv").modal("show");
            //$("#MainContent_fade").modal("show");

            //$('.txt_ngayscan').val() = new Date();
        }

        function trueHuyFileConfirm() {
            $.ajax({
                type: "POST",
                url: "/DeleteFileUploaded.ashx"
            }).done(function (msg) {
                $("#ajax_fade").hide();
                $("#hide_huyfileid").hide();
                $('#fileScanDiv').removeClass("display");
                $('#fileScanDiv').addClass("hidden");
                //$(".uploadify-queue-item").remove();
            });
        }

        function falseHuyFileConfirm() {
            $("#hide_huyfileid").hide();
            $('#fileScanDiv').removeClass("hidden");
            $('#fileScanDiv').addClass("display");
        }

        function showDialogDelete(id, upId) {
            $("#dialogMsg").show();
            $("#MainContent_deleteFileHoSoID").val(id);
            $("#MainContent_nguoiUpFileID").val(upId);
            $('#fade').show();
        }
        function showDeleteFileScanConfirm(id) {
            if (confirm("Dữ liệu bị xóa sẽ không thể phục hồi, bạn có chắc không?"))
                deleteFileHoSo(id);
        }

    </script>
    <script type="text/javascript">
        //$(function () {

        //    var uploaded = 0;

        //    $(".file_dinhkem").uploadify({
        //        'swf': 'asset/uploadfile/uploadify.swf',
        //        'uploader': 'HoSoDonStep2UploadFiles.ashx',
        //        'cancelImg': 'Styles/uploadfile/cancel.png',
        //        'buttonText': 'Chọn File Upload',
        //        'fileDesc': 'Image Files',
        //        'fileSizeLimit': '10MB',
        //        'uploadLimit': '1',
        //        'fileExt': '*.jpg;*.jpeg;*.gif;*.png;*.doc;*.docx;*.pdf;*.xls;*.xlsx',
        //        'multi': false,
        //        'auto': true,
        //        'checkExisting': true,
        //        'removeCompleted': false,
        //        'onUploadComplete': function (file) {
        //            //alert(file.name);
        //            //alert(response.fileName);
        //            $(".txt_fileurl").val(file.name); // alert("Done");
        //            $(".err_txt_fileurl").hide();
        //            return false;
        //        }
        //    });
        //})

        $(function () {
            $('form').each(function () {
                $(this).find('input').keypress(function (e) {
                    // Enter pressed?
                    if (e.which == 10 || e.which == 13) {
                        if ($("#fileScanDiv").css("display") == "block") {
                            $(".submitSaveFileHoSo").click();
                            //$(".link_taive").click() = false;
                        }
                        else
                            $(".submitStep2").click();
                        //$(this).find('input[type=submit]').click();
                    }
                });

                //$(this).find('input[type=submit]').hide();
            });
        });
    </script>
    <!-- end -->
    <!-- jequery chosen: search & dropdownlist -->
    <%--<link href="Styles/dropdownlist/chosen.min.css" rel="stylesheet" type="text/css" />--%>
    <%--<script type="text/javascript" language="Javascript" src="/scripts/new_hosodonthu.js"></script>--%>
    <%--<link href="Styles/custom_new.css" rel="stylesheet" type="text/css" />--%>
    <%--<script src="Scripts/dropdownlist/chosen-vi.jquery.js" type="text/javascript"></script>--%>
    <script type="text/javascript">
        //an form uploadFileScan
        var hideFileScanDiv = $(".hideFileScanDiv").val();
        //alert(hideFileScanDiv);
        if (hideFileScanDiv) {
            $('#fileScanDiv').removeClass("display");
            $('#fileScanDiv').addClass("hidden");
        }

        var confirmDelete = $(".txt_hideFileDeleteConfirm").val();
        if (confirmDelete)
            $('#dialogMsg').hide();
    </script>
    <!-- /end -->

    <%--<div id="main_panel_container"  class="left">--%>
    <div id="fade2" class="black_overlay">
    </div>
    <div id="fade" class="black_overlay" runat="server">
    </div>

    <div id="light" class="ui-dialog ui-widget ui-widget-content ui-corner-all ui-draggable"
        tabindex="-1" role="dialog" aria-labelledby="ui-dialog-title-macapForm" runat="server" style="z-index: 1002; top: 200px; left: 50%; margin-left: -225px; width: 450px">
        <div class="ui-dialog-titlebar ui-widget-header ui-corner-all ui-helper-clearfix">
            <span class="ui-dialog-title" id="ui-dialog-title-macapForm">
                <img src="images/edit-add.png" style="float: left;">
                <font face="arial">&nbsp; Thêm mới/ Sửa tài liệu hướng dẫn</font> </span><a href="#"
                    class="ui-dialog-titlebar-close ui-corner-all close_link" role="button" onclick="hidePop();">
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
                                            <asp:TextBox ID="txtTenDanToc" CssClass="validate[required]" MaxLength="255" name="maCap.tenCap" Width="248" runat="server" />
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
                                <td></td>
                                <td colspan="2" align="left">
                                    <asp:Button ID="btnLuu" class="save validate_button" Text="Lưu lại" runat="server" OnClick="btnLuu_Click" CausesValidation="true" />
                                    <asp:Button ID="btnHuy" class=" save btn-cancel" Text="Hủy bỏ" runat="server" OnClientClick="hidePop(); return false;" CausesValidation="false" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </fieldset>
            </ul>
        </div>
    </div>

    <div id="popXoa" class="ui-dialog ui-widget ui-widget-content ui-corner-all ui-draggable"
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
    </div>

    <div id="msgFade" class="black_overlay">
    </div>


    <div class="content-header">
        <label class="h2" style="float: left; width: 100%">
            Danh sách tài liệu hướng dẫn
                    <%--<asp:LinkButton ID="btnThem" Text="Thêm mới" runat="server" OnClientClick="showAddFileScan()"
                        CssClass="button-add" CausesValidation="false">
                    <img src="images/add.jpeg" alt=""/>
                    <span style="font-size: 13px;cursor: pointer;">Thêm tài liệu</span>
                    </asp:LinkButton>--%>
            <label style="font-size: 1.2em; padding-left: 0; font-weight: bold; float: right" class="h2">
                <a style="font-size: 12px; cursor: pointer;" onclick="showAddFileScan()" id="btnAddFilescan" class="button-add">
                    <img style="vertical-align: top;" src="/images/add.jpeg" alt="add">
                    <span>Thêm file đính kèm</span> </a>
            </label>
        </label>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="#">Tables</a></li>
            <li class="active">Data tables</li>
        </ol>
    </div>

    <%--<div class="dashboard">
            <div class="ico_list" id="uploadfile">
                <label class="h2" style="float: left; width: 100%">
                    Danh sách tài liệu hướng dẫn
                    <label style="font-size: 1.2em; padding-left: 0; font-weight: bold; float: right" class="h2">
                        <a style="font-size: 12px; cursor: pointer;" onclick="showAddFileScan()" id="btnAddFilescan" class="button-add">
                            <img style="vertical-align: top;" src="/images/add.jpeg" alt="add">
                            <span>Thêm file đính kèm</span> </a>
                    </label>
                </label>
            </div>--%>
    <%--<div id="uploadfile" class="" style="margin-top: 10px" runat="server">
                <label style="font-size: 1.2em; padding-left: 0; font-weight: bold" class="h2">
                    File đính kèm <a style="font-size: 12px; cursor: pointer; float: right" onclick="showAddFileScan()"
                        id="btnAddFilescan">
                        <img style="vertical-align: top;" src="/images/add.jpeg" alt="add">
                        <span>Thêm file đính kèm</span> </a>
                </label>
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
                            <!-- message area -->
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        <div style="float: right">
                                            <asp:TextBox name="txtSearch" ID="txtSearch" Width="248" runat="server" />
                                            <asp:Button ID="btnTimKiem" class="save" Text="Tìm kiếm" runat="server" CausesValidation="false"
                                                OnClick="btnTimKiem_Click" />
                                        </div>
                                    </td>
                                </tr>
                            </table>

                            <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="thongbaoSucces_div" class="modal fade">
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

                            <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="thongbaoError_div" class="modal fade">
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

                            <asp:TextBox ID="txt_hideFileDeleteConfirm" CssClass="txt_hideFileDeleteConfirm hidden"
                                runat="server" />
                            <asp:PlaceHolder ID="plh_fileErr" runat="server" Visible="false">
                                <div style="color: Red; text-align: center; font-weight: bold; margin-top: 10px">
                                    <asp:Label ID="lbl_file_err_msg" runat="server" />
                                </div>
                            </asp:PlaceHolder>
                            <%--<asp:UpdatePanel runat="server" UpdateMode="Conditional">
                                <ContentTemplate>--%>
                                    <asp:TextBox ID="hideFileScanDiv" CssClass="hideFileScanDiv hidden" runat="server" />
                                    <table id="table" class="table table-bordered table-hover" style="margin-top: 15px">
                                        <thead>
                                            <tr>
                                                <th style="width: 50px;">STT
                                                </th>
                                                <th>Ngày up
                                                </th>
                                                <th style="">Tên file
                                                </th>
                                                <th style="">Tóm tắt
                                                </th>
                                                <th style="width: 110px">Thao tác
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="rptFileTaiLieu" runat="server" OnItemDataBound="rptFileTaiLieu_ItemDataBound">
                                                <ItemTemplate>
                                                    <tr class="<%# Container.ItemIndex % 2 == 0 ? "even" : "odd" %>">
                                                        <td>
                                                            <%--<asp:Label ID="lblDanTocID" runat="server" Text='<%# Eval("DanTocID") %>'></asp:Label>--%>
                                                            <%=stt ++ %>
                                                        </td>
                                                        <td style="text-align: center; width: 100px">
                                                            <asp:Label ID="lblNgayUp" runat="server" Text=''></asp:Label>
                                                        </td>
                                                        <td style="text-align: left;">
                                                            <asp:Label ID="lblTenFile" runat="server" Text='<%# Eval("TenFile") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblTomTat" runat="server" Text='<%# Eval("TomTat") %>'></asp:Label>
                                                        </td>
                                                        <%--<td style="text-align: center">
                                        <%--<asp:LinkButton ID="btnEdit" Text="text" runat="server" Style="color: White;"
                                            OnClick="btnSua_Click" CommandArgument='<%# Eval("FileUrl") %>' CausesValidation="false">
                    <img src="~/images/icon_download.bmp" style="cursor:pointer;border:0;" alt=""/>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="btnDelete" Text="text" runat="server" Style="color: White;"
                                            OnClick="btnXoa_Click" CommandArgument='<%# Eval("FileTaiLieuID") %>'
                                            CausesValidation="false">
                    <img src="images/delete.png" style="cursor:pointer;border:0;" alt=""/>
                                        </asp:LinkButton>--%>
                                                        <td style="vertical-align: middle; text-align: center; padding-left: 10px;">
                                                            <asp:HiddenField ID="hdf_nguoiup" runat="server" Value='<%# Eval("NguoiUp") %>' />
                                                            <asp:HiddenField ID="hdf_fileurl" runat="server" Value='<%# Eval("FileUrl") %>' />
                                                            <asp:PlaceHolder ID="plh_ownerFile" runat="server" Visible="true">
                                                                <input type="hidden" id='hd_tenfile_<%# Eval("FileTaiLieuID")%>' value='<%# Eval("TenFile") %>' />
                                                                <input type="hidden" id='hd_ngayscan_<%# Eval("FileTaiLieuID")%>' value='<%# Convert.ToDateTime(Eval("NgayUp")).ToString("dd/MM/yyyy") %>' />
                                                                <input type="hidden" id='hd_tomtat_<%# Eval("FileTaiLieuID")%>' value='<%# Eval("TomTat") %>' />
                                                                <input type="hidden" id='hd_fileurl_<%# Eval("FileTaiLieuID")%>' value='<%# Eval("FileURL") %>' />
                                                                <img class="img_button disabled-group" style="margin-bottom: 5px; cursor: pointer"
                                                                    id="btn_edit" title="Sửa" src="images/edit.png" onclick='showEditFileScan(<%# Eval("FileTaiLieuID") %>)' />
                                                                <img alt="Xoa-file" class="img_button disabled-group" style="margin-bottom: 5px; cursor: pointer"
                                                                    id="Img2" title="Xóa file" src="images/cancel.png" onclick='showDialogDelete(<%# Eval("FileTaiLieuID") %>, <%# Eval("NguoiUp") %>)' />
                                                            </asp:PlaceHolder>
                                                            <img alt="Tai-ve-may" class="img_button" style="margin-bottom: 5px; cursor: pointer"
                                                                id="Img1" title="Tải về máy" src="images/download.png" onclick='downloadFileScanNow(<%# Eval("FileTaiLieuID") %>)' />
                                                        </td>
                                                        <%--</td>--%>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </ContentTemplate>
                                <%--<Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btn_savefile" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="btnDeleteFileHoSo" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="btnTimKiem" />
                                </Triggers>
                            </asp:UpdatePanel>--%>
                            <div class="pagination" style="margin-top: 15px;">
                                <asp:PlaceHolder ID="plhPaging" runat="server"></asp:PlaceHolder>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:TextBox ID="show_err_msg" CssClass="show_err_msg hidden" Visible="true" runat="server" />

    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="fileScanDiv" class="modal fade">
        <div class="modal-dialog  modal-md">
            <div class="modal-content">
                <div class="modal-header" style="background-color: #50679E; color: #fff">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <img style="vertical-align: middle;" src="/images/edit-add.png">&nbsp;&nbsp;Thêm
                                                mới / Sửa File đính kèm </span><%--<a href="#" class="ui-dialog-titlebar-close ui-corner-all"
                                                    role="button" onclick="hideFileScan()"><span class="ui-icon ui-icon-closethick">close</span>
                                                </a>--%>
                </div>
                <div class="modal-body">
                    <asp:Panel runat="server" DefaultButton="btn_savefile" Style="padding: 10px;">
                        <asp:PlaceHolder ID="plh_err_file" runat="server" Visible="false">
                            <div style="color: Red; font-weight: bold; text-align: center" id="div_err">
                                <asp:Label ID="file_err_msg" runat="server" />
                            </div>
                        </asp:PlaceHolder>

                        <div class="form-horizontal" role="form">
                            <div class="form-group">
                                <label class="col-lg-3 col-sm-3 control-label">File scan:</label>
                                <div class="col-lg-9">
                               
                                    <%--<asp:RequiredFieldValidator runat="server" ErrorMessage="Vui lòng chọn file upload!"
                                        Display="Dynamic" ControlToValidate="txt_fileurl" ValidationGroup="file_scan"
                                        CssClass="error err_txt_fileurl" />--%>
                                    <asp:TextBox ID="txt_foruploadfile" runat="server" CssClass="txt_foruploadfile hidden" />
                                    <asp:FileUpload ID="FileUploadControl" runat="server" />

                                    <%--<div id="chooseFileDiv" class="display">
                                        <asp:TextBox ID="txt_foruploadfile" runat="server" CssClass="txt_foruploadfile hidden" />
                                        <asp:TextBox ID="txt_fileurl" runat="server" CssClass="txt_fileurl hidden" />
                                        <asp:FileUpload ID="file_upload"  runat="server" Style="width: 284px !important" />
                                    </div>--%>
                                    <div id="editFileDiv" class="hidden">
                                        <input type="text" id="hd_file" readonly="readonly" disabled="disabled" />
                                        <a style="padding-left: 3px; color: blue;" href="javascript: showChooseFile();" id="showChooseFileA">Sửa</a>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-lg-3 col-sm-3 control-label">Tên file: <span style="color: red;">(*)</span></label>
                                <div class="col-lg-9">
                                    <asp:RequiredFieldValidator ControlToValidate="txt_tenfile" ID="RequiredFieldValidator4"
                                        runat="server" ErrorMessage="Vui lòng nhập tên file!" Display="Dynamic" ValidationGroup="file_scan"
                                        CssClass="error err_tenfile" />
                                    <asp:TextBox ID="txt_tenfile" CssClass="form-control" runat="server" Width="300px" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-lg-3 col-sm-3 control-label">Ngày scan:</label>
                                <div class="col-lg-9">
                                    <asp:TextBox ID="txt_ngayscan" Style="width: 120px;" CssClass="datepicker-field" TabIndex="3" runat="server" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-lg-3 col-sm-3 control-label">Tóm tắt:</label>
                                <div class="col-lg-9">
                                    <asp:TextBox ID="txt_tomtat" CssClass="form-control" runat="server" TextMode="MultiLine" Rows="5" Style="line-height: 22px;" Columns="38" />
                                </div>
                            </div>
                        </div>

                        <div class="modal-footer" style="text-align: center">
                            <asp:TextBox ID="txt_fileid" CssClass="hidden txt_fileid" runat="server" />
                            <asp:Button ID="btn_savefile" runat="server" OnClick="btnSaveFileOnclick" Text="Lưu lại"
                                OnClientClick="hideFileScan();ShowAddSuccess('Cập nhật dữ liệu thành công!')" CssClass="btn btn-cancel" ValidationGroup="file_scan"
                                CausesValidation="true" />
                            <input type="button" value="Hủy" class="btn btn-cancel" onclick="hideFileScan();"
                                tabindex="5">
                        </div>
                        <%--<table>
                                <tbody>
                                    <tr>
                                        <th class="field_label" style="width: 100px;">File scan
                                        </th>
                                        <td>
                                            <asp:RequiredFieldValidator runat="server" ErrorMessage="Vui lòng chọn file upload!"
                                                Display="Dynamic" ControlToValidate="txt_fileurl" ValidationGroup="file_scan"
                                                CssClass="error err_txt_fileurl" />
                                            <div id="chooseFileDiv" class="display">
                                                <asp:TextBox ID="txt_foruploadfile" runat="server" CssClass="txt_foruploadfile hidden" />
                                                <asp:TextBox ID="txt_fileurl" runat="server" CssClass="txt_fileurl hidden" />
                                                <asp:FileUpload ID="file_upload" CssClass="file_dinhkem" runat="server" Style="width: 284px !important" />
                                            </div>
                                            <div id="editFileDiv" class="hidden">
                                                <input type="text" id="hd_file" readonly="readonly" disabled="disabled" />
                                                <a style="padding-left: 3px; color: blue;" href="javascript: showChooseFile();" id="showChooseFileA">Sửa</a>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <tr>
                                            <th class="field_label" ">
                                                Tên file <span style="color: red;">(*)</span>
                                            </th>
                                            <td>
                                                <asp:RequiredFieldValidator ControlToValidate="txt_tenfile" ID="RequiredFieldValidator4"
                                                    runat="server" ErrorMessage="Vui lòng nhập tên file!" Display="Dynamic" ValidationGroup="file_scan"
                                                    CssClass="error err_tenfile" />
                                                <asp:TextBox ID="txt_tenfile" CssClass="txt_tenfile" runat="server" Width="300px" />
                                            </td>
                                        </tr>
                                        <th class="field_label">Ngày scan
                                        </th>
                                        <td>
                                            <asp:TextBox ID="txt_ngayscan" class="txt_ngayscan" runat="server" Style="width: 100px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <th class="field_label ">Tóm tắt
                                        </th>
                                        <td>
                                            <asp:TextBox ID="txt_tomtat" CssClass="txt_tomtat" runat="server" TextMode="MultiLine" Rows="5" Style="line-height: 22px" Columns="38" />
                                        </td>
                                    </tr>
                                    <!-- nhanVien -->
                                    <!--  button fields -->
                                    <tr style="height: 50px;">
                                        <td>
                                            <asp:TextBox ID="txt_fileid" CssClass="hidden txt_fileid" runat="server" />
                                        </td>
                                        <td style="padding-left: 65px;">
                                            <asp:Button ID="btn_savefile" runat="server" OnClick="btnSaveFileOnclick" Text="Lưu lại"
                                                OnClientClick="hideFileScan();ShowAddSuccess('Cập nhật dữ liệu thành công!')" CssClass="btn btn-cancel" ValidationGroup="file_scan"
                                                CausesValidation="true" />
                                            <input type="button" value="Hủy" class="btn btn-cancel" onclick="hideFileScan();"
                                                tabindex="5">
                                        </td>
                                    </tr>
                                </tbody>
                            </table>--%>
                    </asp:Panel>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <%--<div style="display: block; z-index: 1004; outline: 0px none; height: auto; width: 480px; top: 130px; left: -240px !important; margin-left: 50%; position: fixed"
            class="ui-dialog ui-widget ui-widget-content ui-corner-all ui-draggable popup">

            <div class="ui-dialog-titlebar ui-widget-header ui-corner-all ui-helper-clearfix">
                <span class="ui-dialog-title" id="ui-dialog-title-filescanForm">
                    <img style="vertical-align: middle;" src="images/edit-add.png">&nbsp;&nbsp;Thêm
                        mới / Sửa File đính kèm </span><a href="#" class="ui-dialog-titlebar-close ui-corner-all"
                            role="button" onclick="hideFileScan()"><span class="ui-icon ui-icon-closethick">close</span>
                        </a>
            </div>

            <asp:Panel runat="server" DefaultButton="btn_savefile" Style="padding: 10px;">
                <asp:PlaceHolder ID="plh_err_file" runat="server" Visible="false">
                    <div style="color: Red; font-weight: bold; text-align: center" id="div_err">
                        <asp:Label ID="file_err_msg" runat="server" />
                    </div>
                </asp:PlaceHolder>
                <table>
                    <tbody>
                        <tr>
                            <th class="field_label" style="width: 100px;">File scan
                            </th>
                            <td>
                                <asp:RequiredFieldValidator runat="server" ErrorMessage="Vui lòng chọn file upload!"
                                    Display="Dynamic" ControlToValidate="txt_fileurl" ValidationGroup="file_scan"
                                    CssClass="error err_txt_fileurl" />
                                <div id="chooseFileDiv" class="display">
                                    <asp:TextBox ID="txt_foruploadfile" runat="server" CssClass="txt_foruploadfile hidden" />
                                    <asp:TextBox ID="txt_fileurl" runat="server" CssClass="txt_fileurl hidden" />
                                    <asp:FileUpload ID="file_upload" CssClass="file_dinhkem" runat="server" Style="width: 284px !important" />
                                </div>
                                <div id="editFileDiv" class="hidden">
                                    <input type="text" id="hd_file" readonly="readonly" disabled="disabled" />
                                    <a style="padding-left: 3px; color: blue;" href="javascript: showChooseFile();" id="showChooseFileA">Sửa</a>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <tr>
                                    <th class="field_label" ">
                                        Tên file <span style="color: red;">(*)</span>
                                    </th>
                                    <td>
                                        <asp:RequiredFieldValidator ControlToValidate="txt_tenfile" ID="RequiredFieldValidator4"
                                            runat="server" ErrorMessage="Vui lòng nhập tên file!" Display="Dynamic" ValidationGroup="file_scan"
                                            CssClass="error err_tenfile" />
                                        <asp:TextBox ID="txt_tenfile" CssClass="txt_tenfile" runat="server" Width="300px" />
                                    </td>
                                </tr>
                            <th class="field_label">Ngày scan
                            </th>
                            <td>
                                <asp:TextBox ID="txt_ngayscan" class="txt_ngayscan" runat="server" Style="width: 100px" />
                            </td>
                        </tr>
                        <tr>
                            <th class="field_label ">Tóm tắt
                            </th>
                            <td>
                                <asp:TextBox ID="txt_tomtat" CssClass="txt_tomtat" runat="server" TextMode="MultiLine" Rows="5" Style="line-height: 22px" Columns="38" />
                            </td>
                        </tr>
                        <!-- nhanVien -->
                        <!--  button fields -->
                        <tr style="height: 50px;">
                            <td>
                                <asp:TextBox ID="txt_fileid" CssClass="hidden txt_fileid" runat="server" />
                            </td>
                            <td style="padding-left: 65px;">
                                <asp:Button ID="btn_savefile" runat="server" OnClick="btnSaveFileOnclick" Text="Lưu lại"
                                    OnClientClick="hideFileScan();ShowAddSuccess('Cập nhật dữ liệu thành công!')" CssClass="save submitSaveFileHoSo" ValidationGroup="file_scan"
                                    CausesValidation="true" />
                                <input type="button" value="Hủy" class="btn btn-cancel" onclick="hideFileScan();"
                                    tabindex="5">
                            </td>
                        </tr>
                    </tbody>
                </table>
            </asp:Panel>
        </div>--%>


    <div id="dialogMsg" style="display: none">
        <asp:Panel DefaultButton="btnDeleteFileHoSo" runat="server">
            <div id='dialogDelete' style="display: block; outline: 0px none; height: auto; width: 480px; top: 20%; left: 397px; border-radius: 6px; padding: 0px 0px 0px;">
                <div style="padding: 5px 10px; border: 1px solid #f0f0f0; background: #DDDDDD; border-top-left-radius: 4px; border-top-right-radius: 4px;">
                    <span>
                        <img style="vertical-align: middle;" src="images/cancel.png" />
                        Xác nhận </span>
                </div>
                <div style="margin: 10px 0 5px; text-align: center">
                    <label style="font-size: 120%">
                        Bạn xác nhận sẽ xóa file hồ sơ này?</label>
                    <div style="margin-top: 10px">
                        <asp:HiddenField ID="deleteFileHoSoID" runat="server" />
                        <asp:HiddenField ID="nguoiUpFileID" runat="server" />
                        <asp:Button ID="btnDeleteFileHoSo" runat="server" Text="Có" CssClass="btn" OnClientClick="hideDiaLogDelete(1);ShowAddSuccess('Xóa dữ liệu thành công!')" Style="border-radius: 4px"
                            OnClick="btnDeleteFileHoSo_Clicked" />
                        <asp:Button ID="btnKhong" runat="server" Style="border-radius: 4px" CssClass="btn btn-cancel"
                            OnClientClick="hideDiaLogDelete(0); return false;" Text="Không"></asp:Button>
                    </div>
                </div>
            </div>
            <div class="ui-widget-overlay">
            </div>
        </asp:Panel>
    </div>

    <div id="hide_huyfileid" style="display: none">
        <div id='huyfileConfirm' style="display: block; outline: 0px none; height: auto; width: 480px; top: 20%; left: 397px; border-radius: 6px;">
            <div style="padding: 5px 10px; border: 1px solid #f0f0f0; background: #f0f0f0">
                <span>
                    <img style="vertical-align: middle;" src="images/cancel.png" />
                    Xác nhận </span>
            </div>
            <div style="margin: 10px 0 5px; text-align: center">
                <label style="font-size: 120%">
                    File tải lên sẽ bị xóa?</label>
                <div style="margin-top: 10px">
                    <button type="button" style="width: 100px; cursor: pointer; border-radius: 4px" onclick="trueHuyFileConfirm()">
                        Có</button>
                    <button type="button" style="width: 100px; cursor: pointer; border-radius: 4px" onclick="falseHuyFileConfirm()">
                        Không</button>
                </div>
            </div>
        </div>
    </div>

    <!-- end #dashboard -->
    <%--<div id="sidebar" class="right" >
        <asp:Literal ID="ltrSideMenu" runat="server"></asp:Literal>
    </div>--%>
    <script type="text/javascript">
        function hidePop() {
            $("#MainContent_light").hide();
            //$("#fade2").hide();
            $("#MainContent_popXoa").hide();
            $("#MainContent_fade").hide();
            //$("#MainContent_messageDiv").hide();
            $("#Form1").validationEngine("hideAll");
            //$("#MainContent_sidebar").hide();
        }
        //function showAddFileScan() {

        //    $("#div_err").addClass("hidden");
        //    $('#fileScanDiv').removeClass();
        //    $('#fileScanDiv').addClass("display");
        //    $('#MainContent_fade').addClass("display");
        //    $(window).scrollTop(0);

        //    var date = new Date();

        //    var dd = date.getDate();
        //    var mm = date.getMonth() + 1; //January is 0!

        //    var yyyy = date.getFullYear();
        //    if (dd < 10) {
        //        dd = '0' + dd;
        //    }
        //    if (mm < 10) {
        //        mm = '0' + mm;
        //    }
        //    var today = dd + '/' + mm + '/' + yyyy;
        //    $('.txt_ngayscan').val(today);

        //    //reset
        //    $('.txt_fileid').val("");
        //    $('#hd_file').val("");
        //    //$('.txt_ngayscan').val("");
        //    $('.txt_tenfile').val("");
        //    $('.txt_tomtat').val("");
        //}
        function hideFileScan() {
            $('#fileScanDiv').modal("hide");
            $('#MainContent_fade').modal("hide");
        }
    </script>
</asp:Content>
