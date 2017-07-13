/* jSelect (using jQuery library).
*--------------------------------------------*
*  @author : ukhome ( ukhome@gmail.com | ntkhoa_friends@yahoo.com )
*--------------------------------------------*
*  @released : 24-Mar-2009 : version 1.0
*--------------------------------------------*
*  @revision history : ( latest version : 1.0 )
*--------------------------------------------*
*      + 24-Mar-2009 : version 1.0
*          - released
*      + 03-Sep-2009 : version 1.1
*          - positioning issues fixed (esp. FF3), remove globalWrapper
*		+ 05-Oct-2009 : version 1.1
*          - reset select issues fixed !
*--------------------------------------------*
*/

/* package Main
*/

//droplist Manager
$selectDroplist_Manager = new function () { //Singleton class
    this.els = []; // element type = jquery object
    this.activeName = null; // index of active droplist

    return this;
}

//droplist class
$selectDroplist_UI = function (jEl, options) { //jEl = jquery element
    var o = this; //self reference

    /* methods */

    this.setupDropListUI = function () {
        /*Create UL reflect <select>*/
        var offset = 0;
        o.select.find("> *").each(function (index) {
            var el = jQuery(this);
            var count = index;

            if ( this.tagName.toLowerCase() == "optgroup" ) {
                //detected <optgroup>
                el.each(function () {
                    var optgroup = jQuery(this);

                    var optName = optgroup.attr("label");
                    var optgroup_el = jQuery("<li></li>");
                    optgroup_el.prepend("<span class=\"OptgroupLabel\">" + optName + "</span>");
                    var optgroup_elsubUL = jQuery("<ul></ul>");
                    o.elUL.append(optgroup_el);
                    optgroup_el.append(optgroup_elsubUL);

                    optgroup.find("option").each(function (index) {
                        var self = jQuery(this);
                        if ( self.attr("value") == "" ) {
                            optgroup_elsubUL.append("<li class=\"SelectUITitle\" value=\"" + parseInt(count + index + offset+1) + "\"><a href=\"#\" title=\"" + self.text() + "\" rel=\"" + self.attr("label") + "\">" + self.text() +"</a></li>");
                        }
                        else if ( self.attr("selected") ) {
                            optgroup_elsubUL.append("<li class=\"Active\" value=\"" + parseInt(count + index + offset+1) + "\"><a href=\"#\" title=\"" + self.text() + "\" rel=\"" + self.attr("label") + "\">" + self.text() +"</a></li>");
                        }
                        else {
                            optgroup_elsubUL.append("<li value=\"" + parseInt(count + index + offset+1) + "\"><a href=\"#\" title=\"" + self.text() + "\" rel=\"" + self.attr("label") + "\">" + self.text() +"</a></li>");
                        }
                    });
                    offset += optgroup.find("option").length - 1;
                });
            }
            else {
                //detected <option>
                if ( el.attr("value") == "" ) {
                    o.elUL.append("<li class=\"SelectUITitle\" value=\"" + parseInt(index + offset+1) + "\"><a href=\"#\" title=\"" + el.text() + "\" rel=\"" + el.attr("label") + "\">" + el.text() +"</a></li>");
                }
                else if ( el.attr("selected") ) {
                    o.elUL.append("<li class=\"Active\" value=\"" + parseInt(index + offset+1) + "\"><a href=\"#\" title=\"" + el.text() + "\" rel=\"" + el.attr("label") + "\">" + el.text() +"</a></li>");
                }
                else {
                    o.elUL.append("<li value=\"" + parseInt(index + offset+1) + "\"><a href=\"#\" title=\"" + el.text() + "\" rel=\"" + el.attr("label") + "\">" + el.text() +"</a></li>");
                }
            }
        });

        //append to DOM
        //o.el = jQuery("<div class=\"DropListUIContainer\"></div>");
        //o.elUL.before(o.el);
        o.el.html(o.elUL);
        /*end. Create UL reflect <select>*/

        /*Wrapper*/
        var elClasses = o.elUL.attr("class").split(" ");
        var addDefaultTheme = true;
        for ( var i = 0 ; i < elClasses.length ; i++ ) {
            if ( elClasses[i].match(/^Theme/) ) {
                o.elWrapper.addClass(elClasses[i]);
                addDefaultTheme = false;
            }
        }

        if ( addDefaultTheme ) {
            o.elWrapper.addClass("Theme_Default");
            o.elUL.addClass("Theme_Default");
        }
        /*end. Wrapper*/

        if ( !o.select .attr("multiple") ) {
            //max droplist height
            o.maxDropListHeight = options != undefined && options.maxDropListHeight != undefined ? parseInt(options.maxDropListHeight) : 300;
            o.config = {
                maxDropListHeight: o.maxDropListHeight
            }

            /*Title*/
            var title = "";
            var hasValue = false;
            o.select.find("option").each(function () {
                var option = jQuery(this);

                if ( option.attr("selected") ) {
                    title = option.text();
                    hasValue = true;
                }
            });

            if ( !hasValue ) {
                title = o.select.attr("title") != "" ? o.select.attr("title") : o.select.find("option:first-child").text();
            }
            /*end. Title*/
            
            /*Disabled option*/
            if ( !o.select.attr("disabled") ) {
                o.droplistTITLE.text(title);
                o.elWrapper.removeClass("Disabled");
           }
            else {
                o.droplistTITLE.text("");
                o.elWrapper.addClass("Disabled");
            }
            /*end. Disabled option*/

            o.el.show();
            o.el.css({
                position: "absolute",
                left: 0,
                display: "none",
                overflow: "hidden",
                width: o.elUL.width()
            });
            o.el.hide();

            /*Binding events*/
            o.el.find("ul > li").each(function (index) {
                var self = jQuery(this);
                
                self.bind("click", function () {
                    if ( self.find("span.OptgroupLabel:first-child").length > 0 ) {
                        //o.el.hideList();
                        return false;
                    }
                    else {
                        if ( !o.select.attr("disabled") ) {
                            //o.droplistTITLE.text( self.text() );
                            o.el.find("ul > li").removeClass("Active");
                            self.addClass("Active");
                            o.droplistTITLE.text( self.text() );
                            o.hideList();
							o.select.val(o.select.find("option").eq(self.attr("value")-1).val());
                            /*call Externall Function*/
                            callExternalFunction(o, $selectDroplist_Manager.els, self.find("a:first").attr("rel"));
                            self.removeClass("Hover");
							o.afterCall();
                            return false;
                        }
                    }
                });
            });

            o.el.bind("click", function (evt) {
                return false; //prevent default action and stop bubble
            });
            /*end. Binding events*/
        }
        else { //multi choices enabled
            var size = o.select.attr("size");
            o.elUL.css({
                height: o.elUL.find("li").eq(0).outerHeight(true)*size,
                overflow: "hidden"
            });

            if ( !o.elUL.parent().hasClass("jScrollPaneContainer") ) {
               o.elUL.jScrollPane({
                    scrollbarWidth: o.options.scrollbarWidth,
                    scrollbarOnLeft: o.options.scrollbarSide == "left" ? true : false
               });
            }

            /*Binding events*/
            var keyChar = null;
            var beginVal_INDEX = null;
            var endVal_INDEX = null;

            /*shortcut function*/
            function clearValues () {
                o.select.find("option").removeAttr("selected");
                o.elUL.find("li").removeClass("Active");
            }
            /*END. shortcut function*/

            o.el.find("ul > li").each(function (index) {
                var self = jQuery(this);
                
                self.bind("click", function (e) {
                    if ( self.find("span.OptgroupLabel:first-child").length > 0 ) {
                        //o.el.hideList();
                        return false;
                    }
                    else {
                        if ( !o.select.attr("disabled") ) {
                            if ( e.ctrlKey && !e.shiftKey ) { //only CTRL
                                 /*pre-proccess for SHIFT key case*/
                                beginVal_INDEX = index;
                                /*END. pre-proccess for SHIFT key case*/
                                o.select.find("option").eq(index).attr("selected", "selected");
                            }
                            else if ( (!e.ctrlKey && e.shiftKey) || (e.ctrlKey && e.shiftKey) ) { //only SHIFT or CTRL+SHIFT: SHIFT take the higher priority
                                if ( !e.ctrlKey ) { //if NOT hold CTRL --> clear values
                                    clearValues();
                                }
                                if ( beginVal_INDEX == null ) {
                                    beginVal_INDEX = index;
                                }
                                else {
                                    endVal_INDEX = index;
                                    if ( beginVal_INDEX != null && endVal_INDEX != null ) {
                                        o.el.find("ul > li").each(function (index) {
                                            var self = jQuery(this);
                                            if ( ( beginVal_INDEX <= endVal_INDEX && index >= beginVal_INDEX && index <= endVal_INDEX ) || ( beginVal_INDEX >= endVal_INDEX && index <= beginVal_INDEX && index >= endVal_INDEX ) ) {
                                                o.select.find("option").eq(index).attr("selected", "selected");
                                                self.addClass("Active");
                                            }
                                        });
                                        endVal_INDEX = null;
                                    }
                                }
                            }
                            else { //no key pressed
                                clearValues();
                                o.select.find("option").eq(index).attr("selected", "selected");
                                /*pre-proccess for SHIFT key case*/
                                beginVal_INDEX = index;
                                /*END. pre-proccess for SHIFT key case*/
                            }
                            self.addClass("Active");
                            //o.select.val(o.select.find("option").eq(self.attr("value")-1).val());
                            /*call Externall Function*/
                            //callExternalFunction(o, $selectDroplist_Manager.els, self.find("a:first").attr("rel"));
                            self.removeClass("Hover");
                            return false;
                        }
                    }
                });
            });
        }

        /* Little trick for IE6 Hover Problem */
        o.el.find("ul > li").each(function (index) {
            var self = jQuery(this);
            
            self.bind("mouseover", function () {
                self.addClass("Hover");
                return false;
            });

            self.bind("mouseout", function () {
                self.removeClass("Hover");
                return false;
            });
        });
        /* end. Little trick for IE6 Hover Problem */
    }

    this.reset = function () {
        //refresh
        o.elUL.empty();
        o.elUL.removeAttr("class");
        //re-create
        o.elUL.attr("title", o.select.attr("title"));
        o.elUL.addClass(o.select.attr("class"));

        this.setupDropListUI();

        /*Re-Create UL reflect <select>*/
    }

    this.showList = function () {
        o.elWrapper.addClass("TopLevel");
        o.el.addClass("DropListUIShow");

        o.reservedHolder = o.elWrapper.clone(true).empty();
        o.reservedHolder.css({
            visibility: "hidden",
            height: o.elWrapper.outerHeight()
        });

        o.elWrapper.before(o.reservedHolder);
        
        var borderLeftWidth = 0;
        var borderTopWidth = 0;
        var offset = {
            left: 0,
            top: 0
        };

        o.elWrapper.hide();
        o.elWrapper.appendTo("body"); //must append to body before calculate position

        //var isFF3 = (/Firefox\/3.*/).test(window.navigator.userAgent);
        //!!! if FF3, substract the border-left/top width of all parent wrapper: bugs in offset()

        /*
        if (isFF3) {
            var parentWrapper = o.reservedHolder.parent();
            borderLeftWidth = 0;
            borderTopWidth = 0;
            while (parentWrapper.attr("tagName").toLowerCase() != "body") { //find all borders of parents
                if ( (/relative|absolute|fixed/).test( parentWrapper.css("position") ) ) {
                    borderLeftWidth += parseInt(parentWrapper.css("borderLeftWidth"));
                    borderTopWidth += parseInt(parentWrapper.css("borderTopWidth"));
                }
                parentWrapper = parentWrapper.parent();
            }
            
            var el = o.reservedHolder.get(0);
            do {
                offset.top += el.offsetTop;
                offset.left += el.offsetLeft;
            }
            while ( el = el.offsetParent ) ; //trick
        }
        else {*/
            offset.top = o.reservedHolder.offset().top;
            offset.left = o.reservedHolder.offset().left;					
        //}
        
        o.elWrapper.css({ //set position for droplistUI
            position: "absolute",
            top: offset.top + borderTopWidth,
            left: offset.left + borderLeftWidth,
            margin: 0
        });
        o.elWrapper.show();
        o.el.show();
        o.setDirection();

        //apply jScrollPane for scrolling
        if (o.el.height() > o.maxDropListHeight) {
           o.elUL.height(o.maxDropListHeight);

           if ( !o.elUL.parent().hasClass("jScrollPaneContainer") ) {
               o.elUL.jScrollPane({
                    scrollbarWidth: o.options.scrollbarWidth,
                    scrollbarOnLeft: o.options.scrollbarSide == "left" ? true : false
               });
           }
        }

        o.eventFire = false;
    }

    this.hideList = function () {
        //remove jScrollPane
        o.el.prepend(o.elUL);
        o.elUL.removeAttr("style");
        o.elUL.next().remove();
        //end. remove jScrollPane

        o.elWrapper.removeClass("TopLevel");
        o.el.removeClass("DropListUIShow");
        o.select.after(o.elWrapper.removeAttr("style"));
        o.el.hide();
        o.reservedHolder.remove();
    }
	
	this.afterCall = function () {
		//client call after action
		if ( options.after_action != undefined ) {
			options.after_action();
		}
	}

    this.setDirection = function () {
        //var windowHeight = jQuery(window).height() + jQuery(window).scrollTop();
		var windowHeight = jQuery.browser.safari ? window.innerHeight : jQuery(window).height();
		windowHeight = windowHeight + jQuery(window).scrollTop();
        var elPostion_Top = o.elWrapper.offset().top;
        var elPostion_Bottom = o.elWrapper.offset().top + o.elWrapper.height();
        var elULHeight = o.elUL.outerHeight();
        var direction = "";

         /*  
        * When o.maxDropListHeight change in case 
        * it's height is greater than top space and bottom space,
        * it need to be reset to its config value for new calculation
        */
        if ( o.config.maxDropListHeight > o.maxDropListHeight ) {
            o.maxDropListHeight = o.config.maxDropListHeight;
        }

        if ( elULHeight <= windowHeight - elPostion_Bottom ) { //no need scroll
            //decide to go down
            direction = "down";
			o.el.removeClass("DropListUIContainerUp");
        }
        else if ( windowHeight - elPostion_Bottom > o.maxDropListHeight ) { //need scroll
            //go down take higher priority if available
            direction = "down";
			o.el.removeClass("DropListUIContainerUp");
        }
        else if ( elULHeight < elPostion_Top - jQuery(window).scrollTop() ) { //no need scroll
            //decide to go up
            direction = "up";
			o.el.addClass("DropListUIContainerUp");
        }
        else if ( elPostion_Top - jQuery(window).scrollTop() > o.maxDropListHeight ) { //need scroll
            //go up take priority when down is unavailable (< maxDropListHeight)
            direction = "up";
			o.el.addClass("DropListUIContainerUp");
        }
        else if ( windowHeight - elPostion_Bottom >= elPostion_Top - jQuery(window).scrollTop() ) { //need scroll
            //no case available but go down better than go up
            direction = "down";
			o.el.removeClass("DropListUIContainerUp");
            o.maxDropListHeight = windowHeight - elPostion_Bottom;
        }
        else { //need scroll
            //no case available but go up better than go down
            direction = "up";
			o.el.addClass("DropListUIContainerUp");
            o.maxDropListHeight = elPostion_Top - jQuery(window).scrollTop();
        }
        var borderTop = (/[0-9]+/).test( o.el.css("borderTopWidth") )
                                ? parseInt(o.el.css("borderTopWidth"))
                                : 0;
        var borderBottom = (/[0-9]+/).test( o.el.css("borderBottomWidth") )
                                ? parseInt(o.el.css("borderBottomWidth"))
                                : 0;

        o.maxDropListHeight -= (borderTop + borderBottom);

        /* Act on direction decision */
        if ( direction == "up" ) { //go up
            o.el.css({
                bottom: o.elWrapper.height() + "px",
                top: "auto"
            });
        }
        else { // go down, direction == "down"
            o.el.css({
                top: "100%",
                bottom: "auto"
            });
        }
    }

    /* end. methods */

    /* constructor */

    o.options = {
        scrollbarWidth: options != undefined && options.scrollbarWidth != undefined ? parseInt(options.scrollbarWidth) : 10,
        scrollbarSide: options != undefined && options.scrollbarSide != undefined ? options.scrollbarSide : "right"
    }
    
    /*<select>*/
    o.select = jEl;
    o.select.addClass("HasSelectUI");
    o.select.css({
        opacity: 0,
        position: "absolute",
        left: "-1000em",
        top: "-1000em"
    });
    o.reservedHolder = null;
    o.elUL = jQuery("<ul title=\"" + o.select.attr("title") + "\"></ul>");
    o.elUL.addClass(o.select.attr("class"));
    o.select.before(o.elUL);
    o.el = jQuery("<div class=\"DropListUIContainer\"></div>");
    o.elUL.before(o.el);
    o.el.html(o.elUL);
    o.elWrapper = jQuery("<div class=\"DropListUI\"></div>");
    o.el.before(o.elWrapper);
    o.elWrapper.html(o.el);
    if ( !o.select .attr("multiple") ) {
        o.droplistTITLE = jQuery("<p></p>");
        o.el.before(o.droplistTITLE);
        o.droplistTITLE.bind("click", function (evt) {
			//client call before action
			if ( options.before_action != undefined ) {
				options.before_action();
			}
            o.eventFire = true;

            if ( !o.select.attr("disabled") ) {
                if ( o.el.hasClass("DropListUIShow") ) {
                    o.hideList();
                }
                else { //showlist
                    if ( $selectDroplist_Manager.activeName != null ) {
                        $selectDroplist_Manager.els[$selectDroplist_Manager.activeName].hideList();
                    }
                    o.showList();
                    $selectDroplist_Manager.activeName = o.select.attr("id");
                }
            }
            return false; //prevent default action and stop bubble
        });
    }

    this.setupDropListUI();
    /*end. <select>*/

    /* END. constructor */
}

jQuery.fn.extend({
    addSelectUI: function() {
        if ( $selectDroplist_Manager != undefined ) {
            jQuery(window).bind("resize", function (evt) {
                if ( $selectDroplist_Manager.activeName != null && $selectDroplist_Manager.els[$selectDroplist_Manager.activeName] != undefined && !$selectDroplist_Manager.els[$selectDroplist_Manager.activeName].eventFire ) {
                    $selectDroplist_Manager.els[$selectDroplist_Manager.activeName].hideList();
                }
            });
			jQuery(document).bind("click", function (evt) {
                if ($selectDroplist_Manager.activeName != null) {
                    $selectDroplist_Manager.els[$selectDroplist_Manager.activeName].hideList();
                }
                //evt.stopPropagation();
                //return false;
            });
            jQuery(window).bind("scroll", function (evt) {
                if ( $selectDroplist_Manager.activeName != null && $selectDroplist_Manager.els[$selectDroplist_Manager.activeName] != undefined && !$selectDroplist_Manager.els[$selectDroplist_Manager.activeName].eventFire ) {
                    $selectDroplist_Manager.els[$selectDroplist_Manager.activeName].hideList();
                }
            });
        }

        var options = arguments[0];
        this.each(function () {
            if ( !jQuery(this).hasClass("HasSelectUI") ) {
                jQuery(this).addClass("HasSelectUI");
                $selectDroplist_Manager.els[jQuery(this).attr("id")] =  new $selectDroplist_UI(jQuery(this), options);
            }
        });
    }
});