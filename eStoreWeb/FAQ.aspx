<%@ Page Title="Frequently Asked Questions" Language="C#" MasterPageFile="~/MasterPages/eStoreMaster.Master" AutoEventWireup="true" CodeBehind="FAQ.aspx.cs" Inherits="eStoreWeb.Help" %>
<%@ MasterType VirtualPath="~/MasterPages/eStoreMaster.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="HelpUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="DivMain">
	            <table>
		            <tr><td><a id="top"></a><h1>Frequently Asked Questions</h1></td></tr>
		            <tr>
			            <td>
				            <ul>
					            <li><a href="#howpay">How do I order and pay?</a></li>
					            <li><a href="#payment">My payment won't go through, what's wrong?</a></li>
					            <li><a href="#shipping">How long does the shipping take?</a></li>
					            <li><a href="#duties">Will there be customs duties, taxes, etc?</a></li>
					            <li><a href="#outlets">Are there any outlets where I can look at your products?</a></li>
					            <li><a href="#exchange">If I am dissatisfied can I get an exchange or refund?</a></li>
				            </ul>
			            </td>
		            </tr>
		            <tr><td id="howpay"></td></tr>
		            <tr><td><h2>How do I order and pay?</h2></td></tr>
		            <tr><td><br>Once you have chosen the items you wish to purchase click "View Cart".  This will then commence the check out and payment process.  Just follow al the prompts which will collect information such as the shipping and payment details.<br>
				            <br>Payment options include Visa and MasterCard.  Orders placed within Australia may also be paid for via direct deposit.  We do not have the facilities to process your order via the phone or mail order.<br>
				            <br>Once you order has been processed you will receive an email confirming your purchase.  Shipping costs are automatically calculated during the check out process.<br>
				            <br>
			            </td>
		            </tr>
		            <tr><td id="payment"></td></tr>
		            <tr><td><h2>My payment won't go through, what's wrong?</h2></td></tr>
		            <tr><td><br>The majority of transactions will not encounter any difficulties.  However if your payment doesn't go through smoothly, the most common reasons include:<br>
				            <ul>
					            <li>The web browser being used does not have cookies enabled.</li>
					            <li>The credit card being used does not have the international transactions facility enabled.</li>
					            <li>The payment system has timed out.  The system will cut off your payment connection if it has been idle for one hour.</li>
				            </ul>
				            Some potential solutions include:<br>
				            <ul>
					            <li>Enable cookies in your browser (this setting is usually found in the browser options).</li>
					            <li>Contact your bank to ensure international transactions are enabled for your credit card.</li>
					            <li>Go to the view cart section again to reinitiate the payment process.</li>
					            <li>Try another credit card.  It sounds obvious but it sometimes does the trick.</li>
					            <li>Try another computer, this also sounds very obvious but sometimes the security settings on a computer system can prevent the successful completion of a transaction.</li>
					            <li>If all else fails, record the error message and e-mail us at <asp:HyperLink ID="ContactHyperlink1" runat="server" /> with all the details that we may further assist you.</li>
				            </ul>
			            </td>
		            </tr>
		            <tr><td id="shipping"></td></tr>
		            <tr><td><h2>How long does the shipping take?</h2></td></tr>
		            <tr><td><br>Orders within Australia are shipped via Express Post, which in most cases will mean 2-3 days for delivery.<br>
				            <br>Orders shipping to locations outside Australia can be shipped via;
				            <ul>
					            <li>Airmail - approximately 10-25 working days from dispatch to delivery, and no tracking,</li>
					            <li>Express Airmail - approximately 5-10 working days from dispatch to delivery, and parcel tracking provided,</li>
					            <li>UPS - approximately 2-5 working days from dispatch to delivery, and parcel tracking provided</li>
				            </ul>
				            Your order will usually be shipped within 72 hours of placement*.  If there will be an extended delay in the shipping of your order, we will advise you via e-mail.<br>
				            Once your order is dispatched, shipping times will vary as follows:<br>
				            <br><em>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;*Order dispatch times may vary depending on stock availability and administrative processes.</em><br>
				            <br>Tracking numbers will be emailed to you once your order has been dispatched.<br>
				            <br>Shipping costs are calculated based on the total weight of the products ordered, the shipping method selected and the delivery location.  This enables us to calculate the lowest shipping cost possible.<br>
				            <br>We ship to Argentina, Australia, Austria, Belgium, Canada, China, Denmark, Fiji, Finland, France, Germany, Greece, Hong Kong, India, Indonesia, Ireland, Israel, Italy, Japan, , Korea, Malaysia, Malta, Netherlands, New Zealand, Norway, Papua New Guinea, Philippines, Poland, Singapore, Solomon Islands, South Africa, Spain, Sri Lanka, Sweden, Switzerland, Taiwan, Thailand, United Kingdom, United States and Vietnam.<br>
				            <br>
			            </td>
		            </tr>
		            <tr><td id="duties"></td></tr>
		            <tr><td><h2>Will there be customs duties, taxes, etc?</h2></td></tr>
		            <tr><td><br>All import duties, taxes or VAT are the sole responsibility of the customer and are not included in the shipping charges.<br>
				            <br>Customs duties and taxes vary from country to country.  If you are unsure if your parcel will be subject to such fees you should contact your local customs or postal authority.<br>
				            <br>
			            </td>
		            </tr>
		            <tr><td id="outlets"></td></tr>
		            <tr><td><h2>Are there any outlets where I can look at your products?</h2></td></tr>
		            <tr><td><br>All our transactions are processed via our website to minimise costs.  This means we can pass on savings to our valued customers and as such we do not have any retail outlets.  However, all our products can be viewed on the website with comprehensive descriptions.  If you have any further questions regarding our products we are more than happy for you to e-mail us at: <asp:HyperLink ID="ContactHyperlink2" runat="server" /><br>
				            <br>
			            </td>
		            </tr>
		            <tr><td id="exchange"></td></tr>
		            <tr><td><h2>If I am dissatisfied can I get an exchange or refund?</h2></td></tr>
		            <tr><td><br>For further information about exchanges and refunds please see our policies page by clicking <a href="policies.aspx">here</a>.<br>
				            <br>
			            </td>
		            </tr>
		            <tr><td><h2>Prices</h2></td></tr>
		            <tr><td id="prices">Only the Australian Dollar prices include the Goods and Services Tax (GST), all other currencies prices exclude GST (GST Free).  Note that the final transaction will be processed in Australian Dollars.<br>
			            <br>
			            </td>
		            </tr>
		            <tr><td><h2>Out of Stock</h2></td></tr>
		            <tr><td>It is possible that a product you order will not be in stock.  If this happens we will notify you via e-mail within a week and give you the choice of putting the product on back order or refunding your credit card.  A product placed on back order will be delayed while we re-order the product from our supplier.<br>
			            <br>
			            </td>
		            </tr>
            		
		            <tr><td><h2>Company Information</h2></td></tr>
		            <tr><td>PetsPlayground is an Australian company based in Melbourne, Victoria in Australia.<br>
			            <br>
			            </td>
		            </tr>
	            </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>