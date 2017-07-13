/*
* Xuan phap design
* code js render list file input before upload file
*
*
*/
var selDiv = "";
var storedFiles = [];

jQuery(document).ready(function () {
    if (CKEDITOR.instances.Contents)
        CKEDITOR.instances.Contents.destroy(true);
    selDiv = document.querySelector("#listfileView");
    jQuery("#files").on("change", handleFileSelect);
    jQuery('#btnAdd').click(function (e) {
        jQuery('#ListUser > option:selected').appendTo('#ListUserAccesses');
        e.preventDefault();
    });
    jQuery('#btnAddAll').click(function (e) {
        jQuery('#ListUser > option').appendTo('#ListUserAccesses');
        e.preventDefault();
        //jQuery("#ListUserAccesses option").prop("selected", true);

    });
    jQuery('#btnRemove').click(function (e) {
        jQuery('#ListUserAccesses > option:selected').appendTo('#ListUser');
        e.preventDefault();
    });
    jQuery('#btnRemoveAll').click(function (e) {
        jQuery('#ListUserAccesses > option').appendTo('#ListUser');
        e.preventDefault();
    });
    jQuery("#btn-selectFile").on('click', function () {
        jQuery("#files").click();
    });

    CKEDITOR.replace('Contents',
                  {
                      fullPage: true,
                      extraPlugins: 'autogrow',
                      autoGrow_maxHeight: 800,
                      customConfig: '/Scripts/ckEditorConfig.js'
                  });

    jQuery("body").on("click", ".selFile", removeFile);
    jQuery("#btn-update").click(function (e) {
        handleForm(e);
    });
    jQuery(".delete-file").on('click', function (e) {
        e.preventDefault();
        var $td = jQuery(this).parent('td.tdfile');
        var fileId = parseFloat($td.attr('id'));
        var urlAction = "/OutNew/DeleteFile/" + fileId;
        jQuery.mbqAjax({
            method: "POST",
            dataType: 'json',
            contentType: false,
            processData: false,
            url: urlAction,
            success: function (result) {
                jQuery($td).parents('tr:first').remove();
            }
        });

    });
    jQuery("#IsAllowAnotherUpdate").on('change', function () {
        if (!jQuery(this).is(":checked")) {
            jQuery('.listUserUpdate').removeClass('show').addClass('hidden');
            jQuery('#listUsercanupdate').removeClass('show').addClass('hidden');
        } else {
            jQuery('.listUserUpdate').removeClass('hidden').addClass('show');
            jQuery('#listUsercanupdate').removeClass('hidden').addClass('show');
        }
         
    });
    jQuery("#addUserUpdate").on('click', function (e) {
        e.preventDefault();
        var html = jQuery("#AllUserList").html();
        jQuery.mbqDialog({
            title: "Chose user for can update",
            content: html,
            columnClass: 'col-md-8 col-md-offset-3',
        });
        AddorUserCanUpdate();
        RemoveUserCanUpdate();
    });
    jQuery('span.deleteRow').click(function () {
        jQuery(this).parents('tr:first').remove();
        return false;
    });
});

function handleFileSelect(e) {
    var files = e.target.files;
    var filesArr = Array.prototype.slice.call(files);
    if (files.length > 0) {
        jQuery("#attachment-show").css("display", "block");
        filesArr.forEach(function (f) {
            // rule filter
            //if (!f.type.match("image.*")) {
            //    return;
            //} 
            storedFiles.push(f);
            var reader = new FileReader();
            reader.onload = function (ev) {
                var tr = document.createElement('tr');
                tr.innerHTML = ['<td class="fileItem">', escape(f.name), '</td><td class="size-file">', Math.round(f.size / 1024, 0),
                    'KB</td> <td class="selFile" data-file="', f.name, '" ><i class="ui-icon ui-icon-close"></i></td>'].join('');
                document.getElementById('listfileView').appendChild(tr, null);

            };
            reader.readAsDataURL(f);

        });
    } else {
        jQuery("#attachment-show").hide();
    }

}
function handleForm(e) {

    var contentsData = CKEDITOR.instances.Contents.getData();
    jQuery("#ListUserAccesses option").prop("selected", true);
    jQuery("#Contents").val(contentsData);
    var form = document.getElementById('frmInfo');
    var formAction = form.action;
    var postData = new FormData(form);// form.serialize(); 
    for (var i = 0, len = storedFiles.length; i < len; i++) {
        postData.append('filesUpdoad', storedFiles[i]);
    }

    e.preventDefault();
    jQuery.mbqAjax({
        method: "POST",
        dataType: 'html',
        contentType: false,
        processData: false,
        url: formAction,
        data: postData,
        success: function (result) {
            if (formAction.indexOf("Infomation") > 0) {
                window.location = '/Infomation/Index';
            } else
                jQuery("#dataInfo").html(result);
        }
    });


}
function removeFile(e) {
    var file = jQuery(this).data("file"); 
    for (var i = 0; i < storedFiles.length; i++) {
        if (storedFiles[i].name === file) {
            storedFiles.splice(i, 1);
            break;
        }
    }
    //jQuery("#files").val(storedFiles);
    jQuery(this).parent().remove();
}

function AddorUserCanUpdate() {
    jQuery("span.addRow").unbind('click');
    //jQuery("span.deleteRow").unbind('click');
    jQuery("span.addRow").on('click', function (e) {
        e.preventDefault();
        var $tr = jQuery(this).parents("tr:first");
        var id = $tr.attr("id").split("_")[1];
        var fullName = $tr.find('td').eq(0).text();
        var departName = $tr.find('td').eq(1).text();
        var $newTr = '<tr id="row_' + id + '"><td>' + fullName + '</td>  <td>' + departName + '</td> <td> ' +
            ' <span class="deleteRow"><i class="ui-icon ui-icon-circle-triangle-w" style="color: red"></i> </span></td> </tr>';
        jQuery(".jconfirm-box").find("#tbody_Usercanupdates").find("tr#emptyrow").remove();
        jQuery(".jconfirm-box").find("#tbody_Usercanupdates").append($newTr);
        jQuery(this).parents('tr:first').remove();
        var listUpdate = jQuery("#UsersCanUpdate").val(); 
        listUpdate += ";" + id; 
        listUpdate = listUpdate + ";";
        listUpdate = listUpdate.replace(";;", ";");
        jQuery("#UsersCanUpdate").val(listUpdate);
        var html = jQuery(".jconfirm-box").find(".content").html();
        e.stopPropagation();
        jQuery(this).off('click');
        jQuery('span.deleteRow').off('click');
        jQuery(this).unbind();
        jQuery('span.deleteRow').unbind();
        jQuery("#AllUserList").html(html);
        addorRemoeListUserUpdate(id, fullName);
        RemoveUserCanUpdate();
        return false;
    });

}

function RemoveUserCanUpdate(parameters) {
    jQuery("span.deleteRow").on('click', function (e) {
        e.preventDefault();
        var $tr = jQuery(this).parents("tr:first");
        var id = $tr.attr("id").split("_")[1];
        var fullName = $tr.find('td').eq(0).text();
        var departName = $tr.find('td').eq(1).text();
        var $newTr = '<tr id="row_' + id + '"><td>' + fullName + '</td>  <td>' + departName + '</td> <td> ' +
            ' <span class="addRow"><i class="ui-icon ui-icon-circle-triangle-e" style="color: red"></i> </span></td> </tr>';
        jQuery(".jconfirm-box").find("#tbody_UserFees").append($newTr);
        jQuery(this).parents('tr:first').remove();
        var listUpdate = jQuery("#UsersCanUpdate").val() + ";";
        listUpdate = listUpdate.replace(";" + id + ";", ";").replace(";;", ";");
        jQuery("#UsersCanUpdate").val(listUpdate);
        var html = jQuery(".jconfirm-box").find(".content").html();
        e.stopPropagation();
        jQuery(this).off('click');
        jQuery('span.addRow').off('click');
        jQuery(this).unbind();
        jQuery('span.addRow').unbind();
        jQuery("#AllUserList").html(html);

        addorRemoeListUserUpdate(id, fullName);
        AddorUserCanUpdate();
        return false;
    });
}
function addorRemoeListUserUpdate(id, fullName) {

    var ul = jQuery("#listUsercanupdate").find("ul").length;
    if (ul <= 0) {
        var $ul = ' <ul id="userUpdateList"></ul>';
        jQuery("#listUsercanupdate").append($ul);
    }
    var $li = jQuery("#userUpdateList").find("#" + id);
    if ($li.attr("id") !== id) {
        var li = '<li class="list-group-item list-group-item-success" id="' + id + '" >' + fullName + '</li>';
        jQuery("#userUpdateList").append(li);
    } else {
        jQuery("#userUpdateList").find("li#" + id).remove();
    }
}