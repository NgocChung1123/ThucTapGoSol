<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CauHinhModuleHT.aspx.cs" Inherits="Com.Gosol.CMS.Web.CauHinhModuleHT" %>

    <asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <style type="text/css">
        .pagination {
            margin-top: 15px;
            text-align: right;
        }

        .pagination a:HOVER {
            background-color: #F5DBB8;
        }

        .pagination a, .pagination span {
            background: #fff;
            padding: 5px;
            margin: 2px;
            border: 1px solid #d9d9d9;
            text-decoration: none;
            -moz-border-radius: 3px;
            -webkit-border-radius: 3px;
        }

        .pagination span.current {
            background: #CFF2FA;
            padding: 5px;
            margin: 2px;
            border: 1px solid #d9d9d9;
            text-decoration: none;
            -moz-border-radius: 3px;
            -webkit-border-radius: 3px;
        }
    </style>

    <script type="text/javascript">
        $(document).ready(function () {
            $(".datepicker").datepicker();
        });


        function showThongBaoSuccess() {
            $("#thongBaoSuccess").modal();
            return false;
        }

        function hideThongBaoSuccess() {
            $("#thongBaoSuccess").modal("show");
            $("#MainContent_fade").show();
        }

        function showAddForm() {
            $("#MainContent_light").show();
            $("#MainContent_fade").show();

            return false;
        }

        function hideAddEditForm() {
            $("#MainContent_light").hide();
            $("#MainContent_fade").hide();
        }

        function resetForm() {
            $("#MainContent_txtSystemConfigID").val("");
            $("#MainContent_txtConfigKey").val("");
            $("#MainContent_txtConfigValue").val("");
            $("#MainContent_txtDescription").val("");
            hideEditFormThamSoHeThong();
        }

        function showEditFormThamSoHeThong() {
            $("#addUserForm").modal("show");
            $("#fade").modal("show");
        }
        function hideEditFormThamSoHeThong() {
            $("#addUserForm").modal("hide");
            $("#fade").modal("hide");
        }

        function updateTrangThaiHienThi() {
            console.log("updateTrangThaiHienThi");
        }

        function showAjaxLoading() {
            $("#progressDiv").show();
            $("#ajax_fade").show();
        }

        function hideAjaxLoading() {
            $("#progressDiv").hide();
            $("#ajax_fade").hide();
        }

        function LoadData(currentPage) {
            console.log("LoadData", currentPage);
            //showAjaxLoading();

            var module = $("#MainContent_ddlModule").val();     
            var txtPageSize = $("#MainContent_hdfPageSize").val();

            $.ajax({
                type: "POST",
                url: "CauHinhModuleHT.aspx/LoadData",
                data: '{currentPage: "' + currentPage + '",module:"' + module + '"}',
                dataType: "json",
                async: "true",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    var json = eval('(' + data.d + ')');
                    console.log("json_LoadData", json);
                    $("#DSDonThuCanDuyetGiaiQuyet >tbody").html("");
                    var stt = 0;
                    if (json.length > 0) {
                        for (var i = 0; i < json.length; i++) {
                            var classtr = "odd";
                            if (i % 2 == 0) {
                                classtr = "even";
                            }

                            stt++;

                            var hiddenGroup = "";
                            var chitietdonthu = "";
                            var checkbox = "<input type='checkbox' id='CheckBox" + json[i].ModuleID + "' class='CheckBoxSelected' onclick='CheckSelected(" + json[i].ModuleID + ")' />";
                            var textbox = "<input type='text' id='TextBox" + json[i].ModuleID + "' onblur='UpdateThuTu(" + json[i].ModuleID + ")' value=" + json[i].ThuTuHienThi + " style='text-align: center; width:30%' />";

                            $("#DSDonThuCanDuyetGiaiQuyet >tbody").append("<tr class='" + classtr + "'>" + hiddenGroup
                                + "</td><td class='sodon'" + chitietdonthu + " style='text-align: center;'>" + stt
                                + "</td><td class='sodon'" + chitietdonthu + " style='text-align: left;'>" + json[i].Muc
                                + "</td><td class='hoten'" + chitietdonthu + " style='text-align: center;'>" + json[i].ModuleStr
                                + "</td><td class='hoten'" + chitietdonthu + " style='text-align: center;'>" + checkbox
                                + "</td><td class='hoten'" + chitietdonthu + " style='text-align: center;'>" + textbox
                                + "</td></tr>");

                            if (json[i].TrangThaiHienThi == true) {
                                $("#CheckBox" + json[i].ModuleID).attr('checked', 'checked');
                                //$("#CheckBox" + json[i].ModuleID).prop('checked', true);
                            }
                        }
                        BindPaging(parseInt(txtPageSize), parseInt(currentPage), json[0].Count);
                    }
                    //hideAjaxLoading();
                }
            });
        }

        function BindPaging(pagesize, currentpage, countsearch) {
            $("#divpaging").html("");
            var Pageall = 1;
            if (countsearch > pagesize) {
                if (countsearch % pagesize == 0) {
                    Pageall = countsearch / pagesize;
                }
                else {
                    Pageall = (countsearch - (countsearch % pagesize)) / pagesize + 1;
                }
            }
            var chuoi = "";

            if (Pageall > 1) {
                if (Pageall > 5) {
                    var start = 1;
                    var end = 10;
                    if (Pageall < 10) {
                        end = Pageall;
                    }
                    else {
                        if (currentpage > 5) {
                            end = currentpage + 5;
                            if (end > Pageall) {
                                end = Pageall;
                                start = end - 10;
                            }
                            else start = currentpage - 5;
                        }
                    }

                    for (var i = start; i <= end; i++) {
                        if (i == currentpage) {
                            chuoi = chuoi + "<span class='current'>" + i + "</span>";
                        }
                        else {
                            chuoi = chuoi + "<a href='#' Onclick='MovingPageDS(" + i + ")'>" + i + "</a>";
                        }
                    }
                }
                else
                    for (var i = 1; i <= Pageall; i++) {
                        if (i == currentpage) {
                            chuoi = chuoi + "<span class='current'>" + i + "</span>";
                        }
                        else {
                            chuoi = chuoi + "<a href='#' Onclick='MovingPageDS(" + i + ")'>" + i + "</a>";
                        }
                    }
                // hien trang dau, trang cuoi
                if (Pageall > 10) {
                    if (currentpage > 6) {
                        chuoi = "<a href='#' Onclick='MovingPageDS(" + 1 + ")'>" + "Trang đầu" + "</a>" + chuoi;
                    }
                    if (Pageall - currentpage > 5) {
                        chuoi = chuoi + "<a href='#' Onclick='MovingPageDS(" + Pageall + ")'>" + "Trang cuối" + "</a>";
                    }
                }

                $("#divpaging").html(chuoi);
            }
        }
        function MovingPageDS(page) {
            LoadData(page);
        }
        function GoSearch() {
            LoadData(1);
        }

        function UpdateThuTu(moduleID) {   
            var thutu = $('#TextBox' + moduleID).val();
            console.log("thutu", thutu);
            $.ajax({
                type: "POST",
                url: "CauHinhModuleHT.aspx/UpdateThuTuHienThi",
                data: '{thutu: "' + thutu + '",moduleID:"' + moduleID + '"}',
                dataType: "json",
                async: "true",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    var json = eval('(' + data.d + ')');
                    console.log("UpdateThuTu", json);  
                }
            });
        }

        function CheckSelected(moduleID) {
            var checkBox = document.getElementById("CheckBox" + moduleID);
            var trangthai = checkBox.checked;
            console.log("CheckSelected", checkBox.checked);
            $.ajax({
                type: "POST",
                url: "CauHinhModuleHT.aspx/UpdateTrangThaiHienThi",
                data: '{trangthai: "' + trangthai + '",moduleID:"' + moduleID + '"}',
                dataType: "json",
                async: "true",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    var json = eval('(' + data.d + ')');
                    console.log("UpdateTrangThaiHienThi", json);
                }
            });
        }
    </script>



    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="addUserForm" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <img style="float: left;" src="/images/edit-add.png">
                    <h4 class="modal-title" style="margin-left: 30px;">Thêm người dùng vào nhóm</h4>
                </div>
                <div class="modal-body">

                    <div class="form-horizontal" role="form">
                        <div class="form-group">
                            <label class="col-lg-3 col-sm-3 control-label"></label>
                            <div class="col-lg-9">
                                <asp:TextBox ID="txtSystemConfigID" runat="server" Visible="false" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-lg-3 col-sm-3 control-label">Tên tham số: <span style="color: red;">*</span></label>
                            <div class="col-lg-9">
                                <asp:TextBox ID="txtConfigKey" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-3 col-sm-3 control-label">Giá trị: <span style="color: red;">*</span></label>
                            <div class="col-lg-9">
                                <asp:TextBox ID="txtConfigValue" runat="server" Enabled="true" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-lg-3 col-sm-3 control-label">Ghi chú:</label>
                            <div class="col-lg-9">
                                <asp:TextBox ID="txtDescription" runat="server" Enabled="true" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                    </div>
                </div>
                <div class="modal-footer" style="text-align: center">
                    <asp:Button ID="btnSubmit" runat="server" CssClass="save validate_button" Text="Lưu lại" OnClick="btnSubmit_Click" />
                    <asp:Button ID="btnReset" runat="server" CssClass="save btn-cancel" Text="Hủy bỏ" OnClick="btnReset_Click" CausesValidation="false" OnClientClick="resetForm(); return false;" />
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <div id="fade" class="black_overlay" runat="server">
    </div>
    <div id="fade2" class="black_overlay">
    </div>

    <div class="content-header">
        <h1>Cấu hình
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
            <li><a href="#">Hệ thống</a></li>
            <li class="active">Khai báo tham số hệ thống</li>
        </ol>
    </div>
    <div style="text-align: center">
        <asp:Label ID="lblMsg" ForeColor="#008d4c" Text="" Visible="false" runat="server" CssClass="" />
    </div>
    <div class="content">
        <div class="row">
            <div class="col-xs-12">
                <div class="box">
                    <div class="box-header">
                        <%--<button type="button" class="btn btn-primary" id="btnThemNhom" onclick="showAddForm(); return false">
                            <span class="glyphicon glyphicon glyphicon-plus-sign" style="margin-right:5px"></span>Thêm nhóm người dùng
                        </button>--%>
                    </div>
                    <div class="box-body table-responsive">

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

                        <div class="col-lg-12" style="padding-bottom:15px">

                            <asp:Panel ID="pnlSearch" runat="server">
                               <%-- <asp:Button ID="btnSearch" runat="server" CausesValidation="false" Style="float: right; margin-right: 10px; margin-bottom: 10px" CssClass="btn btn-default btn-sm" OnClick="btnSearch_Click" Text="Tìm kiếm" />
                                <asp:TextBox ID="txtSearch" placeholder="Nhập nội dung cần tìm kiếm" runat="server" CssClass="form-control" Style="float: right; margin-right: 10px; margin-bottom: 10px; width: 30%"></asp:TextBox>--%>
                                <asp:Label ID="Label1" runat="server" Text="Lọc theo: "></asp:Label>
                                <asp:DropDownList ID="ddlModule" runat="server" CssClass="chosen" DataTextField="ModuleStr" DataValueField="Module" onchange="GoSearch(this);" Width="150" Height="28">
                                </asp:DropDownList>
                                <br />
                            </asp:Panel>
                           
                        </div>
   
                        <asp:HiddenField ID="hdfCurrentPage" runat="server" />
                        <asp:HiddenField ID="hdfPageSize" runat="server" />

                        <div style="width: 100%; max-height: 600px;" class="tableFixHead">
                            <table id="DSDonThuCanDuyetGiaiQuyet" class="table table-bordered table-hover" style="width: 100%;">
                                <thead>
                                    <tr>
                                        <th style="width: 10%; text-align: center;">STT</th>
                                        <th style="width: 30%; text-align: center;">Mục</th>
                                        <th style="width: 20%; text-align: center;">Module</th>
                                        <th style="width: 20%; text-align: center;">Trạng thái hiển thị</th>
                                        <th style="width: 20%; text-align: center;">Thứ tự hiển thị</th>
                                    </tr>
                                </thead>
                                <tbody>
                                </tbody>
                            </table>
                        </div>

                        <div id="divpaging" class="pagination" style="float:right">
                        </div>
                        <br />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- end #dashboard -->
    <div id="sidebar" class="right">
        <asp:Literal ID="ltrSideMenu" runat="server">
        </asp:Literal>
    </div>
    <!-- end #sidebar -->
</asp:Content>
