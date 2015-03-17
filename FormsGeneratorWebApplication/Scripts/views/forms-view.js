;
(function ($, form) {
    'use strict';
    //We will be using strict java script!
    $.extend(form, {

        settings: {

        },

        init: function () {
            var $sample = $('#sample');

            // Dynamic java script command
            $sample.on('mouseover', '.classname', function () {
                samplefunc($(this));
            });
        },
        //Example of a function
        samplefunc: function(selectedObject){
            
        }

    });//extend
})(window.jQuery, window.form || (window.form = {}));

jQuery(function () {
    forms.init();
});