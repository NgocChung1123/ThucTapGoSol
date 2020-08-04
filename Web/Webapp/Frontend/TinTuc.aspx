<%@ Page Title="" Language="C#" MasterPageFile="~/Frontend.Master" AutoEventWireup="true" CodeBehind="TinTuc.aspx.cs" Inherits="Com.Gosol.CMS.Web.Webapp.Frontend.TinTuc" %>

<%@ Register Src="~/Webapp/Frontend/SideBarDanhMucTinTuc.ascx" TagPrefix="uc1" TagName="SideBarDanhMucTinTuc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .u1175_img {
          /*border-width:0px;
          position:absolute;
          left:0px;
          top:0px;*/
          width:8px;
          height:8px;
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
            <span style="font-size: 18px; font-weight:bold; padding-right:15px">Tin tức nổi bật</span>
            <%--<span><img src="/images/tintuc/tintuc_1183.png" class="tin-cung-chuyen-muc-img"/></span>--%>
        </div>
        <div class="row">
            <div class="">
                <div class="col-md-8 content-left body-backgroud">
                <div class="row">
                    <div class="col-md-12">
                        <div class="gallery">
                            <asp:Repeater ID="rptTinHot_Top1" runat="server" OnItemDataBound="rptTinHot_Top1_ItemDataBound">
                                <ItemTemplate>
                                    <div class="bottom-center">
                                        <asp:HyperLink runat="server" ID="hylTin" ForeColor="White">
                                            <h3>
                                                <asp:Label ID="lblTitle" runat="server"></asp:Label>
                                            </h3>
                                        </asp:HyperLink>
                                        <span style="display: none;">
                                            <asp:Label ID="lblTomTat" runat="server"></asp:Label></span>
                                    </div>
                                    <asp:HyperLink runat="server" ID="hplImage" ForeColor="White">
                                        <asp:Image runat="server" ID="imgTinHot_Top1" class=" item-tinnoichinh-image" />
                                    </asp:HyperLink>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>

                    <div class="col-md-6" style="display: block">
                        <div class="item-tintuc-new ">
                            <asp:Repeater ID="rptTinHot_Top2" runat="server" OnItemDataBound="rptTinHot_Top2_ItemDataBound">
                                <ItemTemplate>
                                    <div class="item-tinnoichinh-image-p">
                                        <asp:HyperLink runat="server" ID="hplImage" ForeColor="White">
                                            <asp:Image runat="server" ID="imgTinHot_Top2" class=" item-tinnoichinh-image" />
                                        </asp:HyperLink>
                                    </div>
                                    <div class="item-tintuc-new-widget">
                                        <p class="item-tintuc-noi-bat-title">
                                            <span class="title_news">
                                                <asp:HyperLink runat="server" ID="hylTin" CssClass="hpl-hover">
                                                    <asp:Label ID="lblTitle" runat="server"></asp:Label>
                                                </asp:HyperLink></h2>
                                            </span>
                                        </p>
                                        <span>
                                            <asp:Label ID="lblTomTat" runat="server"></asp:Label></span>
                                        </span>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>

                    <div class="col-md-6" style="display: block">
                        <div class="item-tintuc-new">
                            <asp:Repeater ID="rptTinHot_Top3" runat="server" OnItemDataBound="rptTinHot_Top3_ItemDataBound">
                                <ItemTemplate>
                                    <div class="item-tinnoichinh-image-p">
                                        <asp:HyperLink runat="server" ID="hplImage" ForeColor="White">
                                            <asp:Image runat="server" ID="imgTinHot_Top3" class=" item-tinnoichinh-image" />
                                        </asp:HyperLink>
                                    </div>
                                    <div class="item-tintuc-new-widget">
                                        <p class="item-tintuc-noi-bat-title">
                                            <span style="cursor: pointer" class="title_news">
                                                <asp:HyperLink runat="server" ID="hylTin">
                                                    <asp:Label ID="lblTitle" runat="server"></asp:Label>
                                                </asp:HyperLink></h2>
                                            </span>
                                        </p>
                                        <span>
                                            <asp:Label ID="lblTomTat" runat="server"></asp:Label></span>
                                        </span>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
                </div>

                <div class="col-md-4 side-bar">
                <asp:Repeater ID="rptTinMoi" runat="server" OnItemDataBound="rptTinMoi_ItemDataBound">
                    <ItemTemplate>
                        <div class="item-tin-noi-bat">
                            <div class="item-tin-noi-bat-img">
                                <img src="/images/tintuc/tintuc_1175.png"/>
                            </div>
                            <div class="item-tin-noi-bat-children" style="width: 20%; display: none">
                                <img style="float:left" src="/images/tintuc/tintuc_1175.png"/>
                                <asp:HyperLink runat="server" ID="hplImage" ForeColor="White">
                                    <asp:Image runat="server" ID="imgTinNoiBat_Top4" class="tintuc-image-right" />
                                </asp:HyperLink>
                            </div>
                            <div class="item-tin-noi-bat-children item-tin-noi-bat-children-text">
                                <div class="item-tintuc-new-title">
                                    <span>
                                        <asp:HyperLink runat="server" ID="hylTin">
                                            <asp:Label ID="lblTitle" runat="server"></asp:Label>
                                        </asp:HyperLink></h2>
                                    </span>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <div class="clearfix"></div>
            </div>
            </div>
            
            <%--<div class="clear-both"></div>--%>
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
    <div class="col-lg-3" style="padding-right: 0px;">
        <uc1:SideBarDanhMucTinTuc runat="server" ID="SideBarDanhMucTinTuc" />
    </div>

</asp:Content>
