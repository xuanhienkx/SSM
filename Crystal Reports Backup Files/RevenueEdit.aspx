<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SSM.Models.RevenueModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	 Revenue Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>RevenueEdit</h2>

    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>
        
        <fieldset>
            <legend>Fields</legend>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Id) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Id) %>
                <%: Html.ValidationMessageFor(model => model.Id) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.BRate) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.BRate, String.Format("{0:F}", Model.BRate))%>
                <%: Html.ValidationMessageFor(model => model.BRate) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.ARate) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.ARate, String.Format("{0:F}", Model.ARate)) %>
                <%: Html.ValidationMessageFor(model => model.ARate) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.SRate) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.SRate, String.Format("{0:F}", Model.SRate)) %>
                <%: Html.ValidationMessageFor(model => model.SRate) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.INTransportRate) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.INTransportRate, String.Format("{0:F}", Model.INTransportRate)) %>
                <%: Html.ValidationMessageFor(model => model.INTransportRate) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.INInlandService) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.INInlandService, String.Format("{0:F}", Model.INInlandService)) %>
                <%: Html.ValidationMessageFor(model => model.INInlandService) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.INCreditDebit) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.INCreditDebit, String.Format("{0:F}", Model.INCreditDebit)) %>
                <%: Html.ValidationMessageFor(model => model.INCreditDebit) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.INDocumentFee) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.INDocumentFee, String.Format("{0:F}", Model.INDocumentFee)) %>
                <%: Html.ValidationMessageFor(model => model.INDocumentFee) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.INHandingFee) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.INHandingFee, String.Format("{0:F}", Model.INHandingFee)) %>
                <%: Html.ValidationMessageFor(model => model.INHandingFee) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.INTHC) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.INTHC, String.Format("{0:F}", Model.INTHC)) %>
                <%: Html.ValidationMessageFor(model => model.INTHC) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.INCFS) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.INCFS, String.Format("{0:F}", Model.INCFS)) %>
                <%: Html.ValidationMessageFor(model => model.INCFS) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.AutoName1) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.AutoName1) %>
                <%: Html.ValidationMessageFor(model => model.AutoName1) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.AutoName2) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.AutoName2) %>
                <%: Html.ValidationMessageFor(model => model.AutoName2) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.INAutoValue1) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.INAutoValue1, String.Format("{0:F}", Model.INAutoValue1)) %>
                <%: Html.ValidationMessageFor(model => model.INAutoValue1) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.INAutoValue2) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.INAutoValue2, String.Format("{0:F}", Model.INAutoValue2)) %>
                <%: Html.ValidationMessageFor(model => model.INAutoValue2) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.OUTAutoValue1) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.OUTAutoValue1, String.Format("{0:F}", Model.OUTAutoValue1)) %>
                <%: Html.ValidationMessageFor(model => model.OUTAutoValue1) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.OUTAutoValue2) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.OUTAutoValue2, String.Format("{0:F}", Model.OUTAutoValue2)) %>
                <%: Html.ValidationMessageFor(model => model.OUTAutoValue2) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.INTransportRateOS) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.INTransportRateOS, String.Format("{0:F}", Model.INTransportRateOS)) %>
                <%: Html.ValidationMessageFor(model => model.INTransportRateOS) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.INInlandServiceOS) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.INInlandServiceOS, String.Format("{0:F}", Model.INInlandServiceOS)) %>
                <%: Html.ValidationMessageFor(model => model.INInlandServiceOS) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.INCreditDebitOS) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.INCreditDebitOS, String.Format("{0:F}", Model.INCreditDebitOS)) %>
                <%: Html.ValidationMessageFor(model => model.INCreditDebitOS) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.INDocumentFeeOS) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.INDocumentFeeOS, String.Format("{0:F}", Model.INDocumentFeeOS)) %>
                <%: Html.ValidationMessageFor(model => model.INDocumentFeeOS) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.INHandingFeeOS) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.INHandingFeeOS, String.Format("{0:F}", Model.INHandingFeeOS)) %>
                <%: Html.ValidationMessageFor(model => model.INHandingFeeOS) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.INAutoValue1OS) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.INAutoValue1OS, String.Format("{0:F}", Model.INAutoValue1OS)) %>
                <%: Html.ValidationMessageFor(model => model.INAutoValue1OS) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.INAutoValue2OS) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.INAutoValue2OS, String.Format("{0:F}", Model.INAutoValue2OS)) %>
                <%: Html.ValidationMessageFor(model => model.INAutoValue2OS) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.EXAutoValue1OS) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.EXAutoValue1OS, String.Format("{0:F}", Model.EXAutoValue1OS)) %>
                <%: Html.ValidationMessageFor(model => model.EXAutoValue1OS) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.EXAutoValue2OS) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.EXAutoValue2OS, String.Format("{0:F}", Model.EXAutoValue2OS)) %>
                <%: Html.ValidationMessageFor(model => model.EXAutoValue2OS) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.EXTransportRateOS) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.EXTransportRateOS, String.Format("{0:F}", Model.EXTransportRateOS)) %>
                <%: Html.ValidationMessageFor(model => model.EXTransportRateOS) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.EXTransportRate) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.EXTransportRate, String.Format("{0:F}", Model.EXTransportRate)) %>
                <%: Html.ValidationMessageFor(model => model.EXTransportRate) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.EXInlandServiceOS) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.EXInlandServiceOS, String.Format("{0:F}", Model.EXInlandServiceOS)) %>
                <%: Html.ValidationMessageFor(model => model.EXInlandServiceOS) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.EXInlandService) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.EXInlandService, String.Format("{0:F}", Model.EXInlandService)) %>
                <%: Html.ValidationMessageFor(model => model.EXInlandService) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.EXCommision2Shipper) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.EXCommision2Shipper, String.Format("{0:F}", Model.EXCommision2Shipper)) %>
                <%: Html.ValidationMessageFor(model => model.EXCommision2Shipper) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.EXCommision2Carrier) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.EXCommision2Carrier, String.Format("{0:F}", Model.EXCommision2Carrier)) %>
                <%: Html.ValidationMessageFor(model => model.EXCommision2Carrier) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.EXTax) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.EXTax, String.Format("{0:F}", Model.EXTax)) %>
                <%: Html.ValidationMessageFor(model => model.EXTax) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.EXCreditDebit) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.EXCreditDebit, String.Format("{0:F}", Model.EXCreditDebit)) %>
                <%: Html.ValidationMessageFor(model => model.EXCreditDebit) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.EXCreditDebitOS) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.EXCreditDebitOS, String.Format("{0:F}", Model.EXCreditDebitOS)) %>
                <%: Html.ValidationMessageFor(model => model.EXCreditDebitOS) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.EXDocumentFee) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.EXDocumentFee, String.Format("{0:F}", Model.EXDocumentFee)) %>
                <%: Html.ValidationMessageFor(model => model.EXDocumentFee) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.EXHandingFee) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.EXHandingFee, String.Format("{0:F}", Model.EXHandingFee)) %>
                <%: Html.ValidationMessageFor(model => model.EXHandingFee) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.EXTHC) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.EXTHC, String.Format("{0:F}", Model.EXTHC)) %>
                <%: Html.ValidationMessageFor(model => model.EXTHC) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.EXCFS) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.EXCFS, String.Format("{0:F}", Model.EXCFS)) %>
                <%: Html.ValidationMessageFor(model => model.EXCFS) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.EXManualName1) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.EXManualName1) %>
                <%: Html.ValidationMessageFor(model => model.EXManualName1) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.EXManualName2) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.EXManualName2) %>
                <%: Html.ValidationMessageFor(model => model.EXManualName2) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.EXManualValue1) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.EXManualValue1, String.Format("{0:F}", Model.EXManualValue1)) %>
                <%: Html.ValidationMessageFor(model => model.EXManualValue1) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.ExManualValue2) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.ExManualValue2, String.Format("{0:F}", Model.ExManualValue2)) %>
                <%: Html.ValidationMessageFor(model => model.ExManualValue2) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.EXmanualValue1OS) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.EXmanualValue1OS, String.Format("{0:F}", Model.EXmanualValue1OS)) %>
                <%: Html.ValidationMessageFor(model => model.EXmanualValue1OS) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.EXmanualValue2OS) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.EXmanualValue2OS, String.Format("{0:F}", Model.EXmanualValue2OS)) %>
                <%: Html.ValidationMessageFor(model => model.EXmanualValue2OS) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.InvoiceCustom) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.InvoiceCustom, String.Format("{0:F}", Model.InvoiceCustom)) %>
                <%: Html.ValidationMessageFor(model => model.InvoiceCustom) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Description4Invoice) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Description4Invoice) %>
                <%: Html.ValidationMessageFor(model => model.Description4Invoice) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.INRemark) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.INRemark) %>
                <%: Html.ValidationMessageFor(model => model.INRemark) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.EXRemark) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.EXRemark) %>
                <%: Html.ValidationMessageFor(model => model.EXRemark) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Income) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Income, String.Format("{0:F}", Model.Income)) %>
                <%: Html.ValidationMessageFor(model => model.Income) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.INVI) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.INVI, String.Format("{0:F}", Model.INVI)) %>
                <%: Html.ValidationMessageFor(model => model.INVI) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.INOS) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.INOS, String.Format("{0:F}", Model.INOS)) %>
                <%: Html.ValidationMessageFor(model => model.INOS) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Expense) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Expense, String.Format("{0:F}", Model.Expense)) %>
                <%: Html.ValidationMessageFor(model => model.Expense) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.EXVI) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.EXVI, String.Format("{0:F}", Model.EXVI)) %>
                <%: Html.ValidationMessageFor(model => model.EXVI) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.EXOS) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.EXOS, String.Format("{0:F}", Model.EXOS)) %>
                <%: Html.ValidationMessageFor(model => model.EXOS) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Earning) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Earning, String.Format("{0:F}", Model.Earning)) %>
                <%: Html.ValidationMessageFor(model => model.Earning) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.EarningVI) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.EarningVI, String.Format("{0:F}", Model.EarningVI)) %>
                <%: Html.ValidationMessageFor(model => model.EarningVI) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.EarningOS) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.EarningOS, String.Format("{0:F}", Model.EarningOS)) %>
                <%: Html.ValidationMessageFor(model => model.EarningOS) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.InvAmount1) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.InvAmount1, String.Format("{0:F}", Model.InvAmount1)) %>
                <%: Html.ValidationMessageFor(model => model.InvAmount1) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.InvType1) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.InvType1) %>
                <%: Html.ValidationMessageFor(model => model.InvType1) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.InvAgentId1) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.InvAgentId1) %>
                <%: Html.ValidationMessageFor(model => model.InvAgentId1) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.InvAmount2) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.InvAmount2, String.Format("{0:F}", Model.InvAmount2)) %>
                <%: Html.ValidationMessageFor(model => model.InvAmount2) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.InvType2) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.InvType2) %>
                <%: Html.ValidationMessageFor(model => model.InvType2) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.InvAgentId2) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.InvAgentId2) %>
                <%: Html.ValidationMessageFor(model => model.InvAgentId2) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.BonRequest) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.BonRequest) %>
                <%: Html.ValidationMessageFor(model => model.BonRequest) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.BonApprove) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.BonApprove) %>
                <%: Html.ValidationMessageFor(model => model.BonApprove) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.AmountBonus1) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.AmountBonus1, String.Format("{0:F}", Model.AmountBonus1)) %>
                <%: Html.ValidationMessageFor(model => model.AmountBonus1) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.AmountBonus2) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.AmountBonus2, String.Format("{0:F}", Model.AmountBonus2)) %>
                <%: Html.ValidationMessageFor(model => model.AmountBonus2) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.AccInv1) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.AccInv1) %>
                <%: Html.ValidationMessageFor(model => model.AccInv1) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.AccInv2) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.AccInv2) %>
                <%: Html.ValidationMessageFor(model => model.AccInv2) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.AccInv3) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.AccInv3) %>
                <%: Html.ValidationMessageFor(model => model.AccInv3) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.AccInv4) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.AccInv4) %>
                <%: Html.ValidationMessageFor(model => model.AccInv4) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.AccInvDate1) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.AccInvDate1, String.Format("{0:g}", Model.AccInvDate1)) %>
                <%: Html.ValidationMessageFor(model => model.AccInvDate1) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.AccInvDate2) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.AccInvDate2, String.Format("{0:g}", Model.AccInvDate2)) %>
                <%: Html.ValidationMessageFor(model => model.AccInvDate2) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.AccInvDate3) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.AccInvDate3, String.Format("{0:g}", Model.AccInvDate3)) %>
                <%: Html.ValidationMessageFor(model => model.AccInvDate3) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.AccInvDate4) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.AccInvDate4, String.Format("{0:g}", Model.AccInvDate4)) %>
                <%: Html.ValidationMessageFor(model => model.AccInvDate4) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.ApproveId) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.ApproveId) %>
                <%: Html.ValidationMessageFor(model => model.ApproveId) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.ApproveName) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.ApproveName) %>
                <%: Html.ValidationMessageFor(model => model.ApproveName) %>
            </div>
            
            <p>
                <input type="submit" value="Save" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%: Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

