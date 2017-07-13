var selDiv = "";
var storedFiles = [];
jQuery(document).ready(function () {
    if (CKEDITOR.instances.Message)
        CKEDITOR.instances.Message.destroy(true);
    if (jQuery("#Message").length > 0)
        CKEDITOR.replace('Message',
            {
                fullPage: true,
                extraPlugins: 'autogrow',
                autoGrow_maxHeight: 800,
                customConfig: '/Scripts/ckEditorConfig.js'
            });
    jQuery("#btn-selectFile").on('click', function () {
        jQuery("#files").click();
    });
    jQuery("#files").on("change", handleFileSelect);
    jQuery("body").on("click", ".selFile", removeFile);
    jQuery("#btn-submit").on("click", function (e) { handleForm(e); });
});

function handleFileSelect(e) {
    var files = e.target.files;
    var filesArr = Array.prototype.slice.call(files);
    if (files.length > 0) {
        jQuery("#attachment-show,#attachment").css("display", "block");
        filesArr.forEach(function (f) {
            if (f.size > 20 * 1024 * 1024) {
                alert("File size must under 20mb!");
                return;
            }
            storedFiles.push(f);
            var reader = new FileReader();
            reader.onload = function (ev) {
                var tr = document.createElement('tr');
                tr.innerHTML = ['<td class="fileItem">', escape(f.name), '</td><td class="size-file">', Math.round(f.size / 1024, 0),
                    'KB</td> <td class="selFile" data-file="', f.name, '" ><i class="fa fa-remove" style="color: red"></i></td>'].join('');
                document.getElementById('listfileView').appendChild(tr, null);

            };
            reader.readAsDataURL(f);
        });
    } else {
        jQuery("#attachment-show,#attachment").hide();
    }
}

function removeFile(e) {
    var file = jQuery(this).data("file");
    for (var i = 0; i < storedFiles.length; i++) {
        if (storedFiles[i].name === file) {
            storedFiles.splice(i, 1);
            break;
        }
    }
    jQuery(this).parent().remove();
}

function handleForm(e) {
    if (jQuery(".jconfirm-box").find(".content").find("#Message").length > 0) {
        var contentsData = CKEDITOR.instances.Message.getData();
        jQuery(".jconfirm-box").find(".content").find("#Message").val(contentsData);
    }

    var form = document.getElementById('frm');
    var formAction = form.action;
    var postData = new FormData(form); // form.serialize();
    for (var i = 0, len = storedFiles.length; i < len; i++) {
        postData.append('Uploads', storedFiles[i]);
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
            var obj = JSON.parse(result);
            if (obj.Success === true) {
                jQuery("#btn-submit").parents(".jconfirm-box:first").find('div.closeIcon').click();
                if (jQuery("#email-refresh").length > 0) {
                    jQuery("#email-refresh").click();
                }
            } else {
                jQuery('#btn-submit').parents(".jconfirm-box:first").find(".content").html(obj.View);
                jQuery.mbqAlert({
                    title: "Error",
                    content: obj.Message,
                    columnClass: 'col-md-6 col-md-offset-2',
                    theme: 'bootstrap',
                    type: 'error'
                });
            }
        }
    });
}
 