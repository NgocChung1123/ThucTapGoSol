<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="SyncManage.aspx.cs" Inherits="Com.Gosol.CMS.Web.SyncManage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .redClass {
            color: red;
        }
    </style>

    <script type="text/javascript">

        function showDonThuDongBoDiv() {
            $("#donThuDongBoDiv").show();
            $("#fade").show();
        }

        function hideDonThuDongBoDiv() {
            $("#donThuDongBoDiv").hide();
            $("#fade").hide();
        }

        function showBaoCao2aDiv() {
            $("#baoCao2aDiv").show();
            $("#fade").show();
        }

        function hideBaoCao2aDiv() {
            $("#baoCao2aDiv").hide();
            $("#fade").hide();
        }

        function showBaoCao2bDiv() {
            $("#baoCao2bDiv").show();
            $("#fade").show();
        }

        function hideBaoCao2bDiv() {
            $("#baoCao2bDiv").hide();
            $("#fade").hide();
        }

        function showBaoCao2cDiv() {
            $("#baoCao2cDiv").show();
            $("#fade").show();
        }

        function hideBaoCao2cDiv() {
            $("#baoCao2cDiv").hide();
            $("#fade").hide();
        }

        function showBaoCao2dDiv() {
            $("#baoCao2dDiv").show();
            $("#fade").show();
        }

        function hideBaoCao2dDiv() {
            $("#baoCao2dDiv").hide();
            $("#fade").hide();
        }

        $(document).ready(function () {
            $("#MainContent_ddlReportType").change(function () {
                var loaiDuLieu = $(this).val();
                toogleDateMonthByLoaiDuLieu(loaiDuLieu);
            });

            toogleDateMonthByLoaiDuLieu($("#MainContent_ddlReportType").val());

            $("#checkAllDonThu").change(function () {
                var isChecked = $(this).is(":checked");
                if (isChecked) {
                    $(".check_td input[type='checkbox']").prop("checked", "checked");
                }
                else {
                    $(".check_td input[type='checkbox']").prop("checked", false);
                }
            })
        });

        function toogleDateMonthByLoaiDuLieu(loaiDuLieu) {            
            if (loaiDuLieu == "don_thu") {
                $(".don_thu_row").show();
                $(".bao_cao_row").hide();
            }
            else {
                $(".don_thu_row").hide();
                $(".bao_cao_row").show();
            }
        }

    </script>
    <div id="fade" class="black_overlay" style="z-index: 1001!important">
    </div>
    <div class="dashboard" style="padding: 10px; margin-right: 10px;">
        <asp:Label ID="lblMessage" runat="server"></asp:Label>
        <h3 style="padding-left: 0px;">Thông số kết nối</h3>
        <table style="margin-top: 10px;">
            <tr>
                <td style="width: 140px;">Địa chỉ IP máy chủ:
                </td>
                <td style="width: 220px;">
                    <asp:Label runat="server" Enabled="false" ID="lblServerIP"></asp:Label>
                </td>
                <td style="width: 120px;">Tài khoản đồng bộ
                </td>
                <td style="width: 140px;">
                    <asp:Label runat="server" Enabled="false" ID="lblAccountInfo"></asp:Label>
                </td>
            </tr>
        </table>
        <h3 style="padding-left: 0px; margin-top: 10px;">Chọn dữ liệu đồng bộ</h3>
        <table style="margin-top: 10px;">
            <tr style="height: 35px;">
                <td>Dữ liệu đồng bộ
                </td>
                <td colspan="3">
                    <asp:DropDownList runat="server" ID="ddlReportType">
                        <asp:ListItem Text="Báo cáo 2a" Value="2a"></asp:ListItem>
                        <asp:ListItem Text="Báo cáo 2b" Value="2b"></asp:ListItem>
                        <asp:ListItem Text="Báo cáo 2c" Value="2c"></asp:ListItem>
                        <asp:ListItem Text="Báo cáo 2d" Value="2d"></asp:ListItem>
                        <asp:ListItem Text="Dữ liệu đơn thư" Value="don_thu"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr class="don_thu_row" style="display: none">
                <td style="width: 140px;">Từ ngày
                </td>
                <td style="width: 220px;">
                    <asp:TextBox ID="txtTuNgay" runat="server" CssClass="datepicker"></asp:TextBox>
                </td>
                <td style="width: 120px;">Đến ngày
                </td>
                <td>
                    <asp:TextBox ID="txtDenNgay" runat="server" CssClass="datepicker"></asp:TextBox>
                </td>
            </tr>
            <tr class="bao_cao_row">
                <td style="width: 140px;">Tháng
                </td>
                <td style="width: 220px;">
                    <asp:DropDownList runat="server" ID="ddlThang">
                    </asp:DropDownList>
                </td>
                <td style="width: 120px;">Năm
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlNam"></asp:DropDownList>
                </td>
            </tr>
            <tr style="height: 50px">
                <td></td>
                <td colspan="3" style="vertical-align: bottom">
                    <asp:Button runat="server" Text="Đồng bộ" CssClass="save btn" OnClick="btnDongBo_Click" />
                </td>
            </tr>
        </table>
        <h3 style="padding-left: 0px; margin-top: 15px; border-top: 1px solid #dedede; margin-bottom: 15px; padding-top: 10px;">Lịch sử đồng bộ
        </h3>
        <table class="table" id="table">
            <tr>
                <th>STT</th>
                <th>Thời gian bắt đầu</th>
                <th>Dữ liệu đồng bộ</th>
                <th>Kết quả</th>
            </tr>
            <asp:Repeater ID="rptSyncHistory" runat="server" OnItemDataBound="rptSyncHistory_ItemDataBound">
                <ItemTemplate>
                    <tr>
                        <td>
                            <%=stt ++ %>
                        </td>
                        <td>
                            <asp:Label ID="lblSyncDate" runat="server"></asp:Label>
                        </td>
                        <td>
                            <%# Eval("Description") %>
                        </td>
                        <td>
                            <asp:Label ID="lblSuccess" runat="server"></asp:Label>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </div>

    <div class="confirmDiv" id="donThuDongBoDiv" style="width: 1000px; margin-left: -500px; top: 100px;">
        <label class="title">
            Dữ liệu đồng bộ
        </label>
        <div class="content-div" style="max-height: 500px; overflow: auto;">
            <table class="border table">
                <tr>
                    <th>
                        <input type="checkbox" id="checkAllDonThu" />
                    </th>
                    <th>STT</th>
                    <th>Mã đơn thư</th>
                    <th>Ngày nhập đơn</th>
                    <th>Tên chủ đơn</th>
                    <th>Nội dung</th>
                    <th>Loại đơn</th>
                    <th>Hướng xử lý</th>
                </tr>
                <asp:Repeater ID="rptDonThuDongBo" runat="server" OnItemDataBound="rptDonThuDongBo_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td class="check_td">
                                <asp:CheckBox ID="cbxCheck" runat="server" />
                                <asp:HiddenField ID="hdfXuLyDonID" runat="server" Value='<%# Eval("XuLyDonID") %>' />
                            </td>
                            <td>
                                <%# Container.ItemIndex + 1%>
                            </td>
                            <td>
                                <%# Eval("SoDonThu") %>
                            </td>
                            <td>
                                <asp:Label ID="lblNgayNhapDon" runat="server"></asp:Label>
                            </td>
                            <td>
                                <%# Eval("HoTen") %>
                            </td>
                            <td>
                                <%# Eval("NoiDungDon") %>
                            </td>
                            <td>
                                <%# Eval("TenLoaiKhieuTo") %>
                            </td>
                            <td>
                                <%# Eval("TenHuongGiaiQuyet") %>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <div style="text-align: center">
            <asp:Button ID="btnDongBoDonThu" runat="server" Text="Đồng bộ" CssClass="btn save" OnClick="btnSyncDonThu_Click" />
            <asp:Button ID="btnExportXML" runat="server" Text="Xuất XML" CssClass="btn save" OnClick="btnExportXML_Click" />
            <input type="button" class="btn btn-cancel" value="Đóng" onclick="hideDonThuDongBoDiv(); return false;" />
        </div>
    </div>

    <div class="confirmDiv" id="baoCao2aDiv" style="width: 1000px; margin-left: -500px; top: 100px;">
        <label class="title">
            Dữ liệu đồng bộ
        </label>
        <div class="content-div">
            <table class="border">
                <tr>
                    <td rowspan="4" style="width: 120px; font-weight: bold">Đơn vị
                    </td>
                    <td colspan="8" style="font-weight: bold">Tiếp thường xuyên
                    </td>
                    <td colspan="8" style="font-weight: bold">Tiếp định kỳ và đột xuất của Lãnh đạo
                    </td>
                    <td colspan="10" style="font-weight: bold">Nội dung tiếp công dân (số vụ việc)
                    </td>
                    <td colspan="4" style="font-weight: bold">Kết quả tiếp dân (số vụ việc)
                    </td>
                    <td rowspan="4" style="font-weight: bold">Ghi chú
                    </td>
                </tr>
                <tr>
                    <td rowspan="3" style="font-weight: bold">Lượt
                    </td>
                    <td rowspan="3" style="font-weight: bold">Người
                    </td>
                    <td colspan="2" style="font-weight: bold">Vụ việc
                    </td>
                    <td colspan="4" style="font-weight: bold">Đoàn đông người
                    </td>
                    <td rowspan="3" style="font-weight: bold">Lượt
                    </td>
                    <td rowspan="3" style="font-weight: bold">Người
                    </td>
                    <td colspan="2" style="font-weight: bold">Vụ việc
                    </td>
                    <td colspan="4" style="font-weight: bold">Đoàn đông người
                    </td>
                    <td colspan="6" style="font-weight: bold">Khiếu nại
                    </td>
                    <td colspan="3" style="font-weight: bold">Tố cáo
                    </td>
                    <td rowspan="3" style="font-weight: bold">Phản ảnh, kiến nghị khác
                    </td>
                    <td rowspan="3" style="font-weight: bold">Chưa được giải quyết
                    </td>
                    <td colspan="3" style="font-weight: bold">Đã được giải quyết
                    </td>
                </tr>
                <tr>
                    <td rowspan="2" style="font-weight: bold">Cũ
                    </td>
                    <td rowspan="2" style="font-weight: bold">Mới phát sinh
                    </td>
                    <td rowspan="2" style="font-weight: bold">Số đoàn
                    </td>
                    <td rowspan="2" style="font-weight: bold">Người
                    </td>
                    <td colspan="2" style="font-weight: bold">Vụ việc
                    </td>
                    <td rowspan="2" style="font-weight: bold">Cũ
                    </td>
                    <td rowspan="2" style="font-weight: bold">Mới phát sinh
                    </td>
                    <td rowspan="2" style="font-weight: bold">Số đoàn
                    </td>
                    <td rowspan="2" style="font-weight: bold">Người
                    </td>
                    <td colspan="2" style="font-weight: bold">Vụ việc
                    </td>
                    <td colspan="4" style="font-weight: bold">Lĩnh vực hành chính
                    </td>
                    <td rowspan="2" style="font-weight: bold">Lĩnh vực Tư pháp
                    </td>
                    <td rowspan="2" style="font-weight: bold">Lĩnh vực CT, VH, XH khác
                    </td>
                    <td rowspan="2" style="font-weight: bold">Lĩnh vực hành chính
                    </td>
                    <td rowspan="2" style="font-weight: bold">Lĩnh vực Tư pháp
                    </td>
                    <td rowspan="2" style="font-weight: bold">Tham nhũng
                    </td>
                    <td rowspan="2" style="font-weight: bold">Chưa có QĐ giải quyết
                    </td>
                    <td rowspan="2" style="font-weight: bold">Đã có QĐ giải quyết (lần 1,2, cuối cùng)
                    </td>
                    <td rowspan="2" style="font-weight: bold">Đã có bản án của tòa
                    </td>
                </tr>
                <tr>
                    <td style="font-weight: bold">Cũ
                    </td>
                    <td style="font-weight: bold">Mới phát sinh
                    </td>
                    <td style="font-weight: bold">Cũ
                    </td>
                    <td style="font-weight: bold">Mới phát sinh
                    </td>
                    <td style="font-weight: bold">Về tranh chấp, đòi đất cũ, đền bù, giải tỏa
                    </td>
                    <td style="font-weight: bold">Về chính sách
                    </td>
                    <td style="font-weight: bold">Về nhà, tài sản
                    </td>
                    <td style="font-weight: bold">Về chế độ CC, VC
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>1
                    </td>
                    <td>2
                    </td>
                    <td>3
                    </td>
                    <td>4
                    </td>
                    <td>5
                    </td>
                    <td>6
                    </td>
                    <td>7
                    </td>
                    <td>8
                    </td>
                    <td>9
                    </td>
                    <td>10
                    </td>
                    <td>11
                    </td>
                    <td>12
                    </td>
                    <td>13
                    </td>
                    <td>14
                    </td>
                    <td>15
                    </td>
                    <td>16
                    </td>
                    <td>17
                    </td>
                    <td>18
                    </td>
                    <td>19
                    </td>
                    <td>20
                    </td>
                    <td>21
                    </td>
                    <td>22
                    </td>
                    <td>23
                    </td>
                    <td>24
                    </td>
                    <td>25
                    </td>
                    <td>26
                    </td>
                    <td>27
                    </td>
                    <td>28
                    </td>
                    <td>29
                    </td>
                    <td>30
                    </td>
                    <td>31
                    </td>
                </tr>
                <asp:Repeater ID="rptReport2a" runat="server">
                    <ItemTemplate>
                        <tr class="report-row <%# Eval("CssClass") %>">
                            <td style="text-align: left">
                                <%# Eval("DonVi") %>
                            </td>
                            <td>
                                <%# Eval("Col1Data") %>
                            </td>
                            <td>
                                <%# Eval("Col2Data") %>
                            </td>
                            <td>
                                <%# Eval("Col3Data") %>
                            </td>
                            <td>
                                <%# Eval("Col4Data") %>
                            </td>
                            <td>
                                <%# Eval("Col5Data") %>
                            </td>
                            <td>
                                <%# Eval("Col6Data") %>
                            </td>
                            <td>
                                <%# Eval("Col7Data") %>
                            </td>
                            <td>
                                <%# Eval("Col8Data") %>
                            </td>
                            <td>
                                <%# Eval("Col9Data") %>
                            </td>
                            <td>
                                <%# Eval("Col10Data") %>
                            </td>
                            <td>
                                <%# Eval("Col11Data") %>
                            </td>
                            <td>
                                <%# Eval("Col12Data") %>
                            </td>
                            <td>
                                <%# Eval("Col13Data") %>
                            </td>
                            <td>
                                <%# Eval("Col14Data") %>
                            </td>
                            <td>
                                <%# Eval("Col15Data") %>
                            </td>
                            <td>
                                <%# Eval("Col16Data") %>
                            </td>
                            <td>
                                <%# Eval("Col17Data") %>
                            </td>
                            <td>
                                <%# Eval("Col18Data") %>
                            </td>
                            <td>
                                <%# Eval("Col19Data") %>
                            </td>
                            <td>
                                <%# Eval("Col20Data") %>
                            </td>
                            <td>
                                <%# Eval("Col21Data") %>
                            </td>
                            <td>
                                <%# Eval("Col22Data") %>
                            </td>
                            <td>
                                <%# Eval("Col23Data") %>
                            </td>
                            <td>
                                <%# Eval("Col24Data") %>
                            </td>
                            <td>
                                <%# Eval("Col25Data") %>
                            </td>
                            <td>
                                <%# Eval("Col26Data") %>
                            </td>
                            <td>
                                <%# Eval("Col27Data") %>
                            </td>
                            <td>
                                <%# Eval("Col28Data") %>
                            </td>
                            <td>
                                <%# Eval("Col29Data") %>
                            </td>
                            <td>
                                <%# Eval("Col30Data") %>
                            </td>
                            <td></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <div style="margin-top: 10px;">
                <span style="color: red">(*)</span> Lưu ý: Hãy kiểm tra kĩ dữ liệu trước khi đồng bộ. Dữ liệu sẽ được đồng bộ lên máy chủ TTCP khi bạn bấm nút "Đồng bộ".
            </div>
        </div>
        <div style="text-align: center">
            <asp:Button ID="btnSync2a" runat="server" Text="Đồng bộ" CssClass="btn save" OnClick="btnSync2a_Click" />
            <input type="button" class="btn btn-cancel" value="Đóng" onclick="hideBaoCao2aDiv(); return false;" />
        </div>
    </div>

    <div class="confirmDiv" id="baoCao2bDiv" style="width: 1000px; margin-left: -500px; top: 100px;">
        <label class="title">
            Dữ liệu đồng bộ
        </label>
        <div class="content-div">
            <table class="border">
                <tr>
                    <td rowspan="5" style="width: 120px; font-weight: bold">Đơn vị
                    </td>
                    <td colspan="6" style="font-weight: bold">Tiếp nhận
                    </td>
                    <td colspan="19" style="font-weight: bold">Phân loại đơn khiếu nại, tố cáo (số đơn)
                    </td>
                    <td rowspan="5" style="font-weight: bold">Đơn khác (kiến nghị, phản ánh, đơn nặc danh)
                    </td>
                    <td colspan="5" style="font-weight: bold">Kết quả xử lý đơn khiếu nại, tố cáo
                    </td>
                    <td rowspan="5" style="font-weight: bold">Ghi chú
                    </td>
                </tr>
                <tr>
                    <td rowspan="4" style="font-weight: bold">Tổng số đơn
                    </td>
                    <td rowspan="2" colspan="2" style="font-weight: bold">Đơn tiếp nhận trong kỳ
                    </td>
                    <td rowspan="2" colspan="2" style="font-weight: bold">Đơn kì trước chuyển sang
                    </td>
                    <td rowspan="4" style="font-weight: bold">Đơn đủ điều kiện xử lý
                    </td>
                    <td colspan="13" style="font-weight: bold">Theo nội dung
                    </td>
                    <td colspan="3" style="font-weight: bold">Theo thẩm quyền giải quyết
                    </td>
                    <td colspan="3" style="font-weight: bold">Theo trình tự giải quyết
                    </td>
                    <td rowspan="4" style="font-weight: bold">Số văn bản hướng dẫn
                    </td>
                    <td rowspan="4" style="font-weight: bold">Số đơn chuyển đến cơ quan có thẩm quyền
                    </td>
                    <td rowspan="4" style="font-weight: bold">Số công văn đôn đốc việc giải quyết
                    </td>
                    <td colspan="2" rowspan="2" style="font-weight: bold">Đơn thuộc thẩm quyền
                    </td>
                </tr>
                <tr>
                    <td colspan="7" style="font-weight: bold">Khiếu nại
                    </td>
                    <td colspan="6" style="font-weight: bold">Tố cáo
                    </td>
                    <td rowspan="3" style="font-weight: bold">Của các cơ quan hành chính các cấp
                    </td>
                    <td rowspan="3" style="font-weight: bold">Của cơ quan Tư pháp các cấp
                    </td>
                    <td rowspan="3" style="font-weight: bold">Của cơ quan Đảng
                    </td>
                    <td rowspan="3" style="font-weight: bold">Chưa được giải quyết
                    </td>
                    <td rowspan="3" style="font-weight: bold">Đã được giải quyết lần đầu
                    </td>
                    <td rowspan="3" style="font-weight: bold">Đã được giải quyết nhiều lần
                    </td>
                </tr>
                <tr>
                    <td rowspan="2" style="font-weight: bold">Đơn có nhiều người đứng tên
                    </td>
                    <td rowspan="2" style="font-weight: bold">Đơn có một người đứng tên
                    </td>
                    <td rowspan="2" style="font-weight: bold">Đơn có nhiều người đứng tên
                    </td>
                    <td rowspan="2" style="font-weight: bold">Đơn có một người đứng tên
                    </td>
                    <td colspan="5" style="font-weight: bold">Lĩnh vực hành chính
                    </td>
                    <td rowspan="2" style="font-weight: bold">Lĩnh vực Tư pháp
                    </td>
                    <td rowspan="2" style="font-weight: bold">Về Đảng
                    </td>
                    <td rowspan="2" style="font-weight: bold">Tổng
                    </td>
                    <td rowspan="2" style="font-weight: bold">Lĩnh vực hành chính
                    </td>
                    <td rowspan="2" style="font-weight: bold">Lĩnh vực Tư pháp
                    </td>
                    <td rowspan="2" style="font-weight: bold">Tham nhũng
                    </td>
                    <td rowspan="2" style="font-weight: bold">Về Đảng
                    </td>
                    <td rowspan="2" style="font-weight: bold">Lĩnh vực khác
                    </td>
                    <td rowspan="2" style="font-weight: bold">Khiếu nại
                    </td>
                    <td rowspan="2" style="font-weight: bold">Tố cáo
                    </td>
                </tr>
                <tr>
                    <td style="font-weight: bold">Tổng</td>
                    <td style="font-weight: bold">Liên quan đến đất đai</td>
                    <td style="font-weight: bold">Về nhà, tài sản</td>
                    <td style="font-weight: bold">Về chính sách, chế độ CC, VC</td>
                    <td style="font-weight: bold">Lĩnh vực CT, VH, XH khác
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>1 = 2 + 3 + 4 + 5
                    </td>
                    <td>2
                    </td>
                    <td>3
                    </td>
                    <td>4
                    </td>
                    <td>5
                    </td>
                    <td>6
                    </td>
                    <td>7 = 8 + 9 + 10 + 11
                    </td>
                    <td>8
                    </td>
                    <td>9
                    </td>
                    <td>10
                    </td>
                    <td>11
                    </td>
                    <td>12
                    </td>
                    <td>13
                    </td>
                    <td>14 = 15 + 16 + 17 + 18 +19
                    </td>
                    <td>15
                    </td>
                    <td>16
                    </td>
                    <td>17
                    </td>
                    <td>18
                    </td>
                    <td>19
                    </td>
                    <td>20
                    </td>
                    <td>21
                    </td>
                    <td>22
                    </td>
                    <td>23
                    </td>
                    <td>24
                    </td>
                    <td>25
                    </td>
                    <td>26
                    </td>
                    <td>27
                    </td>
                    <td>28
                    </td>
                    <td>29
                    </td>
                    <td>30
                    </td>
                    <td>31
                    </td>
                    <td>32
                    </td>
                </tr>
                <asp:Repeater ID="rptReport2b" runat="server">
                    <ItemTemplate>
                        <tr class='report-row <%# Eval("CssClass") %>'>
                            <td style="text-align: left">
                                <%# Eval("DonVi") %>
                            </td>
                            <td>
                                <%# Eval("Col1Data") %>
                            </td>
                            <td>
                                <%# Eval("Col2Data") %>
                            </td>
                            <td>
                                <%# Eval("Col3Data") %>
                            </td>
                            <td>
                                <%# Eval("Col4Data") %>
                            </td>
                            <td>
                                <%# Eval("Col5Data") %>
                            </td>
                            <td>
                                <%# Eval("Col6Data") %>
                            </td>
                            <td>
                                <%# Eval("Col7Data") %>
                            </td>
                            <td>
                                <%# Eval("Col8Data") %>
                            </td>
                            <td>
                                <%# Eval("Col9Data") %>
                            </td>
                            <td>
                                <%# Eval("Col10Data") %>
                            </td>
                            <td>
                                <%# Eval("Col11Data") %>
                            </td>
                            <td>
                                <%# Eval("Col12Data") %>
                            </td>
                            <td>
                                <%# Eval("Col13Data") %>
                            </td>
                            <td>
                                <%# Eval("Col14Data") %>
                            </td>
                            <td>
                                <%# Eval("Col15Data") %>
                            </td>
                            <td>
                                <%# Eval("Col16Data") %>
                            </td>
                            <td>
                                <%# Eval("Col17Data") %>
                            </td>
                            <td>
                                <%# Eval("Col18Data") %>
                            </td>
                            <td>
                                <%# Eval("Col19Data") %>
                            </td>
                            <td>
                                <%# Eval("Col20Data") %>
                            </td>
                            <td>
                                <%# Eval("Col21Data") %>
                            </td>
                            <td>
                                <%# Eval("Col22Data") %>
                            </td>
                            <td>
                                <%# Eval("Col23Data") %>
                            </td>
                            <td>
                                <%# Eval("Col24Data") %>
                            </td>
                            <td>
                                <%# Eval("Col25Data") %>
                            </td>
                            <td>
                                <%# Eval("Col26Data") %>
                            </td>
                            <td>
                                <%# Eval("Col27Data") %>
                            </td>
                            <td>
                                <%# Eval("Col28Data") %>
                            </td>
                            <td>
                                <%# Eval("Col29Data") %>
                            </td>
                            <td>
                                <%# Eval("Col30Data") %>
                            </td>
                            <td>
                                <%# Eval("Col31Data") %>
                            </td>
                            <td></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <div style="margin-top: 10px;">
                <span style="color: red">(*)</span> Lưu ý: Hãy kiểm tra kĩ dữ liệu trước khi đồng bộ. Dữ liệu sẽ được đồng bộ lên máy chủ TTCP khi bạn bấm nút "Đồng bộ".
            </div>
        </div>
        <div style="text-align: center">
            <asp:Button ID="Button1" runat="server" Text="Đồng bộ" CssClass="btn save" OnClick="btnSync2b_Click" />
            <input type="button" class="btn btn-cancel" value="Đóng" onclick="hideBaoCao2bDiv(); return false;" />
        </div>
    </div>

    <div class="confirmDiv" id="baoCao2cDiv" style="width: 1000px; margin-left: -500px; top: 100px;">
        <label class="title">
            Dữ liệu đồng bộ
        </label>
        <div class="content-div">
            <table class="border">
                <tr>
                    <td rowspan="4" style="width: 120px; font-weight: bold">Đơn vị
                    </td>
                    <td colspan="4" style="font-weight: bold">Đơn khiếu nại thuộc thẩm quyền
                    </td>
                    <td colspan="21" style="font-weight: bold">Kết quả giải quyết
                    </td>
                    <td rowspan="2" colspan="2" style="font-weight: bold">Chấp hành thời gian giải quyết theo quy định
                    </td>
                    <td colspan="10" style="font-weight: bold">Việc thi hành quyết định giải quyết khiếu nại
                    </td>
                    <td rowspan="4" style="font-weight: bold">Ghi chú
                    </td>
                </tr>
                <tr>
                    <td rowspan="3" style="font-weight: bold">Tổng số đơn khiếu nại
                    </td>
                    <td rowspan="2" colspan="3" style="font-weight: bold">Trong đó
                    </td>
                    <td rowspan="2" colspan="4" style="font-weight: bold">Đã giải quyết
                    </td>
                    <td colspan="6" style="font-weight: bold">Phân tích kết quả (vụ việc)
                    </td>
                    <td colspan="2" rowspan="2" style="font-weight: bold">Kiến nghị thu hồi cho nhà nước
                    </td>
                    <td colspan="2" rowspan="2" style="font-weight: bold">Trả lại cho công dân
                    </td>
                    <td rowspan="3" style="font-weight: bold">Số người được trả lại quyền lợi
                    </td>
                    <td rowspan="2" colspan="2" style="font-weight: bold">Kiến nghị xử lý hành chính
                    </td>
                    <td colspan="4" style="font-weight: bold">Chuyển cơ quan điều tra, khởi tố
                    </td>
                    <td rowspan="3" style="font-weight: bold">Tổng số quyết định phải tổ chức thực hiện trong kỳ báo cáo
                    </td>
                    <td rowspan="3" style="font-weight: bold">Đã thực hiện
                    </td>
                    <td colspan="4" style="font-weight: bold">Thu hồi cho nhà nước
                    </td>
                    <td colspan="4" style="font-weight: bold">Trả lại cho công dân
                    </td>
                </tr>
                <tr>
                    <td rowspan="2" style="font-weight: bold">Khiếu nại đúng
                    </td>
                    <td rowspan="2" style="font-weight: bold">Khiếu nại sai
                    </td>
                    <td rowspan="2" style="font-weight: bold">Khiếu nại đúng 1 phần
                    </td>
                    <td rowspan="2" style="font-weight: bold">Giải quyết lần 1
                    </td>
                    <td colspan="2" style="font-weight: bold">Giải quyết lần 2
                    </td>
                    <td rowspan="2" style="font-weight: bold">Số vụ
                    </td>
                    <td rowspan="2" style="font-weight: bold">Số đối tượng
                    </td>
                    <td colspan="2" style="font-weight: bold">Kết quả
                    </td>
                    <td rowspan="2" style="font-weight: bold">Số vụ việc giải quyết đúng thời hạn
                    </td>
                    <td rowspan="2" style="font-weight: bold">Số vụ việc giải quyết quá thời hạn
                    </td>
                    <td colspan="2" style="font-weight: bold">Phải thu
                    </td>
                    <td colspan="2" style="font-weight: bold">Đã thu
                    </td>
                    <td colspan="2" style="font-weight: bold">Phải trả
                    </td>
                    <td colspan="2" style="font-weight: bold">Đã trả
                    </td>
                </tr>
                <tr>
                    <td style="font-weight: bold">Đơn nhận trong kỳ báo cáo</td>
                    <td style="font-weight: bold">Đơn tồn kỳ trước chuyển sang</td>
                    <td style="font-weight: bold">Tổng số vụ việc</td>
                    <td style="font-weight: bold">Số đơn thuộc thẩm quyền</td>
                    <td style="font-weight: bold">Số vụ việc thuộc thẩm quyền</td>
                    <td style="font-weight: bold">Số vụ việc giải quyết bằng QĐ hành chính</td>
                    <td style="font-weight: bold">Số vụ việc rút đơn thông qua giải thích, thuyết phục</td>
                    <td style="font-weight: bold">Công nhận quyết định lần 1</td>
                    <td style="font-weight: bold">Hủy, sửa quyết định lần 1</td>
                    <td style="font-weight: bold">Tiền (Tr đ)</td>
                    <td style="font-weight: bold">Đất (m2)</td>
                    <td style="font-weight: bold">Tiền (Tr đ)</td>
                    <td style="font-weight: bold">Đất (m2)</td>
                    <td style="font-weight: bold">Tổng số người</td>
                    <td style="font-weight: bold">Số người đã bị xử lý</td>
                    <td style="font-weight: bold">Số vụ đã khởi tố</td>
                    <td style="font-weight: bold">Số đối tượng đã khởi tố</td>
                    <td style="font-weight: bold">Tiền (Tr đ)</td>
                    <td style="font-weight: bold">Đất (m2)</td>
                    <td style="font-weight: bold">Tiền (Tr đ)</td>
                    <td style="font-weight: bold">Đất (m2)</td>
                    <td style="font-weight: bold">Tiền (Tr đ)</td>
                    <td style="font-weight: bold">Đất (m2)</td>
                    <td style="font-weight: bold">Tiền (Tr đ)</td>
                    <td style="font-weight: bold">Đất (m2)</td>
                </tr>
                <tr>
                    <td></td>
                    <td>1 = 2 + 3
                    </td>
                    <td>2
                    </td>
                    <td>3
                    </td>
                    <td>4
                    </td>
                    <td>5
                    </td>
                    <td>6
                    </td>
                    <td>7
                    </td>
                    <td>8
                    </td>
                    <td>9
                    </td>
                    <td>10
                    </td>
                    <td>11
                    </td>
                    <td>12
                    </td>
                    <td>13
                    </td>
                    <td>14
                    </td>
                    <td>15
                    </td>
                    <td>16
                    </td>
                    <td>17
                    </td>
                    <td>18
                    </td>
                    <td>19
                    </td>
                    <td>20
                    </td>
                    <td>21
                    </td>
                    <td>22
                    </td>
                    <td>23
                    </td>
                    <td>24
                    </td>
                    <td>25
                    </td>
                    <td>26
                    </td>
                    <td>27
                    </td>
                    <td>28
                    </td>
                    <td>29
                    </td>
                    <td>30
                    </td>
                    <td>31
                    </td>
                    <td>32
                    </td>
                    <td>33
                    </td>
                    <td>34
                    </td>
                    <td>35
                    </td>
                    <td>36
                    </td>
                    <td>37
                    </td>
                    <td>38
                    </td>
                </tr>
                <asp:Repeater ID="rptReport2c" runat="server">
                    <ItemTemplate>
                        <tr class='report-row <%# Eval("CssClass") %>'>
                            <td style="text-align: left">
                                <%# Eval("DonVi") %>
                            </td>
                            <td>
                                <%# Eval("Col1Data") %>
                            </td>
                            <td>
                                <%# Eval("Col2Data") %>
                            </td>
                            <td>
                                <%# Eval("Col3Data") %>
                            </td>
                            <td>
                                <%# Eval("Col4Data") %>
                            </td>
                            <td>
                                <%# Eval("Col5Data") %>
                            </td>
                            <td>
                                <%# Eval("Col6Data") %>
                            </td>
                            <td>
                                <%# Eval("Col7Data") %>
                            </td>
                            <td>
                                <%# Eval("Col8Data") %>
                            </td>
                            <td>
                                <%# Eval("Col9Data") %>
                            </td>
                            <td>
                                <%# Eval("Col10Data") %>
                            </td>
                            <td>
                                <%# Eval("Col10Data") %>
                            </td>
                            <td>
                                <%# Eval("Col12Data") %>
                            </td>
                            <td>
                                <%# Eval("Col13Data") %>
                            </td>
                            <td>
                                <%# Eval("Col14Data") %>
                            </td>
                            <td>
                                <%# Eval("Col15Data") %>
                            </td>
                            <td>
                                <%# Eval("Col16Data") %>
                            </td>
                            <td>
                                <%# Eval("Col17Data") %>
                            </td>
                            <td>
                                <%# Eval("Col18Data") %>
                            </td>
                            <td>
                                <%# Eval("Col19Data") %>
                            </td>
                            <td>
                                <%# Eval("Col20Data") %>
                            </td>
                            <td>
                                <%# Eval("Col21Data") %>
                            </td>
                            <td>
                                <%# Eval("Col22Data") %>
                            </td>
                            <td>
                                <%# Eval("Col23Data") %>
                            </td>
                            <td>
                                <%# Eval("Col24Data") %>
                            </td>
                            <td>
                                <%# Eval("Col25Data") %>
                            </td>
                            <td>
                                <%# Eval("Col26Data") %>
                            </td>
                            <td>
                                <%# Eval("Col27Data") %>
                            </td>
                            <td>
                                <%# Eval("Col28Data") %>
                            </td>
                            <td>
                                <%# Eval("Col29Data") %>
                            </td>
                            <td>
                                <%# Eval("Col30Data") %>
                            </td>
                            <td>
                                <%# Eval("Col31Data") %>
                            </td>
                            <td>
                                <%# Eval("Col32Data") %>
                            </td>
                            <td>
                                <%# Eval("Col33Data") %>
                            </td>
                            <td>
                                <%# Eval("Col34Data") %>
                            </td>
                            <td>
                                <%# Eval("Col35Data") %>
                            </td>
                            <td>
                                <%# Eval("Col36Data") %>
                            </td>
                            <td>
                                <%# Eval("Col37Data") %>
                            </td>
                            <td></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <div style="margin-top: 10px;">
                <span style="color: red">(*)</span> Lưu ý: Hãy kiểm tra kĩ dữ liệu trước khi đồng bộ. Dữ liệu sẽ được đồng bộ lên máy chủ TTCP khi bạn bấm nút "Đồng bộ".
            </div>
        </div>
        <div style="text-align: center">
            <asp:Button ID="Button2" runat="server" Text="Đồng bộ" CssClass="btn save" OnClick="btnSync2c_Click" />
            <input type="button" class="btn btn-cancel" value="Đóng" onclick="hideBaoCao2cDiv(); return false;" />
        </div>
    </div>

    <div class="confirmDiv" id="baoCao2dDiv" style="width: 1000px; margin-left: -500px; top: 100px;">
        <label class="title">
            Dữ liệu đồng bộ
        </label>
        <div class="content-div">
            <table class="border">
                <tr>
                    <td rowspan="4" style="width: 120px; font-weight: bold">Đơn vị
                    </td>
                    <td colspan="4" style="font-weight: bold">Đơn tố cáo thuộc thẩm quyền
                    </td>
                    <td colspan="16" style="font-weight: bold">Kết quả giải quyết
                    </td>
                    <td rowspan="3" colspan="2" style="font-weight: bold">Chấp hành thời gian giải quyết theo quy định
                    </td>
                    <td colspan="10" style="font-weight: bold">Việc thi hành quyết định xử lý tố cáo
                    </td>
                    <td rowspan="4" style="font-weight: bold">Ghi chú
                    </td>
                </tr>
                <tr>
                    <td rowspan="3" style="font-weight: bold">Tổng số đơn
                    </td>
                    <td rowspan="2" colspan="3" style="font-weight: bold">Trong đó
                    </td>
                    <td rowspan="2" colspan="2" style="font-weight: bold">Đã giải quyết
                    </td>
                    <td rowspan="2" colspan="3" style="font-weight: bold">Phân tích kết quả (vụ việc)
                    </td>
                    <td rowspan="2" colspan="2" style="font-weight: bold">Kiến nghị thu hồi cho Nhà nước
                    </td>
                    <td rowspan="2" colspan="2" style="font-weight: bold">Trả lại cho công dân
                    </td>
                    <td rowspan="3" style="font-weight: bold">Số người được bảo vệ quyền lợi
                    </td>
                    <td colspan="2" rowspan="2" style="font-weight: bold">Kiến nghị xử lý hành chính
                    </td>
                    <td colspan="4" style="font-weight: bold">Chuyển cơ quan điều tra, khởi tố
                    </td>
                    <td rowspan="3" style="font-weight: bold">Tổng số quyết định phải tổ chức thực hiện trong kỳ báo cáo
                    </td>
                    <td rowspan="3" style="font-weight: bold">Đã thực hiện xong
                    </td>
                    <td colspan="4" style="font-weight: bold">Thu hồi cho Nhà nước
                    </td>
                    <td colspan="4" style="font-weight: bold">Trả lại cho công dân
                    </td>
                </tr>
                <tr>
                    <td rowspan="2" style="font-weight: bold">Số vụ</td>
                    <td rowspan="2" style="font-weight: bold">Số đối tượng bị khởi tố</td>
                    <td colspan="2" style="font-weight: bold">Kết quả</td>
                    <td colspan="2" style="font-weight: bold">Phải thu</td>
                    <td colspan="2" style="font-weight: bold">Đã thu</td>
                    <td colspan="2" style="font-weight: bold">Phải trả</td>
                    <td colspan="2" style="font-weight: bold">Đã trả</td>
                </tr>
                <tr>
                    <td style="font-weight: bold">Đơn nhận trong kỳ báo cáo</td>
                    <td style="font-weight: bold">Đơn tồn kỳ trước chuyển sang</td>
                    <td style="font-weight: bold">Tổng số vụ việc</td>
                    <td style="font-weight: bold">Số đơn thuộc thẩm quyền</td>
                    <td style="font-weight: bold">Số vụ việc thuộc thẩm quyền</td>
                    <td style="font-weight: bold">Tố cáo đúng</td>
                    <td style="font-weight: bold">Tố cáo sai</td>
                    <td style="font-weight: bold">Tố cáo đúng 1 phần</td>
                    <td style="font-weight: bold">Tiền (Tr đ)</td>
                    <td style="font-weight: bold">Đất (m2)</td>
                    <td style="font-weight: bold">Tiền (Tr đ)</td>
                    <td style="font-weight: bold">Đất (m2)</td>
                    <td style="font-weight: bold">Tổng số người</td>
                    <td style="font-weight: bold">Số người đã bị xử lý</td>
                    <td style="font-weight: bold">Số vụ đã khởi tố</td>
                    <td style="font-weight: bold">Số đối tượng đã khởi tố</td>
                    <td style="font-weight: bold">Số vụ việc giải quyết đúng thời hạn</td>
                    <td style="font-weight: bold">Số vụ việc giải quyết quá thời hạn</td>
                    <td style="font-weight: bold">Tiền (Tr đ)</td>
                    <td style="font-weight: bold">Đất (m2)</td>
                    <td style="font-weight: bold">Tiền (Tr đ)</td>
                    <td style="font-weight: bold">Đất (m2)</td>
                    <td style="font-weight: bold">Tiền (Tr đ)</td>
                    <td style="font-weight: bold">Đất (m2)</td>
                    <td style="font-weight: bold">Tiền (Tr đ)</td>
                    <td style="font-weight: bold">Đất (m2)</td>
                </tr>
                <tr>
                    <td></td>
                    <td>1 = 2 + 3
                    </td>
                    <td>2
                    </td>
                    <td>3
                    </td>
                    <td>4
                    </td>
                    <td>5
                    </td>
                    <td>6
                    </td>
                    <td>7
                    </td>
                    <td>8
                    </td>
                    <td>9
                    </td>
                    <td>10
                    </td>
                    <td>11
                    </td>
                    <td>12
                    </td>
                    <td>13
                    </td>
                    <td>14
                    </td>
                    <td>15
                    </td>
                    <td>16
                    </td>
                    <td>17
                    </td>
                    <td>18
                    </td>
                    <td>19
                    </td>
                    <td>20
                    </td>
                    <td>21
                    </td>
                    <td>22
                    </td>
                    <td>23
                    </td>
                    <td>24
                    </td>
                    <td>25
                    </td>
                    <td>26
                    </td>
                    <td>27
                    </td>
                    <td>28
                    </td>
                    <td>29
                    </td>
                    <td>30
                    </td>
                    <td>31
                    </td>
                    <td>32
                    </td>
                    <td>33
                    </td>
                </tr>
                <asp:Repeater ID="rptReport2d" runat="server">
                    <ItemTemplate>
                        <tr class='report-row <%# Eval("CssClass") %>'>
                            <td style="text-align: left">
                                <%# Eval("DonVi") %>
                            </td>
                            <td>
                                <%# Eval("Col1Data") %>
                            </td>
                            <td>
                                <%# Eval("Col2Data") %>
                            </td>
                            <td>
                                <%# Eval("Col3Data") %>
                            </td>
                            <td>
                                <%# Eval("Col4Data") %>
                            </td>
                            <td>
                                <%# Eval("Col5Data") %>
                            </td>
                            <td>
                                <%# Eval("Col6Data") %>
                            </td>
                            <td>
                                <%# Eval("Col7Data") %>
                            </td>
                            <td>
                                <%# Eval("Col8Data") %>
                            </td>
                            <td>
                                <%# Eval("Col9Data") %>
                            </td>
                            <td>
                                <%# Eval("Col10Data") %>
                            </td>
                            <td>
                                <%# Eval("Col10Data") %>
                            </td>
                            <td>
                                <%# Eval("Col12Data") %>
                            </td>
                            <td>
                                <%# Eval("Col13Data") %>
                            </td>
                            <td>
                                <%# Eval("Col14Data") %>
                            </td>
                            <td>
                                <%# Eval("Col15Data") %>
                            </td>
                            <td>
                                <%# Eval("Col16Data") %>
                            </td>
                            <td>
                                <%# Eval("Col17Data") %>
                            </td>
                            <td>
                                <%# Eval("Col18Data") %>
                            </td>
                            <td>
                                <%# Eval("Col19Data") %>
                            </td>
                            <td>
                                <%# Eval("Col20Data") %>
                            </td>
                            <td>
                                <%# Eval("Col21Data") %>
                            </td>
                            <td>
                                <%# Eval("Col22Data") %>
                            </td>
                            <td>
                                <%# Eval("Col23Data") %>
                            </td>
                            <td>
                                <%# Eval("Col24Data") %>
                            </td>
                            <td>
                                <%# Eval("Col25Data") %>
                            </td>
                            <td>
                                <%# Eval("Col26Data") %>
                            </td>
                            <td>
                                <%# Eval("Col27Data") %>
                            </td>
                            <td>
                                <%# Eval("Col28Data") %>
                            </td>
                            <td>
                                <%# Eval("Col29Data") %>
                            </td>
                            <td>
                                <%# Eval("Col30Data") %>
                            </td>
                            <td>
                                <%# Eval("Col31Data") %>
                            </td>
                            <td>
                                <%# Eval("Col32Data") %>
                            </td>
                            <td></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <div style="margin-top: 10px;">
                <span style="color: red">(*)</span> Lưu ý: Hãy kiểm tra kĩ dữ liệu trước khi đồng bộ. Dữ liệu sẽ được đồng bộ lên máy chủ TTCP khi bạn bấm nút "Đồng bộ".
            </div>
        </div>
        <div style="text-align: center">
            <asp:Button ID="Button3" runat="server" Text="Đồng bộ" CssClass="btn save" OnClick="btnSync2d_Click" />
            <input type="button" class="btn btn-cancel" value="Đóng" onclick="hideBaoCao2dDiv(); return false;" />
        </div>
    </div>
</asp:Content>
