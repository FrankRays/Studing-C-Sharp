<%@ Page Language="C#" AutoEventWireup="true" %>
<%@ Register Assembly="System.Web.Silverlight" Namespace="System.Web.UI.SilverlightControls" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" style="height:100%;">
<head id="Head1" runat="server">
    <title>Silverlight 2 Chatroom</title>
    <script type="Text/javascript">
        window.onload = function ()
        {
            document.getElementById('Xaml1').focus();
        }
    </script>
</head>
<body style="height:100%;margin:0; padding:0; width: 100%;">
    <form id="form1" runat="server" style="height:100%;">
       <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="width: 100%; text-align:center; height: 100%;">
                    <asp:Silverlight ID="Xaml1" runat="server" Source="~/ClientBin/Silverlight2Chat.xap" MinimumVersion="2.0.31005.0" Width="600" Height="100%" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>