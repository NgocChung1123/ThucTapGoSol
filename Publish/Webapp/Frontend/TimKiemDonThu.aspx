<%@ Page Title="" Language="C#" MasterPageFile="~/Frontend.Master" AutoEventWireup="true" CodeBehind="TimKiemDonThu.aspx.cs" Inherits="Com.Gosol.CMS.Web.Webapp.Frontend.TimKiemDonThu" %>
<%@ Register Src="~/Webapp/Frontend/SideBarTinNoiBat.ascx" TagPrefix="uc1" TagName="SideBarTinNoiBat" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .form-control {
            height: 34px !important;
        }

        .panel-heading {
            border-radius: 0px !important;
        }

        .panel {
            border-radius: 0px !important;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".liTraCuu").addClass("active");
        });
    </script>
    <div class="row">
        <div class="col-lg-9 col-md-9 col-xs-9 col-sm-9">
            <div class="box box-solid">
                <div class="box-header">
                    <i class="fa fa-search"></i>
                    <h1 class="box-title">TRA CỨU TRẠNG THÁI ĐƠN THƯ KHIẾU NẠI, TỐ CÁO</h1>
                </div>
                <div class="box-body">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <div class="col-lg-8 col-md-8 col-xs-8 col-sm-8">
                                <div class="input-group" style="margin-bottom: 5px;">
                                    <input type="text" name="message" placeholder="Nhập số đơn thư..." class="form-control" />
                                    <span class="input-group-btn">
                                        <button type="button" class="btn btn-primary btn-flat">Tra cứu</button>
                                    </span>
                                </div>
                                <div style="color: #a7a7a7; font-size: 12px; cursor: default;">
                                    * Nhập số đơn thư Ông (bà) muốn tra cứu. VD: BTD127, TTT124,..
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="box-header">
                    <i class="fa fa-search"></i>
                    <h1 class="box-title">TRA CỨU VĂN BẢN TRẢ LỜI ĐƠN THƯ KHIẾU NẠI, TỐ CÁO</h1>

                </div>
                <div class="box-body">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <div class="col-lg-8 col-md-8 col-xs-8 col-sm-8">
                                <div class="input-group" style="margin-bottom: 5px;">
                                    <input type="text" name="message" placeholder="Nhập số đơn thư..." class="form-control" />
                                    <span class="input-group-btn">
                                        <button type="button" class="btn btn-warning btn-flat">Tra cứu</button>
                                    </span>
                                </div>
                                <div style="color: #a7a7a7; font-size: 12px; cursor: default;">
                                    * Nhập số đơn thư Ông (bà) muốn tra cứu. VD: BTD127, TTT124,..
                                </div>

                            </div>
                        </div>
                    </div>
                </div>

                <div class="box-header">
                    <i class="fa fa-search"></i>
                    <h1 class="box-title">TRA CỨU KẾT QUẢ GIẢI QUYẾT ĐƠN THƯ</h1>

                </div>
                <div class="box-body">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <div class="col-lg-8 col-md-8 col-xs-8 col-sm-8">
                                <div class="input-group" style="margin-bottom: 5px;">
                                    <input type="text" name="message" placeholder="Nhập số đơn thư..." class="form-control" />
                                    <span class="input-group-btn">
                                        <button type="button" class="btn btn-default btn-flat">Tra cứu</button>
                                    </span>
                                </div>
                                <div style="color: #a7a7a7; font-size: 12px; cursor: default;">
                                    * Nhập số đơn thư Ông (bà) muốn tra cứu. VD: BTD127, TTT124,..
                                </div>

                            </div>
                        </div>
                    </div>
                </div>


                <div class="box-header" style="text-align: center;">
                    <div style="font-size: 20px; color: #515151; margin-bottom: 15px;">CỔNG THÔNG TIN CÔNG BỐ KẾT QUẢ GIẢI QUYẾT KHIẾU NẠI TỐ CÁO</div>
                    <div style="font-weight: bold; line-height: 15px; color: #48b5ee; font-size: 30px; margin-bottom: 15px;">THANH TRA TỈNH BÀ RỊA - VŨNG TÀU</div>
                </div>
                <div class="box-body">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <div class="col-lg-12 col-md-12 col-xs-12 col-sm-12">
                            </div>
                        </div>
                    </div>
                </div>


            </div>
        </div>

        <div class="col-lg-3 col-md-3 col-xs-3 col-sm-3 side-bar" style="padding-left: 0px;">
            <uc1:SideBarTinNoiBat runat="server" ID="SideBarTinNoiBat" />
            <div class="clearfix"></div>
        </div>
    </div>
    <div class="col-lg-9 col-md-9 col-xs-9 col-sm-9" style="display: none;">


        <div class="panel panel-primary" style="min-height: 1000px">


            <div class="panel-heading">TÌM KIẾM QUYẾT ĐỊNH GIẢI QUYẾT ĐƠN THƯ KHIẾU NẠI, TỐ CÁO</div>
            <div class="panel-body">
                <div class="row">
                    <div class=" col-md-12">
                        <div class="form-inline" style="margin-bottom: 10px">
                            <div class="form-group col-md-2">
                                <label class="sr-only">Số đơn thư</label>
                                <p class="form-control-static"><b>Số đơn thư</b></p>

                            </div>
                            <div class="form-group">
                                <asp:TextBox runat="server" ID="txtSearch" Class="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:Button runat="server" ID="btnSearch" Text="Tìm kiếm" Class="btn " OnClick="btnSearch_Click" />
                                <asp:Button runat="server" ID="btnQRcode" Text="Quét QR code" Class="btn " />
                            </div>

                        </div>

                    </div>
                    <div class="col-md-12">
                        <asp:Panel runat="server" ID="pnTable">
                            <table id="example2" class="table table-bordered table-hover table-responsive">
                                <thead>
                                    <tr>
                                        <th style="width: 5%">STT</th>
                                        <th style="width: 10%">Số đơn</th>
                                        <th style="width: 10%">Ngày tiếp nhận</th>
                                        <th style="width: 15%">CQ tiếp nhận</th>
                                        <th style="width: 15%">Tên chủ đơn</th>
                                        <th style="width: 25%">Nội dung vụ việc</th>
                                        <th style="width: 10%">Ngày ban hành QĐ</th>
                                        <th style="width: 10%">File quyết định</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="rptDonThu" runat="server" OnItemDataBound="rptDonThu_ItemDataBound">
                                        <ItemTemplate>
                                            <tr>
                                                <td style="text-align: left">
                                                    <asp:Label ID="lblSTT" Text="" runat="server" />
                                                </td>
                                                <td style="text-align: left">
                                                    <%# Eval("SoDonThu") %>
                                                </td>
                                                <td style="text-align: left">
                                                    <%# Eval("NgayTiepNhan") %>
                                                </td>
                                                <td style="text-align: left"><%# Eval("CoQuanTiepNhan") %></td>
                                                <td style="text-align: left">
                                                    <%# Eval("NguoiDaiDien") %>
                                                </td>
                                                <td style="text-align: left"><%# Eval("NoiDungDon") %></td>
                                                <td style="text-align: left">
                                                    <%# Eval("NgayBanHanh") %>
                                                </td>
                                                <td style="text-align: left">
                                                    <a style="cursor: pointer">
                                                        <img src="../../images/preview.png" style="width: 20px; height: 20px" />
                                                    </a>
                                                    <a href="../../images/011_yes-128.png" download="zzz">
                                                        <img src="../../images/download.png" style="width: 20px; height: 20px" />
                                                    </a>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                            <div class="paginations" style="margin-top: 15px">
                                <asp:PlaceHolder ID="plhPaging" runat="server"></asp:PlaceHolder>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </div>


</asp:Content>
