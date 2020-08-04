<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Com.Gosol.CMS.Web.Login" EnableEventValidation="false"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="css/login_style.css" />
    <script type="text/javascript" src="jquery/jquery-1.8.2.min.js"></script>
    <script type="text/javascript" src="jquery/jqueryEasing.js"></script>
</head>
<body>
    <div class="wrapper2">   
        <div style="height: 200px;">
        </div>     
        <div class="login">
            <div id="header1">
                <img style="margin-left: -45px; margin-top: 5px" src="/image/logodemo.png" />
                <img style="float: right; margin-top: -55px" src="/image/login_header.png" />
            </div>
            <!--header1-->
            <div class="clr">
            </div>
            <div id="col1">
                <h2>
                    Đăng nhập hệ thống quản trị</h2>
                <p>
                    Đăng nhập bằng tài khoản của người quản trị hệ thống để truy cập các chức năng quản
                    trị hệ thống.</p>                
            </div>
            <!--col1-->
            <div id="col2">
                <form runat="server" method="post" autocomplete="on">
                <div id="user">
                    <input type="text" id="txtUsername" name="username" runat="server" placeholder="Tài khoản" />                    
                </div>                
                <div id="password">
                    <input type="password" id="txtPassword" name="password" runat="server" placeholder="Mật khẩu" />                
                </div>     
                <div class="clr"></div>
                <div class="error" id="password_error" runat="server">
                </div>
                <input id="a1" type="checkbox" runat="server" />                
                <p id="nhotoi">
                    Nhớ tôi</p>
                <asp:Button ID="btnLogin"  runat="server" Text="Đăng nhập" CssClass="send" onclick="btnLogin_Click" />
                </form>
            </div>
            <!--col2-->
        </div>
        <!--login-->
        <div class="clr"></div>
    </div>
    <!--wrapper-->
</body>
</html>
