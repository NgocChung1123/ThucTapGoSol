<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="Com.Gosol.CMS.Web.Webapp.Backend.Test.Test" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("button").click(function () {
                alert("AAAAAAAAAAA");
                $("h2").text("Text changed!");
            });
        });
        function ChangeText() {
            $("h2").val("");
        }
    </script>
    <div>
        <h2>Hello world</h2>
        <button id="btnTest" style="color:red; font-size:20px" onclick="return false;">Click Me</button>
        
    </div>
</asp:Content>
