jQuery.noConflict();
jQuery(function(){
    jQuery('body').css('visibility','visible');
    jQuery('div.PageButton').setWidth(); 
});
(function($) {
$.fn.setWidth = function() {
    return this.each(function() {
        var myWidth = 0;
        $(this).children().each(function(){
            myWidth += $(this).outerWidth(true);
        });
        $(this).width(myWidth);
    });
};    
})(jQuery);

function includeHtml(target, url, order, callback) {
    var html;
    html = jQuery.ajax({url: url, async: false}).responseText;
    if(order==null)
        jQuery(target).append(html);
    else 
        jQuery(target).prepend(html);

    if ((callback != null) && (typeof(callback) == "function")) {
        callback();
    }
}
function setButtonDefault() {
    jQuery('input[type="button"],input[type="submit"], button').each(function () {
        var $bt = jQuery(this);
        if ($bt.hasClass("btn") === false) {
            $bt.addClass("btn").addClass("btn-default");
        }
    });
}

/*Misc function for Table: ajust tbody height due to Max Height for FireFox*/
function ajustTBodyHeight () {
    try {
        var tbody = document.getElementsByTagName("tbody");
        for ( var i = 0 ; i < tbody.length ; i ++ ) {
            var tbodyHeight = tbody[i].clientHeight;
            var maxHeight = parseInt( document.defaultView.getComputedStyle(tbody[i], null).getPropertyValue("max-height") );

            if ( tbodyHeight > maxHeight ) {
                tbody[i].style.height = maxHeight + "px";
            }
        }
    }
    catch ( err ) {
        //handle error
    }
}

function formatMoneyNumber(_this) {
    var number = jQuery(_this).val().replace(/\,/g, '');
    if (jQuery.trim(number) != "" && !isNaN(number)) {
        jQuery(_this).val(parseFloat(number).toLocaleString());
    }
    else {
        jQuery(_this).val("0.00");
    }
}
function validatePhoneNumber(number) {
    var pattern = /^[0-9 -]{0,50}$/;
    return pattern.test(number);
}
function validateNumber(number) {
    var pattern = /^[0-9]{0,50}$/;
    return pattern.test(number);
}
/*function advantageSearch() {
    jQuery('a.SectionMode').each(function (i) {
        jQuery(this).click(function () {
            var $this = jQuery(this);
            if ($this.hasClass('Expand')) {
                $this.removeClass('Expand');
            } else {
                $this.addClass('Expand');
            }
            $this.parent().next().toggle("slow");
        });
    });
}*/
function imposeMaxLength(Object, MaxLen) {
    if (Object.getAttribute && Object.value.length > MaxLen)
        Object.value = Object.value.substring(0, MaxLen);
}
function getHost() {
    var url = window.location.href;
    var host = window.location.origin;
    return url.replace(host, "");
}
jQuery(document).ready(function() {
    var host = getHost();
    setButtonDefault();
    jQuery(".MainNavLink").each(function() {
        var link = jQuery(this).find("a").attr("href");
        if (link == host) {
            jQuery(this).find("a").parent('li').addClass("subActive");
        }
    });
    jQuery('.datepicker').each(function () {
        jQuery(this).datepicker({
            autoclose: true,
            onClose: function () {
                $(this).datepicker('remove');
            }
        });
    });
});