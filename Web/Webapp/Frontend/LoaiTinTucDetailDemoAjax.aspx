<%@ Page Title="" Language="C#" MasterPageFile="~/Frontend.Master" AutoEventWireup="true" CodeBehind="LoaiTinTucDetailDemoAjax.aspx.cs" Inherits="Com.Gosol.CMS.Web.Webapp.Frontend.LoaiTinTucDemoAjax" %>

<%@ Register Src="~/Webapp/Frontend/SideBarDanhMucTinTuc.ascx" TagPrefix="uc1" TagName="SideBarDanhMucTinTuc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="/AdminLte/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="/AdminLte/dist/css/AdminLTE.min.css" rel="stylesheet" />
    <link href="../../AdminLte/dist/css/cssTinTuc.css" rel="stylesheet" />
    <input type="hidden" id="loaiTinID" value="0" runat="server" />
    <div class="col-lg-9">
        <div class="row">
            <div class="box box-primary">
                <div class="box-header" style="font-family: 'Varela Round Regular', 'Varela Round'; font-size: 20px; padding-left: 40px; padding-bottom: 0px;">
                    <label>_TENLOAITIN_</label>
                </div>
                <div class="box-body" id="lstTinTuc">
                    <div id="item_TinTuc">
                        <div class="list-loaitin">
                            <div class="item-loaitin">
                                <div class="item-loaitin-children col-lg-4 col-md-4 col-sm-4 col-sm-4">
                                    <a href="_LINKCHITIETTINTUC_">
                                        <img src="_IMAGETINTUC_" class="tintuc-image-right" />
                                    </a>
                                </div>
                                <div class="item-loaitin-children item-loaitin-children-text col-lg-8 col-md-8 col-sm-8 col-sm-12">
                                    <p class="item-tintuc-title-mang-tin">
                                        <span>
                                            <a href="_LINKCHITIETTINTUC_" style="font-size: 14px; font-weight: 700; font-family: 'Varela Round Bold', 'Varela Round'; color: #333333 !important;">
                                                <label>_TIEUDETINTUC_</label>
                                            </a>
                                        </span>
                                    </p>
                                    <p style="font-size: 12px; color: #999999;">
                                        <label class="span-date">_NGAYTAO_</label>
                                        <span>
                                            <a href="_LINKCHITIETTINTUC_" style="font-style: italic">
                                                <span style="font-weight: normal; font-size: 12px; color: #999999;">Xem chi tiết</span>
                                            </a>
                                        </span>
                                    </p>
                                    <span style="font-family: 'Varela Round Regular', 'Varela Round';">
                                        <label>_TOMTAT_</label>
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
            <div class="clearfix"></div>
            <div class="paginations" style="margin-top: 15px; margin-bottom: 15px">
                <ul id="ulPage" class="nav navbar-nav nav-list" style="float: right; padding-right: 20px;">
                </ul>
            </div>
            <div class="clearfix"></div>
        </div>
    </div>
    <div class="col-lg-3">
        <uc1:SideBarDanhMucTinTuc runat="server" ID="SideBarDanhMucTinTuc" />
    </div>
    <div id="liItemPage" style="display: none">
        <li><a href="_LINKNEXTPAGE_" style="padding: 5px; margin: 2px; border: 1px solid #337ab7; text-decoration: none;">_PAGENUMBER_</a></li>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            getALLTinTuc();
        });
        function createPage(lstData) {
            var pageSize = 10;
            var totalRow = lstData.length;
            var totalPage = totalRow / pageSize;
            if (totalRow % pageSize != 0) totalPage++;
            for (let i = 1; i < totalPage; i++) {
                var liTemp = $("#liItemPage").html();
                liTemp = liTemp.replace(/_LINKNEXTPAGE_/g, "/Webapp/Frontend/LoaiTinTucDetailDemoAjax.aspx?mangtinid=" + 1005);
                liTemp = liTemp.replace(/_PAGENUMBER_/g, i);
                console.log("sucesssssss");
                $("#ulPage").append(liTemp);
            }
        }
        function getALLTinTuc() {
            var t = $("#MainContent_loaiTinID").val();
            console.log(t);
            $.ajax({
                type: "POST",
                url: "LoaiTinTucDetailDemoAjax.aspx/GetAlltinTucByIDLoaiTin",
                data ='{loaiTinID: "1005"}',
                dataType: "json",
                async: "false",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    var all = JSON.parse(data.d);
                    console.log(all);
                    if (all = "") {
                        alert("Chua truyen ID loai tin !");
                    }
                },
                error: function () {
                    alert("fail")
                },
            });
        }
    </script>
</asp:Content>
