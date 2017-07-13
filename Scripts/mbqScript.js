(function ($) {

    jQuery.fn.extend({
        mbqDialog: function (option) {
            jQuery(this).click(function () {
                jQuery.mbqDialog(option);
            });
        },
        GetAutoSugget: function (option) {

            jQuery(this).autocomplete({
                source: function (request, response) {
                    var data = '{ term: "' + request.term + '"';
                    if (option.params !== undefined) {
                        data = data + option.params;
                    }
                    data = data + '}';
                    jQuery.mbqAjax({
                        url: option.url,
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        data: data,
                        method: "POST",
                        success: function (result) {
                            response(jQuery.map(result, function (item) {
                                return {
                                    id: item.id,
                                    value: item.Display,
                                    all: item,
                                };
                            }));
                        }
                    });
                },
                minLength: option.minLength !== undefined ? option.minLength : 1,
                select: function (e, ui) {
                    if (option.select !== undefined && option.select) {
                        option.select(e, ui);
                    } 
                    else { 
                        var proid = ui.item.id;
                        if (option.targerId !== undefined)
                            jQuery(option.targerId).val(proid);
                    }
                }
            });
        },
        GetProductAutoSugget: function (option) {
            jQuery(this).autocomplete({
                source: function (request, response) {
                    jQuery.mbqAjax({
                        url: option.url,
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        data: '{"term":"' + request.term + '"}',
                        method: "POST",
                        success: function (result) {
                            response(jQuery.map(result, function (item) {
                                return {
                                    id: item.Id,
                                    value: item.Display,
                                    pUnit: item.Other
                                };
                            }));
                        }
                    });
                },
                minLength: option.minLength !== undefined ? option.minLength : 1,
                select: function (e, ui) {
                    var proid = ui.item.id;
                    jQuery("#" + jQuery(this).parents("td:first").find(".ProductId:first").attr("id")).val(proid);
                    jQuery("#" + jQuery(this).parents("tr:first").find(".UOM:first").attr("id")).val(ui.item.pUnit);
                }
            });

        },
        cleanFormat: function () {
            jQuery(this).find(".currency,.number,p.percents").each(function (i) {
                var self = jQuery(this);
                try {
                    var v = self.autoNumeric('get');
                    self.autoNumeric('destroy');
                    self.val(v);
                } catch (err) {
                    //console.log("Not an autonumeric field: " + self.attr("name"));
                }
            });
        },
        InitFormat: function () {

            jQuery(".number2").number(true, 2);
            jQuery(".number").autoNumeric({
                mDec: 2,
                aPad: false,
                wEmpty: 'zero',
            });
            jQuery(".currency3").autoNumeric({
                mDec: 3,
                aPad: false,
                wEmpty: 'zero',
            });
            jQuery(".currency").autoNumeric('init', {
                mDec: 2,
                aPad: false,
                wEmpty: 'zero',
            });
            jQuery(".currencyVn").autoNumeric({
                mDec: 0,
                aPad: false,
                wEmpty: 'zero',
            });

            jQuery('.percent').autoNumeric('init', {
                aSign: ' %',
                pSign: 's',
            });
            jQuery('.currency4').autoNumeric('init', {
                mDec: 4,
                aPad: false,
                wEmpty: 'zero',
            });
            jQuery('.percents').autoNumeric('init', {
                mDec: 2,
                pSign: 's',
            });
            jQuery(".datepicker").datepicker("destroy");
            jQuery(".datepicker").datepicker({
                dateFormat: "dd/mm/yy",
                changeYear: true,
                changeMonth: true,
                showOn: "both",
                buttonImageOnly: true,
                buttonImage: "/Images/bg-date.png",
                buttonText: "Calendar",
                currentText: 'Now',
                autoSize: true,
                gotoCurrent: true,
                showAnim: 'blind',
                highlightWeek: true
            });
        },
        CheckRequired: function () {
            var isValid = true;
            jQuery(this).find(".required").each(function () {
                var valRef = jQuery(this).val();
                if (valRef === "" || valRef === '0') {
                    isValid = false;
                    if (jQuery(this).attr('type') === 'hidden') {
                        jQuery(this).next().addClass("input-validation-error");
                    } else
                        jQuery(this).addClass("input-validation-error");

                } else {
                    if (jQuery(this).attr('type') === 'hidden') {
                        jQuery(this).next().removeClass("input-validation-error");
                    } else
                        jQuery(this).removeClass("input-validation-error");

                }

            });

            return isValid;
        },
        ValidationDate: function () {
            var isValid = true;
            if (jQuery(this).find(".datepicker-check").length > 0) {
                var txtTodate = jQuery(this).find(".datepicker.toDate").val();
                if (txtTodate === "")
                    return true;
                var todate = Date.parse(txtTodate);
                jQuery(this).find(".datepicker.fromDate").each(function () {
                    var txtFormdate = jQuery(this).val();
                    if (txtFormdate !== "") {
                        var date = Date.parse(txtFormdate);
                        if (todate < date) {
                            isValid = false;
                            jQuery(this).addClass("input-validation-error");
                            jQuery('form').find(".datepicker.toDate").addClass("input-validation-error");
                        } else {
                            jQuery(this).removeClass("input-validation-error");
                            jQuery('form').find(".datepicker.toDate").removeClass("input-validation-error");
                            // isValid = true;
                        }
                    }
                });
            }
            return isValid;
        },
        TablResizable: function () {
            jQuery(this).find("thead tr th").resizable({
                handles: "n, e, s, w",
                animateDuration: "fast",
            });
        }

    });

    jQuery.isEmpty = function (val) {
        return (val === undefined || val === null || val.length <= 0) ? true : false;
    };
    //http://craftpip.github.io/jquery-confirm/
    jQuery.mbqDialog = function (option) {
        var style = "";
        var column = 'col-md-10 col-md-offset-1';
        if (option.columnClass !== undefined) {
            column = option.columnClass;
        }
        //if (option.width !== null)
        //    style = " style='margin: 0px auto;width:" + option.width + "px;'";


        $.dialog({
            resizable: true,
            async: false,
            content: option.content,
            columnClass: column,
            title: option.title,
            dialogClass: "modal-dialog",
            backgroundDismiss: option.backgroundDismiss === undefined ? false : option.backgroundDismiss,
            closeIcon: true,
            confirmButton: false,
            cancelButton: false,
            theme: "bootstrap"
        });

    };
    jQuery.mbqConfirm = function (option) {
        var title = false;
        if (option.title !== undefined) {
            title = option.title;
        }
        var columnClass = 'col-md-6 col-md-offset-3';
        if (option.columnClass !== undefined) {
            columnClass = option.columnClass;
        }
        var content = '';
        if (typeof option === 'string') {
            content = option;
            title = 'Xác nhận';
        } else {
            content = option.content;
        }
        $.confirm({
            resizable: true,
            async: false,
            // icon: "fa fa-warning",
            content: content,
            columnClass: columnClass,
            title: title,
            dialogClass: "modal-dialog",
            confirm: function () {
                if (option.confirm !== undefined)
                    return option.confirm();
                else return true;
            },
            cancel: function () {
                if (option.cancel !== undefined)
                    return option.cancel();
                else return true;
            },
            closeIcon: true,
            theme: "bootstrap",
            confirmButton: 'Có',
            cancelButton: 'Không',
            backgroundDismiss: true
        });
    };
    jQuery.mbqAlert = function (settings) {
        var title = 'Thông báo';
        var type = 'info';
        if (settings.type !== undefined && settings.type !== "") {
            type = settings.type;
        }
        if (settings.title !== undefined && settings.title !== "") {
            title = settings.title;
        }
        var columnClass = 'col-md-6 col-md-offset-3';
        if (settings.columnClass !== undefined) {
            columnClass = settings.columnClass;
        }
        if (title.indexOf("Error")>-1 || title.indexOf('error')>-1) {
            type = 'error';
            columnClass = 'col-md-6 col-md-offset-3';
        }
        if (title.indexOf("SuccessFully") > -1 || title.indexOf('success') > -1) { 
            columnClass = 'col-md-6 col-md-offset-3';
        }
       
        var alertSettings = {
            title: title,
            closeIcon: true,
            content: settings.content,
            confirm: function () {
                if (settings.confirm !== undefined)
                    settings.confirm();
                return;
            },
            confirmButton: 'Đóng',
            dialogClass: "modal-dialog",
            backgroundDismiss: true,
            theme: "bootstrap ",
            confirmButtonClass: 'btn btn-primary',
            columnClass: columnClass + " alerbox",
        };
        $.alert(alertSettings);
        jQuery(".jconfirm-box").find(".title-c").addClass(type);
        //jQuery(".jconfirm-box").find(".content").addClass(type);
    }
})(jQuery);

// start page
jQuery(document).ready(function () {
    jQuery(this).cleanFormat();
    AjaxGlobalHandler.Initiate(options);
    jQuery.mbqUnBlockUI();
    jQuery(document).InitFormat();

    jQuery("form").submit(function (e) {
        jQuery("input[disabled]").each(function () {
            jQuery(this).removeAttr("disabled");
        });
        jQuery(this).cleanFormat();
        return true;
    });
    (jQuery(".ckEditor")).each(function (idx, el) {
        CKEDITOR.replace(el, {
            customConfig: '/Scripts/ckeditor/config.js'
        });
    });

});
function checkSaleQty(frm) {
    var isValid = true;
    jQuery(frm).find(".CurrentQty").each(function () {
        var $tr = jQuery(this).parents("tr:first");
        var $qtyelm = $tr.find(".Quantity:first");
        var qty = parseInt($qtyelm.val().replace(/\,/g, ''));
        var currentQty = parseInt(jQuery(this).val().replace(/\,/g, ''));
        var produc = jQuery($tr).find(".ProductCode").val();
        var warehouse = jQuery($tr).find(".WarehouseId").text();
        if (qty > currentQty) {
            $qtyelm.addClass("Warning");
            isValid = false;
            return;
        }
    });
    return isValid;
}
