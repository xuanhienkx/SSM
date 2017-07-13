/*
Copyright (c) 2003-2012, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
*/

CKEDITOR.editorConfig = function (config) {
    // Define changes to default configuration here. For example:
    config.language = 'vi';
    config.uiColor = '#E0E0E0';
    config.autoGrow_bottomSpace = 50;
   // config.height = 300;
    config.toolbar_Full = [
                    ['Bold', 'Italic', '-', 'NumberedList', 'BulletedList', '-', 'Link', 'Unlink', '-', 'Source', 'Templates', 'Image', 'Flash', 'Table', 'SpecialChar'],
                    '/',
					['Style','Format','Font','FontSize', 'TextColor', 'BGColor'], ['Save', '-', 'Preview', '-', 'Print'],
                    ['Undo', 'Redo'], ['Cut', 'Copy', 'Paste', 'PasteFromWord', 'SelectAll'],
                    ['Find', 'Replace']
    ];
};
 