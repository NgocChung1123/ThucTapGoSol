<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuenMatKhau.aspx.cs" Inherits="Com.Gosol.CMS.Web.QuenMatKhau" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <!-- HTTP 1.1 -->
    <meta http-equiv="Cache-Control" content="no-store" />
    <!-- HTTP 1.0 -->
    <meta http-equiv="Pragma" content="no-cache" />
    <!-- Prevents caching at the Proxy Server -->
    <meta http-equiv="Expires" content="0" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="generator" content="AppFuse 2.0" />
    <!-- leave this for stats please -->
    <link rel="icon" value="/images/favicon.ico" />
    <link rel="stylesheet" type="text/css" media="all" href="styles/style.css" />
    <link href="/Styles/new_style.css" rel="stylesheet" />

    <link href="/AdminLte/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <!-- Font Awesome -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.5.0/css/font-awesome.min.css">
    <a href="/AdminLte/fonts/fontawesome-webfont.woff2"></a>
    <!-- Ionicons -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/ionicons/2.0.1/css/ionicons.min.css">
    <!-- Theme style -->
    <link href="/AdminLte/dist/css/AdminLTE.min.css" rel="stylesheet" />
    <!-- AdminLTE Skins. Choose a skin from the css/skinsfolder instead of downloading all of them to reduce the load. -->
    <link href="/AdminLte/dist/css/skins/_all-skins.min.css" rel="stylesheet" />
    <!-- iCheck -->
    <link href="/AdminLte/plugins/iCheck/flat/blue.css" rel="stylesheet" />
    <!-- Morris chart -->
    <link href="/AdminLte/plugins/morris/morris.css" rel="stylesheet" />
    <!-- jvectormap -->
    <link href="/AdminLte/plugins/jvectormap/jquery-jvectormap-1.2.2.css" rel="stylesheet" />
    <!-- Date Picker -->
    <link href="/AdminLte/plugins/datepicker/datepicker3.css" rel="stylesheet" />
    <!-- Daterange picker -->
    <link href="/AdminLte/plugins/daterangepicker/daterangepicker.css" rel="stylesheet" />
    <!-- bootstrap wysihtml5 - text editor -->
    <link href="/AdminLte/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css" rel="stylesheet" />

    <link href="/AdminLte/jquery.formvalidation/css/formValidation.min.css" rel="stylesheet" />
    <link href="/AdminLte/plugins/select2/select2.min.css" rel="stylesheet" />
    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
  <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
  <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
  <![endif]-->
    <!-- jQuery 2.2.3 -->
    <script type="text/javascript" src="/AdminLte/plugins/jQuery/jquery-2.2.3.min.js"></script>
    <!-- jQuery UI 1.11.4 -->
    <script type="text/javascript" src="https://code.jquery.com/ui/1.11.4/jquery-ui.min.js"></script>
    <!-- Resolve conflict in jQuery UI tooltip with Bootstrap tooltip -->
    <script type="text/javascript">
        $.widget.bridge('uibutton', $.ui.button);
    </script>
    <!-- Bootstrap 3.3.6 -->
    <script type="text/javascript" src="/AdminLte/bootstrap/js/bootstrap.min.js"></script>

    <link rel="Stylesheet" type="text/css" href="styles/smoothness/jquery-ui-1.7.1.custom.css" />
    <link rel="stylesheet" type="text/css" href="styles/markitup.css" />
    <link type="text/css" rel="stylesheet" href="styles/jquery-ui.css" />
    <link type="text/css" rel="stylesheet" href="styles/form.css" />
    <link type="text/css" rel="stylesheet" href="styles/sidemenu.css" />
    <link type="text/css" rel="stylesheet" href="styles/messages.css" />
    <link type="text/css" rel="stylesheet" href="Styles/custom_calendar.css" />
    <link href="Styles/dropdownlist/chosen.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="scripts/jquery-1.6.min.js"></script>
    <script type="text/javascript" src="scripts/jquery-ui-1.8.16.custom.min.js"></script>
    <script type="text/javascript" src="scripts/jquery.localisation-min.js"></script>
    <script type="text/javascript" src="scripts/excanvas.pack.js"></script>
    <script type="text/javascript" src="scripts/jquery.flot.pack.js"></script>
    <script type="text/javascript" src="scripts/jquery.markitup.pack.js"></script>
    <script type="text/javascript" src="scripts/set.js"></script>
    <script type="text/javascript" src="scripts/custom.js"></script>
    <script src="Scripts/dropdownlist/chosen-vi.jquery.js" type="text/javascript"></script>
    <!--	<script type="text/javascript" src="scripts/ui.multiselect.js"></script>-->
    <!-- datepicker tieng viet -->
    <script type="text/javascript" src="scripts/jquery.ui.datepicker-vi.js"></script>
    <!-- Cookie -->
    <script type="text/javascript" src="scripts/jquery.cookie.js"></script>
    <!-- Validattion Engine -->
    <link rel="stylesheet" type="text/css" media="all" href="styles/validationEngine.jquery.css" />
    <script type="text/javascript" src="scripts/jquery.validationEngine-vi.js" charset="utf-8"></script>
    <script type="text/javascript" src="scripts/jquery.validationEngine.js" charset="utf-8"></script>
    <!-- hotkeys -->
    <script type="text/javascript" src="scripts/jquery.hotkeys.js"></script>
    <!-- masked input-->
    <script type="text/javascript" src="scripts/jquery.maskedinput-1.3.js"></script>
    <script type="text/javascript" src="Scripts/ttcp_chitietdonthu.js"></script>
    <script type="text/javascript">

        var originalHeight;
        var hideMsgTimers = new Array();

        // initialise plugins
        $(document).ready(function () {

            $('.validate_button').click(function () {
                var tenNguoiDung = $("#txtTenNguoiDung").val();
                var email = $("#txtEmail").val();
                if (tenNguoiDung == '' || email == '') {
                    $("#lblError").text('Tên người dùng hoặc Email không đưuọc để trống');
                    return false;
                }

                return true;
                $("#lblError").text('');
            });

        });


        function clearForm() {

            $("#txtTenNguoiDung").val('');
            $("#txtEmail").val('');
            $("#lblMessage").text('');
            $("#lblError").text('');

        }

    </script>
</head>
<body style="background: #f1f1f1">
    <a href="#" id="go_top" style="display: none"></a>
    <div id="msgFade" class="black_overlay">
    </div>
    <%--<div id="msgFace" class="black_overlay" style="z-index:9999;display:none" >
    </div>--%>
    <form runat="server">
        <div class="wrapper">
            <header class="main-header">
            <!-- Logo -->
           
            <!-- Header Navbar: style can be found in header.less -->
            <nav class="navbar navbar-static-top" style="background: url(../../../../images/Layer_6.png) left center no-repeat #3c8dbc;">
      
                <!-- Sidebar toggle button-->
              <a href="#" class="sidebar-toggle" data-toggle="offcanvas" role="button">
                <span class="sr-only"></span>
              </a>
                <div class="navbar-custom-menu div-header" style="">
                    <div style="height: 100%;float:left;margin-right: 5px;">
                        <img src="/images/logo_thanhtra.png" style="height: 45px;margin-top: 3px;">
                    </div>
                    <div class="title-header" style="float:left;width:90%;height:100%;line-height:25px">
                        <span id="lblTenCoQuan" style="font-size: 15px;font-weight: bold;color: #fff;text-transform:uppercase;font-family:'Times New Roman'"></span><br>
                        <span style="font-size: 16px;color: yellow;font-weight: bold;text-transform:uppercase;font-family:'Times New Roman'">HỆ THỐNG CỔNG THÔNG TIN KNTC</span>
                    </div>
                  </div>
              <div class="navbar-custom-menu username">
                <ul class="nav navbar-nav">
                  <!-- Notifications: style can be found in dropdown.less -->
                  <li class="dropdown notifications-menu" style="display:none;">
                    <a href="#" class="dropdown-toggle" data-toggle="dropdown" onclick="ShowNotify(); return false;">
                      <i class="fa fa-bell-o"></i>
                      <span class="label label-warning"><span class="notify_sum "></span></span>
                    </a>

                  </li>
                  <!-- User Account: style can be found in dropdown.less -->

                </ul>
              </div>
            </nav>
            </header>


            <div style="width: 800px; margin: 0 auto; padding: 15px; margin-top: 100px;">
                <div class="box">
                    <div class="box-header">
                        <div style="text-align: center; font-weight: 500; font-size: 18px; line-height: 1;">Yêu cầu cấp lại mật khẩu</div>
                    </div>
                    <div class="box-body">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-lg-3 col-md-3 control-label">Tên người dùng <span style="color: red">(*)</span></label>
                                <div class="col-lg-9 col-md-9">
                                    <asp:TextBox ID="txtTenNguoiDung" runat="server" Width="400"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-lg-3 col-md-3 control-label">Email <span style="color: red">(*)</span></label>
                                <div class="col-lg-9 col-md-9">
                                    <asp:TextBox ID="txtEmail" runat="server" Width="400"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-lg-3 col-md-3 control-label"></label>
                                <asp:Label runat="server" ID="lblMessage" Text="" Style="color: green;"></asp:Label>
                                <asp:Label runat="server" ID="lblError" Text="" Style="color: red;"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="box-footer">
                        <div class="form-horizontal" style="text-align: center">
                            <asp:Button runat="server" Text="Gửi yêu cầu" CssClass="btn btn-primary btn-sm validate_button" OnClick="btnSend_Click" />
                            <button id="btnHuy" class="btn btn-danger btn-sm" onclick="clearForm(); return false;">Nhập lại</button>
                        </div>
                    </div>

                </div>
            </div>
        </div>
        <div class="footer">
        </div>
    </form>
</body>
</html>
