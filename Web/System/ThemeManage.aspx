<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ThemeManage.aspx.cs" Inherits="Com.Gosol.CMS.Web.ThemeManage" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="scr1" runat="server" EnablePartialRendering="true">
    </asp:ScriptManager>
    <style type="text/css">
        .ul-config
        {
            padding: 10px;
        }
        
        .ul-config > li:first-child
        {
            border-top: 1px solid #dedede;
        }
        
        .ul-config li
        {
            padding: 30px;
            border: 1px solid #dedede;
            border-top: 0 none;
        }
        
        .table-theme-config
        {
            width: 100%;
        }
        
        .no-actDiv
        {
            display: none !important;
        }
    </style>
    <div id="main_panel_container" class="left" style="width: 99%">
        <div class="dashboard">
     
                    <div class="confirmDiv message" id="thongbaoSucces_div" >
                        <div class="header-message">
                            <label id="lblHeaderSuccess" class="header-message">
                                Thông báo
                            </label>
                            <img src="images/close.ico" class="img-close" onclick="HideMessage(this);" />
                        </div>
                        <div class="content-message">
                            <img alt="" src="../images/messagebox_info.png" style="width: 30px; margin-left: 7px; margin-top: 14px;" /><asp:Label ID="lblContentSuccess" CssClass="content-message"
                                runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="confirmDiv message" id="thongbaoError_div" >
                        <div class="header-message">
                             <label id="lblHeaderErr" class="header-message">
                            Lỗi
                        </label>
                            <img src="images/close.ico" class="img-close" onclick="HideMessage(this);" />
                        </div>
                        <div class="content-message">
                            <img alt="" src="../images/Error.png" style="width: 26px; margin-left: 7px; margin-top: 14px;" /><asp:Label ID="lblContentErr" CssClass="content-message" runat="server"></asp:Label>
                        </div>
                    </div>
            <div class="ico_list">
                <label class="h2">
                    Cấu hình giao diện
                </label>
            </div>
            <div class="content-body">
                <fieldset style="width: 600px">
                    <div style="width: 100%;">
                        <asp:HiddenField ID="hdfThemeConfigID" runat="server" />
                        <ul class="ul-config" id="ul-config">
                            <li>
                                <table class="table-theme-config">
                                    <tr>
                                        <td style="width: 100px">
                                            Logo
                                        </td>
                                        <td style="width: 60%; text-align: center">
                                            <asp:Image Style="cursor: pointer" ID="imgUnitLogo" Width="85px" Height="88px" ImageUrl=""
                                                runat="server" />
                                        </td>
                                        <td>
                                            <asp:FileUpload ID="fileUpload" runat="server" />
                                            <asp:HiddenField ID="hdfFileUpload" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </li>
                            <li>
                                <table class="table-theme-config">
                                    <tr>
                                        <td style="width: 100px">
                                            Tên đơn vị
                                        </td>
                                        <td style="width: 60%; text-align: center">
                                            <asp:TextBox ID="txtUnitName" CssClass="validate[required]" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </li>
                            <li>
                                <table class="table-theme-config">
                                    <tr>
                                        <td style="width: 100px">
                                            Giao diện
                                        </td>
                                        <td style="width: 60%; text-align: center">
                                            <asp:TextBox ID="txtUnitThemeName" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </li>
                            <li>
                                <table class="table-theme-config">
                                    <tr>
                                        <td style="width: 100px" rowspan="1">
                                            Hotline1
                                        </td>
                                        <td style="width: 60%; text-align: center">
                                            <asp:TextBox ID="txtHomePhone" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100px" rowspan="2">
                                            Hotline2
                                        </td>
                                        <td style="width: 60%; text-align: center">
                                            <asp:TextBox ID="txtPhone" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </li>
                        </ul>
                        <div style="margin-left:10px">
                            <asp:Button ID="btnSave" CssClass="save validate_button" OnClick="btnSave_Click" Text="Lưu lại" runat="server"></asp:Button>
                        </div>
                    </div>
                </fieldset>
            </div>
        </div>
    </div>
    <div id="ajax_fade" class="black_overlay" style="z-index: 9000">
    </div>
      <div id="fade2" class="black_overlay">
       
        </div>
    <script type="text/javascript">
        $(document).ready(function () {
            //            $(".ul-config > li").hover(function () {
            //                $(".hdSaveDiv").css("display", "none");
            //                var current_div = $(this).find(".hdSaveDiv");
            //                current_div.css("display","block");
            //            });
        });

        function readURL(input, imgname) {
            var file_name = "No file selected...";
            var err = 0;
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                var size = input.files[0]["size"];
                var file_id = $(input).attr("id");
                if (!(/\.(gif|jpg|jpeg|tiff|png)$/i).test(input.files[0]["name"])) {
                    err = 1;
                }
                if (err == 0) {
                    reader.onload = function (e) {
                        $('#' + imgname).attr('src', e.target.result);
                    }
                    reader.readAsDataURL(input.files[0]);
                    file_name = input.files[0]["name"];
                } else {
                    $("#" + file_id).val("");
                    $('#' + imgname).attr('src', 'UploadFiles/quochuy.png');
                }
            } else {
                $('#' + imgname).attr('src', 'UploadFiles/quochuy.png');
            }
            return file_name;
        }

        $("#MainContent_fileUpload").change(function () {
            readURL(this, 'MainContent_imgUnitLogo');
        });

        $('#MainContent_imgUnitLogo').click(function () {
            $("#MainContent_fileUpload").click();
            return false;
        });

      
    </script>
</asp:Content>
