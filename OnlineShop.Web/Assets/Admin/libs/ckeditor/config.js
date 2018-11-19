/**
 * @license Copyright (c) 2003-2018, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see https://ckeditor.com/legal/ckeditor-oss-license
 */

CKEDITOR.editorConfig = function (config) {
    // Define changes to default configuration here. For example:
    // config.language = 'fr';
    // config.uiColor = '#AADC6E';

    config.filebrowserBrowseUrl = '/Assets/Admin/libs/ckfinder/ckfinder.html';
    config.filebrowserUploadUrl = '/Assets/Admin/libs/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files';
    config.filebrowserWindowWidth = '1000';
    config.filebrowserWindowHeight = '700';

    //config.filebrowserBrowseUrl = '/Assets/Admin/libs/ckfinder/ckfinder.html';
    //config.filebrowserImageBrowseUrl = '/Assets/Admin/libs/ckfinder/ckfinder.html?type=Images';
    //config.filebrowserUploadUrl = '/Assets/Admin/libs/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files';
    //config.filebrowserImageUploadUrl = '/Assets/Admin/libs/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images';
};
