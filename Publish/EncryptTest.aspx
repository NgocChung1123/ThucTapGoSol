<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EncryptTest.aspx.cs" Inherits="Com.Gosol.CMS.Web.EncryptTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Original String: <asp:TextBox ID="txtOriginalText" runat="server" Width="400"></asp:TextBox><br />
        EncryptString: <asp:Label ID="lblEncryptText" runat="server"></asp:Label><br />
        DecryptString: <asp:Label ID="lblDecryptText" runat="server"></asp:Label><br />
        <asp:Button ID="btnEncrypt" runat="server" Text="Encrypt" OnClick="btnEncrypt_Click" />
        <asp:Button ID="btnDecrypt" runat="server" Text="Decrypt" OnClick="btnDecrypt_Click" />
    </div>
    </form>
</body>
</html>
