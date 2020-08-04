<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Com.Gosol.CMS.Web.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Đăng nhập</title>
    <!-- HTTP 1.1 -->
    <meta http-equiv="Cache-Control" content="no-store" />
    <!-- HTTP 1.0 -->
    <meta http-equiv="Pragma" content="no-cache" />
    <!-- Prevents caching at the Proxy Server -->
    <meta http-equiv="Expires" content="0" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="generator" content="AppFuse 2.0" />
    <!-- leave this for stats please -->
    <link rel="stylesheet" type="text/css" media="all" href="Styles/login.css" />
    <link rel="stylesheet" type="text/css" media="all" href="Styles/displaytag.css" />
    <link rel="Stylesheet" type="text/css" href="Styles/smoothness/jquery-ui-1.7.1.custom.css" />
    <link rel="stylesheet" type="text/css" href="styles/markitup.css" />
    <%--<link rel="stylesheet" type="text/css" href="styles/superfish.css" media="screen" />--%>
    <link type="text/css" rel="stylesheet" href="styles/jquery-ui.css" />
    <script type="text/javascript" src="scripts/jquery-1.6.min.js"></script>
    <script type="text/javascript" src="scripts/jquery-ui-1.8.16.custom.min.js"></script>
    <script type="text/javascript" src="scripts/jquery.localisation-min.js"></script>
    <script type="text/javascript" src="scripts/excanvas.pack.js"></script>
    <script type="text/javascript" src="scripts/jquery.flot.pack.js"></script>
    <script type="text/javascript" src="scripts/jquery.markitup.pack.js"></script>
    <script type="text/javascript" src="scripts/set.js"></script>
    <script type="text/javascript" src="scripts/custom.js"></script>
    <!-- Cookie -->
    <script type="text/javascript" src="scripts/jquery.cookie.js"></script>
    <!-- superfish menu -->
    <script type="text/javascript" src="scripts/hoverIntent.js"></script>
    <script type="text/javascript" src="scripts/superfish.js"></script>
    <script type="text/javascript" src="scripts/supersubs.js"></script>
    <!-- Beautiful MsgBox -->
    <link rel="stylesheet" type="text/css" media="all" href="styles/jquery.msgbox.css" />
    <script charset="utf-8" type="text/javascript" src="scripts/jquery.msgbox.js"></script>
    <!-- hotkeys -->
    <%--<script type="text/javascript" src="scripts/jquery.hotkeys.js"></script>--%>
    <!-- masked input-->
    <script type="text/javascript" src="scripts/jquery.maskedinput-1.3.js"></script>
    <!-- pretty loader -->
    <link rel="stylesheet" type="text/css" media="all" href="styles/prettyLoader.css" />
    <script charset="utf-8" type="text/javascript" src="scripts/jquery.prettyLoader.js"></script>
    <!-- custom script -->
    <script type="text/javascript" src="scripts/ttcp_all.js"></script>
    <script type="text/javascript" src="scripts/jquery.blockUI.js"></script>

    <%-- // style new login--%>
    <meta http-equiv="Cache-Control" content="no-store" />
    <!-- HTTP 1.0 -->
    <meta http-equiv="Pragma" content="no-cache" />
    <!-- Prevents caching at the Proxy Server -->
    <meta http-equiv="Expires" content="0" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="generator" content="AppFuse 2.0" />
    <!-- leave this for stats please -->
    <%--<link rel="stylesheet" type="text/css" media="all" href="css/login.css" />--%>
    <%--  //end--%>

    <%--<link href="Styles/loginstyle/css/style.css" rel="stylesheet" />--%>
    <link rel="stylesheet" type="text/css" href="/Styles/CSS3FullscreenSlideshow/css/demo.css" />
    <link rel="stylesheet" type="text/css" href="/Styles/CSS3FullscreenSlideshow/css/style1.css" />
    <script src="/Styles/CSS3FullscreenSlideshow/js/modernizr.custom.86080.js" type="text/javascript"></script>
    <style type="text/css">
        /*form {
                width: 380px;
                margin: 4em auto;
                padding: 3em 2em 2em 2em;
                background: #fafafa;
                border: 1px solid #ebebeb;
                box-shadow: rgba(0,0,0,0.14902) 0px 1px 1px 0px, rgba(0,0,0,0.09804) 0px 1px 2px 0px;
            }*/
    </style>

    <script type="text/javascript">
        var originalHeight;
        // initialise plugins		
        $(document).ready(function () {
            // pretty loader
            $.prettyLoader({
                loader: "images/prettyLoader/ajax-loader.gif"
            });
            // superfish 
            $("ul.sf-menu").supersubs({
                minWidth: 12,   // minimum width of sub-menus in em units 
                maxWidth: 50, 	// maximum width of sub-menus in em units 
                extraWidth: 0.3     // extra width can ensure lines don't sometimes turn over 
            }).superfish({
                delay: 400,                            // one second delay on mouseout 
                animation: { opacity: 'show', height: 'show' },  // fade-in and slide-down animation 
                speed: 'fast'                          // faster animation speed			        
            });

            // mask the date input
            $("input.datepicker").mask("99/99/9999");
        });

    </script>
    <style type="text/css">
        #login_header_div {
            color: #3B5998 !important;
        }

        #loginform label {
            color: #3B5998 !important;
            padding-left: 4px;
        }

        #loginform input.text-input:FOCUS {
            background-color: yellow;
        }

        #loginform input#save:hover {
            background-color: yellow;
            color: blue;
        }

        #loginform input#save:focus {
            background-color: yellow;
            color: blue;
        }
        /*.background-custom{
            background-image: url("/images/img_login/background-login.jpg");
            position:fixed;
            width:100%;
            height:100%;
        }*/
    </style>
</head>
<body id="page" style="overflow:hidden;">
   <%-- <div class="background-custom">

    </div>--%>
    <ul class="cb-slideshow">
        <li><span>Image 01</span>
        </li>
        <li><span>Image 02</span>
        </li>
        <li><span>Image 03</span>
        </li>
        <li><span>Image 04</span>
        </li>
        <li><span>Image 05</span>
        </li>
        <li><span>Image 06</span>
        </li>
    </ul>
    <form runat="server" method="post" defaultbutton="lkBtnLogin">
        <div class="swap_login">
            <div class="swap_formlogin">
                <!--name_form_login-->
                <div class="content_loginform">
                    <!--left_form_login-->
                    <div id="div_login" class="right_form_login">
                        <p style="text-align:center">
                            <img src="images/logo_thanhtra.jpg" style="width: 130px;"/>
                        </p>
                        <p>
                            <label class="label_login">Tài khoản</label><br />
                            <asp:TextBox runat="server" TabIndex="1" MaxLength="24" placeholder="Tên đăng nhập" ID="txtUserName" name="u"></asp:TextBox>
                        </p>
                        <p>
                            <label class="label_login">Mật khẩu</label><br />
                            <asp:TextBox runat="server" TabIndex="2" MaxLength="32" placeholder="Mật khẩu" value="" ID="txtPassword" TextMode="Password" name="p"></asp:TextBox>
                        </p>
                        <p id="error_div" runat="server" style="display: none;margin-top: 5px;">
                            <img src="images/icon_note.png" alt="" width="16" height="16" />
                            <asp:Label ID="lblError" Text="" runat="server" CssClass="lblError" />
                        </p>
                        <asp:LinkButton ID="lkBtnLogin" runat="server" CssClass="btndangnhap_login" OnClick="btnLogin_Click">Đăng nhập</asp:LinkButton>
                        <a class="forget_pas_login" href="System/QuenMatKhau.aspx">Quên mật khẩu ?</a>
                        <br />
                        <p class="ho-tro-pm" style="margin-top: 45px;">
                            Điện thoại: 0643 727 801 - Fax: 0643 727 802
                        </p>
                        <p class="ho-tro-pm" style="line-height: 23px">
                            Email: kntc@thanhtra.baria-vungtau.gov.vn
                        </p>
                        <p class="ho-tro-pm" style="margin-top: 10px;">
                            <a href="http://download.teamviewer.com/download/TeamViewer_Setup_vi-ioc.exe" target="_blank">Tải phần mềm teamview</a>
                        </p>
                    </div>
                    <!--right_form_login-->

                </div>
                <!--content_loginform-->
            </div>
            <!--End swap_formlogin-->
        </div>
        <!--End swap_login-->

    </form>
</body>
</html>