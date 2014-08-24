<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/eStoreMaster.Master" AutoEventWireup="true" CodeBehind="ContactPreference.aspx.cs" Inherits="eStoreWeb.Profile.ContactPreference" %>
<%@ MasterType VirtualPath="~/MasterPages/eStoreMaster.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="DivMain">
        <asp:HyperLink ID="HomeHyperlink" runat="server" NavigateUrl="~/" Text="Home" />&nbsp;&gt;&nbsp;
        <asp:HyperLink ID="ChangePreferences_Breadcrumb" runat="server" Text="Change Details" />
        <hr />
        <h1>Modify Contact Preferences</h1>
        <asp:FormView ID="ContactDetailsFormView" runat="server" 
                      DataKeyNames="ID" 
                      DataSourceID="UserODS">
            <ItemTemplate>
                <asp:CheckBox runat="server" ID="NewsletterPreference" Checked="<%#isRequired((int)Eval(FindControl("RequireNewsletter").ToString()))%>" />
                <asp:Literal runat="server" Text="Yes, please contact me about new product updates." />
            </ItemTemplate>
        </asp:FormView>
        
        <div>
            <asp:Button ID="SaveButton" runat="server"  
                        Text="Save Changes"
                        Font-Size="Smaller"
                        ValidationGroup="SaveChangesGroup"
                        OnClick="SaveButton_Click" />
            <asp:Button ID="CancelButton" runat="server" 
                        CausesValidation="False" 
                        CommandName="Cancel"
                        Font-Size="Smaller"
                        Text="Cancel"
                        OnClick="Cancel_Click" />
        </div>
        <asp:ObjectDataSource ID="UserODS" runat="server"
                              TypeName="eStoreBLL.UserBLL" 
                              OldValuesParameterFormatString="original_{0}" 
                              SelectMethod="GetUserIdByEmail">
            <SelectParameters>
                <asp:SessionParameter Name="username" SessionField="LoginEmailAddress" Type="String" />
                <asp:QueryStringParameter Name="Application" DefaultValue="eStore" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
</asp:Content>