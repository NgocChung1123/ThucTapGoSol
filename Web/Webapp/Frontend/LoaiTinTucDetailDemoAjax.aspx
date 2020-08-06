<%@ Page Title="" Language="C#" MasterPageFile="~/Frontend.Master" AutoEventWireup="true" CodeBehind="LoaiTinTucDetailDemoAjax.aspx.cs" Inherits="Com.Gosol.CMS.Web.Webapp.Frontend.LoaiTinTucDemoAjax" %>

<%@ Register Src="~/Webapp/Frontend/SideBarDanhMucTinTuc.ascx" TagPrefix="uc1" TagName="SideBarDanhMucTinTuc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="/AdminLte/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="/AdminLte/dist/css/AdminLTE.min.css" rel="stylesheet" />
    <link href="../../AdminLte/dist/css/cssTinTuc.css" rel="stylesheet" />
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
            </div>
            <div class="clearfix"></div>
        </div>
    </div>
    <div class="col-lg-3">
        <uc1:SideBarDanhMucTinTuc runat="server" ID="SideBarDanhMucTinTuc" />
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            getALLTinTuc();
        });
        function getALLTinTuc() {
            $.ajax({
                type: "POST",
                url: "TinTucDemo.aspx/getTop3TinHot",
                dataType: "json",
                async: "true",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    var all = JSON.parse(data.d);
                    console.log("aaaaaaaaaa");
                },
                 error: function () {
                     alert("fail")
                },
            });
        }
    </script>
</asp:Content>
