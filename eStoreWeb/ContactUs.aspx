<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/eStoreMaster.Master" AutoEventWireup="true" CodeBehind="ContactUs.aspx.cs" Inherits="eStoreWeb.ContactUs" %>
<%@ MasterType VirtualPath="~/MasterPages/eStoreMaster.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>Contact Us</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="DivMain">
        <h1>Assistance and Order enquiries</h1>
        <p>Send an email to <asp:HyperLink ID="ContactHyperlink1" runat="server" /></p>
        <p>Or use the form provided below.</p>
        <fieldset>
	    <legend>Contact Details</legend>
	    <table border="0">
		    <tbody>
		        <tr>
			        <td>
			            <asp:Label ID="NameTextBoxLabel" runat="server" 
				               Text="Your Name" 
				               AccessKey="n" />
			        </td>
			        <td>
			            <asp:TextBox ID="NameTextBox" runat="server"
					         maxlength="40"
					         Columns="40"
					         AccessKey="n">
			            </asp:TextBox>
			            <asp:RequiredFieldValidator ID="NameTextBoxRequiredFieldValidator" runat="server" 
							        ErrorMessage="Required Field"
							        ControlToValidate="NameTextBox"
							        ValidationGroup="ContactGroup"/>
			            <asp:RegularExpressionValidator ID="NameTextBoxRegularExpressionValidator" runat="server" 
							            ErrorMessage="Invalid Name" 
							            ControlToValidate="NameTextBox" 
							            ValidationExpression="[a-zA-Z'-]+" 
							            ValidationGroup="ContactGroup"
							            Display="Dynamic" />
			        </td>
		        </tr>
		        <tr>
			        <td>
			            <asp:Label ID="EmailLabel" runat="server" 
				               Text="Email Address"
				               AccessKey="e" />
			        </td>
			        <td>
			            <asp:TextBox ID="EmailTextBox" runat="server"
					         MaxLength="40" 
					         Columns="40"
					         AccessKey="e">
			            </asp:TextBox>
			            <asp:RequiredFieldValidator ID="EmailTextBoxRequiredFieldValidator" runat="server" 
							        ErrorMessage="Required Field"
							        ControlToValidate="EmailTextBox"
							        ValidationGroup="ContactGroup"
							        Display="Dynamic"/>
			            <asp:RegularExpressionValidator ID="EmailTextBoxRegularExpressionValidator" runat="server" 
							            ErrorMessage="Invalid Email Address" 
							            ControlToValidate="EmailTextBox" 
							            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
							            ValidationGroup="ContactGroup"
							            Display="Dynamic" />
			        </td>
		        </tr>
		        <tr>
			        <td>
			            <asp:Label ID="SubjectLabel" runat="server" 
				               Text="Subject"
				               AccessKey="s" />
			        </td>
			        <td>
			            <asp:TextBox ID="SubjectTextBox" runat="server"
					         MaxLength="50"
					         Columns="50"
					         AccessKey="s">
			            </asp:TextBox>
			            <asp:RequiredFieldValidator ID="SubjectTextBoxRequiredFieldValidator" runat="server" 
							        ErrorMessage="Required Field"
							        ControlToValidate="SubjectTextBox"
							        ValidationGroup="ContactGroup"
							        Display="Dynamic"/>
			        </td>
		        </tr>
		        <tr valign="top">
			        <td>
			            <asp:Label ID="MessageLabel" runat="server" 
				               Text="Message"
				               AccessKey="m" />
			        </td>
			        <td>
			            <asp:Textbox ID="BodyTextBox" runat="server" 
					         TextMode="MultiLine"
					         Rows="10"
					         Columns="40"
					         Wrap="true"
					         AccessKey="m">
			            </asp:Textbox>
			            <asp:RequiredFieldValidator ID="BodyTextBoxRequiredFieldValidator" runat="server" 
							        ErrorMessage="Required Field"
							        ControlToValidate="BodyTextBox"
							        ValidationGroup="ContactGroup"
							        Display="Dynamic"/>
			        </td>
		        </tr>
		    </tbody>
	    </table>
        </fieldset>
        <p>
            <asp:Button ID="SendEmailButton" runat="server" 
                        Text="Send"
                        OnClick="StoreEmailInDB"
                        ValidationGroup="ContactGroup"/>
        </p>
    </div>
</asp:Content>