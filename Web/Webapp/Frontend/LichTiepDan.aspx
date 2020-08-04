<%@ Page Title="" Language="C#" MasterPageFile="~/Frontend.Master" AutoEventWireup="true" CodeBehind="LichTiepDan.aspx.cs" Inherits="Com.Gosol.CMS.Web.Webapp.Frontend.LichTiepDan" %>

<%@ Register Src="~/Webapp/Frontend/SideBarTinNoiBat.ascx" TagPrefix="uc1" TagName="SideBarTinNoiBat" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <style>
        .control-label {
            text-align: left !important;
        }

        .control-label {
            text-align: left !important;
            white-space: nowrap !important;
        }
    </style>

    <div class="col-md-9">
        <div style="padding-top: 20px;">
            <asp:HiddenField ID="hdfCoQuanID" runat="server" />
            <asp:HiddenField ID="hdfNgayTiep" runat="server" />
            <div class="form-horizontal">
                <div class="form-group">
                    <label class="col-lg-2 col-md-2 col-sm-2 col-xs-2 control-label">Cơ quan tiếp</label>
                    <div class="col-lg-10 col-md-10 col-sm-12 col-xs-12">
                        <asp:DropDownList ID="ddlCoQuanTiep" runat="server" DataTextField="TenCoQuan" CssClass="form-control select2" DataValueField="CoQuanID" Style="width: 100%"></asp:DropDownList>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-lg-2 col-md-2 col-sm-2 col-xs-2 control-label">Thời gian tiếp</label>
                    <div class="col-lg-10 col-md-10 col-sm-12 col-xs-12">
                        <select id="ddlThangTiep" style="width: 200px; padding-left: 10px; height: 30px">
                            <option value="1">Tháng 1</option>
                            <option value="2">Tháng 2</option>
                            <option value="3">Tháng 3</option>
                            <option value="4">Tháng 4</option>
                            <option value="5">Tháng 5</option>
                            <option value="6">Tháng 6</option>
                            <option value="7">Tháng 7</option>
                            <option value="8">Tháng 8</option>
                            <option value="9">Tháng 9</option>
                            <option value="10">Tháng 10</option>
                            <option value="11">Tháng 11</option>
                            <option value="12">Tháng 12</option>
                        </select>
                        <select id="ddlNamTiep" style="width: 100px; padding-left: 10px; height: 30px">
                        </select>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-lg-2 col-md-2 col-sm-2 col-xs-2 control-label"></label>
                    <div class="col-lg-10 col-md-10 col-sm-12 col-xs-12">
                        <button class="btn btn-primary" onclick="TraCuuLich();return false;" id="btnTraCuu">Tra cứu</button>
                        <%--<asp:Button ID="btnSearch" runat="server" CausesValidation="false" CssClass="btn btn-sm btn-primary" OnClick="btnSearch_Click" Text="Tra cứu" style="width: 100px; height: 35px; font-size: 13pt"/>--%>
                    </div>
                </div>
            </div>
        </div>
        <div class="box box-primary">
            <%--<div class="box-header">
                <i class="glyphicon glyphicon-list"></i>
                <span class="box-title">LỊCH TIẾP CÔNG DÂN</span>
            </div>--%>
            <div class="box-body">
                <%--<hr style="margin-top: 0px; margin-bottom: 0px;" />
                <asp:ScriptManager ID="ScriptManager1" runat="server" />
                <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="table-responsive">
                            <table id="table" class="table table-bordered table-hover table-responsive">
                                <thead id="">
                                    <tr>
                                        <th style="width: 5%; text-align: center">STT</th>
                                        <th style="width: 10%; text-align: center">Ngày tiếp</th>
                                        <th style="width: 20%; text-align: center">Cơ quan</th>
                                        <th style="width: 20%; text-align: center">Lãnh đạo tiếp dân</th>
                                        <th style="width: 45%; text-align: center">Nội dung tiếp dân</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="rptLichTiepDan" runat="server" OnItemDataBound="rptLichTiepDan_ItemDataBound">
                                        <ItemTemplate>
                                            <tr>
                                                <td style="text-align: center">
                                                    <asp:Label runat="server" ID="lblStt"></asp:Label></td>
                                                <td style="text-align: center">
                                                    <div id="ngayTiep" runat="server">
                                                        <asp:Label ID="lblNgayTiep" runat="server"></asp:Label>
                                                    </div>
                                                </td>
                                                <td><%# Eval("CoQuanTiep") %></td>
                                                <td><%# Eval("CanBoTiep") %></td>
                                                <td>
                                                    <%# Eval("NDTiep") %>
                                                    <div style="text-align: justify;font-size:16px;">
                                                    Cán bộ <b><%# Eval("CanBoTiep") %></b> tiếp dân về vấn đề <b>
                                                                    <div id="summary<%# Container.ItemIndex + 1 %>" style="cursor: pointer" onclick="showDetail(<%# Container.ItemIndex + 1 %>)">
                                                                        <%#Eval("NDTiep").ToString().Length>=100?Eval("NDTiep").ToString().Substring(0,100) + " ... .":Eval("NDTiep").ToString() %>
                                                                    </div>
                                                                </b>
                                                                <input id="detail<%# Container.ItemIndex + 1 %>" style="display: none" value="<%#Eval("NDTiep") %>" />
                                                    <asp:Label ID="lblNoiDungTiep" runat="server"></asp:Label>
                                                    <asp:HiddenField runat="server" ID="hdfIDLichTiepDan" />
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
                            <div class="clearfix"></div>
                            <div id="messageThongBao" style="display: none;">
                                <div style="width: 100%; padding: 50px 0px 0px 100px;">
                                    <span style="font-size: 23px; font-family: 'Times New Roman'; color: red;">Không có lịch tiếp dân trong khoảng thời gian này !</span>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>--%>
                <table class="calendar-table" id="calendar-table">
                    <thead>
                        <tr>
                            <th style="width: calc(100% / 7)">HAI</th>
                            <th style="width: calc(100% / 7)">BA</th>
                            <th style="width: calc(100% / 7)">TƯ</th>
                            <th style="width: calc(100% / 7)">NĂM</th>
                            <th style="width: calc(100% / 7)">SÁU</th>
                            <th style="width: calc(100% / 7)">BẢY</th>
                            <th style="width: calc(100% / 7)">CHỦ NHẬT</th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
        </div>
    </div>


    <div class="col-md-3">
        <uc1:SideBarTinNoiBat runat="server" ID="SideBarTinNoiBat" />
    </div>

    <%-- Template --%>
    <table id="tableCalendarTemplate" style="display: none">
        <tr>
            <td>_DAY0_
                <div class="container-content" id="day_DAY0_">
                </div>
            </td>
            <td>_DAY1_
                <div class="container-content" id="day_DAY1_">
                </div>
            </td>
            <td>_DAY2_
                <div class="container-content" id="day_DAY2_">
                </div>
            </td>
            <td>_DAY3_
                <div class="container-content" id="day_DAY3_">
                </div>
            </td>
            <td>_DAY4_
                <div class="container-content" id="day_DAY4_">
                </div>
            </td>
            <td>_DAY5_
                <div class="container-content" id="day_DAY5_">
                </div>
            </td>
            <td>_DAY6_
                <div class="container-content" id="day_DAY6_">
                </div>
            </td>
        </tr>
    </table>

    <div id="contentTemplate" style="display: none">
        <div class="content-lichtiepdan" id="_IDCONTENT_">
            <a data-toggle="popover" data-trigger="focus">
                <div>
                    _CONTENT_
                </div>
            </a>
        </div>
    </div>

    <div id="popverTemplate" style="display: none">
        <div style="width: 350px">
            <div style="padding: 5px">
                <img alt="title" src="../../images/frontend/blueicon.png" style="width: 20px" />
                <b>_NOIDUNGTIEP_</b>
            </div>
            <div style="padding: 5px">
                <img alt="title" src="../../images/frontend/cqicon.png" />&ensp;
                Cơ quan tiếp: _COQUANTIEP_
            </div>
            <div style="padding: 5px">
                <img alt="title" src="../../images/frontend/usericon.png" />&ensp;
                Cán bộ tiếp: _CANBOTIEP_
            </div>
        </div>
    </div>

    <%-- End Template --%>

    <link href="/AdminLte/jquery.formvalidation/css/formValidation.min.css" rel="stylesheet" />
    <link href="/AdminLte/plugins/select2/select2.min.css" rel="stylesheet" type="text/css" />
    <script src="/AdminLte/jquery.formvalidation/js/formValidation.min.js"></script>
    <script src="/AdminLte/jquery.formvalidation/js/framework/bootstrap.min.js"></script>
    <script type="text/javascript" src="/AdminLte/plugins/select2/select2.full.min.js"></script>

    <style type="text/css">
        .calendar-table {
            width: 100%;
            margin-top: 20px;
        }

            .calendar-table thead > tr:first-child {
                height: 30px;
            }

            .calendar-table tbody > tr {
                height: 200px;
            }

            .calendar-table th {
                font-weight: normal;
                text-align: center;
                border: 1px solid #ccc;
                border-bottom: none;
            }

            .calendar-table td {
                text-align: center;
                border: 1px solid #ccc;
                border-top: none;
                vertical-align: top;
                font-weight: bold;
                padding: 5px;
            }

        .content-lichtiepdan:not(:first-child) {
            font-weight: normal;
            margin-top: 5px;
            padding: 5px;
            background: #e6e6e6;
            width: 100%;
            text-align: left;
            cursor: pointer;
        }

        .content-lichtiepdan:first-child {
            font-weight: normal;
            padding: 5px;
            background: #e6e6e6;
            width: 100%;
            text-align: left;
            cursor: pointer;
        }

        .container-content {
            max-height: 160px;
            overflow-y: auto;
        }

            .container-content::-webkit-scrollbar {
                width: 5px;
            }

            .container-content::-webkit-scrollbar-thumb {
                background: #ccc;
                border-radius: 12px;
            }

                .container-content::-webkit-scrollbar-thumb:hover {
                    background: #999;
                }

        .popover {
            max-width: 100%;
        }

        .select2-container--default .select2-selection--single {
            padding: 4px 5px;
        }
    </style>

    <script>
        var today = new Date();
        var thisMonth = today.getMonth() + 1;
        var thisYear = today.getFullYear();
        $(document).ready(function () {
            var ddlstr = `<option value=${thisYear - 1}>${thisYear - 1}</option>`;
            ddlstr += `<option value=${thisYear}>${thisYear}</option>`;
            ddlstr += `<option value=${thisYear + 1}>${thisYear + 1}</option>`;
            $("#ddlNamTiep").html(ddlstr);

            $(".select2").select2();

            $("#ddlThangTiep").val(thisMonth)
            $("#ddlNamTiep").val(thisYear)

            $('[data-toggle="popover"]').popover({
                html: true,
                placement: "right"
            });

            $("#btnTraCuu").click();
        });

        function changeCoQuan() {
            var canBoID = $("#MainContent_ddlCoQuanTiep").val();
            $("#MainContent_hdfCoQuanID").val(canBoID);
        }

        function showMessage() {
            $('#header_lichtiepdan').hide();
            $("#messageThongBao").show();
            return false;
        }

        function hideMessage() {
            $("#messageThongBao").hide();
            return false;
        }

        function showDetail(index) {
            var summary = $('#summary' + index).html();
            var detail = $('#detail' + index).val();
            $('#summary' + index).html(detail);
            $('#detail' + index).val(summary);
        };

        function TraCuuLich() {
            var monthSelected = $("#ddlThangTiep").val();
            var yearSelected = $("#ddlNamTiep").val();
            var coquanid = $("#MainContent_ddlCoQuanTiep").val();
            if (coquanid == "Chọn cơ quan") {
                coquanid = 0;
            }
            $.ajax({
                url: "LichTiepDan.aspx/GetDataLichTiepDan",
                type: "POST",
                data: `{'coquanid': ${coquanid}, 'thang': ${monthSelected}, 'nam': ${yearSelected}}`,
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (data) {
                    var ListData = JSON.parse(data.d);
                    for (let i = 0; i < ListData.length; i++) {
                        var NgayTiep = ListData[i].NgayTiep_Str;
                        NgayTiep = NgayTiep.split("/");
                        ListData[i].ContentID = `day${NgayTiep[0]}`
                    }
                    renderCalendar(ListData);
                },
                error: function (error) {
                    renderCalendar();
                }
            });
        }

        function renderCalendar(ListData) {
            //console.log(ListData);
            //Create data lịch
            var today = new Date();
            var thisYear = today.getFullYear();
            var monthSelected = $("#ddlThangTiep").val();
            var soNgayTrongThang = daysInMonth(monthSelected, thisYear);
            var ngayDauTienTrongThang = new Date(thisYear, monthSelected - 1, 1).getDay();
            if (ngayDauTienTrongThang == 0) {
                ngayDauTienTrongThang = 6;
            }
            else {
                ngayDauTienTrongThang--;
            }

            var weekList = [];
            var dayListed = 0;
            var dayStep = 0;

            while (dayListed < soNgayTrongThang) {
                var week = [];
                for (var i = 0; i < 7; i++) {
                    if (dayStep < ngayDauTienTrongThang || dayListed >= soNgayTrongThang) {
                        week.push("");
                        dayStep++;
                    }
                    else {
                        week.push(dayListed + 1);
                        dayListed++;
                        dayStep++;
                    }
                }
                weekList.push(week);
            }

            //Render lịch
            $("#calendar-table > tbody").html("");
            for (var i = 0; i < weekList.length; i++) {
                var rowTemp = "<tr>" + $("#tableCalendarTemplate tr:first-child").html() + "</tr>";
                rowTemp = rowTemp.replace(/_DAY0_/g, weekList[i][0]);
                rowTemp = rowTemp.replace(/_DAY1_/g, weekList[i][1]);
                rowTemp = rowTemp.replace(/_DAY2_/g, weekList[i][2]);
                rowTemp = rowTemp.replace(/_DAY3_/g, weekList[i][3]);
                rowTemp = rowTemp.replace(/_DAY4_/g, weekList[i][4]);
                rowTemp = rowTemp.replace(/_DAY5_/g, weekList[i][5]);
                rowTemp = rowTemp.replace(/_DAY6_/g, weekList[i][6]);
                $("#calendar-table > tbody").append(rowTemp);
            }
            if (ListData && ListData.length > 0) {
                var listIDContent = ListData.map(function (item) {
                    return item.ContentID;
                })
                listIDContent = [...new Set(listIDContent)];

                for (let i = 0; i < listIDContent.length; i++) {
                    var id = listIDContent[i];
                    var ListContent = ListData.filter(item => item.ContentID == id)
                    for (let j = 0; j < ListContent.length; j++) {
                        var NgayTiep = ListContent[j].NgayTiep_Str;
                        NgayTiep = NgayTiep.split("/");
                        var tempContent = $("#contentTemplate").html();
                        tempContent = tempContent.replace(/_CONTENT_/g, ListContent[j].NDTiep);
                        tempContent = tempContent.replace(/_IDCONTENT_/g, `content${NgayTiep[0]}_${j}`);
                        $(`#${id}`).append(tempContent);
                        var contentid = id.replace(/day/g, "content");
                        var popoverTemp = $("#popverTemplate").html();
                        popoverTemp = popoverTemp.replace(/_NOIDUNGTIEP_/g, ListContent[j].NDTiep);
                        popoverTemp = popoverTemp.replace(/_COQUANTIEP_/g, ListContent[j].CoQuanTiep);
                        popoverTemp = popoverTemp.replace(/_CANBOTIEP_/g, ListContent[j].CanBoTiep);
                        $(`#${contentid}_${j}`).popover({
                            html: true,
                            content: popoverTemp,
                            container: 'body'
                        });
                    }
                }
            }
        }

        function daysInMonth(month, year) {
            return new Date(year, month, 0).getDate();
        }
    </script>
</asp:Content>
