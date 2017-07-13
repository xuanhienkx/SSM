

jQuery(document).ready(function () {
    // Create Special ComboBox
    jQuery(".SelectUI").addSelectUI({
        scrollbarWidth: 15 //default is 10
    });
    //jQuery(this).InitFormat();
    if (jQuery(".fullPage") !== undefined) {
        jQuery(".fullPage").parents(".page").css("width", "100%");
    }
});
function ShowInputBaseData(title, content) {
    jQuery.mbqDialog({
        title: "Nhập thông tin " + title,
        content: content,
        columnClass: 'col-md-7 col-md-offset-2',
        theme: 'bootstrap',
    });
    return false;
}
function DelRow() {
    jQuery("td.userRemove").on('click', function (e) {
        var $tr = jQuery(this).parents("tr:first");
        var id = $tr.attr("id").split("_")[1];
        var listUpdate = jQuery("input[class='listIdUser']").val();
        var stringId = ";" + id + ";";
        if (listUpdate.indexOf(stringId) != -1) {
            listUpdate = listUpdate.replace(stringId, ";");
            listUpdate = listUpdate.replace(";;", ";");
            jQuery("input[class='listIdUser']").val(listUpdate);
            $tr.remove(); 
        }
    });
}

function DelRowEvent() {
    jQuery("td.userRemove").on('click', function (e) {
        var $tr = jQuery(this).parents("tr:first");
        var id = $tr.attr("id").split("_")[1];
        var listUpdate = jQuery("input[class='listEventIdUser']").val();
        var stringId = ";" + id + ";";
        if (listUpdate.indexOf(stringId) != -1) {
            listUpdate = listUpdate.replace(stringId, ";");
            listUpdate = listUpdate.replace(";;", ";");
            jQuery("input[class='listEventIdUser']").val(listUpdate);
            $tr.remove();
        }
    });
}
