<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="QLDonThu.aspx.cs" Inherits="Com.Gosol.CMS.Web.Webapp.Backend.DonThu.QLDonThu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="script1" runat="server" EnablePartialRendering="true">
    </asp:ScriptManager>
    <link href="/AdminLte/jquery.formvalidation/css/formValidation.min.css" rel="stylesheet" />
    <%--<link href="/AdminLte/plugins/select2/select2.min.css" rel="stylesheet" />--%>

    <script src="/AdminLte/jquery.formvalidation/js/formValidation.min.js"></script>
    <script src="/AdminLte/jquery.formvalidation/js/framework/bootstrap.min.js"></script>
    <%--<script src="/AdminLte/plugins/select2/select2.min.js" type="text/javascript"></script>--%>

    <link href="/Styles/dropdownlist/chosen.min.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/dropdownlist/chosen.jquery.js" type="text/javascript"></script>
    <script src="../../ckeditor/sample.js"></script>
    <script src="../../../ckeditor/adapters/jquery.js"></script>
    <script src="../../../ckeditor/ckeditor.js"></script>
    <script type="text/javascript">
        function hideMessage() {
            var messageDiv = $("#<%= lblMsg.ClientID %>");
            if (messageDiv.is(":visible")) {
                setTimeout(function () {
                    messageDiv.hide(300);
                }, 2000);
            }
        }

        $(document).ready(function () {
            $("#checkall").change(function () {
                var ischecked = $(this).is(':checked');

                if (ischecked) {
                    $('.checkboxDT').prop("checked", true);
                    $('.checkboxDT').addClass('checkedRow');
                } else {
                    $('.checkboxDT').prop("checked", false);
                    $('.checkboxDT').removeClass('checkedRow');
                }
            });


            $(".js-example-basic-single").select2({
            });
            var config = {
                '.chosen': {}
            }
            for (var selector in config) {
                $(selector).chosen(config[selector]);

            }
            $(".chosen").trigger("chosen:updated");

            //checkValidation();
            setInterval(hideMessage, 2000);

            $('#txtSearch').keypress(function (e) {
                var key = e.which;
                if (key == 13) {
                    $('#btnSearch').click();
                }
            });


        });
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            checkValidation();

            setTimeout(function () {
                $("#successSubmit").modal("hide");
                $("#error").modal("hide");
                $("#mesgAddGroup").modal("hide");
                $("#thongBaoAddSuccess").modal("hide");
                $("#thongBaoEditSuccess").modal("hide");
                $("#thongBaoDeleteError").modal("hide");
            }, 2000);
        });

        $(function () {
            $(window).load(function () {
                setTimeout(function () {
                    $("#successSubmit").modal("hide");
                    $("#error").modal("hide");
                    $("#mesgAddGroup").modal("hide");
                    $("#thongBaoAddSuccess").modal("hide");
                    $("#thongBaoEditSuccess").modal("hide");
                    $("#thongBaoDeleteError").modal("hide");
                }, 2000);
            })
        });
    </script>

    <script type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_initializeRequest(InitializeRequest);
        prm.add_endRequest(EndRequest);
        var postBackElement;
        function InitializeRequest(sender, args) {
            if (prm.get_isInAsyncPostBack()) {
                args.set_cancel(true);
            }
            $("#pleaseWaitDialog").modal();
        }
        function EndRequest(sender, args) {
            $("#pleaseWaitDialog").modal("hide");
        }

    </script>

    <script type="text/javascript">
        function initValidationRule(field, rules, i, options) {
            return false;
        }

        function hideError() {
            $("#error").modal("hide");
        }

        function hideDeleteError() {
            $("#deleteError").modal("hide");
        }


        function hideSubmitError() {
            $("#submitError").modal("hide");
        }

        function hideAddGroupForm() {
            $("#addGroupForm").modal("hide");
        }

        function showAddForm() {
            $("#myModal").modal("show");
            return false;
        }

        function hideAddEditForm() {
            loadForm();
            $('#Form1').bootstrapValidator('resetForm', true);
        }

        function showthongBaoSuccess() {
            $("#successSubmit").modal();
            return false;
        }
        function showThongBaoAddSuccess() {
            $("#thongBaoAddSuccess").modal();
            return false;
        }
        function showthongBaoEditSuccess() {
            $("#thongBaoEditSuccess").modal();
            return false;
        }

        function hideThongBaoAddSuccess() {
            $("#thongBaoAddSuccess").modal();
            return false;
        }
        function hidethongBaoEditSuccess() {
            $("#thongBaoEditSuccess").modal();
            return false;
        }


        function hideSuccessSubmit() {
            $("#successSubmit").modal("hide");

        }

        function showthongBaoError() {
            $("#thongBaoError").modal();
            return false;
        }
        function hidethongBaoError() {
            $("#thongBaoError").modal("hide");
        }

        function showthongBaoDeleteError() {
            $("#thongBaoDeleteError").modal();
            return false;
        }
        function hidethongBaoDeleteError() {
            $("#thongBaoDeleteError").modal("hide");
        }



        function hideSuccessMsg() {
            $("#success").modal("hide");
        }

        function ConfirmDelete(button) {
            $("#deleteConfirm").modal();
            $("#MainContent_hdDeleteID").val($(button).next().val());
            return false;
        }

        function hideDeleteConfirm() {
            $("#deleteConfirm").modal("hide");
            $("#MainContent_hdDeleteID").val(0);
        }

        function ConfirmRefresh(button) {
            $("#refreshConfirm").modal("show");
            $("#MainContent_hdRefreshID").val($(button).next().next().next().val());
            return false;
        }

        function hideRefreshConfirm() {
            $("#refreshConfirm").modal("hide");
            $("#MainContent_hdRefreshID").val(0);
        }

        function ConfirmDeleteGroup(button) {
            $("#groupDeleteConfirm").modal("show");
            $("#MainContent_hdDeleteGroupID").val($(button).next().val());
            return false;
        }

        function hideDeleteGroupConfirm() {
            $("#groupDeleteConfirm").modal("hide");
        }

        function selectUser(tr, userID, event) {
            var sender = event.target;
            if (sender.tagName == "TD") {
                //add selected style and remove selected style of other rows
                $(tr).addClass("selected_hl");
                $(tr).siblings().removeClass("selected_hl");
                //change hidden ddl value
                $("#MainContent_ddlNguoiDung").val(userID);
                $("#MainContent_ddlNguoiDung").change();
            }
        }

        function StopParentEvent(event, control) {
            if (!$(control).parent().parent().hasClass("selected_hl")) {
                event.stopPropagation();
            }
        }


        function showLogIn() {
            $("#loginModal").modal();
        };

        function showMesGroup() {
            $("#mesgAddGroup").modal();
        };
        function hideMesgAddGroup() {
            $("#mesgAddGroup").modal("hide");
        };


        function checkValidation() {
            $('#Form1').formValidation({
                framework: 'bootstrap',
                button: {
                    selector: '#MainContent_btnSave',
                    disabled: 'disabled'
                },
                //excluded: ':disabled',
                icon: {
                    valid: 'glyphicon glyphicon-ok',
                    invalid: 'glyphicon glyphicon-remove',
                    validating: 'glyphicon glyphicon-refresh'
                },
                fields: {
                    ctl00$MainContent$CKEditorNoiDungTraLoi: {
                        validators: {
                            notEmpty: {
                                message: 'Tên loại tin không được để trống'
                            },
                        },
                    },
                },
            })
                //.find('[name="ctl00$MainContent$CKEditorNoiDungTraLoi"]')
                //.ckeditor()
                .editor
            // To use the 'change' event, use CKEditor 4.2 or later
            //.on('change', function () {
            // Revalidate the bio field
            $('#profileForm').formValidation('revalidateField', 'ctl00$MainContent$CKEditorNoiDungTraLoi');
            //});
        };

        function loadForm() {
            $("#MainContent_ddlLinhVuc").val("").trigger('change');
            $("#MainContent_txtNoiDungCauHoi").prop("disabled", false);
            $("#MainContent_hdfIDTraLoiEdit").val("0");
            $("#MainContent_txtNoiDungCauHoi").val("");
            $('#MainContent_CKEditorNoiDungTraLoi').val("");
            $('#MainContent_checkPublic').removeAttr('checked');
            $("#spanTenFile").text("");
        }

        function showFormEdit(idTinTuc) {
            loadForm();
            console.log(idTinTuc);
            $("#MainContent_hdfCauHoiID").val(idTinTuc);
            $.ajax({
                type: "POST",
                url: "TraLoiCauHoi.aspx/GetByID",
                data: '{idTraLoiCauHoi:"' + idTinTuc + '"}',
                dataType: "json",
                async: "true",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    var json = eval('(' + data.d + ')');
                    if (json != null) {
                        console.log(json);
                        $("#MainContent_hdfIDTraLoiEdit").val(json.IDTraLoi);
                        $("#MainContent_hdfCauHoiID").val(json.IDCauHoi);
                        $("#MainContent_txtNoiDungCauHoi").val(json.NDCauHoi);
                        $("#MainContent_txtNoiDungCauHoi").prop("disabled", true);
                        $('#MainContent_CKEditorNoiDungTraLoi').val(json.NDTraLoi);
                        if (json.Public == true) {
                            $('#MainContent_checkPublic').prop('checked', true);
                        }
                        if (json.laTinHot == true) {
                            $('#MainContent_checkLaTinHot').prop('checked', true);
                        }

                        $("#MainContent_ddlLinhVuc").val(json.IDLinhVuc).trigger('change');
                        $("#MainContent_ddlLinhVuc").val(json.IDLinhVuc).prop("disabled", true);
                        $(".chosen").trigger("chosen:updated");
                        showAddForm();
                    }
                }
            });
        };

        
        function CheckboxSelected(rowid) {
            var obj = $("#" + rowid);
            var ischecked = obj.is(':checked');
            if (ischecked) {
                obj.addClass('checkedRow');
            } else {
                obj.removeClass('checkedRow');
            }

        }

        function saveDB() {
            var listRow = document.getElementsByClassName("checkedRow");
            var dataDonThu = "";
            for (var i = 0; i < listRow.length; i++) {
                //console.log(listRow[i].parentElement.parentElement);
                cells = listRow[i].parentElement.parentElement.getElementsByTagName('td');

                for (var j = 2; j < cells.length; j++) {
                    if (j == 2) {
                        dataDonThu += cells[2].children[0].innerText;
                        dataDonThu += ";" + cells[2].children[1].innerText;
                        dataDonThu += ";" + cells[2].children[2].innerText;
                        dataDonThu += ";" + cells[2].children[3].innerText;
                        dataDonThu += ";" + cells[2].children[4].innerText;

                    } else {
                        dataDonThu += ";" + cells[j].innerText;
                    }
                }
                if (i != cells.length - 1) {
                    dataDonThu += "@";
                }
                //console.log('2', dataDonThu);
            }
            $.ajax({
                type: "POST",
                url: "QLDonThu.aspx/SaveDB",
                data: '{dataDonThu:"' + dataDonThu + '"}',

                async: "true",
                contentType: "application/json; charset=utf-8",
                success: function (data1) {
                   
                    var XLDIDstr = data1.d;
                    $.ajax({
                        type: "POST",
                        //url: 'http://192.168.100.42:10008/api/UpdateSyncStatus',
                        url: '<%= System.Configuration.ConfigurationManager.AppSettings["APIUrl_UpdateSyncStatus"].ToString() %>',
                        data: {'XLDIDstr':XLDIDstr},

                        async: "true",
                        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
                        success: function (data2) {
                            window.location.reload(false);
                            if (data2 == 1) {
                                alert('Đồng bộ thành công!')
                            } else {
                                alert('Đồng bộ không thành công')
                            }
                        },
                        error: function (data1) {
                            alert('Đồng bộ không thành công')
                        }
                    });
                    

                },
                error: function (data1) {
                    alert('Đồng bộ không thành công')
                }
            });
        }
        function sync() {
            //var url = 'http://192.168.100.42:10008/api/getportalsync';
            var url = '<%= System.Configuration.ConfigurationManager.AppSettings["APIUrl_GetPortalSync"].ToString() %>';
            $.ajax({
                url: url,
                type: "GET",
                success: function (data) {
                    if (data != null && data != "") {
                        listDonThu = JSON.stringify({
                            'listDonThu': data
                        });
                        //$.ajax({
                        //    type: "POST",
                        //    url: "QLDonThu.aspx/InsertDonThu",
                        //    data: listDonThu,
                        //    dataType: "json",
                        //    async: "true",
                        //    contentType: "application/json; charset=utf-8",
                        //    success: function (data1) {
                        //        window.location.reload(false);
                        //        alert(data1.d);
                        //    },
                        //    error: function (data1) {
                        //        console.log('fail1');
                        //    }
                        //});
                        $("#tmplShowDTDongBo > tbody").html("");

                        for (var i = 0; i < data.length; i++) {
                            var tmpl = "<tr class='rowDonThu'>" + $("#tmplCTDTDB tr:first-child").html() + "</tr>";
                            tmpl = tmpl.replace(/_STT_/g, i + 1);
                            tmpl = tmpl.replace(/_row_/g, i + 1);
                            tmpl = tmpl.replace(/_pSoDon_/g, data[i].SoDonThu);
                            tmpl = tmpl.replace(/_pXuLyDonID_/g, data[i].XuLyDonID);
                            tmpl = tmpl.replace(/_pNgayTiepNhan_/g, data[i].NgayNhapDonStr);
                            tmpl = tmpl.replace(/_pCoQuanTiepNhan_/g, data[i].TenCoQuanTiepNhan);
                            tmpl = tmpl.replace(/_pCanBoTiepNhan_/g, data[i].TenCanBoTiepNhan);
                            tmpl = tmpl.replace(/_pCoQuanID_/g, data[i].CoQuanID);
                            
                            var phanLoaiDon = "";

                            if (data[i].TenLoaiKhieuTo1 != "" && data[i].TenLoaiKhieuTo1 != null) {
                                phanLoaiDon += data[i].TenLoaiKhieuTo1;

                                if (data[i].TenLoaiKhieuTo2 != "") {
                                   
                                    phanLoaiDon += ">" + data[i].TenLoaiKhieuTo2;
                                }
                                if (data[i].TenLoaiKhieuTo3 != "") {
                                   
                                    phanLoaiDon += ">" + data[i].TenLoaiKhieuTo3;
                                }

                                tmpl = tmpl.replace(/_pPhanLoaiDon_/g, phanLoaiDon);
                            }
                            else {
                                tmpl = tmpl.replace(/_pPhanLoaiDon_/g, " ");
                            }
                            tmpl = tmpl.replace(/_pNoiDungDon_/g, data[i].NoiDungDon);
                            var divNguoiDaiDien = "";
                            if (data[i].lsDoiTuongKN != null && data[i].lsDoiTuongKN.length > 0) {

                                for (var j = 0; j < data[i].lsDoiTuongKN.length; j++) {


                                    if (data[i].lsDoiTuongKN[j].HoTen != null && data[i].lsDoiTuongKN[j].HoTen != "") {
                                        if (j == 0) {
                                            divNguoiDaiDien += data[i].lsDoiTuongKN[j].HoTen;
                                        }
                                        else {
                                            divNguoiDaiDien += ", " + data[i].lsDoiTuongKN[j].HoTen;
                                        }
                                    }

                                }

                            }
                            tmpl = tmpl.replace(/_pNguoiDaiDien_/g, divNguoiDaiDien);
                            tmpl = tmpl.replace(/_pCoQuanXuLy_/g, data[i].TenCoQuanXL);
                            tmpl = tmpl.replace(/_pTrangThaiDonThu_/g, data[i].TrangThaiDonThu);
                            tmpl = tmpl.replace(/_pPhongBanXuLy_/g, data[i].TenPhongBanXuLy);
                            
                            if (data[i].NhomKNInfo != null) {
                                if (data[i].NhomKNInfo.StringLoaiDoiTuongKN == "CaNhan") {
                                    tmpl = tmpl.replace(/_pDoiTuongKhieuNai_/g, "Cá nhân");

                                }
                                if (data[i].NhomKNInfo.StringLoaiDoiTuongKN == "CoQuan") {

                                    tmpl = tmpl.replace(/_pDoiTuongKhieuNai_/g, "Cơ quan, tổ chức");
                                }
                                if (data[i].NhomKNInfo.StringLoaiDoiTuongKN == "TapThe") {
                                    tmpl = tmpl.replace(/_pDoiTuongKhieuNai_/g, "Tập thể");
                                }

                            }
                            else {
                                tmpl = tmpl.replace(/_pDoiTuongKhieuNai_/g, "");
                            }
                            $("#tmplShowDTDongBo > tbody").append(tmpl);

                        }

                        $("#tmplDonThuRow").show();
                    }
                    else {
                        console.log('null');
                    }
                },
                error: function (data) {
                    console.log('fail');
                }
            });
        };


    </script>


    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="thongBaoError" class="modal fade">
        <div class="modal-dialog  modal-md">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Thông báo!</h4>
                </div>
                <div class="modal-body">
                    <img alt="" src="../images/Error.png" style="width: 26px; margin-left: 7px;" />
                    <asp:Label ID="lblThongBaoError" runat="server" Style="color: red"></asp:Label>
                </div>
                <div class="modal-footer" style="text-align: center">
                    <button type="button" onclick="hidethongBaoError();" class="btn btn-danger btn-sm">
                        Đóng</button><br />
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
    </div>

    <asp:HiddenField runat="server" ID="hdfIDTraLoiEdit" />




    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="successRSPass" class="modal fade">
        <div class="modal-dialog  modal-md">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Thông báo!</h4>
                    <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
                </div>
                <div class="modal-body">
                </div>

                <div class="modal-footer" style="text-align: center">
                    <button type="button" onclick="hideSuccessMsg();">
                        Đóng</button><br />
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="successSubmit" class="modal fade">
        <div class="modal-dialog  modal-md">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Thông báo!</h4>
                </div>
                <div class="modal-body">
                    <asp:Label ID="lblContentSuccess" runat="server" Style="color: #008d4c"></asp:Label>
                </div>
                <div class="modal-footer" style="text-align: center">
                    <button type="button" onclick="hideSuccessSubmit();">
                        Đóng</button><br />
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
    </div>

    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="submitError" class="modal fade">
        <div class="modal-dialog  modal-md">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Thông báo!</h4>
                </div>
                <div class="modal-body">
                    <asp:Label ID="txtError" runat="server"></asp:Label><div class="jquery-msgbox-buttons">
                    </div>
                    <div class="modal-footer" style="text-align: center">
                        <button type="button" onclick="hideSubmitError();">
                            Đóng</button>
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
    </div>

    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="thongBaoDeleteError" class="modal fade">
        <div class="modal-dialog  modal-md">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Thông báo!</h4>
                </div>
                <div class="modal-body">
                    <img alt="" src="../images/Error.png" style="width: 26px; margin-left: 7px;" />
                    <asp:Label ID="lblthongBaoDeleteError" runat="server" Style="color: red"></asp:Label>
                </div>
                <div class="modal-footer" style="text-align: center">
                    <button type="button" onclick="hideSubmitError();">
                        Đóng</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <!-- Content Header (Page header) -->
    <div class="content-header">
        <h1>Trả lời câu hỏi
            <%--        <small>(Tổng ng dùng)</small>--%>
        </h1>
        <ol class="breadcrumb">
            <li><a href="/Default.aspx"><i class="fa fa-dashboard"></i>Trang chủ</a></li>
            <li><a href="#">Đơn thư</a></li>
            <li class="active">Quản lý đơn thư</li>
        </ol>
    </div>

    <div class="content">
        <div style="display: none;">
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
        </div>
        <div class="row">
            <div class="col-xs-12">
                <div class="box">
                    <div class="box-header">
                        <%--<h3 class="box-title">Hover Data Table</h3>--%>
                        <button type="button" class="btn btn-primary" id="btnSync" onclick="sync(); return false">
                            <span class="glyphicon glyphicon glyphicon-plus-sign" style="margin-right: 5px"></span>Cập nhật
                        </button>
                        <button type="button" class="btn btn-primary" id="btnSaveDB" onclick="saveDB(); return false">
                            <span class="glyphicon glyphicon glyphicon-plus-sign" style="margin-right: 5px"></span>Đồng bộ
                        </button>
                        <asp:Label runat="server" ID="lblSync"> </asp:Label>

                    </div>
                    <div>
                        <div id="tmplDonThuRow" style="display: none">
                            <table id="tmplShowDTDongBo" class="table">
                                <thead>
                                    <tr>
                                        <th>
                                            <input type="checkbox" id='checkall' name="check_row_" class="checkboxAll" /></th>
                                        <th style="width: auto; text-align: center">STT
                                        </th>
                                        <th style="width: auto; text-align: center">Số đơn
                                        </th>
                                        <th style="width: auto; text-align: center">Ngày tiếp nhận
                                        </th>
                                        <th style="width: auto; text-align: center">Cơ quan tiếp nhận
                                        </th>
                                        <th style="width: auto; text-align: center">Cán bộ tiếp nhận
                                        </th>
                                        <th style="width: auto; text-align: center">Phân loại đơn
                                        </th>
                                        <th style="width: auto; text-align: center">Nội dung đơn
                                        </th>
                                        <th style="width: auto; text-align: center">Người đại diện
                                        </th>
                                        <th style="width: auto; text-align: center">Cơ quan xử lý
                                        </th>
                                        <th style="width: auto; text-align: center">Trạng thái đơn thư
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                </tbody>
                            </table>
                            <table id="tmplCTDTDB" style="display: none">
                                <tr>
                                    <td>
                                        <input type="checkbox" id='check_row_' name="check_row_" class="checkboxDT" onchange="CheckboxSelected('check_row_');" /></td>
                                    <td style="text-align: center;">_STT_
                                    </td>
                                    <td style="text-align: center;">
                                        <label id='sodon_row_' class="check_dt_row_">_pSoDon_</label>
                                        <label id='xulydonid_row_' class="check_dt_row_" style="display: none">_pXuLyDonID_</label>
                                        <label style="display: none">_pDoiTuongKhieuNai_</label>
                                        <label style="display: none">_pPhongBanXuLy_</label>
                                        <label style="display: none">_pCoQuanID_</label>
                                    </td>
                                    <td style="text-align: center;">_pNgayTiepNhan_
                                    </td>
                                    <td style="text-align: left;">_pCoQuanTiepNhan_
                                    </td>
                                    <td style="text-align: left;">_pCanBoTiepNhan_
                                    </td>
                                    <td style="text-align: left;">_pPhanLoaiDon_
                                    </td>
                                    <td style="text-align: left;">_pNoiDungDon_
                                    </td>
                                    <td style="text-align: left;">_pNguoiDaiDien_
                                    </td>
                                    <td style="text-align: left;">_pCoQuanXuLy_
                                    </td>
                                    <td style="text-align: left;">_pTrangThaiDonThu_
                                    </td>

                                </tr>
                            </table>

                        </div>
                    </div>
                    <div class="content-body">
                        <div class="box-body">
                            <!-- message area -->
                            <div class="box-header" style="padding: 0px;">
                                <div class="col-lg-4" style="padding: 0px">
                                    <asp:TextBox ID="txtSearch" placeholder="Nhập số đơn thư hoặc người đại diện" AutoPostBack="True" OnTextChanged="txtSearch_TextChanged" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-lg-4" style="padding-left: 5px">
                                    <asp:Button ID="btnSearch" runat="server" CausesValidation="false" CssClass="btn btn-default btn-sm" OnClick="btnSearch_Click" Text="Tìm kiếm" />
                                </div>
                            </div>
                            <div class="table-responsive">
                                <table id="table" class="table table-bordered table-hover" style="margin-top: 15px; width: 100%">
                                    <thead>
                                        <tr>
                                            <th style="width: auto; text-align: center">STT
                                            </th>
                                            <th style="width: auto; text-align: center">Số đơn
                                            </th>
                                            <th style="width: auto; text-align: center">Ngày tiếp nhận
                                            </th>
                                            <th style="width: auto; text-align: center">Cơ quan tiếp nhận
                                            </th>
                                            <th style="width: auto; text-align: center">Cán bộ tiếp nhận
                                            </th>
                                            <th style="width: auto; text-align: center">Phân loại đơn
                                            </th>
                                            <th style="width: auto; text-align: center">Nội dung đơn
                                            </th>
                                            <th style="width: auto; text-align: center">Người đại diện
                                            </th>
                                            <th style="width: auto; text-align: center">Cơ quan xử lý
                                            </th>
                                            <th style="width: auto; text-align: center">Trạng thái đơn thư
                                            </th>
                                            <th style="width: auto; text-align: center; width:100px">Thao tác
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="rptDonThu" runat="server" OnItemDataBound="rptDonThu_ItemDataBound" OnItemCommand="rptDonThu_ItemCommand">
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="text-align: center;">
                                                        <asp:Label runat="server" ID="lblSTT"></asp:Label>
                                                    </td>
                                                    <td style="text-align: center;">
                                                        <%# Eval("SoDonThu") %>
                                                    </td>
                                                    <td style="text-align: center;">
                                                        <asp:Label runat="server" ID="lblNgayTiepNhan"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <%# Eval("CoQuanTiepNhan").ToString().Length>=100?Eval("CoQuanTiepNhan").ToString().Substring(0,99) + " ... .":Eval("CoQuanTiepNhan").ToString() %>
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <%#Eval("CanBoTiepNhan").ToString().Length>=100?Eval("CanBoTiepNhan").ToString().Substring(0,99) + " ... .":Eval("CanBoTiepNhan").ToString() %>
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <%# Eval("PhanLoaiDon") %>
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <%#Eval("NoiDungDon").ToString().Length>=100?Eval("NoiDungDon").ToString().Substring(0,99) + " ... .":Eval("NoiDungDon").ToString() %>
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <%#Eval("NguoiDaiDien").ToString().Length>=100?Eval("NguoiDaiDien").ToString().Substring(0,99) + " ... .":Eval("NguoiDaiDien").ToString() %>
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <%# Eval("CoQuanXuLy") %>
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <%# Eval("TrangThaiDonThu") %>
                                                    </td>
                                                    <td style="text-align: center; vertical-align: middle; width:100px" class="action-cell">

                                                        <asp:LinkButton ID="btnPublic" runat="server" CommandName="Public" CommandArgument='<%# Eval("XuLyDonID") %>' 
                                                         
                                                            CssClass="btn btn-primary btn-sm"></asp:LinkButton>

                                                        <asp:HiddenField ID="hdfDelete" runat="server" Value='<%# Eval("XuLyDonID") %>' />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                                <div class="paginations" style="margin-top: 15px; margin-bottom: 15px">
                                    <asp:PlaceHolder ID="plhPaging" runat="server"></asp:PlaceHolder>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- end #dashboard -->
    </div>

    <link href="/Styles/uploadfile/uploadify.css" rel="stylesheet" />
    <script src="/scripts/uploadfile/jquery.uploadify.js" type="text/javascript"></script>
    <script type="text/javascript">
        function showDetailQues(index) {
            var summary = $('#ndCauHoi' + index).html();
            var detail = $('#detailCauHoi' + index).val();
            $('#ndCauHoi' + index).html(detail);
            $('#detailCauHoi' + index).val(summary);
        };
        function showDetailAnws(index) {
            var summary = $('#ndCauTraLoi' + index).html();
            var detail = $('#detailCauTraLoi' + index).val();
            $('#ndCauTraLoi' + index).html(detail);
            $('#detailCauTraLoi' + index).val(summary);
        };
    </script>
</asp:Content>
