<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CanBoManage.aspx.cs" Inherits="Com.Gosol.CMS.Web.CanBoManage" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager runat="server" ID="ScriptManager1">
    </asp:ScriptManager>
    <link href="/Styles/dropdownlist/chosen.min.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/dropdownlist/chosen.jquery.js" type="text/javascript"></script>


    <style type="text/css">
        .tables-noborder {
            width: 80%;
            margin: 0 auto;
        }

            .tables-noborder tr td {
                height: 50px;
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
        });
    </script>
    <script type="text/javascript">
        var dialog_title = "<img src='images/edit-add.png' style='vertical-align:middle;' /> Thêm/Sửa Cán bộ";
        var checkAvailableUrl = "checkNhanVienAvailable";
        var saveDbUrl = "saveNhanVien";
        var fetchDetailUrl = "editNhanVien";
        // must have for all page
        function initValidationRule(field, rules, i, options) {
            // config ajax rule for a field
            //if (field.attr("id") == "name_frm")
            //{
            //var rule = options.allrules[rules[i + 1]];
            //rule.url = checkAvailableUrl;
            //rule.extraData = "";
            //rule.extraDataDynamic = "#isNew_frm,#oldName_frm";
            //rule.alertTextOk = "Có thể dùng";
            //rule.alertText = "* Đã tổn tại thành phần xã hội này";
            //rule.alertTextLoad = "* đang kiểm tra dữ liệu, xin chờ giây lát....";
            //}
            return false;
        }



        function ShowAddCanBoForm() {
            $("#addCanBoForm").modal();

            return false;
        }

        function HideAddCanBoForm() {
            $("#addCanBoForm").modal("hide");
            ResetForm();
            return false;
        }

        function ResetForm() {
            $("#MainContent_light").hide();
            $("#MainContent_fade").hide();

            $("#MainContent_hdCanBoID").val("");
            $("#MainContent_txtTenCanBo").val("");
            $("#MainContent_txtNgaySinh").val("");
            $("#MainContent_ddlGioiTinh").val("0");
            $("#MainContent_txtDiaChi").val("");
            $("#MainContent_ddlCoQuan").val("0");
            $("#MainContent_cbxQuyenKy").prop("checked", false);
            $("#MainContent_txtEmail").val("");
            $("#MainContent_txtDienThoai").val("");
            //$("#Form1").validationEngine("hideAll");
        }

        function showthongBaoSuccess() {
            $("#messageContent").html("Cập nhật dữ liệu thành công.");
            $("#messageBox").modal();
        }

        function showthongBaoError() {
            $("#messageContent").html("Xảy ra lỗi trong quá trình cập nhật. Vui lòng thử lại sau.");
            $("#messageBox").modal();
        }

        function ConfirmDelete(button) {
            $("#confirm-delete").modal();
            $("#MainContent_hdDeleteID").val($(button).next().val());
            return false;
        }

        function hideDeleteConfirm() {
            $("#confirm-delete").modal("hide");
            $("#MainContent_hdDeleteID").val(0);
        }

        function showMessageBox(message) {
            $("#messageContent").html(message);
            $("#messageBox").modal();
        }

        function hideMessageBox() {
            $("#messageContent").html("");
            $("#messageBox").modal("hide");
        }
    </script>

    <div id="fade" class="black_overlay" runat="server">
    </div>

    <div id="fade2" class="black_overlay">
    </div>

    <!-- Content Header (Page header) -->
    <section class="content-header">
      <h1>
        Khai báo cán bộ
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="/Default.aspx"><i class="fa fa-dashboard"></i> Trang chủ</a></li>
        <li><a href="#">Hệ thống</a></li>
        <li class="active">Khai báo cán bộ</li>
      </ol>
    </section>

    <!-- Main content -->
    <section class="content">
      <div class="row">
        <div class="col-xs-12">
          <div class="box">
            <div class="box-header">
              <%--<h3 class="box-title">Hover Data Table</h3>--%>
                <button type="button" class="btn btn-primary" onclick="ShowAddCanBoForm(); return false;"><span class="glyphicon glyphicon glyphicon-plus-sign" style="margin-right: 5px;"></span>Thêm cán bộ</button>
            </div>
            <!-- /.box-header -->
            <div class="box-body table-responsive">
                <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnSearch">
                                <div class="col-sm-4" style="padding: 5px; width:auto">
                                    <asp:DropDownList ID="ddlLoai" runat="server" CssClass="form-control"  AutoPostBack="true" OnSelectedIndexChanged="ddlLoai_SelectedIndexChanged" Visible="false">
                                        <asp:ListItem Text="Backend" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Frontend" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <asp:Button ID="btnSearch" runat="server" CausesValidation="false" CssClass="btn btn-default btn-sm" Style="float: right; margin-right: 5px; margin-bottom: 10px"
                                    OnClick="btnSearch_Click" Text="Tìm kiếm" />
                                <asp:TextBox ID="txtSearch" runat="server" placeholder="Nhập nội dung cần tìm kiếm" CssClass="form-control input-search" Style="float: right; margin-right: 10px; margin-bottom: 10px; width: 30%"></asp:TextBox>
                            </asp:Panel>
              <table id="example2" class="table table-bordered table-hover">
                <thead>
                <tr>
                  <th style="width:20%; text-align:center">Tên cán bộ</th>
                  <th style="width:10%; text-align:center">Ngày sinh</th>
                  <th style="width:10%; text-align:center">Giới tính</th>
                  <th style="width:30%; text-align:center">Địa chỉ</th>
                  <th style="width:20%; text-align:center">Cơ quan</th>
                    <th style="width:10%; text-align:center">Thao tác</th>
                </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptCanBo" runat="server" OnItemDataBound="rptCanBo_ItemDataBound"
                    OnItemCommand="rptCanBo_ItemCommand">
                    <ItemTemplate>
                        <tr>
                          <td style="text-align:left"><%# Eval("TenCanBo") %></td>
                          <td>
                              <asp:Label ID="lblNgaySinh" runat="server"></asp:Label>
                          </td>
                          <td>
                              <asp:Label ID="lblGioiTinh" runat="server"></asp:Label>
                          </td>
                          <td style="text-align:left"> 
                              <%# Eval("DiaChi") %>
                          </td>
                          <td style="text-align:left">
                              <%# Eval("TenCoQuan") %>
                          </td>
                            <td style="text-align:center">
                                <%--<a href='NguoiDungManage.aspx?canboID=<%# Eval("CanBoID") %>' tooltip="Thêm người dùng" width="16" height="16" style="vertical-align: -3px;">
                                    <asp:Image runat="server" ImageUrl="~/images/add_user.png" Width="16" Height="16" />
                                </a>--%>
                                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.png" CommandName="Edit" CommandArgument='<%# Eval("CanBoID") %>' CausesValidation="false" ToolTip="Sửa" width="20px" style="margin-right: 5px;"/>
                                <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.png" CommandName="Delete"
                                    CommandArgument='<%# Eval("CanBoID") %>' OnClientClick='return ConfirmDelete(this);'
                                    CausesValidation="false" ToolTip="Xóa" width="20px" style=""/>
                                <asp:HiddenField ID="hdCanBoID" runat="server" Value='<%# Eval("CanBoID") %>' />
                            </td>
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
    <!-- /.content -->

    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="addCanBoForm" style="overflow: auto;" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Thêm mới/Sửa thông tin cán bộ</h4>
                </div>
                <div class="modal-body">
                    <table style="" class="tables-noborder">
                        <tr>
                            <th class="field_label lblText right-align" style="width: 25%">
                                <span style="color: red;">*</span> Tên cán bộ:
                                <asp:HiddenField ID="hdCanBoID" runat="server" />
                            </th>
                            <td colspan="2" style="width: 75%">
                                <asp:TextBox ID="txtTenCanBo" class="form-control" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <!-- ngaySinh -->
                        <tr>
                            <th class="field_label lblText right-align">Ngày sinh:
                            </th>
                            <td colspan="2">
                                <asp:TextBox ID="txtNgaySinh" runat="server" class="datepicker form-control"></asp:TextBox>
                            </td>
                        </tr>
                        <!-- gioiTinh -->
                        <tr>
                            <th class="field_label lblText right-align">Giới tính:
                            </th>
                            <td colspan="2">
                                <asp:DropDownList ID="ddlGioiTinh" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="Chọn giới tính" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Nam" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Nữ" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <!-- diaChi -->
                        <tr>
                            <th class="field_label lblText right-align">Địa chỉ:
                            </th>
                            <td colspan="2">
                                <div id="wwctrl_diaChi_frm" class="wwctrl">
                                    <asp:TextBox ID="txtDiaChi" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                        <!-- bo phan -->
                        <tr>
                            <th class="field_label lblText right-align">
                                <span style="color: red;">*</span> Tên cơ quan:
                            </th>
                            <td colspan="2">
                                <asp:DropDownList ID="ddlCoQuan" runat="server" DataValueField="CoQuanID" DataTextField="TenCoQuan" CssClass="validate[required] selectFrm chosen form-control">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th class="field_label lblText right-align">
                                <span style="color: red;">*</span> Chức vụ:
                            </th>
                            <td colspan="2">
                                <asp:DropDownList ID="ddlChucVu" runat="server" DataValueField="ChucVuID" DataTextField="TenChucVu"
                                    CssClass="validate[required] form-control" name="ddlChucVu">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th class="field_label lblText right-align">Email:
                            </th>
                            <td colspan="2">
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="validate[custom[email]] form-control"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th class="field_label lblText right-align">Điện thoại
                            </th>
                            <td colspan="2">
                                <asp:TextBox ID="txtDienThoai" runat="server" CssClass="validate[custom[phone]] form-control"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="display:none">
                            <th class="field_label lblText right-align">Quyền ký:
                            </th>
                            <td colspan="2">
                                <asp:CheckBox ID="cbxQuyenKy" runat="server" />
                            </td>
                        </tr>
                        <tr style="display:none">
                            <th class="field_label lblText right-align">Vai trò
                            </th>
                            <td colspan="2">
                                <asp:DropDownList runat="server" ID="ddlVaiTro" CssClass="form-control">
                                    <asp:ListItem Text="Chuyên viên" Value="3" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Lãnh đạo phòng" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Lãnh đạo đơn vị" Value="1"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">&nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnSubmit" runat="server" CssClass="save btn btn-primary validate_button" Text="Lưu lại" OnClick="btnSubmit_Click" />
                    <button type="button" class="btn btn-default" data-dismiss="modal" onclick="HideAddCanBoForm();">Đóng</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="confirm-delete" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Thông báo</h4>
                </div>
                <div class="modal-body">
                    <p>Bạn có muốn xóa cán bộ này không?&hellip;</p>
                    <asp:HiddenField ID="hdDeleteID" runat="server" Value="0" />
                </div>
                <div class="modal-footer">
                    <asp:Button runat="server" CssClass="btn btn-primary" Text="Đồng ý" OnClick="btnDelete_Click"
                        ID="btnDelete"></asp:Button>
                    <button type="button" class="btn btn-default" data-dismiss="modal" onclick="hideDeleteConfirm();">Không</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="messageBox" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Thông báo</h4>
                </div>
                <div class="modal-body">
                    <span id="messageContent"></span>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal" onclick="hideDeleteConfirm();">Đóng</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

</asp:Content>
