<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="QlTrangThaiHoSo.aspx.cs" Inherits="Com.Gosol.CMS.Web.Webapp.Backend.DonThu.QlTrangThaiHoSo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager runat="server" ID="ScriptManager1" EnablePartialRendering="true">
    </asp:ScriptManager>

    <script src="../../../Scripts/dropdownlist/chosen.jquery.js"></script>
    <link href="../../../Styles/dropdownlist/chosen.min.css" rel="stylesheet" />

    <link href="/AdminLte/ValidateForm/css/template.css" rel="stylesheet" type="text/css" />
    <link href="/AdminLte/ValidateForm/css/validationEngine.jquery.css" rel="stylesheet" type="text/css" />
    <script src="/AdminLte/ValidateForm/js/jquery.validationEngine.js" type="text/javascript"></script>
    <script src="/AdminLte/ValidateForm/js/jquery.validationEngine-vi.js" type="text/javascript"></script>
    <script src="../../../Scripts/khiem.js"></script>
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

        .divInsert {
            background-color: white;
            border: 1px solid;
            padding: 2px 7px;
            margin-bottom: 5px;
        }

        .control-label {
            text-align: left !important;
        }
    </style>

    <script type="text/javascript">
        $(document).ready(function () {
            var config = {
                '.chosen': {}
            }

            for (var selector in config) {
                $(selector).chosen(config[selector]);
            }

            $(".chosen").trigger("chosen:updated");

            $("input.cbAction").on('change', function () {
                $("input.cbAction").not(this).prop('checked', false);
                if ($("input.cbAction").is(':checked')) {
                    $("#btnAction").show();
                    $("#MainContent_hdfDonThuID").val($(this).attr("id"));
                }
                else {
                    $("#btnAction").hide();
                    $("#MainContent_hdfDonThuID").val('');
                }
            });

            $(".select2").select2();
            $(".js-example-basic-single").select2({
            });
        });
    </script>

    <script>
        function ShowAddForm() {
            ResetForm();
            $("#MainContent_hdfDonThuID1").val($("#MainContent_hdfDonThuID").val());
            $("#MainContent_hdfDonThuID").val('');
            $("#notAddForm").hide();
            $("#addForm").show();
        }

        function HideAddForm() {
            $("#notAddForm").show();
            $("#addForm").hide();
            $("#MainContent_hdfDonThuID").val($("#MainContent_hdfDonThuID1").val());
        }

        function ShowEditForm() {
            var donThuId = $("#MainContent_hdfDonThuID").val();
            $.ajax({
                type: "POST",
                url: "QLVanBanTraLoi.aspx/GetByID",
                data: '{donThuId:"' + donThuId + '"}',
                dataType: "json",
                async: "true",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    var json = eval('(' + data.d + ')');
                    if (json != null) {
                        $('#MainContent_hdfDonThuID').val(donThuId);
                        $('#MainContent_txtSoDon').val(json.SoDonThu);
                        //$('#MainContent_txtNgayTiepNhan').val(json.NgayTiepNhan);
                        $('#MainContent_txtNgayTiepNhan').datepicker('setDate', json.NgayTiepNhan);
                        $('#MainContent_ddlCoQuan').val(json.CoQuanID).change();
                        //$("#MainContent_ddlCoQuan").attr("disabled", "disabled");
                        $('#MainContent_txtHoTen').val(json.HoTen);
                        $('#MainContent_txtDiaChi').val(json.DiaChi);
                        $('#MainContent_txtNoiDungDon').val(json.NoiDungDon);
                        $('#MainContent_txtNgayXuLy').val(json.NgayXuLy);
                        $('#MainContent_txtCoQuanXuLy').val(json.CoQuanXuLyID).change();
                        $('#MainContent_ddlHuongXuLy').val(json.TrangThaiDonThu);
                        if (json.FileQuyetDinh != "") {
                            $("#lblFile").show();
                        }
                        else {
                            $("#lblFile").hide();
                        }
                        $("#notAddForm").hide();
                        $("#addForm").show();
                    }
                }
            });
        }

        function ResetForm() {
            //$("#MainContent_hdfDonThuID").val('');
            $('#MainContent_txtSoDon').val('');
            $('#MainContent_txtNgayTiepNhan').val('').datepicker('update');
            $('#MainContent_ddlCoQuan').val('0').change();
            $('#MainContent_txtHoTen').val('');
            $('#MainContent_txtDiaChi').val('');
            $('#MainContent_txtNoiDungDon').val('');
            $('#MainContent_txtNgayXuLy').val('').datepicker('update');
            $('#MainContent_ddlCoQuanXuLy').val('0').change();
            $('#MainContent_ddlHuongXuLy').val('0').change();
            //$('input.cbAction').prop('checked', false);
        }

        function CheckValidate() {

        }

        function CapNhatKNTC() {
            var url = "http://192.168.100.42:10008/api/GetPortalSync";
            $.ajax({
                url: url,
                type: "GET",
                success: function (data) {
                    //console.log(data);
                    if (data != null) {
                        for (var i = 0; i < data.length; i++) {
                            if (data[i].TrangThaiDonID != 2) {
                                $('<tr>').append(
                                    $('<td style="center">').text(i + 1),
                                    $('<td>').text(data[i].SoDonThu),
                                    $('<td>').text(data[i].NguoiDaiDien),
                                    $('<td>').text(data[i].DiaChi),
                                    $('<td>').text(data[i].NoiDungDon),
                                    $('<td>').text(data[i].TenLoaiKhieuTo),
                                    $('<td>').text(data[i].NgayTiepNhan),
                                    $('<td>').text(data[i].TenCoQuanTiepNhan),
                                    $('<td>').text(data[i].NgayBanHanh)
                                ).appendTo("#records_table");
                            }
                        }
                    };
                },
                error: function (data) {
                }
            });
        }

        function DongBoKNTC() {
            $("#syncNotify").modal();
        }

        //function Download(/*filepath*/) {
        //    var filepath = "D:\Projects\CongThongTin\Web\UploadFiles\FileWF\122(553).jpg";
        //    $.ajax({
        //        type: "POST",
        //        url: "/Handler/DownloadFileQuyetDinh.ashx",
        //        dataType: "json",
        //        //data: '{filepath:"' + filepath + '"}',
        //        success: function (data) {

        //        }
        //    });
        //    //$.post("../../../Handler/DownloadFileQuyetDinh.ashx", {
        //    //    filepath: filepath
        //    //}).done(function (data) {

        //    //});
        //}
    </script>

    <section class="content-header">
      <h1>
        Quản lý tra quyết định giải quyết
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="/Default.aspx"><i class="fa fa-dashboard"></i> Trang chủ</a></li>
        <li><a href="#">Quản lý tra cứu</a></li>
        <li class="active">Quản lý tra quyết định giải quyết</li>
      </ol>
    </section>

    <section class="content">
      <div class="row">
        <div class="col-xs-12">
          <div class="box box-primary">
            <div class="box-body">

                <ul class="nav nav-tabs">
    <li class="active"><a data-toggle="tab" href="#home" style="font-size:14px; font-weight:700">Quản lý</a></li>
    <li><a data-toggle="tab" href="#dongbo" style="font-size:14px; font-weight:700">Đồng bộ</a></li>
  </ul>

  <div class="tab-content">
    <div id="home" class="tab-pane fade in active" style="padding-top:10px">
        <div id="notAddForm">
                <div class="box-header" style="padding: 0px; margin-bottom:10px">
                        <asp:Panel runat="server" DefaultButton="btnSearch">
                        <div class="col-lg-3 col-lg-offset-4" style="padding-right:5px">
                            <asp:DropDownList ID="ddlCoQuanSearch" runat="server" DataValueField="CoQuanID" DataTextField="TenCoQuan" AutoPostBack="true" OnSelectedIndexChanged="ddlCoQuanSearch_SelectedIndexChanged"
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
                 <%--   <div id="btnAction" class="col-lg-offset-9 col-lg-3 text-right" style="padding-right:0px; display:none">

                        <input type="button" class="btn btn-primary" value="Thêm QĐ" onclick="ShowAddForm();"/>
                        <input type="button" class="btn btn-primary" value="Sửa" onclick="ShowEditForm();"/>
                        <input type="button" class="btn btn-primary" value="Xóa" onclick="ConfirmDelete();"/>
                    </div>--%>
                    <div class="col-lg-offset-9 col-lg-3 text-right" style="padding-right: 0px;">
                
                                            <asp:Button ID="btnAdd" runat="server" class="btn btn-primary" Text="Thêm QĐ" OnClientClick="ShowAddForm();"  />
                                            <span id="btnAction" style="display: none;">
                                                <asp:Button ID="btnEdit" runat="server" class="btn btn-primary" Text="Sửa" OnClientClick="return ShowEditForm();" />
                    
                                                 <asp:Button ID="btnDelete1" runat="server" class="btn btn-primary" Text="Xoá" OnClientClick="return ConfirmDelete();" />
                                       
                                            </span>
                                        </div>
                    </div>
              <table id="example2" class="table table-bordered table-hover table-responsive">
                <thead>
                <tr>
                    <th style="width:5%">#</th>
                  <th style="width:5%">STT</th>
                  <th style="width:5%">Số đơn</th>
                  <th style="width:10%">Tên chủ đơn</th>
                  <th style="width:10%">Địa chỉ</th>
                  <th style="width:10%">ND đơn</th>
                  <th style="width:10%">Loại khiếu tố</th>
                  <th style="width:10%">Ngày tiếp nhận</th>
                    <th style="width:10%">CQ tiếp nhận</th>
                    <th style="width:10%">Ngày ban hành QĐ</th>
                    <th style="width:10%">File QĐ</th>
                    <th style="width:5%">Hiển thị</th>
                </tr>
                </thead>
                <tbody>
                    
                <asp:Repeater ID="rptVanBan" runat="server" OnItemDataBound="rptVanBan_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td style="text-align:center">
                                <input type="checkbox" class="cbAction" id='<%# Eval("ID") %>'/>
                            </td>
                            <td style="text-align:center">
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
                          <td style="text-align:left">
                              <%# Eval("NoiDungDon") %>

                          </td>
                            <td style="text-align:left">
                                <%# Eval("PhanLoaiDon") %>

                            </td>
                            <td style="text-align:left">
                                <%# Eval("NgayTiepNhan")==null ? "" : Com.Gosol.CMS.Utility.Format.FormatDate(Com.Gosol.CMS.Utility.Utils.ConvertToDateTime(Eval("NgayTiepNhan").ToString(),DateTime.MinValue)) %>
                            </td>
                            <td style="text-align:left">
                                <%# Eval("CoQuanTiepNhan") %>

                            </td>
                            <td style="text-align:left">
                                <%# Eval("NgayBanHanh")==null ? "" : Com.Gosol.CMS.Utility.Format.FormatDate(Com.Gosol.CMS.Utility.Utils.ConvertToDateTime(Eval("NgayBanHanh").ToString(),DateTime.MinValue)) %>
                            </td>
                            <td style="text-align:center">
                                <%# Eval("FileQuyetDinh").ToString()=="" ? "Chưa có":
                                        "<a href = '../../../Handler/DownloadFileQuyetDinh.ashx?filename="+Eval("FileQuyetDinh") + "'><img src='"+"../../../images/download.png"
                                        +"' style='"+"width: 20px; height: 20px"+"'/>" %>
                                <%--<a target="" href='<%# "../../../Handler/DownloadFileQuyetDinh.ashx?filename=" + Eval("FileQuyetDinh").ToString()  %>'>
                                    <img src="../../../images/download.png" style="width: 20px; height: 20px" title="tải về" />
                                </a>--%>
                            </td>
                            <td style="text-align:center">
                                <input type="checkbox" checked />
                            </td
                        </tr>
                    </ItemTemplate>
                    </asp:Repeater>
                
                </tbody>
              </table>
                <div class="paginations" style="margin-top: 15px">
                <asp:PlaceHolder ID="plhPaging" runat="server"></asp:PlaceHolder>
            </div>
            </div>

      <div class="panel panel-primary" id="addForm" style="display:none">
          <div class="panel-heading">Thêm / Sửa văn bản trả lời</div>
          <div class="panel-body" style="padding: 7px">
              <div class="divInsert">
                  <asp:HiddenField ID="hdfDonThuID" runat="server" />
                  <asp:HiddenField ID="hdfDonThuID1" runat="server" />
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <label class="control-label col-md-2">
                                            Số đơn
                                        </label> 
                                        <div class="col-md-4">
                                            <asp:TextBox runat="server" ID="txtSoDon" CssClass="form-control validate[required]"></asp:TextBox>
                                    </div>

                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-md-2">
                                            Ngày tiếp nhận
                                        </label> 
                                        <div class="col-md-4">
                                            <asp:TextBox runat="server" ID="txtNgayTiepNhan" CssClass="datepicker form-control validate[required] validate[custom[date]]"></asp:TextBox>
                                        </div>
                                        <label class="control-label col-md-2">
                                            Cơ quan tiếp nhận
                                        </label> 
                                        <div class="col-md-4">
                                            <asp:DropDownList runat="server" ID="ddlCoQuan" CssClass="select2 form-control validate[required]" DataValueField="CoQuanID" DataTextField="TenCoQuan" style="width:100%"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                           
</div>
              <div class="divInsert">
                  <h4><b><i>Thông tin công dân</i></b></h4>
                  <%--<div style="margin-right:20px; display:initial">
                  <input type="checkbox" style="width:15px; height:15px;"/>Cá nhân
                      </div>
                  <div style="margin-right:20px; display:initial">
                  <input type="checkbox" style="width:15px; height:15px;" />Cơ quan tổ chức
                      </div>
                      <div style="margin-right:20px; display:initial">
                  <input type="checkbox" style="width:15px; height:15px;" />Tập thể
                          </div>
                  <br />
                  <br />--%>
                  <div class="form-horizontal">
                                    <div class="form-group">
                                        <label class="control-label col-md-1">
                                            Họ tên
                                        </label> 
                                        <div class="col-md-7">
                                            <asp:TextBox runat="server" ID="txtHoTen" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    </div>
                      <div class="form-group">
                                        <label class="control-label col-md-1">
                                            Giới tính
                                        </label> 
                                        <div class="col-md-3">
                                            <asp:DropDownList runat="server" ID="ddlGioiTinh" CssClass="form-control" Enabled="false"></asp:DropDownList>
                                    </div>
                          <label class="control-label col-md-1">
                                           CMND
                                        </label> 
                                        <div class="col-md-3">
                                            <asp:TextBox runat="server" ID="txtCMND" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                          <label class="control-label col-md-1">
                                           Điện thoại
                                        </label> 
                                        <div class="col-md-3">
                                            <asp:TextBox runat="server" ID="txtDienThoai" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </div>
                                    </div>
                      <div class="form-group">
                                        <label class="control-label col-md-1">
                                            Nghề nghiệp
                                        </label> 
                                        <div class="col-md-3">
                                            <asp:DropDownList runat="server" ID="ddlNgheNghiep" CssClass="form-control" Enabled="false"></asp:DropDownList>
                                    </div>
                          <label class="control-label col-md-1">
                                            Quốc tịch
                                        </label> 
                                        <div class="col-md-3">
                                            <asp:DropDownList runat="server" ID="ddlQuocTich" CssClass="form-control" Enabled="false"></asp:DropDownList>
                                    </div>
                          <label class="control-label col-md-1">
                                            Dân tộc
                                        </label> 
                                        <div class="col-md-3">
                                            <asp:DropDownList runat="server" ID="ddlDanToc" CssClass="form-control" Enabled="false"></asp:DropDownList>
                                    </div>
                                    </div>
                      <div class="form-group">
                                        <label class="control-label col-md-1">
                                            Tỉnh
                                        </label> 
                                        <div class="col-md-3">
                                            <asp:DropDownList runat="server" ID="ddlTinh" CssClass="form-control" Enabled="false"></asp:DropDownList>
                                    </div>
                          <label class="control-label col-md-1">
                                            Huyện
                                        </label> 
                                        <div class="col-md-3">
                                            <asp:DropDownList runat="server" ID="ddlHuyen" CssClass="form-control" Enabled="false"></asp:DropDownList>
                                    </div>
                          <label class="control-label col-md-1">
                                            Xã
                                        </label> 
                                        <div class="col-md-3">
                                            <asp:DropDownList runat="server" ID="ddlXa" CssClass="form-control" Enabled="false"></asp:DropDownList>
                                    </div>
                                    </div>
                      <div class="form-group">
                                        <label class="control-label col-md-1">
                                           Chi tiết địa chỉ
                                        </label> 
                                        <div class="col-md-11">
                                            <asp:TextBox runat="server" ID="txtDiaChi" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    </div>
                      <div class="form-group">
                                        <label class="control-label col-md-1">
                                            Phân loại đơn
                                        </label> 
                                        <div class="col-md-3">
                                            <asp:DropDownList runat="server" ID="ddlPhanLoaiDon" CssClass="form-control" Enabled="false"></asp:DropDownList>
                                    </div>
                          <label class="control-label col-md-1">
                                            Loại KNTC
                                        </label> 
                                        <div class="col-md-3">
                                            <asp:DropDownList runat="server" ID="ddlLoaiKNTC" CssClass="form-control" Enabled="false"></asp:DropDownList>
                                    </div>
                          <label class="control-label col-md-1">
                                            Chi tiết KNTC
                                        </label> 
                                        <div class="col-md-3">
                                            <asp:DropDownList runat="server" ID="ddlChiTietKNTC" CssClass="form-control" Enabled="false"></asp:DropDownList>
                                    </div>
                                    </div>
                      <div class="form-group">
                                        <label class="control-label col-md-1">
                                            Nội dung đơn
                                        </label> 
                                        <div class="col-md-11">
                                            <asp:TextBox runat="server" ID="txtNoiDungDon" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                    </div>
                      </div>
              </div>
              <div class="divInsert">
                  <h4><b><i>Thông tin xử lý</i></b></h4>
                  <div class="form-horizontal">
                                    <div class="form-group">
                                        <label class="control-label col-md-2">
                                            Ngày xử lý
                                        </label> 
                                        <div class="col-md-4">
                                            <asp:TextBox runat="server" ID="txtNgayXuLy" CssClass="form-control datepicker"></asp:TextBox>
                                    </div>
                                        <label class="control-label col-md-2">
                                            Ngày ban hành
                                        </label> 
                                        <div class="col-md-4">
                                            <asp:TextBox runat="server" ID="txtNgayBanHanh" CssClass="form-control datepicker" Enabled="false"></asp:TextBox>
                                    </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-md-2">
                                            Cơ quan xử lý
                                        </label> 
                                        <div class="col-md-4">
                                            <asp:DropDownList runat="server" ID="ddlCoQuanXuLy" CssClass="select2 form-control" DataValueField="CoQuanID" DataTextField="TenCoQuan" style="width:100%"></asp:DropDownList>
                                        </div>
                                        <label class="control-label col-md-2">
                                            Cơ quan giải quyết
                                        </label> 
                                        <div class="col-md-4">
                                            <asp:DropDownList runat="server" ID="ddlCoQuanGiaiQuyet" CssClass="select2 form-control" DataValueField="CoQuanID" DataTextField="TenCoQuan" style="width:100%" Enabled="false"></asp:DropDownList>
                                    </div>
                                    </div>
                      <div class="form-group">
                          <label class="control-label col-md-2">
                                            Hướng xử lý
                                        </label> 
                                        <div class="col-md-4">
                                            <asp:DropDownList runat="server" ID="ddlHuongXuLy" CssClass="form-control" DataValueField="HuongXuLyID" DataTextField="TenHuongXuLy"></asp:DropDownList>
                                        </div>
                          </div>
                      <div class="form-group">
                          <label class="control-label col-md-2">
                          File quyết định
                                        </label> 
                                        <div class="col-md-4">
                                            <asp:FileUpload runat="server" ID="fileUpload" CssClass="form-control btn"></asp:FileUpload>
                                            <br />
                                            <label id="lblFile" style="display:none">Đã có file quyết định, chọn file khác để thay thế</label>
                                        </div>
                          </div>
                                </div>
              </div>

              <label style="padding:2px 7px">
                  Hiển thị          <input type="checkbox" style="width:15px; height:15px;"/>
              </label>
          </div>
          <div class="panel-footer text-center">
              <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Text="Lưu lại" OnClientClick="return CheckValidate();" OnClick="btnSubmit_Click" />
                    <button type="button" class="btn btn-danger" onclick="HideAddForm();">Hủy bỏ</button>
          </div>
      </div>
    </div>

    <div id="dongbo" class="tab-pane fade" style="padding-top:5px">
        
       <div class="col-lg-3 col-lg-offset-9" style="text-align:right; margin-bottom:5px;">
       <input type="button" value="Cập nhật" class="btn btn-primary" onclick="CapNhatKNTC();" />
        <input type="button" value="Đồng bộ" class="btn btn-primary" onclick="DongBoKNTC();" style="margin-right:-15px;" />
       </div>

        <table id="records_table" class="table table-bordered table-hover table-responsive">
    <tr>
        <th style="width:5%">STT</th>
                  <th style="width:5%">Số đơn</th>
                  <th style="width:10%">Tên chủ đơn</th>
                  <th style="width:10%">Địa chỉ</th>
                  <th style="width:10%">ND đơn</th>
                  <th style="width:10%">Loại khiếu tố</th>
                  <th style="width:10%">Ngày tiếp nhận</th>
                    <th style="width:10%">CQ tiếp nhận</th>
                    <th style="width:10%">Ngày ban hành QĐ</th>
    </tr>
</table>

        <%--<asp:Button ID="btnCapNhat" runat="server" CssClass="btn btn-primary" OnClick="btnCapNhat_Click" />
        <asp:Button ID="btnDongBo" runat="server" CssClass="btn btn-primary" OnClick="btnDongBo_Click" />--%>
        <%--<asp:UpdatePanel ID="up" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnCapNhat" />
                <asp:AsyncPostBackTrigger ControlID="btnDongBo" />
            </Triggers>
            <ContentTemplate>
            <table id="" class="table table-bordered table-hover table-responsive">
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
                    <th style="width:10%">Ngày ban hành QĐ</th>
                </tr>
                </thead>
            <tbody>
                <asp:Repeater ID="rptDongBo" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td style="text-align:center">
                                <asp:Label ID="lblSTTDongBo" Text="" runat="server" />
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
                          <td style="text-align:left">
                              <%# Eval("NoiDungDon") %>

                          </td>
                            <td style="text-align:left">
                                <%# Eval("PhanLoaiDon") %>

                            </td>
                            <td style="text-align:left">
                                <%# Eval("NgayTiepNhan")==null ? "" : Com.Gosol.CMS.Utility.Format.FormatDate(Com.Gosol.CMS.Utility.Utils.ConvertToDateTime(Eval("NgayTiepNhan").ToString(),DateTime.MinValue)) %>
                            </td>
                            <td style="text-align:left">
                                <%# Eval("CoQuanTiepNhan") %>

                            </td>
                            <td style="text-align:left">
                                <%# Eval("NgayBanHanh")==null ? "" : Com.Gosol.CMS.Utility.Format.FormatDate(Com.Gosol.CMS.Utility.Utils.ConvertToDateTime(Eval("NgayBanHanh").ToString(),DateTime.MinValue)) %>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
            </table>
                <br />
                <asp:Label runat="server" ID="lblDongBo"></asp:Label>
            </ContentTemplate>
        </asp:UpdatePanel>--%>
        
    </div>

      <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="syncNotify" class="modal fade">
        <div class="modal-dialog  modal-md">
            <div class="modal-content">
                <div class="modal-header bg-primary">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">
                        <i class="glyphicon glyphicon-bullhorn"></i>
                        Thông báo!
                    </h4>
                </div>
                <div class="modal-body">
                    <asp:Label ID="Label1" runat="server" Style="color: #008d4c">
                    Đồng bộ thành công!</asp:Label>
                </div>
                <div class="modal-footer" style="text-align: center">
                    <button type="button" class="btn btn-sm btn-danger" onclick="HideSyncNotify();">
                        Đóng</button><br />
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
    </div>

    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="successNotify" class="modal fade">
        <div class="modal-dialog  modal-md">
            <div class="modal-content">
                <div class="modal-header bg-primary">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">
                        <i class="glyphicon glyphicon-bullhorn"></i>
                        Thông báo!
                    </h4>
                </div>
                <div class="modal-body">
                    <asp:Label ID="lblContentSuccess" runat="server" Style="color: #008d4c">
                    Cập nhật thành công!</asp:Label>
                </div>
                <div class="modal-footer" style="text-align: center">
                    <button type="button" class="btn btn-sm btn-danger" onclick="HideSuccessNotify();">
                        Đóng</button><br />
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
    </div>

    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="errorNotify" class="modal fade">
        <div class="modal-dialog  modal-md">
            <div class="modal-content">
                <div class="modal-header bg-primary">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">
                        <i class="glyphicon glyphicon-bullhorn"></i>
                        Thông báo!
                    </h4>
                </div>
                <div class="modal-body">
                    <asp:Label ID="txtError" runat="server">
                    Cập nhật thất bại!</asp:Label><div class="jquery-msgbox-buttons">
                    </div>
                    <div class="modal-footer" style="text-align: center">
                        <button type="button" class="btn btn-sm btn-danger" onclick="HideErrorNotify();">
                            Đóng</button>
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
    </div>

    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="deleteConfirm" class="modal fade">
        <div class="modal-dialog  modal-md">
            <div class="modal-content">
                <div class="modal-header bg-primary">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">
                        <i class="glyphicon glyphicon-bullhorn"></i>
                        Thông báo!
                    </h4>
                </div>
                <div class="modal-body">
                    <span>Bạn có chắn chắn muốn xóa?</span>
                </div>
                <div class="modal-footer" style="text-align: center">
                    <asp:Button ID="btnDelete" runat="server" Text="Đồng ý" CssClass="btn btn-primary btn-sm" OnClick="btnDelete_Click" />
                    <button type="button" class=" btn btn-danger btn-sm" role="button" aria-disabled="false" onclick="hideDeleteConfirm();">
                        <span class="ui-button-text">Hủy bỏ</span>
                    </button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

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

    <script type="text/javascript">
        function ShowSuccessNotify() {
            $("#successNotify").modal();
        };

        function ShowErrorNotify() {
            $("#errorNotify").modal();
        };

        function ConfirmDelete() {
            $("#deleteConfirm").modal();
        }

        function HideSuccessNotify() {
            $("#successNotify").modal("hide");
        }

        function HideErrorNotify() {
            $("#errorNotify").modal("hide");
        }

        function HideDeleteConfirm() {
            $("#deleteConfirm").modal("hide");
        }

        function HideSyncNotify() {
            $("#syncNotify").modal("hide");
        }
    </script>
    <link href="/AdminLte/ValidateForm/css/template.css" rel="stylesheet" type="text/css" />
    <link href="/AdminLte/ValidateForm/css/validationEngine.jquery.css" rel="stylesheet" type="text/css" />
    <script src="/AdminLte/ValidateForm/js/jquery.validationEngine.js" type="text/javascript"></script>
    <script src="/AdminLte/ValidateForm/js/jquery.validationEngine-vi.js" type="text/javascript"></script>
    <script type="text/javascript">
        jQuery(document).ready(function () {
            // binds form submission and fields to the validation engine
            jQuery("#Form1").validationEngine({ promptPosition: "topLeft" });
        });
    </script>
</asp:Content>
