<%@ Page Title="" Language="C#" MasterPageFile="~/Frontend.Master" AutoEventWireup="true" ClientIDMode="AutoID" CodeBehind="HoiDap.aspx.cs" Inherits="Com.Gosol.CMS.Web.Webapp.Frontend.HoiDap" EnableEventValidation="false" %>

<%@ Register Src="~/Webapp/Frontend/SideBarTinNoiBat.ascx" TagPrefix="uc1" TagName="SideBarTinNoiBat" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <link href="/AdminLte/jquery.formvalidation/css/formValidation.min.css" rel="stylesheet" />
    <link href="/AdminLte/plugins/select2/select2.min.css" rel="stylesheet" />
    <script src="/AdminLte/plugins/select2/select2.min.js" type="text/javascript"></script>

    <%-- <link href="/AdminLte/plugins/datepicker/datepicker3.css" rel="stylesheet" />
    <link href="/AdminLte/plugins/daterangepicker/daterangepicker.css" rel="stylesheet" />--%>

    <script type="text/javascript" src="/AdminLte/bootstrap/js/bootstrap-datepicker.min.js"></script>
    <script type="text/javascript" src="/AdminLte/plugins/select2/select2.full.min.js"></script>
    <%--    <script type="text/javascript" src="/AdminLte/bootstrap/js/bootstrap.min.js"></script>--%>

    <%--<script src="/AdminLte/jquery.formvalidation/js/formValidation.min.js"></script>--%>
    <%--<script src="/AdminLte/jquery.formvalidation/js/framework/bootstrap.min.js"></script>--%>
    <script type="text/javascript" src="HoiDap.js"></script>
    <%--<script type="text/javascript" src="js/jquery.maskedinput.min.js"></script>--%>
    <link href="/Styles/custom_scroll.css" rel="stylesheet" />
    <style>
        /*#tableNoiDungTimKiem thead, #tableNoiDungTimKiem tbody tr {
            display: table;
            width: 100%;
            table-layout: fixed;
        }

        #tableNoiDungTimKiem tbody {
            display: block;
            max-height: 600px;
            min-height: 30px;
            overflow: auto;
            overflow-x: hidden;
        }*/

        .myCssClass {
            color: red;
        }

        .div-header {
            text-align: center;
            width: 100%;
            height: 50px;
            font-family: 'Times New Roman';
            font-size: 20px;
            color: red;
            padding-top: 10px;
        }

        .list-group-item:last-child {
            border-bottom-left-radius: 0px;
            border-bottom-right-radius: 0px;
            border-top-left-radius: 0px;
            border-top-right-radius: 0px;
        }

        .control-label {
            text-align: left !important;
            white-space: nowrap !important;
        }
    </style>

    <script type="text/javascript">

        $('.validateCapcha').click(function () {
            return false;
        });

        function ShowChiTiet(id) {
            $("#chitietcauhoi").load("ChiTietCauHoi.aspx?id=" + id);
            $("#chitietcauhoi").show();
            $("#ctl00_MainContent_UpdatePanel1").hide();
            //if ($("#chitiet" + id).is(":visible")) {
            //    $("#chitiet" + id).hide();
            //}
            //else {
            //    $(".chitiet").hide();
            //    $("#chitiet" + id).show();
            //}
        }

        function BackChiTiet() {
            $("#ctl00_MainContent_UpdatePanel1").show();
            $("#chitietcauhoi").hide();
        }

        function ShowForm() {
            $("#divGuiCauHoi").toggle();
            $(".listQuestion").hide();
        }
        function ShowList() {

            $("#divGuiCauHoi").hide();
            $(".listQuestion").show();
        }
    </script>

    <div class="col-md-9 content-left">

        <asp:HiddenField runat="server" ID="hdfTab" />

        <div class="box box-solid" style="display: none;" id="divGuiCauHoi">
            <div class="box-header text-center">
                <span class="box-title">GỬI CÂU HỎI</span>
            </div>
            <div class="box-body">
                <div class="form-horizontal" style="padding: 10px;">
                    <div class="form-group">
                        <label class="col-lg-2 col-md-2 col-sm-2 col-xs-3 control-label"><span style="color: red;">*</span>Họ và tên:</label>
                        <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12">
                            <asp:TextBox ID="txtHoTen" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Vui lòng nhập họ tên!" ControlToValidate="txtHoTen" ForeColor="Red" SetFocusOnError="true"
                                Display="Dynamic" ValidationGroup="vldGuiCauHoi"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ControlToValidate="txtHoTen" runat="server" Display="Dynamic"
                                ErrorMessage="Độ dài 3-30 ký tự, không chứa ký tự đặc biệt!" ValidationExpression="[a-zA-Z0-9_ '-'\sáàảãạăâắằấầặẵẫậéèẻẽẹêếềểễệóòỏõọôốồổỗộơớờởỡợíìỉĩịđùúủũụưứửữựÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠƯĂẠẢẤẦẨẪẬẮẰẲẴẶẸẺẼỀỂỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤƯỨỪỬỮỰỲỴÝỶỸỳỵỷỹý]{3,30}$" ForeColor="Red" SetFocusOnError="true" ValidationGroup="vldGuiCauHoi"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-lg-2 col-md-2 col-sm-2 col-xs-3 control-label"><span style="color: red;">*</span>Email:</label>
                        <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12">
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Vui lòng nhập Email!" ControlToValidate="txtEmail" ForeColor="Red" SetFocusOnError="true"
                                Display="Dynamic" ValidationGroup="vldGuiCauHoi"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator9" ControlToValidate="txtEmail" runat="server" ValidationGroup="vldGuiCauHoi" Display="Dynamic"
                                ErrorMessage="Email không hợp lệ!" ValidationExpression="^(([^<>()\[\]\\.,;:\s@']+(\.[^<>()\[\]\\.,;:\s@']+)*)|('.+'))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$" ForeColor="Red" SetFocusOnError="true">
                            </asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-lg-2 col-md-2 col-sm-2 col-xs-3 control-label"><span style="color: red;">*</span>Số điện thoại:</label>
                        <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12">
                            <asp:TextBox ID="txtSDT" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Vui lòng nhập SĐT!" ControlToValidate="txtSDT" ForeColor="Red" SetFocusOnError="true"
                                Display="Dynamic" ValidationGroup="vldGuiCauHoi"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="txtSDT" runat="server" Display="Dynamic"
                                ErrorMessage="Số điện thoại không hợp lệ!" ValidationExpression="^\d{10,11}$" ForeColor="Red" SetFocusOnError="true" ValidationGroup="vldGuiCauHoi">
                            </asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-lg-2 col-md-2 col-sm-2 col-xs-3 control-label"><span style="color: red;">*</span>Lĩnh vực:</label>
                        <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12">
                            <asp:DropDownList ID="ddlLinhVuc_GuiCauHoi" runat="server" DataTextField="TenLinhVuc" CssClass="form-control select2" DataValueField="IDLinhVuc" Style="width: 100%" onchange="changeLinhVucCauHoi()"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" CssClass="rfv" runat="server" ControlToValidate="ddlLinhVuc_GuiCauHoi" ValidationGroup="vldGuiCauHoi" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"
                                ErrorMessage="Vui lòng chọn lĩnh vực!" InitialValue="Chọn lĩnh vực"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-lg-2 col-md-2 col-sm-2 col-xs-3 control-label"><span style="color: red;">*</span>Tiêu đề:</label>
                        <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12">
                            <asp:TextBox ID="txtTitle" runat="server" placeholder="Tiêu đề câu hỏi" Rows="2" CssClass="form-control" Style="resize: none;"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Vui lòng nhập câu hỏi!" ControlToValidate="txtTitle" ForeColor="Red" SetFocusOnError="true"
                                Display="Dynamic" ValidationGroup="vldGuiCauHoi"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-lg-2 col-md-2 col-sm-2 col-xs-3 control-label"><span style="color: red;">*</span>Nội dung:</label>
                        <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12">
                            <asp:TextBox ID="txtGuiCauHoi" runat="server" placeholder="Nội dung câu hỏi" TextMode="multiline" Columns="50" Rows="7" CssClass="form-control" Style="resize: none;"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Vui lòng nhập câu hỏi!" ControlToValidate="txtGuiCauHoi" ForeColor="Red" SetFocusOnError="true"
                                Display="Dynamic" ValidationGroup="vldGuiCauHoi"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-lg-2 col-md-2 col-sm-2 col-xs-3 control-label"><span style="color: red;">*</span>Mã xác nhận:</label>
                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                            <asp:TextBox ID="txtCaptcha" placeholder="Nhập mã xác nhận" runat="server" CssClass="form-control" Style="padding: 5px" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Vui lòng nhập Captcha!" ControlToValidate="txtCaptcha" ForeColor="Red" SetFocusOnError="true"
                                Display="Dynamic" ValidationGroup="vldGuiCauHoi"></asp:RequiredFieldValidator>

                        </div>
                        <div class="col-lg-5 col-md-5 col-sm-5 col-xs-12">
                            <BotDetect:WebFormsCaptcha ID="Captcha" runat="server" />

                            <asp:Label ID="lblBaoLoi" runat="server" Style="padding: 5px" Display="none" ForeColor="Red" />
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-lg-12 text-center">
                            <asp:Button ID="btnGuiCauHoi" runat="server" CssClass=" btn btn-sm btn-primary" OnClick="btnGuiCauHoi_Click" Text="Gửi câu hỏi" ValidationGroup="vldGuiCauHoi" />
                            <input type="button" onclick="ShowList();" value="Quay lại" class="btn btn-sm" />
                        </div>

                    </div>
                </div>
            </div>
        </div>

        <div class="box box-solid listQuestion">
            <div class="box-header text-center">
                <span class="box-title">TÌM KIẾM CÂU HỎI</span>
            </div>

            <div class="box-body">
                <asp:HiddenField ID="hdfCoQuanID" runat="server" />
                <div class="form-horizontal" style="padding: 10px;">
                    <div class="form-group">
                        <label class="col-lg-2 col-md-2 col-sm-2 col-xs-3 control-label" style="text-align: left;">Lĩnh vực:</label>
                        <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12">
                            <asp:DropDownList ID="ddlLinhVuc" runat="server" DataTextField="TenLinhVuc" CssClass="form-control select2" DataValueField="IDLinhVuc" Style="width: 100%"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-lg-2 col-md-2 col-sm-2 col-xs-3 control-label" style="text-align: left;">Nội dung:</label>
                        <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12">
                            <asp:TextBox ID="txtNoiDungCauHoi" runat="server" placeholder="Nội dung câu hỏi" Columns="50" Rows="2" CssClass="form-control" Style="resize: none;"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-ld-12 text-center">
                            <%--<input type="button" id="btnSearch" class="btn" onclick="timKiemCauHoi();" value="Tìm kiếm" />--%>
                            <asp:Button runat="server" ID="btnSearch" OnClick="btnSearch_Click" Text="Tìm kiếm" CssClass="btn btn-sm btn-primary" />
                            <input type="button" onclick="ShowForm();" value="Gửi câu hỏi" class="btn btn-sm btn-default" />
                        </div>
                    </div>
                </div>

                <div class="table-responsive" id="listCauHoi">
                    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                            <%--<asp:AsyncPostBackTrigger ControlID="ctl00_MainContent_hplPage1"/>--%>
                        </Triggers>
                        <ContentTemplate>
                            <table id="tableNoiDungTimKiem" class="table table-bordered table-hover table-responsive" style="margin-top: 15px; width: 100%;">

                                <thead>
                                    <tr>
                                        <th style="width: 5%; text-align: center">STT</th>
                                        <th style="width: 50%; text-align: center">Câu hỏi</th>
                                        <th style="width: 15%; text-align: center">Lĩnh vực</th>
                                        <th style="width: 15%; text-align: center">Người hỏi</th>
                                        <th style="width: 15%; text-align: center">Trạng thái</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="rptCauHoi" runat="server" OnItemDataBound="rptCauHoi_ItemDataBound">
                                        <ItemTemplate>
                                            <tr onclick='<%# "ShowChiTiet(" +Eval("IDCauHoi") + ");"%>' style="cursor: pointer">
                                                <td style="text-align: center">
                                                    <asp:Label runat="server" ID="lblStt"></asp:Label></td>
                                                <td>
                                                    <label><%# Eval("NDCauHoi").ToString().Length >50 ? Eval("NDCauHoi").ToString().Substring(0,50)+"..." : Eval("NDCauHoi") %></label>
                                                </td>
                                                <td><%# Eval("TenLinhVuc") %></td>
                                                <td><%# Eval("HoTen") %></td>
                                                <td>
                                                    <%# (int)Eval("IDTraLoi")!=0 ? "Đã trả lời" : "Chưa trả lời" %>
                                                </td>
                                            </tr>
                                            <tr style="display: none" class="chitiet" id='<%# "chitiet"+ Eval("IDCauHoi")%>'>
                                                <td><%--%# Eval("CreateDate") %>--%></td>
                                                <td><%# Eval("NDCauHoi") %></td>
                                                <td colspan="3"><%# Eval("NDTraLoi") %></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                            <div class="paginations" style="margin-top: 15px; margin-bottom: 15px">
                                <asp:PlaceHolder ID="plhPaging" runat="server"></asp:PlaceHolder>
                            </div>
                            <div class="clearfix"></div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                <div id="chitietcauhoi" style="display: none">
                </div>

            </div>

        </div>

        <asp:HiddenField runat="server" ID="hdfLinhVucCauHoi" />

        <%--<div class="col-md-4 side-bar">
        <div class="list_vertical">
            <section class="accordation_menu" style="">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title text-center">Danh mục hỏi đáp</h3>
                    </div>
                    <table id="tableLinhVuc" class="table" style="width: 100%">
                        <tbody>
                            <asp:Repeater ID="rptLinhVuc" runat="server" OnItemDataBound="rptLinhVuc_ItemDataBound">
                                <ItemTemplate>
                                    <tr>
                                        <td style="width: 100%; padding: 0px !important">
                                            <div>
                                                <ul class="list-group" style="margin: 0px;">
                                                    <a class="list-group-item" href="#tabND_1">
                                                        <asp:Label ID="lblNoiDungLinhVuc" Style="cursor: pointer;" onclick='<%# "showCauHoi(" + Eval("IDLinhVuc") +  ");" %>' runat="server"></asp:Label>
                                                    </a>
                                                </ul>
                                            </div>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>

            </section>
        </div>
    </div>--%>
    </div>

    <div class="col-md-3">
        <uc1:SideBarTinNoiBat runat="server" ID="SideBarTinNoiBat" />
    </div>

    <div id="tmplCauHoiRow" style="display: block">
        <table id="myData">
            <tr onclick='Selected(this);'>
                <td style="width: 100%; padding: 0px;">
                    <div style="display: none;" id="_row_">_row_</div>
                </td>
            </tr>
        </table>
    </div>

    <div class="col-md-8 content-left">
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
                </div>
                <div class="modal-footer" style="text-align: center">
                    <button type="button" onclick="hideSuccessSubmit();" class="btn">
                        Đóng</button><br />
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
    </div>

    <script>
        //$(document).ready(function () {
        //    if ($('.user').css('display') == 'none') {
        //        $("#MainContent_btnGuiCauHoi").css('Enabled', 'false');
        //        alert("ABC");
        //    }
        //    if ($('.user').css('display') == 'block') {
        //        //$("#MainContent_btnGuiCauHoi").css('Enabled', 'false');
        //        alert("ABCD");
        //    }
        //});
    </script>
</asp:Content>
