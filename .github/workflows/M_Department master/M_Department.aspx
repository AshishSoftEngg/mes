<%@ Page Title="Department Master" Language="C#" MasterPageFile="~/BSEBMainMasterPage.master" AutoEventWireup="true"
    CodeFile="M_Department.aspx.cs" Inherits="Payroll_Master_M_Department" %>

<%@ Register Src="~/usercontrols/Messagebox.ascx" TagName="Messagebox" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/usercontrols/DeleteConfirmBox.ascx" TagName="DeleteConfirmBox"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <table cellpadding="0" cellspacing="0" style="width: 100%; border-style: solid; border-width: 1px 1px 1px 1px;">
        <tr class="PageHeaddingBox">
            <td colspan="2" style="text-align: center; border-bottom: solid 1px #666633">
                <b>Department Master</b>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Font-Bold="False" EnableViewState="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="right" width="50%">
                &nbsp;</td>
            <td width="50%">
                &nbsp;</td>
        </tr>
        <tr id="colrow" runat="server" visible="false">
            <td align="right" width="50%">
                Department Code:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </td>
            <td width="50%">
                <asp:Label ID="lblTabid" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="lblDepartmentCode" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="right" width="50%">
                Department Name:<asp:Label ID="lbl_sg" runat="server" ForeColor="Red" Text="*"></asp:Label>&nbsp;&nbsp;&nbsp;
            </td>
            <td width="50%">
                <asp:TextBox ID="txtDepartmentName" MaxLength="180" runat="server"></asp:TextBox>
            </td>
        </tr>       
        <tr>
            <td align="right" width="50%">
                PayBill Signing Autherity:<asp:Label ID="lblPaybillSa" runat="server" ForeColor="Red" Text="*"></asp:Label>&nbsp;&nbsp;&nbsp;
            </td>
            <td width="50%">
                <asp:TextBox ID="txtPayBSA" MaxLength="500" runat="server" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>       
        <tr>
            <td align="right" width="50%">
                Upload Signing Autherity:<asp:Label ID="Label1" runat="server" ForeColor="Red" Text="*"></asp:Label>&nbsp;&nbsp;&nbsp;
            </td>
            <td width="50%">
                <asp:UpdatePanel ID="up1" runat="server">
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnSave" />
                            </Triggers>
                     <ContentTemplate>
                <asp:FileUpload ID="FUuploadSign"  runat="server" CssClass="CS" ViewStateMode="Enabled" />
                        <asp:Label ID="lbl_img" runat="server" Visible="false"></asp:Label>
                         </ContentTemplate>
                    </asp:UpdatePanel>

            </td>

        </tr>
        <tr>

             
           <td align="right" width= "50%" >

                       <%-- <asp:Image ID="img_photo" runat="server" TabIndex="9" Width="100px" Height="120px" />--%>
                    </td>
            <td align="right" width="50%">
                 

            </td>

 
        </tr>
        <tr>
            <td align="right" width="50%">
                &nbsp;</td>
            <td width="50%">
                     <div align="left">
                    <uc1:Messagebox ID="Messagebox" runat="Server" />
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="cmdbutton" />
                    <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" CssClass="cmdbutton" />
                    <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="Delete"
                        Visible="false" CssClass="cmdbutton" />
                </div></td>
        </tr>       
        
        <tr>
            <td colspan="2">
          
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:GridView ID="grdDepartmentMaster" runat="server" AutoGenerateColumns="False"
                    Width="100%" CellPadding="4" AllowPaging="True" OnPageIndexChanging="grdDesignationMaster_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Edit">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkbtnSelect" runat="server" OnClick="LinkbtnSelect_Click">
                                    <asp:Image ID="imgedit" Width="20px" Height="20px" runat="server" ImageUrl="~/Images/Edit.jpg" />
                                </asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Department Code" ItemStyle-HorizontalAlign="Center" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblDepartmentCode" runat="server" Text='<%# Bind("DepartmentCode") %>'> </asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Department Name" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblDepartmentName" runat="server" Text='<%# Bind("DepartmentName") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PayBill Signing Authentication" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblPayBillSignAuth" runat="server" Text='<%# Bind("PayBillSignAuth") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
                       
                        <asp:TemplateField HeaderText="Table ID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lbltabid" runat="server" Text='<%# Bind("TableID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                          
                        <asp:TemplateField HeaderText="Delete">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgDelete" Width="20px" Height="20px" runat="server" ImageUrl="~/Images/delete.jpg"
                                    OnClientClick="return confirm('Are You Sure To Delete?');" OnClick="imgDelete_Click" />
                            </ItemTemplate>
                              <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

                 

            </td>
        </tr>
    </table>
</asp:Content>
