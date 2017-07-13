var $ = jQuery.noConflict();

function addError(item, error) {
    var $item = $(item);
    $item.parent().append("<span class='errors' style='color:red;'><br/>" + error + "</span>");
}

function CleanErrors(modifyForm) {
    var $ModifyForm = modifyForm;
    var $errors = $ModifyForm.find('span[class=errors]').remove();
}



var Customer = {

    SubmitForm: function () {
        var result = true;
        var $ModifyForm = jQuery('#ModifyForm');
        CleanErrors($ModifyForm);
        var $CompanyName = $ModifyForm.find('#CompanyName');
        var $FullName = $ModifyForm.find('#FullName');
        if (jQuery.trim($CompanyName.val()) == '') {
            result = false;
            addError($CompanyName, "Please input AbbName");
        }
        if (jQuery.trim($FullName.val()) == '') {
            result = false;
            addError($FullName, "Please input Customer Name");
        }
        return result;
    }

};


var Supplier = {

    SubmitForm: function () {
        var result = true;
        var $ModifyForm = jQuery('#ModifyForm');
        CleanErrors($ModifyForm);
        var $CompanyName = $ModifyForm.find('#CompanyName');
        var $FullName = $ModifyForm.find('#FullName');
        if (jQuery.trim($CompanyName.val()) == '') {
            result = false;
            addError($CompanyName, "Please input AbbName");
        }
        if (jQuery.trim($FullName.val()) == '') {
            result = false;
            addError($FullName, "Please input FullName");
        }
        return result;
    }

};
var Service = {

    SubmitForm: function () {
        var result = true;
        var $ModifyForm = jQuery('#ModifyForm');
        CleanErrors($ModifyForm);
        var $CompanyName = $ModifyForm.find('#SerivceName');
        var $FullName = $ModifyForm.find('#Name');
        if (jQuery.trim($CompanyName.val()) == '') {
            result = false;
            addError($CompanyName, "Please input Serivce Name");
        }
        if (jQuery.trim($FullName.val()) == '') {
            result = false;
            addError($FullName, "Please input Name");
        }
        return result;
    }

};
var Product = {

    SubmitForm: function () {
        var result = true;
        var $ModifyForm = jQuery('#ModifyForm');
        CleanErrors($ModifyForm);
        var $Code = $ModifyForm.find('#Code');
        var $Name = $ModifyForm.find('#Name');
        var $NameE = $ModifyForm.find('#Uom');
        var $SupplierId = $ModifyForm.find('#SupplierId');
        if (jQuery.trim($Code.val()) == '') {
            result = false;
            addError($Code, "Please input Code");
        }
        if (jQuery.trim($Name.val()) == '') {
            result = false;
            addError($Name, "Please input product name");
        }

        if (jQuery.trim($NameE.val()) == '') {
            result = false;
            addError($NameE, "Please input product unit");
        }
        if (jQuery.trim($SupplierId.val()) == '0') {
            result = false;
            addError($SupplierId, "Supplier not blank or is not valid");
        }
        $ModifyForm.find('#Code').val($Code.val().trim());
        return result;
    }

};

var Warehouse = {
    SubmitForm: function () {
        var result = true;
        var $ModifyForm = jQuery('#ModifyForm');
        CleanErrors($ModifyForm);
        var $CompanyName = $ModifyForm.find('#Code');
        var $FullName = $ModifyForm.find('#Name');
        if (jQuery.trim($CompanyName.val()) == '') {
            result = false;
            addError($CompanyName, "Please input Code");
        }
        if (jQuery.trim($FullName.val()) == '') {
            result = false;
            addError($FullName, "Please input Name");
        }
        return result;
    }
};
var Carrier = {

    gotoItem: function (item) {
        var href = window.location.href;
        href = href.split('#')[0];
        window.location = href + '#' + item;
    },

    SubmitForm: function () {

        var result = true;
        var $ModifyForm = $('#ModifyForm');
        CleanErrors($ModifyForm);
        var $CompanyName = $ModifyForm.find('#AbbName');
        var $FullName = $ModifyForm.find('#CarrierAirlineName');
        if (jQuery.trim($CompanyName.val()) == '') {
            result = false;
            addError($CompanyName, "Please input AbbName");
        }
        if (jQuery.trim($FullName.val()) == '') {
            result = false;
            addError($FullName, "Please input Name");
        }
        return result;
    },

    SubmitAgentForm: function () {

        var result = true;
        var $ModifyForm = $('#ModifyForm');
        CleanErrors($ModifyForm);
        var $CompanyName = $ModifyForm.find('#AbbName');
        var $FullName = $ModifyForm.find('#AgentName');
        if (jQuery.trim($CompanyName.val()) == '') {
            result = false;
            addError($CompanyName, "Please input AbbName");
        }
        if (jQuery.trim($FullName.val()) == '') {
            result = false;
            addError($FullName, "Please input Agent Name");
        }
        return result;
    }

};

var Valid = {
    Carrier: function () {
        var result = true;
        var $ModifyForm = jQuery('#ModifyForm');
        CleanErrors($ModifyForm);
        var $CompanyName = $ModifyForm.find('#AbbName');
        var $FullName = $ModifyForm.find('#CarrierAirLineName');
        if (jQuery.trim($CompanyName.val()) == '') {
            result = false;
            addError($CompanyName, "Please input AbbName");
        }
        if (jQuery.trim($FullName.val()) == '') {
            result = false;
            addError($FullName, "Please input Name");
        }
        return result;
    },
    Area: function () {
        var result = true;
        var $ModifyForm = jQuery('#ModifyForm');
        CleanErrors($ModifyForm);
        var $AreaAddress = $ModifyForm.find('#AreaAddress');
        var $CountryId = $ModifyForm.find('#CountryId');
        if (jQuery.trim($AreaAddress.val()) == '') {
            result = false;
            addError($AreaAddress, "Please input Province/City");
        }
        if (jQuery.trim($CountryId.val()) == '') {
            result = false;
            addError($CountryId, "Please input Country");
        }
        return result;
    },
    Agent: function () {

        var result = true;
        var $ModifyForm = jQuery('#ModifyForm');
        CleanErrors($ModifyForm);
        var $CompanyName = $ModifyForm.find('#AbbName');
        var $FullName = $ModifyForm.find('#FullName');
        if (jQuery.trim($CompanyName.val()) == '') {
            result = false;
            addError($CompanyName, "Please input AbbName");
        }
        if (jQuery.trim($FullName.val()) == '') {
            result = false;
            addError($FullName, "Please input Agent Name");
        }
        return result;
    },
    Country: function () {
        var result = true;
        var $ModifyForm = jQuery('#ModifyForm');
        CleanErrors($ModifyForm);
        var $CompanyName = $ModifyForm.find('#CompanyName');
        if (jQuery.trim($CompanyName.val()) == '') {
            result = false;
            addError($CompanyName, "Please input Company Name");

        }
        return result;
    },
    Unit: function () {
        var result = true;
        var $ModifyForm = jQuery('#ModifyForm');
        CleanErrors($ModifyForm);
        var $CompanyName = $ModifyForm.find('#Unit1');
        if (jQuery.trim($CompanyName.val()) == '') {
            result = false;
            addError($CompanyName, "Please input Unit");

        }
        return result;
    },
    Group: function () {
        var result = true;
        var $ModifyForm = jQuery('#ModifyForm');
        CleanErrors($ModifyForm);
        var $CompanyName = $ModifyForm.find('#Name');
        if (jQuery.trim($CompanyName.val()) == '') {
            result = false;
            addError($CompanyName, "Please input group Name");

        }
        return result;
    },
}