<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LichSuTraCuuDonThu.aspx.cs" Inherits="Com.Gosol.CMS.Web.Webapp.Backend.DonThu.LichSuTraCuuDonThu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager runat="server" ID="ScriptManager1" EnablePartialRendering="true">
    </asp:ScriptManager>

    <link href="/AdminLte/ValidateForm/css/template.css" rel="stylesheet" type="text/css" />
    <link href="/AdminLte/ValidateForm/css/validationEngine.jquery.css" rel="stylesheet" type="text/css" />
    <script src="/AdminLte/ValidateForm/js/jquery.validationEngine.js" type="text/javascript"></script>
    <script src="/AdminLte/ValidateForm/js/jquery.validationEngine-vi.js" type="text/javascript"></script>

    <style type="text/css">
        .tables-noborder {
            width: 100%;
            margin: 0 auto;
        }

            .tables-noborder tr td {
                height: 50px;
            }

        table#example2 tr th {
            text-align: center;
        }
    </style>

    <script type="text/javascript">
        $(document).ready(function () {
            //var config = {
            //    '.chosen': {}
            //};
            //for (var selector in config) {
            //    $(selector).chosen(config[selector]);

            //}
            //$(".chosen").trigger("chosen:updated");

            $("#MainContent_ddlCoQuan").select2();
        });
    </script>

    <section class="content-header">
      <h1>
        Quản lý tra cứu trạng thái hồ sơ
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="/Default.aspx"><i class="fa fa-dashboard"></i> Trang chủ</a></li>
        <li><a href="#">Quản lý tra cứu</a></li>
        <li class="active">Quản lý tra cứu trạng thái hồ sơ</li>
      </ol>
    </section>

    <section class="content">
      <div class="row">
        <div class="col-xs-12">
          <div class="box box-primary">
            <div class="box-body ">
                <div class="box-header" style="padding: 0px;">
                        <asp:Panel runat="server" DefaultButton="btnSearch">
                        <div class="col-lg-3 col-lg-offset-4" style="padding-right:5px">
                            <asp:DropDownList ID="ddlCoQuan" runat="server" DataValueField="CoQuanID" DataTextField="TenCoQuan" AutoPostBack="true" OnSelectedIndexChanged="ddlCoQuan_SelectedIndexChanged"
                                        CssClass="chosen form-control">
                                    </asp:DropDownList>
                        </div>
                        <div class="col-lg-4" style="padding-right:0px">
                             <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control input-search" placeholder="Nhập tên chủ đơn hoặc ND đơn cần tìm kiếm">
                                    </asp:TextBox>
                        </div>
                        <div class="col-lg-1">
                             <asp:Button ID="btnSearch" runat="server" CausesValidation="false" CssClass="btn btn-default btn-sm" Style=" margin-bottom: 10px"
                                        OnClick="btnSearch_Click" Text="Tìm kiếm" />

                        </div>
                            </asp:Panel>
                    </div>
              <table id="example2" class="table table-bordered table-hover table-responsive">
                <thead>
                <tr>
                  <th style="width:5%">STT</th>
                  <th style="width:5%">Số đơn</th>
                  <th style="width:10%">Tên chủ đơn</th>
                  <th style="width:10%">Địa chỉ</th>
                  <th style="width:10%">ND đơn</th>
                  <th style="width:10%">Loại khiếu tố</th>
                  <th style="width:10%">Ngày tiếp nhận</th>
                    <th style="width:10%">CQ tiếp nhận</th>
                    <th style="width:10%">Ngày tra cứu</th>
                    <th style="width:10%">Hướng xử lý</th>
                    <th style="width:10%">Trạng thái hồ sơ</th>
                </tr>
                </thead>
                <tbody>
                    
                <asp:Repeater ID="rptLichSu" runat="server" OnItemDataBound="rptLichSu_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td style="text-align:left">
                                <asp:Label ID="lblSTT" Text="" runat="server" />
                                </td>
                              <td style="text-align:left">
                                  <%# Eval("SoDonThu") %>
                              </td>
                              <td style="text-align:left">
                                  <%# Eval("NguoiDaiDien") %>
                              </td>
                          <td style="text-align:left">
                              <%# Eval("DiaChi") %>
                          </td>
                          <td style="text-align:left"><%# Eval("NoiDungDon") %></td>
                            <td style="text-align:left"><%# Eval("PhanLoaiDon") %></td>
                            <td style="text-align:left">
                                <%# Eval("NgayTiepNhan")==null ? "" : Com.Gosol.CMS.Utility.Format.FormatDate(Com.Gosol.CMS.Utility.Utils.ConvertToDateTime(Eval("NgayTiepNhan").ToString(),DateTime.MinValue)) %>
                            </td>
                            <td style="text-align:left"><%# Eval("CoQuanTiepNhan") %></td>
                            <td style="text-align:left">
                                <%# Eval("NgayTraCuu")==null ? "" : Com.Gosol.CMS.Utility.Format.FormatDate(Com.Gosol.CMS.Utility.Utils.ConvertToDateTime(Eval("NgayTraCuu").ToString(),DateTime.MinValue)) %>
                            </td>
                            <td style="text-align:left"><%# Eval("HuongXuLy") %></td>
                            <td style="text-align:left"><%# Eval("TrangThaiDonThu") %></td>
                        </tr>
                    </ItemTemplate>
                    </asp:Repeater>
                
                </tbody>
              </table>
                <div class="paginations" style="margin-top: 15px">
                <asp:PlaceHolder ID="plhPaging" runat="server"></asp:PlaceHolder>
            </div>
            </div>
            <!-- /.box-body -->
          </div>
          <!-- /.box -->

        </div>
        <!-- /.col -->
      </div>
      <!-- /.row -->
    </section>

</asp:Content>
