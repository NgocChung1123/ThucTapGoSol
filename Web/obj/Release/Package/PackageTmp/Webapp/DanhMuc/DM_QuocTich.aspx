<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DM_QuocTich.aspx.cs" Inherits="Com.Gosol.CMS.Web.SoftWare.DanhMuc.DM_QuocTich" EnableEventValidation="false"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
 <asp:ScriptManager ID="script1" runat="server" EnablePartialRendering="true">
    </asp:ScriptManager>
    <script type="text/javascript">

        $(document).ready(function () {        
            checkValidation();

            setTimeout(function () {
                hidethongBaoSuccess();
                hidethongBaoSuccess();
            }, 3000);
        });

        function showAddEditForm() {
            $("#addEditForm").modal();
            return false;
        }
        function hideAddEditForm() {
            $("#addEditForm").modal("hide");
            clearForm();
        }

        function showthongBaoSuccess() {
            $("#msgSuccess").modal();
            return false;
        }
        function hidethongBaoSuccess() {
            $("#msgSuccess").modal("hide");
        }

        function showthongBaoError() {
            $("#msgError").modal();
            return false;
        }

        function hidethongBaoError() {
            $("#msgError").modal("hide");
        }

        function showFormEdit(quocTichID) {
            $.ajax({
                type: "POST",
                url: "DM_QuocTich.aspx/GetByID",
                data: '{quocTichID:"' + quocTichID + '"}',
                dataType: "json",
                async: "true",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    var json = eval('(' + data.d + ')');
                    if (json != null) {
                        console.log(json);
                        $("#MainContent_hdfQuocTichID").val(quocTichID);
                        $("#MainContent_txtTenQuocTich").val(json.TenQuocTich);
                        showAddEditForm();
                    }
                }
            });
        };

        function clearForm() {
            $("#MainContent_hdfQuocTichID").val("0");
            $("#MainContent_txtTenQuocTich").val("");
        }

        function showFormDelete(quocTichID) {
            $("#MainContent_hdfDelete").val(quocTichID);
            $("#mesDeleteForm").modal();
            return false;
        };


        function checkValidation() {
            $('#Form1').formValidation({
                framework: 'bootstrap',
                button: {
                    selector: '#MainContent_btnSubmit',
                    disabled: 'disabled'
                },
                //excluded: ':disabled',
                icon: {
                    valid: 'glyphicon glyphicon-ok',
                    invalid: 'glyphicon glyphicon-remove',
                    validating: 'glyphicon glyphicon-refresh'
                },
                fields: {
                    ctl00$MainContent$txtTenQuocTich: {
                        validators: {
                            notEmpty: {
                                message: 'Tên quốc tịch không được để trống'
                            },
                        }
                    },
                },
            })
        };
    </script>
    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="addEditForm" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Thêm mới/Sửa thông tin quốc tịch</h4>
                </div>
                <div class="modal-body">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <label class="col-lg-3 col-sm-3 control-label">
                                <span style="color: red;">*</span> Tên quốc tịch:
                                <asp:HiddenField ID="hdfQuocTichID" runat="server" />
                            </label>
                            <div class="col-lg-9">
                                <asp:TextBox ID="txtTenQuocTich" class="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary btn-sm" Text="Lưu lại" OnClick="btnSubmit_Click" />
                    <button type="button" class="btn btn-default" data-dismiss="modal" onclick="hideAddEditForm();">Đóng</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="mesDeleteForm" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Thông báo!</h4>
                    <asp:HiddenField ID="hdfDelete" runat="server" />
                </div>
                <div class="modal-body">
                    <img alt="" src="../../images/Error.png" style="width: 30px; margin-left: 7px;" /><asp:Label ID="Label1" ForeColor="Red" CssClass="content-message" Text="Bạn có chắc chắn muốn xóa thông tin này không"
                        runat="server"></asp:Label>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-primary btn-sm" Text="Đồng ý" OnClick="btnDelete_Click" />
                    <button type="button" class="btn btn-default" data-dismiss="modal" onclick="hideDeleteForm();">Đóng</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" id="msgSuccess" class="modal fade">
        <div class="modal-dialog  modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Thông báo!</h4>
                </div>
                <div class="modal-body">
                    <asp:Label ID="lblMesSuccess" ForeColor="Green" CssClass="content-message" runat="server"></asp:Label>
                </div>
                <div class="modal-footer" style="text-align: center">
                    <button type="button" onclick="hidethongBaoSuccess();" class="btn btn-default btn-sm">
                        Đóng</button>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
    </div>

    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" id="msgError" class="modal fade">
        <div class="modal-dialog  modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Thông báo!</h4>
                </div>
                <div class="modal-body">
                    <asp:Label ID="lblMesError" ForeColor="Red" CssClass="content-message"
                        runat="server"></asp:Label>
                </div>
                <div class="modal-footer" style="text-align: center">
                    <button type="button" onclick="hidethongBaoError();" class="btn btn-default btn-sm">
                        Đóng</button>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
    </div>
    <div class="content-header">
        <h1>Danh mục quốc tịch
        <small></small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="/Default.aspx"><i class="fa fa-dashboard"></i>Trang chủ</a></li>
            <li><a href="#">Hệ thống</a></li>
            <li class="active">Danh mục quốc tịch</li>
        </ol>
    </div>

    <div class="content">
        <div class="row">
            <div class="col-xs-12">
                <div class="box">
                    <div class="box-header">
                        <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary" OnClientClick="showAddEditForm(); return false;" Text="Thêm quốc tịch" />
                        <%--<button type="button" class="btn btn-primary" onclick="showAddEditForm(); return false;">Thêm quốc tịch</button>--%>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body table-responsive">
                        <div class="box-header">
                            <asp:Button ID="btnSearch" runat="server" CausesValidation="false" CssClass="btn btn-default btn-sm" Style="float: right; margin-right: 5px; margin-bottom: 10px"
                                OnClick="btnSearch_Click" Text="Tìm kiếm" />
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" Style="float: right; margin-right: 10px; margin-bottom: 10px; width: 30%"></asp:TextBox>
                        </div>
                        <table id="table" class="table" style="margin-top: 15px">
                            <thead>
                                <tr>
                                    <th style="width: 50px;">STT
                                    </th>
                                    <th>Tên quốc tịch
                                    </th>

                                    <th style="width: 70px">Thao tác
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="rptQuocTich" runat="server" OnItemDataBound="rptQuocTich_ItemDataBound">
                                    <ItemTemplate>
                                        <tr class="<%# Container.ItemIndex % 2 == 0 ? "even" : "odd" %>">
                                            <td>
                                                <%# Container.ItemIndex + 1 %>
                                            </td>
                                            <td style="text-align: left">
                                                <asp:Label ID="lblTenQuocTich" runat="server" Text='<%# Eval("TenQuocTich") %>'></asp:Label>
                                            </td>

                                            <td style="text-align: center">
                                                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/edit.png" OnClientClick='<%# "showFormEdit(" + Eval("QuocTichID") +  "); return false;" %>' ToolTip="Sửa" />
                                                <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/images/delete.png"  OnClientClick='<%# "showFormDelete(" + Eval("QuocTichID") +  "); return false;" %>' ToolTip="Xóa" />
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
    </div>
    <!-- /.content -->
</asp:Content>
