
function SearchOption() {
    jQuery('a.SectionMode').each(function (i) {
        jQuery(this).click(function () {
            var $this = jQuery(this);
            if ($this.hasClass('Expand')) {
                $this.removeClass('Expand');
                if (jQuery("#outFind").length > 0)
                    jQuery("#outFind").hide();
            } else {
                $this.addClass('Expand');
                if (jQuery("#outFind").length > 0)
                    jQuery("#outFind").show();
            }
            jQuery(".SectionBlockWrapper").toggle("slow");
        });
    });


}
function sortAction(fieldSort) {
    jQuery("#Pager_Sidx").val(fieldSort);
    var sord = jQuery("#Pager_Sord").val();
    if (sord == 'asc') {
        jQuery("#Pager_Sord").val('desc');
    } else {
        jQuery("#Pager_Sord").val('asc');
    }
    jQuery("#GridAction").val("Sort");
    var form = jQuery("#SearchBlock").parents("form:first");
    form.submit();
}
function resetFrom(t) {  
    jQuery(t).parents("form:first").trigger("reset");
    if (jQuery("#ClearSearch").length > 0) {
        jQuery("#ClearSearch").val("yes");
    }
    jQuery("#table-sarch,#SectionHeading").find("input:text").each(function() {
        jQuery(this).val("");
    });
    jQuery("#table-sarch,#SectionHeading").find("input:checkbox").removeAttr("Checked");
    if (jQuery("#SearchCriteria_RevenueStatus").length > 0) {
        jQuery("#SearchCriteria_RevenueStatus").val("");
    }
    if (jQuery("#ColorStatus").length > 0) {
        jQuery("#ColorStatus").val("");
    }
    jQuery("#table-sarch,#SectionHeading").find("select").each(function() {
        jQuery(this).val("");
    });
    submitForm();
}
function goToPage(pageIndex) {
    jQuery("#Pager_CurrentPage").val(pageIndex);
    jQuery("#GridAction").val("GoToPage");

    submitForm();
}

function onPageSizeChange() {
    jQuery("#GridAction").val("ChangePageSize");
    submitForm();
}
function submitForm() {
    if (jQuery("#SearchQuickView").length > 0 && jQuery("#SearchQuickView").val() == "") {
        jQuery("#SearchQuickView").val(0);
    }
    var form = jQuery("#SearchBlock").parents("form:first");
    form.trigger('submit');
} 

(function ($) {

    jQuery.fn.extend({
        OrderList: function () {
            var sord = "asc";
            if (jQuery("#Pager_Sord").length > 0) {
                sord = jQuery("#Pager_Sord").val();
            }
            var sidx = "";
            if (jQuery("#Pager_Sidx").length > 0) {
                sidx = jQuery("#Pager_Sidx").val();
            }
            jQuery(".SortHeader").each(function (index) {
                jQuery(this).html('');
            });
            sidx = sidx.replace(".", "_");
            if (sord == 'asc') {
                jQuery("#" + sidx + "_Title").html('<img src="/Images/sort_asc.gif"/>');
            } else {
                jQuery("#" + sidx + "_Title").html('<img src="/Images/sort_desc.gif"/>');
            }
        }
    });
})(jQuery);
jQuery(document).ready(function () {
    SearchOption(); 
});