jQuery.noConflict();
(function($) {
jQuery.fn.mainNavInit = function() {
    var $this = jQuery(this);
    // init vars
    var myNav = jQuery('#mainNavBar');
    var mWidth = 1;    
    var lWidth = new Array();
    var vWidth = 940;
    var liNum = jQuery('> li',myNav).size();
    var speed = 500;
    var options = {
        duration: speed,
        easing: 'linear'
    };
    var myListContents ='';
    var defWidth;
    var pin = eval(checkPinModeCookie());
    var activePin = false;
    // check width of nav' items
    jQuery('#mainNavBar > li > a > span').each(function(){
        var $this = jQuery(this);
        if ($this.width() > 100) $this.width(100);
    });
    // vertical align
    jQuery('#mainNavBar > li > a > span > strong').each(function(){
        var $this = jQuery(this);
        if ($this.height() > 18)  $this.parent().css({paddingTop: '7px', height: '37px'});
    });
    // carousel in nav init
    jQuery('#mainNavBar > li').each(function(i){
        mWidth += jQuery(this).outerWidth();
        lWidth.push(jQuery(this).outerWidth());
    });
    myNav.width(mWidth);
    myNav.parents(".MainNavHolderL2").width(mWidth+200);
    defWidth = mWidth;
    vWidth = mWidth;
    myNav.wrap('<div class="MainNavContainer"><div id="mainNavMask">');
    // add controls when width is longer than mask
    if (mWidth > vWidth) {
        jQuery('.MainNavContainer').prepend('<a id="mainNavPrev" href="#" title="Previous">Previous</a>').append('<a id="mainNavNext" href="#" title="Next">Next</a>');
        jQuery('#mainNavPrev').click(function() {
            if ( myNav.queue("fx").length > 0 ) return false;
            var mLeft = myNav.position().left;
            var mRight =  vWidth - mLeft - mWidth;                              
            var cWidth = 0;    
            var i = liNum - 1;
            var mSpace = 0;
            while ( cWidth <= vWidth - mRight ) {
                cWidth += lWidth[i];
                i--;
            }
            mSpace = mLeft + (cWidth + mRight - vWidth + 1);
            if (cWidth + mRight - vWidth + 1 < lWidth[i+1]) mSpace += lWidth[i];
            if (mSpace > 0) {
                mSpace = 0;
            }
            myNav.animate({ left: mSpace, position: "relative" }, { duration: speed });
            return false;                        
        });
        jQuery('#mainNavNext').click(function () {
            
            if ( myNav.queue("fx").length > 0 ) return false;
            var mLeft = myNav.position().left;                              
            var cWidth = 0;    
            var i = 0;
            var mSpace = 0;
            while ( cWidth <= vWidth - mLeft ) {
                cWidth += lWidth[i];
                i++;
            }
            mSpace = -1 * (cWidth - vWidth + 1);
            if (cWidth + mLeft - vWidth + 1 < lWidth[i-1]) mSpace -= lWidth[i];
            if (mWidth + mSpace <= vWidth) { 
                mSpace = -1 * (mWidth - vWidth);    
            } 
            myNav.animate({ left: mSpace, position: "relative" }, options);
             return false;
        });
    }
    // init events
    jQuery('#mainNavBar > li:first-child').addClass('FirstItem');
    jQuery('#mainNavBar > li.Active').activeThisNav();

    //set width for each MainNavGroup
    jQuery("#subNav").find("li.MainNavGroup ").each(function () {
        var el = jQuery(this);
        var el_width = 0;

        el.find("> ul > li").each(function () {
            var sub_el = jQuery(this);
            el_width += sub_el.outerWidth(true);
        });
        el.width(el_width);
    });

    jQuery("#mainNavBar").bind("mouseover", function(evt){
    	if(!jQuery("#mainNavBar li").hasClass("Active")) return false;
        if ( $this.queue("fx").length > 0 ) return false;
        var ul = jQuery('#subNav> ul>li>ul');
        if ( !pin && $this.hasClass('Compact') && jQuery("#mainNavBar > li.Active").attr("id") != "mainNavBar_adminTab") {
        	activePin = false;
            $this.removeClass('Compact').animate({
                height: '190px'
            }, function() {
                jQuery('#navMode').show();
                ul.show();
                setTabWidth();
            });
            //makeInternalFooterBarPosition();
        }
        evt.stopPropagation();
    });
    jQuery("#mainNavBar").bind("mouseout", function(evt){
    	if(!jQuery("#mainNavBar li").hasClass("Active")) return false;
    	 if (!pin && activePin && !$this.hasClass('Compact') && jQuery("#mainNavBar > li.Active").attr("id") != "mainNavBar_adminTab" ) {
         	activePin = false;
             $this.addClass('Compact').animate({
                 height: '93px'
             }, function(){
                 jQuery('#navMode').hide();
                 setTabWidth();
             });
             //makeInternalFooterBarPosition();
         } 
    	 evt.stopPropagation();
    });

    jQuery('li.MainNavTab > a').bind("click", function(){
        jQuery(this).parent().activeThisNav();
        return true; //For got to node page
    });

    jQuery("#subNav").bind("mouseover", function(evt){
    	if(!jQuery("#mainNavBar li").hasClass("Active"))return false;
        if ( $this.queue("fx").length > 0 ) return false;
        var ul = jQuery('#subNav> ul>li>ul');
        if ( !pin && $this.hasClass('Compact') && jQuery("#mainNavBar > li.Active").attr("id") != "mainNavBar_adminTab") {
        	activePin = false;
            ul.hide();
            $this.removeClass('Compact').animate({
                height: '190px'
            }, function() {
                jQuery('#navMode').show();
                ul.show();
                setTabWidth();
            });
           // makeInternalFooterBarPosition();
        } 
        evt.stopPropagation();
    });

    jQuery(document).bind("mouseover", function(){
    	if(!jQuery("#mainNavBar li").hasClass("Active")) return false;
        if ( $this.queue("fx").length > 0 ) return false;
        var ul = jQuery('#subNav> ul>li>ul');
        if ( !activePin && !pin && !$this.hasClass('Compact') && jQuery("#mainNavBar > li.Active").attr("id") != "mainNavBar_adminTab") {
            ul.hide();
            $this.addClass('Compact').animate({
                height: '93px'
            }, function(){
                ul.show();
                jQuery('#navMode').hide();
                setTabWidth();
            });
        }
    });

    jQuery('li.MainNavGroup > a').click(function(){
        return false;
    });

    // pin me
    jQuery('#navMode').bind("mouseover", function(evt){
        evt.stopPropagation();
    });
    jQuery('#navMode').bind("click", function(event){
        pin = !pin;
        if ( pin ) {
            jQuery(this).addClass('Active');
            activePin = true;
            setCookie("main_nav_pin_mode", "true");
        }
        else {
            jQuery(this).removeClass('Active');
            activePin = false;
            setCookie("main_nav_pin_mode", "false");
        }
        event.preventDefault();
    });

    if( (jQuery("#subNav > ul> li > ul > li").hasClass("Active") && ( pin == undefined || !pin)) || (jQuery("#mainNavBar > li.Active").attr("id") == "mainNavBar_adminTab") ) {
        //has active in sub and no pin-cookie : short-mode 
        $this.addClass('Compact').animate({
            height: '93px'
        }, function(){
            jQuery('#navMode').removeClass('Active').hide();
            setTabWidth();
        });
    }
    else { //no active in sub or pin-cookie=true : long-mode
    	$this.removeClass('Compact').animate({
            height: '190px'
        }, function() {
            jQuery('#navMode').addClass("Active").show();
            setTabWidth();
        });
        pin = true;
    	activePin = true;
    }
    
    if( !jQuery('#mainNavBar > li').hasClass("Active") ){
    	 $this.addClass('Compact').animate({
             height: '93px'
         }, function(){
             jQuery('#navMode').removeClass('Active').hide();
             setTabWidth();
         });
    }
    
    if( jQuery("#mainNavBar > li.Active").attr("id") == "mainNavBar_adminTab" ) {
		//makeInternalFooterBarPosition();
	}
    		

};
jQuery.fn.activeThisNav = function(){
    jQuery('li.MainNavTab').removeClass('Active');
    jQuery(this).addClass('Active');
    if (jQuery(this).attr('id') != 'mainNavBar_adminTab') {
        jQuery('#subNav').removeClass('Admin').html(jQuery('> ul',this).clone());
        jQuery('#navMode').show();
    } else {
    		setHeghtOfNavigationBar();
            jQuery('#subNav').addClass('Admin').html(jQuery('> ul',this).clone());
            jQuery('#navMode').hide();
    }
    jQuery('#subNav li:first-child').addClass('FirstItem');
    jQuery('#subNav li:last-child').addClass('LastItem');
    jQuery('#subNav li:only-child').addClass('OnlyItem').removeClass('FirstItem').removeClass('LastItem');
};
})(jQuery);

jQuery(function(){
    jQuery('#mainNav').mainNavInit();
    //jQuery('#mainNavBar').css("width", "100%");
});

/* cookies */
//Cookies for basket item
    function setCookie(cookieName, string) {
        jQuery.cookie(cookieName, string, { path: '/' });
    }

    function getCookie(cookieName) {
        if (jQuery.cookie(cookieName) != null) {
            return jQuery.cookie(cookieName);
        }
        else {
            return "";
        }
    }

    function checkPinModeCookie() {
        if (getCookie("main_nav_pin_mode") != "") {
            return getCookie("main_nav_pin_mode");
        }
    }
/* END. cookies */

function setHeightOfNavigationBar() {
	 if(jQuery.browser.msie){
		 jQuery('#mainNav').animate({height: '222px'}).removeClass('Compact');
	 }
	 else {
		 jQuery('#mainNav').animate({height: '210px'}).removeClass('Compact');
	 }
}

function setTabWidth () {
    if ( jQuery('#mainNav').hasClass("Compact") ) {
        jQuery(".MainNavGroup").each(function () {
            var holder = jQuery(this);
            var text = holder.find("a:first");
            var iconHolder = holder.find("> ul");

            if ( holder.width() < text.outerWidth(true) + iconHolder.outerWidth(true) ) {
                text.addClass("ShortMode");
            }
        });
    }
    else {
        jQuery(".ShortMode").removeClass("ShortMode");
    }
}
