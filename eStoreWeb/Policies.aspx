<%@ Page Title="Policies" Language="C#" MasterPageFile="~/MasterPages/eStoreMaster.Master" AutoEventWireup="true" CodeBehind="Policies.aspx.cs" Inherits="eStoreWeb.Policies" %>
<%@ MasterType VirtualPath="~/MasterPages/eStoreMaster.Master" %>

<%@ Import Namespace="eStoreWeb.Properties" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="PoliciesUpdatePanel" runat="server" 
                     UpdateMode="Conditional">
        <ContentTemplate>
	        <div id="DivMain">
	            <table>
	                <tr>
		                <td><h1>Policies</h1></td>
		            </tr>
		            <tr>
		                <td>
		                    <ul>
                                <li><a href="#Exchanges">Exchanges</a></li>
                                <li><a href="#Returns">Returns</a></li>
                                <li><a href="#Cancellations">Cancellations</a></li>
                                <li><a href="#Disclaimer">Disclaimer</a></li>
                                <li><a href="#Confidentiality">Confidentiality</a></li>
                                <li><a href="#Security">Security</a></li>
                                <li><a href="#WebSitePolsAndConds">Website Policies &amp; Conditions</a></li>
                                <li><a href="#Cookies">Cookies</a></li>
                            </ul>
                        </td>
                    </tr>
		            <tr>
		                <td><h2><a name="Exchanges"></a>Exchanges</h2></td>
		            </tr>
		            <tr>
		                <td>
		                    If within 30 days of receipt of your goods you wish to exchange them for a different color, please e-mail us at <asp:HyperLink ID="ContactHyperlink1" runat="server" />.  
		                    Goods will only be accepted for an exchange if they have not been used, worn or damaged.<br><br>
					        Product exchanges will be shipped free of charge.  Note that costs associated with the return of goods are the sole responsibility of the customer.<br>
					        <br>
			            </td>
		            </tr>
		            <tr><td><h2><a name="Returns"></a>Returns</h2></td></tr>
		            <tr><td>If within 30 days of receipt of your goods you wish to return them for a refund, please e-mail us at <asp:HyperLink ID="ContactHyperlink2" runat="server" />.  Goods will only be accepted for a refund if they have not been used, worn or damaged.  Goods ordered with the assumption that they were a different brand, look or feel will not be refunded.<br />
                        <br>
					            Please contact us via e-mail prior to your purchase if you have any doubts or queries regarding any of the products.<br>
				            <br>
					            All costs associated with the return of goods are the sole responsibility of the consumer.  The original shipping costs are not refundable.  A <%=getRefundAdminFee().ToString()%>% administration fee will be charged on all refunds to cover processing costs.<br>
				            <br>
				            <b>Please note</b> we do not supply refunds on sale items.
			            </td>
		            </tr>
		            <tr><td><h2><a name="Cancellations"></a>Cancellations</h2></td></tr>
		            <tr><td>Please notify us immediately at <asp:HyperLink ID="ContactHyperlink3" runat="server" /> if you wish to cancel your order. A <%=getCancellationAdminFee().ToString()%>% administration fee will be charged on all cancellations to cover processing costs.<br>
				            <br>
			            </td>
		            </tr>
		            <tr><td><h2><a name="Disclaimer"></a>Disclaimer</h2></td></tr>
		            <tr><td>We constantly strive to achieve best practice at <asp:Label ID="TradingName1" runat="server" /> and as such have chosen to align ourselves with particular delivery systems.  However once an order is dispatched from our premises we are not responsible for any delivery details.<br>
				            <br>It is the consumer's responsibility to ensure correct shipping/delivery details are provided to <asp:Label ID="TradingName2" runat="server" /> via the online payment system.  <asp:Label ID="TradingName3" runat="server" /> is not responsible for reshipping orders at our expense if incorrect details are provided.<br>
				            <br>All delivery times/dates provided are estimates only and are at the discretion of the postal system providers.<br>
				            <br>Replacement items for orders lost in the mail are at <asp:Label ID="TradingName4" runat="server" />'s discretion.<br>
				            <br>Without limiting the operation of any other Terms and Conditions herein, we will not accept liability for any loss or damage arising from a late delivery.  Without limiting the operation of any other Terms and Conditions herein, you agree that late delivery does not constitute a failure of our agreement and does not entitle you to cancellation of an order.  Without limiting the operation of any other Terms and Conditions herein, we will not accept liability for any loss or damage arising from items lost, stolen or damaged after delivery has taken place.<br>
				            <br>
			            </td>
		            </tr>
		            <tr><td><h2><a name="Confidentiality"></a>Confidentiality</h2></td></tr>
		            <tr><td>At <asp:Label ID="TradingName5" runat="server" /> we are committed to protecting our customers' privacy.  We will only collect information for the purpose of meeting our online transaction obligations.  We will only use or disclose personal information for the purpose of meeting the needs of online transactions.<br>
				            <br>We take every reasonable step to ensure all personal information is accurate, complete and up to date.  We take reasonable steps to protect our customers' personal information from misuse, loss and from unauthorised access, modification or disclosure.  Customers' personal information is not disseminated to third parties.<br>
				            <br>By using our website you consent to <asp:Label ID="TradingName6" runat="server" /> collecting information for the purpose of meeting our transaction obligations.<br>
				            <br>
			            </td>
		            </tr>
		            <tr><td><h2><a name="Security"></a>Security</h2></td></tr>
		            <tr><td>We at <asp:Label ID="TradingName7" runat="server" /> have implemented security measures for our website and associated computer systems in an effort to reduce the risk of credit card misuse and for the protection of your privacy.  Online transactions between our customers and our payment system are protected by 128-bit encryption.  This level of protection is used by most reputable  e-commerce websites.  In the event of unauthorised use of your credit card, you must notify your credit card provider in accordance with its reporting rules and procedures.<br>
				            <br><asp:Label ID="TradingName8" runat="server" /> has been enabled for e-commerce using DirectOne E-Commerce system, ensuring that your details are safely encrypted during all stages of your purchase.  During the final stages of the checkout process a session between you and the DirectOne Vault system will be established to allow you to safely type in your credit card or charge card details.<br>
				            <br><em>Why is the payment secure?</em><br>
				            <br>
				            <ul>
					            <li>Your credit card number is entered on a fully encrypted web page.</li>
					            <li>This is either a 128-bit (latest Netscape) or 40-bit (latest MSIE) SSL encrypted webpage, fully certified by Thawte.</li>
					            <li>Credit card numbers are never visible as "plain text" to the Internet.</li>
					            <li>Credit card numbers are securely transferred to the merchant's bank for processing.</li>
					            <li>All DirectOne Merchants have provided their company information to DirectOne.</li>
					            <li>You receive a unique order tracking number for your transaction.</li>
					            <li>If your browser does not support SSL, or only supports 40-bit SSL, we suggest a quick visit to <a href="http://www.netscape.com" target="_blank">Netscape</a> or <a href="http://www.microsoft.com" target="_blank">Microsoft</a> for their latest browser.  Otherwise we recommend increasing the security of your older Netscape browser to 128-bit by using the Fortify program available from <a href="http://www.fortify.net" target="_blank">here</a>.</li>
				            </ul>
			            </td>
		            </tr>
		            <tr><td><h2><a name="WebSitePolsAndConds"></a>Website Policies &amp; Conditions</h2></td></tr>
		            <tr><td>1. Pricing and currency - All prices listed on our website exclude any local taxes, tarrifs or charges and hence these may be charged on goods by local authorities at delivery outside Australia.  All prices on the website default to Australian currency.  The currency conversion amounts appearing within this site are for your information purposes only and are approximate amounts in your selected currency.  Your credit card will be charged in Australian dollars for the amount shown with AUD $ as the currency and the converted amounts shown here are not necessarily the exact amounts that will appear on your credit card statement as international currency exchange rates are prone to change beyond the control of <asp:Label ID="TradingName9" runat="server" />.<br>
				            <br>2. Website Information - All information contained within the website has been provided for your convenience.  To the best of our knowledge all the information on this website is accurate.  However, no warranty is made as to the accuracy or reliability of links and associated information related to external websites (third-party information) and <asp:Label ID="TradingName10" runat="server" /> disclaims all liability and responsibility for any direct or indirect loss or damage which may be suffered by any recipient through relying on such third-party information.<br>
				            <br>3. Links to and from other websites - <asp:Label ID="TradingName11" runat="server" /> is not responsible for the content or any material appearing on any third party sites linked to or from our website.  If you decide to access any of the third party websites linked from our website, you do so entirely at your own risk.  <asp:Label ID="TradingName12" runat="server" /> accepts no liability or responsibility for the actions or omissions of the linked sites in relation to the content contained therein or effects on any local laws.<br>
				            <br>4. Laws - <asp:Label ID="TradingName13" runat="server" /> is based in Melbourne, Australia and the use of this site is governed by the laws of the State of Victoria.  Any legal proceedings arising as a result of using this website or liaising with it will take place in the law courts of the State of Victoria.<br>
				            <br>5. Site changes - We reserve the right to make any changes, modifications, additions, deletions or corrections at any time without prior notice.<br>
				            <br>
			            </td>
		            </tr>
		            <tr><td><h2><a name="Cookies"></a>Cookies</h2></td></tr>
		            <tr><td><asp:Label ID="TradingName14" runat="server" /> uses cookies to tag each visitor's browser with a random unique number.  The cookie simply identifies the session and does not disclose any personal information about the person using the browser.  The cookie enables us to track your shopping cart as you move around <asp:Label ID="TradingName15" runat="server" />.
				            <br>
			            </td>
		            </tr>
	            </table>
            </div>
	    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
