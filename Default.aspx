<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Web2APK.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox runat="server" ID="txtURL" TextMode="Url"></asp:TextBox>
            <asp:Button runat="server" ID="btnSubmitUrl" OnClick="btnSubmitUrl_Click" Text="Submit URL" />
            <br />
            <asp:Label runat="server" ID="lblMsg" ForeColor="Red"></asp:Label>
        </div>
    </form>
</body>
</html>
