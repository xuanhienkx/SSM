<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SSM.Models.BookingConfirmModel>" %>
<%@ Import Namespace="SSM.Models" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	 Booking Confirm
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="BookDivBody">
   <% using (Html.BeginForm())
      { %>
       <% RevenueModel RevenueModel1 = new RevenueModel();
                                            RevenueModel1.Id = Model.ShipmentId;    
                                         %>
                                    <% Html.RenderPartial("_DocumentMenu", RevenueModel1); %>
      <div class="NormalZone BookRefNo">
      <div class="BookRefNoText NormalZone"><%= Model.ShipmentId %></div>
      <div class="BookRefNoLabel NormalZone">Ref No:</div>
      </div>
      <div class="NormalZone">
      <div class="BookDateText NormalZone"><%= DateTime.Now.ToString("dd/MM/yyyy") %></div>
      <div class="BookDate NormalZone">DATE:</div>
      </div>
      <div class="BookTo NormalZone">
        <div class="BookToLabel NormalZone">To:</div>
        <div class="BookToContent NormalZone"><%: Html.TextAreaFor(m => m.BookTo, new { Class = "ShipmentTextArea" })%></div>
      </div>
      <div class="BookFrom NormalZone">
      <div class="BookFromLabel NormalZone">From:</div>
        <div class="BookFromContent NormalZone"><%: Html.TextBoxFor(m => m.BookFrom, new { Class = "ShipmentInput" })%></div>
      </div>
      <div class="BookConfirmText NormalZone">
      BOOKING CONFIRMATION
      </div>
      <div class="NormalZone ThankFor">Thank you very much for your support to our service</div>
      <div class="NormalZone WouldLike">We would like to inform you that we have booked space for you shipment on the flight<br />
      with the details as follows
      </div>
      <div class="NormalZone">
        <div class="NormalZone BookDestination">DESTINATION:</div>
        <div class="NormalZone BookDestinationText"><%: Html.TextBoxFor(m => m.Destination, new { Class="ShipmentInput"})%></div>
      </div>
      <div class="NormalZone">
        <div class="NormalZone BookDestination">COMMODITY:</div>
        <div class="NormalZone BookDestinationText"><%: Html.TextBoxFor(m => m.Commodity, new { Class="ShipmentInput"})%></div>
      </div>
      <div class="NormalZone">
        <div class="NormalZone BookDestination">QUANTITY / WEIGHT:</div>
        <div class="NormalZone BookDestinationText"><%: Html.TextBoxFor(m => m.Quantity, new { Class="ShipmentInput"})%></div>
      </div>
      <div class="NormalZone">
        <div class="NormalZone BookDestination">FLIGHT / DATE:</div>
        <div class="NormalZone BookDestinationText"><%: Html.TextBoxFor(m => m.FlightDate, new { Class="ShipmentInput"})%><label for="FlightDate" class="DateInput"></label></div>
      </div>
      <div class="NormalZone">
        <div class="NormalZone BookDestination">LOADING DATE:</div>
        <div class="NormalZone BookDestinationText"><%: Html.TextBoxFor(m => m.LoadingDate, new { Class="ShipmentInput"})%><label for="LoadingDate" class="DateInput"></label></div>
      </div>
      <div class="NormalZone">
        <div class="NormalZone BookDestination">CLOSING TIME:</div>
        <div class="NormalZone BookDestinationText"><%: Html.TextBoxFor(m => m.ClosingDate, new { Class="ShipmentInput"})%><label for="ClosingDate" class="DateInput"></label></div>
      </div>

      <div class="NormalZone BookFobCharge">FOB CHARGE:</div>
       <div class="NormalZone">
        <div class="NormalZone BookFobChargeLabel">1 - AIRPORT CHARGES:</div>
        <div class="NormalZone BookFobChargeText"><%: Html.TextBoxFor(m => m.AirportCharge, new { Class="ShipmentInput"})%></div>
      </div>
      <div class="NormalZone">
        <div class="NormalZone BookFobChargeLabel">2 - X_PRAY:</div>
        <div class="NormalZone BookFobChargeText"><%: Html.TextBoxFor(m => m.XPray, new { Class="ShipmentInput"})%></div>
      </div>
      <div class="NormalZone">
        <div class="NormalZone BookFobChargeLabel">3 - AWB FEE</div>
        <div class="NormalZone BookFobChargeText"><%: Html.TextBoxFor(m => m.AWBFee, new { Class="ShipmentInput"})%></div>
      </div>
      <div class="NormalZone">
        <div class="NormalZone BookFobChargeLabel">4 - HANDLING</div>
        <div class="NormalZone BookFobChargeText"><%: Html.TextBoxFor(m => m.HandingCharge, new { Class="ShipmentInput"})%></div>
      </div>
      <div class="NormalZone">
        <div class="NormalZone BookFobChargeLabel">5 - AMS</div>
        <div class="NormalZone BookFobChargeText"><%: Html.TextBoxFor(m => m.AMSCharge, new { Class="ShipmentInput"})%></div>
      </div>
      <div class="NormalZone BookContact">
        <div class="NormalZone BookContactLabel">For loading cargo, please contact:</div>
        <div class="NormalZone BooKConfirmText"><%: Html.TextBoxFor(m => m.Contact, new { Class="ShipmentInput"})%></div>
      </div>
      <div class="NormalZone BookRegard">Best Regards
      </div>
      <div class="NormalZone">
      <%: Html.TextBoxFor(m => m.AuthoWord, new { Class="ShipmentInput"})%>
      </div>
      <div style="clear:both;padding-top:30px;">
      <div class="ButtonZone">
         <div class="DocLinkButton">
        <%: Html.ActionLink("Shipment", "Edit", "Shipment", new { id = Model.ShipmentId }, new { Class = "LinkForm" })%>
        </div>
        <div class="DocLinkButton">
        <%: Html.ActionLink("Revenue", "Revenue", "Shipment", new { id = Model.ShipmentId }, new { Class = "LinkForm" })%>
        </div>
        <div class="DocLinkButton">
        <a href="#" onclick="jQuery('#submitButton').click();" class="LinkForm" title="Update Document">Update</a>
        <input id="submitButton" type="submit" value="Update" title="Update Document" style="display:none"/>
        </div>
        <div class="DocLinkButton">
        <%: Html.ActionLink("Print", "PrintBookingConfirm", "Shipment", new { id = 0,ShipmentId = Model.ShipmentId }, new { Class = "LinkForm", target = "_blank" })%>
        </div>

    </div>
    </div>
   <%} %>
   </div>
   <script language="javascript" type="text/javascript">
       jQuery(document).ready(function () {
           jQuery("#FileTab").addClass("Active");
           jQuery('#FileTab').activeThisNav();
           new DateTimePicker('LoadingDate', 'dd/MM/yyyy');
           new DateTimePicker('FlightDate', 'dd/MM/yyyy');
           new DateTimePicker('ClosingDate', 'dd MMM HH:mm:ss');
           jQuery("#DocumentMenuContainer .LinkClose").hide();
           jQuery("#BoolkingConfirm").addClass("LinkActive");
       });
    </script>
</asp:Content>
