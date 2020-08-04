<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoockScreen.aspx.cs" Inherits="Com.Gosol.CMS.Web.LoockScreen" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <meta charset="character_set">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>Lockscreen</title>
    <link rel="shortcut icon" href="/images/favicon_ttcp.ico">
    <!-- Tell the browser to be responsive to screen width -->
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
    <!-- Bootstrap 3.3.6 -->
    <link rel="stylesheet" href="/AdminLte/bootstrap/css/bootstrap.min.css">
    <!-- Font Awesome -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.5.0/css/font-awesome.min.css">
    <!-- Ionicons -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/ionicons/2.0.1/css/ionicons.min.css">
    <!-- Theme style -->
    <link rel="stylesheet" href="/AdminLte/dist/css/AdminLTE.min.css">

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
  <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
  <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
  <![endif]-->
</head>
<body class="hold-transition lockscreen" style="height: auto !important;">
    <!-- Automatic element centering -->
    <div class="lockscreen-wrapper">
        <div class="lockscreen-logo">
            <a href="#"><b>khóa màn hình</b>LTE</a>
        </div>
        <!-- User name -->
        <div class="lockscreen-name">
            <asp:Label ID="UserName" runat="server"></asp:Label></div>

        <!-- START LOCK SCREEN ITEM -->
        <div class="lockscreen-item">
            <!-- lockscreen image -->
            <div class="lockscreen-image">
                <img src="/images/avatar-default1.png" alt="User Image">
            </div>
            <!-- /.lockscreen-image -->

            <!-- lockscreen credentials (contains the form) -->
            <form id="lockscreen" runat="server" class="lockscreen-credentials">
                <asp:Panel ID="pn" runat="server" DefaultButton="lbtnLogin">
                    <div class="input-group">
                        <%--<input type="password" class="form-control" placeholder="mật khẩu">--%>
                        <asp:TextBox ID="txtPassWord" runat="server" TextMode="Password" class="form-control" placeholder="mật khẩu"></asp:TextBox>

                        <div class="input-group-btn">
                            <asp:LinkButton ID="lbtnLogin" runat="server" OnClick="btnLogin_Click">
                                <button type="button" class="btn"><i class="fa fa-arrow-right text-muted"></i></button>
                                <asp:Button ID="btnLogin" runat="server" class="btn" Style="display: none" OnClick="btnLogin_Click" />
                            </asp:LinkButton>
                        </div>
                    </div>
                    
                </asp:Panel>
            </form>
            <!-- /.lockscreen credentials -->

        </div>
        <p id="error_div" runat="server" style="display: none;text-align:center">
                        <img src="images/icon_note.png" alt="" width="16" height="16" style="vertical-align: text-bottom;"/>
                        <asp:Label ID="lblError" Text="" runat="server" CssClass="lblError" />
                    </p>
        <!-- /.lockscreen-item -->
        <div class="help-block text-center">
            Nhập mật khẩu của bạn để tiếp tục làm việc
 
        </div>
        <div class="text-center">
            <a href="login.aspx">Đăng nhập bằng tài khoản khác</a>
        </div>
        <div class="lockscreen-footer text-center">
            Copyright &copy; 2017 <b><a href="#" class="text-black">Gosolution JSC</a></b><br>
            All rights reserved
 
        </div>
    </div>
    <!-- /.center -->

    <!-- jQuery 2.2.3 -->
    <script src="/AdminLte/plugins/jQuery/jquery-2.2.3.min.js"></script>
    <!-- Bootstrap 3.3.6 -->
    <script src="/AdminLte/bootstrap/js/bootstrap.min.js"></script>
</body>
</html>
