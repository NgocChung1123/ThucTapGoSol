<%@ Page Title="" Language="C#" MasterPageFile="~/Frontend.Master" AutoEventWireup="true" CodeBehind="TrinhTuThuTuc.aspx.cs" Inherits="Com.Gosol.CMS.Web.Webapp.Frontend.ThuTuc" EnableEventValidation="false" %>

<%@ Register Src="~/Webapp/Frontend/SideBarTinNoiBat.ascx" TagPrefix="uc1" TagName="SideBarTinNoiBat" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <link href="/AdminLte/jquery.formvalidation/css/formValidation.min.css" rel="stylesheet" />
    <link href="/AdminLte/plugins/select2/select2.min.css" rel="stylesheet" />

    <script src="/AdminLte/plugins/select2/select2.min.js" type="text/javascript"></script>

    <script type="text/javascript" src="/AdminLte/bootstrap/js/bootstrap-datepicker.min.js"></script>
    <script type="text/javascript" src="/AdminLte/plugins/select2/select2.full.min.js"></script>
    <link href="/AdminLte/dist/css/font-awesome.min.css" rel="stylesheet" />
    <link href="/Styles/custom_scroll.css" rel="stylesheet" />
    <style>
        /*#table-wrapper {
            position: relative;
            margin-top: 10px;
        }

        #table-scroll {
            height: 600px;
            overflow: auto;
            margin-top: 10px;
        }

        #table-wrapper #table {
            width: 100%;
        }

            #table-wrapper #table * {
                background: white;
                color: black;
            }

        .right {
            float: left;
        }

        .left {
            float: right;
            margin: 0px;
        }*/

        .control-label {
            text-align: left !important;
            white-space: nowrap !important;
        }

        .dotdot {
            max-height: 50px;
            max-width: 300px;
            white-space: nowrap;
            overflow: hidden;
            text-overflow: ellipsis
        }
    </style>

    <script>
        $(document).ready(function () {
            $("#txtSearchKey").on("keyup", function () {
                //alert("ahihi");
                var value = $(this).val().toLowerCase();
                $("#table tr").filter(function () {
                    $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
                });
            });

            e = jQuery.Event("keypress")
            e.which = 13 //choose the one you want
            $("#txtSearch").keypress(function () {
                $("#loginForm").modal("hide");
            }).trigger(e)

            $("i").hover(
                function () {
                    $(this).addClass("fa-lg");
                }, function () {
                    $(this).removeClass("fa-lg");
                }
            );
        });

        function reset(id) {
            $('#trinhtuthutuc' + id).html("");
            $('#trinhtuthutuc1').html("");
        };

        function loadThuTuc(id) {
            reset(id);
            $.ajax({
                type: "POST",
                url: "TrinhTuThuTuc.aspx/LoadThuTuc",
                data: "{ 'id':'" + id + "' }",
                dataType: "json",
                async: "true",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    //debugger;
                    var json = eval('(' + data.d + ')');
                    if (json != null) {
                        if (json.length > 0) {
                            var html = "";
                            html += "<table class='table table-bordered'><tr><td style='text-align:center'><b>STT</b></td><td><b style='text-align:left'>Trình tự thực hiện</b></td><td style='text-align:center'><b>Tệp đính kèm</b></td><tr>";
                            for (var i = 0; i < json.length; i++) {
                                html += "<tr><td>" + (i + 1) + "</td><td>" + json[i].NDThuTuc + "</td><td>" + "</td></tr>";
                            }
                            html += "</table>";
                            $("#trinhtuthutuc1").append(html);
                            $("#chitietthutuc").show();
                            $("#MainContent_UpdatePanel1").hide();
                            $(".UpdatePanel1").hide();
                            //for (var i = 0; i < json.length; i++) {
                            //    html += "<h5>";
                            //    html += json[i].NDThuTuc;
                            //    html += "</h5>";
                            //}
                            //var class_icon = $("#icon" + id).hasClass("fa-chevron-circle-down")
                            ////alert(class_icon);
                            //if (class_icon == true) {
                            //    $("#trinhtuthutuc" + id).append(html);
                            //    $('#icon' + id).removeClass('fa-chevron-circle-down').addClass('fa-chevron-circle-up');
                            //}
                            //else {
                            //    $("#trinhtuthutuc" + id).append("");
                            //    $('#icon' + id).removeClass('fa-chevron-circle-up').addClass('fa-chevron-circle-down');
                            //}



                        }

                        //else {
                        //    var class_icon = $("#icon" + id).hasClass("fa-chevron-circle-down")
                        //    if (class_icon == true) {
                        //        $("#trinhtuthutuc" + id).append("<span style='color:red;padding-left:30px;font-size:14px;'>Loại thủ tục này chưa có thông tin chi tiết</span>");
                        //        $('#icon' + id).removeClass('fa-chevron-circle-down').addClass('fa-chevron-circle-up');
                        //    }
                        //    else {
                        //        $("#trinhtuthutuc" + id).append("");
                        //        $('#icon' + id).removeClass('fa-chevron-circle-up').addClass('fa-chevron-circle-down');
                        //    }
                        //}

                    }
                }
            });
        }

    </script>

    <script>
        function hideSuccessSubmit() {
            $("#successSubmit").modal("hide");
        }

        function showthongBaoSuccess() {
            $("#successSubmit").modal("show");
            return false;
        }
    </script>

    <script>
        function ShowChiTiet(id) {
            $("#chitietthutuc").load("ChiTietThuTuc.aspx?id=" + id);
            $("#chitietthutuc").show();
            $("#MainContent_UpdatePanel1").hide();
             $(".UpdatePanel1").hide();
        }

        function BackChiTiet() {
            $("#MainContent_UpdatePanel1").show();
             $(".UpdatePanel1").show();
            $("#chitietthutuc").hide();
        }
    </script>

    <div class="col-md-9 content-left">
        <div style="margin-bottom: 30px;">
            <div class="box-header" style="text-align: center">
            </div>
            <div>
                <div class="form-horizontal" style="padding-top: 10px;">
                    <div class="form-group">
                        <label class="control-label col-lg-2 col-md-2 col-sm-2 col-xs-2">Từ khóa</label>
                        <div class="col-lg-8 col-md-8 col-sm-8 col-xs-12 form-group">
                            <%--<input type="text" id="txtSearchKey" placeholder="Nhập tên thủ tục cần tìm kiếm" class="form-control" />--%>
                            <asp:TextBox runat="server" ID="txtSearch" placeholder="Nhập tên thủ tục cần tìm kiếm" CssClass="form-control"></asp:TextBox>

                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12 text-center">
                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-sm btn-primary" Text="Tìm kiếm" OnClick="btnSearch_Click" OnClientClick="BackChiTiet();" />
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="box box-primary UpdatePanel1">
            <div class="box-header">
                <i class="glyphicon glyphicon-list"></i>
                <span class="box-title">DANH SÁCH THỦ TỤC</span>
            </div>
            <div class="box-body">
                <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                    </Triggers>
                    <ContentTemplate>
                        <div id="table_wrapper">
                            <div id="table-scroll">
                                <table id="table" class="table table-bordered table-hover" style="margin-top: 15px; width: 100%">
                                    <thead>
                                        <tr>
                                            <td style="width: 10%; text-align: center"><b>STT</b></td>
                                            <td style="width: 70%; text-align: center"><b>Tên thủ tục hành chính</b></td>
                                            <td style="width: 20%; text-align: center"><b>Tải thủ tục</b></td>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="rptThuTuc" runat="server" OnItemDataBound="rptThuTuc_ItemDataBound">
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="text-align: center;">
                                                        <asp:Label runat="server" ID="lblSTT"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left; cursor: pointer;" onclick='<%# "ShowChiTiet(" +Eval("LoaiThuTucID") + ");"%>'>
                                                        <div class="right">
                                                            <%--<div onclick="loadThuTuc(<%# Eval("LoaiThuTucID") %>);">--%>
                                                            <%# Eval("TenLoaiThuTuc") %>
                                                            <%--</div>--%>
                                                        </div>
                                                    </td>
                                                    <td style="text-align: center;">

                                                        <asp:LinkButton ID="download" ToolTip="Bấm vào đây để tải thủ tục" runat="server" CssClass='<%# Eval("FileUrl") == ""?"hidden":"" %>' CommandArgument='<%# Eval("FileUrl") %>' CommandName="Download_File" OnClick="Download_Click">
                                                                <i class="fa fa-download"></i>
                                                        </asp:LinkButton>

                                                        <%-- <div title="Bấm để xem trình tự thực hiện thủ tục" onclick="loadThuTuc(<%# Eval("LoaiThuTucID") %>);">
                                                                <i id="icon<%# Eval("LoaiThuTucID") %>" class="fa fa-chevron-circle-down"></i>
                                                            </div>--%>

                                                        <%--<span class="left" style="font-size: 12px; padding-right: 5px;"><%# Eval("FileName") %></span>--%>

                                                        <div style="margin-left: 30px; clear: left" id="trinhtuthutuc<%# Eval("LoaiThuTucID") %>">
                                                        </div>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>

                                <div class="paginations" style="margin-top: 15px; margin-bottom: 15px">
                                    <asp:PlaceHolder ID="plhPaging" runat="server"></asp:PlaceHolder>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

             
            </div>
            <!-- end #dashboard -->
        </div>
           <div id="chitietthutuc" style="display: none">
              
                </div>
   
    </div>

    <div class="col-md-3">
        <uc1:SideBarTinNoiBat runat="server" ID="SideBarTinNoiBat" />
    </div>

    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="successSubmit" class="modal fade">
        <div class="modal-dialog  modal-md">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Thông báo!</h4>
                </div>
                <div class="modal-body">
                    <asp:Label ID="lblContentSuccess" runat="server" Style="color: #008d4c"></asp:Label>
                    //<asp:Label ID="lblContentSuccess1" runat="server" Style="color: #008d4c"></asp:Label>
                </div>
                <div class="modal-footer" style="text-align: center">
                    <button type="button" onclick="hideSuccessSubmit();">
                        Đóng</button><br />
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
    </div>

</asp:Content>
