;
(function ($, form) {
    'use strict';
    //We will be using strict java script!
    $.extend(form, {

        settings: {

        },

        arrayCount: 0,

        init: function () {
            var $formname = $('#top-div');
            var $itemZone = $('#item-zone');

            $formname.on('click', '.btn-download', function () {
                form.download($(this));
            });

            $formname.on('click', '.btn-edit', function () {
                form.edit($(this));
            });

        },



        download: function (selectedObject) {
            var guid = selectedObject.attr('name');
            location.href = form.settings.UrlDownload + '?guid=' + guid;

        },
        edit: function (selectedObject) {
            var guid = selectedObject.attr('name');
            location.href = form.settings.UrlEdit + '?guid=' + guid;

        }

    });//extend
})(window.jQuery, window.form || (window.form = {}));
