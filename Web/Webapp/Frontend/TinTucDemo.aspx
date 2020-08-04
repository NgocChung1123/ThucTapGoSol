<%@ Page Title="" Language="C#" MasterPageFile="~/Frontend.Master" AutoEventWireup="true" CodeBehind="TinTucDemo.aspx.cs" Inherits="Com.Gosol.CMS.Web.Webapp.Frontend.TinTucDemo" %>

<%@ Register Src="~/Webapp/Frontend/SideBarDanhMucTinTuc.ascx" TagPrefix="uc1" TagName="SideBarDanhMucTinTuc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="/AdminLte/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="/AdminLte/dist/css/AdminLTE.min.css" rel="stylesheet" />
    <link href="../../AdminLte/dist/css/cssTinTuc.css" rel="stylesheet" />
    <style>
        .u1175_img {
            /*border-width:0px;
          position:absolute;
          left:0px;
          top:0px;*/
            width: 8px;
            height: 8px;
        }

        @media (max-width:1500px) {
            .tin-cung-chuyen-muc-img {
                width: 50%;
            }
        }

        @media (min-width:1600px) {
            .tin-cung-chuyen-muc-img {
                width: 60%;
            }
        }
    </style>

    <div class="col-lg-9" style="padding-right: 0px;">
        <div class="box-header">
            <span style="font-size: 18px; font-weight: bold; padding-right: 15px">Tin tức nổi bật</span>
            <%--<span><img src="/images/tintuc/tintuc_1183.png" class="tin-cung-chuyen-muc-img"/></span>--%>
        </div>
        <div class="row">
            <div class="">
                <div class="col-md-8 content-left body-backgroud" style="">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="gallery" id="TinTop1">
                                <%--<div class="bottom-center">
                                    ImageBig+titleBig
                                 </div>--%>
                                <a id="linkTinTop1" href="">
                                    <img id="imageTinTop1" alt="image" src="/UploadFiles/FileWF/20180131095807_hinh-anh-hinh-nen-arsenal-dep-moi-2016-56.jpg" />
                                    <h3 id="TitletinTop1"></h3>
                                </a>
                            </div>
                        </div>
                        <div id="tinHot2_3">
                            <div class="col-md-6" style="display: block" id="tinHot2">
                                <div class="item-tintuc-new">
                                    <a href="/Webapp/Frontend/TinTucDetail.aspx?tintuc=4104" id="linktinHot2">
                                        <img id="imageTinHot2" style="width: 100%;" alt="image" src="/UploadFiles/FileWF/20180131095807_hinh-anh-hinh-nen-arsenal-dep-moi-2016-56.jpg" />
                                        <h3 id="TitletinTop2">Title</h3>
                                    </a>
                                    <p id="tomtatHot2">tom Tat</p>
                                </div>

                            </div>
                            <div class="col-md-6" style="display: block">
                                <div class="item-tintuc-new">
                                    <a href="/Webapp/Frontend/TinTucDetail.aspx?tintuc=4104" id="linktinHot3">
                                        <img id="imageTinHot3" style="width: 100%;" alt="image" src="/UploadFiles/FileWF/20180131095807_hinh-anh-hinh-nen-arsenal-dep-moi-2016-56.jpg" />
                                        <h3 id="TitletinTop3">Title</h3>
                                    </a>
                                    <p id="tomtatHot3">tom Tat</p>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
                <div class="col-md-4 side-bar" style="">
                    <ul id="allTinHot">
                    </ul>
                </div>
            </div>

           <asp:Repeater runat="server" ID="rptLoaiTinTuc" OnItemDataBound="rptLoaiTinTuc_ItemDataBound">
                <ItemTemplate>
                    <div>
                    </div>
                    <div class="col-md-12" runat="server" id="divNoiDungTin">
                        <div class="content-left body-backgroud">
                            <div class="box-primary">
                                <div class="box-header">
                                    <%--<i class="glyphicon glyphicon-list"></i>--%>
                                    <span>
                                        <asp:HyperLink runat="server" ID="hplLoaiTin" class="box-title" style="color: #333; padding-right: 10px">
                                            <asp:Label runat="server" ID="lblTenLoaiTin"></asp:Label>
                                        </asp:HyperLink>
                                    </span>                                  
                                    <span><img src="/images/tintuc/tintuc_1183.png" class="tin-cung-chuyen-muc-img"/></span>
                                    <span style="float:right;"><img src="/images/tintuc/tintuc_1186.png"/></span>  
                                    <span>
                                          <asp:HyperLink runat="server" ID="hplXemThem">
                                              <span style="font-family: 'Varela Round Regular', 'Varela Round'; font-size: 14px;float:right;padding-top: 2px;padding-right: 10px;font-weight:normal">Xem thêm</span>
                                          </asp:HyperLink>
                                    </span>
                                </div>
                                <div class="box-body">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <asp:Repeater runat="server" ID="rptTinTucChildren" OnItemDataBound="rptTinTucChildren_ItemDataBound">
                                                <ItemTemplate>    
                                                    <div class="item-loaitin">
                                                        <div class="item-loaitin-children" style="width: 100%">
                                                            <asp:HyperLink runat="server" ID="hplImage" ForeColor="White">
                                                                <asp:Image runat="server" ID="imgTinKinhTeNoiBat" class="tintuc-image-right" />
                                                            </asp:HyperLink>
                                                        </div>
                                                       
                                                    </div>
                                                     <p class="item-tintuc-title-mang-tin">
                                                        <span class="title_news">
                                                            <asp:HyperLink runat="server" ID="hylTin">
                                                                <asp:Label ID="lblTitle" runat="server"></asp:Label>
                                                            </asp:HyperLink></h2>
                                                        </span>

                                                    </p>
                                                    <p style="font-size: 12px; color: #999999;">
                                                        <asp:Label ID="lblNgayTao" runat="server" CssClass="span-date"></asp:Label>
                                                         <span>
                                                            <asp:HyperLink runat="server" ID="hplXemChiTiet" Style="font-style:italic">
                                                                <span style="font-weight: normal;font-size: 12px;color: #999999;">Xem chi tiết</span>
                                                            </asp:HyperLink>
                                                        </span>
                                                    </p>
                                                    <div class="item-loaitin-children item-loaitin-children-text" style="width: 100%;padding-left: 0px;">
                                                            <span>
                                                                <asp:Label ID="lblTomTat" runat="server"></asp:Label></span>
                                                            </span>
                                                     </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                        <div class="col-md-6" style="border-left: 1px solid #eee;">
                                            <ul class="list-group list-group-flush">
                                                <asp:Repeater ID="rptTinTucLienQuan" runat="server" OnItemDataBound="rptTinTucLienQuan_ItemDataBound">
                                                    <ItemTemplate>
                                                            <%--<div>
                                                                <a style="width:25%;">
                                                                    <asp:HyperLink runat="server" ID="hplImageTinLienQuan" ForeColor="White">
                                                                        <asp:Image runat="server" ID="imgTinTucLienQuan" class="tintuc-image-tin-lien-quan" />
                                                                    </asp:HyperLink>
                                                                </a>
                                                                <span>
                                                                    <asp:HyperLink runat="server" ID="hylTin">
                                                                        <asp:Label ID="lblTitle" runat="server"></asp:Label>
                                                                    </asp:HyperLink>
                                                                </span> 
                                                            </div> --%>
                                                       <table style="margin-bottom: 10px;">
                                                           <tr>
                                                               <td style="width:25%">
                                                                   <asp:HyperLink runat="server" ID="hplImageTinLienQuan" ForeColor="White">
                                                                        <asp:Image runat="server" ID="imgTinTucLienQuan" class="tintuc-image-tin-lien-quan" />
                                                                    </asp:HyperLink>
                                                               </td>
                                                               <td style="padding-left:10px">
                                                                   <p>
                                                                       <asp:HyperLink runat="server" ID="hylTin">
                                                                            <asp:Label ID="lblTitle" runat="server"></asp:Label>
                                                                        </asp:HyperLink>
                                                                   </p>                                                                   
                                                                   <p style="font-size: 12px; color: #999999;">
                                                                        <asp:Label ID="lblNgayTao" runat="server" CssClass="span-date"></asp:Label>
                                                                        <span>
                                                                            <asp:HyperLink runat="server" ID="hplXemChiTiet" Style="font-style:italic">
                                                                                <span style="font-weight: normal;font-size: 12px;color: #999999;">Xem chi tiết</span>
                                                                            </asp:HyperLink>
                                                                        </span>
                                                                    </p>
                                                               </td>
                                                           </tr>
                                                       </table>
                                                       <%-- <li class="list-group-item" style="border-bottom: 1px solid #eee !important;">
                                                            <asp:HyperLink runat="server" ID="hylTin">
                                                                 <asp:Label ID="lblTitle" runat="server"></asp:Label>
                                                            </asp:HyperLink>
                                                        </li>--%>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="tintuc-title-mangtin" style="display: none;">
                            </div>
                        </div>
                    </div>

                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
    <div class="col-lg-3" style="padding-right: 0px; float: right; padding-top: 0px;">
        <uc1:SideBarDanhMucTinTuc runat="server" ID="SideBarDanhMucTinTuc" />
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            getTop3TinHot();
            get10TinHot();
            //getAllLoaiTin();
        });
        function getTop3TinHot() {
            $.ajax({
                type: "POST",
                url: "TinTucDemo.aspx/getTop3TinHot",
                dataType: "json",
                async: "true",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    var Top3Hot = JSON.parse(data.d);
                    var link1 = '/Webapp/Frontend/TinTucDetail.aspx?tintuc=' + Top3Hot[0].IDTinTuc;
                    $("#linkTinTop1").attr('href', link1);
                    $("#imageTinTop1").attr('src', '/' + Top3Hot[0].ImageUrl);
                    $("#TitletinTop1").text(Top3Hot[0].TieuDe);
                    var link2 = '/Webapp/Frontend/TinTucDetail.aspx?tintuc=' + Top3Hot[1].IDTinTuc;
                    $("#linktinHot2").attr('href', link2);
                    $("#imageTinHot2").attr('src', '/' + Top3Hot[1].ImageUrl);
                    $("#TitletinTop2").text(Top3Hot[1].TieuDe);
                    $("#tomtatHot2").text(Top3Hot[1].TomTat);
                    var link3 = '/Webapp/Frontend/TinTucDetail.aspx?tintuc=' + Top3Hot[2].IDTinTuc;
                    $("#linktinHot3").attr('href', link3);
                    $("#imageTinHot3").attr('src', '/' + Top3Hot[2].ImageUrl);
                    $("#TitletinTop3").text(Top3Hot[2].TieuDe);
                    $("#tomtatHot3").text(Top3Hot[2].TomTat);
                    //for (let i = 0; i < Top3Hot.length; i++) {
                    //}
                }
            });
        }
        function get10TinHot() {
            $.ajax({
                type: "POST",
                url: "TinTucDemo.aspx/getAllTinHot",
                dataType: "json",
                async: "true",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    var allTopHot = JSON.parse(data.d);
                    for (let i = 0; i < allTopHot.length; i++) {
                        var link = '/Webapp/Frontend/TinTucDetail.aspx?tintuc=' + allTopHot[i].IDTinTuc;
                        var item = "<a style='color:black' href='" + link + "'><li style='font-size:16px;text-decoration: underline;color:#3C8DBC;'>" + allTopHot[i].TieuDe + "</li></a></br>";
                        $("#allTinHot").append(item);
                    }
                }
            });
        }
        function getAllLoaiTin() {
            $.ajax({
                type: "POST",
                url: "TinTucDemo.aspx/getAllLoaiTinTuc",
                dataType: "json",
                async: "true",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    var allLoaiTin = JSON.parse(data.d);
                    $("label").text(allLoaiTin[0].TenLoaiTin);
                    $("a.box-title").attr("href", "/Webapp/Frontend/LoaiTinTucDetail.aspx?mangtinid=" + allLoaiTin[0].IDLoaiTin);
                    //for (let i = 0; i < allLoaiTin.length; i++) {
                    //    $("#lbTenLoaiTin").text(allLoaiTin[i].TenLoaiTin);
                    //    $("#linkLoaiTin").attr("href", "/Webapp/Frontend/LoaiTinTucDetail.aspx?mangtinid=" + allLoaiTin[i].IDLoaiTin);
                    //    $("#linkXemThem").attr("href", "/Webapp/Frontend/LoaiTinTucDetail.aspx?mangtinid=" + allLoaiTin[i].IDLoaiTin);

                    //}
                }
            });
        }
    </script>
</asp:Content>

