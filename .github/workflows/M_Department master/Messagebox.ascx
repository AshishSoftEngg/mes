<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Messagebox.ascx.vb" Inherits="Usercontrol_Messagebox" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<%--<link href="../css/styles.css" rel="stylesheet" type="text/css" />--%>

<asp:Button ID="btn_popup_message" runat="server" Style="display: none" />
    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btn_popup_message"
        PopupControlID="Panel_message" OkControlID="btn_ok_message" BackgroundCssClass="modalBackground"  />
    <asp:Panel ID="Panel_message" runat="server" CssClass="panel" Style="display: none;">
        <br />
        <asp:Label ID="lbl_msg" runat="server"></asp:Label>
        <br />
        <br />
        <div style="text-align: center;">
            <asp:Button ID="btn_ok_message" runat="server" Text="OK"  />
        </div>
    </asp:Panel> 