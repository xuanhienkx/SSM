var isBlock = false;
var waiting = '<div class="progress-container"> <div class="progress"> <div class="progress-bar"> <div class="progress-shadow"></div> </div> </div> </div> ';
var options = {
    AjaxWait: {
        AjaxWaitMessage: waiting,
        AjaxWaitMessageCss: { top: "60px", border: "none" },
        Index: 999999
    },
    AjaxErrorMessage: "<h6>Error! please contact the monkey!</h6>",
    SessionOut: {
        StatusCode: 401,
        RedirectUrl: '/Shipment/Index/0'
    }
};

var AjaxGlobalHandler = {
    Initiate: function (options) {
        jQuery.ajaxSetup({
            async: false,
            cache: false,
        });

        // Ajax events fire in following order
        jQuery(document).ajaxStart(function (data) {
            if (!isBlock) {
                jQuery.mbqBlockUI();
            }
        })
            .ajaxError(function (e, xhr, opts, thrownError) {

                if (options.SessionOut.StatusCode === xhr.status) {
                    document.location.replace(options.SessionOut.RedirectUrl);
                    return;
                }
                if (0 === xhr.status && thrownError === "canceled")
                    return;
                if (500 === xhr.status) {
                    jQuery(".closeIcon").click();
                }
                bindErrors(xhr.status + ": " + thrownError, options);
                jQuery.mbqUnBlockUI();
            })
            .ajaxStop(function () {
                jQuery.mbqUnBlockUI();
            })
            .ajaxSend(function (e, xhr, opts) {

            })
            .ajaxSuccess(function (e, xhr, opts) {
                jQuery.mbqUnBlockUI();
                setButtonDefault();
            }).ajaxComplete(function (e, xhr, opts) {
                jQuery.mbqUnBlockUI();
                setButtonDefault();
            });
    }
};

function bindErrors(data, settings) {
    var errs = data;
    if (data.value !== undefined)
        errs = data.value;
    if (settings.error !== undefined) {
        settings.error(errs);
    } else if (settings.targetError != undefined && settings.targetError.length > 0) {
        settings.targetError.html(errs);
    } else {

        var $ctx = '<div class="row alert alert-error">Lỗi</div>';
        if (Object.prototype.toString.call(errs) === '[object Array]') {
            var ul = '<ul class="error">';
            for (var i = 0; i < errs.length; i++) {
                var li = '<li>' + errs[i] + '</li>';
                ul += li;
            }
            ul += '</ul>';
            $ctx = $ctx.replace('Lỗi', ul);
        } else {
            $ctx = errs;
        }
        jQuery.mbqAlert({
            title: 'Error',
            type: 'error',
            content: $ctx
        });
    }
}

function handleAjax(option) {
    var mode = option.method != undefined ? option.method : option.data != null ? "POST" : "GET";
    jQuery.ajax({
        url: option.url,
        type: mode,
        processData: option.processData == undefined ? {} : option.processData,
        contentType: option.contentType == undefined ? {} : option.contentType,
        dataType: option.dataType == undefined ? {} : option.dataType,
        data: option.data == undefined ? {} : option.data,
        success: function (data, status, xhr) {
            // danh cho delete row of grid table
            if (data.value != undefined && data.value.IsRemve != undefined) {
                if (data.value.IsRemve == true) {
                    var $td = jQuery("td#" + data.value.TdId);
                    if ($td != undefined && $td.length > 0) {
                        $td.parent('tr:first').remove();
                    }
                }
                if (data.value.IsRefreshList != undefined && data.value.IsRefreshList === true) {
                    jQuery("#btn-search").click();
                } 
            }
            if (data.value != undefined && data.value.CloseOther != undefined && data.value.CloseOther === true) { 
                jQuery(document).find(".jconfirm-box").find(".closeIcon").each(function () { 
                    jQuery(this).click();
                });
            }
            // event remove follow user
            if (data.value != undefined && data.value.FormClose != undefined && data.value.FormClose === true) { 
                jQuery("form").parents(".jconfirm-box:first").find(".closeIcon").click();
                if (jQuery(".jconfirm-box").find("#btn-dialogRefesh").length > 0) {
                    jQuery("#btn-dialogRefesh").find("a.fa-refresh").click();
                } else {
                    jQuery("#btn-pageSearch").click();
                }
            }
            if (data.value != undefined && data.value.Redirect != undefined && data.value.Redirect === true) {
                window.location = data.value.Url;
            }

            if (option.success && option.success !== undefined)
                jQuery.mbqAjaxSuccess(data, option);
            else {
                var columnClass = 'col-md-10 col-md-offset-2';
                if (data.success === true) {
                    
                    if (data.dialog !== undefined && data.dialog == true) {
                        var title = data.value.Title;
                        if (title.indexOf("SuccessFully") > 0 || title.indexOf('success') > 0) {
                            columnClass = 'col-md-6 col-md-offset-3';
                        }
                        if (data.value.ColumnClass !== undefined) {
                            columnClass = data.value.ColumnClass;
                        }

                        jQuery.mbqDialog({
                            content: data.value.Views,
                            title: data.value.Title,
                            columnClass: columnClass,
                        });

                        jQuery.mbqGetFuction("", ["data", "status", "xhr"]).apply(this, arguments);
                    } else {
                        jQuery('#' + option.targetId).html(data.value);
                        jQuery.mbqGetFuction("", ["data", "status", "xhr"]).apply(this, arguments);
                        jQuery('#' + option.targetId).InitFormat();
                    }
                } else if (data.success === false) {
                    columnClass = 'col-md-6 col-md-offset-2';
                    if (data.dialog !== undefined && data.dialog == true) {
                        var title = data.value.Title;
                        if (title.indexOf("SuccessFully") > 0 || title.indexOf('success') > 0) {
                            columnClass = 'col-md-6 col-md-offset-3';
                        }
                        if (data.value.ColumnClass !== undefined) {
                            columnClass = data.value.ColumnClass;
                        }

                        jQuery.mbqAlert({
                            content: data.value.Views,
                            title: data.value.Title,
                            columnClass: columnClass,
                        });

                        jQuery.mbqGetFuction("", ["data", "status", "xhr"]).apply(this, arguments);
                    } else {
                        bindErrors(data, { targetError: jQuery('#' + option.targetId) });
                        jQuery('#' + option.targetId).addClass('text-danger');
                    }
                } else {
                    jQuery('#' + option.targetId).html(data);
                }
            }
            jQuery.mbqUnBlockUI();
        }
    });
}

(function ($) {
    jQuery.mbqGetFuction = function (code, argNames) {
        var fn = window, parts = (code || "").split(".");
        while (fn && parts.length) {
            fn = fn[parts.shift()];
        }
        if (typeof (fn) === "function") {
            return fn;
        }
        argNames.push(code);
        return Function.constructor.apply(null, argNames);
    };
    jQuery.mbqAjaxSuccess = function (data, settings) {
        if (data.success !== undefined && data.success === false)
            bindErrors(data, settings);
        else if (settings.success !== undefined)
            settings.success(data);
        else if (settings.target !== undefined)
            if (data.value !== undefined) {
                settings.target.html(jQuery.parseHTML(data.value));
            } else {
                settings.target.html(jQuery.parseHTML(data));
            }
    };
    jQuery.mbqAjax = function (option) {
        if (option.confirm !== undefined && option.confirm.content !== undefined) {
            jQuery.mbqConfirm({
                content: option.confirm.content,
                title: option.confirm.confimTitle != undefined ? option.confirm.confimTitle : "Xác nhận",
                confirm: function (obj) {
                    handleAjax(option);
                }
            });
        } else {
            handleAjax(option);
        }
    };
    jQuery.fn.extend({
        //for ajax link
        mbqAjax: function () {
            var target = jQuery(this).data("ajax-update");
            if (target !== undefined && target !== null) {
                target = target.replace("#", '');
            }
            var contenttype = jQuery(this).data("ajax-contenttype");
            var datatype = jQuery(this).data("ajax-datatype");
            var url = jQuery(this).attr("data-ajax-url");
            jQuery.mbqAjax({
                url: url,
                dataType: datatype,
                contentType: contenttype,
                confirm: { title: "Xác nhận", content: jQuery(this).data("ajax-confirm") },
                targetId: target,
                method: jQuery(this).attr("data-ajax-method"),
            });
            return false;
        }
    });
    jQuery.mbqBlockUI = function () {
        if (!isBlock) {
            jQuery("#waitting").append(waiting);
            jQuery("#waitting").show();
            isBlock = true;
            jQuery("#waitting").css("z-index", 99999999);
        }
    }
    jQuery.mbqUnBlockUI = function () {
        jQuery("#waitting").css("z-index", -1);
        jQuery("#waitting").html("");
        jQuery("#waitting").hide();
        isBlock = false;
    }

})(jQuery);